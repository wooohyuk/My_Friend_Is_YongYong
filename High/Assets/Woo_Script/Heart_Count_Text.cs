using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heart_Count_Text : MonoBehaviour
{
    public Text Heart_Count;

    private void Update()
    {
        Heart_Count.text = GameManager.instance.heartCount.ToString();
    }
}
