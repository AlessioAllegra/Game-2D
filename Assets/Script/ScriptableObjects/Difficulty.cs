using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Difficulty", order = 2)]
public class Difficulty : ScriptableObject
{
    public new string name;
    public float playerHealth;    
}
