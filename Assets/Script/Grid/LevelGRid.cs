using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGRid : MonoBehaviour
{

    public static LevelGRid Instance { get; private set;}
    public event EventHandler OnAnyUnitMovedGridPosition;
   [SerializeField] private Transform gridDebugPerfab;

    private GridSystem gridSystem;

    private void Awake() {
        if(Instance !=null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        gridSystem = new GridSystem(10,10, 2f);
        gridSystem.CreateDebugObjects(gridDebugPerfab);
    }
    
    public void AddUnitAtGridPosition(GridPosition gridPosition , Unit unit)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        gridObject.AddUnit(unit);
    }
    public List<Unit> GetUnitGridPosition(GridPosition gridPosition)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        return gridObject.GetUnitList();

    }

    public void RemoveUnitAtGridPosition(GridPosition gridPosition, Unit unit)
    {
         GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        gridObject.RemoveUnit(unit);

    }

    public GridPosition GetGridPosition( Vector3 worldPosition) => gridSystem.GetGridPosition(worldPosition);
    public Vector3 GetWorldPosition(GridPosition gridPosition) => gridSystem.GetWorldPosition(gridPosition);
    public bool IsVaildGridPosition(GridPosition gridPosition) => gridSystem.IsVaildGridPosition(gridPosition);
    public int GetWidth() => gridSystem.GetWidth();

    public int GetHeight() => gridSystem.GetHeight();
    public void UnitMoveGridPosition(Unit unit,GridPosition fromGridPosition, GridPosition toGridPosition)
    {
       RemoveUnitAtGridPosition(fromGridPosition, unit);
        AddUnitAtGridPosition(toGridPosition,unit);

        OnAnyUnitMovedGridPosition?.Invoke(this, EventArgs.Empty);
    }

    public bool HasAnyUnitOnGridPosition(GridPosition gridPosition)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        return gridObject.HasAnyUnit();
    }

     public Unit GetUnitOnGridPosition(GridPosition gridPosition)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        return gridObject.GetUnit();
    }



    /*
    this function is same as the above function it is lambda expersion for the same 
        public GridPosition GetGridPosition( Vector3 worldPoistion)
        {
            return gridSystem.GetGridPosition(worldPosition )
        }



    */

}
