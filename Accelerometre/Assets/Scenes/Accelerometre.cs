using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using UnityEngine.InputSystem;
using TMPro;


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

    public void StartAcc()
    {
        clickStart = true;
        timeStart = Time.time;

        //Name Date management
        fileDateName = DateTime.Now.ToShortTimeString();
        Debug.Log(fileDateName);
    }



    public void StopAcc()
    {
        clickStart = false;

        //Name Management
        fileCustomName = inputFieldName.text;
        if (fileCustomName == "​")
        {
            fileCustomName = "Test";
        }


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


        System.IO.File.WriteAllLines(Application.persistentDataPath + "/" + fileCustomName + "_" + fileDateName+  ".csv", csvData);
        resultMessageText.text = "File save at " + Application.persistentDataPath + "/" + fileCustomName + "_" + fileDateName + ".csv";
        Debug.Log("File save at" + Application.persistentDataPath + "/" + fileCustomName + "_" + fileDateName + ".csv");
        listDataAcc.Clear();
    }


    private void Start()
    {
        clickStart = false;
        listDataAcc = new();
        fileCustomName = "";
        InputSystem.EnableDevice(Accelerometer.current);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (clickStart==true)
        {
            Vector3 valueAcc= Accelerometer.current.acceleration.ReadValue();

            listDataAcc.Add(new Mesure
            {
                Time = (Time.time - timeStart) + Time.deltaTime,
                data = valueAcc,
                module = Vector3.Magnitude(valueAcc)
            });

        }
        
    }
}
