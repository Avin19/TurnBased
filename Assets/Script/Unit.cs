using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private Vector3 targetPosition;
    private GridPosition gridPosition;
    private MoveAction moveAction;
    private SpinAction spinAction;
    private BaseAction[] baseActionArray;
    private void Awake() {
        moveAction  = GetComponent<MoveAction>();
        spinAction = GetComponent<SpinAction>();
        baseActionArray = GetComponents<BaseAction>();

    }
    
    private void Start() {
        gridPosition= LevelGRid.Instance.GetGridPosition(transform.position);
        LevelGRid.Instance.AddUnitAtGridPosition(gridPosition,this);
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
}
