﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    // Bu script içerisinde sahnemizde karakterimiz ilerlerken karşısına engeller ve ödüllerin random şekilde çıkmasını sağlayan kodları yazacağız.

    public GameObject Sterilize; // Dezenfektan nesnemiz.
    public GameObject Stone; // Taş nesnemiz.
    public GameObject CoronaMask; // Maske nesnemiz.
    public GameObject EnemyPeople; // Kalabalık insan toplulukları.
    public GameObject CoronaBus; // Sosyal mesafesiz otobüs.



    Transform Player; // Player konumu almak için

    int health = 100; // Canımızı tutuyor. Zamana bağlı olarak düşmesini sağlayacağız.
    int score = 0; // Puanımızı tutacağız.
    int ScoreFrame = 0; // Geçen zamana göre ekstra puan eklemesi yapacağız.
    bool GameStatus = false; // Oyunun durdurulup durdurulmadığını kontrol etmek için

    public TMPro.TextMeshProUGUI ScoreBoard; // Oyun sahnemizdeki skorumuzu aktaracağımız alan.
    public TMPro.TextMeshProUGUI HealthTxt;


    List<GameObject> CoronaBusObject;
    List<GameObject> CoronaMaskObject;
    List<GameObject> SterilizeObject;
    List<GameObject> EnemyPeopleObject;
    List<GameObject> StoneObject;


    public GameObject GameStoppedPanel; // Temel değişkenler

    void Start()
    {
        // Başlangıçta boş listelerimizi oluşturuyoruz 
        CoronaBusObject = new List<GameObject>();
        CoronaMaskObject = new List<GameObject>();
        SterilizeObject = new List<GameObject>();
        EnemyPeopleObject = new List<GameObject>();
        StoneObject = new List<GameObject>();

        Player = GameObject.Find("Player").transform;


        ObjectCreate(Sterilize, 120, SterilizeObject);
        ObjectCreate(CoronaMask, 190, CoronaMaskObject);
        ObjectCreate(CoronaBus, 180, CoronaBusObject);
        ObjectCreate(EnemyPeople, 10, EnemyPeopleObject);
        ObjectCreate(Stone, 100, StoneObject);

        InvokeRepeating("CreateCoronaMaskObject", 1.0f, 10.0f);
        InvokeRepeating("CreateCoronaBusObject", 1.0f, 9.0f);
        InvokeRepeating("CreateSterilizeObject", 2.0f, 10.0f);
        InvokeRepeating("CreateEnemyPeople", 5.0f, 10.0f);
        InvokeRepeating("CreateStone", 3.0f, 10.0f);

        ScoreBoard.text = "SCORE " + score.ToString();
        HealthTxt.text = health.ToString();


    } // Oyun başlangıcında yapılan işlemler

    void ObjectCreate(GameObject article, int amount, List<GameObject> AllObject) // Bu methodumuzda nesne türetimi yapacağız. Parametre olarak nesne ve adedi gelecek.
    {
        // 0 dan miktara kadar yeni nesneler oluşturduk ve list'e ekledik.
        for (int i = 0; i < amount; i++)
        {
            GameObject new_article = Instantiate(article);
            new_article.SetActive(false); // Yeni oluşturulan nesnelerimizi görünmez hale getirdik.
            AllObject.Add(new_article);
        }
    } // Sahnede nesne oluşturma metodu

    public void ScoreUp(int point) // Oyundaki puan toplama kodları bu alandan yönetilecek.
    {
        if (GameStatus == false) // Oyun durdurulmadıysa aldığımız objelerde puan verilecek.
        {
            score += point;
            ScoreBoard.text = "SCORE " + score.ToString();
        }
    } // Puan toplama işlemleri

public void HealthUp(int point) // Oyundaki can alma işlemleri bu metod ile yapılacak.
    {
        if (GameStatus == false) // Eğer oyun durdurulmamış ise can verecek.
        {
            if (health < 100)
            {
                health += point;
                HealthTxt.text = health.ToString();
            }
            if (health + 10 > 100) // Eğer canı aldığımızda, canımız 100'den fazla oluyorsa, canımızı 100'e getirir.
            {
                health = 100;
                HealthTxt.text = health.ToString();
            }
        }
    }

    public void HealthDown()
    {
        if (GameStatus == false) // Eğer oyun durdurulmamış ise can azalacak.
        {
            if (health > 0)
            {
                health = health - 1; // Oyun devam ettiği süre boyunca her frame'de canı azaltacağız. Can 0'dan küçük olduğu durumlarda bu işlem yapılmayacak.
            }
        }
    } // Can ile ilgili işlemler

