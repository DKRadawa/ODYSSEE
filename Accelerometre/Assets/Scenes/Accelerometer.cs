using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class Accelerometer : MonoBehaviour
{
    [SerializeField] public List<float> dataAccX = new List<float>();
    [SerializeField] public List<float> dataAccY = new List<float>();
    [SerializeField] public List<float> dataAccZ = new List<float>();

    [SerializeField] public bool clickStart;
    [SerializeField] public string NameX;
    [SerializeField] public string NameY;
    [SerializeField] public string NameZ;

    public GameObject inputFieldX;
    public GameObject inputFieldY;
    public GameObject inputFieldZ;
    
    private string separator = ";";

     public void StartAcc()
    {
        clickStart = true;
    }

    public void StopAcc()
    {
        clickStart = false;
        NameX = inputFieldX.GetComponent<Text>().text;
        NameY = inputFieldY.GetComponent<Text>().text;
        NameZ = inputFieldZ.GetComponent<Text>().text;

        string saveStringX = string.Join(separator, dataAccX);
        File.WriteAllText(Application.dataPath + "/" + NameX, saveStringX);
        Debug.Log("Sauvegarde de X");

        string saveStringY = string.Join(separator, dataAccY);
        File.WriteAllText(Application.dataPath + "/" + NameY, saveStringY);
        Debug.Log("Sauvegarde de Y");

        string saveStringZ = string.Join(separator, dataAccZ);
        File.WriteAllText(Application.dataPath + "/" + NameZ, saveStringZ);
        Debug.Log("Sauvegarde de Z");

        dataAccX.Clear();
        dataAccY.Clear();
        dataAccZ.Clear();
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
            dataAccX.Add(Input.acceleration.x);
            dataAccY.Add(Input.acceleration.y);
            dataAccZ.Add(Input.acceleration.z);
        }
        
    }
}
