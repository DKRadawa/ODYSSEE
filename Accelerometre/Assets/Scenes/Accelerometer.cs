using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;


public class Accelerometer : MonoBehaviour
{
    public class Mesure
    {
        public float Time { get; set; }
        public double dataX { get; set; }
        public double dataY { get; set; }
        public double dataZ { get; set; }
        public double module { get; set; }
    }

    [SerializeField] public List<Mesure> dataAcc = new List<Mesure>();

    [SerializeField] public bool clickStart;
    [SerializeField] public string nameFile;
    [SerializeField] public string Date;

    [SerializeField] public bool timeBegin;
    [SerializeField] public float timeStart;

    public GameObject inputFieldName;
    public GameObject inputFieldDate;

    public void StartAcc()
    {
        clickStart = true;
        timeBegin = true;
    }



    public void StopAcc()
    {
        clickStart = false;

        nameFile = inputFieldName.GetComponent<Text>().text;
        Date = inputFieldDate.GetComponent<Text>().text;

        string headerLine = string.Join(";", dataAcc.GetType().GetProperties().Select(p=>p.Name));
        var dataLines = from mes in dataAcc
                        let dataLine = string.Join(";", mes.GetType().GetProperties().Select(p => p.GetValue(mes)))
                        select dataLine;
        var csvData = new List<string>();
        csvData.Add(headerLine);
        csvData.AddRange(dataLines);

        System.IO.File.WriteAllLines(Application.dataPath + "/" + nameFile + Date + ".csv", csvData);


        dataAcc.Clear();
 
        Debug.Log("Reset des tableaux");
    }


    private void Start()
    {
        clickStart = false;
    }

    // Update is called once per frame
     void Update()
    {
        if (clickStart==true)
        {
            if (timeBegin==true)
            {
                timeStart = Time.time;
                timeBegin = false;
            }
            double x = Input.acceleration.x;
            double y = Input.acceleration.y;
            double z = Input.acceleration.z;
            
            dataAcc.Add(new Mesure
            {
                Time = (Time.time-timeStart)+Time.deltaTime,
                dataX= x,
                dataY= y,
                dataZ= z,
                module= Math.Sqrt(x*x+y*y+z*z)
            }) ;
        }
        
    }
}
