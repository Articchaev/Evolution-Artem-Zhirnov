using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableBox : MonoBehaviour
{
    [SerializeField]
    public List<Card> Creatures = new List<Card>();
    [SerializeField]
    int Otstyp;
    // Start is called before the first frame update
    public void UpdateLayout()
    {
        for (int i = 0; i < Creatures.Count; i++)
        {
            Creatures[i].transform.localPosition = new Vector3(Otstyp * i - (Creatures.Count - 1) * Otstyp / 2, 0, 0);
            Creatures[i].transform.localRotation = Quaternion.Euler(0, 0, 0);
            Creatures[i].transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLayout();
    }
}
