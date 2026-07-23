using System;
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
    ItemModels[] seedList;
    [Space]
    public GameObject animalPrefab;
    public ItemModels[] animalList;
    Vector3 spawnPos_2leg;
    Vector3 spawnPos_4leg;


    public Vector3 SpawnPos_2leg { get => spawnPos_2leg; set => spawnPos_2leg = value; }
    public Vector3 SpawnPos_4leg { get => spawnPos_4leg; set => spawnPos_4leg = value; }

    public Dictionary<Vector2Int, CropPlotData> farmGrip = new Dictionary<Vector2Int, CropPlotData>();

    protected virtual void Awake()
    {
        seedList = GameObject.FindAnyObjectByType<SeedInvenManager>().item ?? null;
        SpawnPos_2leg = new Vector3(40, 5, 0);
        SpawnPos_4leg = new Vector3(70, 5, 0);
    }

    public void addAnimal(int id, Vector3 farmPos)
    {
        GameObject newAnimal = Instantiate(animalPrefab, farmPos, Quaternion.identity);
        var animalData = animalList.FirstOrDefault(x => x.id == id);
        if (animalData != null)
        {
            AnimalGeneral _newAnimal = newAnimal.GetComponent<AnimalGeneral>();
            _newAnimal.animalModel = animalData;
            _newAnimal.CreatureID = animalData.id;
            _newAnimal.Name = animalData.itemName;
            _newAnimal.Sprite.sprite = animalData.sprite;
            _newAnimal.Type = CreatureRepo.CreatureType.animal;
            _newAnimal.TimeSpawn = DateTime.Now.ToString("O");
        }
        //else return;
    }



    public void InteractWithPlot(Vector2Int gripPos, string tool)
    {

        //2. kiem tra xem o dat da co trong list farmGrip chua
        if (!farmGrip.ContainsKey(gripPos))
        {
            CropPlotData newPlot = new CropPlotData(gripPos);
            farmGrip.Add(gripPos, newPlot);
            // Save_and_Load.instance.addPlotList(newPlot);

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


    public bool TryPlanSeed(Vector3Int worldPos, ItemModels seedItem)
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
    public void UpdateTileVisual(CropPlotData plot)
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
