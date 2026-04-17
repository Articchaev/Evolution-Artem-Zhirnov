using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Lobby : MonoBehaviour
{
    [SerializeField]
    Button ReturnButton;
    private void Return()
    {
        SceneManager.LoadScene(0);
    }
    void Start()
    {
        ReturnButton.onClick.AddListener(Return);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
