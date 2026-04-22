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
            if (EnemyTable.curentcard == null || YourTable.curentcard == null || YourTable.curentcard.HaveFood >= YourTable.curentcard.Needfood || CarnivorousCreatures.Contains(YourTable.curentcard))
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
                CarnivorousCreatures.Add(YourTable.curentcard);
                if (EnemyTable.curentcard.abilky.FirstOrDefault(card => card.config.mainability is Running && card.cardstage == 1) != null)
                {
                    int k = Random.Range(0, 2);
                    if (k == 0)
                    {
                        return;
                    }
                }
                YourTable.curentcard.foodBlocks.Add(GameObject.Instantiate(bluefood, YourTable.curentcard.transform));
                YourTable.curentcard.HaveFood += 1;
                if (EnemyTable.curentcard.abilky.FirstOrDefault(card => card.config.mainability is Poisonous && card.cardstage == 1) != null)
                {
                    PoisonedCreatures.Add(YourTable.curentcard);
                }
                if (YourTable.curentcard.HaveFood < YourTable.curentcard.Needfood)
                {
                    YourTable.curentcard.foodBlocks.Add(GameObject.Instantiate(bluefood, YourTable.curentcard.transform));
                    YourTable.curentcard.HaveFood += 1;
                }
                GameObject.Destroy(EnemyTable.curentcard.gameObject);
                EnemyTable.Creatures.Remove(EnemyTable.curentcard);
                Card ScavengerCreature  = YourTable.Creatures.FirstOrDefault(card => card.abilky.FirstOrDefault(card2 => card2.config.mainability is Scavenger && card2.cardstage == 1) != null);
                if (ScavengerCreature == null)
                {
                    ScavengerCreature = EnemyTable.Creatures.FirstOrDefault(card => card.abilky.FirstOrDefault(card2 => card2.config.mainability is Scavenger && card2.cardstage == 1) != null);
                }
                if (ScavengerCreature != null)
                {
                    ScavengerCreature.foodBlocks.Add(GameObject.Instantiate(bluefood, ScavengerCreature.transform));
                    ScavengerCreature.HaveFood += 1;
                }
                botik.botikuseability();
            }
        }
    }
}
