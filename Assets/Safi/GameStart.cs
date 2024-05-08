using UnityEngine;

public class GameStart : MonoBehaviour
{

    bool Initialized = false;
    private void Awake()
    {
        if (!Initialized)
        {
            InitializeGame();
        }
    }
    void InitializeGame()
    {
        DataSaveLoad.LoadProgress();
        Initialized = true;
    }
}