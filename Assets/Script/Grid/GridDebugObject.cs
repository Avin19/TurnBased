using UnityEngine;
using TMPro;
/*

This Script is connected to perfab of Cell to display the cell grid position


*/

public class GridDebugObject : MonoBehaviour
{
  private GridObject gridObject;
  [SerializeField] private TextMeshPro gridText;

  public void SetGridObject(GridObject gridObject)
  {
    this.gridObject = gridObject;

  }
  private void Update()
  {
    gridText.SetText(gridObject.ToString());
  }
}
