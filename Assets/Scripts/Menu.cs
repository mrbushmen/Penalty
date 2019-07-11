using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public void ResetLevel()
    {
        Application.LoadLevel(0);
    }
    public void ResetTotalScore()
    {
        PlayerPrefs.SetInt("totalGoals", 0);
        PlayerPrefs.SetInt("totalFails", 0);
    }
}
