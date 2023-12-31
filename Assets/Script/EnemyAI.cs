using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public enum State
    {
        WaitingForEnemyTurn,
        TakingTurn,
        Busy,
    }
    private State state;
    private float timer;
    private void Awake()
    {
        state = State.WaitingForEnemyTurn;
    }
    private void Start()
    {
        TurnSystem.Instance.OnEndTurn += OnEndTurn;
    }

    private void OnEndTurn(object sender, EventArgs e)
    {
        if(!TurnSystem.Instance.IsPlayerTurn())
        {
            state = State.TakingTurn;
            timer = 2f;
        }
        
    }

    private void Update()
    {
        if (TurnSystem.Instance.IsPlayerTurn())
        {
            return;
        }
        switch (state)
        {
            case State.WaitingForEnemyTurn:
                break;
            case State.TakingTurn:
                timer -= Time.deltaTime;
                if (timer <= 0f)
                {
                    state = State.Busy;
                    //TakeEnemyAiAction(SetSateTakingTurn);
                    TurnSystem.Instance.NextTurn();
                }
                break;
            case State.Busy:
                break;

        }

    }
    private void SetSateTakingTurn()
    {
        timer = 0.5f;
        state =State.TakingTurn;
    }
    private void TakeEnemyAiAction(Action onEnemyAIActionComplete)
    {
            foreach(Unit enemyUnit in UnitManager.Instance.GetEnemyList())
            {
                TakeEnemyAiAction(enemyUnit, onEnemyAIActionComplete);
            }
    }
    private void TakeEnemyAiAction(Unit unit, Action onEnemyAIActionComplete)
    {
        
    }
}
