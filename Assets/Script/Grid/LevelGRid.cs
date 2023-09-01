using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGRid : MonoBehaviour
{

    public static LevelGRid Instance { get; private set;}
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
    public bool IsVaildGridPosition(GridPosition gridPosition) => gridSystem.IsVaildGridPosition(gridPosition);

    public void UnitMoveGridPosition(Unit unit,GridPosition fromGridPosition, GridPosition toGridPosition)
    {
       RemoveUnitAtGridPosition(fromGridPosition, unit);
        AddUnitAtGridPosition(toGridPosition,unit);
    }

    public bool HasAnyUnitOnGridPosition(GridPosition gridPosition)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        return gridObject.HasAnyUnit();
    }
    /*
    this function is same as the above function it is lambda expersion for the same 
        public GridPosition GetGridPosition( Vector3 worldPoistion)
        {
            return gridSystem.GetGridPosition(worldPosition )
        }



    */
}
