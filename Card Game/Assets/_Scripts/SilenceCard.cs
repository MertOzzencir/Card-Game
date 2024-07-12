using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilenceCard : MonoBehaviour,ICards
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
            PlayerMovement.instance.gotSilence = true;
        }
        if (RoundManager.instance.GeneralTurn == RoundManager.Turn.PlayerTurn)
        {
            Enemy.Instance.gotSilence = true;
        }
    }

    public Transform TransformInformations()
    {
        return transform;
    }
}
