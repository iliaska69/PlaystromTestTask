using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskObj : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().material.renderQueue = 3002;
    }
}
