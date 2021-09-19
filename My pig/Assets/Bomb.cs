using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets;
public class Bomb : MonoBehaviour
{
    // Time after which the bomb explodes
    float time = 3.0f;

    public Player player;
    // Worm game objects, needed to destroy the worms after being hit 
    public GameObject worm1;
    public GameObject worm2;
    public GameObject worm3;
    public int explode_size = 2;
    // Explosion Prefab
    public GameObject explosion;
    private bool exploded = false;
    public bool isenemy=false;
    void Start()
    {
        Invoke("Explode", time);
    }
    void Explode2()
    {
        // center one
        Instantiate(explosion, Round(transform.position), Quaternion.identity); //1


        StartCoroutine(CreateExplosions(Vector3.forward));
        StartCoroutine(CreateExplosions(Vector3.right));
        StartCoroutine(CreateExplosions(Vector3.back));
        StartCoroutine(CreateExplosions(Vector3.left));


        //GetComponent<MeshRenderer>().enabled = false; //2
        exploded = true;
        //transform.Find("Collider").gameObject.SetActive(false); //3
        Destroy(gameObject, .3f); //4
        //player.bombs++;
    }
    void Explode()
    {
        StartCoroutine(CreateExplosions(Vector3.forward));
        StartCoroutine(CreateExplosions(Vector3.right));
        StartCoroutine(CreateExplosions(Vector3.back));
        StartCoroutine(CreateExplosions(Vector3.left));
        // Remove Bomb from game
        Destroy(gameObject);

        // Spawn Explosion
        var exp = Instantiate(explosion,
                    transform.position,
                    Quaternion.identity);
        exp.GetComponent<ADstr>().exploded=isenemy;
        // Destroy stuff
        Vector2 pos = transform.position;
        blck.destroyBlockAt(pos.x, pos.y);   // same
        blck.destroyBlockAt(pos.x, pos.y + 1); // top
        blck.destroyBlockAt(pos.x + 1, pos.y);   // right
        blck.destroyBlockAt(pos.x, pos.y - 1); // bottom
        blck.destroyBlockAt(pos.x - 1, pos.y);   // left


        destroyBomber();
    }
    private Vector3 Round(Vector3 v)
    {
        return new Vector3(Mathf.RoundToInt(v.x), Mathf.RoundToInt(v.y), Mathf.RoundToInt(v.z));
    }
    private IEnumerator CreateExplosions(Vector3 direction)
    {

        List<Vector3> instantiate_list = new List<Vector3>();
        //1
        for (int i = 1; i <= explode_size; i++)
        {
            //2
            RaycastHit hit;
            //3
            Physics.Raycast(Round(transform.position), direction, out hit,i);

            //4
            if (!hit.collider)
            {
                instantiate_list.Add(transform.position + (i * direction));

                //6
            }
            else
            { //7


                if (hit.collider.CompareTag("Breakable"))
                {
                    instantiate_list.Add(transform.position + (i * direction));
                }
                else if (hit.collider.CompareTag("Player") || hit.collider.CompareTag("powerup") || hit.collider.CompareTag("Bomb"))
                {
                    instantiate_list.Add(transform.position + (i * direction));
                    continue;
                }
                if (hit.collider.tag=="Player"){
                    //Move.BombExplosion();
                }
                print(hit.collider.tag);
                break;
            }

        }

        foreach (Vector3 v in instantiate_list)
        {
            Instantiate(explosion, v,
               //5 
               explosion.transform.rotation);
            //8
            yield return new WaitForSeconds(.05f);
        }
    }
    void destroyBomber()
	{
		if (Move.bombermanPositionRounded == BombDrop.roundedPos || Move.bombermanPositionRounded == BombDrop.rangeUp
						|| Move.bombermanPositionRounded == BombDrop.rangeLeft || Move.bombermanPositionRounded == BombDrop.rangeRight
						|| Move.bombermanPositionRounded == BombDrop.rangeDown)
		{
			Move.BombExplosion();
		}


		if (GetPosition.roundedWormPos1 == BombDrop.roundedPos || GetPosition.roundedWormPos1 == BombDrop.rangeUp
						|| GetPosition.roundedWormPos1 == BombDrop.rangeLeft || GetPosition.roundedWormPos1 == BombDrop.rangeRight
						|| GetPosition.roundedWormPos1 == BombDrop.rangeDown)
		{

			if (worm1 == null)
			{
				//worm1 = GameObject.Find("Worm1");
				//Destroy(worm1.gameObject);
			}
		}

		if (GetPosition.roundedWormPos2 == BombDrop.roundedPos || GetPosition.roundedWormPos2 == BombDrop.rangeUp
						|| GetPosition.roundedWormPos2 == BombDrop.rangeLeft || GetPosition.roundedWormPos2 == BombDrop.rangeRight
						|| GetPosition.roundedWormPos2 == BombDrop.rangeDown)
		{
			if (worm2 == null)
			{
				//worm2 = GameObject.Find("Worm2");
				//Destroy(worm2.gameObject);
			}
		}

		if (GetPosition.roundedWormPos3 == BombDrop.roundedPos || GetPosition.roundedWormPos3 == BombDrop.rangeUp
			|| GetPosition.roundedWormPos3 == BombDrop.rangeLeft || GetPosition.roundedWormPos3 == BombDrop.rangeRight
			|| GetPosition.roundedWormPos3 == BombDrop.rangeDown)
		{
			if (worm3 == null)
			{
				//worm3 = GameObject.Find("Worm3");
				//Destroy(worm3.gameObject);
			}
		}

	}
}
