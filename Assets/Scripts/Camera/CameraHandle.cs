using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraHandle : MonoBehaviour
{
    private CinemachineVirtualCamera virtualCamera;

    private void OnEnable()
    {
        EventCenter.Instance.AddListener<IOperator>("ChangeOperator", OnChangeOperator);
        EventCenter.Instance.AddListener("PlayerDead", OnPlayerDead);
    }

    private void OnDisable()
    {
        EventCenter.Instance.RemoveListener<IOperator>("ChangeOperator", OnChangeOperator);
        EventCenter.Instance.RemoveListener("PlayerDead", OnPlayerDead);
    }

    private void Awake()
    {
        virtualCamera=GetComponent<CinemachineVirtualCamera>();
    }

    public void SetFollowTra(Transform targetTra)
    {
        virtualCamera.Follow=targetTra;
    }

    public void OnChangeOperator(IOperator _operator)
    {
        SetFollowTra(_operator.GetOperatorObject().transform);
    }

    public void OnPlayerDead()
    {
        SetFollowTra(null);
    }
}
