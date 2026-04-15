using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "DeckConfig", menuName = "configs")]
public class Deckconfig : ScriptableObject
{
    [SerializeField]
    List<Cardconfig> cardconfigs;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
