using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieTextScript : MonoBehaviour
{
    [SerializeField] float DieTime;
    [SerializeField] float Speed;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, DieTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * Speed *Time.deltaTime);
    }
}
