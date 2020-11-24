using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoronaBusMove : MonoBehaviour
{
    // CoronaBus Nesnemizin player'a doğru gelmesini sağlayacak olan kod bloğu

    public float CoronaBusVeriable = 2.0f;

    void Update()
    {
        transform.Translate(0, 0, CoronaBusVeriable * Time.deltaTime);
    }
}
