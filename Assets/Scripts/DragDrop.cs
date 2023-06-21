using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop : MonoBehaviour
{
    public GameObject Canvas;


    private bool isDragging = false;
    private GameObject startParent;
    private Vector2 startPosition;
    private GameObject dropZone;
    private bool isOverDropZone;
    private string collidedwith;
    private BattleState state;  

    // Start is called before the first frame update
    void Start()
    {
        Canvas = GameObject.Find("Main Canvas");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Colliding!");
        isOverDropZone = true;
        collidedwith = collision.gameObject.name; 
        dropZone = collision.gameObject;

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("Uncolliding!");
        isOverDropZone = false;
        collidedwith = ""; 
        dropZone = null;

    }

    public void startDrag()
    {
        if(state == BattleState.PLAYERTURN) {
            isDragging = true;
            startParent = transform.parent.gameObject;
            startPosition = transform.position;
        }
    }

    public void EndDrag()
    {
        if (state == BattleState.PLAYERTURN)
        {
            isDragging = false;
            if (isOverDropZone)
            {
                transform.SetParent(dropZone.transform, false);
                // calculate card effects here
                GameObject entity = GameObject.Find(collidedwith);
                calculate(entity, gameObject.GetComponent<Card>());
                
            }
            else
            {
                transform.position = startPosition;
                transform.SetParent(startParent.transform, false);
            }
        }
    }

    void calculate(GameObject entity, Card c) 
    {
        Debug.Log("Doing:");
        Debug.Log(c.damage);
        Debug.Log(c.heal);
        Debug.Log(c.poison);
        entity.GetComponent<Player>().sanity -= c.damage;
        entity.GetComponent<Player>().sanity += c.heal;
        if(c.poison > 0) entity.GetComponent<Player>().poison++;
        if(c.poison > 0) entity.GetComponent<Player>().poisonq.Add(c.poison + GameObject.Find("BattleSystem").GetComponent<BattleSystem>().turn);

        
        Debug.Log("Self Poison:");
        Debug.Log(c.spoison);
        Debug.Log(c.cardName); 
        Player hamlet = GameObject.Find("Hamlet").GetComponent<Player>();
        if (c.spoison > 0)
        {

            hamlet.poison++;
        }
        if (c.spoison > 0) hamlet.poisonq.Add(c.spoison + GameObject.Find("BattleSystem").GetComponent<BattleSystem>().turn);

        hamlet.sanity -= c.sdamage;
        if(c.purify)
        {
            hamlet.poison = 0;
            hamlet.poisonq.Clear();
        }

        Destroy(gameObject, 2);
        GameObject.Find("BattleSystem").GetComponent<BattleSystem>().state = BattleState.ENEMYTURN;                
    }

    // Update is called once per frame
    void Update()
    {
        if(isDragging)
        {
            transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            transform.SetParent(Canvas.transform, true);
        }
        state = GameObject.Find("BattleSystem").GetComponent<BattleSystem>().state;
    }
}
