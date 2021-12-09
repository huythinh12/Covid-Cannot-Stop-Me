using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool isTurnOn;
    [SerializeField] private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        MonoBehaviour[] component = player.GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour c in component)
        {
            c.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance != null)
        {
            if (GameManager.Instance.isCameraReadyInGame && !isTurnOn)
            {
                isTurnOn = true;
                MonoBehaviour[] component = player.GetComponents<MonoBehaviour>();
                foreach (MonoBehaviour c in component)
                {
                    c.enabled = true;
                }
            }
        }
    }
}
