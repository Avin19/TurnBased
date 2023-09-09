using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitRagDollSpawner : MonoBehaviour
{
   [SerializeField] private Transform pfUnitRagDollTransform;

   private HealthSystem healthSystem;


   private void Start()
   {
    healthSystem = GetComponent<HealthSystem>();
    healthSystem.OnDead += OnDead;

   }

   private void OnDead(object sender, EventArgs e)
   {
    Instantiate(pfUnitRagDollTransform,transform.position, transform.rotation);
   }
}
