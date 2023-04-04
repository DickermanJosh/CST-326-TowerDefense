using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI roundsText;
    public SceneFader sceneFader;
    public string menuSceneName = "MainMenu";
    public void OnEnable()
    {
        roundsText.text = PlayerStats.Rounds.ToString();
    }

    public void Retry()
    {
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        sceneFader.FadeTo(menuSceneName);
    }
}
