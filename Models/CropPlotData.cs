using System;
using UnityEngine;

public class CropPlotData
{
    public Vector2Int gridPos; // toa do cua  dat trong grid
    public PlotState state; // trang thai cua o dat

    // phan thong tin mo rong sau nay
    public int plantedCropID;
    public int currentGrowthStage; // giai doan phat trien hien tai cua cay trong 
    public int timeWaterd; // thoi gian da duoc cham soc
    public string plantTImeAsString;
    /// <summary>
    /// ham khoi tao khi tao moi 1 o dat
    /// </summary>
    /// <param name="gridPosition"></param>
    public CropPlotData(Vector2Int gridPosition)
    {
        gridPos = gridPosition;
        state = PlotState.raw;
        plantedCropID = -1;
        currentGrowthStage = 0;
        timeWaterd = 0;
        plantTImeAsString = string.Empty;
    }

    public bool isCanPlant()
    {
        if (plantedCropID < 0)
        {
            return true;
        }
        return false;
    }

    public void PlanSeed(Item seedItem)
    {
        plantedCropID = seedItem.id;

        plantTImeAsString = DateTime.Now.ToString("O");
    }

    public enum PlotState
    {
        raw,
        tilled,
        watered,
    }
}
