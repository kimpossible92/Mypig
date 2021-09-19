using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blck : MonoBehaviour
{
    public bool destroyable;

    public static blck getBlockAt(float x, float y)
    {
        blck[] blocks = FindObjectsOfType<blck>();
        foreach (blck b in blocks)
        {
            if (b.transform.position.x == x &&
                b.transform.position.y == y)
                return b;
        }
        return null;
    }

    public static void destroyBlockAt(float x, float y)
    {
        blck b = getBlockAt(x, y);
        if (b != null && b.destroyable)
            Destroy(b.gameObject);
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
