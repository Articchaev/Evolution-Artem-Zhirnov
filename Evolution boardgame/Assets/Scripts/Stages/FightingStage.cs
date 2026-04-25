using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    [SerializeField]
    Botik botik;
    [SerializeField]
    TableBox EnemyTable;
    public void ChangeState()
    {
        for (int i = 0; i < EnemyTable.Creatures.Count(); i++)
        {
            botik.botikuseability();
        }
        for (int i = 0; i < foodStage.Food.Count; i++)
        {
            botik.PlayBotikFood();
        }
        foreach(RedFood Redfood in foodStage.Food)
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
