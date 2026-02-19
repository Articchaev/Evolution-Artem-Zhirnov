using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class Card : MonoBehaviour
{
    [SerializeField]
    TMP_Text cardname;
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
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
