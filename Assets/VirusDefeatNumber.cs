using System;
using UnityEngine;
using TMPro;
public class VirusDefeatNumber : MonoBehaviour
{
    public static int virusNumberDefeat;
    private TMP_Text textNumberVirusDefeat;

    private void Start()
    {
        textNumberVirusDefeat = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        textNumberVirusDefeat.text = virusNumberDefeat.ToString();
    }
}
