using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosion : MonoBehaviour
{
    // Start is called before the first frame update
    private Collider2D bomb;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private float explosionDuration;
    [SerializeField] private int explosionRange;
    
    void Start()
    {
      bomb = gameObject.GetComponent<Collider2D> ();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerExit2D(Collider2D other) {
      bomb.isTrigger = false;
    }

    public void Explode() {
      GameObject explosion = Instantiate (explosionPrefab, this.gameObject.transform.position, Quaternion.identity) as GameObject;
      Destroy(explosion, this.explosionDuration);
      CreateExplosions (Vector2.left/2.5f);
      CreateExplosions (Vector2.right/2.5f);
      CreateExplosions (Vector2.up/2.5f);
      CreateExplosions (Vector2.down/2.5f);
      Destroy (this.gameObject);
    }

    private void CreateExplosions(Vector2 direction) {
		ContactFilter2D contactFilter = new ContactFilter2D ();

		Vector2 explosionDimensions = explosionPrefab.GetComponent<SpriteRenderer> ().bounds.size;
		Vector2 explosionPosition = (Vector2)this.gameObject.transform.position + (explosionDimensions.x * direction);
    
		for (int explosionIndex = 1; explosionIndex < explosionRange; explosionIndex++) {
			Collider2D[] colliders = new Collider2D[4];
			Physics2D.OverlapBox (explosionPosition, explosionDimensions, 0.0f, contactFilter, colliders);
			GameObject explosion = Instantiate (explosionPrefab, explosionPosition, Quaternion.identity) as GameObject;
			Destroy(explosion, this.explosionDuration);
			explosionPosition += (explosionDimensions.x * direction);
		}
	}
}
