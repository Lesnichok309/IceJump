using UnityEngine;

public class FollowScript
{
    public Quaternion Rotate(Quaternion Angle,Transform HoRotate, float Speed) // поворачивать объект в сторону определенного угла
    {
        return Quaternion.Slerp(HoRotate.rotation, Angle, Speed * Time.deltaTime);
    }

    public Vector3 Move(Transform HoMove, Transform Target, Vector3 Distance,float Speed) // Подвинуть объект на позицию от цели
    {
        return Vector3.Slerp(HoMove.position, Target.position + Distance, Speed * Time.deltaTime);
    }

    public Vector3 Move(Transform HoMove, Transform Target, float Speed)   // Перегрузка Move без дистанции(в координаты цели)
    {
        return Move(HoMove, Target, Vector3.zero, Speed);
    }

}
