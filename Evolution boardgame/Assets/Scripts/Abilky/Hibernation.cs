using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Hibernation", menuName = "configs/Hibernation")]
public class Hibernation : Mainabilityconfig
{
    public override void OnAbilkaPlay(Card card)
    {
        card.hibernationabilka = true;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
