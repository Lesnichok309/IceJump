using UnityEngine;

public class TargetScript : MonoBehaviour
{
    public static bool Toutch;
    [SerializeField] float _xScore;
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "PlayerBody" && !Toutch)
        {
            Grounded(other.collider, 0);
            print("OnHead!");
        }
        else
        {
            if (other.gameObject.CompareTag("Player") && !Toutch)
            {
                Grounded(other.collider, _xScore);
                print("GoodLand");
            }
        }

        
    }

    private void Grounded(Collider other, float BonusScore)
    {
        print(BonusScore);
        GlobalEventScript.SendPlayerLand(BonusScore);
        Toutch = true;

        PlayerController Player;// = other.GetComponent<PlayerController>(out Player);
        if (other.TryGetComponent<PlayerController>(out Player))
        {
            Player.StopMove();
        }
        
        
    }
}
