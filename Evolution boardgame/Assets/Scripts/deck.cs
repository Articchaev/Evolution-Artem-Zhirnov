using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class deck : MonoBehaviour
{
    [SerializeField]
    List<Card> CardsDeck;
    [SerializeField]
    private int maxCards;
    int currentcards;
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
    public void givecard()
    {
        currentcards--;
        deckstage(currentcards);
    }
    void Start()
    {
        deckstage(56);
        currentcards = maxCards;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
