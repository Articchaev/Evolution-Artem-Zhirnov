using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Video;

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
    [SerializeField]
    SpriteRenderer backside;
    public event Action<Card> onCardClick;
    Vector3 Scale0;
    Vector3 Scale2;
    bool ischosen;
    int layer;
    int cardstage = 1;
    // Start is called before the first frame update
    public void Turn()
    {
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
    public void activatecard()
    {
        onCardClick.Invoke(this);
        layer = SortingGroup.sortingOrder;
        StartCoroutine(ChangeScale(Scale2, 0.5f));
        ischosen = true;
        SortingGroup.sortingOrder = 10000;
    }
    public void deactivatecard()
    {
        StartCoroutine(ChangeScale(Scale0, 0.5f));
        ischosen = false;
        SortingGroup.sortingOrder = layer;
    }
    private void OnMouseDown()
    {
        if (!ischosen)
        {
            activatecard();
        }
        else
        {
            deactivatecard();
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
    private void OnDestroy()
    {
        onCardClick = null;
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
