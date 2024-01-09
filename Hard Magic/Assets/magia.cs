using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class magia : MonoBehaviour
{
    float speed, dano, duracaoDaParede, paredeTimer = 0, danoContinuoTime = 0.3f, danoContinuoTimer = 0;
    int time;
    bool dieOnContact, danoContinuo;
    float deathTime;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, deathTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
        if(danoContinuo) danoContinuoTimer += Time.deltaTime;
    }

    public void spawnar(float spd = 0, float dmg = 0, int team = 0, bool doc = false, float dt = 0, float pdur = 0, bool continuo = false){
        speed = spd;
        dano = dmg;
        time = team;
        deathTime = dt;
        dieOnContact = doc;
        duracaoDaParede = pdur;
        danoContinuo = continuo;
    }

    private void OnTriggerEnter(Collider other){
        if(other.gameObject.GetComponent<battleManager>() != null){
            if(other.gameObject.GetComponent<battleManager>().time != time && dano != 0 && !danoContinuo){
                other.gameObject.GetComponent<battleManager>().aplicadano(dano);
                if(GetComponent<miniGelinhos>() != null) GetComponent<miniGelinhos>().spawnarGelo();
                if(dieOnContact) Destroy(gameObject);
                if(duracaoDaParede > 0){
                    NavMeshObstacle obs = gameObject.AddComponent<NavMeshObstacle>();
                    obs.center = new Vector3(-0.6624889f, 1.469994f, 0.01673761f);
                    obs.size = new Vector3(3.165337f, 2.93967f, 1.033475f);
                    dano = 0;
                }
            }
        }else{
            if(dieOnContact)
                Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.GetComponent<battleManager>() != null){
            if(other.gameObject.GetComponent<battleManager>().time != time && dano != 0){
                other.gameObject.GetComponent<battleManager>().aplicadano(dano);
                if(duracaoDaParede > 0){
                    NavMeshObstacle obs = gameObject.AddComponent<NavMeshObstacle>();
                    obs.center = new Vector3(-0.6624889f, 1.469994f, 0.01673761f);
                    obs.size = new Vector3(3.165337f, 2.93967f, 1.033475f);
                    dano = 0;
                }
            }
        }else{
            if(dieOnContact)
                Destroy(gameObject);
        }
    }

    private void OnTriggerStay(Collider other) {
        if(danoContinuo && danoContinuoTimer >= danoContinuoTime){
            if(other.GetComponent<battleManager>() != null){
                if(other.GetComponent<battleManager>().time != time && dano != 0){
                    other.GetComponent<battleManager>().aplicadano(dano);
                    danoContinuoTimer = 0;
                }
            }
        }
    }
}
