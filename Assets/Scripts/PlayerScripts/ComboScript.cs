using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class ComboScript : MonoBehaviour
{
    [SerializeField] PlayerController Player;
    [SerializeField] Animator MyAnimator;

    int ComboNumber;
    bool GoodCombo = false;

    //[Header("WASD")]
    KeyCode[] ComboKeys = new KeyCode[4];

    [Header("Варианты комбо по кнопкам")]
    public string[] Combinations;

    [Header("Текущее комбо")]
    [SerializeField] string CurrentCombo;

    public static List<ComboInfo> Combos = new List<ComboInfo>();
    public class ComboInfo
    { 
        public string ComboName { get; private set; }
        public string ComboKey { get; private set; }
        public int ComboBonus { get; private set; }
        public int ComboNumber { get; private set; }

        public void CreateNewCombo(string Name,int BonusScore, int Number, string Key)
        { 
            ComboName = Name;
            ComboBonus = BonusScore;
            ComboNumber = Number;
            ComboKey = Key;
        }
    }
    private void Awake()
    {
        ComboKeys[0] = KeyCode.W;
        ComboKeys[1] = KeyCode.A;
        ComboKeys[2] = KeyCode.S;
        ComboKeys[3] = KeyCode.D;

        Combos.Add(new ComboInfo());
        Combos[0].CreateNewCombo("StopCombo", 0, 0, "13");

        Combos.Add(new ComboInfo());
        Combos[1].CreateNewCombo("GrabBoard", 50, 1, "22");

        Combos.Add(new ComboInfo());
        Combos[2].CreateNewCombo("Floor", 100, 2, "123");

        Combos.Add(new ComboInfo());
        Combos[3].CreateNewCombo("MutantJump", 100, 3, "032");

        Combos.Add(new ComboInfo());
        Combos[4].CreateNewCombo("EasyFall", 25, 4, "23");


        GlobalEventScript.RestartGame.AddListener(Restart);
    }
    private void Update()
    {

        if (Player.InAir)
        {
            for (int i = 0; i < ComboKeys.Length; i++)
            {
                if (Input.GetKeyDown(ComboKeys[i]))
                {
                    CurrentCombo += i;
                    CheckCombination(CurrentCombo);
                }
            } 
        }
        if (ComboNumber >= 0)
        {
            if (ComboNumber > 0 && MyAnimator.GetInteger("Combo") == 0)
            {
                GlobalEventScript.SendPlayerUsedCombo(ComboNumber);
            }
            MyAnimator.SetInteger("Combo", ComboNumber);
            ComboNumber = -1;
        }
    }
    private void CheckCombination(string Chek)
    {
        for (int i = 0; i < Combinations.Length; i++)
        {
            GoodCombo = false;
            string TestCombo = Combinations[i];

            if (TestCombo.Length >= Chek.Length)
            {
                for (int j = 0; j < Chek.Length; j++)
                {
                    if (Chek[j] == TestCombo[j])
                    {
                       GoodCombo = true;
                    }
                    else
                    { 
                       GoodCombo = false;
                       break;
                    }
                }
            }

            if (TestCombo == Chek)
            {
                ComboNumber = i;
                //print("Use combo number " + i);
                CurrentCombo = "";
                break;
            }
            else 
            {
                if (GoodCombo)
                {
                    StopAllCoroutines();
                    StartCoroutine(WaitComboTime());
                    break;
                }
            }   
        }
        if (!GoodCombo)
        {
            CurrentCombo = "";
            //print("BadCombo");
        }

    }
    IEnumerator WaitComboTime()
    {
        //print(CurrentCombo + " wait...");
        yield return new WaitForSeconds(1);
        //print("LoseComboTime");
        CurrentCombo = "";
    }
    private void Restart()
    {
        MyAnimator.SetInteger("Combo", 0);
        ComboNumber = 0;
        CurrentCombo = "";
    }

}
