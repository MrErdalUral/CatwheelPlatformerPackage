﻿using UnityEngine;

namespace Assets.Input_Handlers
{
    public class AxisRawHandler : MonoBehaviour
    {
        public FloatUnityEvent FloatAction;
        public string AxisName;


        void Update()
        {
            FloatAction.Invoke(Input.GetAxisRaw(AxisName));
        }
    }
}