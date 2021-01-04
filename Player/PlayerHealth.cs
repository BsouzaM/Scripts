using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {
	public static bool dead = false;

	float originalWidth = 1920.0f; //Transforme esses em float para corrigir o problema de placement
	float originalHeight = 1080.0f;
	Vector3 scale;
	public GUIStyle text;
	public Texture2D background;
	public Sprite deadSprite;

	void Awake()
	{
        
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (dead == true) {

			killPlayer ();

			if (Input.GetKeyDown (KeyCode.R)) {
				revivePlayer ();
				SceneManager.LoadScene (SceneManager.GetActiveScene().name);
			}
		}
	}

	void killPlayer()
	{
		if (GetComponent<PlayerAnimate> ().enabled == true) {

            PlayerAnimate pa = GetComponent<PlayerAnimate> ();
			PlayerMovement pm = GetComponent<PlayerMovement> ();
			RotateToCursor rot = GetComponent<RotateToCursor> ();
			WeaponAttack wa = GetComponent<WeaponAttack> ();
			legDir ld = GetComponentInChildren<legDir> ();

            wa.dropWeapon ();

            pa.legsSprite.sprite = null;
			pa.legsSprite.enabled = false;
			ld.enabled = false;

			pa.torsoSprite.sprite = deadSprite;
			pa.enabled = false;

			rot.enabled = false;
			wa.enabled = false;

			pm.enabled = false;
			CircleCollider2D col = GetComponent<CircleCollider2D> ();
			col.enabled = false;
		}
	}

	void revivePlayer(){

		PlayerAnimate pAnimation = GetComponent<PlayerAnimate>();
		PlayerMovement pMovement = GetComponent<PlayerMovement>();
		RotateToCursor rotCursor = GetComponent<RotateToCursor>();
		WeaponAttack wAttack = GetComponent<WeaponAttack>();
		legDir ld = GetComponentInChildren<legDir>();

        wAttack.dropWeapon ();

        pAnimation.legsSprite.enabled = true;
		ld.enabled = true;
		pAnimation.enabled = true;
		rotCursor.enabled = true;
		wAttack.enabled = true;
		pMovement.enabled = true;

		CircleCollider2D col = GetComponent<CircleCollider2D>();

        col.enabled = true;
		dead = false;
	}

	void OnGUI()
	{
		GUI.depth = 0;
		scale.x = Screen.width/originalWidth;
		scale.y = Screen.height/originalHeight;
		scale.z =1;

        var svMat = GUI.matrix;
		GUI.matrix = Matrix4x4.TRS(Vector3.zero,Quaternion.identity,scale);

		if (dead == true) {
			Rect posForRestart = new Rect (100,originalHeight-200,500,150);
			GUI.DrawTexture (posForRestart,background);
			GUI.Box (posForRestart,"Press 'R' to restart",text);
		}

		GUI.matrix = svMat;
	}
}
