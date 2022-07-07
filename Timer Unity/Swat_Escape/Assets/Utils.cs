using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Utils : MonoBehaviour
{
    public static int TextToInt(TextMeshProUGUI text)
    {
        int length = text.text.Length;
        string s = text.text.Remove(length - 1);
        return int.Parse(s);
    }
}
