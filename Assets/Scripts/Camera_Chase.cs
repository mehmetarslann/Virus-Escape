using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Chase : MonoBehaviour
{
    Transform PlayerPosition; // Oyuncunun son pozisyonu almak için Transform tipinde bir değişken oluşturduk. Bu script ile kameranın oyuncuyu takip etmesini sağlayacağız.
    Vector3 Distance;

    float speed = 4.0f;
    void Start()
    {
        PlayerPosition = GameObject.Find("Player").transform; // Oyun nesnelerimiz üzerinde Player isimi nesneyi arayıp bulur.
    }

    // Update is called once per frame
    void LateUpdate() // Kamera takip kodları LateUpdate içerisinde yazılır. LateUpdate, Update'den sonra bir kez çalışır. 
    {
        Distance = new Vector3(PlayerPosition.position.x, transform.position.y, PlayerPosition.position.z - 2.5f); // Oyuncu ile kamera arasındaki mesafe
        transform.position = Vector3.Lerp(transform.position, Distance, speed * Time.deltaTime); // Zamana bağlı olarak yumaşak takip
    }
}
