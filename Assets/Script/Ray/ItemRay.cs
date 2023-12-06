using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

[Flags]
public enum RAYFLAGS
{
    STATUS = 1 << 8,
    INVENTORY = 1 << 9,

}

public class ItemRay : MonoBehaviour
{
    [SerializeField]
    GraphicRaycaster graphicRay;
    PointerEventData pEventData = new PointerEventData(null);
    List<RaycastResult> rayResult = new List<RaycastResult>();

    RAYFLAGS mask;

    [SerializeField]
    GameObject commentBox;
    [SerializeField]
    TextMeshProUGUI comment;
    public RAYFLAGS InvenMask;
    public RAYFLAGS statusMask;


    // Start is called before the first frame update
    void Start()
    {
        if (graphicRay == null)
            graphicRay = GameObject.Find("DungeonUI").GetComponent<GraphicRaycaster>();

        InvenMask |= RAYFLAGS.INVENTORY;
        statusMask |= RAYFLAGS.STATUS;

    }


    private void Update()
    {
        commentBox.SetActive(false);

        RAYFLAGS flag = InvenMask |= statusMask;
        SearchItemToMask(flag);
    }

    public void SearchItemToMask(RAYFLAGS mask)
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current);
        pointerData.position = Input.mousePosition;

        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, raycastResults);


        foreach (RaycastResult result in raycastResults)
        {
            if( ((RAYFLAGS)result.gameObject.layer ^ mask) != 0)
            {

                if (Input.GetMouseButtonDown(0))
                {
                    //if (result.gameObject.TryGetComponent<IClickUseAble>(out IClickUseAble slot))
                    //    slot.OnClickUse();
                }
                
            }


            //if (result.gameObject.TryGetComponent<IExplainAble>(out IExplainAble comment))
            //{
                
            //    Vector3 mousePosition = Input.mousePosition;
            //    mousePosition.z = 10; // 이 값은 카메라와 오브젝트 사이의 거리에 따라 조정해야 합니다.
            //    commentBox.transform.position = mousePosition;

            //    if(comment.Comment != null) 
            //    {
            //        commentBox.SetActive(true);
            //        this.comment.text = comment.Comment;
            //    }
            //}


        }
    }
    
    //void ViewComment()
    //{

    //    PointerEventData pointerData = new PointerEventData(EventSystem.current);
    //    pointerData.position = Input.mousePosition;
    //    List<RaycastResult> raycastResults = new List<RaycastResult>();
    //    EventSystem.current.RaycastAll(pointerData, raycastResults);

    //    foreach (RaycastResult result in raycastResults)
    //    {
    //        if (result.gameObject.TryGetComponent<IExplainAble>(out IExplainAble comment))
    //            gameObject.transform.position = Input.mousePosition;
            
    //    }


    //}



    public void GetItem()
    {
        rayResult.Clear();
        pEventData.position = Input.mousePosition;
        graphicRay.Raycast(pEventData, rayResult);



    }

    


}
