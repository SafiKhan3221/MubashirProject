using UnityEngine;

public class Test : MonoBehaviour
{
    // this is the test script use to save and retrieve data from json

    void Start()
    {
        //save value in json 
        DataSaver.dataSaver.CurrentLevel= 10;
     
        DataSaver.dataSaver.Mode = "FreeMode";
        //DataSaveLoad.SaveProgress();

        //save value in json
        //DataSaver.instance.Mode = "BikeVsBike";

        DataSaveLoad.SaveProgress();

        //retrieve value from json
      
    }

  
}
