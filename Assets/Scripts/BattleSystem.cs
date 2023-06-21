using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    public GameObject Cards;
    public GameObject PlayerArea;
    public GameObject EnemyArea;
    public Sprite Stab;
    public Sprite Back;
    public Sprite Heal; 
    public Sprite Poison;
    public BattleState state; 
    public static List<Card> hamletDeck = new List<Card>();
    public static List<Card> enemyDeck = new List<Card>();
    public static List<Card> enemyHand = new List<Card>(); 
    public BattleState oldState;
    private int turn = 0; 
    Player hamlet;
    Player enemy; 

    public void onClick()
    {
        gameObject.GetComponent<Image>().enabled = false;
        gameObject.GetComponent<Button>().interactable = false;
        hamlet = GameObject.Find("Hamlet").GetComponent<Player>();
        enemy = GameObject.Find("Ghost").GetComponent<Player>();

        StartBattle(); 
    }

    void StartBattle()
    {
        for (int i = 0; i < 4; i++)
        {
            Card c = hamletDeck[Random.Range(0, hamletDeck.Count)];

            GameObject card = Instantiate(Cards, new Vector2(0, 0), Quaternion.identity);
            card.GetComponent<Card>().damage = c.damage;
            card.GetComponent<Card>().id = c.id;
            card.GetComponent<Card>().cardName = c.cardName;
            card.GetComponent<Card>().heal = c.heal;
            card.GetComponent<Card>().type = c.type;
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
            card.GetComponent<Card>().type = c.type;
            card.GetComponent<Card>().cardFront = c.cardFront;
            card.GetComponent<Card>().cardBack = c.cardBack;
            card.transform.SetParent(EnemyArea.transform, false);
            card.GetComponent<Image>().sprite = c.cardBack;
            enemyHand.Add(c); 
        }
        state = BattleState.PLAYERTURN;
    }

    IEnumerator PlayerTurn()
    {
        //calculate poison

        if(hamlet.sanity <= 0)
        {
            state = BattleState.LOST;
            yield return new WaitForSeconds(2f);
        }
        else
        {
            //turn playerturn banner on
            yield return new WaitForSeconds(2f);
            GameObject.Find("YourTurn").GetComponent<Image>().enabled = true;
            yield return new WaitForSeconds(2f);
            GameObject.Find("YourTurn").GetComponent<Image>().enabled = false;
        }

    }
    IEnumerator WinBattle()
    {
        //turn playerturn banner on

        yield return new WaitForSeconds(2f);
    }
    IEnumerator LostBattle()
    {
        //turn playerturn banner on

        yield return new WaitForSeconds(2f);
    }

    IEnumerator EnemyTurn()
    {
        //calculate poison
        if(enemy.sanity <= 0)
        {
            state = BattleState.WON; 
            yield return new WaitForSeconds(2f);
        }
        else
        {
            yield return new WaitForSeconds(2f);
            //turn playerturn banner on
            GameObject.Find("EnemyTurn").GetComponent<Image>().enabled = true;
            yield return new WaitForSeconds(2f);
            GameObject.Find("EnemyTurn").GetComponent<Image>().enabled = false;


            //get cards
            int cards = enemyHand.Count;

            bool hasHeal = false;
            bool hasDmg = false;
            bool hasPoison = false;
            bool special = false;
            //search for unique card
            for (int i = 0; i < cards; ++i)
            {
                if (enemyHand[i].type == "unique")
                {
                    Debug.Log("Special!");
                    StartCoroutine(calculate(enemyHand[i], i));
                    special = true;
                    break; 
                }
                if (enemyHand[i].type == "heal") hasHeal = true;
                if (enemyHand[i].type == "dmg") hasDmg = true;
                if (enemyHand[i].type == "poison") hasPoison = true;
            }

            if(!special)
            {
                if(enemy.sanity <= 12 && hasHeal) 
                {
                    for (int i = 0; i < cards; ++i)
                    {
                        Debug.Log("Healing!");
                        if (enemyHand[i].type == "heal")
                        {
                            StartCoroutine(calculate(enemyHand[i], i));
                            break;
                        }
                    }
                }
                else if(enemy.sanity >= 18 && (hasDmg || hasPoison))
                {
                    Debug.Log("Attacking!"); 
                    for (int i = 0; i < cards; ++i)
                    {
                        if (enemyHand[i].type == "dmg" || enemyHand[i].type == "poison")
                        {
                            StartCoroutine(calculate(enemyHand[i], i));
                            break;
                        }
                    }
                }
                else
                {
                    Debug.Log("Random!");
                    int rand = Random.Range(0, cards); 
                    StartCoroutine(calculate(enemyHand[rand], rand)); 
                }
            }
        }
    }

    IEnumerator calculate(Card chosen, int id)
    {
        Debug.Log("Doing:"); 
        Debug.Log(chosen.damage);
        Debug.Log(chosen.heal);
        Debug.Log(chosen.poison);
        hamlet.sanity -= chosen.damage;
        enemy.sanity += chosen.heal;
        hamlet.poison += chosen.poison;
        enemyHand.RemoveAt(id);

        Debug.Log(chosen.cardName); 
        for(int i = 0; i<EnemyArea.transform.childCount; i++)
        {
            Debug.Log(EnemyArea.transform.GetChild(i).GetComponent<Card>().cardName);
            if(EnemyArea.transform.GetChild(i).GetComponent<Card>().cardName == chosen.cardName) {
                EnemyArea.transform.GetChild(i).GetComponent<Image>().sprite = chosen.cardFront;
                yield return new WaitForSeconds(2f);
                Debug.Log("DESTROYING!"); 
                Debug.Log(EnemyArea.transform.childCount);
                Destroy(EnemyArea.transform.GetChild(i).gameObject);
                Debug.Log(EnemyArea.transform.childCount);
                break; 
            }
        };

        //draw new card
        Card c = enemyDeck[Random.Range(0, enemyDeck.Count)];
        GameObject card = Instantiate(Cards, new Vector2(0, 0), Quaternion.identity);
        card.GetComponent<Card>().damage = c.damage;
        card.GetComponent<Card>().id = c.id;
        card.GetComponent<Card>().cardName = c.cardName;
        card.GetComponent<Card>().heal = c.heal;
        card.GetComponent<Card>().poison = c.poison;
        card.GetComponent<Card>().type = c.type;
        card.GetComponent<Card>().cardFront = c.cardFront;
        card.GetComponent<Card>().cardBack = c.cardBack;
        card.transform.SetParent(EnemyArea.transform, false);
        card.GetComponent<Image>().sprite = c.cardBack;
        enemyHand.Add(c);
        Debug.Log(enemyHand.Count);
        GameObject.Find("BattleSystem").GetComponent<BattleSystem>().state = BattleState.PLAYERTURN;
    }

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        hamletDeck.Add(new Card(0, "Stab", "dmg", 3, 0, 0, Stab, Back));
        hamletDeck.Add(new Card(1, "Heal", "heal", 0, 2, 0, Heal, Back));
        hamletDeck.Add(new Card(2, "Poison", "poison", 1, 0, 2, Poison, Back));

        Debug.Log(GameObject.Find("Ghost"));
        if (GameObject.Find("Ghost") != null)
        {
            enemyDeck.Add(new Card(0, "Stab", "dmg", 3, 0, 0, Stab, Back));
            enemyDeck.Add(new Card(1, "Heal", "heal", 0, 2, 0, Heal, Back));
            enemyDeck.Add(new Card(2, "Poison", "poison", 1, 0, 2, Poison, Back));
        }

    }


    void WON() { }
    void LOST() { }

    // Update is called once per frame
    void Update()
    {
        if (state != oldState)
        {
            if(state == BattleState.PLAYERTURN)
            {
                StartCoroutine(PlayerTurn());
            }
            if (state == BattleState.ENEMYTURN)
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
                card.GetComponent<Card>().type = c.type;
                card.transform.SetParent(PlayerArea.transform, false);
                card.GetComponent<Image>().sprite = c.cardFront;
                StartCoroutine(EnemyTurn());
            }
            if (state == BattleState.WON)
            {
                //StartCoroutine(WON());
            }
            if (state == BattleState.LOST)
            {
             //   StartCoroutine(LOST());
            }
        }
        oldState = state;
       // Debug.Log(state); 
    }
}
