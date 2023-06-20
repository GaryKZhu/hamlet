using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCards : MonoBehaviour
{
    public GameObject Card1;
    public GameObject Card2;
    public GameObject PlayerArea;
    public GameObject EnemyArea; 
    public void onClick()
    {
        for (int i = 0; i < 4; i++)
        {
            GameObject card = Instantiate(Card1, new Vector2(0, 0), Quaternion.identity);
            card.transform.SetParent(PlayerArea.transform, false);
        }
        for (int i = 0; i < 4; i++)
        {
            GameObject card = Instantiate(Card1, new Vector2(0, 0), Quaternion.identity);
            card.transform.SetParent(EnemyArea.transform, false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
