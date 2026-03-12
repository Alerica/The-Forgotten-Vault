using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private Image fillImage;

    private int maxHP;

    private void Start()
    {
        maxHP = playerHealth.CurrentHP;
    }

    private void Update()
    {
        fillImage.fillAmount =
            (float)playerHealth.CurrentHP / maxHP;
    }
}