using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class battleManager : MonoBehaviour
{
    public float vida, mana = 100, manaPS = 2, manaCastPS = 2;
    float vidaatual, manaAtual = 0, manaCast = 0;
    public int time;
    GameObject hbfiller, prefabDmgText, manafiller, loadfiller;
    bool castando = false;
    // Start is called before the first frame update
    void Start()
    {
        hbfiller = transform.Find("Canvas").Find("HealthBar").Find("hbfiller").gameObject;
        if(time == 0){
            manafiller = transform.Find("Canvas").Find("Manabar").Find("manafiller").gameObject;
            loadfiller = transform.Find("Canvas").Find("Manabar").Find("filler").gameObject;
        }
        vidaatual = vida;
        prefabDmgText = Resources.Load<GameObject>("dmgCanvas");
    }

    // Update is called once per frame
    void Update()
    {
        if(!castando){
            manaAtual += manaPS * Time.deltaTime;
            if(manaAtual > mana){
                manaAtual = mana;
            }
        }else{
            manaCast += Time.deltaTime * manaCastPS;
            if(manaCast > manaAtual) manaCast = manaAtual;
        }
        if(time == 0){
            manafiller.GetComponent<Image>().fillAmount = manaAtual/mana;
            loadfiller.GetComponent<Image>().fillAmount = manaCast/mana;
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

    public float castMana(){
        castando = !castando;
        if(!castando){
            float aux = manaCast;
            manaCast = 0;
            return aux;
        }
        return -1;
    }
}
