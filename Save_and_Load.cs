using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Save_and_Load : MonoBehaviour
{
    public static Save_and_Load instance;
    public string pathfile = null;
    private void Awake()
    {
        pathfile = Path.Combine(Application.persistentDataPath + "/savedataPlayer.json");
        instance = this;
        if (!File.Exists(pathfile))
            return;
        else
            File.Create(pathfile).Dispose();
    }
    [System.Serializable]
    public class SaveModel
    {
        AccountModels.Account username;
        AccountModels.Account password;
        // string username;
        //string password;
        public int id;
        public string name;
        public int level;
        public List<CropPlotData> plotdataList = new List<CropPlotData>();

        public SaveModel(PlayerService player)
        {
            this.id = player.playerID;
            this.name = player.playerName;
            this.level = player.playerLevel;
        }

    }

    public void SaveData(PlayerService player)
    {
        SaveModel data = new SaveModel(player);

        if (FarmGripService.Instance.GetAllPlotData() != null)
        {
            data.plotdataList = FarmGripService.Instance.GetAllPlotData();
        }
        else
            Debug.Log("plotdata null");

        string jsonfile = JsonUtility.ToJson(data, true);

        File.WriteAllText(pathfile, jsonfile);
        Debug.Log("savedata done");
    }


    public void LoadData(PlayerService player)
    {
        string jsonsave = File.ReadAllText(pathfile);

        if (string.IsNullOrEmpty(jsonsave))
        {
            SaveModel dataRestore = JsonUtility.FromJson<SaveModel>(jsonsave);
            player.playerID = dataRestore.id;
            player.playerName = dataRestore.name;
            player.playerLevel = dataRestore.level;
            if (FarmGripService.Instance != null)
            {
                FarmGripService.Instance.RestorePlotData(dataRestore.plotdataList);
            }
            else Debug.Log("khong the khoi phuc lai du lieu o dat");

        }
        else return;

    }

}
