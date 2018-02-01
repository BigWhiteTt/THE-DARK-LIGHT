using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSphereEffect : MonoBehaviour {
    public int attack;
    private List<WolfBabyManager> list = new List<WolfBabyManager>();
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == Tags.Enemy)
        {
            WolfBabyManager wolf = other.GetComponent<WolfBabyManager>();
            int index = list.IndexOf(wolf);
            if (index == -1)
            {
                wolf.TakeDamage(attack);
                list.Add(wolf);
            }
        }
        
    }
}
