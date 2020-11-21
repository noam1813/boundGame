using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class NeedleManager : MonoBehaviour
{
    [SerializeField] private Vector3 speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = new Vector2(-1+Random.Range(0, 2), -1+Random.Range(0, 2))*3f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += speed * Time.deltaTime;
        
        if (
            (transform.position.x < systemManager.instance.leftTopPos.x && speed.x < 0) ||
            (transform.position.x > systemManager.instance.rightDownPos.x && speed.x > 0)
        )
        {
            speed.x *= -1f;
        }
        
        if (
            (transform.position.y < systemManager.instance.leftTopPos.y && speed.y < 0) ||
            (transform.position.y > systemManager.instance.rightDownPos.y && speed.y > 0)
        )
        {
            speed.y *= -1f;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bar"))
        {
            speed.y *= -1f;
        }
    }
}
