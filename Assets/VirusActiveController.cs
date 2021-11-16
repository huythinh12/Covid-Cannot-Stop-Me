using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;
using UnityEngine.UI;

[DefaultExecutionOrder(400)]
public class VirusActiveController : MonoBehaviour
{
    // dùng tạm vì cái này phải thay bằng timer từ gamemanager
    public float timeActive;
    public ParticleSystem particleActive;
    public Texture m_MainTexture;
    public bool isActiveVirus; 
    
    //demo 
    public Text txt;
    private Renderer m_Renderer;
    private Animator anim;
    private BehaviorTree behaviorTree;
    private float posYBodyOrigin = 1.24737f;
    private NavMeshAgent agent;
    private Transform bodyRoot;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        bodyRoot = transform.GetChild(1);
        behaviorTree = GetComponent<BehaviorTree>();
        anim = GetComponent<Animator>();
        m_Renderer = GetComponentInChildren<SkinnedMeshRenderer>();
        m_Renderer.material.SetTexture("_MainTex", null);

        StartCoroutine(ActiveInTime());
    }

    private IEnumerator ActiveInTime()
    {
        while (timeActive >= 0)
        {
            txt.text = timeActive.ToString();
            timeActive--;
            yield return new WaitForSeconds(1);
        }
        float timeAction = 2;
        bodyRoot.DOMoveY(posYBodyOrigin, timeAction);
        
        yield return new WaitForSeconds(timeAction);
        var particle = Instantiate(particleActive,transform.position + new Vector3(0,2,0), Quaternion.identity);
        
        yield return null;
        if (!GetComponent<VirusDetectCollider>().isDestroyUnActiveVirus)
        {
            isActiveVirus = true;
            Destroy(particle,1f);
            agent.enabled = true;
            behaviorTree.enabled = true;
            anim.enabled = true;
            m_Renderer.material.SetTexture("_MainTex", m_MainTexture);
        }    
       
    }
}