using UnityEngine;

public class MoveInAir
{ 
    public void RotateWASD(Transform Object, float Horizontal, float Vertical,float Speed)
    {
        Object.Rotate(Vector3.forward * - Horizontal * Speed);
        Object.Rotate(Vector3.right * Vertical * Speed);
    }
}
