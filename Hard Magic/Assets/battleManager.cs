using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class battleManager : MonoBehaviour
{
    public float vida, mana, manaPS;
    float vidaatual, manaAtual;
    public int time;
    GameObject hbfiller, prefabDmgText, manafiller, loadfiller;
    bool castando;
    // Start is called before the first frame update
    void Start()
    {
        hbfiller = transform.Find("Canvas").Find("HealthBar").Find("hbfiller").gameObject;
        manafiller = transform.Find("Canvas").Find("Manabar").Find("manafiller").gameObject;
        loadfiller = transform.Find("Canvas").Find("Manabar").Find("filler").gameObject;
        vidaatual = vida;
        prefabDmgText = Resources.Load<GameObject>("dmgCanvas");
    }

    // Update is called once per frame
    void Update()
    {
        if(!castando){
            manaAtual += manaPS;
            if(manaAtual > mana){
                manaAtual = mana;
            }
        }
    }

    public void aplicadano(float dano){
        vidaatual -= dano;
        if(hbfiller){
            hbfiller.GetComponent<Image>().fillAmount = vidaatual/vida;
        }
        GameObject dmgText = Instantiate(prefabDmgText);
        dmgText.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = ""+dano;
        dmgText.transform.position = transform.position + Vector3.up;
        if(vidaatual <= 0 && time != 0){
            Destroy(gameObject);
        }
    }

    public void gastarMana(float n){
        manaAtual -= n;
        if(manaAtual < 0){
            manaAtual = 0;
        }
        manafiller.GetComponent<Image>().fillAmount = manaAtual/mana;
    }
}
