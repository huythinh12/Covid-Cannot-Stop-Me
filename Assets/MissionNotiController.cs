using UnityEngine;

public class MissionNotiController : MonoBehaviour
{
    public GameObject completed, fail;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance != null)
        {
            if (GameManager.Instance.isWin)
                completed.SetActive(true);
            else if (GameManager.Instance.isFail)
                fail.SetActive(true);
        }
    }
}