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
        private int currentResolutionIndex = 0;

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
                if (listResolutions[i].width == Screen.currentResolution.width &&
                    listResolutions[i].height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = i;
                }

            }
            resolutionDropdown.AddOptions(options);
            resolutionDropdown.value = currentResolutionIndex;
            resolutionDropdown.RefreshShownValue();

        }
        
        public void SetResolution(int resolutionIndex)
        {
            Resolution resolution = listResolutions[resolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        }


    }
}