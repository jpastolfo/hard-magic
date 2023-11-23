using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class battleManager : MonoBehaviour
{
    public float vida, mana = 100, manaPS = 2, manaCastPS = 2;
    float vidaatual, manaAtual = 100, manaCast = 0, custo2 = 100, custo3 = 100;
    public int time;
    GameObject hbfiller, prefabDmgText, manafiller, loadfiller;
    bool castando = false;

    Color blue, orange, purple;
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
        blue = new Color(0.8862745f, 0.2470588f, 0.7797884f, 1);
        orange = new Color(1, 0.6864846f, 0.172549f, 1);
        purple = new Color(0.6741618f, 0.172549f, 1, 1);
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
            if(manaCast >= custo2 && manaCast < custo3){
                loadfiller.GetComponent<Image>().color = orange;
            }
            if(manaCast >= custo3){
                loadfiller.GetComponent<Image>().color = purple;
            }
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

    public float castMana(float custo, float c2, float c3){
        castando = !castando;
        custo2 = c2;
        custo3 = c3;
        if(!castando){
            loadfiller.GetComponent<Image>().color = blue;
            float aux = manaCast;
            if(manaCast < custo){
                manaAtual -= custo;
            }else{
                manaAtual -= manaCast;
            }
            manaCast = 0;
            if(aux < custo) return custo;
            return aux;
        }
        return -1;
    }
}
