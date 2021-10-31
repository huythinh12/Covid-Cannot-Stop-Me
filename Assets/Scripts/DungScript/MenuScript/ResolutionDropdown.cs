using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace Menu
{
    public class ResolutionDropdown : MonoBehaviour
    {
        [SerializeField]
        private TMPro.TMP_Dropdown resolutionDropdown;
        private Resolution[] listResolutions;


        private void Start()
        {
            ResolutionOption();
        }
        private void ResolutionOption()
        {
            listResolutions = Screen.resolutions;

            resolutionDropdown.ClearOptions();

            List<string> options = new List<string>();

            for (int i = 0; i < listResolutions.Length; i++)
            {
                string option = listResolutions[i].width + "x" + listResolutions[i].height;
                options.Add(option);
            }
            resolutionDropdown.AddOptions(options);
        }

    }
}