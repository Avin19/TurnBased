using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class BaseAction : MonoBehaviour
{
   public static event EventHandler OnAnyActionStarted;
   public static event EventHandler OnAnyActionCompleted;
   protected Unit unit;
   protected bool isActive;
   protected Action OnActionComplete;

   protected virtual void Awake()
   {
      unit = GetComponent<Unit>();
   }

   public abstract string GetActionName();

   public abstract void TakeAction(GridPosition gridPosition, Action OnActionComplete);

   public virtual bool IsVaildActionGridPosition(GridPosition gridPosition)
   {
      List<GridPosition> validGridpositionList = GetValidActionGridPosition();
      return validGridpositionList.Contains(gridPosition);
   }
   public abstract List<GridPosition> GetValidActionGridPosition();

   public virtual int GetActionPointCost()
   {
      return 1;
   }
   protected void ActionStart(Action onActionComplete)
   {
      isActive = true;
      this.OnActionComplete = onActionComplete;
      OnAnyActionStarted?.Invoke(this, EventArgs.Empty);
   }
   protected void ActionCompleted()
   {
      isActive = false;
      OnActionComplete();
      OnAnyActionCompleted?.Invoke(this, EventArgs.Empty);
   }
   public Unit GetUnit()
   {
      return unit;
   }

}
