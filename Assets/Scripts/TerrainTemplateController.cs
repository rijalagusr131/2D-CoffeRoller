using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainTemplateController : MonoBehaviour
{
    private const float debugLineHeight = 10.0f;

    public GameObject[] diamondContainer;

    public void SpawnObject(){
        if(diamondContainer != null){
            for(int i = 0; i < diamondContainer.Length; i++){
                if(!diamondContainer[i].activeSelf){
                    diamondContainer[i].SetActive(true);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Debug.DrawLine(transform.position + Vector3.up * debugLineHeight / 2, transform.position + Vector3.down * debugLineHeight / 2, Color.green);
    }
}
