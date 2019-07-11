using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Out : MonoBehaviour
{
    public delegate void OutAction();
    public static event OutAction OnOut;

    private Ball ball;

    private void Start()
    {
        ball = FindObjectOfType<Ball>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            if (ball.IsFree)
                OnOut?.Invoke();
        }
    }
}
