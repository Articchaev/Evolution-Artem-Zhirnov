using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "DeckConfig", menuName = "configs")]
public class Deckconfig : ScriptableObject
{
    [SerializeField]
    public List<Cardconfig> cardconfigs;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
