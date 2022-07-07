using System;
using UnityEngine;
using TMPro;

public class UploadResult : MonoBehaviour
{
    [SerializeField]
    private string[] listStats;

    [SerializeField]
    private TextMeshProUGUI[] displayStats;

    private void OnEnable()
    {
        this.UpdateStats();
    }


    public void UpdateStats()
    {
        for (int i = 0; i < listStats.Length; i++)
        {
            if (GameManager.questStats.ContainsKey(listStats[i]))
            {
                displayStats[i].text = GameManager.questStats[listStats[i]].ToString();
            }
            else
            {
                displayStats[i].text = "0";
            }
        }
    }

}
