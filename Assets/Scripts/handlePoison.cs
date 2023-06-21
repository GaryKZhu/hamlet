using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class handlePoison : MonoBehaviour
{

    public GameObject entity;
    public GameObject poisonicon; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(entity.GetComponent<Player>().poison == 0)
        {
            poisonicon.GetComponent<Image>().enabled = false;
            poisonicon.transform.GetChild(0).GetComponent<Text>().text = ""; 
        }
        else
        {
            poisonicon.GetComponent<Image>().enabled = true;
            poisonicon.transform.GetChild(0).GetComponent<Text>().text = entity.GetComponent<Player>().poison.ToString(); 
        }
    }
}
