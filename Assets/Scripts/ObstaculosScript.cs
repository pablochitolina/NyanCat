using UnityEngine;
using System.Collections;

public class ObstaculosScript : MonoBehaviour {
	public GameObject asteroid1;
	public GameObject asteroid2;
	public GameObject asteroid3;
	public GameObject asteroid4;
	/*public GameObject obstaculo1;
	public GameObject obstaculo2;
	public GameObject obstaculo3;
	public GameObject obstaculo4;
	public GameObject obstaculo5;
	public GameObject obstaculo6;
	public GameObject obstaculo7;
	public GameObject obstaculo8;*/
	private GameObject obstaculoEscolhido;
	private GameObject obstaculoInstance;
	private float freqObs = 2f;
	private float freqObsDef = 2f;
	public NyanScript nyan;
	private int filaObstaculo = 0;
	private float tamObstaculo;
	private int chanceAcid = 10;

	// Use this for initialization
	void Start () {

		Renderer rendObs = asteroid1.GetComponent<Renderer> ();
		tamObstaculo = rendObs.bounds.size.y/2;
	}
	
	// Update is called once per frame
	void Update () {

		freqObs -= Time.deltaTime;
		if (freqObs <= 0 && nyan.iniciou && !nyan.morto) {

			/*int obstac = Random.Range(0,7);
			switch(obstac){
			case 0:
				obstaculoEscolhido = obstaculo1;
				break;
			case 1:
				obstaculoEscolhido = obstaculo2;
				break;
			case 2:
				obstaculoEscolhido = obstaculo3;
				break;
			case 3:
				obstaculoEscolhido = obstaculo4;
				break;
			case 4:
				obstaculoEscolhido = obstaculo5;
				break;
			case 5:
				obstaculoEscolhido = obstaculo6;
				break;
			case 6:
				obstaculoEscolhido = obstaculo7;
				break;
			default:
				obstaculoEscolhido = obstaculo8;
				break;
			}*/

			switch(filaObstaculo){
			case 0:
				obstaculoEscolhido = asteroid1;
				filaObstaculo = 1;
				break;
			case 1:
				obstaculoEscolhido = asteroid2;
				filaObstaculo = 2;
				break;
			case 2:
				obstaculoEscolhido = asteroid3;
				filaObstaculo = 3;
				break;
			default:
				obstaculoEscolhido = asteroid4;
				filaObstaculo = 0;
				break;

			}

			//obstaculoEscolhido = asteroid1;

			//obstaculoInstance = Instantiate (obstaculoEscolhido);

			//obstaculoEscolhido.SetActive(true);
			obstaculoEscolhido.transform.position = new Vector3(Camera.main.transform.position.x + 13f,Random.Range(-4f,4f),8f);


			if((int)Random.Range(0,chanceAcid) == (int)Random.Range(0,chanceAcid) && (nyan.objAcid.transform.position.x - Camera.main.transform.position.x) < -10f){
			
				nyan.objAcid.SetActive(true);
				float posX = Random.Range((obstaculoEscolhido.transform.position.x - tamObstaculo - 2f), (obstaculoEscolhido.transform.position.x + tamObstaculo + 2f));
				if(obstaculoEscolhido.transform.position.y > 0){
					nyan.objAcid.transform.position = new Vector3(posX,Random.Range(-4f,(obstaculoEscolhido.transform.position.y - tamObstaculo -1f)),0);
				}else{
					nyan.objAcid.transform.position = new Vector3(posX,Random.Range(4f,(obstaculoEscolhido.transform.position.y + tamObstaculo + 1f)),0);
				}

			}


			freqObs = freqObsDef;
			
		}
	
	}

}
