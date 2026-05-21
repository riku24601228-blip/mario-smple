using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class GameUI : MonoBehaviour
{
    private TextMeshProUGUI itemCountText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;

    void Start()
    {
        if (itemCountText == null)
        {
            itemCountText = GameObject.Find("ItemCountText")?.GetComponent<TextMeshProUGUI>();
        }

        UpdateUI();
    }

    void Update()
    {
        UpdateUI();
    }
    private void UpdateUI()
    {
        if (GameManager.Instance == null) return;

        if (itemCountText != null)
        {
            itemCountText.text = "ITEMS: " +
                GameManager.Instance.GetItemCount() + " / " +
                GameManager.Instance.GetRequiredItemCount();
        }

        if (scoreText != null)
        {
            scoreText.text = "SCORE: " +
                GameManager.Instance.GetScore();
        }

        if (timerText != null)
        {

            int timeInt = Mathf.CeilToInt(
                GameManager.Instance.GetRemainingTime());
            timerText.text = "TIME: " + timeInt;


            if (timeInt <= 10)
            {
                timerText.color = Color.red;
            }
            else
            {
                timerText.color = Color.white;
            }
        }
    }
}
