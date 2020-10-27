/*10-24-2020
 * EnemyData.cs
 * New Enemy Data will be made from here
 * call data from here use EnemyData = Data;
 */
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Enemy Data", menuName = "Scriptables/EnemyData", order = 1)]
public class EnemyData : ScriptableObject
{
    //All EnemyData is Here for referance
    public string enemyName;
    [TextArea]public string description;
    public GameObject gameModel;
    public int health = 20;
    public float speed = 2f;
    public float detectRange = 10f;
    public float weaponDamage = 1f;
}
