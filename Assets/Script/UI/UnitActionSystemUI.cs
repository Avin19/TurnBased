using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class UnitActionSystemUI : MonoBehaviour
{
    [SerializeField] private Transform pfActionSystemUIButton;
    [SerializeField] private Transform pfActionSystemContainer;
    [SerializeField] private TextMeshProUGUI actionPointText;
    private List<ActionButtonUI> actionButtonUIList;
    
    private void Awake()
    {
        actionButtonUIList = new List<ActionButtonUI>();
    }
    // Start is called before the first frame update
    void Start()
    {
        UnityActionSystem.Instance.OnUnitSelection += OnSelectedUnitChanged;
         UnityActionSystem.Instance.OnSelcetedActionChange += OnSelcetedActionChange;
         UnityActionSystem.Instance.onActionChanged += OnActionChanged;
        CreateUnitActionButton();
        UpdateSelectedVisual();
         UpdateActionPoint();
       

    }

    private void OnActionChanged(object sender, EventArgs e)
    {
        UpdateActionPoint();
    }

    private void OnSelcetedActionChange(object sender, EventArgs e)
    {
        UpdateSelectedVisual();
         UpdateActionPoint();
    }

    private void OnSelectedUnitChanged(object sender, EventArgs e)
    {
        CreateUnitActionButton();
        UpdateSelectedVisual();
        UpdateActionPoint();
    }

    private void CreateUnitActionButton()
    {

        foreach(Transform buttonTransform in pfActionSystemContainer)
        {
            Destroy(buttonTransform.gameObject);
        }
        actionButtonUIList.Clear();
        Unit selectedUnit = UnityActionSystem.Instance.GetSelectedUnit();

        foreach(BaseAction baseAction in selectedUnit.GetBaseActions())
        {
            Transform actionButtonTransfom =Instantiate(pfActionSystemUIButton, pfActionSystemContainer);
            ActionButtonUI actionButtonUI = actionButtonTransfom.GetComponent<ActionButtonUI>();
             actionButtonUIList.Add(actionButtonUI);
            actionButtonTransfom.Find("Selected").gameObject.SetActive(false);
            actionButtonUI.SetBaseAction(baseAction);
           
        }
    }
    private void UpdateSelectedVisual()
    {
        foreach(ActionButtonUI actionButtonUI in actionButtonUIList)
        {
            actionButtonUI.transform.Find("Selected").gameObject.SetActive(false);
        }
        
    }

    private void UpdateActionPoint()
    {
        Unit selectedunit = UnityActionSystem.Instance.GetSelectedUnit();
        actionPointText.text = " Action Point : "+ selectedunit.GetActionPoint();
    }
}
