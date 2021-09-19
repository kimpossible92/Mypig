using System.Collections;
using UnityEngine;


public class ADstr : MonoBehaviour
{
    public float time;
    public int explode_size = 2;
    // Explosion Prefab
    public GameObject explosion;
    public bool exploded = false;
    IEnumerator Start()
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
    void Explode()
    {
        // Remove Bomb from game
        Destroy(gameObject);
        //destroyBomber();
    }
    private void OnCollisionEnter(Collision collision)
    {
    }
}
