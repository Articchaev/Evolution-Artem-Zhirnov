using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Main_Ability_Config", menuName = "configs")]

public class Mainabilityconfig : ScriptableObject
{
    [SerializeField]
    public Sprite ImageCard;
    [SerializeField]
    public Sprite LeftDirection;
    [SerializeField]
    public Sprite RightDirection;
    [SerializeField]
    public string EnglishName;
    [SerializeField]
    public string RussianName;
    [SerializeField]
    public string Description;
    [SerializeField]
    public Color BackgroundColor;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
