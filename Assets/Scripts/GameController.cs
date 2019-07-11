using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public delegate void FiveReached();
    public static event FiveReached OnFiveReached;

    [Range(1f,3f)]
    public float restartDelay = 1f; //Пауза при сбросе позиции мяча

    private int goals = 0;
    public int Goals
    {
        get
        {
            return goals;
        }

        private set
        {
            goals = value;
            if (value >= 5)
            {
                ShowResults();
            }
        }
    }

    private int fails = 0;
    public int Fails
    {
        get
        {
            return fails;
        }

        private set
        {
            fails = value;
            if (value >= 5)
            {
                ShowResults();
            }
        }
    }


    private int totalGoals = 0;
    private int totalFails = 0;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        totalGoals = PlayerPrefs.GetInt("totalGoals");
        totalFails = PlayerPrefs.GetInt("totalFails");
    }
    private void OnEnable()
    {
        Goal.OnGoal += AddGoal;
        Goalkeeper.OnCatch += AddFail;
        Out.OnOut += AddFail;
    }
    private void OnDisable()
    {
        Goal.OnGoal -= AddGoal;
        Goalkeeper.OnCatch -= AddFail;
        Out.OnOut -= AddFail;
    }
       
    public void AddGoal()
    {
        Goals += 1;
    }

    public void AddFail()
    {
        Fails += 1;
    }

    public int GetTotalGoals()
    {
        return totalGoals;
    }
    public int GetTotalFails()
    {
        return totalFails;
    }

    public void ShowResults()
    {
        totalGoals += Goals;
        totalFails += Fails;
        PlayerPrefs.SetInt("totalGoals", totalGoals);
        PlayerPrefs.SetInt("totalFails", totalFails);
        StartCoroutine(Wait());        
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(restartDelay);
        OnFiveReached?.Invoke();
    }
}
