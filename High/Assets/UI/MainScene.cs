using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainScene : MonoBehaviour
{
    public Image touch2Start;
    bool isFaidIn = false;

    void Update()
    {
        Color c = touch2Start.color;

        if (c.a > 0.5f && !isFaidIn)
            c.a -= 0.01f;
        else if (c.a <= 0.5f && !isFaidIn)
            isFaidIn = true;
        if (c.a < 1 && isFaidIn)
            c.a += 0.02f;
        else if (c.a >= 1 && isFaidIn)
            isFaidIn = false;

        touch2Start.color = c;

        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}
