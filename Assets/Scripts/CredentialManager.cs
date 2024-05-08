using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CredentialManager : MonoBehaviour
{
 
    public TMP_InputField userNameField;
    public TMP_InputField passwordField;
    public Button loginButton;
    public GameObject loginPanel, PopUpPanel, searchPanel, fetchDataTablePanel;
    Dictionary<int, string> staffDetails = new Dictionary<int, string>
    {
        {101,"mub1999" },
        {102,"saf1999" },
        {103,"sab1999" }
    };

    public string excelFilePath;
    public string phoneNumberToSearch;

    [System.Serializable]
    public class PlayerData
    {
        public int name;
        public int id;
        public int marks;

    }

    [System.Serializable]
    public class PlayerDataList
    {
        public PlayerData[] playerData;
    }

    public TextAsset textAssetData;
    public PlayerDataList playerDataList=new PlayerDataList();


    void ReadCSV()
    {
        string[] data = textAssetData.text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);

        int tableSize = data.Length / 3 - 1;
        playerDataList.playerData= new PlayerData[tableSize];
        for (int i = 0; i < tableSize; i++)
        {
            playerDataList.playerData[i] = new PlayerData();
            playerDataList.playerData[i].name =int.Parse( data[3*(i+1)]);
            playerDataList.playerData[i].id=int.Parse(data[ 3*(i+1)+1]);
            playerDataList.playerData[i].marks=int.Parse(data[ 3*(i+1)+2]);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
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
        loginButton.onClick.AddListener(adminDetails);

        ReadCSV();
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

                Debug.Log("Invalid password");
            }
        }
        else
        {
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
    // Update is called once per frame
    void Update()
    {
        
    }
}
