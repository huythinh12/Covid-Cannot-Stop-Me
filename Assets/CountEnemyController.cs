using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountEnemyController : MonoBehaviour
{
    // private VirusHealth[] virus;
    //
    // [SerializeField] private TMP_Text txtNumberVirus;
    //
    // // Start is called before the first frame update
    // void Start()
    // {
    //     virus = FindObjectsOfType<VirusHealth>();
    // }
    //
    // // Update is called once per frame
    // void Update()
    // {
    //     if (GameManager.Instance != null)
    //     {
    //         var virus = FindObjectsOfType<VirusHealth>();
    //         txtNumberVirus.text = virus.Length.ToString();
    //         
    //         if (virus.Length <= 0 )
    //         {
    //             if (!PlayerHealth.isEmptyHP)
    //             {
    //                 GameManager.Instance.isWin = true;
    //                 
    //             }
    //             else
    //             {
    //                 GameManager.Instance.isFail = true;
    //             }
    //         }
    //         else if(virus.Length >0 && GameManager.Instance.isEndTime)
    //         {
    //             GameManager.Instance.isFail = true;
    //         }
    //     }
    // }
}