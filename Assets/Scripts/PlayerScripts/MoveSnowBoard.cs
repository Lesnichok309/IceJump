using UnityEngine;

public class MoveSnowboard
{
    Transform _player;
    Rigidbody _rb;
    public void MoveAD(float Horizontal,float speed)
    {   
        if (Horizontal != 0)
        {
            _rb.AddForce(_player.right * speed * 2 * Horizontal);
            _player.Rotate(Vector3.up * Horizontal);
            _player.Rotate(Vector3.forward/2 * -Horizontal);
        }
    }
    public void MoveForward(float speed)
    {
        _rb.AddForce(_player.forward * speed);
    }     
    public void StayBalance()
    {
        Vector3 PlayerRotate = _player.localEulerAngles;
        if (PlayerRotate.z > 180)
        {
            PlayerRotate += Vector3.forward * 0.2f;
        }
        else
        {
            if (PlayerRotate.z < 180)
            { 
                PlayerRotate -= Vector3.forward * 0.2f;
            }
        }

        _player.eulerAngles = PlayerRotate;
    }
    public void Jump(float JumpSpeed)
    {
        _rb.AddForce(_player.transform.up * JumpSpeed,ForceMode.Impulse);
    }
    public void Initiate(Transform PlayerObject, Rigidbody PlayerRB)
    { 
        _player = PlayerObject;
        _rb = PlayerRB;       
    }

    public void StopMove()
    {
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
    }
}
