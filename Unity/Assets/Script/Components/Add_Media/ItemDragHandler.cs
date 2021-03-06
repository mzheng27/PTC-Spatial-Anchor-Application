﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour, IDragHandler, IEndDragHandler 
{
    private MenuIcon icon;
    
    void Awake()
    {
        icon = GetComponent<MenuIcon>();
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //dragDropPanel.SetActive(false);
        transform.localPosition = Vector3.zero;
        icon.OnDrop();
    }
}
