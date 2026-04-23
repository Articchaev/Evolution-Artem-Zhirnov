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
    public Button TurnButton;
    [SerializeField]
    deck Cardsdeck;
    [SerializeField]
    GameStateContext contexth;
    [SerializeField]
    FoodStage foodstageh;
    [SerializeField]
    Botik botik;
    [SerializeField]
    Table table;
    [SerializeField]
    public TableBox YourTable;
    [SerializeField]
    public TableBox botiktable;
    int handstage = 1;
    
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
            if(handstage == 2)
            {
                cards[i].transform.localRotation = Quaternion.Euler(180, 0, -targetRot);
            }
            else
            {
                cards[i].transform.localRotation = Quaternion.Euler(0, 0, targetRot);
            }
            
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
        Cardconfig config = Cardsdeck.givecard();
        cards.Last().SetUpView(config);
        cards.Last().YourTable = YourTable;
        cards.Last().botiktable = botiktable;
        cards.Last().Hand = this;
    }
    public void Turn1()
    {
        if (contexth.nowstate is not EvolutionStage)
        {
            return;
        }
        if (handstage == 2)
        {
            handstage++;
            table.colider.enabled = true;
        }
        else if (handstage == 1)
        {
            handstage++;
            table.colider.enabled = false;
        }
        else
        {
            handstage = 1;
            table.colider.enabled = false;
        }

    }
    public void OnDestroy()
    {
        TurnButton.onClick.RemoveAllListeners();
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
            Cardconfig config = Cardsdeck.givecard();
            cards.Last().SetUpView(config);
            cards.Last().Hand = this;
            cards.Last().YourTable = YourTable;
            cards.Last().botiktable = botiktable;
        }
        LayoutInstant();
        TurnButton.onClick.AddListener(Turn1);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
