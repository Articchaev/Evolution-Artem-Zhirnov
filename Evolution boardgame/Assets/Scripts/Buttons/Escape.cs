using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Escape : MonoBehaviour
{
    [SerializeField]
    Button escapebutton;
    private void escape()
    {
        Application.Quit();
    }
    void Start()
    {
        escapebutton.onClick.AddListener(escape);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
