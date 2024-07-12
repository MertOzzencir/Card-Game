using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShuffleCards : MonoBehaviour
{
    public GameObject[] cards;
    public GameObject enemyShufflePoint;
    public int cardsToShuffle;

    private void Start()
    {
        ShuffleTheCards(true);
        RoundManager.OnTurn += ShuffleTheCards;
    }
    public void ShuffleTheCards(bool canShuffle)
    {
        if (canShuffle)
        {

     
        if(RoundManager.instance.GeneralTurn == RoundManager.Turn.PlayerTurn)
        {
            for(int i = 0;i< cardsToShuffle; i++)
            {
                Debug.Log("Shuffled for Player");
                int randomIndex = UnityEngine.Random.Range(0,cards.Length);
                GameObject go = Instantiate(cards[randomIndex]);
                go.transform.position = new Vector3(transform.position.x+i,.2f + 0.05f,transform.position.z);
            }
        }

        if(RoundManager.instance.GeneralTurn == RoundManager.Turn.EnemyTurn)
        {
            for (int i = 0; i < cardsToShuffle; i++)
            {
                Debug.Log("Shuffled for Enemy");
                int randomIndex = UnityEngine.Random.Range(0, cards.Length);
                GameObject go = Instantiate(cards[randomIndex]);
                go.layer = 9;
                go.transform.position = new Vector3(enemyShufflePoint.transform.position.x+i + 0.05f, .2f, enemyShufflePoint.transform.position.z);
                RoundManager.instance.queue.Enqueue(go.GetComponent<ICards>());
                
                Debug.Log("Selected Card for Enemy: " + cards[randomIndex]);
                
            }

        }
        }

    }
}
