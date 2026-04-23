using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PiracyExecutor : MonoBehaviour
{
    [SerializeField]
    public TableBox YourTable;
    [SerializeField]
    public FoodStage foodstage;
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
                || YourTable.curentcard.HaveFood >= YourTable.curentcard.Needfood 
                || EnemyTable.curentcard.HaveFood >= EnemyTable.curentcard.Needfood 
                || EnemyTable.curentcard.HaveFood == 0 || PiracyCreatures.Contains(YourTable.curentcard) == true 
                || YourTable.curentcard.abilky.FirstOrDefault(card => card.config.mainability is Piracy && card.cardstage == 1) == null)
            {
                return;
            }
            RedFood f = foodstage.Food[0];
            GameObject.Destroy(f.gameObject);
            foodstage.Food.Remove(f);
            PiracyCreatures.Add(YourTable.curentcard);
        }
    }
}
