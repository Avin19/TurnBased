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
        if (!TurnSystem.Instance.IsPlayerTurn())
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
                    if(TryTakeEnemyAIAction(SetSateTakingTurn))
                    {
                        state = State.Busy;
                    }
                    else
                    {
                        TurnSystem.Instance.NextTurn();
                    }
                }
                break;
            case State.Busy:
                break;

        }

    }
    private void SetSateTakingTurn()
    {
        if (!TurnSystem.Instance.IsPlayerTurn())
        {
            timer = 0.5f;
            state = State.TakingTurn;
        }
    }
    private bool TryTakeEnemyAIAction(Action onEnemyAIActionComplete)
    {
        foreach (Unit enemyUnit in UnitManager.Instance.GetEnemyList())
        {
            if (TryTakeEnemyAIAction(enemyUnit, onEnemyAIActionComplete))
            {
                return true;
            }
        }
        return false;
    }
    private bool TryTakeEnemyAIAction(Unit enemyUnit, Action onEnemyAIActionComplete)
    {
        SpinAction spinAction = enemyUnit.GetSpinAction();
        GridPosition actionGridPosition = enemyUnit.GetGridPosition();
        if (!spinAction.IsVaildActionGridPosition(actionGridPosition))
        {

            return false;
        }
        if (!enemyUnit.TrySpendPointToTakeAnAction(spinAction))
        {
            return false;

        }

        spinAction.TakeAction(actionGridPosition, onEnemyAIActionComplete);
        return true;

    }
}
