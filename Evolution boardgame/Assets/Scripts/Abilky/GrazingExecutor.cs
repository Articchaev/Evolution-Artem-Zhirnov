using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GrazingExecutor : MonoBehaviour
{
    [SerializeField]
    public TableBox YourTable;
    [SerializeField]
    public FoodStage foodstage;
    public List<Card> GrazingCreatures= new List<Card>();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (YourTable.curentcard == null || YourTable.curentcard.abilky.FirstOrDefault(card => card.config.mainability is Grazing && card.cardstage == 1) == null || GrazingCreatures.Contains(YourTable.curentcard) == true)
            {
                return;
            }
            RedFood f = foodstage.Food[0];
            GameObject.Destroy(f.gameObject);
            foodstage.Food.Remove(f);
            GrazingCreatures.Add(YourTable.curentcard);
        }
    }
}
