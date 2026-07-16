using UnityEngine;


public class ButtonRepo : ButtonFunc
{
    public static ButtonRepo Instance { get; private set; }
    [SerializeField] public GameObject CharInfor;
    [SerializeField] public GameObject Inventory;

    private void Awake()
    {
        Instance = this;
    }
    /// <summary>
    /// Bat hoi thoai thong tin nhan vat
    /// </summary>
    /// <exception cref="System.NotImplementedException"></exception>
    public override void action1()
    {
        CharInfor.SetActive(true);
    }


    /// <summary>
    /// bat inventory
    /// </summary>
    /// <exception cref="System.NotImplementedException"></exception>
    public override void action2()
    {
        Inventory.SetActive(true);
        CharInfor.SetActive(false);
    }

    public override void action3()
    {
        throw new System.NotImplementedException();
    }

    public override void action4()
    {
        throw new System.NotImplementedException();
    }

    public override void action5()
    {
        throw new System.NotImplementedException();
    }

    public void Exit()
    {
        Exit2MainScreen();
    }

}
