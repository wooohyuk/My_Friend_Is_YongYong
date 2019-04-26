using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Pang_GPSManager : MonoBehaviour
{
    [SerializeField]
    private Text textGps;

    public static Pang_GPSManager instance;

    private void Awake()
    {
        instance = this;
    }

    float Old_Lat;
    float Old_Lot;

    float Cur_Lat;
    float Cur_Lot;

    float Res_Lat;
    float Res_Lot;

    double Result;
    double Result_Sum=50f;

    double detailed_num = 1.0f;

    const double RAD = 0.000008998719243599958;

    public UnityEngine.UI.Image AA;

    public UnityEngine.UI.Image BB;

    double lastResult;

    public void YMCA()
    {
        if (Old_Lat >= Cur_Lat)
        {
            Res_Lat=Old_Lat - Cur_Lat;
        }
        else
        {
            Res_Lat = Cur_Lat- Old_Lat;
        }
        if (Old_Lot >= Cur_Lot)
        {
            Res_Lot = Old_Lot - Cur_Lot;
        }
        else
        {
            Res_Lot = Cur_Lot - Old_Lot;
        }

        Result = Mathf.Sqrt(Res_Lat * Res_Lat + Res_Lot * Res_Lot);

        Result = Result / RAD;
    }

    void Jogging_Bar()
    {
       // if(Result_Sum - Result_Sum != 0)
        AA.transform.Translate(660/(float)Result_Sum, 0, 0);
    }

    public void StartTakeAWay()
    {
        Old_Lat = Pang_gps.Instance.latitude;
        Old_Lot = Pang_gps.Instance.longitude;
        Result_Sum = 0;
        UIManager.instance.jogingGague.alpha = 1;
    }

    private void Update()
    {
        YMCA();
        if (Result != lastResult)
        {
            Result_Sum = Result_Sum + Result; 
            lastResult = Result;
            Jogging_Bar();
        }
        BB.fillAmount = (float)Result_Sum / 50;
        if (Result_Sum >= 50f) //50 도달 시 50에 고정
        {
            Result_Sum = 50f;

        }
        Cur_Lat = Pang_gps.Instance.latitude;
        Cur_Lot = Pang_gps.Instance.longitude;

        //if(!(Cur_Lat == Old_Lat && Cur_Lot == Old_Lot))
        //{
         //   YMCA();

 ///           Old_Lat = Cur_Lat;
   //         Old_Lot = Old_Lot;
    //    }
    }
}
