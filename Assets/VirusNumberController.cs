using UnityEngine;
using TMPro;

public class VirusNumberController : MonoBehaviour
{
    private TMP_Text textNumberInfected;

    // Start is called before the first frame update
    void Start()
    {
        textNumberInfected = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance != null)
            textNumberInfected.text = GameManager.Instance.listInfected.Count.ToString();
    }
}