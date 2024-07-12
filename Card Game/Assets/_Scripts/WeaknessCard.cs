using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaknessCard : MonoBehaviour, ICards
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
            PlayerMovement.instance.AttackDamage = 1;
        }
        if (RoundManager.instance.GeneralTurn == RoundManager.Turn.PlayerTurn)
        {
            Enemy.Instance.AttackDamage = 1;
        }
    }

    public Transform TransformInformations()
    {
       return transform;
    }
}
