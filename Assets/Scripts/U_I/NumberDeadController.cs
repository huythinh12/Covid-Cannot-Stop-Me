using UnityEngine;
using TMPro;
public class NumberDeadController : MonoBehaviour
{
    private TMP_Text textNumberDead;

    private void Start()
    {
        textNumberDead = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance != null)
        {
            textNumberDead.text = GameManager.Instance.countOfDead.ToString();
        }
    }
}
