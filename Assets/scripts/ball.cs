using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ball : MonoBehaviour
{
    private bool gameWaiting;
    private bool isDead;
    private bool changed;
    
    float rotateZ, rotateY, rotateX;
    float speed = 8f;
    private float score;
    
    Rigidbody rb;

    [Header("screens")] 
    public GameObject gameScreen;
    public GameObject deadScreen;
    public GameObject waitScreen;
    public GameObject winScreen;
    
    [Header("texts")] 
    public Text scoreText;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //oyun  bekleme modunda
        rb.useGravity = false;
        gameWaiting = true;
        gameScreen.SetActive(false);
        deadScreen.SetActive(false);
        winScreen.SetActive(false);
        waitScreen.SetActive(true);
        

    }

    void Update()
    {
        //eğer değer false olursa oyun başlar
        if (gameWaiting)
        {
            transform.eulerAngles = new Vector3(rotateX, rotateY, rotateZ);
            rotateZ += 0.4f;
            rotateY += 0.4f;
            rotateX += 0.4f;
            if (Input.GetMouseButtonDown(0)&&!isDead)
            {
                gameWaiting = false;
            }

        }
        else
        {
            scoreText.text = score.ToString();
            rb.useGravity = true;
            
            transform.eulerAngles = new Vector3(rotateX/3, 0, 0);
            rb.AddForce(Vector3.forward * speed);
            rotateX += speed;
            speed += Time.deltaTime * 0.07f;

            if (Input.GetMouseButtonDown(0))
            {
                if (changed)
                {
                    changed = false;
                    transform.position= new Vector3(transform.position.x-1.7f, transform.position.y , transform.position.z );
                }
                else
                {
                    changed = true;
                    transform.position= new Vector3(transform.position.x+1.7f, transform.position.y , transform.position.z );
                }
            }
            gameScreen.SetActive(true);
            waitScreen.SetActive(false);
        }
    }

    void win()
    {
        gameWaiting = true;
        isDead = true;
        gameScreen.SetActive(false);
        winScreen.SetActive(true);
        waitScreen.SetActive(false);
    }
    

    public void restart()
    {
        SceneManager.LoadScene(0);
    }

    void dead()
    {
        gameWaiting = true;
        isDead = true;
        gameScreen.SetActive(false);
        deadScreen.SetActive(true);
        waitScreen.SetActive(false);
    }

    public void exit()
    {
        Application.Quit();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "wall")
        {
            dead();
        }
        if (other.gameObject.tag == "Finish")
        {
            win();
        }
        if (other.gameObject.tag == "bonus")
        {
            score = score + Random.Range(10, 20);
            Destroy(other.gameObject);
        }
    }
}
