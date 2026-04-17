using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "CardConfig", menuName = "configs")]
public class Cardconfig : ScriptableObject
{
    [SerializeField]
    public DopAbilityConfig dopability;
    [SerializeField]
    public Mainabilityconfig mainability;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
