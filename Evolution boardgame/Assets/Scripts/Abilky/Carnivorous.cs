using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Carnivorous", menuName = "configs/Carnivorous")]
public class Carnivorous : DopAbilityConfig
{
    public override void OnAbilkaPlay(Card card)
    {
        card.Needfood+=1;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
