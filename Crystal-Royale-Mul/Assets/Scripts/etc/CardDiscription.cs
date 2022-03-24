using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDiscription : MonoBehaviour
{
    [SerializeField]
    private GameObject cost;

    [SerializeField]
    private GameObject duration;

    [SerializeField]
    private GameObject cooldown;

    [SerializeField]
    private GameObject hp;

    [SerializeField]
    private GameObject amount;

    private Card card;
    // Start is called before the first frame update
    void Start()
    {
        card = this.GetComponent<DragDrop>().card;
        if (this.GetComponent<DragDrop>().card.type == "Area")
        {
            cost.GetComponent<Text>().text = "Cost: "+card.cost.ToString("0");
            duration.GetComponent<Text>().text = "A:"+card.duration.ToString("0");
            cooldown.GetComponent<Text>().text = "CD:"+card.cooldown.ToString("0");
        }
        if (this.GetComponent<DragDrop>().card.type == "Minion")
        {
            cost.GetComponent<Text>().text = "Cost: " + card.cost.ToString("0");
            hp.GetComponent<Text>().text = "HP:" + card.health.ToString("0");
            amount.GetComponent<Text>().text = "AM:" + card.amount.ToString("0");
            cooldown.GetComponent<Text>().text = "CD:" + card.cooldown.ToString("0");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
