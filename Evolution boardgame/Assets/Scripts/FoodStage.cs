using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodStage : MonoBehaviour, IGameState
{
    [SerializeField]
    GameStateContext gameStateContext;
    [SerializeField]
    FightingStage fightingStage;
    [SerializeField]
    Image imagefight;
    [SerializeField]
    Image imagefood;
    [SerializeField]
    Sprite fightactive;
    [SerializeField]
    Sprite foodinactive;
    public void ChangeState()
    {
        gameStateContext.SetStage(fightingStage);
        Debug.Log("Перешел в игровую фазу");
        imagefight.sprite = fightactive;
        imagefood.sprite = foodinactive;
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
