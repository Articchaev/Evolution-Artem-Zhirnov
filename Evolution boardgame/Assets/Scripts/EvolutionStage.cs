using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvolutionStage : MonoBehaviour, IGameState
{
    [SerializeField]
    GameStateContext gameStateContext;
    [SerializeField]
    FoodStage foodStage;
    public void ChangeState()
    {
        gameStateContext.SetStage(foodStage);
        Debug.Log("Перешел в фазу определения кормовой базы");
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
