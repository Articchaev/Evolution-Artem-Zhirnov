using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class Botikhand : MonoBehaviour
{
    public List<Card> cards = new List<Card>();
    [SerializeField]
    deck Cardsdeck;
    [SerializeField]
    GameStateContext contexth;
    [SerializeField]
    FoodStage foodstageh;
    [SerializeField]
    Card cardprefab;
    [SerializeField]
    float maxRotation;
    [SerializeField]
    float cardSpacing;
    [SerializeField]
    float arcHeight;
    public void AddCard()
    {
        if (Cardsdeck.currentcards <= 0)
        {
            return;
        }
        cards.Add(Instantiate(cardprefab, gameObject.transform));
        cards.Last().context = contexth;
        cards.Last().foodStage = foodstageh;
        LayoutInstant();
        Cardsdeck.givecard();
        cards.Last().BotikTurn(3);
        cards.Last().Botikcard = true;
    }
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
            cards[i].SortingGroup.sortingOrder = n * 10 - i;
        }
    }
    void Start()
    {
        for (int i = 0; i < 6; i++)
        {
            cards.Add(Instantiate(cardprefab, gameObject.transform));
            cards.Last().context = contexth;
            cards.Last().foodStage = foodstageh;
            Cardsdeck.givecard();
            cards.Last().BotikTurn(3);
            cards.Last().Botikcard = true;
        }
        LayoutInstant();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
