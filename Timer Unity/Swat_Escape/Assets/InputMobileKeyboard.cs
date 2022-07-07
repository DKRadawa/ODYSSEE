using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputMobileKeyboard : MonoBehaviour
{
    private TouchScreenKeyboard keyboard;
    private TextMeshProUGUI textValue;
    public TouchScreenKeyboard.Status status = TouchScreenKeyboard.Status.LostFocus;

    // Start is called before the first frame update
    void Start()
    {
        keyboard = new TouchScreenKeyboard("",TouchScreenKeyboardType.Default,false,false,false,false,"",3);
        textValue = GetComponentInParent<TextMeshProUGUI>();
    }

    public void OpenKeyboard()
    {
        keyboard = TouchScreenKeyboard.Open("9",TouchScreenKeyboardType.ASCIICapable);
    }


    public void CloseKeyboard()
    {
        if(keyboard != null)
        {
            if (keyboard.status == TouchScreenKeyboard.Status.Done)
            {
                keyboard = null;
            }
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        if (keyboard != null)
        {
            status = keyboard.status;
        }
        
    }
}
