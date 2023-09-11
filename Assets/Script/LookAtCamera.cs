using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
   private Transform cameraTransform;
   [SerializeField] private bool invert;

   private void Awake()
   {
    cameraTransform = Camera.main.transform;
   }

   private void LateUpdate()
   {
    if(invert)
    {
        Vector3 dir = (cameraTransform.position - transform.position).normalized;
        transform.LookAt(transform.position+dir*-1);
    }
    else{
        transform.LookAt(cameraTransform);
    }
    
   }
}
