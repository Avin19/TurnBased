using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitWorldUi : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI actionPointText;

    [SerializeField] private Unit unit;
    [SerializeField] private Image healthBar;
    [SerializeField] private HealthSystem healthSystem;

    private void Start()
    {
        Unit.OnAnyActionPointChange += OnAnyActionPointChange;
        healthSystem.OnDamaged += HealthSystem_OnDamage;
        UpdateActionPointText();
        UpdateHealthBar();
    }

    private void HealthSystem_OnDamage(object sender, EventArgs e)
    {
        UpdateHealthBar();
    }

    private void OnAnyActionPointChange(object sender, EventArgs e)
    {
        UpdateActionPointText();
    }

    private void UpdateActionPointText()
    {
        actionPointText.text = unit.GetActionPoint().ToString();
    }

    private void UpdateHealthBar()
    {
        healthBar.fillAmount= healthSystem.GetHealthAmount();
    }
}
