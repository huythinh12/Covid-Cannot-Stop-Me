using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NPCDetectVirus))]
[RequireComponent(typeof(NPCDetectInZone))]
public class NPCState : MonoBehaviour
{
    public bool isAlly;
    public int currentHealth;
    public int countInject;
    public int maxHealth = 2;
    public bool isVirusInside;
    public bool isDef;
    private bool isTurnOn;
    private bool isTurnOn2;
    public bool isRunning;
    private float speed = 2.8f;
    private float saveSpeed;
    private float maxSpeed = 3.5f;
    private NavMeshAgent agent;

    private void Start()
    {
        if (!isAlly)
        {
            agent = GetComponent<NavMeshAgent>();
            agent.speed = saveSpeed = speed;
            currentHealth = maxHealth;
        }
     
     
    }

    private void Update()
    {
        if (isAlly && !isTurnOn2)
        {
            isTurnOn2 = true;
            transform.name = "Ally";
            transform.tag = "Ally";
            transform.parent.parent = null;
            transform.GetChild(3).gameObject.SetActive(false);
            GetComponent<CapsuleCollider>().enabled = false;
            GetComponent<FollowTarget>().enabled = true;
            GetComponent<NavMeshAgent>().enabled = true;
            GetComponent<NPCDetectInZone>().enabled = false;
            GetComponent<NPCAnimationsController>().enabled = true;
            agent = GetComponent<NavMeshAgent>();
            agent.speed = saveSpeed = speed;
            currentHealth = maxHealth;
        }
        if (currentHealth <= 0 && !isTurnOn)
        {
            isTurnOn = true;
            GameManager.Instance.countOfDead++;
            UnactiveComponent();
            Destroy(gameObject, 3);
        }

        if (isVirusInside)
        {
            agent.speed = 2.5f;
        }
        else
        {
            if (isRunning)
                agent.speed = maxSpeed;
            else
                agent.speed = saveSpeed;
        }


        // //test
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     currentHealth--;
        //     isVirusInside = true;
        // }
    }

    private void UnactiveComponent()
    {
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<NPCPatrolRandom>().enabled = false;
        GetComponent<NPCDetectVirus>().enabled = false;
        GetComponent<NPCDetectInZone>().enabled = false;
    }
}