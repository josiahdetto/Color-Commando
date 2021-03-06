using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerShootScript : MonoBehaviour
{
    public GameObject projectile;
    public Transform projectileSpawnPos;
	
	private float fracScreenWidth;
    private float widthMinusFrac;
	private int screenHeight;
    private int screenWidth;
	
	bool shooted;	
	
	// Start is called before the first frame update
    void Start()
    {
        screenHeight = Screen.height;
        screenWidth = Screen.width;
        fracScreenWidth = screenWidth / fracScreenWidth;
        widthMinusFrac = screenWidth - fracScreenWidth;
    }
	
	void Shoot() {
		GameObject p = Instantiate(projectile,
								projectileSpawnPos.position,
								projectileSpawnPos.rotation);		
			
		shooted = true;
		Invoke("ResetShoot", 0.5f);
		
	}
	
	void ResetShoot() {
		shooted = false;
	}

    // Update is called once per frame
    void Update()
    {
		if (!shooted) {
			if (Input.touchCount > 0) {
				Touch touch = Input.GetTouch(0); // get first touch since touch count is greater than zero
				if (touch.position.x <= fracScreenWidth)
					if (touch.phase == TouchPhase.Stationary)
						Shoot();
			} else if(Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject()) {
				//Cache mouse position
				Vector2 mouseCache = Input.mousePosition;
				//If mouse x position is less than or equal to a fraction of the screen width
				if (mouseCache.x <= fracScreenWidth)
					Shoot();
			}
		}
				
    }
}
