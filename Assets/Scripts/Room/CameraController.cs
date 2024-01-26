using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private CinemachineVirtualCamera _cinemachine;

    private void Start()
    {
        _cinemachine = GetComponent<CinemachineVirtualCamera>();
    }

    public void SetFollowTarget(Transform followTarget)
    {
        _cinemachine.Follow = followTarget;
    }
}
