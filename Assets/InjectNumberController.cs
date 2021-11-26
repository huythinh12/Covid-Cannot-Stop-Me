using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class InjectNumberController : MonoBehaviour
{
    private TMP_Text textNumberOfVaccine;

    private void Start()
    {
        textNumberOfVaccine = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        textNumberOfVaccine.text = Mission2Controller.numberVaccineInTown.ToString();
    }
}
