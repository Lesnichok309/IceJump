using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndUiScript : MonoBehaviour
{
    [SerializeField] GameObject EndLevelCanvas;
    [SerializeField] TextMeshProUGUI PlayerScoreText;
    [SerializeField] TextMeshProUGUI NeedScoreText;
    [SerializeField] TextMeshProUGUI WinText;
    [SerializeField] Button NextLevelButton;

    private void Awake()
    {
        GlobalEventScript.RestartGame.AddListener(Restart);
    }
    public void ShowEndCanvas(int PlayerScore, int ScoreToNextLevel)
    {
        EndLevelCanvas.SetActive(true);

        PlayerScoreText.text = PlayerScore.ToString();
        NeedScoreText.text = ScoreToNextLevel.ToString();

        if (PlayerScore >= ScoreToNextLevel)
        {
            print(PlayerScore + " Win " + ScoreToNextLevel);
            WinText.text = "Победа";
            
            NextLevelButton.interactable = true;
        }
        else
        {
            print(PlayerScore + " Loose " + ScoreToNextLevel);
            WinText.text = "Поражение";

            NextLevelButton.interactable = false;
        }
    }
    private void HideEndCanvas()
    {
        EndLevelCanvas.SetActive(false);
    }

    private void Restart()
    {
        HideEndCanvas();   
    }
}
