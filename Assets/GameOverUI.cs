using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
public class GameOverUI : MonoBehaviour
{
    private TextMeshProUGUI gameOverText;

    private TextMeshProUGUI pressSpaceText;

    void Start()
    {
        if (gameOverText == null)
        {
            gameOverText = GameObject.Find("GameOverText")?.GetComponent<TextMeshProUGUI>();
        }

        if (pressSpaceText == null)
        {
            pressSpaceText = GameObject.Find("PressSpaceText")?.GetComponent<TextMeshProUGUI>();
        }

        if (gameOverText != null)
        {
            gameOverText.text = "GAME OVER";
        }

        if (pressSpaceText != null)
        {
            pressSpaceText.text = "PRESS SPACE TO TITLE";
        }
    }

    void Update()
    {
        if (pressSpaceText != null)
        {
            float alpha = Mathf.Abs(Mathf.Sin(Time.time * 2f));
            Color color = pressSpaceText.color;
            color.a = alpha;
            pressSpaceText.color = color;
        }

        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.ReturnToTitle();
            }
        }
    }
}
