using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class Card : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer dop1;
    [SerializeField]
    TMP_Text cardname;
    public GameStateContext context;
    [SerializeField]
    TMP_Text cardnamerus;
    [SerializeField]
    TMP_Text carddescription;
    [SerializeField]
    TMP_Text dopcard;
    [SerializeField]
    TMP_Text dopcardrus;
    [SerializeField]
    SpriteRenderer background1;
    [SerializeField]
    SpriteRenderer background2;
    [SerializeField]
    SpriteRenderer directionleft;
    [SerializeField]
    SpriteRenderer directionright;
    [SerializeField]
    SpriteRenderer imagecard;
    [SerializeField]
    SpriteRenderer imagedopcard;
    [SerializeField]
    public SortingGroup SortingGroup;
    [SerializeField]
    public SpriteRenderer backside;
    public hand Hand;
    public event Action<Card> onCardClick;
    Vector3 Scale0;
    Vector3 Scale2;
    bool ischosen;
    public Cardconfig config;
    [SerializeField]
    public int layer;
    public int activatenumber = -1;
    public int cardstage = 1;
    public bool active;
    public bool Ontable = false;
    public int Needfood = 0;
    public int HaveFood = 0;
    public List<FoodBlock> foodBlocks = new List<FoodBlock>();
    public FoodStage foodStage;
    public bool Botikcard = false;
    public Botik botikh;
    public List<Card> abilky = new List<Card>();
    public bool OnCard = false;
    [SerializeField]
    public float otstyp;
    [SerializeField]
    public BoxCollider2D colider;
    public TableBox YourTable;
    public TableBox botiktable;
    public bool activefightstage = false;
    public bool hibernationabilka = false;

    public void SetUpView(Cardconfig abilitycard)
    {
        config = abilitycard;
        cardname.text = abilitycard.mainability.EnglishName;
        cardnamerus.text = abilitycard.mainability.RussianName;
        carddescription.text = abilitycard.mainability.Description;
        background1.color = abilitycard.mainability.BackgroundColor;
        imagecard.sprite = abilitycard.mainability.ImageCard;
        if (abilitycard.mainability.RightDirection != null)
        {
            directionright.sprite = abilitycard.mainability.RightDirection;
        }
        else
        {
            directionright.gameObject.SetActive(false);
        }
        if (abilitycard.mainability.LeftDirection != null)
        {
            directionleft.sprite = abilitycard.mainability.LeftDirection;
        }
        else
        {
            directionleft.gameObject.SetActive(false);
        }
        if (abilitycard.dopability != null)
        {
            background2.color = abilitycard.dopability.BackGroundColor;
            imagedopcard.sprite = abilitycard.dopability.DopImage;
            dopcard.text = abilitycard.dopability.EnglishText;
            dopcardrus.text = abilitycard.dopability.RussianText;
            if (abilitycard.dopability is not Carnivorous)
            {
                dop1.gameObject.SetActive(false);
            }
        }
        else
        {
            background2.gameObject.SetActive(false);
            imagedopcard.gameObject.SetActive(false);
            dopcard.gameObject.SetActive(false);
            dopcardrus.gameObject.SetActive(false);
            dop1.gameObject.SetActive(false);
        }
    }
    public void Turn()
    {
        if (context.nowstate is not EvolutionStage)
        {
            return;
        }
        if (cardstage == 1)
        {
            transform.Rotate(0, 0, 180);
            cardstage = 2;
        }
        else if (cardstage == 2)
        {
            transform.Rotate(0, 0, 180);
            backside.gameObject.SetActive(true);
            cardstage = 3;
        }
        else
        {
            cardstage = 1;
            backside.gameObject.SetActive(false);
        }
    }
    public void BotikTurn(int x)
    {
        if (x == 2)
        {
            transform.localEulerAngles = new Vector3(0, 0, 180);
            cardstage = 2;
            backside.gameObject.SetActive(false);
        }
        else if (x == 3)
        {
            transform.localEulerAngles = new Vector3(0, 0, 0);
            backside.gameObject.SetActive(true);
            cardstage = 3;
        }
        else
        {
            cardstage = 1;
            transform.localEulerAngles = new Vector3(0, 0, 0);
            backside.gameObject.SetActive(false);
        }
    }
    public void activatecard()
    {
        onCardClick?.Invoke(this);
        layer = SortingGroup.sortingOrder;
        StartCoroutine(ChangeScale(Scale2, 0.5f));
        ischosen = true;
        SortingGroup.sortingOrder = 10000;
        active = true;
    }
    public void deactivatecard()
    {
        StartCoroutine(ChangeScale(Scale0, 0.5f));
        ischosen = false;
        SortingGroup.sortingOrder = layer;
        active = false;

    }
    public void activatefightcard()
    {
        if (YourTable.Creatures.Contains(this))
        {
            foreach (Card i in YourTable.Creatures)
            {
                if (i.activefightstage)
                {
                    i.deactivatefightcard();
                }
            }
        }
        else if (botiktable.Creatures.Contains(this))
        {
            foreach (Card i in botiktable.Creatures)
            {
                if (i.activefightstage)
                {
                    i.deactivatefightcard();
                }
            }
        }
        StartCoroutine(ChangeScale(Scale2, 0.5f));
        activefightstage = true;
    }
    public void deactivatefightcard()
    {
        StartCoroutine(ChangeScale(Scale0, 0.5f));
        activefightstage = false;
    }
    private void OnMouseDown()
    {
        if (Ontable)
        {
            if (foodStage.currentfood != null && context.nowstate is FightingStage && cardstage == 3 && HaveFood < Needfood && activefightstage == false && !Botikcard)
            {
                foodStage.currentfood.clearSubs();
                foodStage.currentfood.onCard = true;
                HaveFood += 1;
                foodStage.currentfood.transform.SetParent(gameObject.transform);
                foodBlocks.Add(foodStage.currentfood);
                RedFood a = foodStage.currentfood;
                foodStage.Food.Remove(foodStage.currentfood);
                a.deactivatefood();
                botikh.botikuseability();
            }
            else if (context.nowstate is EvolutionStage && cardstage == 3 && Hand.curentcard != null && Hand.curentcard.cardstage != 3 && (!Botikcard || Hand.curentcard.config.mainability is Parasite))
            {
                Card c = Hand.curentcard;
                if (Hand.curentcard.cardstage == 2)
                {
                    if (Hand.curentcard.config.dopability == null)
                    {
                        return;
                    }
                    if (abilky.FirstOrDefault(card => card.config.mainability is Scavenger && card.cardstage == 1) && c.config.dopability is Carnivorous)
                    {
                        return;
                    }
                    Hand.curentcard.clearSubs();
                    Hand.curentcard.Needfood = 0;
                    Hand.TurnButton.onClick.RemoveListener(Hand.curentcard.Turn);
                    Hand.curentcard.OnCard = true;
                    Hand.curentcard.transform.SetParent(transform);
                    abilky.Add(Hand.curentcard);
                    Hand.cards.Remove(Hand.curentcard);
                    Hand.LayoutInstant();
                    c.transform.localScale = new Vector3(1, 1, 1);
                    c.transform.localEulerAngles = new Vector3(180, 0, 0);
                    c.transform.localPosition = new Vector3(0, abilky.Count * otstyp, 0);
                    c.SortingGroup.sortingOrder = -abilky.Count;
                    c.colider.enabled = false;
                    c.config.dopability.OnAbilkaPlay(this);
                }
                else
                {
                    if (abilky.FirstOrDefault(card => card.config.dopability is Carnivorous && card.cardstage == 2) && c.config.mainability is Scavenger)
                    {
                        return;
                    }
                    
                    Hand.curentcard.clearSubs();
                    Hand.curentcard.Needfood = 0;
                    Hand.TurnButton.onClick.RemoveListener(Hand.curentcard.Turn);
                    Hand.curentcard.OnCard = true;
                    Hand.curentcard.transform.SetParent(transform);
                    abilky.Add(Hand.curentcard);
                    Hand.cards.Remove(Hand.curentcard);
                    Hand.LayoutInstant();
                    c.transform.localScale = new Vector3(1, 1, 1);
                    c.transform.localEulerAngles = new Vector3(0, 0, 0);
                    c.transform.localPosition = new Vector3(0, abilky.Count * otstyp, 0);
                    c.SortingGroup.sortingOrder = -abilky.Count;
                    c.colider.enabled = false;
                    c.config.mainability.OnAbilkaPlay(this);
                }
                botikh.botikdosmth();
            }
            else if (context.nowstate is FightingStage && cardstage == 3)
            {
                if (!activefightstage)
                {
                    activatefightcard();
                }
                else
                {
                    deactivatefightcard();
                }
            }
            else
            {
                return;
            }
        }
        else
        {
            if (OnCard)
            {
                return;
            }
            if (!ischosen)
            {
                activatecard();
            }
            else
            {
                deactivatecard();
            }
        }

    }
    public IEnumerator ChangeScale(Vector3 scale2, float timex)
    {
        Vector3 scale = gameObject.transform.localScale;
        if (scale != scale2)
        {
            Vector3 pos = gameObject.transform.localPosition;
            float time = 0;
            while (time < timex)
            {
                gameObject.transform.localScale = Vector3.Lerp(scale, scale2, time / timex);
                //gameObject.transform.localPosition = Vector3.Lerp(pos, 2 * pos, time / 1);
                time += Time.deltaTime;
                yield return null;
            }
        }
    }
    public void clearSubs()
    {
        onCardClick = null;
    }
    private void OnDestroy()
    {
        clearSubs();
    }
    void Start()
    {
        Scale0 = gameObject.transform.localScale;
        Scale2 = 2 * Scale0;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
