using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;
public class GameManager : MonoBehaviour
{
    private int scorePerItem = 100;
    private int scorePerStomp = 200;
    private int timeBonus = 10;
    private float timeLimit = 60f;
    private int score = 0;
    private float remainingTime;
    public static GameManager Instance { get; private set; }
    public enum GameState
    {
        Title,
        Playing,
        GameOver,
        GameClear
    }

    public GameState CurrentState { get; private set; }

    private int itemCount = 0;

    private int requiredItemCount = 3;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateStateFromScene();
    }

    void Update()
    {
        if (CurrentState == GameState.Playing)
        {
            remainingTime -= Time.deltaTime;
            if (remainingTime <= 0f)
            {
                remainingTime = 0f;
                GameOver();
                return;
            }
        }
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            switch (CurrentState)
            {
                case GameState.Title:
                    StartGame();
                    break;
                case GameState.GameOver:
                case GameState.GameClear:
                    ReturnToTitle();
                    break;
            }
        }
    }

    private void UpdateStateFromScene()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        switch (sceneName)
        {
            case "TitleScene":
                CurrentState = GameState.Title;
                break;

            case "GameScene":
                CurrentState = GameState.Playing;
                break;

            case "GameOverScene":
                CurrentState = GameState.GameOver;
                break;

            case "GameClearScene":
                CurrentState = GameState.GameClear;
                break;
        }
        if (sceneName == "TitleScene" &&
        SoundManager.Instance != null)
        {
            SoundManager.Instance.PlayBGM("title");
        }
    }

    public void StartGame()
    {
        itemCount = 0;
        score = 0;
        remainingTime = timeLimit;
        CurrentState = GameState.Playing;
        SceneManager.LoadScene("GameScene");
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlayBGM("game");
        }

        SceneManager.LoadScene("GameScene");
    }


    public void ReturnToTitle()
    {
        itemCount = 0;
        CurrentState = GameState.Title;
        SceneManager.LoadScene("TitleScene");
    }

    public void GameOver()
    {
        CurrentState = GameState.GameOver;
        SceneManager.LoadScene("GameOverScene");
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.StopBGM();
            SoundManager.Instance.PlaySE("gameover");
        }

        SceneManager.LoadScene("GameOverScene");
    }

    public void GameClear()
    {
        CurrentState = GameState.GameClear;
        SceneManager.LoadScene("GameClearScene");
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.StopBGM();
            SoundManager.Instance.PlaySE("clear");
        }

        SceneManager.LoadScene("GameClearScene");
    }

    public void CollectItem()
    {
        itemCount++;
        score += scorePerItem;
        Debug.Log("スコア" + score + "アイテム取得: " + itemCount + " / " + requiredItemCount);

        if (itemCount >= requiredItemCount)
        {
            int bonus = Mathf.CeilToInt(remainingTime) * timeBonus;
            score += bonus;
            GameClear();
        }
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlaySE("item");
        }

        if (itemCount >= requiredItemCount)
        {
            GameClear();
        }
    }

    public int GetItemCount()
    {
        return itemCount;
    }

    public int GetRequiredItemCount()
    {
        return requiredItemCount;
    }
    public void AddScore(int points)
    {
        score += points;
    }
    public int GetScore()
    {
        return score;
    }
    public float GetRemainingTime()
    {
        return remainingTime;
    }
}