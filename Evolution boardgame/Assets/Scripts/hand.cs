using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hand : MonoBehaviour
{
    [SerializeField]
    Card cardprefab;
    [SerializeField]
    float maxRotation;
    [SerializeField]
    float cardSpacing;
    [SerializeField]
    float arcHeight;
    List<Card> cards = new List<Card>();
    private void LayoutInstant()
    {
        int n = cards.Count;
        if (n == 0) return;

        float center = (n - 1) * 0.5f;

        for (int i = 0; i < n; i++)
        {
            float t = n == 1 ? 0f : (i - center) / center; // -1..1
            Vector2 targetPos = new Vector2((i - center) * cardSpacing, -Mathf.Abs(t) * arcHeight);
            float targetRot = -t * maxRotation;

            cards[i].transform.localPosition = targetPos;
            cards[i].transform.localRotation = Quaternion.Euler(0, 0, targetRot);
            cards[i].transform.SetSiblingIndex(i); // порядок отрисовки
            cards[i].SortingGroup.sortingOrder = n - i;
        }
    }
    void Start()
    {
        for (int i = 0; i < 6; i++)
        {
            cards.Add(Instantiate(cardprefab, gameObject.transform));

        }
        LayoutInstant();
    }
    // Update is called once per frame
    void Update()
    {
        LayoutInstant();
    }
}
