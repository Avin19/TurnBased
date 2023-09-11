using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class GridSystemVisual : MonoBehaviour
{

    [Serializable]
    public struct GridVisualTypeMaterials
    {
        public GridVisualType gridVisualType;
        public Material material;
    }
    public enum GridVisualType
    {
        White,
        Blue,
        Red,
        RedSoft,
        Yellow,
    }
    public static GridSystemVisual Instance { get; private set; }
    [SerializeField] private Transform pfGridSystemViusalSingleTransform;
    private GridSystemVisualSingle[,] gridSystemVisualSinglesArray;
    [SerializeField] private List<GridVisualTypeMaterials> gridVisualTypeMaterialsList;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    void Start()
    {
        gridSystemVisualSinglesArray = new GridSystemVisualSingle[
            LevelGRid.Instance.GetWidth(),
            LevelGRid.Instance.GetHeight()
        ];
        for (int x = 0; x < LevelGRid.Instance.GetWidth(); x++)
        {
            for (int z = 0; z < LevelGRid.Instance.GetHeight(); z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);
                Transform gridSystemVisualSingleTransform = Instantiate(pfGridSystemViusalSingleTransform, LevelGRid.Instance.GetWorldPosition(gridPosition), Quaternion.identity);
                gridSystemVisualSinglesArray[x, z] = gridSystemVisualSingleTransform.GetComponent<GridSystemVisualSingle>();
            }
        }
        UnityActionSystem.Instance.OnSelcetedActionChange += UnityActionSystem_OnSelectedActionChange;
        LevelGRid.Instance.OnAnyUnitMovedGridPosition += LevelGRid_OnAnyUnitMovedGridPosition;
        UpdateVisual();
    }

    private void LevelGRid_OnAnyUnitMovedGridPosition(object sender, EventArgs e)
    {
        UpdateVisual();
    }

    private void UnityActionSystem_OnSelectedActionChange(object sender, EventArgs e)
    {
        UpdateVisual();
    }
    public void HideAllGridPosition()
    {
        foreach (GridSystemVisualSingle gridSystemVisualSingle in gridSystemVisualSinglesArray)
        {
            gridSystemVisualSingle.Hide();
        }
    }

    private void ShowGridPositionRange(GridPosition gridPosition, int range, GridVisualType gridViusalType)
    {
        List<GridPosition> gridPositionList = new List<GridPosition>();
        for (int x = -range; x <= range; x++)
        {
            for (int z = -range; z <= range; z++)
            {
                GridPosition testGridPosition = gridPosition + new GridPosition(x,z);
                if(!LevelGRid.Instance.IsVaildGridPosition(testGridPosition))
                {
                    continue;
                }
                int testDistance = Mathf.Abs(x) + Mathf.Abs(z);
                if (testDistance > range)
                {
                    continue;
                }
                gridPositionList.Add(testGridPosition);
            }
        }
        ShowGridPositionList(gridPositionList, gridViusalType);
    }
    public void ShowGridPositionList(List<GridPosition> gridPositionsList, GridVisualType gridVisualType)
    {
        foreach (GridPosition gridPosition in gridPositionsList)
        {
            gridSystemVisualSinglesArray[gridPosition.x, gridPosition.z].Show(GetGridVisualTypeMaterial(gridVisualType));
        }
    }
    public void UpdateVisual()
    {
        HideAllGridPosition();

        Unit selectedUnit = UnityActionSystem.Instance.GetSelectedUnit();
        BaseAction selectedAction = UnityActionSystem.Instance.GetSelectedAction();
        GridVisualType gridVisualType;
        switch (selectedAction)
        {
            default:
            case MoveAction moveAction:
                gridVisualType = GridVisualType.White;
                break;
            case SpinAction spinAction:
                gridVisualType = GridVisualType.Blue;
                break;
            case ShootAction shootAction:
                gridVisualType = GridVisualType.Red;

                ShowGridPositionRange(selectedUnit.GetGridPosition(), shootAction.GetShootingRange(), GridVisualType.RedSoft);
                break;

        }

        ShowGridPositionList(selectedAction.GetValidActionGridPosition(), gridVisualType);

    }
    private Material GetGridVisualTypeMaterial(GridVisualType gridVisualType)
    {
        foreach (GridVisualTypeMaterials gridVisualTypeMaterial in gridVisualTypeMaterialsList)
        {
            if (gridVisualTypeMaterial.gridVisualType == gridVisualType)
            {
                return gridVisualTypeMaterial.material;
            }
        }
        Debug.LogError("could not find GridVisualTypeMaterial");
        return null;
    }
}
