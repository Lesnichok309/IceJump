using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class UIScoreScript : MonoBehaviour
{
    
    ScoreScript _playerScore = new ScoreScript();
    [SerializeField] PlayerController Player;
    public int PlayerScore;
    [SerializeField] List<int> JumpScores = new List<int>();
    [SerializeField] TextMeshProUGUI AirPointText;

       

    private void Awake()
    {
        GlobalEventScript.RestartGame.AddListener(Restart);
        GlobalEventScript.OnPlayerUsedCombo.AddListener(FlyComboText);
    }
    void FixedUpdate()
    {
        PlayerScore = EndGameScore(1);
        if (Player.InAir)
        {
            AirPointText.gameObject.SetActive(true);
            _playerScore.UpSore();
            AirPointText.text ="+ " + _playerScore._score.ToString();
        }
        else
        {
            if (AirPointText.gameObject.activeSelf == true)
            {
                StartFlyText();
                AirPointText.gameObject.SetActive(false);
            }
            
            if (_playerScore._score > 0)
            {
                JumpScores.Add(_playerScore._score);
                _playerScore.Restart();
            }
        }
        
    }
    private void Restart() 
    {
        JumpScores.Clear();
        _playerScore.Restart();
    }
    public int EndGameScore(float LandBonus)
    {
        int EndScore = 0;
        for (int i = 0; i < JumpScores.Count; i++)
        {
            EndScore += JumpScores[i];
        }
        EndScore = Mathf.RoundToInt(EndScore*LandBonus);
        return EndScore;
    }
    private void StartFlyText()
    {
        TextMeshProUGUI Text = Instantiate(AirPointText, AirPointText.transform.parent.transform);
        Text.gameObject.GetComponent<DieTextScript>().enabled = true;
    }

    private void FlyComboText(int ComboNumer)
    {
        TextMeshProUGUI Text = Instantiate(AirPointText, AirPointText.transform.parent.transform);
        ComboScript.ComboInfo PrintCombo = ComboScript.Combos[ComboNumer];
        Text.text = PrintCombo.ComboName + " +" + PrintCombo.ComboBonus;
        JumpScores.Add(PrintCombo.ComboBonus);
        Text.gameObject.GetComponent<DieTextScript>().enabled = true;
    }
}
