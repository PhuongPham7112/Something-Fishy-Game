using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private GameObject player;
    public Image img;
    private RectTransform rootRect;
    private RectTransform rect;
    private Transform parentAfterDrag;
    public Item item;

    private void Start()
    {
        InitializeItem(item);
        rect = GetComponent<RectTransform>();
        rootRect = transform.parent.parent.gameObject.GetComponent<RectTransform>();
        player = PlayerSingleton.Instance.gameObject;
    }
    

    public void InitializeItem(Item newItem)
    {
        item = newItem;
        img.sprite = newItem.imageSprite;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        img.raycastTarget = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // check if outside of Toolbar
        if (!AreRectsOverlapping(rect, rootRect))
        {
            Debug.Log("Outside = drop object");
            Vector3 mouseScreenPosition = Input.mousePosition;
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, Camera.main.nearClipPlane) + Camera.main.transform.forward * 20.0f);
            Instantiate(item.itemModel, mouseWorldPosition, Quaternion.identity);
            Destroy(gameObject);
        }
        else
        {
            img.raycastTarget = true;
            transform.SetParent(parentAfterDrag);
        }
    }

    public void SetInitialParent(Transform parent)
    {
        parentAfterDrag = parent;
    }

    private bool AreRectsOverlapping(RectTransform rect1, RectTransform rect2)
    {

        // Calculate adjusted positions based on anchor points
        Vector2 rect1Position = GetBottomLeftCornerPosition(rect1);
        Vector2 rect2Position = GetBottomLeftCornerPosition(rect2);

        // Calculate adjusted sizes
        Vector2 rect1Size = ConvertSizeToWorldToScreenSpace(rect1);
        Vector2 rect2Size = ConvertSizeToWorldToScreenSpace(rect2);

        Debug.Log(rect1Position + " " + rect1Size);
        Debug.Log(rect2Position + " " + rect2Size);

        Rect rect1Rect = new Rect(rect1Position, rect1Size);
        Rect rect2Rect = new Rect(rect2Position, rect2Size);

        return rect1Rect.Overlaps(rect2Rect);
    }

    private Vector3 GetBottomLeftCornerPosition(RectTransform rectTransform)
    {
        Vector3 localBottomLeftCorner = new Vector3(-rectTransform.rect.width * rectTransform.pivot.x,
                                                    -rectTransform.rect.height * rectTransform.pivot.y,
                                                    0);

        return rectTransform.TransformPoint(localBottomLeftCorner);
    }

    private Vector2 ConvertSizeToWorldToScreenSpace(RectTransform rectTransform)
    {
        Vector3[] corners = new Vector3[4];
        rectTransform.GetWorldCorners(corners);

        Vector3 bottomLeftCorner = corners[0];
        Vector3 topRightCorner = corners[2];

        Vector2 bottomLeftScreen = RectTransformUtility.WorldToScreenPoint(null, bottomLeftCorner);
        Vector2 topRightScreen = RectTransformUtility.WorldToScreenPoint(null, topRightCorner);

        Vector2 screenSize = topRightScreen - bottomLeftScreen;

        return screenSize;
    }
}
