using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magia : MonoBehaviour
{
    float speed, dano;
    int time;
    bool dieOnContact;
    float deathTime;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, deathTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    public void spawnar(float spd = 0, float dmg = 0, int team = 0, bool doc = false, float dt = 0){
        speed = spd;
        dano = dmg;
        time = team;
        deathTime = dt;
        dieOnContact = doc;
    }

    private void OnTriggerEnter(Collider other){
        if(other.GetComponent<battleManager>() != null){
            if(other.GetComponent<battleManager>().time != time){
                other.GetComponent<battleManager>().aplicadano(dano);
                if(dieOnContact)
                    Destroy(gameObject);
            }
        }else{
            if(dieOnContact)
                Destroy(gameObject);
        }
    }
}
