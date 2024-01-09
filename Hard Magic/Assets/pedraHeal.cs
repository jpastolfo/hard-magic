using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pedraHeal : MonoBehaviour
{
    GameObject boss;
    public float healFreq = 0.2f, healQuant = 3f;
    float healtimer = 0;
    // Start is called before the first frame update
    void Start()
    {
        boss = GameObject.Find("boss");
    }

    // Update is called once per frame
    void Update()
    {
        if(healtimer >= healFreq){
            healtimer = 0;
            boss.GetComponent<battleManager>().aplicadano(-1 *healQuant);
        }
        healtimer += Time.deltaTime;
    }

    private void OnCollisionEnter(Collision other) {
        Debug.Log("aa");
        boss.GetComponent<boss>().achaLugarPraPedra(gameObject);
    }
}
