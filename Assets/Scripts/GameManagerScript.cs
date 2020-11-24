using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    // Bu script içerisinde sahnemizde karakterimiz ilerlerken karşısına engeller ve ödüllerin random şekilde çıkmasını sağlayan kodları yazacağız.

    public GameObject Sterilize; // Dezenfektan nesnemiz.
    public GameObject CoronaMask; // Maske nesnemiz.
    public GameObject EnemyPeople; // Kalabalık insan toplulukları.
    public GameObject CoronaBus; // Sosyal mesafesiz otobüs.

    Transform Player; // Player konumu almak için

    int score = 0; // Puanımızı tutacağız.


    // Nesnelerin bazılarında x ve y koordinatlarında problem çıktığı için problem çıkan nesneler farklı listeler içerisinde yazılacak.
    List<GameObject> CoronaBusObject;
    List<GameObject> CoronaMaskObject;
    List<GameObject> SterilizeObject;
    List<GameObject> EnemyPeopleObject;



    void Start()
    {
        // Başlangıçta boş listelerimizi oluşturuyoruz 

        CoronaBusObject = new List<GameObject>();
        CoronaMaskObject = new List<GameObject>();
        SterilizeObject = new List<GameObject>();
        EnemyPeopleObject = new List<GameObject>();

        Player = GameObject.Find("Player").transform;


        ObjectCreate(Sterilize, 3, SterilizeObject);
        ObjectCreate(CoronaMask, 3, CoronaMaskObject);
        ObjectCreate(CoronaBus, 3, CoronaBusObject);
        ObjectCreate(EnemyPeople, 3, EnemyPeopleObject);

        InvokeRepeating("CreateCoronaMaskObject", 0.0f, 1.0f);
        InvokeRepeating("CreateCoronaBusObject", 2.0f, 3.0f);
        InvokeRepeating("CreateSterilizeObject", 4.0f, 5.0f);
        InvokeRepeating("CreateEnemyPeople", 3.0f, 4.0f);

    }


    public void ScoreUp(int point) // Oyundaki puan toplama kodları bu alandan yönetilecek.
    {
        score += point;
        Debug.Log(score); // Daha sonra düzenlenecek. Test amaçlı eklendi.
    }

    public void ScoreDown(int point) // Oyundaki puan düşürme kodları bu alandan yönetilecek.
    {
        score -= point;
        Debug.Log(score); // Daha sonra düzenlenecek. Test amaçlı eklendi.
    }


    void CreateSterilizeObject() // Dezenfektanın sahneye eklenmesi ile ilgili işlemler
    {
        int r_sterilize = Random.Range(0, SterilizeObject.Count);

        if (SterilizeObject[r_sterilize].activeSelf == false)
        {
            SterilizeObject[r_sterilize].SetActive(true);
            int r_location = Random.Range(0, 2);

            if (r_location == 0)
            {
                SterilizeObject[r_sterilize].transform.position = new Vector3(-0.6f, 0.1f, Player.position.z + 10.0f);
            }
            if (r_location == 1)
            {
                SterilizeObject[r_sterilize].transform.position = new Vector3(-3.9f, 0.1f, Player.position.z + 10.0f);

            }

        }
        else
        {
            foreach (GameObject sterilize in SterilizeObject)
            {
                if (sterilize.activeSelf == false)// görünürlüğü pasif ise
                {
                    sterilize.SetActive(true); // aktif yap

                    int r_location2 = Random.Range(0, 2);

                    if (r_location2 == 0)
                    {
                        sterilize.transform.position = new Vector3(-0.6f, 0.1f, Player.position.z + 10.0f);
                    }
                    if (r_location2 == 1)
                    {
                        sterilize.transform.position = new Vector3(-3.9f, 0.1f, Player.position.z + 10.0f);
                    }

                    return; // döngüyü sonlandır
                }
            }

        }
    }

    void CreateEnemyPeople() // EnemyPeople sahneye eklenmesi ile ilgili işlemler
    {
        int r_people = Random.Range(0, EnemyPeopleObject.Count);

        if (EnemyPeopleObject[r_people].activeSelf == false)
        {
            EnemyPeopleObject[r_people].SetActive(true);
            int r_location = Random.Range(0, 2);

            if (r_location == 0)
            {
                EnemyPeopleObject[r_people].transform.position = new Vector3(1.6f, 2.8f, Player.position.z + 10.0f);
            }
            if (r_location == 1)
            {
                EnemyPeopleObject[r_people].transform.transform.position = new Vector3(-1.4f, 2.8f, Player.position.z + 10.0f);

            }

            if (EnemyPeopleObject[r_people].tag == "CoronaBus")
            {
                if (Player.gameObject.GetComponent<PlayerControls>().IsEnfected == true)
                {
                    EnemyPeopleObject[r_people].SetActive(false);
                }

            }

        }
        else
        {
            foreach (GameObject people in EnemyPeopleObject)
            {
                if (people.activeSelf == false)// görünürlüğü pasif ise
                {
                    people.SetActive(true); // aktif yap

                    int r_location2 = Random.Range(0, 2);

                    if (r_location2 == 0)
                    {
                        people.transform.position = new Vector3(1.6f, 2.8f, Player.position.z + 10.0f);
                    }
                    if (r_location2 == 1)
                    {
                        people.transform.position = new Vector3(-1.4f, 2.8f, Player.position.z + 10.0f);
                    }

                    if (people.tag == "EnemyPeople")
                    {
                        if (Player.gameObject.GetComponent<PlayerControls>().IsEnfected == true)
                        {
                            people.SetActive(false);
                        }

                    }

                    return; // döngüyü sonlandır
                }
            }

        }
    }

    void CreateCoronaMaskObject() // Maskenin sahneye eklenmesi ile ilgili işlemler
    {
        int r_mask = Random.Range(0, CoronaMaskObject.Count);

        if (CoronaMaskObject[r_mask].activeSelf == false)
        {
            CoronaMaskObject[r_mask].SetActive(true);
            int r_location = Random.Range(0, 2);

            if (r_location == 0)
            {
                CoronaMaskObject[r_mask].transform.position = new Vector3(1.0f, 0.1f, Player.position.z + 10.0f);
            }
            if (r_location == 1)
            {
                CoronaMaskObject[r_mask].transform.position = new Vector3(-2.1f, 0.1f, Player.position.z + 10.0f);
            }

            if (CoronaMaskObject[r_mask].tag == "CoronaMask") // Eğer çarpıştığımız nesnenin tagı CoronaMask ise
            {
                if (Player.gameObject.GetComponent<PlayerControls>().IsTakenHealth == true)
                {
                    CoronaMaskObject[r_mask].SetActive(false);
                }

            }

        }
        else
        {
            foreach (GameObject mask in CoronaMaskObject)
            {
                if (mask.activeSelf == false)// görünürlüğü pasif ise
                {
                    mask.SetActive(true); // aktif yap

                    int r_location2 = Random.Range(0, 2);

                    if (r_location2 == 0)
                    {
                        mask.transform.position = new Vector3(1.0f, 0.1f, Player.position.z + 10.0f);
                    }
                    if (r_location2 == 1)
                    {
                        mask.transform.position = new Vector3(-2.1f, 0.1f, Player.position.z + 10.0f);
                    }

                    if (mask.tag == "CoronaMask")
                    {
                        if (Player.gameObject.GetComponent<PlayerControls>().IsTakenHealth == true)
                        {
                            mask.SetActive(false);
                        }
                        
                    }

                    return; // döngüyü sonlandır
                }
            }

        }
    }


    void CreateCoronaBusObject() // CoronaBus'ün sahneye eklenmesi ile ilgili işlemler
    {
        int r_bus = Random.Range(0, CoronaBusObject.Count);

        if (CoronaBusObject[r_bus].activeSelf == false)
        {
            CoronaBusObject[r_bus].SetActive(true);
            int r_location = Random.Range(0, 2);

            if (r_location == 0)
            {
                CoronaBusObject[r_bus].transform.position = new Vector3(1.4f, -0.1f, Player.position.z + 10.0f);
            }
            if (r_location == 1)
            {
                CoronaBusObject[r_bus].transform.position = new Vector3(-2.0f, -0.1f, Player.position.z + 10.0f);

            }

            if (CoronaBusObject[r_bus].tag == "CoronaBus")
            {
                if (Player.gameObject.GetComponent<PlayerControls>().IsEnfected == true)
                {
                    CoronaBusObject[r_bus].SetActive(false);
                }

            }

        }
        else
        {
            foreach (GameObject bus in CoronaBusObject)
            {
                if (bus.activeSelf == false)// görünürlüğü pasif ise
                {
                    bus.SetActive(true); // aktif yap

                    int r_location2 = Random.Range(0, 2);

                    if (r_location2 == 0)
                    {
                        bus.transform.position = new Vector3(1.4f, -0.1f, Player.position.z + 10.0f);
                    }
                    if (r_location2 == 1)
                    {
                        bus.transform.position = new Vector3(-2.0f, -0.1f, Player.position.z + 10.0f);

                    }

                    if (bus.tag == "CoronaBus")
                    {
                        if (Player.gameObject.GetComponent<PlayerControls>().IsEnfected == true)
                        {
                            bus.SetActive(false);
                        }

                    }



                    return; // döngüyü sonlandır
                }
            }

        }
    }


    void ObjectCreate(GameObject article, int amount, List<GameObject> AllObject) // Bu methodumuzda nesne türetimi yapacağız. Parametre olarak nesne ve adedi gelecek.
    {
        // 0 dan miktara kadar yeni nesneler oluşturduk ve list'e ekledik.
        for (int i = 0; i < amount; i++)
        {
            GameObject new_article = Instantiate(article);
            new_article.SetActive(false); // Yeni oluşturulan nesnelerimizi görünmez hale getirdik.
            AllObject.Add(new_article);
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}
