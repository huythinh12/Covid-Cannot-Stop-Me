using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public GameObject bullet;
    private GameObject bulletSave;

    private void Update()
    {
        CheckConditionToCreateNewBall();
    }

    private void CheckConditionToCreateNewBall()
    {
        if (bulletSave == null)
        {
            bulletSave = Instantiate(bullet);
        }
        else if (bulletSave != null)
        {
            bulletSave.SetActive(true);
        }
    }
    

    private void OnDisable()
    {
        if(bulletSave!=null)
            bulletSave.SetActive(false);
    }
}