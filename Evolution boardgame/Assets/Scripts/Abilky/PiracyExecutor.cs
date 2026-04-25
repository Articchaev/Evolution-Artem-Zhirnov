using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PiracyExecutor : MonoBehaviour
{
    [SerializeField]
    public TableBox YourTable;
    [SerializeField]
    TableBox EnemyTable;
    public List<Card> PiracyCreatures = new List<Card>();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (EnemyTable.curentcard == null || YourTable.curentcard == null 
                || YourTable.curentcard.HaveFood >= YourTable.curentcard.Needfood + YourTable.curentcard.abilky.Count(card => card.config.dopability is FatTissue && card.cardstage == 2)
                || EnemyTable.curentcard.HaveFood >= EnemyTable.curentcard.Needfood 
                || EnemyTable.curentcard.HaveFood == 0 || PiracyCreatures.Contains(YourTable.curentcard) == true 
                || YourTable.curentcard.abilky.FirstOrDefault(card => card.config.mainability is Piracy && card.cardstage == 1) == null)
            {
                return;
            }
            FoodBlock f = EnemyTable.curentcard.foodBlocks[0];
            EnemyTable.curentcard.foodBlocks.Remove(f);
            f.transform.SetParent(YourTable.curentcard.transform);
            YourTable.curentcard.foodBlocks.Add(f);
            EnemyTable.curentcard.HaveFood -= 1;
            YourTable.curentcard.HaveFood += 1;
            PiracyCreatures.Add(YourTable.curentcard);
        }
    }
}
