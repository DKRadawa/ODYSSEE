using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Quest : MonoBehaviour 
{
    [SerializeField]
    private string questName;
    [SerializeField]
    private bool questStart = false;
    [SerializeField]
    private bool questFinished = false;
    [SerializeField]
    private int questTime=5;
    [SerializeField]
    private int questProgress = 0;
    [SerializeField]
    private int questPercent = 0;
    [SerializeField]
    private bool questTimeInitialized = false;




    public void progressQuest()
    {
        if (!questStart)
        {
            questStart = true;
        }
        questProgress++;
        if (MobileMovement.hasMoved)
        {
            questProgress = 0;
        }
        questPercent = (int)(questProgress * 100) / questTime;
        if (questProgress == questTime)
        {
            GameManager.AddQuestStats(questName);
            questFinished = true;
        }
    }

    public bool getQuestFinished()
    {
        return questFinished;
    }

    

    public int getQuestPercent()
    {
        return questPercent;
    }


    public string getQuestName()
    {
        return questName;
    }

    public void setQuestName(string _questName)
    {
        questName = _questName;
    }

    public void resetQuest()
    {
        questTimeInitialized = false;
        questProgress = 0;
        questPercent = 0;
        questStart = false;
        questFinished = false;
    }

    public void initQuestTime()
    {
        questTimeInitialized = true;
    }

    public void initQuestTime(string timeName)
    {
        questTime = GameManager.GetQuestTime(timeName);
        questTimeInitialized = true;
    }

    public void initQuestTime(string minTimeName,string maxTimeName)
    {
        int minTimeValue = GameManager.GetQuestTime(minTimeName);
        int maxTimeValue = GameManager.GetQuestTime(maxTimeName);

        questTime = Random.Range(minTimeValue,maxTimeValue);
        questTimeInitialized = true;
    }

    public bool getInitState()
    {
        return questTimeInitialized;
    }

}
