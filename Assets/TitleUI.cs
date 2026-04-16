using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class TitleUI : MonoBehaviour
{
    private TextMeshProUGUI titleText;

    private TextMeshProUGUI pressSpaceText;

    void Start()
    {

        if (titleText == null)
        {
            titleText = GameObject.Find("TitleText")?.GetComponent<TextMeshProUGUI>();
        }

        if (pressSpaceText == null)
        {
            pressSpaceText = GameObject.Find("PressSpaceText")?.GetComponent<TextMeshProUGUI>();
        }

        if (titleText != null)
        {
            titleText.text = "MARIO SAMPLE";
        }

        if (pressSpaceText != null)
        {
            pressSpaceText.text = "PRESS SPACE TO START";
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
                GameManager.Instance.StartGame();
            }
        }
    }
}
