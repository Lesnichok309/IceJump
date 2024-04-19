using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class ScoreScript
{
    public int _score { get; private set; }

    public void UpSore() 
    {
         _score++;
    }
    public void UpScore(int Bonus)
    {
        if (Bonus > 0)
        {
            _score += Bonus;
        }
    }
    public int EndLevelScore(float Bonus) 
    {
        int EndScore = Mathf.RoundToInt(_score * Bonus);
        return EndScore;
    }
    public bool CheckWin(int HaveScore, int NeedScore)
    {
        if (HaveScore >= NeedScore)
        {
            return true;
        }
        else
        {
            return false;
        }
    }  

    
    public void Restart()
    {
        _score = 0;
    }
}
