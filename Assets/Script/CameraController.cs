
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{

    private const float MIN_OFFSET_VALUE = 2f;
    private const float MAX_OFFSET_VALUE = 12f;
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    private Vector3 targetFollowOffset;
    private CinemachineTransposer cinemachineTransposer;
    private void Start()
    {
        cinemachineTransposer = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        targetFollowOffset = cinemachineTransposer.m_FollowOffset;
    }
    private void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleZoom();





    }
    private void HandleMovement()
    {
        Vector3 moveDir = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.W))
        {
            moveDir.z = +1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveDir.z = -1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveDir.x = -1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveDir.x = +1f;
        }
        float moveSpeed = 10f;

        Vector3 moveVector = transform.forward * moveDir.z + transform.right * moveDir.x;
        transform.position += moveVector * moveSpeed * Time.deltaTime;

    }
    private void HandleRotation()
    {
        float rotationSpeed = 60f;
        Vector3 rotationVector = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.Q))
        {
            rotationVector.y = +1f;
        }
        if (Input.GetKey(KeyCode.E))
        {
            rotationVector.y = -1f;
        }

        transform.eulerAngles += rotationVector * rotationSpeed * Time.deltaTime;

    }
    private void HandleZoom()
    {
        if (Input.mouseScrollDelta.y > 0)
        {
            targetFollowOffset.y -= 1f;
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            targetFollowOffset.y += 1f;
        }
        targetFollowOffset.y = Mathf.Clamp(targetFollowOffset.y, MIN_OFFSET_VALUE, MAX_OFFSET_VALUE);
        float zoomSpeed = 5f;
        cinemachineTransposer.m_FollowOffset = Vector3.Lerp(cinemachineTransposer.m_FollowOffset, targetFollowOffset, Time.deltaTime * zoomSpeed);

    }
}
