using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FightingStage : MonoBehaviour, IGameState
{
    [SerializeField]
    GameStateContext gameStateContext;
    [SerializeField]
    DeathStage deathStage;
    [SerializeField]
    Image imagefight;
    [SerializeField]
    Image imagedeath;
    [SerializeField]
    Sprite fightinactive;
    [SerializeField]
    Sprite deathactive;
    [SerializeField]
    FoodStage foodStage;
    public void ChangeState()
    {
        foreach(GameObject Redfood in foodStage.Food)
        {
            GameObject.Destroy(Redfood.gameObject);
        }
        foodStage.Food.Clear();
        gameStateContext.SetStage(deathStage);
        Debug.Log("Перешел в фазу вымирания");
        imagefight.sprite = fightinactive;
        imagedeath.sprite = deathactive;
        gameStateContext.ChangeState();
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