public void TryAgain()
    {
        GameStatus = true; // Eğer karakter ölmüşse oyun durumu true olacak.
        SceneManager.LoadScene("Scenes/FirstScene"); // Yeniden oyna butonuna basıldığında sahnemizin yeniden yüklenmesini sağlar.
        Time.timeScale = 1.0f; // Yeniden yüklenen sahnemiz 0.0f'de bulunuyor. Bu sahnenin işlemeye devam etmesi için 1.0f bölümüne alırız.
    }

    public void GameStop() // Oyunu durdurmak için ekranın sağ köşesindeki butona bastığımızda buradaki işlemler gerçekleşir.
    {
        GameStatus = true;
        GameStoppedPanel.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void ContinueButton() // Oyuna devam et butonuna basılması halinde bu metodumuz çalışır. Sahneyi kaldığı yerden devam ettiririz
    {
        GameStatus = true;
        GameStoppedPanel.SetActive(false);
        Time.timeScale = 1.0f; // Yeniden yüklenen sahnemiz 0.0f'de bulunuyor. Bu sahnenin işlemeye devam etmesi için 1.0f bölümüne alırız.
    }

    public void LeaderBoard()
    {
        // Oyuna puan sıralaması eklenirse eğer, gerekli kodlar buradan kontrol edilecek.
    } // Oyun Panelleri

void CreateStone() // Stone nesnesinin sahneye eklenmesi ile ilgili işlemler
    {
        int r_stone = Random.Range(0, StoneObject.Count);

        if (StoneObject[r_stone].activeSelf == false)
        {
            StoneObject[r_stone].SetActive(true);
            int r_location = Random.Range(0, 2);

            if (r_location == 0)
            {
                StoneObject[r_stone].transform.position = new Vector3(0.9f, 0.5f, Player.position.z + 10.0f);
            }
            if (r_location == 1)
            {
                StoneObject[r_stone].transform.position = new Vector3(-2f, 0.5f, Player.position.z + 10.0f);

            }

        }
        else
        {
            foreach (GameObject stone in StoneObject)
            {
                if (stone.activeSelf == false)// görünürlüğü pasif ise
                {
                    stone.SetActive(true); // aktif yap

                    int r_location2 = Random.Range(0, 2);

                    if (r_location2 == 0)
                    {
                        stone.transform.position = new Vector3(0.9f, 0.5f, Player.position.z + 10.0f);
                    }
                    if (r_location2 == 1)
                    {
                        stone.transform.position = new Vector3(-2f, 0.5f, Player.position.z + 10.0f);
                    }

                    return; // döngüyü sonlandır
                }
            }

        }
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
                SterilizeObject[r_sterilize].transform.position = new Vector3(1.4f, 0.27f, Player.position.z + 10.0f);
            }
            if (r_location == 1)
            {
                SterilizeObject[r_sterilize].transform.position = new Vector3(-2f, 0.27f, Player.position.z + 10.0f);

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
                EnemyPeopleObject[r_people].transform.position = new Vector3(1.4f, 0.43f, Player.position.z + 10.0f);
            }
            if (r_location == 1)
            {
                EnemyPeopleObject[r_people].transform.transform.position = new Vector3(-2f, 0.43f, Player.position.z + 10.0f);

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
                CoronaMaskObject[r_mask].transform.position = new Vector3(1.4f, 0.3f, Player.position.z + 10.0f);
            }
            if (r_location == 1)
            {
                CoronaMaskObject[r_mask].transform.position = new Vector3(-2.0f, 0.3f, Player.position.z + 10.0f);
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
    } // Nesnelerin oluşturulması işlemleri

void ScoreUpTime()
    {
        if (GameStatus == false) // Eğer oyun durdurulmamış ise puan vermeye devam et.
        {
            if (ScoreFrame % 47 == 0) // Belirli bir zaman geçtikten sonra puan vermek için 47 asal sayısını kullandık.
            {
                score += 1;
            }
            ScoreBoard.text = "SCORE " + score.ToString();
        }
    }

    void HealthUpTime()
    {
        if (GameStatus == false) // Eğer oyun durdurulmamış ise zamana bağlı olarak can azalacak.
        {
            if (ScoreFrame % 97 == 0)
            {
                health -= 1;
                if (health == 0) // Eğer canımız sıfır olursa yeniden oyna metodumuz çalışacak.
                {
                    TryAgain();
                }
            }
            HealthTxt.text = health.ToString();
        }
    } // Puan ve sağlık işlemleri

    void Update()
    {
        ScoreFrame++;
        ScoreUpTime();
        HealthUpTime();

    } // Update Fonksiyonu
}
