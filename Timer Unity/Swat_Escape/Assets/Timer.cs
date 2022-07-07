using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private float initTimerValueInSeconds;

    [SerializeField]
    private float currentTime;

    [SerializeField]
    private bool isTimerRunning;

    [SerializeField]
    public bool hasTimeout;



    public void StartTimer()
    {
        hasTimeout = false;
        isTimerRunning = true;
    }

    public void StopTimer()
    {
        hasTimeout = false;
        isTimerRunning = false;
        currentTime = initTimerValueInSeconds;
    }

    public void PauseTimer()
    {
        hasTimeout = false;
        isTimerRunning = false;
    }

    public float GetCurrentTime()
    {
        return currentTime;
    }

    public bool IsTimerRunning()
    {
        return isTimerRunning;
    }

    public void setInitTimerValueInSeconds(float value)
    {
        initTimerValueInSeconds = value;
        currentTime = initTimerValueInSeconds;
    }


    // Start is called before the first frame update
    void Start()
    {
        StopTimer();
        currentTime = initTimerValueInSeconds;
    }


    // Update is called once per frame
    private void Update()
    {
        if (isTimerRunning==false)
        {
            return;
        }

        currentTime -= Time.deltaTime;
        if (currentTime < 0)
        {
            currentTime = 0f;
            hasTimeout = true;
            isTimerRunning = false;
        }
    }
}
