using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonStats : MonoBehaviour
{
    [SerializeField]
    private string questName; 

    public void AddStats()
    {
        GameManager.AddQuestStats(questName);
    }
}
