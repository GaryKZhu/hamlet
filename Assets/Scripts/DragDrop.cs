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
        isDragging = true;
        startParent = transform.parent.gameObject;
        startPosition = transform.position;
    }

    public void EndDrag()
    {
        isDragging = false;
        if(isOverDropZone)
        {
            transform.SetParent(dropZone.transform, false);
            // calculate card effects here
            GameObject entity = GameObject.Find(collidedwith);
            entity.GetComponent<Player>().sanity -= gameObject.GetComponent<Card>().damage;
            entity.GetComponent<Player>().sanity += gameObject.GetComponent<Card>().heal;
            entity.GetComponent<Player>().sanity = Mathf.Min(entity.GetComponent<Player>().sanity, 20); 
            entity.GetComponent<Player>().poison += gameObject.GetComponent<Card>().poison; 
            Destroy(gameObject, 5);
        }
        else
        {
            transform.position = startPosition;
            transform.SetParent(startParent.transform, false); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isDragging)
        {
            transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            transform.SetParent(Canvas.transform, true);
        }
    }
}
