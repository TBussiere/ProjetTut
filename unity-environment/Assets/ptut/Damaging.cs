using UnityEngine;
using System.Collections;

public class Damaging : MonoBehaviour {
    
    public int Dmg = 1;

    public void OnTriggerEnter2D(Collider2D o)
    {
        if (o.tag == "Player")
        {
            playerControler temp = o.GetComponent<playerControler>();

            temp.damageTaking(Dmg);
        }
    }
}
