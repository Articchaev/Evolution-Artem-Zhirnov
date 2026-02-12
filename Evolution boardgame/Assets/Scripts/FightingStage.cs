using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightingStage : MonoBehaviour, IGameState
{
    [SerializeField]
    GameStateContext gameStateContext;
    [SerializeField]
    DeathStage deathStage;
    public void ChangeState()
    {
        gameStateContext.SetStage(deathStage);
        Debug.Log("Перешел в фазу вымирания");
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
