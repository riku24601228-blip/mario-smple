using UnityEngine;
using TMPro;
public class GameUI : MonoBehaviour
{
    private TextMeshProUGUI itemCountText;

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
        if (itemCountText != null && GameManager.Instance != null)
        {
            int current = GameManager.Instance.GetItemCount();
            int required = GameManager.Instance.GetRequiredItemCount();
            itemCountText.text = "ITEMS: " + current + " / " + required;
        }
    }
}
