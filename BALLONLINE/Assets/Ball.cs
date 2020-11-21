using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class Ball : MonoBehaviour
{
    [SerializeField] private float dashHorizontalSpeed;
    [SerializeField] private float horizontalSpeed;

    [SerializeField] private float dashVerticalSpeed;
    [SerializeField] private float verticalSpeedAccel;

    [SerializeField] private int score;

    private Rigidbody rd;
    [SerializeField] private BallText text;

    [SerializeField] private bool isMove;

    public float verticalSpeed;

    private bool isDead;
    // Start is called before the first frame update
    void Start()
    {
        rd = GetComponent<Rigidbody>();
        score = 0;
        
        text.UpdateText(0);
    }


    void Update()
    {
        if (!isDead)
        {
            PlayerMove();
        }

    }


    private void PlayerMove()
    {
        Vector3 velocity = Vector3.Lerp(rd.velocity, new Vector3(0f, 0f, 0f),0.02f);
        float axis = default;
        
        if (Input.GetKeyDown(GetKeyCode("Left")))
        {
            axis = -1;
        }
        else if(Input.GetKeyDown(GetKeyCode("Right")))

        {
            axis = 1;
        }

        if (Input.GetKeyDown(GetKeyCode("Left")) || Input.GetKeyDown(GetKeyCode("Right")))
        {
            velocity = new Vector3(rd.velocity.x+dashHorizontalSpeed * Mathf.Sign(axis),rd.velocity.y,0f);
            if (rd.velocity.x > dashHorizontalSpeed*1.5f)
            {
                velocity.x = dashHorizontalSpeed;
            }

            if (rd.velocity.x < -dashHorizontalSpeed*1.5f)
            {
                velocity.x = -dashHorizontalSpeed;
            }
        }

        if (Input.GetKeyDown(GetKeyCode("Down")))
        {
            Debug.Log("Dash");
            verticalSpeed = 0f;
            velocity.y -= dashVerticalSpeed;
        }
        
        //画面端処理

        if (
            (transform.position.x < systemManager.instance.leftTopPos.x && velocity.x < 0) ||
            (transform.position.x > systemManager.instance.rightDownPos.x && velocity.x > 0)
            )
        {
            velocity.x *= -1f;
        }
        
        verticalSpeed -= verticalSpeedAccel * Time.deltaTime;
        transform.position += new Vector3(0f, verticalSpeed, 0f) * Time.deltaTime;

        rd.velocity = velocity;
    }

    private void HitToBar()
    {
        Debug.Log("OK");
        if (rd.velocity.y < 0f)
        {
            rd.velocity = new Vector3(rd.velocity.x,-rd.velocity.y,0f);    
        }

        var rad = Mathf.Atan2(30f,rd.velocity.x);
        Debug.Log(rd.velocity);
        Debug.Log(rad);
        if (verticalSpeed < 0f)
        {
            verticalSpeed = Mathf.Abs(Mathf.Sin(rad)*30f);    
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bar"))
        {
            HitToBar();
        }

        if (other.gameObject.CompareTag("Ball"))
        {
            var distance = GetDistance() + other.gameObject.GetComponent<Ball>().GetDistance();
            Debug.Log(distance);
            Vector3 enemyPos = other.gameObject.transform.position;
            Vector3 myPos = transform.position;
            
            Vector2 dxy= new Vector2(enemyPos.x-myPos.x,enemyPos.y-myPos.y);
            float rad = (Mathf.Atan2(dxy.y, dxy.x)*Mathf.Rad2Deg+180f)*Mathf.Deg2Rad;
            verticalSpeed = 0f;
            rd.velocity = new Vector3(distance*Mathf.Cos(rad),distance*Mathf.Sin(rad),0f);

        }

        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            score++;
            text.UpdateText(score);
        }
        
        if(other.gameObject.CompareTag("Needle"))
        {
            isDead = true;
            verticalSpeed = 0;
            gameObject.SetActive(false);
            int lostScore = score / 2;
            score -= lostScore;
            text.UpdateText(score);
            
            SpawnManager.instance.SpawnLoop(lostScore);
            Observable.Timer(TimeSpan.FromSeconds(1)).Subscribe(_ =>
            {
                isDead = false;
                gameObject.SetActive(true);
                rd.velocity = Vector3.zero;
                transform.position = new Vector2(Random.Range(-8f, 16f), 8f);
            }).AddTo(this);
        }
        
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Bar"))
        {
            HitToBar();
        }
    }
    
    public float GetDistance()
    {
        var distance = new Vector3(rd.velocity.x,rd.velocity.y-verticalSpeed,0f).magnitude;
        return distance;
    }


    private KeyCode GetKeyCode(string key)
    {
        if (isMove)
        {
            switch (key)
            {
                case "Left":
                    return KeyCode.LeftArrow;
                    break;
                case "Right":
                    return KeyCode.RightArrow;
                    break;
                case "Down":
                    return KeyCode.DownArrow;
                    break;
                default:
                    return KeyCode.Escape;
            }
        }
        else
        {
            switch (key)
            {
                case "Left":
                    return KeyCode.A;
                    break;
                case "Right":
                    return KeyCode.D;
                    break;
                case "Down":
                    return KeyCode.S;
                    break;
                default:
                    return KeyCode.Escape;
            }
        }
    }
}
