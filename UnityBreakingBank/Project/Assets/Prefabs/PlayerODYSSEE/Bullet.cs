using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //public GameObject hitEffect;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //GameObject effect = Instantiate(hit)
        //Destroy effect
        Destroy(gameObject);
    }
}
