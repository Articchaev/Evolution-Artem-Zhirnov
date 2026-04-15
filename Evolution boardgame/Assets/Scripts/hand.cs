using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class hand : MonoBehaviour
{
    [SerializeField]
    Card cardprefab;
    [SerializeField]
    float maxRotation;
    [SerializeField]
    float cardSpacing;
    [SerializeField]
    float arcHeight;
    public List<Card> cards = new List<Card>();
    [SerializeField]
    Button TurnButton;
    [SerializeField]
    deck Cardsdeck;
    [SerializeField]
    GameStateContext contexth;
    [SerializeField]
    FoodStage foodstageh;
    [SerializeField]
    Botik botik;
    
    public Card curentcard => cards.FirstOrDefault(c => c.active == true);
    public void LayoutInstant()
    {
        int n = cards.Count;
        if (n == 0) return;

        float center = (n - 1) * 0.5f;

        for (int i = 0; i < n; i++)
        {
            float t = n == 1 ? 0f : (i - center) / center; // -1..1
            Vector2 targetPos = new Vector2((i - center) * cardSpacing, -Mathf.Abs(t) * arcHeight);
            float targetRot = -t * maxRotation;

            cards[i].transform.localPosition = targetPos;
            cards[i].transform.localRotation = Quaternion.Euler(0, 0, targetRot);
            cards[i].transform.SetSiblingIndex(i); // порядок отрисовки
            cards[i].SortingGroup.sortingOrder = n*10 - i;
        }
    }
    public void ChooseCard(Card card)
    {
        for (int i = 0; i < cards.Count; i++) 
        {
            if (cards[i] != card)
            {
                cards[i].layer = cards[i].SortingGroup.sortingOrder;
                cards[i].deactivatecard();
            }
        }
    }
    public void AddCard()
    {
        if (Cardsdeck.currentcards <= 0)
        {
            return;
        }
        cards.Add(Instantiate(cardprefab, gameObject.transform));
        cards.Last().onCardClick += ChooseCard;
        cards.Last().context = contexth;
        cards.Last().botikh = botik;
        cards.Last().foodStage = foodstageh;
        TurnButton.onClick.AddListener(cards.Last().Turn);
        LayoutInstant();
        Cardsdeck.givecard();
    }
    void Start()
    {
        for (int i = 0; i < 6; i++)
        {
            cards.Add(Instantiate(cardprefab, gameObject.transform));
            cards.Last().onCardClick += ChooseCard;
            cards.Last().context = contexth;
            cards.Last().botikh = botik;
            cards.Last().foodStage = foodstageh;
            TurnButton.onClick.AddListener(cards.Last().Turn);
            Cardsdeck.givecard();
        }
        LayoutInstant();
    }
    // Update is called once per frame
    void Update()
    {

    }
}
