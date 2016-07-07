using UnityEngine;
using System.Collections;
using TestingMe;
using UnityEngine.UI;

public class GrabData : MonoBehaviour {


	// initializing some variables
	string url = "http://www.felix.gullamolar.com/latest";
	private DataStruct[] curArray;
	WWW www;
	Vector3[] vertices;
	Color[] colors;
	public Text textA;
	public Text textB;
	public Text textC;
	public Text textD;
	public Text textE;
	public Text textF;
	public GameObject VesselA;
	public GameObject VesselB;
	public GameObject VesselC;
	public GameObject VesselD;
	public GameObject VesselE;
	public GameObject VesselF;
	Mesh meshA;
	Mesh meshB;
	Mesh meshC;
	Mesh meshD;
	Mesh meshE;
	Mesh meshF;

	public void setUpdateRange(bool five) {
		if (five) {
			url = "http://www.felix.gullamolar.com/latest";
		} else {
			url = "http://www.felix.gullamolar.com/latestRow";
		}
	}


	void Start() {
		// initial assign of the Gameobject meshes
		meshA = VesselA.GetComponent<MeshFilter> ().mesh;
		meshB = VesselB.GetComponent<MeshFilter> ().mesh;
		meshC = VesselC.GetComponent<MeshFilter> ().mesh;
		meshD = VesselD.GetComponent<MeshFilter> ().mesh;
		meshE = VesselE.GetComponent<MeshFilter> ().mesh;
		meshF = VesselF.GetComponent<MeshFilter> ().mesh;

		// Start a Coroutine which fetches the 5 last database rows and visualizes them
		StartCoroutine ("getLatestFive");
	}

	// Fetches the 5 last database rows and visualizes them in 1s steps (infinite loop)
	IEnumerator getLatestFive() {
		while (true) {
			www = new WWW(url);
			while (!www.isDone) {
			}
			curArray = JsonHelper.getJsonArray<DataStruct> (www.text);
			Debug.Log (curArray.Length);
			for (int i = curArray.Length-1; i >= 0; i--) {
				// update Vessel vertices
				updateVesselColor (curArray [i].Unit1.Temp, curArray [i].Unit1.Level, meshA, 14.5f, 1.0f);
				updateVesselColor (curArray [i].Unit2.Temp, curArray [i].Unit2.Level, meshB, 7.5f, 1.0f);
				updateVesselColor (curArray [i].Unit3.Temp, curArray [i].Unit3.Level, meshC, 12.0f, 1.2f);
				updateVesselColor (curArray [i].Unit4.Temp, curArray [i].Unit4.Level, meshD, 14.5f, 1.0f);
				updateVesselColor (curArray [i].Unit5.Temp, curArray [i].Unit5.Level, meshE, 7.5f, 1.0f);
				updateVesselColor (curArray [i].Unit6.Temp, curArray [i].Unit6.Level, meshF, 12.0f, 1.2f);

				// update GUI text
				textA.text = curArray [i].Unit1.buttonString ();
				textB.text = curArray [i].Unit2.buttonString ();
				textC.text = curArray [i].Unit3.buttonString ();
				textD.text = curArray [i].Unit4.buttonString ();
				textE.text = curArray [i].Unit5.buttonString ();
				textF.text = curArray [i].Unit6.buttonString ();

				yield return new WaitForSeconds(1);
			}
		}
	}

	// changes the color of all vertices of the model curMesh with respect to temperature and fill level
	// the limit is for manually setting the highest Y coordinate of the mesh model
	void updateVesselColor(int temp, int level, Mesh curMesh, float limit, float low) {
		vertices = curMesh.vertices;
		colors = new Color[vertices.Length];

		// Color Mapping from temperature (0-100) to RGB values
		float ratio = temp / 50.0f;
		float b = Mathf.Max(0, 255 * (1 - ratio));
		float r = Mathf.Max(0, 255 * (ratio - 1));
		float g = (255 - b - r);
		Color fillColor = new Color(r / 255.0f,g / 255.0f,b / 255.0f,1);

		// assigns the color to all vertices that are below the measured fill level
		int i = 0;
		while (i < vertices.Length) {
			if ((vertices [i].y >= low) && (vertices [i].y <= ((limit-low) * level * 0.01f + low))) { //
				colors[i] = fillColor;
			} else {
				colors[i] = Color.gray;
			}
			i++;
		}
		curMesh.colors = colors;
	}

	
	// Update is called once per frame
	void Update () {
		
	}
}
