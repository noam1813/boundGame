using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BallText : MonoBehaviour
{
    private TextMeshPro text;


    private void Start()
    {
        text = GetComponent<TextMeshPro>();
    }
    


    public void UpdateText(int score)
    {
        text.text = score.ToString();
    }
}
