using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class SpawnVirusController : MonoBehaviour
{
    public Transform[] pointsToSpawn;
    public int maxRange;
    public GameObject[] virus;
    public int timeToSpawn;
    private int numberOVirus;
    private bool isTurnOn;

    private void Update()
    {
        if (GameManager.Instance != null)
            if (GameManager.Instance.isCameraReadyInGame && !isTurnOn)
            {
                isTurnOn = true;
                StartCoroutine(StartSpawn());
            }
    }

    IEnumerator StartSpawn()
    {
        while (true)
        {
            numberOVirus = GameObject.FindGameObjectsWithTag("Virus").Length;
            if (numberOVirus >= maxRange)
            {
                var coroutineCheckNumberOfNPC = StartCoroutine(CheckNumberOfNPC());
                yield return new WaitUntil(() => numberOVirus < maxRange);
                StopCoroutine(coroutineCheckNumberOfNPC);
            }
            else
            {
                yield return new WaitForSeconds(timeToSpawn);
                SetRandomSpawn();
            }
        }
    }

    IEnumerator CheckNumberOfNPC()
    {
        while (true)
        {
            numberOVirus = GameObject.FindGameObjectsWithTag("Virus").Length;
            yield return null;
        }
    }

    private void SetRandomSpawn()
    {
        var indexPoint = Random.Range(0, pointsToSpawn.Length);
        var indexVirus = Random.Range(0, virus.Length);
        var newPos = pointsToSpawn[indexPoint].transform.position + Vector3.right * Random.Range(-4, 5) +
                     Vector3.forward * Random.Range(-4, 5);
        
        if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            Instantiate(virus[indexVirus], newPos, Quaternion.identity);
        }
        else
        {
            Instantiate(virus[0], newPos, Quaternion.identity);
        }
    }
}