using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Card : MonoBehaviour
{
    public int id;
    public string cardName;
    public int damage;
    public int heal;
    public string type; 
    public int poison;
    public Sprite cardFront;
    public Sprite cardBack;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Card(int Id, string CardName, string Type, int Damage,  int Heal, int Poison, Sprite CardFront, Sprite CardBack)
    {
        id = Id;
        cardName = CardName;
        damage = Damage;
        heal = Heal;
        type = Type; 
        poison = Poison;
        cardFront = CardFront;
        cardBack = CardBack;
    }
}
