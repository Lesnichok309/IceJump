using UnityEngine;

public class CameraScript : MonoBehaviour
{
    
    FollowScript FollowScript = new FollowScript();

    [SerializeField] Transform _player;
    [SerializeField] float _speed;
    [SerializeField] Transform _jumpCamera;

    public bool GoToJump = false;

    Vector3 _cameraDistance = new Vector3(0,5,-5);
    Quaternion _defCameraRotation;

    Vector3 _jumpPointDistance;
    


    private void Awake()
    {
        GlobalEventScript.RestartGame.AddListener(Restart); 

        transform.position = _player.position + _cameraDistance; // Выдержать расстояние камеры после генерации мира

        _defCameraRotation = transform.rotation;                    //Запомнить угол поворота
        _jumpPointDistance = _jumpCamera.position-_player.position; // Расстояние JumpCamera относительно игрока

        _jumpCamera.SetParent(null);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            GoToJump = !GoToJump;    
        }
        
        if (GoToJump) 
        {
            transform.position = FollowScript.Move(transform, _jumpCamera, _speed/2);
            transform.rotation = FollowScript.Rotate(_jumpCamera.rotation, transform, _speed/2);
        }
        else
        {
            transform.position = FollowScript.Move(transform, _player, _cameraDistance, _speed);
            transform.rotation = FollowScript.Rotate(_defCameraRotation, transform, _speed);
        }

        _jumpCamera.position = FollowScript.Move(_jumpCamera, _player, _jumpPointDistance, _speed);
     
    }
    private void Restart()
    {
        GoToJump = false;
    }

}
