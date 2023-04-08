using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NetworkManagerUI : MonoBehaviour
{
    [SerializeField] private Button hostButton;
    [SerializeField] private Button clientButton;
    [SerializeField] private TMP_InputField inputFieldJoinCode;

    [SerializeField] private testRelay testRelay;

    // Start is called before the first frame update
    private void Awake() 
    {
        hostButton.onClick.AddListener(() =>
        {
            testRelay.CreateRelay();
        });
        clientButton.onClick.AddListener(() =>
        {
            testRelay.JoinRelay(inputFieldJoinCode.text);
        });

    }

}
