using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UnitActionSystemUI : MonoBehaviour
{
    [SerializeField] private Transform pfActionSystemUIButton;
    [SerializeField] private Transform pfActionSystemContainer;
    private List<ActionButtonUI> actionButtonUIList;
    
    private void Awake()
    {
        actionButtonUIList = new List<ActionButtonUI>();
    }
    // Start is called before the first frame update
    void Start()
    {
        UnityActionSystem.Instance.OnUnitSelection += OnSelectedUnitChanged;
        CreateUnitActionButton();
    }

    private void OnSelectedUnitChanged(object sender, EventArgs e)
    {
        CreateUnitActionButton();
        UpdateSelectedVisual();
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
        
    }
}
