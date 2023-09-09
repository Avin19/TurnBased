using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class ShootAction : BaseAction
{
    private enum State
    {
        Aiming,
        Shooting,
        Cooloff,
    }
    private State state;
    public event EventHandler<OnShootingEventArgs> OnShooting;// can pass normal target unit using "" public event EventHandler<Unit> OnShooting; but using class to pass multiple arugments

    public class OnShootingEventArgs: EventArgs
    {
        public Unit targetUnit;
        public Unit shootingUnit;
    }
    private int maxShootDistance = 7;
    private float stateTimer;
    private Unit targetUnit;
    private bool canShootBullet;


    private void Update()
    {
        if (!isActive)
        {
            return;
        }
        stateTimer -= Time.deltaTime;


        switch (state)
        {
            case State.Aiming:

                Vector3 lookdir = (targetUnit.GetWorldPosition() - unit.GetWorldPosition()).normalized;
                float rotateSpeed = 20f;
                transform.forward = Vector3.Slerp(transform.forward, lookdir, Time.deltaTime * rotateSpeed);
                break;
            case State.Shooting:
                if (canShootBullet)
                {
                    Shoot();
                    canShootBullet = false;
                }

                break;
            case State.Cooloff:

                break;

        }
        if (stateTimer <= 0f)
        {
            NextStage();
        }

    }
    private void NextStage()
    {
        switch (state)
        {
            case State.Aiming:
                state = State.Shooting;
                float shootingState = 0.1f;
                stateTimer = shootingState;
                break;

            case State.Shooting:
                state = State.Cooloff;
                float coolState = 0.5f;
                stateTimer = coolState;
                break;
            case State.Cooloff:
                ActionCompleted();
                break;
        }
    }
    public override string GetActionName()
    {
        return "Shoot";
    }

    public override List<GridPosition> GetValidActionGridPosition()
    {

        List<GridPosition> validGridPositionList = new List<GridPosition>();
        GridPosition unitGridPosition = unit.GetGridPosition();
        for (int x = -maxShootDistance; x <= maxShootDistance; x++)
        {
            for (int z = -maxShootDistance; z <= maxShootDistance; z++)
            {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;
                if (!LevelGRid.Instance.IsVaildGridPosition(testGridPosition))
                {
                    continue;
                }
                int testArea = Mathf.Abs(x) + Mathf.Abs(z);
                if (testArea > maxShootDistance)
                {
                    continue;
                }
                if (unitGridPosition == testGridPosition)
                {
                    //Same Grid position where the unit is already at 
                    continue;
                }
                if (!LevelGRid.Instance.HasAnyUnitOnGridPosition(testGridPosition))
                {
                    // Grid position is empty;


                    continue;
                }
                Unit tragetUnit = LevelGRid.Instance.GetUnitOnGridPosition(testGridPosition);
                if (tragetUnit.IsEnemy() == unit.IsEnemy())
                {
                    //Both unit are on same team
                    continue;
                }
                validGridPositionList.Add(testGridPosition);

            }
        }

        return validGridPositionList;
    }
    private void Shoot()
    {
        OnShooting?.Invoke(this, new OnShootingEventArgs {
            targetUnit = targetUnit,
            shootingUnit = unit
        });
        targetUnit.Damage(40);
    }


    public override void TakeAction(GridPosition gridPosition, Action OnActionComplete)
    {
        ActionStart(OnActionComplete);

        targetUnit = LevelGRid.Instance.GetUnitOnGridPosition(gridPosition);
        state = State.Aiming;
        float aimingStateTime = 1f;
        stateTimer = aimingStateTime;

        canShootBullet = true;

    }
}
