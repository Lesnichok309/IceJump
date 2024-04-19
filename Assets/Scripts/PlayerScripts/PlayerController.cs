using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    MoveSnowboard BoardMoves = new MoveSnowboard();
    MoveInAir AirMoves = new MoveInAir();
    AnimationScript MyAnimation = new AnimationScript();
    
    [SerializeField] Animator MyAnimator;
    [SerializeField] float _speed;
    [SerializeField] float _jumpSpeed;
    [SerializeField] float _jumpRotateSpeed;
    public bool InAir { get; private set;} = false;
    
    float _defSpeed;
    Vector3 _defPlayerPosition;
    Quaternion _defPlayerRotation;

    private void Start()
    {
        GlobalEventScript.RestartGame.AddListener(Restart);
    
        BoardMoves.Initiate(transform, GetComponent<Rigidbody>());
        MyAnimation.Initialize(MyAnimator);

        _defSpeed = _speed;
        _defPlayerPosition = transform.position;
        _defPlayerRotation = transform.rotation;
    }

    private void FixedUpdate()
    {
        
        if (!InAir)
        {
            BoardMoves.StayBalance();
            Accelerate();
            BoardMoves.MoveForward(_speed);
            BoardMoves.MoveAD(Input.GetAxis("Horizontal"), _speed);
        }
        else
        {
            AirMoves.RotateWASD(transform, Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), _jumpRotateSpeed);
        }
    }

    private void Update()
    {
        if (!InAir)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                BoardMoves.Jump(_jumpSpeed);            
            }
        }
        
        
    }

    private void Accelerate()
    {
        if (Input.GetKey(KeyCode.S))
        {
            _speed = _defSpeed * 2;
            MyAnimation.Crouch();
        }
        else
        {
            _speed = _defSpeed;
            MyAnimation.StandUp();
        }
    }   

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            CancelInvoke();
            InAir = false;
            MyAnimation.Landing();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Invoke("StartFly",0.5f);
        }
    }

    public void StopMove()
    {
        BoardMoves.StopMove();
        InAir = false;
    }
    private void StartFly()
    {
        InAir = true;
        MyAnimation.InAir();
    }
    private void Restart()
    {
        this.enabled = true;
        StopMove();
        InAir = false;
        MyAnimation.StartAnimation(AnimationScript.PlayerAnimation.Idle);
        Rigidbody PlayerRig = GetComponent<Rigidbody>();
        PlayerRig.interpolation = RigidbodyInterpolation.None;
        PlayerRig.position = _defPlayerPosition;
        PlayerRig.rotation = _defPlayerRotation;
        PlayerRig.interpolation = RigidbodyInterpolation.Interpolate;
        
    }
}

