using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIService : ButtonRepo
{

    [SerializeField] TMP_Dropdown dropdown;
    [SerializeField] PlayerService playerService;
    [SerializeField] TextMeshProUGUI id;
    [SerializeField] TextMeshProUGUI name;
    [SerializeField] TextMeshProUGUI level;

    private void Update()
    {
        showInforChar();
    }

    void showInforChar()
    {
        id.text = playerService.playerID.ToString();
        name.text = playerService.playerName;
        level.text = playerService.playerLevel.ToString();
    }

    public void ThongTinNhanVatClick()
    {
        Debug.Log("Thong tin nhan vat button clicked");
        Instance.action1();
    }


    public void InventoryClick()
    {
        Debug.Log("Inventory button clicked");
        Instance.action2();
    }

    public void ExitClick()
    {
        Debug.Log("Exit button clicked");
        Instance.Exit();
    }

    public void OndropdownMenu(int index)
    {
        Debug.Log("Dropdown selected index: " + index);
        switch (index)
        {

            case 1:
                Inventory.SetActive(false);
                Instance.action1();
                dropdown.value = 0;
                break;
            case 2:
                Inventory.SetActive(true);
                CharInfor.SetActive(false);
                dropdown.value = 0;
                break;
            case 3:
                dropdown.value = 0;
                Inventory.SetActive(false);
                CharInfor.SetActive(false);
                dropdown.gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }

    public void sampleDropClick()
    {
        Debug.Log("Sample drop button clicked");
    }


    public void CityCLick()
    {
        Debug.Log("City button clicked");
    }
    public void FarmClick()
    {
        Debug.Log("Farm button clicked");
        SceneManager.LoadScene("FrontFarm");
    }
    public void ParkClick()
    {
        Debug.Log("Park button clicked");
    }
    public void VilligerClick()
    {
        Debug.Log("Villiger button clicked");
    }
    public void ShoppingClick()
    {
        Debug.Log("Shopping button clicked");
    }
    public void FishingClick()
    {
        Debug.Log("Fishing button clicked");
    }

}
