using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NPCDetectVirus))]
[RequireComponent(typeof(NPCDetectInZone))]
public class NPCState : MonoBehaviour
{
    public int currentHealth;
    public int countInject;
    public int maxHealth = 2;
    public bool isVirusInside;
    public bool isDef;
    private bool isTurnOn;
    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (currentHealth <= 0 && !isTurnOn)
        {
            isTurnOn = true;
            UnactiveComponent();
            Destroy(gameObject,3);
        }

        //test
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentHealth--;
            isVirusInside = true;
        }
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
