using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magiaDeMetal : MonoBehaviour
{
    public float range, dano, castDuration = 0.3f, metalDuracao = 1f, custoBase = 0;
    bool castando = false;

    float TimerCastDuration = 0;

    public LayerMask mask;

    GameObject prefabMetal;

    int time = 0;

    // Start is called before the first frame update
    void Start()
    {
        prefabMetal = Resources.Load<GameObject>("metalArea");
    }

    // Update is called once per frame
    void Update()
    {
        if(castando){
            TimerCastDuration += Time.deltaTime;
        }
        if(Input.GetKeyDown(KeyCode.Space) && !castando){
            castando = true;
            GetComponent<playerMovement>().slow(0.5f);
        }
        if(Input.GetKeyUp(KeyCode.Space)){
            //if(TimerCastDuration >= castDuration){
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if( Physics.Raycast(ray, out hit, mask) )
                {
                    //Debug.DrawLine( transform.position, hit.point );
                    Vector3 target = hit.point;
                    GameObject metal = Instantiate(prefabMetal);
                    metal.transform.position = target;
                    Destroy(metal, metalDuracao);
                    metal.transform.GetChild(0).GetComponent<AnimControlDuracao>().duracao = metalDuracao;
                    metal.GetComponent<magia>().spawnar(dmg: dano, team: time, dt: metalDuracao);
                    //AreaSkill(target);
                }
            //}
            castando = false;
            TimerCastDuration = 0;
            GetComponent<playerMovement>().slow(1);
        }
    }
}
