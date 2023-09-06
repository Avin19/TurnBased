using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

    private const int ACTION_POINT_MAX =2;
    private Vector3 targetPosition;
    private GridPosition gridPosition;
    private MoveAction moveAction;
    private SpinAction spinAction;
    private int actionPoints =ACTION_POINT_MAX;
    private BaseAction[] baseActionArray;
    private void Awake() {
        moveAction  = GetComponent<MoveAction>();
        spinAction = GetComponent<SpinAction>();
        baseActionArray = GetComponents<BaseAction>();

    }
    
    private void Start() {
        gridPosition= LevelGRid.Instance.GetGridPosition(transform.position);
        LevelGRid.Instance.AddUnitAtGridPosition(gridPosition,this);
        TurnSystem.Instance.OnEndTurn += OnEndTurn;
    }

    private void OnEndTurn(object sender, EventArgs e)
    {
        actionPoints = ACTION_POINT_MAX;
    }

    private void Update()
    {
      
        GridPosition newGridPosition = LevelGRid.Instance.GetGridPosition(transform.position);
        if( newGridPosition != gridPosition)
        {
            //unit has changed the position ;
            LevelGRid.Instance.UnitMoveGridPosition(this, gridPosition, newGridPosition);
            gridPosition = newGridPosition;
        }
        
    }
    public SpinAction GetSpinActionUnit()
    {
        return spinAction;
    }

    public MoveAction GetMoveActionUnit()
    {
            return moveAction;
    }
    public GridPosition GetGridPosition()
    {
        return gridPosition;
    }
    public BaseAction[] GetBaseActions()
    {
        return baseActionArray;
    }
    public bool TrySpendPointToTakeAnAction(BaseAction baseAction)
    {
        if(CanSpendPointToTakeAnAction(baseAction))
        {
            SpendActionPoint(baseAction.GetActionPointCost());
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool CanSpendPointToTakeAnAction( BaseAction baseAction)
    {
        if(actionPoints >= baseAction.GetActionPointCost())
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private void SpendActionPoint(int amount)
    {
        actionPoints -= amount;
    }
    public int GetActionPoint()
    {
        return actionPoints;
    }
}
