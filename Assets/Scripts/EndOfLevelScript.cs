using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndOfLevelScript : MonoBehaviour
{
    
    [SerializeField] PlayerController Player;
    [SerializeField] int ScoreToNextLevel;
    [SerializeField] EndUiScript EndUI;
    [SerializeField] UIScoreScript ScoreUI;
    
    
    private void Start()
    {
        GlobalEventScript.RestartGame.AddListener(Restart);
        GlobalEventScript.OnPlayerLand.AddListener(StartEndLevel);
    }
  
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartLevel();
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            NextLevel();
        }
    }

    public void StartEndLevel(float Bonus)
    {
        
        StartCoroutine(EndGame(0.3f,Bonus));
    }
    IEnumerator EndGame(float Time,float Bonus)
    { 
        yield return new WaitForSeconds(Time);
        int EndScore = ScoreUI.EndGameScore(Bonus);
        EndUI.ShowEndCanvas(EndScore, ScoreToNextLevel);
    }

    public void RestartLevel()
    {
        GlobalEventScript.SendRestartGame();
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ExitGame()
    { 
        Application.Quit();
    }
    private void Restart()
    {
        TargetScript.Toutch = false;
    }
}
