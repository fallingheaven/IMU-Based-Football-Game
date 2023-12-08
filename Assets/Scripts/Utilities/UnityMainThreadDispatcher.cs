using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public static class UnityMainThreadDispatcher
{
    private static readonly object LockObject = new object();
    private static readonly Queue<Action> ActionQueue = new Queue<Action>();
    private static GameObject _dispatcherObject;

    public static void Initialize()
    {
        if (_dispatcherObject != null) return;
        lock (LockObject)
        {
            if (_dispatcherObject != null) return;
            
            _dispatcherObject = new GameObject("UnityMainThreadDispatcher");
            Object.DontDestroyOnLoad(_dispatcherObject);
            _dispatcherObject.AddComponent<DispatcherBehaviour>();
        }
    }

    public static void Enqueue(Action action)
    {
        lock (LockObject)
        {
            ActionQueue.Enqueue(action);
        }
    }

    private class DispatcherBehaviour : MonoBehaviour
    {
        private void Update()
        {
            lock (LockObject)
            {
                while (ActionQueue.Count > 0)
                {
                    var action = ActionQueue.Dequeue();
                    action.Invoke();
                }
            }
        }
    }
}