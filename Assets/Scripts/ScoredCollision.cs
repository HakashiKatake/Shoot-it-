using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoredCollision : MonoBehaviour
{
    private AudioSource netSwishSound;
    // Start is called before the first frame update
    void Start()
    {
        netSwishSound = GetComponent<AudioSource>();
    }

    private void OnTriggerExit2D(Collider2D other) {
        netSwishSound.Play();
    }
}
