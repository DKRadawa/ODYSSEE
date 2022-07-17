using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Accelerometer : MonoBehaviour
{
    [SerializeField] public List<float> dataAccX = new List<float>();
    [SerializeField] public List<float> dataAccY = new List<float>();
    [SerializeField] public List<float> dataAccZ = new List<float>();

    [SerializeField] public bool clickStart;

    private string separator = ";";

     public void StartAcc()
    {
        clickStart = true;
    }

    public void StopAcc()
    {
        clickStart = false;

        string saveStringX = string.Join(separator, dataAccX);
        File.WriteAllText(Application.dataPath + "/dataX.csv", saveStringX);
        Debug.Log("Sauvegarde de X");

        string saveStringY = string.Join(separator, dataAccY);
        File.WriteAllText(Application.dataPath + "/dataY.csv", saveStringY);
        Debug.Log("Sauvegarde de Y");

        string saveStringZ = string.Join(separator, dataAccZ);
        File.WriteAllText(Application.dataPath + "/dataZ.csv", saveStringZ);
        Debug.Log("Sauvegarde de Z");

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
            dataAccX.Add(Input.acceleration.x);
            dataAccY.Add(Input.acceleration.y);
            dataAccZ.Add(Input.acceleration.z);
        }
        
    }
}
