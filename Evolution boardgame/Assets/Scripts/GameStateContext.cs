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
    IGameState nowstate;
    // Start is called before the first frame update
    void ChangeState()
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
