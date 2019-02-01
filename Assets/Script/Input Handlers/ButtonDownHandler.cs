﻿using UnityEngine;

namespace Assets.Input_Handlers
{
    public class ButtonDownHandler : MonoBehaviour
    {

        public BoolUnityEvent ButtonAction;
        public string InputName;

        // Update is called once per frame
        void Update()
        {
            ButtonAction.Invoke(Input.GetButtonDown(InputName));
        }
    }
}