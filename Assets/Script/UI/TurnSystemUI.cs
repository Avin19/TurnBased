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
    }

    private void OnEndTurn(object sender, EventArgs e)
    {
        UpdateTurnNumber();
    }

    private void UpdateTurnNumber()
    {
        turnNumberText.text= " TURN "+ TurnSystem.Instance.GetTurnNumber();
    }

}
