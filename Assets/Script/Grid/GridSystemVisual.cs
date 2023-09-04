using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class GridSystemVisual : MonoBehaviour
{

    public static GridSystemVisual Instance { get; private set;}
    [SerializeField] private Transform pfGridSystemViusalSingleTransform;
    private GridSystemVisualSingle[,] gridSystemVisualSinglesArray;

    void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance =this;
    }
    void Start()
    {
        gridSystemVisualSinglesArray = new GridSystemVisualSingle[
            LevelGRid.Instance.GetWidth(),
            LevelGRid.Instance.GetHeight()
        ];
        for (int x = 0; x< LevelGRid.Instance.GetWidth(); x++)
        {
            for( int z=0; z< LevelGRid.Instance.GetHeight(); z++)
            {
                GridPosition gridPosition = new GridPosition(x,z);
                Transform gridSystemVisualSingleTransform =Instantiate(pfGridSystemViusalSingleTransform, LevelGRid.Instance.GetWorldPosition(gridPosition),Quaternion.identity);
                gridSystemVisualSinglesArray[x,z] = gridSystemVisualSingleTransform.GetComponent<GridSystemVisualSingle>();
            }
        }
        
    }
    void Update()
    {
        UpdateVisual();
    }

    public void HideAllGridPosition()
    {
        foreach(GridSystemVisualSingle gridSystemVisualSingle in gridSystemVisualSinglesArray)
        {
            gridSystemVisualSingle.Hide();
        }
    }
    public void ShowGridPositionList( List<GridPosition> gridPositionsList)
    {
        foreach (GridPosition gridPosition in gridPositionsList)
        {
            gridSystemVisualSinglesArray[gridPosition.x,gridPosition.z].Show();
        }
    }
    public void UpdateVisual()
    {
        HideAllGridPosition();
        BaseAction selectedAction = UnityActionSystem.Instance.GetSelectedAction();
        ShowGridPositionList(selectedAction.GetValidActionGridPosition());

    }
}
