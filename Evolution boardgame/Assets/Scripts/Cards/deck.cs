using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class deck : MonoBehaviour
{
    [SerializeField]
    List<GameObject> CardsDeck;
    [SerializeField]
    private int maxCards;
    [SerializeField]
    Deckconfig deckconfig;
    List<Cardconfig> cardsindeck;
    public int currentcards;
    // Start is called before the first frame update
    public void deckstage(int n)
    {
        int a = maxCards / CardsDeck.Count;
        for (int i = 0; i < CardsDeck.Count; i++)
        {
            if (i < n / a)
            {
                CardsDeck[i].gameObject.SetActive(true);
            }
            else
            {
                CardsDeck[i].gameObject.SetActive(false);
            }
        }
    }
    public Cardconfig givecard()
    {

        currentcards--;
        deckstage(currentcards);
        Cardconfig c = cardsindeck[Random.Range(0, cardsindeck.Count)];
        cardsindeck.Remove(c);
        return c;
    }
    void Start()
    {
        deckstage(84);
        currentcards = maxCards;
        cardsindeck = new List<Cardconfig>(deckconfig.cardconfigs);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
