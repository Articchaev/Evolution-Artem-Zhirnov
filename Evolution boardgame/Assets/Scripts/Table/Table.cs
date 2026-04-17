using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Table : MonoBehaviour
{
    [SerializeField]
    TableBox YourTable;
    [SerializeField]
    hand Hand;
    [SerializeField]
    Button TurnButton;
    [SerializeField]
    GameStateContext context;
    [SerializeField]
    public  BoxCollider2D colider;
    [SerializeField]
    Botikhand botikhand;
    [SerializeField]
    TableBox botiktable;
    private void OnMouseDown()
    {
        PlayCard();
    }
    public void PlayCard()
    {
        if(Hand.curentcard == null || Hand.curentcard.cardstage != 3 || context.nowstate is not EvolutionStage)
        {
            return;
        }
        Hand.curentcard.clearSubs();
        Hand.curentcard.Needfood = 1;
        TurnButton.onClick.RemoveListener(Hand.curentcard.Turn);
        Hand.curentcard.Ontable = true;
        Hand.curentcard.transform.SetParent(YourTable.transform);
        YourTable.Creatures.Add(Hand.curentcard);
        Hand.cards.Remove(Hand.curentcard);
        Hand.LayoutInstant();
        PlayBotikCard();

    }
    public void PlayBotikCard()
    {
        if (botikhand.cards.Count <= 0)
        {
            return;
        }
        Card card = botikhand.cards[Random.Range(0, botikhand.cards.Count - 1)];
        card.Needfood = 1;
        card.Ontable = true;
        card.transform.SetParent(botiktable.transform);
        botiktable.Creatures.Add(card);
        botikhand.cards.Remove(card);
        botikhand.LayoutInstant();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
