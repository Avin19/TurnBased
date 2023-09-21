using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
   private List<Unit> unitList;
   private List<Unit> friendlyUnitList;
   private List<Unit> enemyUnitList;
   private void Awake()
   {
      unitList = new List<Unit>();
      friendlyUnitList = new List<Unit>();
      enemyUnitList = new List<Unit>();
   }
   private void Start()
   {
      Unit.onAnyUitSpawned += Unit_OnAnyUnitSpawned;
      Unit.onAnyUnitDead += Unit_OnAnyUnitDead;
   }

    private void Unit_OnAnyUnitDead(object sender, EventArgs e)
    {
        
    }

    private void Unit_OnAnyUnitSpawned(object sender, EventArgs e)
    {
        
    }
}
