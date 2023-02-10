using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class OrderUpdate : MonoBehaviour,IEndDragHandler,IPointerEnterHandler
{
    
    private RectTransform _rectTransformBeingDragged;
    [SerializeField] private RectTransform _rectTransformA;
    [SerializeField] public static int originalIndex;
    [SerializeField] private GridLayoutGroup  gridLayout;
    private static int _itemIndex;
    private RectTransform rectTransform;
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        int siblingIndex = rectTransform.GetSiblingIndex();
        if(siblingIndex!=originalIndex)
        {
          _itemIndex=siblingIndex;
          _rectTransformA=rectTransform; 
        }
        Debug.Log("Sibling index of rectTransform: " + siblingIndex);
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _rectTransformBeingDragged = GetRectTransformUnderMouse();
        }

        if (Input.GetMouseButtonUp(0))
        {
            _rectTransformBeingDragged = null;
     
        }

        if (_rectTransformBeingDragged != null)
        {
            _rectTransformBeingDragged.position = Input.mousePosition;
            originalIndex = _rectTransformBeingDragged.GetSiblingIndex();

            if (_rectTransformA == null)
            {
                _rectTransformA = Get_rectTransformA();
            }

            if (_rectTransformA != null)
            {
                bool isOverlapping = RectTransformUtility.RectangleContainsScreenPoint(_rectTransformA, _rectTransformBeingDragged.position, null);
                if (isOverlapping)
                {
                    Debug.Log(_rectTransformA.GetSiblingIndex());
                    int siblingIndex = _rectTransformA.GetSiblingIndex();
                    _itemIndex=siblingIndex;
                    _rectTransformBeingDragged.SetSiblingIndex(siblingIndex);
                    _rectTransformA.SetSiblingIndex(originalIndex);    
                }
            }
        }
    }


    private RectTransform GetRectTransformUnderMouse()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        foreach (RaycastResult result in results)
        {
            if (result.gameObject.GetComponent<RectTransform>() != null)
            {
                return result.gameObject.GetComponent<RectTransform>();
            }
        }

        return null;
    }
     
    public void OnEndDrag(PointerEventData eventData)
    {
        gridLayout.enabled = false;
        gridLayout.enabled = true;
    }
    private RectTransform Get_rectTransformA()
    {
        return  null;
    }

}