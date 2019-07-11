using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;


    [SerializeField]
    private GameObject goalMsg;
    [SerializeField]
    private GameObject failMsg;
    [SerializeField]
    private GameObject catchMsg;
    [SerializeField]
    private GameObject inGameUI;

    [SerializeField]
    private GameObject resultScr;

    [SerializeField]
    private TextMeshProUGUI goalsText; //для вывода текущих попаданий
    [SerializeField]
    private TextMeshProUGUI failsText; //для вывода текущих промахов

    [SerializeField]
    private TextMeshProUGUI resultGoals;
    [SerializeField]
    private TextMeshProUGUI resultFails;

    [SerializeField]
    private TextMeshProUGUI totalGoals;
    [SerializeField]
    private TextMeshProUGUI totalFails;


    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    private void OnEnable()
    {
        Goal.OnGoal += ShowGoalMessage;
        Goalkeeper.OnCatch += ShowCatchMessage;
        Out.OnOut += ShowFailMessage;
        GameController.OnFiveReached += ShowResultScreen;
    }
    private void OnDisable()
    {
        Goal.OnGoal -= ShowGoalMessage;
        Goalkeeper.OnCatch -= ShowCatchMessage;
        Out.OnOut -= ShowFailMessage;
        GameController.OnFiveReached -= ShowResultScreen;
    }

    public void UpdateStats()
    {
        goalsText.SetText(GameController.instance.Goals.ToString());
        failsText.SetText(GameController.instance.Fails.ToString());
    }

    public void ShowGoalMessage()
    {
        goalMsg.SetActive(true);        
        StartCoroutine(HideMessages());        
    }

    public void ShowFailMessage()
    {
        failMsg.SetActive(true);        
        StartCoroutine(HideMessages());
    }

    public void ShowCatchMessage()
    {
        catchMsg.SetActive(true);
        StartCoroutine(HideMessages());
    }

    public void ShowResultScreen()
    {
        resultScr.SetActive(true);
        inGameUI.SetActive(false);
        resultGoals.SetText(GameController.instance.Goals.ToString());
        resultFails.SetText(GameController.instance.Fails.ToString());

        totalFails.SetText(GameController.instance.GetTotalFails().ToString());
        totalGoals.SetText(GameController.instance.GetTotalGoals().ToString());
    }

    IEnumerator HideMessages()
    {
        yield return new WaitForSeconds(GameController.instance.restartDelay);
        goalMsg.SetActive(false);
        failMsg.SetActive(false);
        catchMsg.SetActive(false);
        UpdateStats();
    }
}
