using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Botik : MonoBehaviour
{
    [SerializeField]
    FoodStage foodstage;
    [SerializeField]
    TableBox botiktable;
    public void PlayBotikFood()
    {
        if (botiktable.Creatures.Count <=0 || foodstage.Food.Count <= 0)
        {
            return;
        }
        RedFood food = foodstage.Food[0];
        Card card = botiktable.Creatures[Random.Range(0, botiktable.Creatures.Count - 1)];
        food.clearSubs();
        food.onCard = true;
        card.HaveFood += 1;
        food.transform.SetParent(card.transform);
        card.foodBlocks.Add(food);
        RedFood a = food;
        foodstage.Food.Remove(food);
        a.deactivatefood();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
