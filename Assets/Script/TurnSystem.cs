using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{
    public static TurnSystem Instance { get; private set; }
    private int turnNumber=1;

    private bool isplayerTurn = true;

    public event EventHandler OnEndTurn;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void NextTurn()
    {
        turnNumber++;
        isplayerTurn = !isplayerTurn;
        OnEndTurn?.Invoke(this, EventArgs.Empty);
    }


    public int GetTurnNumber()
    {
        return turnNumber;
    }
    public bool IsPlayerTurn()
    {
        return isplayerTurn;
    }
}
