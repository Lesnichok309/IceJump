using UnityEngine;
using UnityEngine.Events;

public static class GlobalEventScript 
{
    public static UnityEvent<float> OnPlayerLand = new UnityEvent<float>();

    public static UnityEvent<int> OnPlayerUsedCombo = new UnityEvent<int>();

    public static UnityEvent RestartGame = new UnityEvent();

    public static void SendPlayerLand(float Bonus)
    {
        OnPlayerLand.Invoke(Bonus);
    }
    public static void SendRestartGame()
    {
        //Time.timeScale = 0;
        RestartGame.Invoke();
    }

    public static void SendPlayerUsedCombo(int ComboNumber)
    {
        OnPlayerUsedCombo.Invoke(ComboNumber);
    }
}
