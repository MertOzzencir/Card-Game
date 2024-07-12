using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AttackCard : MonoBehaviour,ICards
{
    public int Manacost;
    public string TheNameOfCard;
    public TextMeshProUGUI AttackValue;
    private void Update()
    {
        if(gameObject.layer == 6)
        {
            AttackValue.text = "+" + PlayerMovement.instance.AttackDamage + " ATTACK";
        }
        else
        {
            AttackValue.text = "+" + Enemy.Instance.AttackDamage + " ATTACK";
        }
    }
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
        if(RoundManager.instance.GeneralTurn == RoundManager.Turn.EnemyTurn)
        {
            PlayerMovement.instance.PlayerHealth -= Enemy.Instance.AttackDamage ;
        }
        if (RoundManager.instance.GeneralTurn == RoundManager.Turn.PlayerTurn)
        {
            Enemy.Instance.EnemyHeath -= PlayerMovement.instance.AttackDamage;
        }
    }

    public Transform TransformInformations()
    {
        return transform;
    }
}
