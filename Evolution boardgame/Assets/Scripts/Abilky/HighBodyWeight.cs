using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "HighBodyWeight", menuName = "configs/HighBodyWeight")]
public class HighBodyWeight : Mainabilityconfig
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
