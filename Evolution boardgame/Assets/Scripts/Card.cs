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
    Vector3 Scale0;
    Vector3 Scale2;
    bool ischosen;
    // Start is called before the first frame update
    public void Turn()
    {
        if (backside.gameObject.activeSelf)
        {
            backside.gameObject.SetActive(false);
        }
        else
        {
            backside.gameObject.SetActive(true);
        }
    }
    private void OnMouseDown()
    {
        if (!ischosen)
        {
            StartCoroutine(ChangeScale(Scale2, 0.5f));
            ischosen = true;
        }
        else
        {
            StartCoroutine(ChangeScale(Scale0, 0.5f));
            ischosen= false;
        }
    }
    private IEnumerator ChangeScale(Vector3 scale2, float timex)
    {
        Vector3 scale = gameObject.transform.localScale;
        if (scale != scale2)
        {
            Vector3 pos = gameObject.transform.localPosition;
            float time = 0;
            SortingGroup.sortingOrder = 10000;
            while (time < timex)
            {
                gameObject.transform.localScale = Vector3.Lerp(scale, scale2, time / timex);
                //gameObject.transform.localPosition = Vector3.Lerp(pos, 2 * pos, time / 1);
                time += Time.deltaTime;
                yield return null;
            }
        }
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
