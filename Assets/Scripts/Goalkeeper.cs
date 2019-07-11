using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goalkeeper : MonoBehaviour
{

    public delegate void CatchAction();
    public static event CatchAction OnCatch;

    private bool isCatching = true;
    private bool isMovingRight= true;

    private Transform myTransform;

    [SerializeField]
    [Range(1f,3f)]
    private float speed=1f;

    private Vector3 startPos;
    private Vector3 leftPos;
    private Vector3 rightPos;

    private Ball ball;

    void Start()
    {  
        myTransform = transform;
        startPos = myTransform.position;
        leftPos = startPos + new Vector3(-2, 0, 0); //Добавить настройку?
        rightPos = startPos + new Vector3(2, 0, 0);

        ball = FindObjectOfType<Ball>();
    }
        
    void Update()
    {
        if (isCatching)
        {
            if (isMovingRight)
                MoveRight();
            else
                MoveLeft();
        }
    }

    void MoveLeft()
    {
        myTransform.position = Vector3.MoveTowards(myTransform.position, rightPos+Vector3.right, Time.deltaTime * speed);
        if (myTransform.position.x > rightPos.x)
            isMovingRight = !isMovingRight;
    }

    void MoveRight()
    {
        myTransform.position = Vector3.MoveTowards(myTransform.position, leftPos + Vector3.left, Time.deltaTime*speed);
        if (myTransform.position.x < leftPos.x)
            isMovingRight = !isMovingRight;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            isCatching = false;
            if (ball.IsFree)
            {
                OnCatch?.Invoke();
                StartCoroutine(ResetPosition());
            }
        }
    }
    
    IEnumerator ResetPosition()
    {
        yield return new WaitForSeconds(GameController.instance.restartDelay);
        transform.position = startPos;
        isCatching = true;
    }
}
