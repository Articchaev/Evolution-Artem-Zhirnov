using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Parasite", menuName = "configs/Parasite")]
public class Parasite : Mainabilityconfig
{
    public override void OnAbilkaPlay(Card card)
    {
        card.Needfood += 2;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
