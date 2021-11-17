using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionsFX : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("RemoveDelay",2f);
    }
    private void RemoveDelay()
    {
        Destroy(gameObject);
    }
}
