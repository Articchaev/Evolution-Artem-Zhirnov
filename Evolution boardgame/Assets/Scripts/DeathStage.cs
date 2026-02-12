using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathStage : MonoBehaviour, IGameState
{
    [SerializeField]
    GameStateContext gameStateContext;
    [SerializeField]
    EvolutionStage evolutionStage;

    public void ChangeState()
    {
        gameStateContext.SetStage(evolutionStage);
        Debug.Log("Перешел в фазу развития");
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
