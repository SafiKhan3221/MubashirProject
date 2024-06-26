using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CredentialManager : MonoBehaviour
{
 
    public TMP_InputField userNameField;
    public TMP_InputField passwordField;
    public TMP_InputField userCnic;
    public Button loginButton,searchBtn;
    public GameObject loginPanel, PopUpPanel, searchPanel, fetchDataTablePanel,loadingScreen;
    public TextMeshProUGUI showcnicText,showSNumbText,showblcktext;







    Dictionary<int, string> staffDetails = new Dictionary<int, string>
    {
        {101,"mub1999" },
        {102,"saf1999" },
        {103,"sab1999" }
    };


    public TextMeshProUGUI popUpText;
    [System.Serializable]
    public class PlayerData
    {

        public long cnic;
        public long srNumber;
       
        public long blockCode;

    }

    [System.Serializable]
    public class PlayerDataList
    {
        public PlayerData[] playerData;
    }

    public TextAsset textAssetData;
    public PlayerDataList playerDataList=new PlayerDataList();
    private static string GetSavePath()
    {
        return Path.Combine(Application.persistentDataPath, "vps.json");
    }
    private void Awake()
    {
        Debug.Log(Application.persistentDataPath);
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        if (File.Exists(GetSavePath()))
        {
            string fileContent = File.ReadAllText(GetSavePath());
            // Create a new TextAsset and assign the file content
            textAssetData = new TextAsset(fileContent);

            // Optionally, you can set the name of the TextAsset
            textAssetData.name = Path.GetFileNameWithoutExtension("vps.json");
            //JsonUtility.FromJsonOverwrite(fileContent, DataSaver.dataSaver);

            Debug.Log("Game Load Successful --> " + GetSavePath());
        }
        else
        {
            //SaveProgress();
        }
    }
    void ReadCSV()
    {
        string[] data = textAssetData.text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);

        int tableSize = data.Length / 3 - 1;
        playerDataList.playerData= new PlayerData[tableSize];
        for (int i = 0; i < tableSize; i++)
        {
            playerDataList.playerData[i] = new PlayerData();
            playerDataList.playerData[i].srNumber =Int64.Parse( data[3*(i+1)]);
            playerDataList.playerData[i].cnic= Int64.Parse(data[ 3*(i+1)+1]);
            playerDataList.playerData[i].blockCode=Int64.Parse(data[ 3*(i+1)+2]);
        }
    }

    string ReturnData(int cnic)
    {
        for (int i = 0; i < playerDataList.playerData.Length; i++)
        {
            if (playerDataList.playerData[i].srNumber == cnic)
            {
                return playerDataList.playerData[i].cnic.ToString();
            }
        }
        return string.Empty;
    }
    public void SearchData()
    {
       
        if (Int64.TryParse(userCnic.text, out long  data))
        {


            print(playerDataList.playerData.Length);
     
            for(int i = 0;i < playerDataList.playerData.Length; i++)
            {
             
                if (playerDataList.playerData[i].cnic == data)
                {
                    //print(playerDataList.playerData[i].cnic);
                    //print(playerDataList.playerData[i].srNumber);
                    //print(playerDataList.playerData[i].blockCode);
                    showcnicText.text = playerDataList.playerData[i].cnic.ToString();
                    showSNumbText.text = playerDataList.playerData[i].srNumber.ToString();
                    showblcktext.text = playerDataList.playerData[i].blockCode.ToString();
                    fetchDataTablePanel.SetActive(true);
                    PopUpPanel.SetActive(false);
                    break;
                  
                }
                else
                {
                    showcnicText.text =string.Empty;
                    showSNumbText.text = string.Empty;
                    showblcktext.text = string.Empty;
                    PopUpPanel.SetActive(!false);
                    popUpText.text = "CNIC Number is Incorrect! Try Again";
                    //fetchDataTablePanel.SetActive(false);
                 

                }
            }
            //if (staffDetails.TryGetValue(cnic, out foundCnic)&& foundCnic==ReturnData(cnic))
            //{
               
            //}
            //else
            //{
            //    PopUpPanel.SetActive(true);

            //    Debug.Log("Invalid data");
            //}
        }
        else
        {
            showcnicText.text = string.Empty;
            showSNumbText.text = string.Empty;
            showblcktext.text = string.Empty;
            popUpText.text = "CNIC Number is Incorrect! Try Again";
            PopUpPanel.SetActive(true);
        }
    }
    // Start is called before the first frame update
  IEnumerator Start()
    {
         loadingScreen.SetActive(!false);
        ReadCSV();

        yield return new WaitForSeconds(6f);
        loadingScreen.SetActive(false);

        if (PlayerPrefs.GetInt("FirstTime") != 1)
        {
            loginPanel.SetActive(true);
            searchPanel.SetActive(!true);
        }
        else
        {
            loginPanel.SetActive(!true);
            searchPanel.SetActive(true);
        }
        showcnicText.text = string.Empty;
        showSNumbText.text = string.Empty;
        showblcktext.text = string.Empty;
        loginButton.onClick.RemoveAllListeners();
        loginButton.onClick.AddListener(adminDetails);
       searchBtn.onClick.RemoveAllListeners();
       searchBtn.onClick.AddListener(SearchData);

       
    }
    public void adminDetails()
    {
        //Get Username from Input then convert it to int
        if(int.TryParse(userNameField.text, out int id))
        {
            int userName = id;
            //Get Password from Input 
            string password = passwordField.text;

            string foundPassword;
            if (staffDetails.TryGetValue(userName, out foundPassword) && (foundPassword == password))
            {
                    loginPanel.SetActive(false); 
                searchPanel.SetActive(!false);
                PlayerPrefs.SetInt("FirstTime", 1);
                Debug.Log("User authenticated");
            }
            else
            {
                PopUpPanel.SetActive(true);
                popUpText.text = "UserName or Password is Incorrect! Try Again";
                Debug.Log("Invalid password");
            }
        }
        else
        {
            popUpText.text = "UserName or Password is Incorrect! Try Again";
            PopUpPanel.SetActive(true);
        }

    }

    public void OnInputFieldValueChanged()
    {
        // Check if both username and password fields have text
        bool isUsernameValid = !string.IsNullOrEmpty(userNameField.text);
        bool isPasswordValid = !string.IsNullOrEmpty(passwordField.text);

        // Enable the login button if both fields are not empty
        loginButton.interactable = isUsernameValid && isPasswordValid;
    }
    public  void SaveProgress()
    {
      

        // Optionally, you can set the name of the TextAsset
        textAssetData.name = Path.GetFileNameWithoutExtension("vps.json");
        string saveDataHashed = JsonUtility.ToJson(textAssetData, true);
        //Debug.Log(saveDataHashed);
        File.WriteAllText(GetSavePath(), saveDataHashed);
    }
}
