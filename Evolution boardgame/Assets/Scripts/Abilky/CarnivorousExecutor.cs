using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CarnivorousExecutor : MonoBehaviour
{
    [SerializeField]
    public TableBox YourTable;
    [SerializeField]
    public TableBox EnemyTable;
    [SerializeField]
    public BlueFood bluefood;
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
                GameObject.Destroy(EnemyTable.curentcard.gameObject);
                EnemyTable.Creatures.Remove(EnemyTable.curentcard);
                YourTable.curentcard.foodBlocks.Add(GameObject.Instantiate(bluefood, YourTable.curentcard.transform));
                YourTable.curentcard.HaveFood += 1;
                CarnivorousCreatures.Add(YourTable.curentcard);
                if (EnemyTable.curentcard.abilky.FirstOrDefault(card => card.config.mainability is Poisonous && card.cardstage == 1) != null)
                {
                    PoisonedCreatures.Add(YourTable.curentcard);
                }
                if (YourTable.curentcard.HaveFood < YourTable.curentcard.Needfood)
                {
                    YourTable.curentcard.foodBlocks.Add(GameObject.Instantiate(bluefood, YourTable.curentcard.transform));
                    YourTable.curentcard.HaveFood += 1;
                }
            }
        }
    }
}
