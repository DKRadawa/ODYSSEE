using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public GameObject player;


    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position = player.transform.position + 10*Vector3.back;
    }
}
