using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class U10PS_DissolveOverTime : MonoBehaviour
{
    private SkinnedMeshRenderer skinnedMeshRenderer;

    public float speed = .5f;

    private void Start()
    {
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
    }

    private float t = 0.0f;

    private void Update()
    {
        // Material[] mats = meshRenderer.materials;
        Material[] mats1 = skinnedMeshRenderer.materials;

        mats1[0].SetFloat("_Cutoff", Mathf.Sin(t * speed));
        t += Time.deltaTime;
        // Unity does not allow meshRenderer.materials[0]...
        skinnedMeshRenderer.materials = mats1;
    }
}