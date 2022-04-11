using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondPick : MonoBehaviour
{
    public AudioClip diamondSound;
    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player"){
            GameManager.Instance.numberOfDiamond += 1;
            AudioSource.PlayClipAtPoint(diamondSound, transform.position);
            gameObject.SetActive(false);
        }
    }
}
