using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartCanvas : MonoBehaviour
{
    public static HeartCanvas instance;

    public GameObject heart;

    void Awake()
    {
        instance = this;
    }

    public void MakeHeart()
    {
        Soundclip_Changer.hotsix = 3;
        Instantiate(heart, new Vector2(0, -200), Quaternion.identity, transform);
        StartCoroutine(UIManager.instance.ChangeTextBox("오홍홍 조아용~!"));
    }
}