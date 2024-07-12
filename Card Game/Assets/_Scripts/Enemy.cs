using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static Enemy Instance;
    public int EnemyHeath;
    public int AttackDamage = 2;
    public int HealtValue = 2;
    public bool gotSilence = false;
    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        
    }
}
