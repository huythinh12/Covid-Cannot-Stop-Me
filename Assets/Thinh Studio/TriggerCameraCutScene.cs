using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCameraCutScene : MonoBehaviour
{
    public GameObject panelBlendOut;
    // Start is called before the first frame update
    void Start()
    {
        print("xin chao");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TriggerPanel()
    {
        panelBlendOut.SetActive(true);
    }
}
