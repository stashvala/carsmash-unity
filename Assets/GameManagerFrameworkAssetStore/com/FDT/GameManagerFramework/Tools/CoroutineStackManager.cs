using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

namespace com.FDT.GameManagerFramework
{
	/*
	 * 		A stack of coroutines to execute one after another, to use with CoroutineStackManager
	 * 
	 */

	public delegate IEnumerator IEnumeratorCoroutineDelegate ();
	public delegate IEnumerator IEnumeratorCoroutineDelegateParam<T> (T param);
	public delegate E ParameterReturner<E>();

	public class CoroutineStack:MonoBehaviour
	{
		internal CoroutineStackManager mgr;
		Dictionary<Delegate, int> orderByDelegate = new Dictionary<Delegate, int>();
		Dictionary<Delegate, Delegate> callerByDelegate = new Dictionary<Delegate, Delegate>();
		List<Delegate> items = new List<Delegate>();
		
		protected bool _running = false;
		public bool running { get { return _running;  } }

		public void Add<T>( IEnumeratorCoroutineDelegateParam<T> func, ParameterReturner<T> caller)
		{
			Add<T>(func, caller, 0);
		}
		public void AddLast<T>( IEnumeratorCoroutineDelegateParam<T> func, ParameterReturner<T> caller)
		{
			int currentLast = getLastOrder();
			Add<T>(func, caller, currentLast+1);
		}
		public void AddFirst<T>( IEnumeratorCoroutineDelegateParam<T> func, ParameterReturner<T> caller)
		{
			int currentFirst = getFirstOrder();
			Add<T>(func, caller, currentFirst-1);
		}
		public void Add<T>( IEnumeratorCoroutineDelegateParam<T> func, ParameterReturner<T> caller, int order)
		{
			items.Add(func);
			orderByDelegate[func] = order;
			callerByDelegate[func] = caller;
			Sort();
		}
		public void Add( IEnumeratorCoroutineDelegate func)
		{
			Add(func, 0);
		}
		public void AddLast( IEnumeratorCoroutineDelegate func)
		{
			int currentLast = getLastOrder();
			Add(func, currentLast+1);
		}
		public void AddFirst( IEnumeratorCoroutineDelegate func)
		{
			int currentFirst = getFirstOrder();
			Add(func, currentFirst-1);
		}
		public void Add(IEnumeratorCoroutineDelegate func, int order)
		{
			items.Add(func);
			orderByDelegate[func] = order;
			Sort();
		}
		public void RemoveFunc(IEnumeratorCoroutineDelegate func)
		{
			items.Remove(func);
			orderByDelegate.Remove(func);
			callerByDelegate.Remove(func);
		}
		public void RemoveFunc<T>(IEnumeratorCoroutineDelegateParam<T> func)
		{
			items.Remove(func);
			orderByDelegate.Remove(func);
			callerByDelegate.Remove(func);
		}
		public void Run()
		{
			if (items.Count>0 && !running)
				this.StartCoroutine("sequenceRun");
		}
		protected IEnumerator sequenceRun()
		{
			_running = true;
			List<Delegate> runningItems = new List<Delegate>(items);
			foreach (System.Delegate o in runningItems)
			{
				if (o is IEnumeratorCoroutineDelegate)
				{
					yield return (o.Target as MonoBehaviour).StartCoroutine(o.Method.Name);
				}
				else 
				{
					object param = callerByDelegate[o].DynamicInvoke();
					yield return (o.Target as MonoBehaviour).StartCoroutine(o.Method.Name, param);
				}
			}
			_running = false;
		}
		protected int getLastOrder()
		{
			int corder = int.MinValue;
			foreach (Delegate d in items)
			{
				if (orderByDelegate[d] > corder)
					corder = orderByDelegate[d];
			}
			return corder;
		}
		protected int getFirstOrder()
		{
			int corder = int.MaxValue;
			foreach (Delegate d in items)
			{
				if (orderByDelegate[d] < corder)
					corder = orderByDelegate[d];
			}
			return corder;
		}
		protected void Sort()
		{
			var sortedList = items.OrderBy(i => {
				return orderByDelegate[i];
			});
			items = sortedList.ToList();
		}
		public void Clear()
		{
			items.Clear ();
			orderByDelegate.Clear();
			callerByDelegate.Clear();
		}
	}
	/*
	 * 		Coroutine stack manager is used to create and manage CoroutineStacks.
	 * 
	 * 		Instantiated after the first use.
	 * 
	 */
	public class CoroutineStackManager:SingletonGameObject<CoroutineStackManager>
	{
		List<CoroutineStack> items = new List<CoroutineStack>();
		
		public override void Initialize ()
		{
			
		}
		public CoroutineStack Create()
		{
			CoroutineStack i = gameObject.AddComponent<CoroutineStack>();
			i.mgr = this;
			items.Add(i);
			return i;
		}
	}
}
