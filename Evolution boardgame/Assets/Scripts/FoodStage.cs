using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodStage : MonoBehaviour, IGameState
{
    [SerializeField]
    GameStateContext gameStateContext;
    [SerializeField]
    FightingStage fightingStage;
    public void ChangeState()
    {
        gameStateContext.SetStage(fightingStage);
        Debug.Log("Перешел в игровую фазу");
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
