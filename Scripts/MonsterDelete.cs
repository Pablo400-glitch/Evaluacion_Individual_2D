using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDelete : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D weapon)
    {
        if (weapon.gameObject.CompareTag("Weapon"))
        {
            Destroy(gameObject);
        }
    }
}
