using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int Money;
    public static int Lives;
    public int startLives = 20;
    public int startMoney = 400;
    private void Start()
    {
        Money = startMoney;
        Lives = startLives;
    }
}