using UnityEngine;

public class FarmGripService : FarmGridRepo
{
    public static FarmGripService Instance { get; set; }
    private void Awake()
    {
        Instance = this;

    }

    public bool TrongCay(Vector3 screenPos, Item seedItem)
    {
        Vector3 _screenPos = ConvertFromUI_To_World(screenPos);
        Vector3Int cellPlot = soilTile.WorldToCell(_screenPos);
        cellPlot.z = 0;
        if (TryPlanSeed(cellPlot, seedItem))
        {

            return true;
        }
        else
        {

            return false;
        }
    }

    private Vector3 ConvertFromUI_To_World(Vector3 ScreenPos)
    {
        Vector3 _Pos = Camera.main.ScreenToWorldPoint(ScreenPos);
        _Pos.z = 0;
        return _Pos;
    }

    public void CheckClickFarmGrip(Vector3 screenPos, string toolHandle)
    {
        Vector3 _WorPos = ConvertFromUI_To_World(screenPos);
        //1. chuyen doi tu toa do click tu real world sang toa do tilemap
        Vector3Int cellPos = soilTile.WorldToCell(_WorPos);
        Vector2Int gripPos = new Vector2Int(cellPos.x, cellPos.y);

        // kiem tra xem vi tri click do co trong pham vi cua tile soil hay khong
        if (!soilTile.HasTile(cellPos))
        {

            Debug.Log("click out of tile");
            return;
        }
        else
        {
            InteractWithPlot(gripPos, toolHandle);
        }
    }

    public bool isCropPlotHarvest(Vector3 screenPos)
    {
        Vector3 WorPos = ConvertFromUI_To_World(screenPos);
        Vector3Int _WorPos = soilTile.WorldToCell(WorPos);
        Vector2Int _WorPos2D = new Vector2Int(_WorPos.x, _WorPos.y);
        if (Evaluate_CropPlot(_WorPos2D))
        {
            Debug.Log("thu hoach");
            return true;
        }
        else
        {
            Debug.Log("chua thu hoach duoc || cha co cay nao de thu hoach");
            return false;
        }
    }
}
