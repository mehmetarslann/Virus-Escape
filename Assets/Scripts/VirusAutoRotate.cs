using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusAutoRotate : MonoBehaviour
{
    // Sahnemizdeki Virus(Enemy) nesnemize animasyon(z pozisyonunda dönmesini sağlar) vermek için gerekli kodlar.
    string Name;
    void Start()
    {
        Name = gameObject.tag;
    }

    
    void Update()
    {
        if(Name=="Die")
        {
            transform.Rotate(0, 0, -1);
        }
        
    }
}
