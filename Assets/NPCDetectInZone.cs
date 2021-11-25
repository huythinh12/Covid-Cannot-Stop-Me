using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class NPCDetectInZone : MonoBehaviour
{
    private Stopwatch loadTimer;
    public Slider shield1, shield2;
    public GameObject notiInzone;
    public GameObject warning;
    private int currentState;
    private int loadTimeEnd = 5;

    void Start()
    {
        shield1.maxValue = loadTimeEnd;
        shield2.maxValue = loadTimeEnd;
        loadTimer = new Stopwatch();
    }

    // Update is called once per frame
    void Update()
    {
        //if shield2 not yet complete
        if (shield2.value < shield2.maxValue)
        {
            if (shield1.value >= shield1.maxValue)
                GetComponent<NPCState>().countInject = 1;

            if (loadTimer != null && loadTimer.IsRunning)
            {
                if (shield1.value < shield1.maxValue)
                {
                    shield1.value = (float) loadTimer.Elapsed.TotalSeconds;
                }
                else
                {
                    shield2.value = (float) loadTimer.Elapsed.TotalSeconds;
                }

                //stop hold mouse 
                if (Input.GetMouseButtonUp(0))
                {
                    loadTimer.Stop();
                    if (shield1.value < shield1.maxValue)
                        shield1.value = 0;
                    if (shield2.value < shield2.maxValue)
                        shield2.value = 0;
                }

                //hold mouse enough 
                if (loadTimer.Elapsed.TotalSeconds >= loadTimeEnd)
                {
                    loadTimer.Stop();
                    if (currentState < 2)
                        currentState++;
                }
            }
        }
        else
        {
            GetComponent<NPCState>().countInject = 2;
            //do something when done with gamemanager
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("ZoneToCastSkill"))
        {
            var state = GetComponent<NPCState>();

            notiInzone.SetActive(true);
            if (!Input.GetMouseButton(0))
            {
                warning.SetActive(false);
                shield1.gameObject.SetActive(false);
                shield2.gameObject.SetActive(false);
            }
            else
            {
                notiInzone.SetActive(false);
            }

            //healing
            if (Input.GetKeyDown(KeyCode.F))
            {
                state.currentHealth = state.maxHealth;
                state.isVirusInside = false;
            }

            //lv 2
            if (Input.GetMouseButtonDown(0))
            {
                if (!state.isVirusInside)
                {
                    if (shield2.value < shield2.maxValue)
                    {
                        shield1.gameObject.SetActive(true);
                        shield2.gameObject.SetActive(true);

                        loadTimer.Reset();
                        loadTimer.Start();
                    }
                    else
                    {
                        shield2.gameObject.SetActive(true);
                    }
                }
                else
                {
                    warning.SetActive(true);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ZoneToCastSkill"))
        {
            notiInzone.SetActive(false);
            shield1.gameObject.SetActive(false);
            shield2.gameObject.SetActive(false);
        }
    }
}