using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class avisaObossqueNaoEncaixa : MonoBehaviour
{
    GameObject boss;
    // Start is called before the first frame update
    void Start()
    {
        boss = GameObject.Find("boss");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log("aaa");
        if(!other.gameObject.GetComponent<battleManager>()){
            boss.GetComponent<boss>().naoEncaixa();
            Debug.Log("bbb");
            Destroy(gameObject);
        }
    }
}
