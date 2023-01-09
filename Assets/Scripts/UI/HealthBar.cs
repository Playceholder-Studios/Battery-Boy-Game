using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public RectTransform rectTransform;
    public float m_maxWidth;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        m_maxWidth = rectTransform.rect.xMax;
    }

    private void Update()
    {
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        float playerHealthPercentage = (float)GameManager.Instance.PlayerController.playerHealth.CurrentHealth / GameManager.Instance.PlayerController.playerHealth.MaxHealth;
        rectTransform.sizeDelta = new Vector2(m_maxWidth * playerHealthPercentage, rectTransform.rect.height);
    }
}
