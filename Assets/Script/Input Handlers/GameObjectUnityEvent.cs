using System;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Input_Handlers
{
    [Serializable]
    public class GameObjectUnityEvent : UnityEvent<GameObject>
    {
    }
}