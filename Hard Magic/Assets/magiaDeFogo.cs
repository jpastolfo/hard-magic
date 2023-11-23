using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magiaDeFogo : MonoBehaviour
{
    public float dano = 10, duracao = 5f;
    public int range = 3;
    int time = 0;
    GameObject prefabFogo, prefabFoguinho;

    float TimerCastDuration = 0;

    public LayerMask mask;

    bool castando = false;
    // Start is called before the first frame update
    void Start()
    {
        prefabFogo = Resources.Load<GameObject>("fogo");
        prefabFoguinho = Resources.Load<GameObject>("FogoUnidade");
    }

    // Update is called once per frame
    void Update()
    {
        if(castando){
            TimerCastDuration += Time.deltaTime;
        }
        if(Input.GetKeyDown(KeyCode.E) && !castando){
            castando = true;
            GetComponent<playerMovement>().slow(0.5f);
        }
        if(Input.GetKeyUp(KeyCode.E)){
            //if(TimerCastDuration >= castDuration){
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if( Physics.Raycast(ray, out hit, mask) )
                {
                    //Debug.DrawLine( transform.position, hit.point );
                    Vector3 target = hit.point - transform.position;
                    GameObject fogo = Instantiate(prefabFogo);
                    fogo.transform.position = transform.position;
                    fogo.transform.forward = new Vector3(target.x, 0, target.z).normalized;                    
                    Destroy(fogo, duracao);
                    fogo.GetComponent<magia>().spawnar(dmg: dano, team: time, dt: duracao);
                    fogo.GetComponent<BoxCollider>().size = new Vector3(1,1,range);
                    fogo.GetComponent<BoxCollider>().center = new Vector3(1,1,(range- 1)/2);
                    GameObject obj = new GameObject("oie");
                    obj.transform.position = hit.point;
                    for(int i = 0; i < range; i++){
                        GameObject foguinho = Instantiate(prefabFoguinho);
                        foguinho.transform.position = fogo.transform.position + new Vector3(target.x, 0, target.z).normalized * i;
                        foguinho.transform.SetParent(fogo.transform);
                        foguinho.GetComponent<AnimControlDuracao>().duracao = duracao;
                    }
                    //AreaSkill(target);
                }
            //}
            castando = false;
            TimerCastDuration = 0;
            GetComponent<playerMovement>().slow(1);
        }
    }
}
