using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;

public class GameInfo : MonoBehaviour
{
    public static GameInfo Instance;
    string Player;
    List<PlayerInfo> ScoresList;
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
        ScoresList = new List<PlayerInfo>();
        Filepath = Application.persistentDataPath + "/hiscore.json";
        LoadHighScore();
    }

    private void LoadHighScore()
    {
        if (!File.Exists(Filepath))
        {
            Debug.Log("No Highscore found!");
            return;
        }
        GameData hiscores = JsonUtility.FromJson<GameData>(File.ReadAllText(Filepath));
        for(int i = 0; i < hiscores.hiscoreInts.Count; i++)
        {
            ScoresList.Add(new PlayerInfo(hiscores.hiscoreStrs[i], hiscores.hiscoreInts[i]));
        }
    }

    private static int ComparePlayerScores(PlayerInfo x, PlayerInfo y)
    {
        if (x.score > y.score)
            return -1;
        if (x.score < y.score)
            return 1;
        return 0;
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void NewGame()
    {
        Player = GameObject.Find("PlayerName").GetComponent<TMP_InputField>().text;
        Debug.Log($"My name is {Player}");
        SceneManager.LoadScene(1);
    }

    public string PlayerName() { return Player; }
    public int TopScore() 
    {
        if (ScoresList.Count > 0)
            return ScoresList[0].score;
        else
            return 0;
    }
    public string TopName() 
    {
        if (ScoresList.Count > 0)
            return ScoresList[0].name;
        else
            return "Nobody";
    }

    public void SubmitFinalScore(int score)
    {
        PlayerInfo newEntry = new PlayerInfo(Player, score);
        ScoresList.Add(newEntry);
        ScoresList.Sort(ComparePlayerScores);
        if(ScoresList.Count > 10)
            ScoresList = ScoresList.GetRange(0, 10);
        GameData newData = new GameData();
        foreach(var entry in ScoresList)
        {
            newData.hiscoreInts.Add(entry.score);
            newData.hiscoreStrs.Add(entry.name);
        }
        File.WriteAllText(Filepath, JsonUtility.ToJson(newData));
    }

    public int GetNumHiscores() { return ScoresList.Count; }
    public string GetNameOfNthScore(int n) { return ScoresList[n].name; }
    public int GetScoreOfNthScore(int n) { return ScoresList[n].score; }

    public void GoToHiscore() { SceneManager.LoadScene(2); }
    public void GoToMenu() { SceneManager.LoadScene(0); }

    public class PlayerInfo
    {
        public PlayerInfo(string _name, int _score)
        {
            name = _name;
            score = _score;
        }
        public string name;
        public int score;
    }

    [System.Serializable]
    public class GameData
    {
        public GameData()
        {
            hiscoreInts = new List<int>();
            hiscoreStrs = new List<string>();
        }
        //Cant use a list of custom types without way more work :(
        public List<int> hiscoreInts;
        public List<string> hiscoreStrs;
    }
}
