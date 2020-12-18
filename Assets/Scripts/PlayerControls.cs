using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    Rigidbody rc;
    float JumpingPower = 5.0f; // Karakterin zıplama gücü
    float RunPower = 2.0f; // Karakterin ileri yönde gitme gücü

    bool left; // Karakterin sağa ve sola hareketlerini kontrol etmek için, true or false
    bool right;
    bool Up;

    bool isJump = false; // Burada karakterin havada olup olmadığını kontrol edeceğiz. 2 defa zıplamasını engellemek amaçlı.

    Animator JumpAnim; // Karakterimiz zıpladığında zıplama animasyonu devreye girecek.

    Transform ground_1; // 1.Yol - Başlangıç yolu
    Transform ground_2; // 2.Yol

    GameManagerScript gamemanager; // GameManager Scriptimizdeki metodlarımıza erişeceğiz.

    public GameObject TryAgainPanel;



    public bool IsTakenHealth = false; // Sağlık nesnesi alınırsa true olacak.
    public bool IsEnfected = false; // Karakterimiz enfekte olursa, yani düşman nesneleriyle temas ederse. ( CoronaBus & EnemyPeople) // Temel değişkenler

    void Start()
    {
        rc = GetComponent<Rigidbody>(); //RigidBody nesnemizi ekledik.
        JumpAnim = GetComponent<Animator>(); // Animator nesnemizi getirdik.

        ground_1 = GameObject.Find("Ground_1").transform; // Sahnede Ground_1 isimli nesneyi bulur ve konumunu alır.
        ground_2 = GameObject.Find("Ground_2").transform; // Sahnede Ground_2 isimli nesneyi bulur ve konumunu alır.

        gamemanager = GameObject.Find("GameManager").GetComponent<GameManagerScript>(); // Oyun yöneticisi nesnemizin içindeki componentlere eriştik.
    } // Start Fonksiyonu

void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Ground_1") // Sahnemizdeki yol 1 ve yol 2'ye çarpışma companent'i ekledik. Burada da kontrollerini yapıp yolların lokasyonlarını değiştireceğiz.
        {
            ground_2.position = new Vector3(ground_2.position.x, ground_2.position.y, ground_1.position.z + 10.0f); // Yol 1 ile yol 2 arasında z pozisyonunda 10 birimlik bir fark olduğu için 10f ekledik.
        }
        if (col.gameObject.name == "Ground_2")
        {
            ground_1.position = new Vector3(ground_1.position.x, ground_1.position.y, ground_2.position.z + 10.0f); // Yol 1 ile yol 2 arasında z pozisyonunda 10 birimlik bir fark olduğu için 10f ekledik.
        }

        // Sağlık nesneleri ve çarpışma sonundaki etkileşimler

        if (col.gameObject.tag == "Sterilize")
        {
            RunPower = RunPower + 0.05f; // Dezenfektan aldığımızda karakterimiz 0.05f daha hızlı koşar.
            col.gameObject.SetActive(false); // Çarptığımızda pasif hale getiriyoruz.
            gamemanager.ScoreUp(10); // Oyuncu dezenfektan aldığında 10 puan kazanır.
            gamemanager.HealthUp(10); // Oyuncu dezenfektan aldığında canı 10 artacak.
            IsTakenHealth = true; // Sağlık nesnesi alındı. Değişkenimiz true oldu.

        }

        if (col.gameObject.tag == "CoronaMask")
        {
            RunPower = RunPower + 0.05f; // Maske aldığımızda karakterimiz 0.05f daha hızlı koşmaya başlar.
            col.gameObject.SetActive(false); // Çarptığımızda pasif hale getiriyoruz.
            gamemanager.ScoreUp(10); // Oyuncu maske aldığında 10 puan kazanır.
            gamemanager.HealthUp(10); // Oyuncu maske aldığında canı 10 aratacak.
            IsTakenHealth = true; // Sağlık nesnesi alındı. Değişkenimiz true oldu.
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Die") // Karakterimizi kovalayan 'Enemy' isimli nesnemizin tag'ı Die olduğu için 'Die' olarak sorgulama yaptık.
        {

            // Ölüm Ekranı
            TryAgainPanel.SetActive(true);
            Time.timeScale = 0.0f;
        }

        if (col.gameObject.tag == "CoronaBus")
        {

            // Ölüm Ekranı
            TryAgainPanel.SetActive(true);
            Time.timeScale = 0.0f;
        }

        if (col.gameObject.tag == "EnemyPeople")
        {

            // Ölüm Ekranı
            TryAgainPanel.SetActive(true);
            Time.timeScale = 0.0f;
        }
    }

    private void OnCollisionStay(Collision collision)
    {

        isJump = false;

    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "CoronaBus" || collision.gameObject.name == "stone") // Eğer taş veya otobüse temas ediyorsa tekrar zıplama pasif olur.
        {
            isJump = true;
        }
    } // Çarpışma Componentleri 

void Update()
    {
        if (Input.touchCount > 0)
        {
            // Karakterimizin sağa veya sola dönüşlerini touch deltaposition ile belirleriz. 
            Touch P_Touch = Input.GetTouch(0);
            if (P_Touch.deltaPosition.x > 100f) // x ekseninde + değerler sağa dönüşleri temsil eder.
            {
                left = false;
                right = true;
            }
            if (P_Touch.deltaPosition.x < -100f) // x ekseninde - değerler sola dönüşleri temsil eder.
            {
                left = true;
                right = false;

            }

            if (P_Touch.deltaPosition.y > 100f)
            {
                Up = true;
            }
        }

        if (right == true) // eğer sağa dönüş true ise
        {
            // Sağ tarafa doğru kaydırılmak istendiğinde karakterimizin sağa doğru kaç birimlik hareket edeceğini belirttik. Sağdan 1.4f olana kadar hareket edebilir.
            transform.position = Vector3.Lerp(transform.position, new Vector3(1.4f, transform.position.y, transform.position.z), RunPower * Time.deltaTime);

        }

        if (left == true) // eğer sola dönüş true ise
        {
            // Sol tarafa doğru kaydırılmak istendiğinde karakterimizin sola doğru kaç birimlik hareket edeceğini belirttik. Soldan -2f olana kadar hareket edebilir.
            transform.position = Vector3.Lerp(transform.position, new Vector3(-2.0f, transform.position.y, transform.position.z), RunPower * Time.deltaTime);
        }

        if (Up == true)
        {
            Jump();
        }

        // Karakterimizin ileri yöndeki hareket kodları
        // Karakterimizin z ekseninde belirttiğimiz koşu hızı ile hareket edecektir. Bunun her frame içerisinde yapacağı için zamana bağlı olarak çarpma işlemi yaptık.
        transform.Translate(0, 0, RunPower * Time.deltaTime);
    } // Update Fonksiyonu

public void Jump() // Ekrana tıklandığında karakterimiz zıplayacak.
    {
        if (isJump == false) // Karakter yere temas ediyorsa, zıplayabilir.
        {
            JumpAnim.SetTrigger("Jump"); // Animatör içerisinde oluşturmuş olduğumuz Jump isimli trigger devreye girer.
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, 2f, transform.position.z), JumpingPower * Time.deltaTime);
            rc.velocity = Vector3.zero;
            rc.velocity = Vector3.up * JumpingPower;
            Up = false;
        }

    } // Zıplama Fonksiyonu
}
