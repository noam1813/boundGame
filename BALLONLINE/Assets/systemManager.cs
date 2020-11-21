using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class systemManager : MonoBehaviour
{
    public static systemManager instance;

    [SerializeField] private Camera mainCamera;

    public Vector3 leftTopPos;
    public Vector3 rightDownPos;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        leftTopPos = new Vector3(-12f,-8f,0f);
        rightDownPos = new Vector3(12f,8f,0f);
        
        Debug.Log("Position setting");
        Debug.Log (getScreenTopLeft ().x + ", " + getScreenTopLeft ().y);
        Debug.Log (getScreenBottomRight ().x + ", " + getScreenBottomRight ().y);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && Input.GetKey(KeyCode.LeftControl))
        {
            SceneManager.LoadScene(0);
        }
    }
    
    private Vector3 getScreenTopLeft() {
        // 画面の左上を取得
        return Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
    }

    private Vector3 getScreenBottomRight() {
        // 画面の右下を取得
        return Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));
    }
}
