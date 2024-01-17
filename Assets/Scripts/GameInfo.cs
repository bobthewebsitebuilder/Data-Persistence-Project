using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GameInfo : MonoBehaviour
{
    public static GameInfo Instance;

    [SerializeField] private int bestScore;
    [SerializeField] private string bestPlayersName;
    [SerializeField] private string currentPlayersName;

    public int BestScore
    {
        get { return bestScore; }
        set { bestScore = value; }
    }

    public string BestPlayersName
    {
        get { return bestPlayersName; }
        set { bestPlayersName = value; }
    }

    public string CurrentPlayersName
    {
        get { return currentPlayersName; }
        set { currentPlayersName = value; }
    }

    [System.Serializable]
    class GameInfoToSerialize
    {
        public int bestScore;
        public string bestPlayersName;

        public GameInfoToSerialize(int bestScore, string bestPlayersName)
        {
            this.bestScore = bestScore;
            this.bestPlayersName = bestPlayersName;
        }
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        Load();
    }

    string GetGameInfoPath()
    {
        return Application.persistentDataPath + "/gameinfo.json";
    }

    public void Save()
    {
        string json = JsonUtility.ToJson(new GameInfoToSerialize(bestScore, bestPlayersName));
        File.WriteAllText(GetGameInfoPath(), json);
    }

    public void Load()
    {
        string path = GetGameInfoPath();
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            GameInfoToSerialize data = JsonUtility.FromJson<GameInfoToSerialize>(json);

            bestScore = data.bestScore;
            bestPlayersName = data.bestPlayersName;
        }
    }

    public bool UpdateBestScore(int newScore)
    {
        if (bestScore <= newScore)
        {
            bestScore = newScore;
            bestPlayersName = currentPlayersName;
            return true;
        }
        return false;
    }

    public string GetBestScoreText()
    {
        return "Best Score : " + bestPlayersName + " : " + bestScore;
    }
}
