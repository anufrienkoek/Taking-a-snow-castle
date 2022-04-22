using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Progress : MonoBehaviour
{
    public int Coins;
    public int Level;
    public Color BackgroundColor;
    public bool isMusicOn;

    public static Progress Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        Load();
    }

    public void SetLevel(int level)
    {
        Level = level;
        Save();
    }

    public void AddCoins(int value)
    {
        Coins += value;
        Save();
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        DeleteFile();
        Load();
    }
    
    [ContextMenu("DeleteFile")]
    public void DeleteFile()
    {
        SaveSystem.DeleteFile();
    }
    
    [ContextMenu("Save")]
    public void Save()
    {
        SaveSystem.Save(this);
    }
    
    [ContextMenu("Load")]
    public void Load()
    {
        ProgressData progressData = SaveSystem.Load();
        if (progressData != null)
        {
            Coins = progressData.Coins;
            Level = progressData.Level;

            Color color = new Color
            {
                r = progressData.BackgroundColor[0],
                g = progressData.BackgroundColor[1],
                b = progressData.BackgroundColor[2]
            };
            BackgroundColor = color;

            isMusicOn = progressData.isMusicOn;
        }
        else
        {
            Coins = 0;
            Level = 1;
            BackgroundColor = Color.black;
            isMusicOn = true;
        }
    }
}
