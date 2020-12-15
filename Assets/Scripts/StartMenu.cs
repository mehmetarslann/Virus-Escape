using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    // Oyunun başlangıç ekranındaki Play butonuna basıldığında diğer sahneye aktarma işlemi
    
    public void PlayButton()
    {
        SceneManager.LoadScene("FirstScene");
    }
}
