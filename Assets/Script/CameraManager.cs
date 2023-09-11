using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;


public class CameraManager : MonoBehaviour
{
   [SerializeField] private GameObject actionCameraGameObject;

private void Start()
{
   BaseAction.OnAnyActionStarted += BaseAction_OnAnyActionStarted;
   BaseAction.OnAnyActionCompleted += BaseAction_OnAnyActionComplted;
   Hide();
}
private void BaseAction_OnAnyActionComplted(object sender , EventArgs e)
{
   switch(sender)
   {
      case ShootAction shootAction:
         Unit shooterUnit = shootAction.GetUnit();
         Unit targetUnit = shootAction.GetTargetUnit();
         Vector3 cameraCharacterHeight = Vector3.up * 1.75f;
         Vector3 shootDir = (targetUnit.GetWorldPosition() - shooterUnit.GetWorldPosition()).normalized;
         float shoulderoffsetAmount =0.5f;
         Vector3 sholderOffset = Quaternion.Euler(0,90,0)*shootDir*shoulderoffsetAmount;

         Vector3 actionCameraPosition = shooterUnit.GetWorldPosition() + cameraCharacterHeight +sholderOffset+(shootDir*-1);

         actionCameraGameObject.transform.position = actionCameraPosition;
         actionCameraGameObject.transform.LookAt(targetUnit.GetWorldPosition()+ cameraCharacterHeight);

         Hide();
         break;

   }
}
private void BaseAction_OnAnyActionStarted(object sender , EventArgs e)
{
   switch(sender)
   {
      case ShootAction shootAction:
         Show();
         break;
   
   }
}
   private void Show()
   {
       actionCameraGameObject.SetActive(true);

   }
   private void Hide()
   {
       actionCameraGameObject.SetActive(false);
   }
}
