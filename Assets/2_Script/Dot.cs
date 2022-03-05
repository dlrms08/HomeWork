using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dot : MonoBehaviour
{
    public DotType dotType = DotType.Top;
    public Color[] colors;
    private SpriteRenderer renderer;

    public Vector2Int pos;

    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        int rnd = Random.Range(0, 6);
        dotType = (DotType)rnd;
        renderer.color = colors[rnd];
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
