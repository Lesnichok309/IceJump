using UnityEngine;

public class СameraWallScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Camera.main.GetComponent<CameraScript>().GoToJump = true;
        }
    }
}
