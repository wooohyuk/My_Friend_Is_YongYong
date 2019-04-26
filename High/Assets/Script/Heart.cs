using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    float temp = 0;
    float posX;

    private void Awake()
    {
        posX = Random.Range(-300, 300);
    }

    void Update()
    { 
        var pos = transform.position;

        pos.x = posX + 540 + Mathf.Sin( temp += 3 * Time.deltaTime) * 200;
        pos.y += 150 * Time.deltaTime;

        transform.position = pos;
    }

    public void HeartClick()
    {
        GameManager.instance.heartCount++;
        Soundclip_Changer.hotsix = 4;
        Destroy(gameObject);
    }
}
