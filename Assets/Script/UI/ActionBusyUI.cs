using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionBusyUI : MonoBehaviour
{
   void Start()
   {
        UnityActionSystem.Instance.OnUnityBusy += OnBusy;
        gameObject.SetActive(false);
   }

    private void OnBusy(object sender, bool isbusy)
    {
        gameObject.SetActive(isbusy);
    
    }

    

}
