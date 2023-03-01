using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    public static float Speed { get; set; } = 1.5f;

    public GameObject shoot_effect;
	public GameObject hit_effect;

	Rigidbody2D rb;
    

    void Start () {
		rb = GetComponent<Rigidbody2D>();
        rb.AddRelativeForce(Vector2.up * Speed, ForceMode2D.Impulse);

		GameObject obj = (GameObject)Instantiate(
			shoot_effect, 
			transform.position - new Vector3(0, 0, 5), 
			Quaternion.identity);

		Destroy(gameObject, 5f); 
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}


	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.tag != "Projectile")
		{
			Instantiate(hit_effect, transform.position, Quaternion.identity);
			Destroy(gameObject);

			if (collider.gameObject.tag == "Enemy")
			{
				Enemy enemy = collider.gameObject.GetComponent<Enemy>();
				enemy.TakeDamage(Cannon.Damage);

			}
		}
	}

}
