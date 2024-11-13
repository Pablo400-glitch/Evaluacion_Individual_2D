using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static PlayerMovement;

public class Subscriptor : MonoBehaviour
{
    public PlayerMovement player;
    public Text itemCounterText;
    public Text Health;

    void Start()
    {
        player.onItemCollected += UpdateItemCounter;
        player.onHealthLost += UpdateHealthCounter;
        Health.text = "Health " + player.playerHealth;
    }

    void UpdateItemCounter(int totalItemsCollected)
    {
        itemCounterText.text = "Items Collected: " + totalItemsCollected.ToString();
    }

    void UpdateHealthCounter(int healthLost)
    {
        Health.text = "Health " + healthLost;
    }

    void OnDestroy()
    {
        player.onItemCollected -= UpdateItemCounter;
        player.onHealthLost -= UpdateHealthCounter;
    }
}
