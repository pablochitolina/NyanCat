using UnityEngine;
using System.Collections;

public class NyanScript : MonoBehaviour {
	
	public GameObject nyan;
	public GameObject rbNormal;
	public GameObject rbAcid;
	public GameObject asteroid1;
	public GameObject asteroid2;
	public GameObject asteroid3;
	public GameObject asteroid4;
	public GameObject body;
	public bool acid = false;
	public bool iniciou = false;
	public GameObject objDistancia;
	public GameObject menu;
	public bool morto = false;
	public GameObject soundOn;
	public GameObject soundOff;
	public GameObject objAcid;

	private GameObject rbInstance;
	private float tamRb;
	private float ultimoRb;
	private long numInstanceRb = 1;
	private Rigidbody2D rbNyan;
	private float forcaPulo = 25;
	private float tempoPress = 0.02f;
	private float tempoPressDef = 0.02f;

	private float tempoOscila = 0.2f;
	private float tempoOscilaDef = 0.2f;
	private int direcao = -1;
	private float distancia;
	private TextMesh textDistancia;
	private AudioSource audioSouce;
	private GameObject deadPuppy;

	private float freqMudaCor = 0.02f;
	private float mudaCor = 0.02f;
	private int cor = 1;

	private float tempoShine = 10f;
	private float defTempoShine = 10f;

	public GameObject tutorial;

	
	// Use this for initialization
	void Start () {


		audioSouce = Camera.main.GetComponent<AudioSource> ();
		deadPuppy = GameObject.Find ("Nyan/Puppy/dead");
		deadPuppy.SetActive (false);
		objAcid.SetActive(false);

		if (PlayerPrefs.GetString ("sound") != "mute") {
			soundOn.SetActive (true);
			soundOff.SetActive (false);
			audioSouce.mute = false;
		} else {
			soundOn.SetActive (false);
			soundOff.SetActive (true);
			audioSouce.mute = true;
		}

		rbNyan = nyan.GetComponent<Rigidbody2D> ();
		Renderer rendRB = rbNormal.GetComponent<Renderer> ();
		tamRb = rendRB.bounds.size.x;
		ultimoRb = rbNormal.transform.position.x + 0.35f;

		textDistancia = objDistancia.GetComponent<TextMesh> ();
		
	}

	private Color HexToColor(string hex){
		byte r = byte.Parse (hex.Substring(0,2),System.Globalization.NumberStyles.HexNumber);
		byte g = byte.Parse (hex.Substring(2,2),System.Globalization.NumberStyles.HexNumber);
		byte b = byte.Parse (hex.Substring(4,2),System.Globalization.NumberStyles.HexNumber);

		return new Color32 (r,g,b,255);
	}
	
