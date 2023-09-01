
using UnityEngine;

public class MouseWorldPosition : MonoBehaviour
{

    public static MouseWorldPosition Instance { get; private set; }
    [SerializeField] private LayerMask mousePointerLayerMask;
    private void Awake()
    {
        Instance = this;
    }
    public Vector3 GetMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, mousePointerLayerMask);
        return raycastHit.point;
    }

}
