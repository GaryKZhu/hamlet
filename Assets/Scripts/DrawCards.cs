using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawCards : MonoBehaviour
{
    public GameObject Cards;
    public GameObject PlayerArea;
    public GameObject EnemyArea;
    public Sprite Stab;
    public Sprite Back;
    public Sprite Heal; 
    public Sprite Poison;

    public static List<Card> hamletDeck = new List<Card>();
    public static List<Card> enemyDeck = new List<Card>();


    public void onClick()
    {

        for (int i = 0; i < 4; i++)
        {
            Card c = hamletDeck[Random.Range(0, hamletDeck.Count)];
            GameObject card = Instantiate(Cards, new Vector2(0, 0), Quaternion.identity);
            card.GetComponent<Card>().damage = c.damage;
            card.GetComponent<Card>().id = c.id;
            card.GetComponent<Card>().cardName = c.cardName;
            card.GetComponent<Card>().heal = c.heal;
            card.GetComponent<Card>().poison = c.poison;
            card.GetComponent<Card>().cardFront = c.cardFront;
            card.GetComponent<Card>().cardBack = c.cardBack;
            card.transform.SetParent(PlayerArea.transform, false);
            card.GetComponent<Image>().sprite = c.cardFront;
        }

        for (int i = 0; i < 4; i++)
        {
            Card c = enemyDeck[Random.Range(0, enemyDeck.Count)];
            GameObject card = Instantiate(Cards, new Vector2(0, 0), Quaternion.identity);
            card.GetComponent<Card>().damage = c.damage;
            card.GetComponent<Card>().id = c.id;
            card.GetComponent<Card>().cardName = c.cardName;
            card.GetComponent<Card>().heal = c.heal;
            card.GetComponent<Card>().poison = c.poison;
            card.GetComponent<Card>().cardFront = c.cardFront;
            card.GetComponent<Card>().cardBack = c.cardBack;
            card.transform.SetParent(EnemyArea.transform, false);
            card.GetComponent<Image>().sprite = c.cardBack;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        hamletDeck.Add(new Card(0, "Stab", 3, 0, 0, Stab, Back));
        hamletDeck.Add(new Card(1, "Heal", 0, 2, 0, Heal, Back));
        hamletDeck.Add(new Card(2, "Poison", 1, 0, 2, Poison, Back));

        Debug.Log(GameObject.Find("Ghost"));
        if (GameObject.Find("Ghost") != null)
        {
            enemyDeck.Add(new Card(0, "Stab", 3, 0, 0, Stab, Back));
            enemyDeck.Add(new Card(1, "Heal", 0, 2, 0, Heal, Back));
            enemyDeck.Add(new Card(2, "Poison", 1, 0, 2, Poison, Back));
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
