using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeltAtk2 : MonoBehaviour
{
    public float dano, TempoDeSoco;
    public float tempo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        battleManager bm = other.GetComponent<battleManager>();
        if (bm != null)
        {
            if (bm.time == 0)
            {
                tempo += Time.deltaTime;
                if (tempo >= TempoDeSoco)
                { 
                    bm.aplicadano(dano);
                    tempo = 0;
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        battleManager bm = other.GetComponent<battleManager>();
        if(bm != null){
            if(bm.time == 0){
                tempo = 0.9f;
            }
        }
    }
   /* private void OnTriggerEnter(Collider other)
    {
        tempo = 0;
    }*/
}
