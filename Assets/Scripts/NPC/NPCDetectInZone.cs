using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NPCDetectInZone : MonoBehaviour
{
    [SerializeField] private ParticleSystem effectRefresh;
    public Slider shield1, shield2;
    public GameObject notiInzone;
    public Slider allyJoin;
    public GameObject warning;
    
    private Stopwatch loadTimer;
    private int currentState;
    private int loadTimeEnd = 5;
    private bool isTurnOn;
    void Start()
    {
        effectRefresh.Stop();
        shield1.maxValue = loadTimeEnd;
        shield2.maxValue = loadTimeEnd;
        loadTimer = new Stopwatch();
    }

    // Update is called once per frame
    void Update()
    {
        //mission 2
        //if shield2 not yet 
        if (SceneManager.GetActiveScene().buildIndex == 3)
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
                        if (allyJoin.value < allyJoin.maxValue)
                            allyJoin.value = 0;
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
                if (!isTurnOn)
                {
                    isTurnOn = true;
                    GetComponent<NPCState>().countInject = 2;
                    Mission2Controller.numberVaccineInTown++;
                }
            }
        //mission 3 
        if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            if (allyJoin.value >= allyJoin.maxValue)
            {
                allyJoin.gameObject.SetActive(false);
                notiInzone.SetActive(false);
                GetComponent<NPCState>().isAlly = true;
            
                return;
            }

            //stop hold mouse 
            if (loadTimer != null && loadTimer.IsRunning)
            {
                if (allyJoin.value < allyJoin.maxValue)
                    allyJoin.value = (float) loadTimer.Elapsed.TotalSeconds;
                if (Input.GetMouseButtonUp(0))
                {
                    loadTimer.Stop();
                    if (allyJoin.value < allyJoin.maxValue)
                        allyJoin.value = 0;
                }

                if (loadTimer.Elapsed.TotalSeconds >= loadTimeEnd)
                {
                    loadTimer.Stop();
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // lv > 1
        //healing
        if (SceneManager.GetActiveScene().buildIndex > 2)
        {
            var state = GetComponent<NPCState>();

            if (other.CompareTag("ZoneToCastSkill"))
            {
                //mission 2
                if (SceneManager.GetActiveScene().buildIndex == 3)
                {
                    if (!Input.GetMouseButton(0))
                    {
                        notiInzone.SetActive(true);
                        warning.SetActive(false);
                        shield1.gameObject.SetActive(false);
                        shield2.gameObject.SetActive(false);
                    }
                    else
                    {
                        notiInzone.SetActive(false);
                        shield1.gameObject.SetActive(true);
                        shield2.gameObject.SetActive(true);
                    }

                    if (Input.GetMouseButtonDown(0))
                    {
                        if (!state.isVirusInside)
                        {
                            if (shield2.value < shield2.maxValue)
                            {
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
                }//mission 3
                else if (SceneManager.GetActiveScene().buildIndex == 4)
                {
                    //do something
                    if (!state.isAlly)
                    {
                        if (!Input.GetMouseButton(0))
                        {
                            notiInzone.SetActive(true);
                            allyJoin.gameObject.SetActive(false);
                        }
                        else
                        {
                            notiInzone.SetActive(false);
                            allyJoin.gameObject.SetActive(true);
                        }

                        if (Input.GetMouseButtonDown(0))
                        {
                            loadTimer.Reset();
                            loadTimer.Start();
                        }
                    }
                }
                //refresh after virus inject 
                if (Input.GetKeyDown(KeyCode.F) && state.currentHealth > 0 && state.currentHealth <2)
                {
                    effectRefresh.Play();
                    state.currentHealth = state.maxHealth;
                    state.isVirusInside = false;
                    GameManager.Instance.countOfHeal++;
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