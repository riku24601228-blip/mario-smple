using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
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
    }

    public void StartGame()
    {
        itemCount = 0;
        CurrentState = GameState.Playing;
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
    }

    public void GameClear()
    {
        CurrentState = GameState.GameClear;
        SceneManager.LoadScene("GameClearScene");
    }

    public void CollectItem()
    {
        itemCount++;
        Debug.Log("アイテム取得: " + itemCount + " / " + requiredItemCount);

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
}