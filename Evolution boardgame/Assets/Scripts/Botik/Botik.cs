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
    [SerializeField]
    PiracyExecutor executorpiracy;
    public bool botikusegrazing = false;
    public void PlayBotikFood()
    {
        if (botiktable.Creatures.Count <=0 || foodstage.Food.Count <= 0 || botiktable.Creatures.Where(creature => creature.HaveFood < creature.Needfood).Count() <= 0)
        {
            return;
        }
        FoodBlock food = foodstage.Food[0];
        Card card = botiktable.Creatures.Where(creature => (creature.HaveFood < creature.Needfood + creature.abilky.Count(card => card.config.dopability is FatTissue && card.cardstage == 2)) 
            && !(EnemyTable.Creatures.IndexOf(creature) != 0
            && creature.abilky.FirstOrDefault(card => card.config.mainability is Symbiosis && card.cardstage == 1) != null
            && EnemyTable.Creatures[EnemyTable.Creatures.IndexOf(creature) - 1].HaveFood < EnemyTable.Creatures[EnemyTable.Creatures.IndexOf(creature) - 1].Needfood))
            .ToList()[Random.Range(0, botiktable.Creatures.Count - 1)];
        if (card.abilky.FirstOrDefault(card => card.config.dopability is FatTissue && card.cardstage == 2) != null
            && card.HaveFood < card.Needfood + card.abilky.Count(card => card.config.dopability is FatTissue && card.cardstage == 2))
        {
            GameObject.Destroy(food.gameObject);
            foodstage.Food.Remove(food as RedFood);
            food = GameObject.Instantiate(card.yellowfood, gameObject.transform);
        }
        if (food is RedFood redfood)
        {
            redfood.clearSubs();
            redfood.onCard = true;
            foodstage.Food.Remove(redfood);
            redfood.deactivatefood();
        }

        food.transform.SetParent(card.transform);
        card.foodBlocks.Add(food);
        card.HaveFood += 1;
        card.UseCommunication();
        card.UseCooperation();
    }





    public void botikuseability()
    {
        botikusepiracy();
        if (!botikusegrazing)
        {
            foreach (Card card in EnemyTable.Creatures)
            {
                if (card.abilky.FirstOrDefault(card2 => card2.cardstage == 1 && card.config.mainability is Grazing) != null && foodstage.Food.Count() > 0)
                {
                    RedFood f = foodstage.Food[0];
                    GameObject.Destroy(f.gameObject);
                    foodstage.Food.Remove(f);
                }
            }
            botikusegrazing = true;
        }
        foreach (Card i in botiktable.Creatures)
        {
            if (i.abilky.FirstOrDefault(card2 => card2.config.dopability is Carnivorous && card2.cardstage == 2) != null)
            {
                if (executor.CarnivorousCreatures.Contains(i) || i.HaveFood >= i.Needfood + i.abilky.Count(card => card.config.dopability is FatTissue && card.cardstage == 2))
                {
                    continue;
                }
                if (PlayBotikcarnivorous(i))
                {
                    return;
                }
            }
        }
        PlayBotikFood();
    }





    public void botikusepiracy()
    {
        foreach (Card card in botiktable.Creatures)
        {
            if (card.abilky.FirstOrDefault(card2 => card2.config.mainability is Piracy && card2.cardstage == 1) == null)
            {
                continue;
            }
            if (executorpiracy.PiracyCreatures.Contains(card) == true)
            {
                continue;
            }
            Card a = (Card)YourTable.Creatures.Where(creature => creature.Needfood > creature.HaveFood && creature.HaveFood > 0);
            if (a == null)
            {
                continue;
            }
            FoodBlock f = a.foodBlocks[0];
            a.foodBlocks.Remove(f);
            f.transform.SetParent(card.transform);
            card.foodBlocks.Add(f);
            a.HaveFood -= 1;
            card.HaveFood += 1;
            executorpiracy.PiracyCreatures.Add(card);
        }
    }






    public bool PlayBotikcarnivorous(Card creature)
    {
        Card targetcreature = null;
        if (cantarget(creature).Count() == 0)
        {
            return false;
        }
        targetcreature = cantarget(creature)[Random.Range(0, cantarget(creature).Count)];
        if (targetcreature.abilky.FirstOrDefault(card => card.config.mainability is Mimicry &&  card.cardstage == 1) != null)
        {
            foreach (Card i in cantarget(creature))
            {
                if (i.abilky.FirstOrDefault(card => card.config.mainability is Mimicry && card.cardstage == 1) == null)
                {
                    targetcreature = i;
                    break;
                }
            }
        }
        
        executor.CarnivorousCreatures.Add(creature);
        if (creature.HaveFood < creature.Needfood)
        {
            if (targetcreature.abilky.FirstOrDefault(card => card.config.mainability is Running && card.cardstage == 1) != null)
            {
                int k = Random.Range(0, 2);
                if (k == 0)
                {
                    return true;
                }
            }
            creature.foodBlocks.Add(GameObject.Instantiate(bluefood, creature.transform));
            creature.HaveFood += 1;
            creature.UseCooperation();
        }
        else if (creature.abilky.FirstOrDefault(card => card.config.dopability is FatTissue && card.cardstage == 2) != null
            && creature.HaveFood < creature.Needfood + creature.abilky.Count(card => card.config.dopability is FatTissue && card.cardstage == 2))
        {
            if (targetcreature.abilky.FirstOrDefault(card => card.config.mainability is Running && card.cardstage == 1) != null)
            {
                int k = Random.Range(0, 2);
                if (k == 0)
                {
                    return true;
                }
            }
            creature.foodBlocks.Add(GameObject.Instantiate(creature.yellowfood, creature.transform));
            creature.HaveFood += 1;
            creature.UseCooperation();
        }
        if (targetcreature.abilky.FirstOrDefault(card => card.config.mainability is TailLoss && card.cardstage == 1) != null)
        {
            Card creature1 = targetcreature.abilky.FirstOrDefault(card => card.config.mainability is Parasite && card.cardstage == 1);
            if (creature1 == null)
            {
                creature1 = targetcreature.abilky[0];
            }
            targetcreature.abilky.Remove(creature1);
            GameObject.Destroy(creature1.gameObject);
            targetcreature.AbilkaRovno();

            return true;
        }
        if (targetcreature.abilky.FirstOrDefault(card => card.config.mainability is Poisonous && card.cardstage == 1) != null)
        {
            executor.PoisonedCreatures.Add(creature);
        }
        if (creature.HaveFood < creature.Needfood)
        {
            creature.foodBlocks.Add(GameObject.Instantiate(bluefood, creature.transform));
            creature.HaveFood += 1;
            creature.UseCooperation();
        }
        else if (creature.abilky.FirstOrDefault(card => card.config.dopability is FatTissue && card.cardstage == 2) != null
            && creature.HaveFood < creature.Needfood + creature.abilky.Count(card => card.config.dopability is FatTissue && card.cardstage == 2))
        {
            creature.foodBlocks.Add(GameObject.Instantiate(creature.yellowfood, creature.transform));
            creature.HaveFood += 1;
            creature.UseCooperation();
        }
        if (targetcreature != YourTable.Creatures.Last()
            && YourTable.Creatures[YourTable.Creatures.IndexOf(targetcreature) + 1].abilky.FirstOrDefault(card => card.config.mainability is Symbiosis && card.cardstage == 1) != null)
        {
            Card c = YourTable.Creatures[YourTable.Creatures.IndexOf(targetcreature) + 1];
            Card f = c.abilky.FirstOrDefault(card => card.config.mainability is Symbiosis && card.cardstage == 1);
            c.abilky.Remove(f);
            GameObject.Destroy(f.gameObject);
            c.AbilkaRovno();
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
            if (ScavengerCreature.HaveFood < ScavengerCreature.Needfood)
            {

                ScavengerCreature.foodBlocks.Add(GameObject.Instantiate(bluefood, ScavengerCreature.transform));
                ScavengerCreature.HaveFood += 1;
                ScavengerCreature.UseCooperation();
            }
            else if (ScavengerCreature.abilky.FirstOrDefault(card => card.config.dopability is FatTissue && card.cardstage == 2) != null
                && ScavengerCreature.HaveFood < ScavengerCreature.Needfood + ScavengerCreature.abilky.Count(card => card.config.dopability is FatTissue && card.cardstage == 2))
            {
                ScavengerCreature.foodBlocks.Add(GameObject.Instantiate(ScavengerCreature.yellowfood, ScavengerCreature.transform));
                ScavengerCreature.HaveFood += 1;
                ScavengerCreature.UseCooperation();
            }
        }

        return true;
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
            if (card.abilky.FirstOrDefault(card => card.config.mainability is Symbiosis && card.cardstage == 1) != null)
            {
                continue;
            }
            if (EnemyTable.Creatures.IndexOf(atackcreature) != 0
                    && atackcreature.abilky.FirstOrDefault(card => card.config.mainability is Symbiosis && card.cardstage == 1) != null
                    && EnemyTable.Creatures[EnemyTable.Creatures.IndexOf(atackcreature) - 1].HaveFood < EnemyTable.Creatures[EnemyTable.Creatures.IndexOf(atackcreature) - 1].Needfood)
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
        Card card = botikhand.cards[Random.Range(0, botikhand.cards.Count)];
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
                if (card.config.dopability != null && card.config.dopability is Carnivorous 
                    && creature.abilky.FirstOrDefault(card2 => card2.config.dopability is Carnivorous && card2.cardstage == 2) == null)
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
                    if (creature.abilky.Count() >= 3 && botiktable.Creatures.FirstOrDefault(card => card.abilky.Count() < 3) != null) 
                    {
                        continue;
                    }
                    if (creature.abilky.FirstOrDefault(card => card.config.dopability is Carnivorous && card.cardstage == 2) && card.config.mainability is Scavenger)
                    {
                        continue;
                    }
                    if (card.abilky.FirstOrDefault(card1 => card1.config.mainability is Parasite))
                    {
                        cardtoplay = card;
                        creatureonplay = YourTable.Creatures[Random.Range(0, YourTable.Creatures.Count())];
                        playdopability = false;
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
        if (botiktable.Creatures.Count <= 2)
        {
            PlayBotikCard();
            return;
        }
        else if (botiktable.Creatures.Select(creature => creature.abilky.Count()).Sum() <= 3)
        {
            PlayBotikability();
        }
        else if (botiktable.Creatures.Count() == 3 && botiktable.Creatures.Select(creature => creature.abilky.Count()).Sum() <= 3)
        {
            PlayBotikability();
        }
        else if (botiktable.Creatures.Count() == 3 && botiktable.Creatures.Select(creature => creature.abilky.Count()).Sum() >= 3)
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
