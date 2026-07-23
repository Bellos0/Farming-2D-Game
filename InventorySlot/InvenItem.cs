using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InvenItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [Header("Item Info, link voi item.cs")]
    ItemModels item;
    int qty;
    [Space]
    [Header("Thuoc tinh tac dong den drag and drop event")]
    Transform originalTrans;
    Image image;
    [Space]
    [SerializeField] Text? count;
    public Transform OriginalTrans { get => originalTrans; set => originalTrans = value; }
    public Image Image { get => image; set => image = value; }
    public ItemModels Item { get => item; set => item = value; }
    public int Qty { get => qty; set => qty = value; }


    private void Awake()
    {
        image = GetComponent<Image>();

        Qty = 1;
        refreshQty();
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        OriginalTrans = transform.parent; // luu vi tri ban dau cua item truoc khi keo
        Image.raycastTarget = false; // tat raycast de item khong bi block khi keo
        transform.SetParent(transform.root); // dat item len root de no hien tren tat ca cac object khac
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position; // dat item theo vi tri con tro chuot khi keo
    }


    public virtual void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(OriginalTrans);
        transform.localPosition = Vector3.zero; // dat item ve vi tri ban dau neu khong tha vao slot

        Image.raycastTarget = true; // bat lai raycast de item co the block cac object khac
    }

    public void refreshQty()
    {
        count.text = Qty.ToString();
        if (Qty <= 1 || count == null)
        {
            count.gameObject.SetActive(false);
        }
        else
        {
            count.gameObject.SetActive(true);
        }

    }



}
