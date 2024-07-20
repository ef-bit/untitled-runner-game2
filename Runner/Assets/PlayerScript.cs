using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
public class PlayerScript : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float speed, shift, jumpForce;
    [SerializeField] AudioClip itemSFX, shieldSFX, obstacleSFX, destroySFX;
    [SerializeField] AudioSource sound, music;  // sound: char sfx, music: camera bg music
    [SerializeField]Animator anim;
    [SerializeField] TMP_Text score;
    [SerializeField] GameObject gameOverMenu;
    [SerializeField] GameObject itemVFX,ShieldVFX,obstacleVFX;

    bool isGameOver;

    bool isShield;
    
    

    float roundScore;
    void Start()
    {
      
    }

    // fizik sistemiyle kullanılır
    private void FixedUpdate() 
    {
        if(!isGameOver)
        {
        // karakteri haraket ettireceğiz
        rb.MovePosition(transform.position + transform.forward * speed * Time.deltaTime);
        }
    }
    
    void Update()
    {
        if(!isGameOver)
        {
            roundScore += Time.deltaTime;

            score.text = "Score" + roundScore.ToString("f1");
        if (Input.GetKeyDown(KeyCode.A)&&transform.position.x > -shift)
        {
            // sola
            transform.Translate(-shift, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.D)&&transform.position.x < shift)
        {
            // sağa
            transform.Translate(shift, 0, 0);
        }
        if(Input.GetKeyDown(KeyCode.Space) && rb.velocity.y ==0 )  
        {
        rb.AddForce(transform.up *jumpForce, ForceMode.Impulse);
        }
        if(rb.velocity.y>0)
        {
          anim.SetBool("jump" , true);
        }
        if(rb.velocity.y == 0)
        {
          anim.SetBool("jump" , false);
        }
        }
    }

     private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.CompareTag("Obstacle"))
        {
            if(isShield)
            {
                Destroy(other.gameObject);
                sound.clip=destroySFX;
                sound.Play();
            }
            else
            {
                isGameOver = true;
                gameOverMenu.SetActive(true);
                anim.SetBool("death" , true);

                GameObject vfx = Instantiate(obstacleVFX, transform.position, transform.rotation);
                Destroy(vfx, 3f);
                sound.clip=obstacleSFX;
                sound.Play();
                music.Stop();
             }
        }
        }

         private void OnTriggerEnter(Collider other) 
        {
            if(other.CompareTag("Money"))
            {
                 roundScore+=5;
                  score.text = "Score" + roundScore.ToString("f1");
                 GameObject vfx = Instantiate(itemVFX, other.transform.position, other.transform.rotation);
                    Destroy(vfx, 3f);
                  sound.clip=itemSFX;
                  sound.Play();
                  Destroy(other.gameObject);
            }
            else if (other.CompareTag("Shield"))
            {
                 isShield = true; 
                 Invoke("DeactivateShield",6f);   // kalkan aktif
                 GameObject vfx = Instantiate(ShieldVFX, transform.position + transform.up, other.transform.rotation);
                 vfx.transform.SetParent(this.transform);        // vfx'i chield'ım haline getirdim
                Destroy(vfx, 6f);
                sound.clip=shieldSFX;
                sound.Play();
                Destroy(other.gameObject);  // sahneden kaldır
            }
        }
        
        void DeactivateShield()
        {
            isShield = false;
        }
}