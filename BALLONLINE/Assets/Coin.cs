using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private Rigidbody rd;
    
    public float verticalSpeed;
    [SerializeField] private float verticalSpeedAccel;
    
    
    void Start()
    {
        rd = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        verticalSpeed -= verticalSpeedAccel * Time.deltaTime;
        transform.position += new Vector3(0f, verticalSpeed, 0f) * Time.deltaTime;
    }

    void Update()
    {
        var axis = Input.GetAxis("Horizontal");

            Vector3 velocity = rd.velocity;

            velocity = Vector3.Lerp(velocity, new Vector3(0f, 0f, 0f),0.02f);

            rd.velocity = velocity;
    }
    
    
    private void HitToBar()
    {
        Debug.Log("OK");
        if (rd.velocity.y < 0f)
        {
            rd.velocity = new Vector3(rd.velocity.x,-rd.velocity.y,0f);    
        }
        
        if (verticalSpeed < 0f)
        {
            verticalSpeed = 20f;    
        }
    }
    

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bar"))
        {
            HitToBar();
        }
    }
    

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Bar"))
        {
            HitToBar();
        }
    }
}
