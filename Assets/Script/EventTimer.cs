using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Use this script to create timed unity events
/// </summary>
public class EventTimer : MonoBehaviour
{
    public string EventName;

    public UnityEvent OnTimerStart;
    public UnityEvent OnTimerEnd;
    public UnityEvent OnTimer;

    private bool _isTimerOn;

    void Update()
    {
        if(_isTimerOn)
            OnTimer.Invoke();
    }

    public void StartTimer(float time)
    {
        StartCoroutine(Timer(time));
    }

    private IEnumerator Timer(float time)
    {
        OnTimerStart.Invoke();
        _isTimerOn = true;
        yield return new WaitForSeconds(time);
        _isTimerOn = false;
        OnTimerEnd.Invoke();

    }
}
