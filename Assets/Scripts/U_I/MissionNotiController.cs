using UnityEngine;

public class MissionNotiController : MonoBehaviour
{
    public GameObject completed, fail;

    private bool isTurnOn;
    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance != null)
        {
            if (GameManager.Instance.isClearAllStage)
                completed.SetActive(true);
            else if (GameManager.Instance.isFail && !isTurnOn)
            {
                isTurnOn = true;
                fail.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }
}