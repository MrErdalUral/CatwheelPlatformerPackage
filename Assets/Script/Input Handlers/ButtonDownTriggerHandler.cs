using UnityEngine;
using UnityEngine.Events;

namespace Assets.Input_Handlers
{
    public class ButtonDownTriggerHandler : MonoBehaviour
    {

        public UnityEvent ButtonAction;
        public string InputName;

        // Update is called once per frame
        void Update()
        {

            if (Input.GetButtonDown(InputName))
                ButtonAction.Invoke();
        }
    }
}