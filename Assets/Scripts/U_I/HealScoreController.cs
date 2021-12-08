using UnityEngine;
using TMPro;

public class HealScoreController : MonoBehaviour
{
    private TMP_Text textNumberHeal;

    private void Start()
    {
        textNumberHeal = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        if (GameManager.Instance != null)
            textNumberHeal.text = GameManager.Instance.countOfHeal.ToString();
    }
}