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
        TurnButton.onClick.RemoveListener(Hand.curentcard.Turn);
        Hand.curentcard.Ontable = true;
        Hand.curentcard.transform.SetParent(YourTable.transform);
        YourTable.Creatures.Add(Hand.curentcard);
        Hand.cards.Remove(Hand.curentcard);
        Hand.LayoutInstant();

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
