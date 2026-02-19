using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class info : MonoBehaviour
{
    [SerializeField]
    GameObject infoc;
    [SerializeField]
    Button infobutton;
    void open()
    {
        if (infoc.activeSelf)
        {
            infoc.SetActive(false);
        }
        else
        {
            infoc.SetActive(true);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        infobutton.onClick.AddListener(open);
        infoc.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
