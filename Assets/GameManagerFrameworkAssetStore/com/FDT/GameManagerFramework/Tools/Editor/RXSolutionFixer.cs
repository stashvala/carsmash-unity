using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System;

/// <summary>
/// original version available in https://gist.github.com/MattRix/d3b602ef002c6dae9580
/// </summary>

class RXSolutionFixer : AssetPostprocessor 
{
	public static bool SHOULD_REMOVE_UNITYPROJ = true;

	private static void OnGeneratedCSProjectFiles() //secret method called by unity after it generates the solution
	{
		string currentDir = Directory.GetCurrentDirectory();
		string[] slnFiles = Directory.GetFiles(currentDir, "*.sln");
		string[] csprojFiles = Directory.GetFiles(currentDir, "*.csproj");
		
		foreach(var filePath in slnFiles)
		{
			if(SHOULD_REMOVE_UNITYPROJ) FixSolution(filePath);
		}

		foreach(var filePath in csprojFiles)
		{
			FixProject(filePath);
		}
	}
	
	static bool FixProject(string filePath)
	{
		string content = File.ReadAllText(filePath);

		string searchString = "<TargetFrameworkVersion>v3.5</TargetFrameworkVersion>";
		string replaceString = "<TargetFrameworkVersion>v4.0</TargetFrameworkVersion>";

		if(content.IndexOf(searchString) != -1)
		{
			content = Regex.Replace(content,searchString,replaceString);
			File.WriteAllText(filePath,content);
			return true;
		}
		else 
		{
			return false;
		}
	}

	//we will remove all unityproj references
	static bool FixSolution(string filePath)
	{
		string content = File.ReadAllText(filePath);

		bool didChange = false;

		int nextIndex = 0;

		while(true)
		{
			int projStartIndex = content.IndexOf("Project(",nextIndex);
			if(projStartIndex == -1) break; //no more projects

			int projEndIndex = content.IndexOf("EndProject",projStartIndex);
			if(projEndIndex == -1) break; //unmatched opening project

			string projectString = content.Substring(projStartIndex,projEndIndex-projStartIndex);

			if(projectString.IndexOf(".unityproj") > -1) //is unityproj
			{
				content = content.Remove(projStartIndex,(projEndIndex-projStartIndex)+10);//remove the whole project tag if unityproj is found
				didChange = true; 
			}
			else 
			{
				nextIndex = projEndIndex;
			}
		}

		if(didChange)
		{
			File.WriteAllText(filePath,content);
			return true;
		}
		else 
		{
			return false;
		}
	}
}