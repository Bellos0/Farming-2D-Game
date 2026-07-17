using UnityEngine;


public class PlayerService : CreatureRepo
{
    [SerializeField] FarmGripService? farmGripService;
    Item curHoldingItem;
    private void Awake()
    {
        isFlip = true;
        rb = GetComponent<Rigidbody2D>() ?? GetComponentInChildren<Rigidbody2D>();
        cd = GetComponent<Collider2D>() ?? GetComponentInChildren<Collider2D>();
        anm = GetComponent<Animator>() ?? GetComponentInChildren<Animator>();
        transform.position = new Vector3(-11f, -2, 0);
        Debug.Log($"gia tri khoi tao cua {curHoldingItem}");
    }
    void Update()
    {
        Debug.Log($"gia tri khoi tao cua {curHoldingItem}");
        HandleFlip();
        Move(10f);

        //1. chon item bang phim so hotkey
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SeedInvenManager.Instance.SelectSeed = 0;

            curHoldingItem = SeedInvenManager.Instance.PickUpSeed(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            curHoldingItem = new Item("liem");
            curHoldingItem.Type = Item.ItemType.liem;
        }
        //2. click chuot trai  de su dung item dang cam
        if (Input.GetMouseButtonDown(0))
        {

            Vector3 mouUIPos = Input.mousePosition;
            mouUIPos.z = 0;
            if (curHoldingItem?.Type == Item.ItemType.seed)
            {
                farmGripService.TrongCay(mouUIPos, curHoldingItem);
            }

            else
                farmGripService.CheckClickFarmGrip(mouUIPos, "hoe");
        }

        // chuot phai click thu hoach
        if (Input.GetMouseButtonDown(1))
        {

            Vector3 mouUIPos = Input.mousePosition;
            mouUIPos.z = 0;

            if (curHoldingItem?.Type == Item.ItemType.liem)
            {
                // farmGripService.TrongCay(mouUIPos, curHoldingItem);
                farmGripService.isCropPlotHarvest(mouUIPos);
            }

        }
    }

    public void Flip()
    {
        if (rb.velocity.x > 0F && !isFlip)
        {
            // transform.rotation;
        }
    }

    public void Move(float speed)
    {
        float Xinput = Input.GetAxis("Horizontal");
        float Yinput = Input.GetAxis("Vertical");

        rb.velocity = new Vector2(Xinput * speed, Yinput * speed);
    }

    public void Production()
    {
        throw new System.NotImplementedException();
    }



    protected override void HandleFlip()
    {
        base.HandleFlip();
    }

    public int playerID { get => CreatureID; set => CreatureID = value; }
    public string playerName { get => Name; set => Name = value; }
    public int playerLevel { get => Level; set => Level = value; }
}
