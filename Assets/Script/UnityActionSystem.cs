
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class UnityActionSystem : MonoBehaviour
{


    public static UnityActionSystem Instance { get; private set; }
    public event EventHandler OnUnitSelection;
    public event EventHandler OnSelcetedActionChange;

    public event EventHandler<bool> OnUnityBusy;

    public event EventHandler onActionChanged;
    [SerializeField] private Unit selectedUnit;
    [SerializeField] private LayerMask unitSelectLayerMask;
    private BaseAction selectedAction;
    private bool isBusy;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    private void Start()
    {
        SelectedUnit(selectedUnit);
    }
    private void Update()
    {
        if (isBusy)
        {
            return;
        }


        if (Input.GetMouseButtonDown(0))
        {

            if (HandleUnitSelection()) return;

            // if (selectedUnit.GetMoveActionUnit().IsVaildActionGridPosition(mouseGridPosition))
            // {
            //     SetBusy();
            //     selectedUnit.GetMoveActionUnit().Move(mouseGridPosition , ClearBusy);
            // }
            //selectedUnit.GetMoveActionUnit().Move(MouseWorldPosition.Instance.GetMousePosition());

            // selectedUnit.GetComponent<MoveAction>().Move(MouseWorldPosition.Instance.GetMousePosition());
            // selectedUnit.Move(MouseWorldPosition.Instance.GetMousePosition());
        }
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (!TurnSystem.Instance.IsPlayerTurn())
        {
            return;
        }

        HandleSelectedAction();


        if (Input.GetMouseButtonDown(1))
        {
            // SetBusy();
            // selectedUnit.GetSpinActionUnit().Spin(ClearBusy);
        }
    }
    private void HandleSelectedAction()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GridPosition mouseGridPosition = LevelGRid.Instance.GetGridPosition(MouseWorldPosition.Instance.GetMousePosition());
            if (selectedAction.IsVaildActionGridPosition(mouseGridPosition))
            {
                if (selectedUnit.TrySpendPointToTakeAnAction(selectedAction))
                {
                    SetBusy();
                    selectedAction.TakeAction(mouseGridPosition, ClearBusy);
                    onActionChanged?.Invoke(this, EventArgs.Empty);
                }

            }

        }
    }
    private void SetBusy()
    {
        isBusy = true;
        OnUnityBusy?.Invoke(this, isBusy);
    }
    private void ClearBusy()
    {
        isBusy = false;
        OnUnityBusy?.Invoke(this, isBusy);

    }
    private bool HandleUnitSelection()
    {
        Debug.Log("here");

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, unitSelectLayerMask))
        {
            if (raycastHit.transform.TryGetComponent<Unit>(out Unit unit))
            {
                if (unit == selectedUnit)
                {
                    // then this unit is already selected 
                    return false;
                }

                if (unit.IsEnemy())
                {

                    //Clicked on the Emeny
                    return false;
                }
                SelectedUnit(unit);
                return true;
            }


        }

        return false;
    }

    private void SelectedUnit(Unit unit)
    {
        selectedUnit = unit;
        SetSelectedAction(unit.GetMoveActionUnit());
        OnUnitSelection?.Invoke(this, EventArgs.Empty);
    }
    public void SetSelectedAction(BaseAction baseAction)
    {
        selectedAction = baseAction;
        OnSelcetedActionChange?.Invoke(this, EventArgs.Empty);
    }
    public Unit GetSelectedUnit()
    {
        return selectedUnit;
    }

    public BaseAction GetSelectedAction()
    {
        return selectedAction;
    }
}
