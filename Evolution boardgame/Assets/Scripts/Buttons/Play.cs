using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Play : MonoBehaviour
{
    [SerializeField]
    Button PlayButton;
    private void play()
    {
        SceneManager.LoadScene(1);
    }
    void Start()
    {
        PlayButton.onClick.AddListener(play);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
