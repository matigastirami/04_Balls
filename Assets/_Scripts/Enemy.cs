using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    private Rigidbody _rigidbody;

    [SerializeField]
    private float moveForce;

    private GameObject _player;
    
    // Start is called before the first frame update
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        _player = GameObject.Find("Player");
    }

    // Update is called once per frame
    private void Update()
    {
        // Movement vector = destination - origin
        // Normalize the vector because if not, the enemy will accelerate a lot if the player is futher
        Vector3 lookDirection = (_player.transform.position - this.transform.position).normalized;

        _rigidbody.AddForce(lookDirection * moveForce, ForceMode.Force);

        // Destroy if the enemy falls from the game zone
        // Other technique: Creating an invisible collider (A "Kill zone") with a trigger.
        if (this.transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }
}
