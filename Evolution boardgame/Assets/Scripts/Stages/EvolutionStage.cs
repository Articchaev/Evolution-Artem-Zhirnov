using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EvolutionStage : MonoBehaviour, IGameState
{
    
    [SerializeField]
    GameStateContext gameStateContext;
    [SerializeField]
    FoodStage foodStage;
    [SerializeField]
    Image imageevolution;
    [SerializeField]
    Image imagefood;
    [SerializeField]
    Sprite foodactive;
    [SerializeField]
    Sprite evolutioninactive;
    [SerializeField]
    Table table;
    [SerializeField]
    Botikhand botikhand;
    [SerializeField]
    Botik botik;
    public void ChangeState()
    {
        while (botikhand.cards.Count() > 0)
        {
            botik.botikdosmth();
        }
        gameStateContext.SetStage(foodStage);
        Debug.Log("Перешел в фазу определения кормовой базы");
        imageevolution.sprite = evolutioninactive;
        imagefood.sprite = foodactive;
        gameStateContext.ChangeState();
        table.colider.enabled = false;
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
