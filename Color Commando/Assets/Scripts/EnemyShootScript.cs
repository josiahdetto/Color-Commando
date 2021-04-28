﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootScript : MonoBehaviour
{
    [SerializeField]
	int shootDelay = 3;
	
	public GameObject projectile;
    public Transform projectileSpawnPos;
	
	bool shooted;
	
	// Start is called before the first frame update
    void Start()
    {
        
    }
	
	void Shoot() {
		GameObject p = Instantiate(projectile,
								projectileSpawnPos.position,
								projectileSpawnPos.rotation);		
			
		shooted = true;
		Invoke("ResetShoot", shootDelay);
		
	}
	
	void ResetShoot() {
		shooted = false;
	}

    // Update is called once per frame
    void Update()
    {
        if (!shooted)
			Shoot();
    }
}
