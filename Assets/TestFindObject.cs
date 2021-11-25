using UnityEngine;

public class TestFindObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<PlayerHealth>().gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
