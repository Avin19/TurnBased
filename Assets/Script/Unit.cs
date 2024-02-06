using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

    private const int ACTION_POINT_MAX = 2;
    public static event EventHandler OnAnyActionPointChange;
    public static event EventHandler OnAnyUitSpawned;
    public static event EventHandler OnAnyUnitDead;

    private Vector3 targetPosition;
    private GridPosition gridPosition;
    private MoveAction moveAction;
    private SpinAction spinAction;
    private HealthSystem healthSystem;
    private int actionPoints = ACTION_POINT_MAX;
    private BaseAction[] baseActionArray;

    [SerializeField] private bool isEnemy;
    private void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();
        moveAction = GetComponent<MoveAction>();
        spinAction = GetComponent<SpinAction>();
        baseActionArray = GetComponents<BaseAction>();

    }

    private void Start()
    {
        gridPosition = LevelGRid.Instance.GetGridPosition(transform.position);
        LevelGRid.Instance.AddUnitAtGridPosition(gridPosition, this);
        TurnSystem.Instance.OnEndTurn += OnEndTurn;
        healthSystem.OnDead += OnDead;
        OnAnyUitSpawned?.Invoke(this,EventArgs.Empty);
    }



    private void Update()
    {

        GridPosition newGridPosition = LevelGRid.Instance.GetGridPosition(transform.position);
        if (newGridPosition != gridPosition)
        {
            //unit has changed the position 
            GridPosition oldGridPosition = gridPosition;
             gridPosition = newGridPosition;
            LevelGRid.Instance.UnitMoveGridPosition(this, oldGridPosition, newGridPosition);
           
        }

    }
    public SpinAction GetSpinAction()
    {
        return spinAction;
    }

    public MoveAction GetMoveAction()
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
        if (CanSpendPointToTakeAnAction(baseAction))
        {
            SpendActionPoint(baseAction.GetActionPointCost());
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool CanSpendPointToTakeAnAction(BaseAction baseAction)
    {
        if (actionPoints >= baseAction.GetActionPointCost())
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
        OnAnyActionPointChange?.Invoke(this, EventArgs.Empty);
    }
    public int GetActionPoint()
    {
        return actionPoints;
    }
    private void OnEndTurn(object sender, EventArgs e)
    {
        if (IsEnemy() && !TurnSystem.Instance.IsPlayerTurn() || !IsEnemy() && TurnSystem.Instance.IsPlayerTurn())
        {
            actionPoints = ACTION_POINT_MAX;
            OnAnyActionPointChange?.Invoke(this, EventArgs.Empty);
        }
    }

    public bool IsEnemy()
    {
        return isEnemy;
    }
    public void Damage(int damageAmount)
    {
        healthSystem.Damage(damageAmount);
    }
    public Vector3 GetWorldPosition()
    {
        return transform.position;
    }
    private void OnDead(object sender, EventArgs e)
    {
        LevelGRid.Instance.RemoveUnitAtGridPosition(gridPosition,this);
        Destroy(gameObject);
        OnAnyUnitDead?.Invoke(this, EventArgs.Empty);
    }
}
