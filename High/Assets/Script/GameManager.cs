using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int heartCount;

    private void Awake()
    {
        instance = this;
    }
}
