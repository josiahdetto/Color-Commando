﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootScript : MonoBehaviour
{
    [SerializeField]
	int shootDelay = 3;
	
	public GameObject projectile;
    public Transform[] spawnPos;
	
	GameObject player;
	bool shooted;
	int index;
	
	// Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }
	
	void Shoot() {								
		
		float temp = 0;
		float dist = 0;
		for (int i = 0; i < spawnPos.Length; i++) {
			temp = Vector3.Distance(player.transform.position,
									spawnPos[i].position);
			if (temp >= dist) {
				dist = temp;
				index = i + 1;
			}
		}
		GameObject p = Instantiate(projectile,	
									spawnPos[index].position,
									spawnPos[index].rotation);
		p.GetComponent<EnemyProjectileScript>().face = index;
		p.GetComponent<EnemyProjectileScript>().colour = GetComponent<MeshRenderer>().material.color;
		
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