	// Update is called once per frame
	void Update () {



		if (acid) {
			/*mudaCor -= Time.deltaTime;
			if(mudaCor < 0 ){
				switch(cor){
				case 1:
					Camera.main.backgroundColor = HexToColor("FF0000");
					cor = 2;
					break;
				case 2:
					Camera.main.backgroundColor = HexToColor("FF00FF");
					cor = 3;
					break;
				case 3:
					Camera.main.backgroundColor = HexToColor("0000FF");
					cor = 4;
					break;
				case 4:
					Camera.main.backgroundColor = HexToColor("00FFFF");
					cor = 5;
					break;
				case 5:
					Camera.main.backgroundColor = HexToColor("00FF00");
					cor = 6;
					break;
				default:
					Camera.main.backgroundColor = HexToColor("FFFF00");
					cor = 1;
					break;

				}
				mudaCor = freqMudaCor;
			}*/
			tempoShine -=Time.deltaTime;
			if(tempoShine < 0){

				acid = false;
				body.GetComponent<Animator>().Play("bodyNormal");
				asteroid1.GetComponent<Animator>().Play("asteroid1Normal");
				asteroid2.GetComponent<Animator>().Play("asteroid2Normal");
				asteroid3.GetComponent<Animator>().Play("asteroid3Normal");
				asteroid4.GetComponent<Animator>().Play("asteroid4Normal");
				
				Camera.main.backgroundColor = Color.black;
				Debug.Log ("Muda acid");

				tempoShine = defTempoShine;
			}
		}

		if (!morto){
			nyan.transform.Translate (Vector2.right * Time.deltaTime * 5);
			Camera.main.transform.Translate (Vector2.right * Time.deltaTime * 5);
			if (iniciou) {
				distancia +=Time.deltaTime;
				textDistancia.text = distancia.ToString("0.00") + " AU";
			}
		}

		tempoPress -= Time.deltaTime;

		if (Input.GetButton ("Fire1") && tempoPress <= 0 && !morto) {
			rbNyan.AddForce (Vector2.up * forcaPulo);
			tempoPress = tempoPressDef;
			iniciou = true;
			
		} else {
			if(iniciou){
				tutorial.SetActive (false);
				rbNyan.AddForce (Vector2.up * forcaPulo * (-0.25f));
				//rbNyan.gravityScale = 0.9f;
			}else{
				tempoOscila -= Time.deltaTime;
				if(tempoOscila <= 0){
					direcao *= (-1);
					tempoOscila = tempoOscilaDef;
				}
				nyan.transform.Translate (Vector2.up * Time.deltaTime * direcao);
			}
		}
		

		if (((nyan.transform.position.x ) - ultimoRb) >= tamRb && !morto) {

			if(!acid){
				rbInstance = Instantiate (rbNormal);
			}else{
				rbInstance = Instantiate (rbAcid);
			}
			rbInstance.transform.position = new Vector3 (rbNormal.transform.position.x + (tamRb * numInstanceRb), nyan.transform.position.y, rbNormal.transform.position.z);
			ultimoRb = rbNormal.transform.position.x + (tamRb * numInstanceRb) + 0.35f;
			numInstanceRb++;
			
			Destroy (rbInstance, 1.2f);
		}


		if (Input.GetButtonDown ("Fire1")) {
			RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
			//Debug.Log ("Raycast: " + hit.collider.gameObject.tag);
			if (hit.collider != null) {
				if (hit.collider.gameObject.tag == "exit") {
					Application.Quit();
				}
				if (hit.collider.gameObject.tag == "again") {
					Application.LoadLevel("jogo");
				}
				if (hit.collider.gameObject.tag == "soundOn") {
					PlayerPrefs.SetString ("sound","unmute");
					soundOn.SetActive (true);
					soundOff.SetActive (false);
					audioSouce.mute = false;
				}
				if (hit.collider.gameObject.tag == "soundOff") {
					PlayerPrefs.SetString ("sound","mute");
					soundOn.SetActive (false);
					soundOff.SetActive (true);
					audioSouce.mute = true;
				}
			}
		}
	}

	void mostraMenu(){

		float maxPref = PlayerPrefs.GetFloat ("maxLY",0f);

		menu.SetActive (true);
		menu.transform.position = new Vector3(Camera.main.transform.position.x,Camera.main.transform.position.y,0f);
		textDistancia.text = "";
		GameObject.Find ("texto/max").GetComponent<TextMesh> ().text = "MAX: " + maxPref.ToString("0.00") + " AU";
		GameObject.Find ("texto/now").GetComponent<TextMesh> ().text = "NOW: " + distancia.ToString("0.00") + " AU";

		if (distancia > maxPref) {
			PlayerPrefs.SetFloat ("maxLY",distancia);
		}

	}

	void OnCollisionEnter2D(Collision2D coll) {
		if(coll.gameObject.tag == "colDown"){
			nyan.transform.position = new Vector3(nyan.transform.position.x,nyan.transform.position.y,-20f);
			if(!morto){
				mortoFunc ();
			}
		}
		if (coll.gameObject.tag == "obstaculo" || coll.gameObject.tag == "colUp") {
			mortoFunc ();
		}

	}

	void mortoFunc (){
		acid = false;
		body.GetComponent<Animator>().Play("bodyNormal");
		asteroid1.GetComponent<Animator>().Play("asteroid1Normal");
		asteroid2.GetComponent<Animator>().Play("asteroid2Normal");
		asteroid3.GetComponent<Animator>().Play("asteroid3Normal");
		asteroid4.GetComponent<Animator>().Play("asteroid4Normal");

		Camera.main.backgroundColor = Color.black;

		morto = true;
		GameObject.Find ("Nyan/Puppy/puppy").SetActive (false);
		body.SetActive (false);
		deadPuppy.SetActive (true);
		mostraMenu();


	}

	void OnTriggerEnter2D(Collider2D coll){
		if(coll.gameObject.tag == "acid"){
			acid = true;
			tempoShine = defTempoShine;
			objAcid.SetActive(false);
			body.GetComponent<Animator>().Play("bodyAcid");
			asteroid1.GetComponent<Animator>().Play("asteroid1Acid");
			asteroid2.GetComponent<Animator>().Play("asteroid2Acid");
			asteroid3.GetComponent<Animator>().Play("asteroid3Acid");
			asteroid4.GetComponent<Animator>().Play("asteroid4Acid");

		}
		
	}

}
