using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using Unity.Collections;

public class PlayerMovement : NetworkBehaviour
{
    public float moveSpeed = 7f;

    [SerializeField] 
    private  Rigidbody2D rigidBody;

    private Camera playerCamera;
    private GameObject playerFieldOfView;


    //Network Variable to test
    private NetworkVariable<float> randomNumber = new NetworkVariable<float>(1f, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public override void OnNetworkSpawn()
    {
        randomNumber.OnValueChanged += (float previousValue, float newValue) => { };
    }

    public struct MyCustomData:INetworkSerializable
    {
        public int _int;
        public bool _bool;
        public string message;
        public FixedString128Bytes message32;


        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref _int);
            serializer.SerializeValue(ref _bool);
        }
    }
        

    Vector2 movement;
    Vector2 mousePosition;

    private void Start()
    {
        playerCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        playerFieldOfView = GameObject.Find("FoV");
        playerFieldOfView.GetComponent<FieldOfView>().enabled = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) return;

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePosition = playerCamera.ScreenToWorldPoint(Input.mousePosition);
        playerCamera.gameObject.transform.position = this.transform.position + 10 * Vector3.back;



    }

    private void FixedUpdate()
    {
        if (!IsOwner) return;
        rigidBody.MovePosition(rigidBody.position + movement * moveSpeed * Time.fixedDeltaTime);

        Vector2 lookDirection = mousePosition - rigidBody.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg ;

        rigidBody.rotation = angle;
        playerFieldOfView.GetComponent<FieldOfView>().SetAimDirection(lookDirection);
        playerFieldOfView.GetComponent<FieldOfView>().SetOrigin(transform.position);
    }


}
