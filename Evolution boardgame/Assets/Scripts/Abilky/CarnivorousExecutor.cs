using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using UnityEngine;

public class CarnivorousExecutor : MonoBehaviour
{
    [SerializeField]
    public TableBox YourTable;
    [SerializeField]
    public TableBox EnemyTable;
    [SerializeField]
    public BlueFood bluefood;
    [SerializeField]
    Botik botik;
    public List<Card> CarnivorousCreatures = new List<Card>();
    public List<Card> PoisonedCreatures = new List<Card>();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (EnemyTable.curentcard == null || YourTable.curentcard == null || CarnivorousCreatures.Contains(YourTable.curentcard))
            {
                return;
            }
            if (YourTable.curentcard.abilky.FirstOrDefault(card => card.config.dopability is Carnivorous && card.cardstage == 2) != null)
            {
                if (YourTable.curentcard.abilky.FirstOrDefault(card => card.config.mainability is Swimming && card.cardstage == 1) != null
                    && EnemyTable.curentcard.abilky.FirstOrDefault(card => card.config.mainability is Swimming && card.cardstage == 1) == null
                    || YourTable.curentcard.abilky.FirstOrDefault(card => card.config.mainability is Swimming && card.cardstage == 1) == null
                    && EnemyTable.curentcard.abilky.FirstOrDefault(card => card.config.mainability is Swimming && card.cardstage == 1) != null)
                {
                    return;
                }
                if (YourTable.curentcard.abilky.FirstOrDefault(card => card.config.mainability is HighBodyWeight && card.cardstage == 1) == null
                    && EnemyTable.curentcard.abilky.FirstOrDefault(card => card.config.mainability is HighBodyWeight && card.cardstage == 1) != null)
                {
                    return;
                }
                if (EnemyTable.curentcard.abilky.FirstOrDefault(card => card.config.mainability is Burrowing && card.cardstage == 1) != null
                    && EnemyTable.curentcard.HaveFood >= EnemyTable.curentcard.Needfood)
                {
                    return;
                }
                if (YourTable.curentcard.abilky.FirstOrDefault(card => card.config.mainability is SharpVision && card.cardstage == 1) == null
                    && EnemyTable.curentcard.abilky.FirstOrDefault(card => card.config.mainability is Camo && card.cardstage == 1) != null)
                {
                    return;
                }
                if (EnemyTable.curentcard.abilky.FirstOrDefault(card => card.config.mainability is Symbiosis && card.cardstage == 1) != null)
                {
                    return;
                }
                if (YourTable.Creatures.IndexOf(YourTable.curentcard) !=0 
                    && YourTable.curentcard.abilky.FirstOrDefault(card => card.config.mainability is Symbiosis && card.cardstage == 1) != null
                    && YourTable.Creatures[YourTable.Creatures.IndexOf(YourTable.curentcard) - 1].HaveFood < YourTable.Creatures[YourTable.Creatures.IndexOf(YourTable.curentcard) - 1].Needfood)
                {
                    return;
                }
                Card targetcreature = EnemyTable.curentcard;
                if (targetcreature.abilky.FirstOrDefault(card => card.config.mainability is Mimicry && card.cardstage == 1) != null)
                {
                    foreach (Card i in cantarget(YourTable.curentcard))
                    {
                        if (i.abilky.FirstOrDefault(card => card.config.mainability is Mimicry && card.cardstage == 1) == null)
                        {
                            targetcreature = i;
                            break;
                        }
                    }
                }
                CarnivorousCreatures.Add(YourTable.curentcard);
                if (YourTable.curentcard.HaveFood < YourTable.curentcard.Needfood)
                {
                    if (targetcreature.abilky.FirstOrDefault(card => card.config.mainability is Running && card.cardstage == 1) != null)
                    {
                        int k = Random.Range(0, 2);
                        if (k == 0)
                        {
                            return;
                        }
                    }
                    YourTable.curentcard.foodBlocks.Add(GameObject.Instantiate(bluefood, YourTable.curentcard.transform));
                    YourTable.curentcard.HaveFood += 1;
                }
                else if (YourTable.curentcard.abilky.FirstOrDefault(card => card.config.dopability is FatTissue && card.cardstage == 2) != null
                    && YourTable.curentcard.HaveFood < YourTable.curentcard.Needfood + YourTable.curentcard.abilky.Count(card => card.config.dopability is FatTissue && card.cardstage == 2))
                {
                    if (targetcreature.abilky.FirstOrDefault(card => card.config.mainability is Running && card.cardstage == 1) != null)
                    {
                        int k = Random.Range(0, 2);
                        if (k == 0)
                        {
                            return;
                        }
                    }
                    YourTable.curentcard.foodBlocks.Add(GameObject.Instantiate(YourTable.curentcard.yellowfood, YourTable.curentcard.transform));
                    YourTable.curentcard.HaveFood += 1;
                    YourTable.curentcard.UseCooperation();
                }
                else
                {
                    CarnivorousCreatures.Remove(YourTable.curentcard);
                    return;
                }
                if (targetcreature.abilky.FirstOrDefault(card => card.config.mainability is TailLoss && card.cardstage == 1) != null)
                {
                    Card creature = targetcreature.abilky.FirstOrDefault(card => card.config.mainability is Parasite && card.cardstage == 1);
                    if (creature == null)
                    {
                        creature = targetcreature.abilky[0];
                    }
                    targetcreature.abilky.Remove(creature);
                    GameObject.Destroy(creature.gameObject);
                    targetcreature.AbilkaRovno();

                    return;
                }
                if (targetcreature.abilky.FirstOrDefault(card => card.config.mainability is Poisonous && card.cardstage == 1) != null)
                {
                    PoisonedCreatures.Add(YourTable.curentcard);
                }

                if (YourTable.curentcard.HaveFood < YourTable.curentcard.Needfood)
                {
                    YourTable.curentcard.foodBlocks.Add(GameObject.Instantiate(bluefood, YourTable.curentcard.transform));
                    YourTable.curentcard.HaveFood += 1;
                    YourTable.curentcard.UseCooperation();
                }

                else if (YourTable.curentcard.abilky.FirstOrDefault(card => card.config.dopability is FatTissue && card.cardstage == 2) != null
                    && YourTable.curentcard.HaveFood < YourTable.curentcard.Needfood + YourTable.curentcard.abilky.Count(card => card.config.dopability is FatTissue && card.cardstage == 2))
                {
                    YourTable.curentcard.foodBlocks.Add(GameObject.Instantiate(YourTable.curentcard.yellowfood, YourTable.curentcard.transform));
                    YourTable.curentcard.HaveFood += 1;
                }

                if (targetcreature != EnemyTable.Creatures.Last() 
                    && EnemyTable.Creatures[EnemyTable.Creatures.IndexOf(targetcreature) + 1].abilky.FirstOrDefault(card => card.config.mainability is Symbiosis && card.cardstage == 1) != null)
                {
                    Card c = EnemyTable.Creatures[EnemyTable.Creatures.IndexOf(targetcreature) + 1];
                    Card f = c.abilky.FirstOrDefault(card => card.config.mainability is Symbiosis && card.cardstage == 1);
                    c.abilky.Remove(f);
                    GameObject.Destroy(f.gameObject);
                    c.AbilkaRovno();
                }

                GameObject.Destroy(targetcreature.gameObject);
                EnemyTable.Creatures.Remove(targetcreature);
                Card ScavengerCreature  = YourTable.Creatures.FirstOrDefault(card => card.abilky.FirstOrDefault(card2 => card2.config.mainability is Scavenger && card2.cardstage == 1) != null);
                
                if (ScavengerCreature == null)
                {
                    ScavengerCreature = EnemyTable.Creatures.FirstOrDefault(card => card.abilky.FirstOrDefault(card2 => card2.config.mainability is Scavenger && card2.cardstage == 1) != null);
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
                botik.botikuseability();
            }
        }
    }
    public List<Card> cantarget(Card atackcreature)
    {
        List<Card> targets = new List<Card>();
        foreach (Card card in EnemyTable.Creatures)
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
            if (YourTable.Creatures.IndexOf(atackcreature) != 0
                    && atackcreature.abilky.FirstOrDefault(card => card.config.mainability is Symbiosis && card.cardstage == 1) != null
                    && YourTable.Creatures[YourTable.Creatures.IndexOf(atackcreature) - 1].HaveFood < YourTable.Creatures[YourTable.Creatures.IndexOf(atackcreature) - 1].Needfood)
            {
                continue;
            }
            targets.Add(card);
        }
        return targets;
    }
}
