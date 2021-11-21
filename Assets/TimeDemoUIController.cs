using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  TMPro;
public class TimeDemoUIController : MonoBehaviour
{
    private TMP_Text time;

    public GameObject missionFail;
    // Start is called before the first frame update
    void Start()
    {
        time = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance != null)
        {
            if (GameManager.Instance.isCameraReadyInGame)
            {
                time.text = GameManager.Instance.timeCountDown.ToString();
            }

            if (GameManager.Instance.isEndTime)
            {
                missionFail.SetActive(true);
            }
        }
    }
}