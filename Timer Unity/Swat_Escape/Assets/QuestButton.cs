using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class QuestButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Quest quest;

    [SerializeField]
    private string textAtStart;

    [SerializeField]
    private string textAtEnd = "Quete terminé!!!";

    [SerializeField]
    private bool transitionAtEnd;
    private const int TRANSITION_TIME = 3;

    [SerializeField]
    private GameObject gameObjectAtEnd;

    [SerializeField]
    private bool buttonPressed;
    private Coroutine coroutineButtonPressed;

    [SerializeField]
    private string questTimeMinValue;

    [SerializeField]
    private string questTimeMaxValue;



    public void OnPointerDown(PointerEventData eventData)
    {
        buttonPressed = true;
        string questName = this.gameObject.name;

        if (!GameManager.questList.ContainsKey(questName))
        {
            initQuest();
        }

        if(!GameManager.GetQuest(this.gameObject.name).getInitState())
        {
            GameManager.GetQuest(this.gameObject.name).initQuestTime(questTimeMinValue, questTimeMaxValue);
        }

        coroutineButtonPressed = StartCoroutine(OnButtonPressed(questName));

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        buttonPressed = false;
        StopCoroutine(coroutineButtonPressed);
    }

    public void initQuest()
    {
        Quest quest = gameObject.AddComponent<Quest>();
        quest.setQuestName(this.gameObject.name);
        GameManager.CreateNewQuest(quest.getQuestName(), quest);
        GetComponentInChildren<TextMeshProUGUI>().text = textAtStart;
    }

    public void resetQuest()
    {
        GameManager.EndQuest(this.gameObject.name);
        Destroy(GetComponent<Quest>());
        GetComponentInChildren<TextMeshProUGUI>().text = textAtStart;
    }



    private IEnumerator OnButtonPressed(string questName)
    {
        Quest quest = GameManager.GetQuest(questName);
        if (!quest.getQuestFinished())
        {
            GetComponentInChildren<TextMeshProUGUI>().text = textAtStart + "\n" +  quest.getQuestPercent().ToString() + "%";
        }
        yield return new WaitForSeconds(1);

        if(buttonPressed)
        {
            GameManager.GetQuest(questName).progressQuest();
            GetComponentInChildren<TextMeshProUGUI>().text = textAtStart + "\n" + quest.getQuestPercent().ToString() + "%";

            if (GameManager.GetQuest(questName).getQuestFinished())
            {
                StartCoroutine(TransitionEndQuest());
                yield break;
            }

            StartCoroutine(OnButtonPressed(questName)); 
        }

            
    }

    private IEnumerator TransitionEndQuest()
    {
        GetComponentInChildren<TextMeshProUGUI>().text = textAtEnd + "\n" + "Switch in 3";
        yield return new WaitForSeconds(1);
        GetComponentInChildren<TextMeshProUGUI>().text = textAtEnd + "\n" + "Switch in 2";
        yield return new WaitForSeconds(1);
        GetComponentInChildren<TextMeshProUGUI>().text = textAtEnd + "\n" + "Switch in 1";
        yield return new WaitForSeconds(1);
        if (transitionAtEnd)
        {
            gameObjectAtEnd.SetActive(true);
            gameObject.transform.parent.gameObject.SetActive(false);
        }
        resetQuest();
        StopAllCoroutines();

    }

}