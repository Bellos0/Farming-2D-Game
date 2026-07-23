using UnityEngine;


public class AnimalGeneral : CreatureRepo
{
    public static AnimalGeneral Instance;

    public ItemModels animalModel;
    SpriteRenderer sprite;
    public GameObject ProdPrefab;
    public ItemModels[] prodList;
    public bool isLife;
    string timeSpawn;

    public SpriteRenderer Sprite { get => sprite; set => sprite = value; }
    public string TimeSpawn { get => timeSpawn; set => timeSpawn = value; }

    protected override void Awake()
    {
        Instance = this;
        base.Awake();
        RFlip = 1;
        distanceMove = 5f;
        startX = transform.position.x;
        startY = transform.position.y;
        sprite = GetComponent<SpriteRenderer>();

    }

    private void Update()
    {
        Move(2f);
        HandleFlip();
    }

    public override void Production(int id, Vector3 prodPos)
    {
        //  timeSpawn = DateTime.Now.ToString("O");
        GameObject AnmProd = Instantiate(ProdPrefab, prodPos, Quaternion.identity);
        PickupStuff _AnmProd = AnmProd.GetComponent<PickupStuff>();
        _AnmProd.itemdata = AnmProd.GetComponent<PickupStuff>().Initialize(prodList[id]);

    }

    // cho ham di chuyen cua may con ga trong nay
    public override void Move(float speed)
    {
        rb.velocity = new Vector2(speed * RFlip, rb.velocity.y);
        float CurDistanceX = Mathf.Abs(transform.position.x - startX);
        float CurDistanceY = Mathf.Abs(transform.position.y - startY);
        if (CurDistanceX >= distanceMove)
        {
            rb.velocity *= -1;
            startX = transform.position.x;
        }

    }
    protected override void HandleFlip()
    {
        if (rb.velocity.x > 0 && RFlip < 0)
        {
            Flip();
        }
        if (rb.velocity.x < 0 && RFlip > 0)
        {
            Flip();
        }
    }
    public override void Flip()
    {
        base.Flip();
        RFlip = -1 * RFlip;
    }

}
