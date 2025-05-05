using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(UIBehaviour))]
public class CustomButton : MonoBehaviour
{
    public UIBehaviour uiComponent;

    private UnityAction<BaseEventData> pointerEnterCallBack;
    private UnityAction<BaseEventData> pointerClickCallBack;
    private UnityAction<BaseEventData> pointerExitCallBack;

    public UnityEvent pointerEnterEvent;
    public UnityEvent pointerClickEvent;
    public UnityEvent pointerExitEvent;

    private void Start()
    {
        pointerEnterCallBack = (value) => { pointerEnterEvent?.Invoke();};
        pointerClickCallBack = (value) => { pointerClickEvent?.Invoke(); };
        pointerExitCallBack = (value) => { pointerExitEvent?.Invoke(); };
        UIManager.AddCustomEventListener(uiComponent, EventTriggerType.PointerEnter, pointerEnterCallBack);
        UIManager.AddCustomEventListener(uiComponent, EventTriggerType.PointerClick, pointerEnterCallBack);
        UIManager.AddCustomEventListener(uiComponent, EventTriggerType.PointerExit, pointerEnterCallBack);
    }
}
