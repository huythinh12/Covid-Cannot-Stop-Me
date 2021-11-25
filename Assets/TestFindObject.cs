using System.Collections.Generic;
using UnityEngine;

public class TestFindObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var list = new List<string>();
        list.Add("A A");
        list.Add("B+s");
        list.Add("a A");
        if (list.Contains("s"))
        {
            print("co ton tai ");
        }
        else
        {
            print("khong ton tai ");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
