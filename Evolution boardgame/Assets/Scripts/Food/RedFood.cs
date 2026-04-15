using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.GraphView.GraphView;

public class RedFood : FoodBlock
{
    public event Action<RedFood> onFoodClick;
    Vector3 Scale0;
    Vector3 Scale2;
    bool ischosen;
    public bool active;
    public bool onCard;
    public void activatefood()
    {
        if (onCard)
        {
            return;
        }
        onFoodClick?.Invoke(this);
        StartCoroutine(ChangeScale(Scale2, 0.5f));
        ischosen = true;
        active = true;
    }
    public void deactivatefood()
    {
        StartCoroutine(ChangeScale(Scale0, 0.5f));
        ischosen = false;
        active = false;

    }
    public void clearSubs()
    {
        onFoodClick = null;
    }
    private void OnDestroy()
    {
        clearSubs();
    }
    private void OnMouseDown()
    {
        if (!ischosen)
        {
            activatefood();
        }
        else
        {
            deactivatefood();
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
