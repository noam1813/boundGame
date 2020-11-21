using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour
{
    [SerializeField] private float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerCore.instance.isBarPlayer)
        {
            var axis = Input.GetAxis("Horizontal");
            transform.position += new Vector3(axis*speed,0f,0f)*Time.deltaTime;
        }
    }
}
