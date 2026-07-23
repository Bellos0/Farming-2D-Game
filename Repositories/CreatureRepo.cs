using UnityEngine;

public class CreatureRepo : MonoBehaviour, ICreature
{

    [Header("Rigidbody2D property")]
    protected Rigidbody2D rb;
    protected Collider2D cd;
    protected Animator anm;

    [Space]
    [Header("Creature Property")]
    [SerializeField] int creatureID;
    [SerializeField] string name;
    [SerializeField] int level;
    public bool isFlip; // tuc la nhan vat quay ben phai
    protected int RFlip;
    protected float distanceMove; // animal infor
    protected float startX;// animal infor
    protected float startY; // anmal infor
    [SerializeField] CreatureType type;

    public CreatureRepo()
    {
    }

    public int CreatureID { get => creatureID; set => creatureID = value; }
    public string Name { get => name; set => name = value; }
    public int Level { get => level; set => level = value; }
    public CreatureType Type { get => type; set => type = value; }

    protected virtual void Awake()
    {
        isFlip = true;
        rb = GetComponent<Rigidbody2D>() ?? GetComponentInChildren<Rigidbody2D>();
        cd = GetComponent<Collider2D>() ?? GetComponentInChildren<Collider2D>();
        anm = GetComponent<Animator>() ?? GetComponentInChildren<Animator>();
    }


    protected virtual void HandleFlip()
    {
        if (rb.velocity.x > 0 && !isFlip)
        {
            Flip();
        }
        if (rb.velocity.x < 0 && isFlip)
        {
            Flip();
        }
    }


    /// <summary>
    /// su dung lat mat object trong khi di chuyen ve phia trai hay phai
    /// </summary>
    /// <exception cref="System.NotImplementedException"></exception>
    public virtual void Flip()
    {
        isFlip = !isFlip;
        transform.Rotate(0, 180f, 0);
    }

    /// <summary>
    /// di chuyen object,
    /// player thi di chuyen bang phim tat
    /// con object dang animal and pet thi di chuyen tu dong
    /// </summary>
    /// <exception cref="System.NotImplementedException"></exception>
    public virtual void Move(float speed)
    {

    }


    /// <summary>
    /// animal, plant trong game co thoi gian de tao ra san pham nhu egg, milk.
    /// </summary>
    /// <exception cref="System.NotImplementedException"></exception>
    public virtual void Production(int id, Vector3 prodPos)
    {

    }



    public enum CreatureType
    {
        player,
        npc,
        animal,
        plant,
        pet
    }
}
