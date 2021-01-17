using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveForce;

    private Rigidbody _rigidbody;

    private bool hasPowerUp;

    private float powerUpTime = 10;

    [SerializeField] private float powerUpForce;

    // It's better to make it private and use GameObject.find("NAME") in the method start
    public GameObject focalPoint;

    public GameObject[] powerUpIndicators;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        
        // Rotating around focal point makes it necessary to move the ball regarding the focal point forward direction
        _rigidbody.AddForce(focalPoint.transform.forward * (moveForce * forwardInput), ForceMode.Force);

        foreach (GameObject indicator in powerUpIndicators)
        {
            indicator.transform.position = this.transform.position + 0.5f * Vector3.down;
        }

        if (this.transform.position.y < -10)
        {
            SceneManager.LoadScene("Prototype 4");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            hasPowerUp = true;
            
            Destroy(other.gameObject);

            StartCoroutine(PowerUpCountDown());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hasPowerUp && collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Collision has happened");

            GameObject enemy = collision.gameObject;
            
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();

            // Collisions will always happen with the same distance, so it's no necessary to normalize the Vector
            Vector3 awayFromPlayer = enemy.transform.position - this.transform.position;
            
            enemyRigidbody.AddForce(awayFromPlayer * powerUpForce, ForceMode.Impulse);
        }
    }

    IEnumerator PowerUpCountDown()
    {
        foreach (GameObject indicator in powerUpIndicators)
        {
            indicator.gameObject.SetActive(true);

            yield return new WaitForSeconds(powerUpTime / powerUpIndicators.Length);

            indicator.gameObject.SetActive(false);
        }

        hasPowerUp = false;
    }
}
