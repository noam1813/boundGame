using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class timeManager : MonoBehaviour
{
    public static timeManager instance;
    [SerializeField] private Text timetext;

    [SerializeField] private float startTime;

    private float nowTime;

    private IDisposable countDownObservable;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        nowTime = startTime;
        countDownObservable = Observable.Interval(TimeSpan.FromSeconds(0.1f)).Subscribe(_ =>
        {
            CountDown();
        }).AddTo(this);
    }
    

    public void CountDown()
    {
        nowTime -= 0.1f;
        ShowText();   
    }

    void ShowText()
    {
        timetext.text = (Mathf.Floor(nowTime*10f) / 10f).ToString();
    }
}
