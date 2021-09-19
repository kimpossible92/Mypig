using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets
{
    public class Worm : MonoBehaviour
    {
        public float speed = 0.02f;
        public static Vector2 roundedWormPos;
        public GameObject bombPrefab;
        Animator anim;
        private ArrayList wait_bombs = new ArrayList(); 
        private Player player;
        // Current State
        bool moving = false;

        // Wait time Remaining for idle
        int idle_wait = 0;

        // Current movement goal
        Vector2 goal;
        Vector3 startpos1;
        public float limitx1 = -2, limitx = 16f,limity1=-1,limity=7;
        void Start()
        {
            startpos1 = transform.position;
            // Get Animation component, set default animation
            anim = GetComponent<Animator>();
            anim.SetInteger("Direction", 4);
            player = GetComponent<Player>();
            // Initialize movement variables
            goal = transform.position;
        }
        private void dropBomb()
        {
            if (bombPrefab)
            {
                GameObject go = Instantiate(bombPrefab, new Vector3(Mathf.RoundToInt(transform.position.x),
                 Mathf.RoundToInt(transform.position.y), bombPrefab.transform.position.z),
                 bombPrefab.transform.rotation);
                go.GetComponent<Bomb>().isenemy=true;
                wait_bombs.Add(go);

                //go.GetComponent<Bomb>().explode_size = player.explosion_power;
                //go.GetComponent<Bomb>().player = player;
            }
        }
        int lifes = 8;
        void OnTriggerEnter(Collider collider)
        {
            if (collider.tag == "Explosion")
            {
                lifes--; print(lifes);
                transform.position = startpos1;
                if (lifes <= 0) { Destroy(gameObject); }
            }
        }
        void FixedUpdate()
        {
            if(transform.position.x<limitx1|| transform.position.x > limitx|| transform.position.y < limitx1|| transform.position.y > limity)
            {
                transform.position = startpos1;
            }
            Vector2 pos = transform.position;

            if (moving)
            {
                if (pos == goal)
                {
                    moving = false;
                    idle_wait = Random.Range(1, 100);
                }

                else
                {
                    Vector2 v = Vector2.MoveTowards(pos, goal, speed);
                    transform.position = v;
                }
            }

            else
            {
                // Idle
                if (idle_wait > 0)
                {
                    // Update the animation parameter
                    anim.SetInteger("Direction", 4);
                    if(Vector2.Distance(transform.position, FindObjectOfType<Move>().transform.position) < 2f)
                    {
                        int pr = Random.Range(0,3);
                        //if(pr==0)dropBomb();
                    }
                    // Wait a bit so it doesn't look nervous
                    --idle_wait;
                }
                else
                {
                    // Find valid Directions
                    List<int> dirs = validDirections();

                    // Select one of them randomly
                    int dir = dirs[Random.Range(0, dirs.Count)];

                    // Update animation parameter
                    anim.SetInteger("Direction", dir);

                    // Calculate next goal based on direction
                    switch (dir)
                    {
                        case 0: goal = pos + new Vector2(0, 1); break;
                        case 1: goal = pos + new Vector2(1, 0); break;
                        case 2: goal = pos + new Vector2(0, -1); break;
                        case 3: goal = pos + new Vector2(-1, 0); break;
                    }

                    // Change state to moving
                    moving = true;
                }
            }
        }

        List<int> validDirections()
        {
            List<int> res = new List<int>();
            Vector2 pos = transform.position;
            pos.x = Mathf.Round(pos.x);
            pos.y = Mathf.Round(pos.y);

            if (!blck.getBlockAt(pos.x, pos.y + 1)) // up
                res.Add(0);

            if (!blck.getBlockAt(pos.x + 1, pos.y)) // right
                res.Add(1);

            if (!blck.getBlockAt(pos.x, pos.y - 1)) // down
                res.Add(2);

            if (!blck.getBlockAt(pos.x - 1, pos.y)) // left
                res.Add(3);

            return res;
        }
    }
}