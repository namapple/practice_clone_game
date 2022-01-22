using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScaler : MonoBehaviour
{
    void Start()
    {
        float worldHeight = Camera.main.orthographicSize * 2f;
        float worldWidth = worldHeight * Screen.width / Screen.height;

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        float height = sr.bounds.size.y;
        float width = sr.bounds.size.x;

        Vector3 tempScale = transform.localScale;
        tempScale.x = worldWidth / width;
        tempScale.y = worldHeight / height;
        transform.localScale = tempScale;
    }
}
