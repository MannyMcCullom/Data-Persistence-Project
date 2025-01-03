using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class NameHandler : MonoBehaviour
{
    static string[] playerName = new string[6]; // new variable declared
    static int[] playerScore = new int[6];

    public TMP_InputField inputBox;
    public TextMeshProUGUI scoreDisplay;
    public TextMeshProUGUI bestScore;

    public static NameHandler nameInstance;

    private void Awake()
    {
        LoadHighscore(playerName, playerScore);

        if (SceneManager.GetActiveScene().name == "highscore")
        {
            DisplayScore();
        }

        if (SceneManager.GetActiveScene().name == "main")
        {
            DisplayBestScore();
        }
    }

    [System.Serializable]
    class SaveData
    {
        public string[] playerName = new string[6];
        public int[] playerScore = new int[6];
    }

    public void DisplayBestScore()
    {
        if (playerScore[0] > 0)
        {
            bestScore.text = "Best Score: " + playerName[0] + " " + playerScore[0];
        }
        else
        {
            bestScore.text = "";
        }
    }

    public void DisplayScore()
    {
        scoreDisplay.text = "";
        for (int i = 0; i < 5; i++)
        {
            scoreDisplay.text += playerName[i] + " " + playerScore[i];
            scoreDisplay.text += "\n";
        }
    }

    public void ResetScore()
    {
        SaveData data = new SaveData();

        for (int index = 0; index < 6; index++)
        {
            data.playerName[index] = "AAA";
            data.playerScore[index] = 0;
        }

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefilescore.json", json);

        Debug.Log("High Score Reset:");
        for (int index = 0; index < 6; index++)
        {
            Debug.Log(data.playerName[index] + " " + data.playerScore[index]);
        }

        LoadHighscore(playerName, playerScore);

        if (SceneManager.GetActiveScene().name == "highscore")
        {
            DisplayScore();
        }
    }

    public void LoadHighscore(string[] names, int[] scores)
    {
        string path = Application.persistentDataPath + "/savefilescore.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            Debug.Log("Scores Loaded:");
            for (int index = 0; index < 6; index++)
            {
                names[index] = data.playerName[index];
                scores[index] = data.playerScore[index];

                Debug.Log(names[index] + " " + scores[index]);
            }
        }

        else
        {
            Debug.Log("File not Loaded.");
            ResetScore();
        }
    }

    public void CheckScore()
    {
        int tempScore;
        string tempName;

        for (int index = 0; index < 5; index++)
        {
            if (playerScore[index] < playerScore[index + 1] )
            {
                tempScore = playerScore[index];
                tempName = playerName[index];

                playerScore[index] = playerScore[index + 1];
                playerName[index] = playerName[index + 1];

                playerScore[index + 1] = tempScore;
                playerName[index + 1] = tempName;

                index = -1;
            }
        }
    }

    public void UpdateScore(string[] names, int[] scores)
    {
        CheckScore();
        SaveData data = new SaveData();

        for (int index = 0; index < 6; index++)
        {
            data.playerName[index] = names[index];
            data.playerScore[index] = scores[index];
        }

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefilescore.json", json);
        Debug.Log("Scores Updated");
    }

    public void SavePlayer(string name, int score)
    {
        playerName[5] = name;
        playerScore[5] = score;

        UpdateScore(playerName, playerScore);

        Debug.Log("Player Saved: " + name + " " + score);
    }

    public void LoadPlayer()
    {
        string path = Application.persistentDataPath + "/savefilescore.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            playerName[5] = data.playerName[5];
            playerScore[5] = data.playerScore[5];

            Debug.Log("Player Loaded: " + playerName[5] + " " + playerScore[5]);
        }

        else
        {
            Debug.Log("File not Loaded.");
            LoadHighscore(playerName, playerScore);
        }
    }

    public void AlocatePlayer()
    {
        SavePlayer(inputBox.text, 0);
    }

    public string getPlayerName()
    {
        return playerName[5];
    }

    public int getPlayerScore()
    {
        return playerScore[5];
    }
}
