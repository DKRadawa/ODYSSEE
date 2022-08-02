using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using UnityEngine.InputSystem;


public class Accelerometre : MonoBehaviour
{
    public class Mesure
    {
        public float Time { get; set; }
        public Vector3 data { get; set; }  
        public double module { get; set; }
    }

    [SerializeField] public List<Mesure> dataAcc = new List<Mesure>();

    [SerializeField] public bool clickStart;
    [SerializeField] public string nameFile;

    [SerializeField] public bool timeBegin;
    [SerializeField] public float timeStart;

    public Text realtimeValue;
    public Text frequence;
    public Text frequenceModif;

    public GameObject inputFieldName;

    public void StartAcc()
    {
        clickStart = true;
        timeBegin = true;
        InputSystem.DisableDevice(Accelerometer.current);
        Accelerometer.current.samplingFrequency=10;      
        InputSystem.EnableDevice(Accelerometer.current);
    }



    public void StopAcc()
    {
        clickStart = false;

        nameFile = inputFieldName.GetComponent<Text>().text;
       

        string headerLine = string.Join(";", dataAcc.GetType().GetProperties().Select(p=>p.Name));
        var dataLines = from mes in dataAcc
                        let dataLine = string.Join(";", mes.GetType().GetProperties().Select(p => p.GetValue(mes)))
                        select dataLine;
        var csvData = new List<string>();
        csvData.Add(headerLine);
        csvData.AddRange(dataLines);

        System.IO.File.WriteAllLines(Application.persistentDataPath + "/" + nameFile + ".csv", csvData);


        dataAcc.Clear();
 
        Debug.Log("Reset des tableaux");
    }


    private void Start()
    {
        clickStart = false;
 
        InputSystem.EnableDevice(Accelerometer.current);
        float freq= Accelerometer.current.samplingFrequency;
        frequence.text = freq.ToString();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (clickStart==true)
        {
            if (timeBegin==true)
            {
                timeStart = Time.time;
                timeBegin = false;
            }

            Vector3 valueAcc= Accelerometer.current.acceleration.ReadValue();

            dataAcc.Add(new Mesure
            {
                Time = (Time.time - timeStart) + Time.deltaTime,
                data = valueAcc,
                module = Vector3.Magnitude(valueAcc)
            });

            realtimeValue.text = valueAcc.ToString();
            float freqModif = Accelerometer.current.samplingFrequency;
            frequenceModif.text = freqModif.ToString();
        }
        
    }
}
