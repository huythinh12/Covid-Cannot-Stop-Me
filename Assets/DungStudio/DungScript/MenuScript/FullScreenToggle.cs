using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Menu
{
    public class FullScreenToggle : MonoBehaviour
    {
       public void SetFullScreen(bool isFullScreen)
        {
            Screen.fullScreen = isFullScreen;
        }
    }
}
