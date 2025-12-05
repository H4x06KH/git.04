using System.Collections;
using System.ComponentModel.Design;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody body;
    public float force;
    public GameObject focalPoint;
    public GameObject indicator;
    private bool hasPowerup;
    public float powerupStrength;

    void SetPowerup(bool active)
    {
        hasPowerup = active;
        indicator.SetActive(active);
    }

   
   

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<Rigidbody>();
        SetPowerup(false);
    }

    // Update is called once per frame
    void Update()
    {
        indicator.transform.position = transform.position 
            + (Vector3.down * 0.55f);

        float verticalInput = Input.GetAxis("Vertical");
        body.AddForce(focalPoint.transform.forward * verticalInput);
    }
     void OnCollisionEnter(Collision collision)
    { 


        if (hasPowerup && collision.collider.CompareTag("Enemy"))
        {
            Debug.Log("Collision with" + collision.collider.gameObject.name
                + "Enemy while having a powerup");
            Rigidbody enemyBody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPLayer = (collision.gameObject.transform.position - transform.position).normalized;
            enemyBody.AddForce(powerupStrength * awayFromPLayer,ForceMode.Impulse);
        }
    }

     void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            SetPowerup(true);
            Destroy(other.gameObject);
            StopAllCoroutines();
            StartCoroutine(PowerupCountDown());
        }
        IEnumerator PowerupCountDown()
        {
            yield return new WaitForSeconds(5);
            Debug.Log("Powerup almost depleted");
            yield return new WaitForSeconds(2);
            Debug.Log("Powerup DEPLETED");
            SetPowerup(false);

        }
         
        
    }
}
