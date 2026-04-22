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
    [SerializeField]
    Botik botikmosg;
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
        Card c = Hand.curentcard;
        Hand.cards.Remove(Hand.curentcard);
        c.StopAllCoroutines();
        c.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        Hand.LayoutInstant();
        botikmosg.botikdosmth();

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
