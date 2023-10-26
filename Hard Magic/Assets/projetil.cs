using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projetil : MonoBehaviour
{
    float speed, dano;
    int time;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    public void spawnar(float spd, float dmg, int team){
        speed = spd;
        dano = dmg;
        time = team;
    }

    private void OnTriggerEnter(Collider other){
        if(other.GetComponent<battleManager>() != null){
            other.GetComponent<battleManager>().aplicadano(dano);
        }
        Destroy(gameObject);
    }
}
