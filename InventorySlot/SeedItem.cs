using UnityEngine;
using UnityEngine.EventSystems;

public class SeedItem : InvenItem
{
    public override void OnEndDrag(PointerEventData eventData)
    {
        Image.raycastTarget = true;
        bool successed = FarmGripService.Instance.TrongCay(Input.mousePosition, this.Item);
        if (successed)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.SetParent(OriginalTrans);
            transform.localPosition = Vector3.zero;
        }

    }
}
