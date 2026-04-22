using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    [SerializeField]
    deck TableDeck;
    [SerializeField]
    TableBox BotikTable;
    [SerializeField]
    Botikhand Cardbotikhand;
    [SerializeField]
    CarnivorousExecutor executor;
    [SerializeField]
    List<Card> CardHiber = new List<Card>();
    public void ChangeState()
    {
        if (TableDeck.currentcards == 0)
        {
            DieCreatures();
            foreach (Card i in CardHiber)
            {
                GameObject.Destroy(i.gameObject);
                YourTable.Creatures.Remove(i);
                BotikTable.Creatures.Remove(i);
            }
            CardHiber.Clear();
            if (YourTable.Creatures.Count >= BotikTable.Creatures.Count)
            {

                SceneManager.LoadScene(2);
            }
            else
            {
                SceneManager.LoadScene(3);
            }
        }
        DieCreatures();
        foreach (Card i in CardHiber)
        {
            if (i.hibernationabilka)
            {
                i.hibernationabilka = false;
            }
            else
            {
                GameObject.Destroy(i.gameObject);
                YourTable.Creatures.Remove(i);
                BotikTable.Creatures.Remove(i);
            }
        }
        CardHiber.Clear();
       
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
        if (BotikTable.Creatures.Count == 0)
        {
            for (int i = 0; i < 6; i++)
            {
                Cardbotikhand.AddCard();
            }
        }
        else
        {
            for (int i = 0; i < BotikTable.Creatures.Count + 1; i++)
            {
                Cardbotikhand.AddCard();
            }
        }
        
        gameStateContext.SetStage(evolutionStage);
        Debug.Log("Перешел в фазу развития");
        imageevolution.sprite = evolutionactive;
        imagedeath.sprite = deathinactive;
        table.colider.enabled = true;
        executor.CarnivorousCreatures.Clear();
    }
    public void DieCreatures()
    {
        List<Card> CardDel = new List<Card>();
        foreach (Card i in YourTable.Creatures)
        {
            if (i.HaveFood < i.Needfood)
            {
                if (i.abilky.FirstOrDefault(card => card.config.mainability is Hibernation && card.cardstage == 1) != null)
                {
                    CardHiber.Add(i);
                }
                else
                {
                    CardDel.Add(i);
                }
            }
            if (i.abilky.FirstOrDefault(card => card.config.mainability is Hibernation && card.cardstage == 1) != null && CardHiber.Contains(i) == false)
            {
                i.hibernationabilka = true;
            }
            i.HaveFood = 0;
            foreach (FoodBlock j in i.foodBlocks)
            {
                GameObject.Destroy(j.gameObject);
            }
            i.foodBlocks.Clear();
        }
        foreach (Card i in BotikTable.Creatures)
        {
            if (i.HaveFood < i.Needfood)
            {
                CardDel.Add(i);
            }
            i.HaveFood = 0;
            foreach (FoodBlock j in i.foodBlocks)
            {
                GameObject.Destroy(j.gameObject);
            }
            i.foodBlocks.Clear();
        }
        foreach (Card i in executor.PoisonedCreatures)
        {
            GameObject.Destroy(i.gameObject);
            YourTable.Creatures.Remove(i);
            BotikTable.Creatures.Remove(i);
        }
        foreach (Card i in CardDel)
        {
            GameObject.Destroy(i.gameObject);
            YourTable.Creatures.Remove(i);
            BotikTable.Creatures.Remove(i);
        }
        CardDel.Clear();
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
