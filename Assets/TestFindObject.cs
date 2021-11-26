using UnityEngine;

public class TestFindObject : MonoBehaviour
{
    public GameObject npc;

    public Transform[] pointToSpawn;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn",2,2);
    
    }

    private void Spawn()
    {
        var rdPoint = Random.Range(0, pointToSpawn.Length);
        if (pointToSpawn[rdPoint].childCount <= 0)
        {
            var newNPc = Instantiate(npc, pointToSpawn[rdPoint].position, Quaternion.identity,pointToSpawn[rdPoint].transform);
            var mainBody = newNPc.transform.GetChild(0);
            //set mask true
            mainBody.transform.GetChild(2).gameObject.SetActive(true);
            //set icon false
            mainBody.transform.GetChild(4).gameObject.SetActive(false);
            //set default with npc ally spawn 
            mainBody.gameObject.layer = LayerMask.NameToLayer("Default");
            mainBody.transform.GetChild(0).gameObject.SetActive(false);
            mainBody.transform.GetChild(1).gameObject.SetActive(true);
            mainBody.GetComponent<NPCDetectVirus>().enabled = false;
            mainBody.GetComponent<NPCPatrolRandom>().enabled = false;
        }
    
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
