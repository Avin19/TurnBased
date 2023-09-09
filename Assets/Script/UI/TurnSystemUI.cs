using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurnSystemUI : MonoBehaviour
{
   
    private Button endTurnBtn;
    private TextMeshProUGUI turnNumberText;
    [SerializeField] private GameObject enemyTurnVisual;


    private void Awake()
    {
        endTurnBtn = transform.Find("EndTurnButton").GetComponent<Button>();
        turnNumberText = transform.Find("NextTurnText").GetComponent<TextMeshProUGUI>();
    }
    private void Start()
    {
        UpdateTurnNumber();

        endTurnBtn.onClick.AddListener(()=>{
            TurnSystem.Instance.NextTurn();
            
        });
        TurnSystem.Instance.OnEndTurn += OnEndTurn;
        UpdateEnemyTurnVisual();
        UpdateEnemyTurnBtn();
    }

    private void OnEndTurn(object sender, EventArgs e)
    {
        
        UpdateTurnNumber();
        UpdateEnemyTurnVisual();
        UpdateEnemyTurnBtn();
    }

    private void UpdateTurnNumber()
    {
        turnNumberText.text= " TURN "+ TurnSystem.Instance.GetTurnNumber();
    }
    private void UpdateEnemyTurnVisual()
    {
        enemyTurnVisual.SetActive(!TurnSystem.Instance.IsPlayerTurn());    
    }
    private void UpdateEnemyTurnBtn()
    {
        endTurnBtn.gameObject.SetActive(TurnSystem.Instance.IsPlayerTurn());
    }

}
