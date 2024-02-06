using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpinAction : BaseAction
{
 
   
    private float totalSpinAmount;

    private void Update()
    {
        if (!isActive )
        {
           return;
        }
        float spinAmount = 360 * Time.deltaTime;
        transform.eulerAngles += new Vector3(0, spinAmount, 0);
        totalSpinAmount += spinAmount;
        if(totalSpinAmount >= 360f)
        {
            ActionCompleted();
        }
    }
    public override string GetActionName()
    {
        return "Spin";
    }
    public override void TakeAction(GridPosition gridPosition ,Action onSpinComplete)
    {
       
        totalSpinAmount = 0f;
         ActionStart(OnActionComplete);
    }
     public override List<GridPosition> GetValidActionGridPosition()
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();
        GridPosition unitGridPosition = unit.GetGridPosition();

        return new List<GridPosition>{
            unitGridPosition
        };
    }
}
