using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ActionButtonUI : MonoBehaviour
{
    private TextMeshProUGUI textMeshPro;
    private Button button;
    private Transform selectedVisual;

    void Awake()
    {
        textMeshPro = transform.Find("Text").GetComponent<TextMeshProUGUI>();
        button = GetComponent<Button>();
        selectedVisual = transform.Find("Selected").GetComponent<Transform>();
    }

    public void SetBaseAction(BaseAction baseAction)
    {
        textMeshPro.text =  baseAction.GetActionName().ToUpper();
        button .onClick.AddListener(()=>{
            UnityActionSystem.Instance.SetSelectedAction(baseAction);
            selectedVisual.gameObject.SetActive(true);

        });

    }


   
}
