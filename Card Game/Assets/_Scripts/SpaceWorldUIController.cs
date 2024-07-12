using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class SpaceWorldUIController : MonoBehaviour
{
    public static SpaceWorldUIController instance;
    public GameObject _turnSignPlayer;
    public GameObject _turnSignEnemy;
    public GameObject PlayerManaUsed;
    public GameObject EnemyManaUsed;
    public GameObject PlayerHealth;
    public GameObject EnemyHealth;
    TextMeshProUGUI _playerManaUsedText;
    TextMeshProUGUI _enemyManaUsedText;
    TextMeshProUGUI _playerHealth;
    TextMeshProUGUI _enemyText;
    List<int>_playerManaUsed;
    List<int> _enemyManaUsed;
    public PlayerCounter[] counters;
   
    void Awake()
    {
        instance = this;
        _playerManaUsed = new List<int>(new int[10]);
        _enemyManaUsed = new List<int>(new int[10]);


        _playerManaUsedText = PlayerManaUsed.GetComponent<TextMeshProUGUI>();
        _enemyManaUsedText = EnemyManaUsed.GetComponent<TextMeshProUGUI>();
        _playerHealth = PlayerHealth.GetComponent<TextMeshProUGUI>();   
         _enemyText = EnemyHealth.GetComponent<TextMeshProUGUI>();

    }

    // Update is called once per frame
    void Update()
    {
        ManaHealUI();
        _enemyManaUsedText.text = _enemyManaUsed.Sum().ToString();


    }
    public void ManaChanged(int index,int value,bool PlayerCounterAction)
    {
        if(PlayerCounterAction)
            _playerManaUsed[index] = value;

        else if(!PlayerCounterAction)
        {
            _enemyManaUsed[index] = value;
        }
       
    }
    void ManaHealUI()
    {

        if (_playerManaUsed.Sum() > RoundManager.instance.TotalMana)
            _playerManaUsedText.color = Color.red;
        else
            _playerManaUsedText.color = Color.white;
        _playerManaUsedText.text = _playerManaUsed.Sum().ToString();
        _playerHealth.text = PlayerMovement.instance.PlayerHealth.ToString();
        _enemyText.text = Enemy.Instance.EnemyHeath.ToString();
    }
    public void TurnSign(RoundManager.Turn turn)
    {
        switch(turn)
        {
            case RoundManager.Turn.EnemyTurn:
                _turnSignPlayer.SetActive(false);
                _turnSignEnemy.SetActive(true);
                break;

            case RoundManager.Turn.PlayerTurn:
                _turnSignPlayer.SetActive(true);
                _turnSignEnemy.SetActive(false);

                break;
            
        }
    }

    


}
