using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FarmGridRepo : MonoBehaviour
{
    [Header("tilemap references")]
    public Tilemap soilTile;
    //[SerializeField] Grid grid;

    [Header("tile resource")]
    public TileBase? soilRaw;
    public TileBase? soilTiled;
    public TileBase? soilTileWatered;

    [Space]
    Item[] seedList;

    Dictionary<Vector2Int, CropPlotData> farmGrip = new Dictionary<Vector2Int, CropPlotData>();

    private void Awake()
    {
        seedList = GameObject.FindAnyObjectByType<SeedInvenManager>().item ?? null;
    }
    public void InteractWithPlot(Vector2Int gripPos, string tool)
    {


        //2. kiem tra xem o dat da co trong list farmGrip chua
        if (!farmGrip.ContainsKey(gripPos))
        {
            CropPlotData newPlot = new CropPlotData(gripPos);
            farmGrip.Add(gripPos, newPlot);
            Debug.Log("add new plot interact with plot");
        }


        // lay du lieu o  dattu farmgrip de xu y

        CropPlotData currentPlot = farmGrip[gripPos];
        if (tool.ToLower() == "hoe")
        {
            currentPlot.state = CropPlotData.PlotState.tilled; // doi data thanh da quoc dat
            UpdateTileVisual(currentPlot);
        }
        if (tool.ToLower() == "can")
        {
            currentPlot.state = CropPlotData.PlotState.watered; // doi data thanh da quoc dat
            UpdateTileVisual(currentPlot);
        }
    }


    public bool TryPlanSeed(Vector3Int worldPos, Item seedItem)
    {
        // khong co seeditem thi bo qua luon khoi phai trong trot gi nua
        if (seedItem == null) { return false; }

        // chuyen sang toa do 2D de truy xuat qua dictionanry.
        Vector2Int gridPos2D = new Vector2Int(worldPos.x, worldPos.y);

        // kiem tra xem tao do da co dung torng tile map khong
        if (soilTile.HasTile(worldPos))
        {

            CropPlotData currentPlot = GetSeedInSoil(gridPos2D);

            // neu currentplot ma chua co thi tao moi luon.
            if (currentPlot == null)
            {
                currentPlot = new CropPlotData(gridPos2D);
                farmGrip.Add(gridPos2D, currentPlot);
            }

            if (currentPlot.isCanPlant(currentPlot))
            //if (currentPlot.state == CropPlotData.PlotState.tilled && currentPlot.isCanPlant())
            {
                currentPlot.PlanSeed(seedItem);
                UpdateTileVisual(currentPlot);
                Debug.Log("repo trong cay thanh cong");
                return true;
            }
        }
        Debug.Log("repo trong cay that bai");
        return false;
    }



    /// <summary>
    /// lay duoc thong tin cay co trong o dat
    /// </summary>
    /// <param name="gridPos"></param>
    /// <returns></returns>
    private CropPlotData GetSeedInSoil(Vector2Int gridPos)
    {
        if (farmGrip.TryGetValue(gridPos, out CropPlotData plotData))
            return plotData;
        else return null;
    }


    /// <summary>
    /// hien thi cac tile tuong ung voi trang thai cua dat
    /// </summary>
    /// <param name="plot"></param>
    private void UpdateTileVisual(CropPlotData plot)
    {
        Vector3Int tilePos = new Vector3Int(plot.gridPos.x, plot.gridPos.y, 0);

        switch (plot.state)
        {
            case CropPlotData.PlotState.raw:
                //soilTile.SetTile(tilePos, soilRaw);
                Debug.Log("raw");
                break;
            case CropPlotData.PlotState.tilled:
                //soilTile.SetTile(tilePos, soilTiled);
                Debug.Log("tilled");
                break;
            case CropPlotData.PlotState.watered:
                //soilTile.SetTile(tilePos, soilTileWatered);
                Debug.Log("watered");
                break;
            default:
                break;
        }
    }

    public bool Evaluate_CropPlot(Vector2Int gridPos)
    {

        CropPlotData plotData = GetSeedInSoil(gridPos);
        // kiem tra thong tin o dat, neu empty thi bo qua thoi
        if (plotData == null || plotData.isCanPlant(plotData))
        {
            Debug.Log("thua dat khong co cay");
            return false;
        }
        int minRequire;
        if (seedList != null)
        {

            minRequire = seedList.FirstOrDefault(seed => seed.id == plotData.plantedCropID).minToGrow;

            //var min = seedList.Where(seed => seed.id >= 0)
            //         .Select(seed => seed.minToGrow);
            //int _min = int.Parse(min.ToString());
        }
        else minRequire = 0;


        double minPassed = TimeManager.instance.GetMinPassedSince(plotData.plantTImeAsString);
        if (minPassed > minRequire)
        {
            Debug.Log("co the thu haoch");
            plotData.state = CropPlotData.PlotState.raw;
            UpdateTileVisual(plotData);
            return true;
        }
        else
        {
            double minLeft = minRequire - minPassed;
            Debug.Log($"con {minLeft} moi thu hoach duoc");
            return false;
        }
    }
}
