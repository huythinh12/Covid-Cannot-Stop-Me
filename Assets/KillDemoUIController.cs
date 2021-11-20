using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class KillDemoUIController : MonoBehaviour
{
    private TMP_Text txtKill;
    // Start is called before the first frame update
    void Start()
    {
        txtKill = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        var virus = FindObjectsOfType<VirusHealth>();
        txtKill.text = virus.Length.ToString();
    }
}
