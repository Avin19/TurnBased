using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : MonoBehaviour
{

    [SerializeField]private Animator animator;
    private Vector3 targetPosition;
    [SerializeField]private int maxMoveDistance ;
    private Unit unit;
    private void Awake() {
        unit = GetComponent<Unit>();
         targetPosition = transform.position;
    }
   
    void Update()
    {
         Vector3 moveDir = (targetPosition - transform.position);
        float moveSpeed = 4f;
        if (Vector3.Distance(targetPosition, transform.position) >= 0.1f)
        {         
            transform.position += moveDir * Time.deltaTime * moveSpeed;
            animator.SetBool("IsWalking", true);
            float rotateSpeed= 20f;
            transform.forward =Vector3.Slerp(transform.forward,moveDir,Time.deltaTime*rotateSpeed);
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }
    }

    public void Move(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
    }

    public bool IsVaildActionGridPosition(GridPosition gridPosition)
    {
        List<GridPosition> validGridpositionList = GetValidActionGridPosition();
        return validGridpositionList.Contains(gridPosition);
    }
    public List<GridPosition> GetValidActionGridPosition()
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();
        GridPosition unitGridPosition = unit.GetGridPosition();
        for(int x = - maxMoveDistance; x <= maxMoveDistance ; x++)
        {
            for(int z =- maxMoveDistance; z <= maxMoveDistance ; z++)
            {
                GridPosition offsetGridPosition = new GridPosition(x,z);
                GridPosition testGridPosition = unitGridPosition+offsetGridPosition;
                if(!LevelGRid.Instance.IsVaildGridPosition(testGridPosition))
                {
                    continue;
                }
                if(unitGridPosition == testGridPosition)
                {
                    //Same Grid position where the unit is already at 
                    continue;
                }
                if(LevelGRid.Instance.HasAnyUnitOnGridPosition(testGridPosition))
                {
                    // Grid position already occupied with the another unit
                 
                    continue;
                }
                validGridPositionList.Add(testGridPosition);
                Debug.Log(testGridPosition);
            }
        }

        return validGridPositionList;

    }
   
} 
