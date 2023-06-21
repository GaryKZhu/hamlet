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
    public Sprite Stab1;
    public Sprite Stab2;
    public Sprite Stab3;
    public Sprite Stab4;
    public Sprite FinalAct;
    public Sprite HoratiosVigil;
    public Sprite SpectralBlade;
    public Sprite SoulboundPact;
    public Sprite OpheliaLament;
    public Sprite DaggerofBetrayal;
    public Sprite GertrudeLove;
    public Sprite VenomousDagger;
    public Sprite SerpentsFang;

    public Sprite HamletRapier;

    public Sprite Back;
    public Sprite Heal1;
    public Sprite Heal2;
    public Sprite Heal3;
    public Sprite Heal4;
    public Sprite Poison1;
    public Sprite Poison2;

    public BattleState state; 
    public static List<Card> hamletDeck = new List<Card>();
    public static List<Card> enemyDeck = new List<Card>();
    public static List<Card> enemyHand = new List<Card>();
    public BattleState oldState; 
    public int turn = 0;
    public int enemystate = 1; 
    Player hamlet;
    Player enemy;
    public GameObject EnemyObject;
    public Sprite Ghost;
    public Sprite GhostDead;
    public Sprite Polonius;
    public Sprite PoloniusDead;
    public Sprite Claudius;
    public Sprite ClaudiusDead;
    public Sprite GhostTurn;
    public Sprite PoloniusTurn;
    public Sprite ClaudiusTurn;

    public void onClick()
    {
        state = BattleState.START; oldState = BattleState.START; 
        gameObject.GetComponent<Image>().enabled = false;
        gameObject.GetComponent<Button>().interactable = false;
        hamlet = GameObject.Find("Hamlet").GetComponent<Player>();
        enemy = GameObject.Find("Enemy").GetComponent<Player>();
        if (enemystate == 1)
        {
            EnemyObject.GetComponent<Image>().sprite = Ghost;
            EnemyObject.GetComponent<Player>().sanity = 20;
            GameObject.Find("EnemyHP").GetComponent<Slider>().maxValue = 20;
        }
        else if(enemystate == 2) {
            EnemyObject.GetComponent<Image>().sprite = Polonius;
            EnemyObject.GetComponent<Player>().sanity = 30;
            GameObject.Find("EnemyHP").GetComponent<Slider>().maxValue = 30;
        }
        else if(enemystate == 3)
        {
            EnemyObject.GetComponent<Image>().sprite = Claudius;
            EnemyObject.GetComponent<Player>().sanity = 40;
            GameObject.Find("EnemyHP").GetComponent<Slider>().maxValue = 40;
        }

        turn = 0; 
        hamlet.sanity = 20;
        hamlet.poison = 0;
        hamlet.poisonq = new List<int>();
        enemy.poison = 0;
        enemy.poisonq = new List<int>();
        enemyDeck = new List<Card>();
        hamletDeck = new List<Card>();
        enemyHand = new List<Card>();

        hamletDeck.Add(new Card(1, "Sword Strike", "dmg", 1, 0, 0, 0, 0, false, Stab1, Back));
        hamletDeck.Add(new Card(2, "Reckless Charge", "dmg", 2, 0, 0, 1, 0, false, Stab2, Back));
        hamletDeck.Add(new Card(3, "Momento Mori", "dmg", 5, 0, 0, 0, 0, false, Stab3, Back));
        hamletDeck.Add(new Card(4, "Skull Crusher", "dmg", 2, 0, 0, 0, 0, false, Stab4, Back));
        hamletDeck.Add(new Card(5, "Final Act", "unique", 4, 0, 0, 0, 1, false, FinalAct, Back));
        hamletDeck.Add(new Card(8, "HamletRapier", "unique", 3, 0, 0, 0, 0, false, HamletRapier, Back));
        hamletDeck.Add(new Card(10, "HoratiosVigil", "unique", 4, 0, 0, 0, 0, false, HoratiosVigil, Back));

        hamletDeck.Add(new Card(12, "Remedy of Serenity", "heal", 0, 1, 0, 0, 0, false, Heal1, Back));
        hamletDeck.Add(new Card(13, "Respite of the Woods", "heal", 0, 2, 0, 0, 0, false, Heal2, Back));
        hamletDeck.Add(new Card(15, "Solace of Soliloquy", "heal", 0, 3, 0, 0, 0, false, Heal3, Back));
        hamletDeck.Add(new Card(16, "Restorative Elixir", "heal", 0, 2, 0, 0, 0, true, Heal4, Back));

        hamletDeck.Add(new Card(19, "Envenomed Blade", "poison", 1, 0, 2, 0, 0, false, Poison1, Back));
        hamletDeck.Add(new Card(20, "Plague of Madness", "poison", 0, 0, 4, 0, 2, false, Poison2, Back)); 



        Debug.Log(enemystate);
        if (enemystate == 1)
        {
            enemyDeck.Add(new Card(1, "Sword Strike", "dmg", 1, 0, 0, 0, 0, false, Stab1, Back));
            enemyDeck.Add(new Card(2, "Reckless Charge", "dmg", 2, 0, 0, 1, 0, false, Stab2, Back));
            enemyDeck.Add(new Card(3, "Momento Mori", "dmg", 5, 0, 0, 0, 0, false, Stab3, Back));
            enemyDeck.Add(new Card(4, "Skull Crusher", "dmg", 2, 0, 0, 0, 0, false, Stab4, Back));
            enemyDeck.Add(new Card(12, "Remedy of Serenity", "heal", 0, 1, 0, 0, 0, false, Heal1, Back));
            enemyDeck.Add(new Card(13, "Respite of the Woods", "heal", 0, 2, 0, 0, 0, false, Heal2, Back));
            enemyDeck.Add(new Card(15, "Solace of Soliloquy", "heal", 0, 3, 0, 0, 0, false, Heal3, Back));
            enemyDeck.Add(new Card(16, "Restorative Elixir", "heal", 0, 2, 0, 0, 0, true, Heal4, Back));

            enemyDeck.Add(new Card(19, "Envenomed Blade", "poison", 1, 0, 2, 0, 0, false, Poison1, Back));
            enemyDeck.Add(new Card(20, "Plague of Madness", "poison", 0, 0, 4, 0, 2, false, Poison2, Back));

            enemyDeck.Add(new Card(6, "Spectral Blade", "unique", 2, 0, 0, 0, 0, false, SpectralBlade, Back));
            enemyDeck.Add(new Card(7, "Soulbound Pact", "unique", 4, 0, 0, 2, 0, false, SoulboundPact, Back));
            GameObject.Find("EnemyTurn").GetComponent<Image>().sprite = GhostTurn;


        }

        if (enemystate == 2)
        {
            enemyDeck.Add(new Card(1, "Sword Strike", "dmg", 1, 0, 0, 0, 0, false, Stab1, Back));
            enemyDeck.Add(new Card(2, "Reckless Charge", "dmg", 2, 0, 0, 1, 0, false, Stab2, Back));
            enemyDeck.Add(new Card(3, "Momento Mori", "dmg", 5, 0, 0, 0, 0, false, Stab3, Back));
            enemyDeck.Add(new Card(4, "Skull Crusher", "dmg", 2, 0, 0, 0, 0, false, Stab4, Back));
            enemyDeck.Add(new Card(12, "Remedy of Serenity", "heal", 0, 1, 0, 0, 0, false, Heal1, Back));
            enemyDeck.Add(new Card(13, "Respite of the Woods", "heal", 0, 2, 0, 0, 0, false, Heal2, Back));
            enemyDeck.Add(new Card(15, "Solace of Soliloquy", "heal", 0, 3, 0, 0, 0, false, Heal3, Back));
            enemyDeck.Add(new Card(16, "Restorative Elixir", "heal", 0, 2, 0, 0, 0, true, Heal4, Back));
            enemyDeck.Add(new Card(19, "Envenomed Blade", "poison", 1, 0, 2, 0, 0, false, Poison1, Back));
            enemyDeck.Add(new Card(20, "Plague of Madness", "poison", 0, 0, 4, 0, 2, false, Poison2, Back));

            enemyDeck.Add(new Card(9, "Ophelia's Lament", "unique", 3, 1, 0, 0, 0, false, OpheliaLament, Back));
            GameObject.Find("EnemyTurn").GetComponent<Image>().sprite = PoloniusTurn;


        }
        if (enemystate == 3)
        {
            enemyDeck.Add(new Card(1, "Sword Strike", "dmg", 1, 0, 0, 0, 0, false, Stab1, Back));
            enemyDeck.Add(new Card(2, "Reckless Charge", "dmg", 2, 0, 0, 1, 0, false, Stab2, Back));
            enemyDeck.Add(new Card(3, "Momento Mori", "dmg", 5, 0, 0, 0, 0, false, Stab3, Back));
            enemyDeck.Add(new Card(4, "Skull Crusher", "dmg", 2, 0, 0, 0, 0, false, Stab4, Back));
            enemyDeck.Add(new Card(12, "Remedy of Serenity", "heal", 0, 1, 0, 0, 0, false, Heal1, Back));
            enemyDeck.Add(new Card(13, "Respite of the Woods", "heal", 0, 2, 0, 0, 0, false, Heal2, Back));
            enemyDeck.Add(new Card(15, "Solace of Soliloquy", "heal", 0, 3, 0, 0, 0, false, Heal3, Back));
            enemyDeck.Add(new Card(16, "Restorative Elixir", "heal", 0, 2, 0, 0, 0, true, Heal4, Back));
            enemyDeck.Add(new Card(19, "Envenomed Blade", "poison", 1, 0, 2, 0, 0, false, Poison1, Back));
            enemyDeck.Add(new Card(20, "Plague of Madness", "poison", 0, 0, 4, 0, 2, false, Poison2, Back));

            enemyDeck.Add(new Card(11, "Dagger of Betrayal", "unique", 4, 0, 0, 0, 0, false, DaggerofBetrayal, Back));
            enemyDeck.Add(new Card(13, "Gertrude's Love", "heal", 0, 4, 0, 0, 0, false, GertrudeLove, Back));
            enemyDeck.Add(new Card(17, "Venomous Dagger", "unique", 2, 0, 2, 0, 0, false, VenomousDagger, Back));
            enemyDeck.Add(new Card(18, "Serpent's Fang", "unique", 1, 0, 1, 0, 0, false, SerpentsFang, Back));
            GameObject.Find("EnemyTurn").GetComponent<Image>().sprite = ClaudiusTurn;
        }

        if (EnemyArea.transform.childCount > 0) {
            for (int i = EnemyArea.transform.childCount-1; i >= 0; i--)
            {
                Destroy(EnemyArea.transform.GetChild(i).gameObject);
            };
        }
        if (PlayerArea.transform.childCount > 0)
        {
            for (int i = PlayerArea.transform.childCount-1; i >= 0; i--)
            {
                Destroy(PlayerArea.transform.GetChild(i).gameObject);
            };
        }


        Debug.Log(enemyDeck.Count);
        Debug.Log(hamletDeck.Count); 
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
            card.GetComponent<Card>().spoison = c.spoison;
            card.GetComponent<Card>().sdamage = c.sdamage;
            card.GetComponent<Card>().purify = c.purify;
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
            card.GetComponent<Card>().spoison = c.spoison;
            card.GetComponent<Card>().sdamage = c.sdamage;
            card.GetComponent<Card>().purify = c.purify;
            card.GetComponent<Card>().cardFront = c.cardFront;
            card.GetComponent<Card>().cardBack = c.cardBack;
            card.transform.SetParent(EnemyArea.transform, false);
            card.GetComponent<Image>().sprite = c.cardBack;
            enemyHand.Add(c); 
        }
        StartCoroutine(PlayerTurn());
    }

    IEnumerator PlayerTurn()
    {
        //calculate poison
        hamlet.poisonq.Sort();
        if (hamlet.poisonq.Count > 0 && hamlet.poisonq[0] <= turn)
        {
            Debug.Log("Current Damage:"); Debug.Log(hamlet.poison); Debug.Log(hamlet.poisonq[0]);
            hamlet.poison--;
            hamlet.poisonq.RemoveAt(0);
        }
        hamlet.sanity -= hamlet.poison;
        Debug.Log("Poison Count: Hamlet");
        Debug.Log(hamlet.poisonq.Count);
        if (hamlet.poisonq.Count > 0) Debug.Log(hamlet.poisonq[0]);
        Debug.Log("Current Turn:"); Debug.Log(turn);
        if(hamlet.sanity <= 0)
        {
            state = BattleState.LOST;
            yield return new WaitForSeconds(2f);
        }
        else
        {
            //turn playerturn banner on
            yield return new WaitForSeconds(1f);
            GameObject.Find("YourTurn").GetComponent<Image>().enabled = true;
            yield return new WaitForSeconds(2f);
            GameObject.Find("YourTurn").GetComponent<Image>().enabled = false;
            state = BattleState.PLAYERTURN;
        }

    }
    IEnumerator WinBattle()
    {
        //turn playerturn banner on
        if(enemystate == 1)
        {
            EnemyObject.GetComponent<Image>().sprite = GhostDead;
        }
        if (enemystate == 2)
        {
            EnemyObject.GetComponent<Image>().sprite = PoloniusDead;
        }
        if (enemystate == 3)
        {
            EnemyObject.GetComponent<Image>().sprite = ClaudiusDead;
        }


        yield return new WaitForSeconds(1f);
        GameObject.Find("Win").GetComponent<Image>().enabled = true;
        yield return new WaitForSeconds(1f);
        GameObject.Find("Win").GetComponent<Image>().enabled = false;
        enemystate++;
        enemystate = Mathf.Min(3, enemystate);
        gameObject.GetComponent<Image>().enabled = true;
        gameObject.GetComponent<Button>().interactable = true;
    }

    IEnumerator LostBattle()
    {
        //turn lose banner on

        yield return new WaitForSeconds(1f);
        GameObject.Find("Lose").GetComponent<Image>().enabled = true;
        yield return new WaitForSeconds(1f);
        GameObject.Find("Lose").GetComponent<Image>().enabled = false;
        enemystate = 1;
        gameObject.GetComponent<Image>().enabled = true;
        gameObject.GetComponent<Button>().interactable = true;
    }

    IEnumerator EnemyTurn()
    {
        if (enemy.poisonq.Count > 0 && enemy.poisonq[0] <= turn)
        {
            enemy.poison--;
            enemy.poisonq.RemoveAt(0);
        }
        enemy.sanity -= enemy.poison;
        enemy.poisonq.Sort();
        Debug.Log(enemy.poisonq.Count);
        if (enemy.poisonq.Count > 0) Debug.Log(enemy.poisonq[0]);
        Debug.Log("Current Turn:"); Debug.Log(turn);
        if (enemy.poisonq.Count > 0 && enemy.poisonq[0] == turn)
        {
            Debug.Log("Current Damage:"); Debug.Log(enemy.poison); Debug.Log(enemy.poisonq[0]);
            enemy.poison--;
            enemy.poisonq.RemoveAt(0);
        }
        //calculate poison
        if (enemy.sanity <= 0)
        {
            state = BattleState.WON; 
            yield return new WaitForSeconds(1f);
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
                   // Debug.Log("Special!");
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
                      //  Debug.Log("Healing!");
                        if (enemyHand[i].type == "heal")
                        {
                            StartCoroutine(calculate(enemyHand[i], i));
                            break;
                        }
                    }
                }
                else if(enemy.sanity >= 18 && (hasDmg || hasPoison))
                {
                 //   Debug.Log("Attacking!"); 
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
                 //   Debug.Log("Random!");
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

        if (chosen.purify || chosen.id == 16)
        {
            enemy.poison = 0;
            enemy.poisonq.Clear();
        }

        if (chosen.poison > 0) hamlet.poison++;
        if(chosen.poison > 0) hamlet.poisonq.Add(chosen.poison + turn);

        if (chosen.spoison > 0) enemy.poison++;
        if (chosen.spoison > 0) enemy.poisonq.Add(chosen.spoison + GameObject.Find("BattleSystem").GetComponent<BattleSystem>().turn);
        enemy.sanity -= chosen.sdamage;


        enemyHand.RemoveAt(id);

   //     Debug.Log(chosen.cardName); 
        for(int i = 0; i<EnemyArea.transform.childCount; i++)
        {
         //   Debug.Log(EnemyArea.transform.GetChild(i).GetComponent<Card>().cardName);
            if(EnemyArea.transform.GetChild(i).GetComponent<Card>().cardName == chosen.cardName) {
                EnemyArea.transform.GetChild(i).GetComponent<Image>().sprite = chosen.cardFront;
                yield return new WaitForSeconds(2f);
            ///    Debug.Log("DESTROYING!"); 
          //      Debug.Log(EnemyArea.transform.childCount);
                Destroy(EnemyArea.transform.GetChild(i).gameObject);
           //     Debug.Log(EnemyArea.transform.childCount);
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
        card.GetComponent<Card>().spoison = c.spoison;
        card.GetComponent<Card>().sdamage = c.sdamage;
        card.GetComponent<Card>().purify = c.purify;
        card.GetComponent<Card>().cardFront = c.cardFront;
        card.GetComponent<Card>().cardBack = c.cardBack;
        card.transform.SetParent(EnemyArea.transform, false);
        card.GetComponent<Image>().sprite = c.cardBack;
        enemyHand.Add(c);
        //   Debug.Log(enemyHand.Count);
        StartCoroutine(PlayerTurn());

        //GameObject.Find("BattleSystem").GetComponent<BattleSystem>().state = BattleState.PLAYERTURN;
        turn++;
    }

    // Start is called before the first frame update
    void Start()
    {

    }



    // Update is called once per frame
    void Update()
    {
        if (state != oldState)
        {
           /* if(state == BattleState.PLAYERTURN)
            {
                StartCoroutine(PlayerTurn());
            }*/ 
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
                card.GetComponent<Card>().spoison = c.spoison;
                card.GetComponent<Card>().sdamage = c.sdamage;
                card.GetComponent<Card>().purify = c.purify;
                card.transform.SetParent(PlayerArea.transform, false);
                card.GetComponent<Image>().sprite = c.cardFront;
                StartCoroutine(EnemyTurn());
            }
            if (state == BattleState.WON)
            {
                StartCoroutine(WinBattle());
            }
            if (state == BattleState.LOST)
            {
                StartCoroutine(LostBattle());
            }
        }
        oldState = state;
       // Debug.Log(state); 
    }
}
