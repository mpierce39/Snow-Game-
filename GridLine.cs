using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridLine : MonoBehaviour {


    public void Set(Color color, Vector2 position, Vector2 size)
    {
        transform.position = position;
        transform.localScale = size;
        GetComponent<SpriteRenderer>().color = color;
    }
}
