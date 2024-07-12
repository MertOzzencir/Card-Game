using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public enum Turn
    {
        EnemyTurn,
        PlayerTurn
    }
    public static event Action<bool> OnTurn;
    public static RoundManager instance;
    public List<PlayerCounter> playerCounters = new List<PlayerCounter>();
    public List<PlayerCounter> enemyCounters = new List<PlayerCounter>();
    public Turn GeneralTurn;
    public List<ICards> cardsOnCounter = new List<ICards>();
   
    public Queue<ICards> queue = new Queue<ICards>();
   
      
    public int TotalMana;
    public int manaHolder;
    public int manaHolderInstance;
    int manaCheckerForEnemy;
    bool canClear;
    bool enemyBrain = true;
    bool enemyCanStop;
    int counterChecker=0;
    int enemyCardDrawTry;
    int reduceSizeOfQueue;
    
   
    ICards[] tempCards;
    private void Awake()
    {
        instance = this;
      
        int firstTurn = UnityEngine.Random.Range(0, 2);
        if(firstTurn == 0)
        {
            GeneralTurn = Turn.EnemyTurn;
        }
        else
        {
            GeneralTurn = Turn.PlayerTurn;
        }
    }
    private void Start()
    {
        SpaceWorldUIController.instance.TurnSign(GeneralTurn);

    }
    private void Update()
    {
        if (GeneralTurn == Turn.PlayerTurn && !PlayerMovement.instance.gotSilence)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                SetCardsForPlayer();
                Perform(Turn.EnemyTurn);
                ResetTheTempCards();
            }

        }
        else
        {
            PlayerMovement.instance.gotSilence = false;
            SetTurns(Turn.EnemyTurn, false);

        }
       
        if(GeneralTurn == Turn.EnemyTurn &&  !Enemy.Instance.gotSilence)
        {

            if (!enemyCanStop)
            {
                Debug.Log("Enemy Turn");
                enemyCanStop = true;
                enemyBrain = true;
                StartCoroutine(DestroyEnemyCard());
            }

        }
        else
        {
            Enemy.Instance.gotSilence = false;
            SetTurns(Turn.PlayerTurn, false);
        }
      

    }
    IEnumerator DestroyEnemyCard()
    {
        yield return null;
        DestroyCards(enemyCounters);
        StartCoroutine(EnemyBrain());

    }
    
    IEnumerator EnemyBrain()
    {
        yield return new WaitForSeconds(1.5f);
       
       
        
        while (enemyBrain)
        {
           
            
            for (int i = 0; i < enemyCounters.Count + counterChecker; i++)
            {
             
                ICards[] tempCards = queue.ToArray();
                int randomIndex = UnityEngine.Random.Range(0,tempCards.Length + reduceSizeOfQueue);
                ICards selectedCard = tempCards[randomIndex];
                manaCheckerForEnemy += selectedCard.ManaCost();
                cardsOnCounter.Add(selectedCard);
                reduceSizeOfQueue--;
                Queue<ICards> newQueue = new Queue<ICards>();


                for (int t = 0; t < tempCards.Length; t++)
                {
                    if (t != randomIndex) 
                    {
                        newQueue.Enqueue(tempCards[t]);
                    }
                }
                newQueue.Enqueue(selectedCard);
                queue.Clear();
                 foreach(ICards cards in newQueue)
                {
                    queue.Enqueue(cards);

                }
                 newQueue.Clear(); 
            }
            
            enemyCardDrawTry++;
            if (manaCheckerForEnemy <= TotalMana)
            {
                tempCards = queue.ToArray();
                queue.Clear();
                for (int i = 0; i < tempCards.Length + reduceSizeOfQueue; i++)
                {
                    queue.Enqueue(tempCards[i]);

                }

                enemyBrain = false;
                for (int i = 0; i < enemyCounters.Count + counterChecker; i++)
                {
                    
                    cardsOnCounter[i].TransformInformations().position = enemyCounters[i].transform.position + new Vector3(0, 0.1f, 0);
                    
                    yield return new WaitForSeconds(1.5f);
                }
                Perform(Turn.PlayerTurn);
                
               
                ResetTheTempCards();
                enemyCanStop = false;

            }
            else
            {

                if (queue.Count < 5 || enemyCardDrawTry > 5)
                {
                    counterChecker = -1;
                    enemyCardDrawTry = 0;
                }
                else
                {
                    counterChecker = 0;
                }
                ResetTheTempCards();
            }
            reduceSizeOfQueue = 0;
            manaCheckerForEnemy = 0;

            yield return null;
        }
    }

    public void SetTurns(Turn turn,bool canShuffle)
    {
        GeneralTurn = turn;
        SpaceWorldUIController.instance.TurnSign(GeneralTurn);
        OnTurn?.Invoke(canShuffle);
      

    }

    void Perform(Turn turn)
    {
        foreach (ICards card in cardsOnCounter)
        {
            manaHolder += card.ManaCost();
            canClear = false;

        }
        if (manaHolder <= TotalMana)
        {
            foreach (ICards card in cardsOnCounter)
            {
                card.Perform();
                canClear = true;


            }
            SetTurns(turn,true);


        }
        manaHolder = 0;


        if (canClear)
        {
            
            DestroyCards(playerCounters);
        }
        

    }
    void ResetTheTempCards()
    {
        cardsOnCounter.Clear();
    }
   

    public void SetCardsForPlayer()
    {
        for (int i = 0; i < playerCounters.Count; i++)
        {
            PlayerCounter playerCounter = playerCounters[i].GetComponent<PlayerCounter>();
            if (playerCounter._objectHolder != null)
            {
                Debug.Log(playerCounters[i] + " has " + playerCounter._objectHolder);
                cardsOnCounter.Add(playerCounter._objectHolder.GetComponent<ICards>());
                
            }
        }
    }

    public void DestroyCards(List<PlayerCounter> counters)
    {
       for (int i = 0; i < counters.Count; i++)
        {
            PlayerCounter playerCounter = counters[i].GetComponent<PlayerCounter>();
            if (playerCounter._objectHolder != null)
            {
                
                Destroy(playerCounter._objectHolder);
            }
        }
    }
}
