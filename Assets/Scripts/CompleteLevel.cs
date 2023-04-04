using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteLevel : MonoBehaviour
{
    public string nextLevel = "Level02";
    public int nextLevelIndex = 2;
    public SceneFader fader;
    public string menuSceneName = "MainMenu";

    public void Continue()
    {
        Debug.Log("LEVEL CLEARED");
        PlayerPrefs.SetInt("levelReached", nextLevelIndex);
        fader.FadeTo(nextLevel);
    }
    
    public void Menu()
    {
        fader.FadeTo(menuSceneName);
    }
}
