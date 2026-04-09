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
    [SerializeField]
    hand Cardhand;
    [SerializeField]
    TableBox YourTable;
    [SerializeField]
    Table table;
    public void ChangeState()
    {
        List<Card> CardDel = new List<Card>();
        foreach (Card i in YourTable.Creatures)
        {
            if (i.HaveFood < i.Needfood)
            {
                CardDel.Add(i);
            }
        }
        foreach (Card i in CardDel)
        {
            GameObject.Destroy(i.gameObject);
            YourTable.Creatures.Remove(i);
        }
        CardDel.Clear();
        if (YourTable.Creatures.Count == 0)
        {
            for (int i = 0; i < 6; i++)
            {
                Cardhand.AddCard();
            }
        }
        else
        {
            for (int i = 0; i < YourTable.Creatures.Count + 1; i++)
            {
                Cardhand.AddCard();
            }
        }
        gameStateContext.SetStage(evolutionStage);
        Debug.Log("Перешел в фазу развития");
        imageevolution.sprite = evolutionactive;
        imagedeath.sprite = deathinactive;
        table.colider.enabled = true;
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
