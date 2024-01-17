using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

[DefaultExecutionOrder(1000)]
public class MenuHandler : MonoBehaviour
{
    [SerializeField] private Text topScore;
    [SerializeField] private TMP_InputField playerNameInput;

    void SetTopScoreAndPlayerName()
    {
        GameInfo gameData = GameInfo.Instance;
        if (gameData == null)
        {
            return;
        }

        topScore.text = gameData.GetBestScoreText();
        playerNameInput.text = gameData.CurrentPlayersName;
    }

    void Start()
    {
        SetTopScoreAndPlayerName();
    }

    public void StartNew()
    {
        GameInfo.Instance.CurrentPlayersName = playerNameInput.text;
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        GameInfo.Instance.Save();
        Application.Quit();
    }
}
