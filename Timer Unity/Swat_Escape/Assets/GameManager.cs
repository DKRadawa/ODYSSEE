using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GameManager : MonoBehaviour
{
    public static Dictionary<string, Quest> questList = new Dictionary<string, Quest>();

    public static Dictionary<string, int> questStats = new Dictionary<string, int>();

    private static Dictionary<string, int> questTime = new Dictionary<string, int>();

    #region Civil Value
    //Phase 1
    [SerializeField]
    private TextMeshProUGUI minText;
    [SerializeField]
    private TextMeshProUGUI maxText;
    [SerializeField]
    private TextMeshProUGUI friendDoorText;

    //Phase 2
    [SerializeField]
    private TextMeshProUGUI bankOpenText;


    #endregion

    #region Robber Value
    //Phase 1
    [SerializeField]
    private TextMeshProUGUI slowText;
    [SerializeField]
    private TextMeshProUGUI normalText;
    [SerializeField]
    private TextMeshProUGUI fastText;

    //Phase 2
    [SerializeField]
    private TextMeshProUGUI gatherText;
    [SerializeField]
    private TextMeshProUGUI gatherPenalityText;
    [SerializeField]
    private TextMeshProUGUI depositText;
    [SerializeField]
    private TextMeshProUGUI runawayText;


    #endregion

    public void initValueCivilOption()
    {
        questTime.Clear();
        questList.Clear();
        questStats.Clear();
        questTime.Add("Open Cell Room Quest Min Value", Utils.TextToInt(minText));
        questTime.Add("Open Cell Room Quest Max Value", Utils.TextToInt(maxText));
        questTime.Add("Open Friend Room Quest Value", Utils.TextToInt(friendDoorText));

        questTime.Add("Open Escape Bank Room Quest Value", Utils.TextToInt(bankOpenText));
        
    }

    public void initValueRobberOption()
    {
        questTime.Clear();
        questList.Clear();
        questStats.Clear();
        questTime.Add("Open Stronghold Quest Slow Value", Utils.TextToInt(slowText));
        questTime.Add("Open Stronghold Quest Medium Value", Utils.TextToInt(normalText));
        questTime.Add("Open Stronghold Quest Fast Value", Utils.TextToInt(fastText));

        questTime.Add("Gather Quest Value", Utils.TextToInt(gatherText));
        questTime.Add("Gather Penality Quest Value", Utils.TextToInt(gatherPenalityText));
        questTime.Add("Deposit Quest Value", Utils.TextToInt(depositText));
        questTime.Add("Runaway Quest Value", Utils.TextToInt(runawayText));
    }


    public static Quest GetQuest(string _questName)
    {
        return questList[_questName];
    }

    public static void EndQuest(string _questName)
    {
        GameManager.questList.Remove(_questName);
    }

    public static int GetQuestTime(string _questTimeName)
    {
        return questTime[_questTimeName];
    }



    public static void CreateNewQuest(string questName, Quest quest)
    {
        if (!questList.ContainsKey(questName))
        {
            questList.Add(questName, quest);
        }
        else
        {
            Debug.LogError("This quest already exist!");
        }
    }

    public static void AddQuestStats(string questName)
    {
        if (questStats.ContainsKey(questName))
        {
            questStats[questName] += 1;
        }
        else
        {
            questStats.Add(questName,1);
        }
        
    }


}
