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

    public GameObject atk;
    bool castando = false;

    public LayerMask mask;

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
        blue = new Color(0.3840512f, 0.5693969f, 0.8773585f, 1);
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
            if(time == 0){
                if(Input.GetKeyUp(KeyCode.Alpha1)){
                    GetComponent<projetilDeGelo>().activate(true);
                    GameObject dmgText = Instantiate(prefabDmgText);
                    dmgText.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "GELO";
                    //dmgText.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.blue;
                    dmgText.transform.position = transform.position + Vector3.up;
                    GetComponent<magiaDeMetal>().activate(false);
                    GetComponent<magiaDeFogo>().activate(false);
                }else{
                    if(Input.GetKeyUp(KeyCode.Alpha2)){
                        GameObject dmgText = Instantiate(prefabDmgText);
                        dmgText.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "METAL";
                        dmgText.transform.position = transform.position + Vector3.up;
                        GetComponent<projetilDeGelo>().activate(false);
                        GetComponent<magiaDeMetal>().activate(true);
                        GetComponent<magiaDeFogo>().activate(false);
                    }else{
                        if(Input.GetKeyUp(KeyCode.Alpha3)){
                            GameObject dmgText = Instantiate(prefabDmgText);
                            dmgText.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "FOGO";
                            dmgText.transform.position = transform.position + Vector3.up;
                            GetComponent<projetilDeGelo>().activate(false);
                            GetComponent<magiaDeMetal>().activate(false);
                            GetComponent<magiaDeFogo>().activate(true);
                        }
                    }
                }
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
        if(vidaatual > vida) vidaatual = vida;
        if(hbfiller){
            hbfiller.GetComponent<Image>().fillAmount = vidaatual/vida;
        }
        GameObject dmgText = Instantiate(prefabDmgText);
        dmgText.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = ""+dano;
        dmgText.transform.position = transform.position + Vector3.up;
        if(vidaatual <= 0 && time != 0){
            if(atk!=null){Destroy(atk);} 
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
        }else{
            if(manaAtual < custo){
                castando = false;
                return -2;
            }
        }
        return -1;
    }
}
