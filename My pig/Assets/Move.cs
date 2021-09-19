using System.Collections;
using UnityEngine;


public class Move : MonoBehaviour
{
	public static bool isInvincible = false;
	public static float timeSpentInvincible;
	public Texture2D lifeIconTexture;
	public static bool dead = false;
	public static int life = 100;
	[SerializeField] LayerMask layer;
	public float speed = 0.1f;
	public float limitx1 = -2, limitx = 16f, limity1 = -1, limity = 7;
	// NEED TO ADD
	public static Vector2 bombermanPosition, bombermanPositionRounded;

	Animator anim;

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
			
			if (left) { dir.y = speed; }
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
		Rect lifeIconRect = new Rect(10, 10, 32, 32);
		GUI.DrawTexture(lifeIconRect, lifeIconTexture);

		GUIStyle style = new GUIStyle();
		style.fontSize = 30;
		style.fontStyle = FontStyle.Bold;
		style.normal.textColor = Color.yellow;

		Rect labelRect = new Rect(lifeIconRect.xMax + 10, lifeIconRect.y, 60, 32);
		GUI.Label(labelRect, life.ToString(), style);
	}

	void OnGUI()
	{
		DisplayLifeCount();
	}
}
