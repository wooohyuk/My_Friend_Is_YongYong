using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stress_Bar : MonoBehaviour
{
    public UnityEngine.UI.Image Fill;
    private YongYong yong;
    float gauge;
    
    // Use this for initialization
    void Start()
    {

    }

    private void Awake()
    {
        yong = FindObjectOfType<YongYong>();
    }

    // Update is called once per frame
    void Update()
    {
        gauge = yong.stress/100f;
        Fill.fillAmount = gauge;
    }
}
