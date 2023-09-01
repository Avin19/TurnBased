using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GridDebugObject : MonoBehaviour
{
  private GridObject gridObject;
  [SerializeField] private TextMeshPro gridText;

  public void SetGridObject(GridObject gridObject)
  {
    this.gridObject = gridObject;
    //gridText.SetText()
  }
  private void Update() {
    gridText.SetText(gridObject.ToString());
  }
}
