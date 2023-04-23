using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using UnityEngine.InputSystem;
using System.Collections;
using TMPro;
using System.IO;


public class Accelerometre : MonoBehaviour
{
    public class Mesure
    {
        public float Time { get; set; }
        public Vector3 data { get; set; }
        public double module { get; set; }
    }

    private List<Mesure> listDataAcc;

    private bool clickStart;
    private string fileCustomName;
    private string fileDateName;
    private float timeStart;


    [SerializeField]
    private TextMeshProUGUI inputFieldName;

    [SerializeField]
    private TextMeshProUGUI resultMessageText;

    [SerializeField]
    private Toggle enduranceModeToggle;
    private bool enduranceMode;


    //Stats endurance Mode
    private float maximumTotalValue;
    private float minimumTotalValue;

    [SerializeField]
    private TextMeshProUGUI totalMaximumText;

    [SerializeField]
    private TextMeshProUGUI totalMinimumText;

    private float maximumOneMinuteValue;
    private float minimumOneMinuteValue;

    [SerializeField]
    private TextMeshProUGUI oneMinuteMaximumText;

    [SerializeField]
    private TextMeshProUGUI oneMinuteMinimumText;


    public void StartAcc()
    {
        clickStart = true;
        timeStart = Time.time;

        InputSystem.EnableDevice(Accelerometer.current);
        try
        {
            Accelerometer.current.samplingFrequency = 100;
        }
        catch(Exception e)
        {
            Debug.LogError("Exception = " + e.Message);
        }
        
   
        StartCoroutine(Tempo());

        enduranceMode = enduranceModeToggle.isOn;

        //Name Date management
        fileDateName = DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString();
        Debug.Log(fileDateName);

        if (enduranceMode)
        {
            StartCoroutine(ChronoResetOneMinute());
        }

    }



    public void StopAcc()
    {
        clickStart = false;
        StopAllCoroutines();
        Debug.Log("Sampling Value = "  + Accelerometer.current.samplingFrequency.ToString());
        //Name Management
        fileCustomName = inputFieldName.text;
        if (fileCustomName == "Name​")
        {
            fileCustomName = "Test";
        }

        SaveFileAndClear();
        
    }

    public void Refresh()
    {
        maximumOneMinuteValue = 1f;
        minimumOneMinuteValue = 1f;
    }


    private void Start()
    {
        clickStart = false;
        listDataAcc = new();
        fileCustomName = "Name";

        maximumTotalValue = 1f;
        maximumOneMinuteValue = 1f;

        minimumTotalValue = 1f;
        minimumOneMinuteValue = 1f;
    }

    private void LateUpdate()
    {
        if (clickStart == true)
        {
            totalMaximumText.text = maximumTotalValue.ToString();
            totalMinimumText.text = minimumTotalValue.ToString();
            oneMinuteMaximumText.text = maximumOneMinuteValue.ToString();
            oneMinuteMinimumText.text = minimumOneMinuteValue.ToString();
        }
           
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (clickStart == true)
        {
            Vector3 valueAcc = Accelerometer.current.acceleration.ReadValue();
            float _module = valueAcc.magnitude;


            listDataAcc.Add(new Mesure
            {
                Time = (Time.time - timeStart) + Time.deltaTime,
                data = valueAcc,
                module = _module
            }); 


            //Endurance Mode
            if (_module > maximumTotalValue)
            {
                maximumTotalValue = _module;
            }

            if (_module < minimumTotalValue)
            {
                minimumTotalValue = _module;
            }



            if (_module > maximumOneMinuteValue)
            {
                maximumOneMinuteValue = _module;
            }

            if (_module < minimumOneMinuteValue)
            {
                minimumOneMinuteValue = _module;
            }



        }

    }

    private void SaveFileAndClear()
    {
        //Fill with data
        List<string> csvData = new();
        string headerLine = "Time;Accelerometer Module";
        csvData.Add(headerLine);

        foreach (Mesure item in listDataAcc)
        {
            csvData.Add(string.Join(";", item.Time, item.module));
        }

        /* var dataLines = from mes in listDataAcc
                         let dataLine = string.Join(";", mes.GetType().GetProperties().Select(p => p.GetValue(mes)))
                         select dataLine;*/

        //string pathSave = "/storage/emulated/0/Android/Appli_test"; //instead of Application.dataPath 
        
        string pathSave = Application.persistentDataPath;

        System.IO.File.WriteAllLines(pathSave + "/" + fileCustomName + "_" + fileDateName + ".csv", csvData);
        resultMessageText.text = "File save at " + pathSave + "/" + fileCustomName + "_" + fileDateName + ".csv";
        Debug.Log("File save at " + pathSave + "/" + fileCustomName + "_" + fileDateName + ".csv");


        //Write some text to the test.txt file
        /*StreamWriter writer = new StreamWriter(pathSave + "/" + fileCustomName + "_" + fileDateName + ".csv", true);
        writer.WriteLine(csvData);
        writer.Close();*/
        //resultMessageText.text = "File save at " + pathSave + "/" + fileCustomName + "_" + fileDateName + ".csv";


        listDataAcc.Clear();
    }

    private IEnumerator ChronoResetOneMinute()
    {
        yield return new WaitForSeconds(60);
        minimumOneMinuteValue = 1f;
        maximumOneMinuteValue = 1f;
        StartCoroutine(ChronoResetOneMinute());

        SaveFileAndClear();

    }

    private IEnumerator Tempo()
    {
        yield return new WaitForSeconds(1);
        maximumTotalValue = 1f;
        maximumOneMinuteValue = 1f;

        minimumTotalValue = 1f;
        minimumOneMinuteValue = 1f;

    }



}
