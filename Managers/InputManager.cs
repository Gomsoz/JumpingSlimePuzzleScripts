using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager
{
    public Action<RaycastHit2D> BlockSelect = null;

    public void InputUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            /*Ray mousePos = Camera.main.ScreenPointToRay(Input.mousePosition); //Ray를 이용하여 마우스 상호작용
                RaycastHit2D hit2D;
                hit2D = Physics2D.GetRayIntersection(mousePos, Mathf.Infinity);
                */

            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //Vector2를 이용하여 마우스 상호작용        
            RaycastHit2D hit2D;
            hit2D = Physics2D.Raycast(mousePos, Vector2.zero, 0f);

            if (hit2D.collider != null && hit2D.collider.CompareTag("Block"))
                BlockSelect.Invoke(hit2D);
        }
            
    }

    public void Init()
    {
    }
}
