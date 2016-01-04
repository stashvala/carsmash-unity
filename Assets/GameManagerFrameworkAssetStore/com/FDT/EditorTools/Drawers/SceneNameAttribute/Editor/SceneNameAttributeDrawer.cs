using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections.Generic;
#region Header
/**
 *
 * original version available in https://github.com/anchan828/property-drawer-collection
 * 
**/
#endregion 
namespace com.FDT.EditorTools
{
	[CustomPropertyDrawer(typeof(SceneNameAttribute))]
	public class SceneNameAttributeDrawer : PropertyDrawer
	{
	    private SceneNameAttribute sceneNameAttribute
	    {
	        get
	        {
	            return (SceneNameAttribute)attribute;
	        }
	    }


	    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	    {
	        string[] sceneNames = GetEnabledSceneNames();

	        if (sceneNames.Length == 0)
	        {
	            EditorGUI.LabelField(position, ObjectNames.NicifyVariableName(property.name), "Scene is Empty");
	            return;
	        }

	        int[] sceneNumbers = new int[sceneNames.Length];

	        SetSceneNambers(sceneNumbers, sceneNames);

	        if (!string.IsNullOrEmpty(property.stringValue))
	            sceneNameAttribute.selectedValue = GetIndex(sceneNames, property.stringValue);

			string l = string.IsNullOrEmpty (sceneNameAttribute.label) ? label.text : sceneNameAttribute.label;
			sceneNameAttribute.selectedValue = EditorGUI.IntPopup(position, l, sceneNameAttribute.selectedValue, sceneNames, sceneNumbers);

	        property.stringValue = sceneNames[sceneNameAttribute.selectedValue];
	    }

	    string[] GetEnabledSceneNames()
	    {
	        List<EditorBuildSettingsScene> scenes = (sceneNameAttribute.enableOnly ? EditorBuildSettings.scenes.Where(scene => scene.enabled) : EditorBuildSettings.scenes).ToList();
	        HashSet<string> sceneNames = new HashSet<string>();
	        scenes.ForEach(scene =>
	        {
	            sceneNames.Add(scene.path.Substring(scene.path.LastIndexOf("/") + 1).Replace(".unity", string.Empty));
	        });
	        return sceneNames.ToArray();
	    }

	    void SetSceneNambers(int[] sceneNumbers, string[] sceneNames)
	    {
	        for (int i = 0; i < sceneNames.Length; i++)
	        {
	            sceneNumbers[i] = i;
	        }
	    }

	    int GetIndex(string[] sceneNames, string sceneName)
	    {
	        int result = 0;
	        for (int i = 0; i < sceneNames.Length; i++)
	        {
	            if (sceneName == sceneNames[i])
	            {
	                result = i;
	                break;
	            }
	        }
	        return result;
	    }
	}
}