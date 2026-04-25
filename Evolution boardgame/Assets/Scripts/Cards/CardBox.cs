using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBox : MonoBehaviour
{
    [SerializeField]
    Card card;
    float h;
    float v;
    [SerializeField]
    float ofseth;
    [SerializeField]
    float ofsetv;
    [SerializeField]
    float otstyp;
    private void UpdateFood()
    {
        foreach (FoodBlock i in card.foodBlocks)
        {
            i.transform.localPosition = new Vector3(ofseth + h, ofsetv + v, 0);
            if (h == 0)
            {
                h = otstyp;
            }
            else
            {
                h = 0;
                v -= otstyp;
            }

        }
        h = 0;
        v = 0;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateFood();
    }
}
