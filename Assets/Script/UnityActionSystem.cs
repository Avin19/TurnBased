
using UnityEngine;
using System;

public class UnityActionSystem : MonoBehaviour
{


    public static UnityActionSystem Instance { get; private set; }
    public event EventHandler OnUnitSelection;
    [SerializeField] private Unit selectedUnit;
    [SerializeField] private LayerMask unitSelectLayerMask;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    private void Update()
    {


        if (Input.GetMouseButtonDown(0))
        {

            if (HandleUnitSelection()) return;

            selectedUnit.GetMoveActionUnit().Move(MouseWorldPosition.Instance.GetMousePosition());

           // selectedUnit.GetComponent<MoveAction>().Move(MouseWorldPosition.Instance.GetMousePosition());
           // selectedUnit.Move(MouseWorldPosition.Instance.GetMousePosition());


        }
    }
    private bool HandleUnitSelection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, unitSelectLayerMask))
            {
                if (raycastHit.transform.TryGetComponent<Unit>(out Unit unit))
                {
                    SelectedUnit(unit);

                    return true;
                }


            }
        }
        return false;
    }

    private void SelectedUnit(Unit unit)
    {
        selectedUnit = unit;
        OnUnitSelection?.Invoke(this, EventArgs.Empty);
    }
    public Unit GetSelectedUnit()
    {
        return selectedUnit;
    }
}
