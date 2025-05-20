using UnityEngine;
using System.Collections;
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float jumpForce = 14f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float touchDeadZone = 50f;
    //int PointPlataform = 0;
    [Header("Score Settings")]
    [SerializeField] private float scoreMultiplier = 1f;
    [SerializeField] private ScoreManager scoreManager;

    private Rigidbody rb;
    private BoxCollider coll;
    private bool isGrounded;
    private float highestY;
    private Vector3 startPosition;
    private Vector2 touchStartPos;
    private bool isTouching = false;
    private bool canPassThrough = false;
   

    [Header("Platform Settings")]
    [SerializeField] private float platformPassThroughTime = 0.5f;
    [SerializeField] private LayerMask platformLayer;
    private bool isJumpingFromPlatform = false;
    private Collider playerCollider;

    private void Awake()
    {
        playerCollider = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<BoxCollider>();
        startPosition = transform.position;
        highestY = startPosition.y;
    }

    private void Update()
    {
        HandleMobileInput();
        CheckFall();
    }

    private void HandleMobileInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    isTouching = true;
                    touchStartPos = touch.position;
                    break;

                case TouchPhase.Moved:
                case TouchPhase.Stationary:
                    float touchPosX = touch.position.x;
                    float screenMiddle = Screen.width / 2f;

                   
                    if (touchPosX < screenMiddle - touchDeadZone)
                    {
                        rb.linearVelocity = new Vector3(-moveSpeed, rb.linearVelocity.y, 0);
                    }
                
                    else if (touchPosX > screenMiddle + touchDeadZone)
                    {
                        rb.linearVelocity = new Vector3(moveSpeed, rb.linearVelocity.y, 0);
                    }
                    else
                    {
                        rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
                    }
                    break;

                case TouchPhase.Ended:
                    isTouching = false;
                    rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
                    break;
            }
        }
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, 0);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false;
    }



    private void CheckFall()
    {
        if (transform.position.y < startPosition.y - 5f)
        {
            GlobalSceneManager.Instance.LoadNormal("GameOver");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        

        if (collision.gameObject.CompareTag("Plataform") && !isJumpingFromPlatform)
        {
            ContactPoint contact = collision.contacts[0];

           
            if (contact.normal.y > 0.1f)
            {
                Destroy(collision.gameObject);


                scoreManager.AddScore(1);


                rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, 0);
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

               
                StartCoroutine(EnablePlatformPassThrough());
            }
        }
    }
    private bool IsPlatform(GameObject obj)
    {
        return platformLayer == (platformLayer | (1 << obj.layer));
    }

    private IEnumerator EnablePlatformPassThrough()
    {
        isJumpingFromPlatform = true;

        // Ignorar colisiones con la capa de plataformas
        Physics.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Plataform"), true);

        yield return new WaitForSeconds(platformPassThroughTime);

        // Restaurar colisiones con la capa de plataformas
        Physics.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Plataform"), false);

        isJumpingFromPlatform = false;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            isGrounded = false;
        }
    }

}