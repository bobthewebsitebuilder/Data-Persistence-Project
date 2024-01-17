using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public GameObject GameOverText;
    [SerializeField] private Text topScoreText;

    private bool gameStarted = false;
    private int points = 0;
    private bool gameOver = false;

    void Start()
    {
        InitializeBricks();
        SetTopScoreText();
    }

    private void Update()
    {
        if (!gameStarted)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                StartGame();
        }
        else if (gameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                RestartGame();
            else if (Input.GetKeyDown(KeyCode.Escape))
                ReturnToMenu();
        }
    }

    void InitializeBricks()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    void StartGame()
    {
        gameStarted = true;
        float randomDirection = Random.Range(-1.0f, 1.0f);
        Vector3 forceDir = new Vector3(randomDirection, 1, 0);
        forceDir.Normalize();

        Ball.transform.SetParent(null);
        Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

    void AddPoint(int point)
    {
        points += point;
        ScoreText.text = $"Your score: {points}";
    }

    void SetTopScoreText()
    {
        topScoreText.text = GameInfo.Instance.GetBestScoreText();
    }

    public void GameOver()
    {
        gameOver = true;

        if (GameInfo.Instance.UpdateBestScore(points))
            SetTopScoreText();

        GameOverText.SetActive(true);
    }
}
