using System.Collections;
using UnityEngine;


public class Move : MonoBehaviour
{
	public static bool isInvincible = false;
	public static float timeSpentInvincible;
	public Texture2D lifeIconTexture;
	public static bool dead = false;
	public static int life = 100;
	[SerializeField] LayerMask layer; public float speed2 = 0.04f;
	public float speed = 0.1f;[SerializeField]
	bool showGUI = true;
	public float limitx1 = -2, limitx = 16f, limity1 = -1, limity = 7;
	// NEED TO ADD
	public static Vector2 bombermanPosition, bombermanPositionRounded;
	Vector2 dir2;int dr=5;
	Animator anim;
	void OnGUI()
	{
		if (!showGUI)
			return;
		
		var down = Physics.Raycast(transform.position, Vector2.down, 0.3f, layer);
		var up = Physics.Raycast(transform.position, Vector2.up, 0.3f, layer);
		var left = Physics.Raycast(transform.position, Vector2.left, 0.3f, layer);
		var right = Physics.Raycast(transform.position, Vector2.right, 0.3f, layer);
		if (GUI.Button(new Rect(335,15, 160, 40), "W"))
		{
			anim.SetInteger("Direction", 0);dr = 0;
		}
		if (GUI.Button(new Rect(15, 55, 160, 50), "A"))
		{
			anim.SetInteger("Direction", 3); dr = 3;
		}

		if (GUI.Button(new Rect(275, 55, 160, 50), "S"))
		{
			anim.SetInteger("Direction", 2); dr = 2;
		}
		if (GUI.Button(new Rect(575, 55, 160, 50), "D"))
		{
			anim.SetInteger("Direction", 1); dr = 1;
        }
        //if (Input.GetMouseButtonUp(0))
        //{
        //    anim.SetInteger("Direction", 1); dr = 5;
        //    dir2 = Vector2.zero;
        //}
        if (GUI.Button(new Rect(115, 15, 150, 40), "Stop"))
		{
			anim.SetInteger("Direction", 1); dr = 5;
			dir2 = Vector2.zero;
		}
		if (dr == 0)
		{
			dir2.x = 0;
			if (up) { dir2.y = -speed2; }
			else { dir2.y = speed2; }
		}
		if (dr == 3)
		{
			//anim.SetInteger("Direction", 3); dr = 3;
			dir2.y = 0;
			if (left) { dir2.x = speed2; }
			else { dir2.x = -speed2; }
		}

		if (dr == 2)
		{
			dir2.x = 0;
			//anim.SetInteger("Direction", 2); dr = 2;
			if (down) { dir2.y = speed2; }
			else dir2.y = -speed2;
		}
		if (dr==1)
		{
			dir2.y = 0;
			//anim.SetInteger("Direction", 1); dr = 1;
			if (right) { dir2.x = -speed2; }
			else dir2.x = speed2;
		}
		transform.Translate(dir2);
		DisplayLifeCount();
	}
	void Start()
	{
		anim = GetComponent<Animator>();
		dead = false;
		life = 100; startpos1 = transform.position;
	}
	

	void Update()
	{
		if (transform.position.x < limitx1 || transform.position.x > limitx || transform.position.y < limitx1 || transform.position.y > limity)
		{
			transform.position = startpos1;
		}
		Vector2 dir = Vector2.zero;
		var down = Physics.Raycast(transform.position, Vector2.down, 0.3f, layer);
		var up	= Physics.Raycast(transform.position, Vector2.up, 0.3f, layer);
		var left = Physics.Raycast(transform.position, Vector2.left, 0.3f, layer);
		var right = Physics.Raycast(transform.position, Vector2.right, 0.3f, layer);
		Debug.DrawRay(transform.position, Vector2.down * 0.3f, Color.green);
		Debug.DrawRay(transform.position, Vector2.up * 0.3f, Color.green);
		Debug.DrawRay(transform.position, Vector2.left * 0.3f, Color.green);
		Debug.DrawRay(transform.position, Vector2.right * 0.3f, Color.green);
		if (Input.GetKey(KeyCode.UpArrow)|| Input.GetKeyDown(KeyCode.W))
		{
			anim.SetInteger("Direction", 0);
			if (up) { dir.y = -speed; }
            else { dir.y = speed; }
		}
		else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
		{
			anim.SetInteger("Direction", 1);
			if (right) { dir.x = -speed; }
			else dir.x = speed;
		}
		else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
		{
			anim.SetInteger("Direction", 2);
		    if (down) { dir.y = speed; }
			else dir.y = -speed;
		}
		else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
		{
			anim.SetInteger("Direction", 3);
			if (left) { dir.x = speed; }
			else { dir.x = -speed; }
		}
		transform.Translate(dir);

		bombermanPosition = transform.position;
		float tempX = Mathf.Round(bombermanPosition.x);
		float tempY = Mathf.Round(bombermanPosition.y);
		//Debug.Log (bombermanPosition);
		bombermanPositionRounded = new Vector2(tempX, tempY);

		if (dead)
		{
			Application.LoadLevel("SampleScene");
		}

		if (isInvincible)
		{
			timeSpentInvincible += Time.deltaTime;

			if (timeSpentInvincible < 3f)
			{
				float remainder = timeSpentInvincible % .3f;
                GetComponent<Renderer>().enabled = remainder > .15f;
            }

			else
			{
				GetComponent<Renderer>().enabled = true;
				isInvincible = false;
			}
		}
	}
	Vector3 startpos1;
	void OnTriggerEnter(Collider collider)
	{
        if (collider.tag == "enemy")
		{
			
			life -= 10; transform.position = startpos1;
			if (life <= 0)
            {
                dead = true;
            }
            isInvincible = true;
            timeSpentInvincible = 0; 
		}
	}


	public static void BombExplosion()
	{
		life -= 20;
		if (life <= 0)
		{
			dead = true;
		}
		isInvincible = true;
		timeSpentInvincible = 0;
	}

	void DisplayLifeCount()
	{
		Rect lifeIconRect = new Rect(10, 150, 32, 32);
		GUI.DrawTexture(lifeIconRect, lifeIconTexture);

		GUIStyle style = new GUIStyle();
		style.fontSize = 30;
		style.fontStyle = FontStyle.Bold;
		style.normal.textColor = Color.yellow;

		Rect labelRect = new Rect(lifeIconRect.xMax + 10, lifeIconRect.y, 60, 32);
		GUI.Label(labelRect, life.ToString(), style);
	}

}
