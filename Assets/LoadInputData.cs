using UnityEngine;
using TMPro;
public class LoadInputData : MonoBehaviour
{
    private TMP_Text txtInputFieldNamePlayer;
    private void Start()
    {
        var txtInputFieldNamePlayer = GetComponent<TMP_InputField>();
        txtInputFieldNamePlayer.text = PlayerPrefs.GetString("PlayerName");
    }
}
