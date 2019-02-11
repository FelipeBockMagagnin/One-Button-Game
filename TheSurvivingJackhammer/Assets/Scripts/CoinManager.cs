﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    private int coins;
    CoinManager instance;
    public static int coinMultiplicator;
    public int earnedcoins;
    public GameObject CoinParticle;
    MarketManager marketManager;
    

    bool firstTime = true;

    private void Awake()
    {
        HighScoreManager.points = 0;
        marketManager = GameObject.Find("MarketManager").GetComponent<MarketManager>();
        DontDestroyOnLoad();
        ResetCoinMultiplicator();
    }

    private void Start()
    {
        LoadCoins();
    }

    private void Update()
    {
        coinMultiplicator = (HighScoreManager.points / 10) + 1;
        
    }


    public void ResetCoinMultiplicator()
    {
        coinMultiplicator = 1;
        earnedcoins = 0;
        HighScoreManager.points = 0;
    }

    public void IncreseCoinMultiplicator()
    {
        coinMultiplicator += 2;
    }

    private void DontDestroyOnLoad()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("CoinManager");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    public void DecreaseCoins(int amount)
    {
        coins -= amount;
        Debug.Log("Coins gastos: " + amount + " restando " + coins + " coins");
        SaveCoins();
    }

    public void ChangeCoins()
    {
        if (!firstTime)
        {
            SaveCoins();
        }
        firstTime = false;
    }

    public int GetCoins()
    {
        return coins;
    }

    public void SaveCoins()
    {
        marketManager.SaveBuy();
        coins += earnedcoins;
        earnedcoins = 0;
        PlayerPrefs.SetInt("Coins", coins);
        HighScoreManager.CheckScore();
    }

    public void InstantiateCoinParticle(Transform transform)
    {
        GameObject coin = Instantiate(CoinParticle, transform.position, Quaternion.identity);
        Destroy(coin, 5);
    }

    public void IncreaseEarnedCoins()
    {
        earnedcoins += 1 * coinMultiplicator;
        print("coinmanager earned coins: " + earnedcoins);
    }

    private void LoadCoins()
    {
        coins = PlayerPrefs.GetInt("Coins");
        Debug.Log(PlayerPrefs.GetInt("Coins"));
    }

    


}
