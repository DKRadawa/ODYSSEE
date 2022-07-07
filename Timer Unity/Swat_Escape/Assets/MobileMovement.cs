using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileMovement : MonoBehaviour
{
    [SerializeField]
    private Vector3 movement;
    [SerializeField]
    private float moduleMovement;
    [SerializeField]
    private float MaxModulus=0;

    [SerializeField]
    public static bool hasMoved = false;

    private const float moveCooldownTime = 3f;
    private const float moveThresholdMagnitude = 1.01f;

    private Coroutine movementCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rawTilt = Input.acceleration;
        movement = rawTilt;
        moduleMovement = rawTilt.magnitude;

        if (moduleMovement > MaxModulus)
        {
            MaxModulus = moduleMovement;
        }

        if (moduleMovement> moveThresholdMagnitude)
        {
            if (hasMoved)
            {
                StopCoroutine(movementCoroutine);
                movementCoroutine = StartCoroutine(HasMoved());
            }
            else
            {
                movementCoroutine = StartCoroutine(HasMoved());
            }
            
        }

    }

    public bool getHasMoved()
    {
        return hasMoved;
    }

    private IEnumerator HasMoved()
    {
        hasMoved = true;
        yield return new WaitForSeconds(moveCooldownTime);
        hasMoved = false;
        MaxModulus = moduleMovement;
    }
}
