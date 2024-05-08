using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataSaveLoad 
{
    //DataSaver dataSaver;
    public static void SaveProgress()
    {
        string saveDataHashed = JsonUtility.ToJson(DataSaver.dataSaver, true);
        //Debug.Log(saveDataHashed);
        File.WriteAllText(GetSavePath(), saveDataHashed);
    }

    public static DataSaver SaveObjectCreator()
    {
        DataSaver CheckSave = new DataSaver();
        return CheckSave;
    }

    public static string SaveObjectJSON()
    {
        string saveDataString = JsonUtility.ToJson(SaveObjectCreator(), true);
        return saveDataString;
    }

    public static void LoadProgress()
    {
        if (File.Exists(GetSavePath()))
        {
            string fileContent = File.ReadAllText(GetSavePath());
            JsonUtility.FromJsonOverwrite(fileContent, DataSaver.dataSaver);

            Debug.Log("Game Load Successful --> " + GetSavePath());
        }
        else
        {
            
            Debug.Log("New Game Creation Successful --> " + GetSavePath());
            SaveProgress();
        }
    }

    public static void DeleteProgress()
    {
        if (File.Exists(GetSavePath()))
        {
            File.Delete(GetSavePath());
        }
    }
    private static string GetSavePath()
    {
        return Path.Combine(Application.persistentDataPath, "SaveGameData.json");
    }

}
[Serializable]
public class DataSaver
{
    public static DataSaver  dataSaver = new DataSaver();

    public string mode;
    public int current_level;
 

   

    public int CurrentLevel { set { current_level = value; } get { return current_level; } }


    public string Mode { set { mode = value; } get { return mode; } }



       public List<DataContainer> dataContainers = new List<DataContainer>();

    public List<string> guns_name=new List<string>();

    /// <summary>
    /// Currency Workd


    public DataSaver()
    {

    }
    public  DataSaver(string _gunName)
    {
        if (guns_name == null) guns_name.Add(_gunName);
    }
    public void AddGunsName(string _guns)
    {
        //Debug.Log(_guns);
        if (guns_name == null) { guns_name.Add(_guns); return; }
        if (guns_name.Contains(_guns)) { Debug.Log("Name"); return; }
            guns_name.Add(_guns);
        
    }
    //public DataSaver(string mode)
    //{
    //    this.mode = mode;

    //}

}
[System.Serializable]
public class DataContainer
{
    public int cnic;
    public int S_no;
    public int bloackCode;
}