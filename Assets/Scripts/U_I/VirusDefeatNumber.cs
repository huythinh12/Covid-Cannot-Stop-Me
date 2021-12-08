using UnityEngine;
using TMPro;
public class VirusDefeatNumber : MonoBehaviour
{
    private TMP_Text textNumberVirusDefeat;

    private void Start()
    {
        textNumberVirusDefeat = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        textNumberVirusDefeat.text = Mission3Controller.numberVirusDefeat.ToString();
    }
}
