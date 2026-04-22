using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Xml;
using UnityEngine;
using UnityEngine.XR;

public class Botik : MonoBehaviour
{
    [SerializeField]
    FoodStage foodstage;
    [SerializeField]
    TableBox botiktable;
    [SerializeField]
    Botikhand botikhand;
    [SerializeField]
    CarnivorousExecutor executor;
    [SerializeField]
    BlueFood bluefood;
    [SerializeField]
    TableBox YourTable;
    [SerializeField]
    TableBox EnemyTable;
    public void PlayBotikFood()
    {
        if (botiktable.Creatures.Count <=0 || foodstage.Food.Count <= 0 || botiktable.Creatures.Where(creature => creature.HaveFood < creature.Needfood).Count() <= 0)
        {
            return;
        }
        RedFood food = foodstage.Food[0];
        Card card = botiktable.Creatures.Where(creature => creature.HaveFood<creature.Needfood).ToList()[Random.Range(0, botiktable.Creatures.Count - 1)];
        food.clearSubs();
        food.onCard = true;
        card.HaveFood += 1;
        food.transform.SetParent(card.transform);
        card.foodBlocks.Add(food);
        RedFood a = food;
        foodstage.Food.Remove(food);
        a.deactivatefood();
    }
    public void botikuseability()
    {
        foreach (Card i in botiktable.Creatures)
        {
            if (i.abilky.FirstOrDefault(card2 => card2.config.dopability is Carnivorous && card2.cardstage == 2) != null)
            {
                if (executor.CarnivorousCreatures.Contains(i) || i.HaveFood >= i.Needfood)
                {
                    continue;
                }
                PlayBotikcarnivorous(i);
                return;
            }
        }
        PlayBotikFood();
    }
    public void PlayBotikcarnivorous(Card creature)
    {
        Card targetcreature = null;
        if (cantarget(creature).Count() == 0)
        {
            return;
        }
        targetcreature = cantarget(creature)[Random.Range(0, cantarget(creature).Count)];
        executor.CarnivorousCreatures.Add(creature);
        if (targetcreature.abilky.FirstOrDefault(card => card.config.mainability is Running && card.cardstage == 1) != null)
        {
            int k = Random.Range(0, 2);
            if (k == 0)
            {
                return;
            }
        }
        creature.foodBlocks.Add(GameObject.Instantiate(bluefood, creature.transform));
        creature.HaveFood += 1;
        if (targetcreature.abilky.FirstOrDefault(card => card.config.mainability is Poisonous && card.cardstage == 1) != null)
        {
            executor.PoisonedCreatures.Add(creature);
        }
        if (creature.HaveFood < creature.Needfood)
        {
            creature.foodBlocks.Add(GameObject.Instantiate(bluefood, creature.transform));
            creature.HaveFood += 1;
        }
        GameObject.Destroy(targetcreature.gameObject);
        YourTable.Creatures.Remove(targetcreature);
        Card ScavengerCreature = EnemyTable.Creatures.FirstOrDefault(card => card.abilky.FirstOrDefault(card2 => card2.config.mainability is Scavenger && card2.cardstage == 1) != null);
        if (ScavengerCreature == null)
        {
            ScavengerCreature = YourTable.Creatures.FirstOrDefault(card => card.abilky.FirstOrDefault(card2 => card2.config.mainability is Scavenger && card2.cardstage == 1) != null);
        }
        if (ScavengerCreature != null)
        {
            ScavengerCreature.foodBlocks.Add(GameObject.Instantiate(bluefood, ScavengerCreature.transform));
            ScavengerCreature.HaveFood += 1;
        }
    }
    public List<Card> cantarget(Card atackcreature)
    {
        List<Card> targets = new List<Card>();
        foreach (Card card in YourTable.Creatures)
        {
            if (atackcreature.abilky.FirstOrDefault(card => card.config.mainability is Swimming && card.cardstage == 1) != null
                    && card.abilky.FirstOrDefault(card => card.config.mainability is Swimming && card.cardstage == 1) == null
                    || atackcreature.abilky.FirstOrDefault(card => card.config.mainability is Swimming && card.cardstage == 1) == null
                    && card.abilky.FirstOrDefault(card => card.config.mainability is Swimming && card.cardstage == 1) != null)
            {
                continue;
            }
            if (atackcreature.abilky.FirstOrDefault(card => card.config.mainability is HighBodyWeight && card.cardstage == 1) == null
                    && card.abilky.FirstOrDefault(card => card.config.mainability is HighBodyWeight && card.cardstage == 1) != null)
            {
                continue;
            }
            if (atackcreature.abilky.FirstOrDefault(card => card.config.mainability is SharpVision && card.cardstage == 1) == null
                    && card.abilky.FirstOrDefault(card => card.config.mainability is Camo && card.cardstage == 1) != null)
            {
                continue;
            }
            if (card.abilky.FirstOrDefault(card => card.config.mainability is Burrowing && card.cardstage == 1) != null
                    && card.HaveFood >= card.Needfood)
            {
                continue;
            }
            targets.Add(card);
        }
        return targets;
    }
    public void PlayBotikCard()
    {
        if (botikhand.cards.Count <= 0)
        {
            return;
        }
        Card card = botikhand.cards[Random.Range(0, botikhand.cards.Count - 1)];
        card.Needfood = 1;
        card.Ontable = true;
        card.transform.SetParent(botiktable.transform);
        botiktable.Creatures.Add(card);
        card.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        botikhand.cards.Remove(card);
        botikhand.LayoutInstant();
    }
    public void PlayBotikability()
    {
        bool playdopability = false;
        Card cardtoplay = null;
        Card creatureonplay = null;
        foreach (Card card in botikhand.cards)
        {
            foreach (Card creature in botiktable.Creatures)
            {
                if (card.config.dopability != null && card.config.dopability is Carnivorous && creature.abilky.FirstOrDefault(card2 => card2.config.dopability is Carnivorous && card2.cardstage == 2) == null)
                {
                    if (creature.abilky.FirstOrDefault(card => card.config.mainability is Scavenger && card.cardstage == 1))
                    {
                        continue;
                    }
                    playdopability = true;
                    cardtoplay = card;
                    creatureonplay = creature;
                    break;
                }
                if (creature.abilky.FirstOrDefault(card2 => card2.config.mainability.GetType() == card.config.mainability.GetType() && card2.cardstage == 1) == null)
                {
                    if (creature.abilky.FirstOrDefault(card => card.config.dopability is Carnivorous && card.cardstage == 2) && card.config.mainability is Scavenger)
                    {
                        continue;
                    }
                    playdopability = false;
                    cardtoplay = card;
                    creatureonplay = creature;
                    break;
                }
                if (card.config.dopability != null && card.config.dopability is FatTissue)
                {
                    playdopability = true;
                    cardtoplay = card;
                    creatureonplay = creature;
                    break;
                }
            }

        }
        if(cardtoplay == null)
        {
            PlayBotikCard();
            return;
        }
        if (playdopability)
        {
            cardtoplay.BotikTurn(2);
            cardtoplay.clearSubs();
            cardtoplay.Needfood = 0;
            cardtoplay.OnCard = true;
            cardtoplay.transform.SetParent(creatureonplay.transform);
            creatureonplay.abilky.Add(cardtoplay);
            botikhand.cards.Remove(cardtoplay);
            botikhand.LayoutInstant();
            cardtoplay.transform.localScale = new Vector3(1, 1, 1);
            cardtoplay.transform.localEulerAngles = new Vector3(180, 0, 0);
            cardtoplay.transform.localPosition = new Vector3(0, creatureonplay.abilky.Count * cardtoplay.otstyp, 0);
            cardtoplay.SortingGroup.sortingOrder = -creatureonplay.abilky.Count;
            cardtoplay.colider.enabled = false;
            cardtoplay.config.dopability.OnAbilkaPlay(creatureonplay);
        }
        else
        {
            cardtoplay.BotikTurn(1);
            cardtoplay.clearSubs();
            cardtoplay.Needfood = 0;
            cardtoplay.OnCard = true;
            cardtoplay.transform.SetParent(creatureonplay.transform);
            creatureonplay.abilky.Add(cardtoplay);
            botikhand.cards.Remove(cardtoplay);
            botikhand.LayoutInstant();
            cardtoplay.transform.localScale = new Vector3(1, 1, 1);
            cardtoplay.transform.localEulerAngles = new Vector3(0, 0, 0);
            cardtoplay.transform.localPosition = new Vector3(0, creatureonplay.abilky.Count * cardtoplay.otstyp, 0);
            cardtoplay.SortingGroup.sortingOrder = -creatureonplay.abilky.Count;
            cardtoplay.colider.enabled = false;
            cardtoplay.config.mainability.OnAbilkaPlay(creatureonplay);
        }

    }
    public void botikdosmth()
    {
        if (botiktable.Creatures.Count < 2)
        {
            PlayBotikCard();
            return;
        }
        else if (botiktable.Creatures.Select(creature => creature.abilky.Count()).Sum() <= 3)
        {
            PlayBotikability();
        }
        else if (botiktable.Creatures.Count() == 3 && botiktable.Creatures.Select(creature => creature.abilky.Count()).Sum() <= 6)
        {
            PlayBotikability();
        }
        else if (botiktable.Creatures.Count() == 3 && botiktable.Creatures.Select(creature => creature.abilky.Count()).Sum() >= 6)
        {
            PlayBotikCard();
        }
        else
        {
            PlayBotikability();
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
