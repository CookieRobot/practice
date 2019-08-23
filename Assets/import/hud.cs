using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hud : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;
    public GameObject text;
    int counter = 3;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void removeHeart()
    {
        switch (counter)
        {
            case 3:
                heart3.gameObject.SetActive(false);
            break;
            case 2:
                heart2.gameObject.SetActive(false);
            break;
            case 1:
                heart1.gameObject.SetActive(false);
                Text edit = text.GetComponent<Text>();
                edit.text = "Gameover Press R to restart";
            break;
            case 0:
                
            break;
        }
        counter--;
    }

}
