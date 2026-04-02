using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStateContext : MonoBehaviour
{
    [SerializeField]
    Button NextStageButton;
    [SerializeField]
    EvolutionStage evolutionStage;
    public IGameState nowstate;
    public void ChangeState()
    {
        nowstate.ChangeState();
    }
    public void SetStage(IGameState gameState)
    {
        nowstate = gameState;
    }
    void Start()
    {
        NextStageButton.onClick.AddListener(ChangeState);
        nowstate = evolutionStage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
