using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathStage : MonoBehaviour, IGameState
{
    [SerializeField]
    GameStateContext gameStateContext;
    [SerializeField]
    EvolutionStage evolutionStage;
    [SerializeField]
    Image imageevolution;
    [SerializeField]
    Image imagedeath;
    [SerializeField]
    Sprite evolutionactive;
    [SerializeField]
    Sprite deathinactive;
    public void ChangeState()
    {
        gameStateContext.SetStage(evolutionStage);
        Debug.Log("Перешел в фазу развития");
        imageevolution.sprite = evolutionactive;
        imagedeath.sprite = deathinactive;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
