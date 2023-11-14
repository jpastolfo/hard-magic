using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class battleManager : MonoBehaviour
{
    public float vida;
    float vidaatual;
    public int time;
    GameObject hbfiller;
    // Start is called before the first frame update
    void Start()
    {
        hbfiller = transform.Find("Canvas").Find("HealthBar").Find("hbfiller").gameObject;
        vidaatual = vida;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void aplicadano(float dano){
        vidaatual -= dano;
        if(hbfiller){
            hbfiller.GetComponent<Image>().fillAmount = vidaatual/vida;
        }
        if(vidaatual <= 0 && time != 0){
            Destroy(gameObject);
        }
        //Debug.Log("aaa");
    }
}
