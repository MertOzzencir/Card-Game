using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;

public class BlockCard : MonoBehaviour,ICards
{
    public int Manacost;
    public string TheNameOfCard;
    public string CardName()
    {
        return TheNameOfCard;
    }
    public int ManaCost()
    {
        return Manacost;
    }

    public void Perform()
    {
        if (RoundManager.instance.GeneralTurn == RoundManager.Turn.EnemyTurn)
        {
            Enemy.Instance.EnemyHeath += Enemy.Instance.HealtValue;
        }
        if (RoundManager.instance.GeneralTurn == RoundManager.Turn.PlayerTurn)
        {
            PlayerMovement.instance.PlayerHealth += PlayerMovement.instance.HealtValue;
            
        }
    }

    public Transform TransformInformations()
    {
        return transform;
    }
}
