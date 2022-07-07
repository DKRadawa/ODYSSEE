using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerController : MonoBehaviour
{
    private Timer timer;

    [SerializeField]
    private TextMeshProUGUI valueTimer;

    [SerializeField]
    private TextMeshProUGUI statusTimer;

    [SerializeField]
    private TextMeshProUGUI initValueWritten;


    // Start is called before the first frame update
    void Start()
    {
        timer = GetComponent<Timer>();
        updateInitValue();
    }

    public void updateInitValue()
    {
        timer.setInitTimerValueInSeconds(Utils.TextToInt(initValueWritten));
        int _value = (int)timer.GetCurrentTime();
        valueTimer.SetText(_value.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        if (timer.IsTimerRunning())
        {
            statusTimer.SetText("Started");

            int _value = (int)timer.GetCurrentTime();
            valueTimer.SetText(_value.ToString());
        }
        else
        {
            statusTimer.SetText("Stopped");
        }
        
    }
}
