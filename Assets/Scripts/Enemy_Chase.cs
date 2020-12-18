using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Chase : MonoBehaviour
{
    Transform Player; // Player nesnemizin konumunu alacağız, tekip etmek için.

    //RaycastHit ray; // Virüs nesnemiz ile karşısına çıkan objeler arasındaki mesafeyi ölçeceğiz.
    void Start()
    {

        Player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, Player.position, 2.11f * Time.deltaTime);
    }
}
