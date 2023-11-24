using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    //[SerializeField] SoundManager sm;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BtnClick(int index)
    {
        SceneManager.LoadScene(index);

    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void PlayClickAudio()
    {
        //sm.PlayClickSound();
    }
}
