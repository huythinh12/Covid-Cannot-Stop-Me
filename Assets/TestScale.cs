using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TestScale : MonoBehaviour
{
    public Texture m_MainTexture, m_Normal, m_Metal;
    Renderer m_Renderer;
    // Start is called before the first frame update
    void Start()
    {
        m_Renderer = GetComponent<Renderer> ();
        
        m_Renderer.material.SetTexture("_MainTex", m_MainTexture);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
