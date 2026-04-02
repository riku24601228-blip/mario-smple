using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.InputSystem;
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


    public void StartGame()
    {
        itemCount = 0;
        CurrentState = GameState.Playing;

    }


    public void ReturnToTitle()
    {
        itemCount = 0;
        CurrentState = GameState.Title;

    }

    public void GameOver()
    {
        CurrentState = GameState.GameOver;

    }

    public void GameClear()
    {
        CurrentState = GameState.GameClear;

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