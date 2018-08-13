using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour {

	float speed = 2;

	bool alive = true;

	int health = 3;

	public Material deathMaterial;

    public GameObject warningSymbolGameObject;

	int score = 0;

    bool isOnScreen = false;

    GameObject warningSymbol;

    Transform playerTransform;

	// Use this for initialization
	void Start () {

        warningSymbol = Instantiate(warningSymbolGameObject, transform.position, Quaternion.identity);

		//GetComponent<Collider2D>()    
        //GetComponent<Rigidbody2D>().AddForce(transform.forward * 100);
		
		foreach(GameObject g in GameObject.FindGameObjectsWithTag("Wall")) {
			Physics2D.IgnoreCollision(g.GetComponent<Collider2D>(), GetComponent<Collider2D>());
		}

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

	}
	
	// Update is called once per frame
	void Update () {
		if(alive) {
            //Set isOnScreen appropriately
            if(Mathf.Abs(transform.position.x) < 8 && Mathf.Abs(transform.position.y) < 5){
                isOnScreen = true;
            }


            //warning symbols
            //Draw ray from enemy to player
                Debug.DrawLine(transform.position, playerTransform.position);
            //warningSymbol = transform.GetChild(0).gameObject;
            //Only draw warning symbol if enemy is not visible, else destroy it
            if(isOnScreen == false){
                //Calculate which side the line intersects with
                //Slope of line
                float enemyPlayerLineSlope = (transform.position.y - playerTransform.position.y) / (transform.position.x - playerTransform.position.x);
                Vector2 enemyPosition = new Vector2(transform.position.x, transform.position.y);
                Vector2 playerPosition = new Vector2(playerTransform.position.x, playerTransform.position.y);
                //print(enemyPlayerLineSlope * 4.5);
                //Rectangle is 15.6 wide, 9 tall, centered at 0,0 (so half-values are 7.8 and 4.5)
                if(-4.5 <= enemyPlayerLineSlope * 7.8 && enemyPlayerLineSlope * 7.8 <= 4.5){
                    //Left or right side?
                    if(transform.position.x > playerTransform.position.x){
                        //right side
                        warningSymbol.transform.position = LineIntersection(enemyPosition, playerPosition, new Vector2(7.8f, 4.5f), new Vector2(7.8f, -4.5f));
                    } else if(transform.position.x < playerTransform.position.x){
                        //left side
                        warningSymbol.transform.position = LineIntersection(enemyPosition, playerPosition, new Vector2(-7.8f, 4.5f), new Vector2(-7.8f, -4.5f));
                    } else {
                        print("uh oh");
                    }
                } else {
                    //Top or bottom edge?
                    if(transform.position.y > playerTransform.position.y){
                        //top side
                        warningSymbol.transform.position = LineIntersection(enemyPosition, playerPosition, new Vector2(-7.8f, 4.5f), new Vector2(7.8f, 4.5f));
                    } else {
                        //bottom side
                        warningSymbol.transform.position = LineIntersection(enemyPosition, playerPosition, new Vector2(-7.8f, -4.5f), new Vector2(7.8f, -4.5f));
                    }
                }
                if (warningSymbol.transform.position == new Vector3(0, 0, 0))
                {
                    warningSymbol.GetComponent<SpriteRenderer>().enabled = false;
                }

            } else if(isOnScreen) {
                Destroy(warningSymbol);
            }


			// float z = transform.rotation.eulerAngles.z;
            transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform.position);

			float playerX = GameObject.FindGameObjectWithTag("Player").transform.position.x;
			float playerY = GameObject.FindGameObjectWithTag("Player").transform.position.y;
			transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(playerY - transform.position.y, playerX - transform.position.x));

            transform.Translate(Vector3.Normalize(GameObject.FindGameObjectWithTag("Player").transform.position - transform.position) * Time.deltaTime * speed);
			if(health <= 0) {
				death();
            }
		}
		if(transform.position.x > -8 && transform.position.x < 8 && transform.position.y < 4 && transform.position.y > -4) {
			foreach(GameObject g in GameObject.FindGameObjectsWithTag("Wall")) {
				Physics2D.IgnoreCollision(g.GetComponent<Collider2D>(), GetComponent<Collider2D>(), false);
			}
		}
		
	}

	private void death() {
		alive = false;
		GameObject.FindGameObjectWithTag("ScoreText").GetComponent<TextScript>().score += 5;
		GameObject.FindGameObjectWithTag("ScoreText").GetComponent<TextScript>().scoreTimer = 0;
		GetComponent<Renderer>().material = deathMaterial;
		// GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
		GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>().bullets += 5;
		Rigidbody2D rb = GetComponent<Rigidbody2D>();
		rb.mass = 10;
		GetComponent<ParticleSystem>().Play();
        warningSymbol.GetComponent<SpriteRenderer>().enabled = false;
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.tag == "Bullet") {
			if(alive) {
				health -= 1;
			}
            //Play bullet particle system on impact
            ParticleSystem psystem = other.GetComponent<ParticleSystem>();
            psystem.Play();
            //Set bullet speed to 0
            other.GetComponent<BulletScript>().speed = 0;
            //Hide mesh renderer
            other.GetComponent<MeshRenderer>().enabled = false;
            //Destroy bullet in 0.2 seconds (after particles have played)
            Destroy(other.gameObject, 0.2f);

		} else if(other.gameObject.tag == "SpuperBullet" && alive) {
			death();
		}
		// if(other.gameObject.tag == "SpuperBullet" && alive == false) {
        //     GameObject.FindGameObjectWithTag("ScoreText").GetComponent<TextScript>().score += 5;
		// 	Destroy(gameObject);

		// }
	}
	private void OnCollisionEnter2D(Collision2D other) {
		if(other.gameObject.tag == "Player" && alive) {
            
			SceneManager.LoadScene("End");
		}
	}

	private void OnBecameVisible()
	{
        //isOnScreen = true;
	}

    private static Vector3 LineIntersection(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4)
    {
        Vector3 intersection = new Vector3(0,0,0);
        float Ax, Bx, Cx, Ay, By, Cy, d, e, f, num/*,offset*/;
        float x1lo, x1hi, y1lo, y1hi;

        Ax = p2.x - p1.x;
        Bx = p3.x - p4.x;

        // X bound box test
        if (Ax < 0){
            x1lo = p2.x; x1hi = p1.x;
        } else {
            x1hi = p2.x; x1lo = p1.x;
        }
        if (Bx > 0) {
            if (x1hi < p4.x || p3.x < x1lo) return new Vector3(0,0,0);
        } else {
            if (x1hi < p3.x || p4.x < x1lo) return new Vector3(0, 0, 0);
        }

        Ay = p2.y - p1.y;
        By = p3.y - p4.y;

        // Y bound box test
        if (Ay < 0) {
            y1lo = p2.y; y1hi = p1.y;
        } else {
            y1hi = p2.y; y1lo = p1.y;
        }
        if (By > 0) {
            if (y1hi < p4.y || p3.y < y1lo) return new Vector3(0, 0, 0);
        } else {
            if (y1hi < p3.y || p4.y < y1lo) return new Vector3(0, 0, 0);
        }

        Cx = p1.x - p3.x;
        Cy = p1.y - p3.y;

        d = By * Cx - Bx * Cy;  // alpha numerator
        f = Ay * Bx - Ax * By;  // both denominator

        // alpha tests//
        if (f > 0) {
            if (d < 0 || d > f) return new Vector3(0, 0, 0);
        } else {
            if (d > 0 || d < f) return new Vector3(0, 0, 0);
        }

        e = Ax * Cy - Ay * Cx;  // beta numerator

        // beta tests 
        if (f > 0) {
            if (e < 0 || e > f) return new Vector3(0, 0, 0);
        } else {
            if (e > 0 || e < f) return new Vector3(0, 0, 0);
        }

        // check if parallel
        if (f == 0) return new Vector3(0, 0, 0);

        // compute intersection coordinates
        num = d * Ax; // numerator
        intersection.x = p1.x + num / f;

        num = d * Ay;
        intersection.y = p1.y + num / f;

        return intersection;
    }
}

