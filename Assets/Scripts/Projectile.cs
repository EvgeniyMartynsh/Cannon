using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
	
	public GameObject shoot_effect;
	public GameObject hit_effect;
	public GameObject firing_ship;

	Rigidbody2D rb;
    public static float Speed { get; set; } = 1.5f;

    // Use this for initialization
    void Start () {
		rb = GetComponent<Rigidbody2D>();
        rb.AddRelativeForce(Vector2.up * Speed, ForceMode2D.Impulse);

		GameObject obj = (GameObject)Instantiate(shoot_effect, transform.position - new Vector3(0, 0, 5), Quaternion.identity); //Spawn muzzle flash
		obj.transform.parent = firing_ship.transform;
		Destroy(gameObject, 5f); 
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	
	//void OnTriggerEnter2D(Collider2D col) {

	//	//Don't want to collide with the ship that's shooting this thing, nor another projectile.
	//	if (col.gameObject != firing_ship && col.gameObject.tag != "Projectile") {
	//		Instantiate(hit_effect, transform.position, Quaternion.identity);
	//		Destroy(gameObject);
	//	}
	//}
	
	
	
}
