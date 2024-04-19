using UnityEngine;

public class AnimationScript
{
    Animator _animator;

    public enum PlayerAnimation
    {
        Idle,Jump,Crouch
    }
    public PlayerAnimation Animating { get; private set; } = PlayerAnimation.Idle;

    public void Initialize(Animator animator)
    {
        _animator = animator;
    }

    public void StartAnimation(PlayerAnimation NextAnimation)
    { 
        switch (NextAnimation)
        {
            case PlayerAnimation.Idle:
                Landing();
                StandUp();
                EndCombo();
                break;
            case PlayerAnimation.Jump:
                break;

            case PlayerAnimation.Crouch:
                break;
        }
    }

    public void Crouch()
    {
        _animator.SetBool("Crouch",true);
        Animating = PlayerAnimation.Crouch;
    }
    public void StandUp()
    {
        _animator.SetBool("Crouch", false);
        Animating = PlayerAnimation.Idle;
    }
    public void InAir()
    {
        _animator.SetBool("InAir", true);
        Animating = PlayerAnimation.Jump;
    }
    public void Landing()
    {
        _animator.SetBool("InAir", false);
        Animating = PlayerAnimation.Idle;
    }
    public void EndCombo()
    {
        _animator.SetInteger("Combo", 0);
    }
}
