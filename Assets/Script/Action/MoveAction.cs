using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : BaseAction
{

    public event EventHandler OnStartMoving;
    public event EventHandler OnStopMoving;
    [SerializeField] private Animator animator;
    private Vector3 targetPosition;
    [SerializeField] private int maxMoveDistance;

    protected override void Awake()
    {
        base.Awake();
        targetPosition = transform.position;
    }
    public override string GetActionName()
    {
        return "Move";
    }

    void Update()
    {
        if (!isActive)
        {
            return;
        }
        Vector3 moveDir = (targetPosition - transform.position);
        float moveSpeed = 4f;
        if (Vector3.Distance(targetPosition, transform.position) >= 0.1f)
        {
            transform.position += moveDir * Time.deltaTime * moveSpeed;


        }
        else
        {
            OnStopMoving?.Invoke(this, EventArgs.Empty);
            ActionCompleted();
        }
        float rotateSpeed = 20f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }

    public override void TakeAction(GridPosition gridPosition, Action OnActionComplete)
    {

        this.targetPosition = LevelGrid.Instance.GetWorldPosition(gridPosition);
        OnStartMoving?.Invoke(this, EventArgs.Empty);
        ActionStart(OnActionComplete);

    }


    public override List<GridPosition> GetValidActionGridPosition()
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();
        GridPosition unitGridPosition = unit.GetGridPosition();
        for (int x = -maxMoveDistance; x <= maxMoveDistance; x++)
        {
            for (int z = -maxMoveDistance; z <= maxMoveDistance; z++)
            {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;
                if (!LevelGrid.Instance.IsVaildGridPosition(testGridPosition))
                {
                    continue;
                }
                if (unitGridPosition == testGridPosition)
                {
                    //Same Grid position where the unit is already at 
                    continue;
                }
                if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition))
                {
                    // Grid position already occupied with the another unit

                    continue;
                }
                validGridPositionList.Add(testGridPosition);

            }
        }

        return validGridPositionList;

    }

}
