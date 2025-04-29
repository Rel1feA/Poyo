using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraHandle : MonoBehaviour
{
    //private CinemachineVirtualCamera virtualCamera;

    //private void OnEnable()
    //{
    //    EventCenter.Instance.AddListener<IOperator>("ChangeOperator", OnChangeOperator);
    //    EventCenter.Instance.AddListener("PlayerDead", OnPlayerDead);
    //}

    //private void OnDisable()
    //{
    //    EventCenter.Instance.RemoveListener<IOperator>("ChangeOperator", OnChangeOperator);
    //    EventCenter.Instance.RemoveListener("PlayerDead", OnPlayerDead);
    //}

    //private void Awake()
    //{
    //    virtualCamera=GetComponent<CinemachineVirtualCamera>();
    //}

    //public void SetFollowTra(Transform targetTra)
    //{
    //    virtualCamera.Follow=targetTra;
    //}

    //public void OnChangeOperator(IOperator _operator)
    //{
    //    SetFollowTra(_operator.GetOperatorObject().transform);
    //}

    //public void OnPlayerDead()
    //{
    //    SetFollowTra(null);
    //}

    //上述代码为摄影机跟随角色时，在玩家切换操控主体是摄影机会跟随对应主体的逻辑，目前不需要先注释

    public int id;
    private CinemachineVirtualCamera _virtualCamera;

    private void Awake()
    {
        _virtualCamera=GetComponent<CinemachineVirtualCamera>();
    }

    private void OnEnable()
    {
        EventCenter.Instance.AddListener<RoomData>("TransferRoom", OnTransferRoom);
    }

    private void OnDisable()
    {
        EventCenter.Instance.RemoveListener<RoomData>("TransferRoom", OnTransferRoom);
    }

    public void OnTransferRoom(RoomData roomData)
    {
        if(roomData.roomCameraID==id)
        {
            _virtualCamera.Priority = 20;
        }
        else
        {
            _virtualCamera.Priority = 10;
        }
    }
}
