using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{

    private YongYong yong;
    public GameObject food;

    private void Awake()
    {
        yong = FindObjectOfType<YongYong>();

    }

    private void Start()
    {
        //yong.isTakeAWalk = true;
        //Pang_GPSManager.instance.StartTakeAWay();

    }

    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.Space))
        {

            yong.stress = 0;
        }
    }
}
