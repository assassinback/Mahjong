using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateClouds : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyCloud());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator DestroyCloud()
    {
        yield return new WaitForSeconds(5f);
        Destroy(this.gameObject);
    }
}
