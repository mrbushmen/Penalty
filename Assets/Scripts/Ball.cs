using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody rb;

    private bool isKickable = true;
    public bool IsFree { get; private set; } = true;  //Свойство используется для исключения двойных противоположных срабатываний

    private Vector3 startPos;
        
    private void OnEnable()
    {
        Goal.OnGoal += GoalOrOut;
        Goalkeeper.OnCatch += Catched;
        Out.OnOut += GoalOrOut;
    }

    private void OnDisable()
    {
        Goal.OnGoal -= GoalOrOut;
        Goalkeeper.OnCatch -= Catched;
        Out.OnOut -= GoalOrOut;        
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();        
        startPos = transform.position;
        isKickable = true;
        IsFree = true;
    }

    public void Catched()
    {
        IsFree = false;
        Stop();
        StartCoroutine(ResetPosition());
    }
    public void GoalOrOut()
    {
        IsFree = false;
        StartCoroutine(ResetPosition());
    }

    private void OnMouseDown() //для отладки
    {
        #if UNITY_EDITOR            
            Move(Vector3.forward, 700f);
        #endif
    }

    public void Move(Vector3 direction, float force)
    {
        if (isKickable)
        {
            if (force > 1000) force = 1000; //Ограничение силы удара
            float upForce = force / 5100f;
            direction += Vector3.up * upForce;
            rb.AddForce(direction * force);
            isKickable = false;
        }
    }

    private void Stop()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    IEnumerator ResetPosition()
    {
        yield return new WaitForSeconds(GameController.instance.restartDelay);
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.position = startPos;
        isKickable = true;
        IsFree = true;
    }
}
