using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO; 

public class GameInfo : MonoBehaviour
{

    public static GameInfo Instance;
    PlayerInfo Player;
    PlayerInfo HiScore;
    private string Filepath;
    // Start is called before the first frame update
    void Start()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        Player = new PlayerInfo();
        HiScore = new PlayerInfo();
        Filepath = Application.persistentDataPath + "/hiscore.json";
        LoadHighScore();

    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void NewGame()
    {
        Player.score = 0;
        Player.name = GameObject.Find("PlayerName").GetComponent<TMP_InputField>().text;
        Debug.Log($"My name is {Player.name}");
        SceneManager.LoadScene(1);
    }

    public string PlayerName() { return Player.name; }
    public int TopScore() { return HiScore.score; }
    public string TopName() { return HiScore.name; }

    public void SubmitFinalScore(int score)
    {
        Player.score = score;
        if (Player.score > HiScore.score)
        {
            HiScore.score = Player.score;
            HiScore.name = Player.name;
            File.WriteAllText(Filepath, JsonUtility.ToJson(HiScore));
        }
    }

    private void LoadHighScore()
    {
        if (!File.Exists(Filepath))
        {
            HiScore.score = 0;
            HiScore.name = "";
            Debug.Log("No Highscore found!");
            return;
        }
        HiScore = JsonUtility.FromJson<PlayerInfo>(File.ReadAllText(Filepath));
        Debug.Log("Loaded the highscore!");
    }

    [System.Serializable]
    public class PlayerInfo
    {
        public string name;
        public int score;
    }
}
