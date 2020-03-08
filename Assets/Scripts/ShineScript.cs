using UnityEngine;
using System.Collections;

public class ShineScript : MonoBehaviour {

	private GameObject shineEscolhido;
	public GameObject shineNormal;
	public GameObject shine1;
	public GameObject shine2;
	public GameObject shine3;
	public GameObject shine4;
	public GameObject shine5;
	public GameObject shine6;
	private GameObject shineInstance;
	private float freqShine = 0.01f;
	private float freqShineDef = 0.01f;

	public NyanScript nyan;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		freqShine -= Time.deltaTime;
		if (freqShine <= 0) {
			if(nyan.acid){
				int obstac = Random.Range(0,5);
				switch(obstac){
				case 0:

					shineEscolhido = shine1;
					break;
				case 1:
					shineEscolhido = shine2;
					break;
				case 2:
					shineEscolhido = shine3;
					break;
				case 3:
					shineEscolhido = shine4;
					break;
				case 4:
					shineEscolhido = shine5;
					break;
				default:
					shineEscolhido = shine6;
					break;
				}
			}else{
				shineEscolhido = shineNormal;
			}

			shineInstance = Instantiate (shineEscolhido);
			shineInstance.transform.position = new Vector3(Random.Range(Camera.main.transform.position.x - 10f,Camera.main.transform.position.x + 10f),Random.Range(-5f,-1.667f),9f);
			Destroy(shineInstance, 0.3f);
			shineInstance = Instantiate (shineEscolhido);
			shineInstance.transform.position = new Vector3(Random.Range(Camera.main.transform.position.x - 10f,Camera.main.transform.position.x + 10f),Random.Range(-1.668f,1.667f),9f);
			Destroy(shineInstance, 0.3f);
			shineInstance = Instantiate (shineEscolhido);
			shineInstance.transform.position = new Vector3(Random.Range(Camera.main.transform.position.x - 10f,Camera.main.transform.position.x + 10f),Random.Range(1.667f,5f),9f);
			Destroy(shineInstance, 0.3f);

			freqShine = freqShineDef;

		}
	
	}
}
