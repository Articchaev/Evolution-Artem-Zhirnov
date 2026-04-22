using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Dop_Ability_Config", menuName = "configs")]
public class DopAbilityConfig : ScriptableObject
{
    [SerializeField]
    public string EnglishText;
    [SerializeField]
    public string RussianText;
    [SerializeField]
    public Sprite DopImage;
    [SerializeField]
    public Color BackGroundColor;
    public virtual void OnAbilkaPlay(Card card)
    {

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
