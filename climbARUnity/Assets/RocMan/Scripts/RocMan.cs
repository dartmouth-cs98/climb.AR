﻿using UnityEngine;

public class RocMan : MonoBehaviour
{

    // Game Objects
    public GameObject[] ghosts;
    public GameObject ghost;
    public GameObject ghostSprite;
    public GameObject livesRemaining;
    //public GameObject[] handholds;
    //public GameObject Handhold;
    // TODO: include handholds when the game state requires access to them...
    // TODO: include skeleton tracking game objects...

    // Other variables 
    public const int numGhosts = 1;

    void Start()
    {
        this.CreateGhosts();
    }

    void Update()
    {

    }

    void CreateGhosts()
    {
        int startX = 0; int startY = 0;
        int offset = 0;
        this.ghosts = new GameObject[RocMan.numGhosts];
        for (int i = 0; i < RocMan.numGhosts; i++)
        {
            Vector2 pos = new Vector2(startX + offset, startY);
            Vector3 scale = new Vector3(0.1f, 0.1f, 0.1f);

            this.ghosts[i] = GameObject.Instantiate(ghost);
            this.ghosts[i].name = "Ghost " + i;
            this.ghosts[i].transform.localPosition = pos;
            this.ghosts[i].transform.localScale = scale;

            GameObject ghostSpriteObject = GameObject.Instantiate(ghostSprite);
            ghostSpriteObject.transform.localPosition = pos;
            ghostSpriteObject.transform.localScale = scale;
            ghostSpriteObject.transform.SetParent(this.ghosts[i].transform);

            Rigidbody2D rigid = this.ghosts[i].AddComponent<Rigidbody2D>();
            rigid.isKinematic = true;

            CircleCollider2D col = this.ghosts[i].AddComponent<CircleCollider2D>();
            col.radius = 4.0f;
            col.enabled = true;
            col.isTrigger = true;

            this.ghosts[i].GetComponent<GhostMovement>().livesRemaining = livesRemaining;

            offset += 3;
        }
        // TODO: in GhostMovement script add movement and collision detection -- and upon collision, migrate to new scene: https://docs.unity3d.com/Manual/CollidersOverview.html
        // TODO: after collision stuff, add skeleton stuff
        // TODO: after skeleton collision, brainstorm mini game to build for RocMan
    }
}
