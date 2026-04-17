using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using static UnityEditor.PlayerSettings;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class FoodStage : MonoBehaviour, IGameState
{
    [SerializeField]
    GameStateContext gameStateContext;
    [SerializeField]
    FightingStage fightingStage;
    [SerializeField]
    Image imagefight;
    [SerializeField]
    Image imagefood;
    [SerializeField]
    Sprite fightactive;
    [SerializeField]
    Sprite foodinactive;
    public int a;
    public List<RedFood> Food;
    [SerializeField]
    hand Hand;
    [SerializeField]
    RedFood RedFoodprefab;
    [SerializeField]
    GameObject Root;
    Vector3 pos;
    public RedFood currentfood => Food.FirstOrDefault(c => c.active == true);
    public void ChooseFood(RedFood card)
    {
        for (int i = 0; i < Food.Count; i++)
        {
            if (Food[i] != card)
            {
                Food[i].deactivatefood();
            }
        }
    }
    public void ChangeState()
    {
        List<Vector3> vector3s = new List<Vector3>() { new Vector3(-0.5f, -0.25f, 0), new Vector3(-0.5f, 0.25f, 0), new Vector3(0.5f, 0.25f, 0), new Vector3(0.5f, -0.25f, 0), new Vector3(-1, -0.25f, 0), new Vector3(-1, 0.25f, 0), new Vector3(1, -0.25f, 0), new Vector3(1, 0.25f, 0) };
        a = Random.Range(3, 8);
        foreach (Card card in Hand.cards)
        {
            if (card.cardstage == 2)
            {
                card.transform.Rotate(0, 0, 180);
                card.cardstage = 1;
            }
            else
            {
                card.cardstage = 1;
                card.backside.gameObject.SetActive(false);
            }

        }
        for (int i = 0; i < a; i++)
        {
            Food.Add(Instantiate(RedFoodprefab, Root.transform));
            Food.Last().onFoodClick += ChooseFood;
            pos = vector3s[Random.Range(0, 7 - i)];
            vector3s.Remove(pos);
            Food[i].transform.localPosition = pos;
        }
        gameStateContext.SetStage(fightingStage);
        Debug.Log("Перешел в игровую фазу");
        imagefight.sprite = fightactive;
        imagefood.sprite = foodinactive;
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
