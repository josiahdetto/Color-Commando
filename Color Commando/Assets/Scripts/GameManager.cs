﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject enemy;
	public MarklessAR markless;
	[SerializeField]
	float spawnDist;
	
	public bool isArcade;
	
	private WebCamTexture backCam;	 
	System.Random rnd = new System.Random();
	
	float zCoor;
	
	int height, width;
	
	// Start is called before the first frame update
    void Start()
    {
		zCoor = transform.position.z + spawnDist;
		width = Screen.width;
        height = Screen.height;
		
		if (isArcade)
			backCam = markless.backCam;
    }

    // Update is called once per frame
    void Update()
    {
		GameObject foe = GameObject.FindWithTag("Enemy");
		
		if (foe == null) {
			if (isArcade)
				ArcadeSpawnEnemy();
			else
				StartCoroutine(ClassicSpawnEnemy());
		}
	}
	
	void ArcadeSpawnEnemy() {
		float x = UnityEngine.Random.value;
		float y = UnityEngine.Random.value;
		
		//Vector3 spawnPos = new Vector3(x, y, zCoor);
		Vector3 spawnPos = Camera.main.ViewportToWorldPoint(new Vector3(x, y, spawnDist));
		
		Color colour = backCam.GetPixel((int)(spawnPos.x), (int)(spawnPos.y));
		Debug.Log(colour); 
		
		GameObject player = GameObject.FindWithTag("Player");
		GameObject foe = Instantiate(enemy, spawnPos, transform.rotation);
		foe.GetComponent<EnemyMovementScript>().target = player.transform;
		foe.GetComponent<MeshRenderer>().material.color = colour;
		
	}
	
	IEnumerator ClassicSpawnEnemy() {
		yield return new WaitForEndOfFrame();

        // Create a texture the size of the screen, RGB24 format
        Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);

        // Read screen contents into the texture
        tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        tex.Apply();
		
		float x = UnityEngine.Random.value;
		float y = UnityEngine.Random.value;
		
		//Vector3 spawnPos = new Vector3(x, y, zCoor);
		Vector3 spawnPos = Camera.main.ViewportToWorldPoint(new Vector3(x, y, spawnDist));
		
		Color colour = tex.GetPixel((int)(spawnPos.x), (int)(spawnPos.y));
		Debug.Log(colour); 
		
		GameObject player = GameObject.FindWithTag("Player");
		GameObject foe = Instantiate(enemy, spawnPos, transform.rotation);
		foe.GetComponent<EnemyMovementScript>().target = player.transform;
		foe.GetComponent<MeshRenderer>().material.color = colour;
	}
	
}