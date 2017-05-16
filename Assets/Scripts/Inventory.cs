using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

	List<Item> list;
	List<Item> fastList;
	public GameObject inventory; 
	public GameObject container;
	public Text hintText;

	// Use this for initialization
	void Start () {
		list = new List<Item> ();
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update () {
		Ray ray1 = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit1;
		if (Physics.Raycast (ray1, out hit1, 2f)) {
			Item item = hit1.collider.GetComponent<Item> ();
			if (item != null) {
				hintText.text = "Press (F) to pick up " + item.itemName + ".";
			} 
		}
		if (Physics.Raycast (ray1, out hit1)) {
			Item item = hit1.collider.GetComponent<Item> ();
			if (item == null) {
				hintText.text = "";
			} 
		}

		if (Input.GetKeyDown (KeyCode.F)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit, 2f)) {
				Item item = hit.collider.GetComponent<Item> ();
				if (item != null) {
					list.Add (item);
					Destroy (hit.collider.gameObject);
				}
			}
		}
		if (Input.GetKeyUp (KeyCode.I) || Input.GetKeyUp (KeyCode.Tab)) {
			if (inventory.activeSelf) {
				inventory.SetActive (false);
				for (int i = 0; i < inventory.transform.childCount; i++) {
					if (inventory.transform.GetChild (i).transform.childCount > 0) {
						Destroy (inventory.transform.GetChild (i).transform.GetChild (0).gameObject);
					}
				}
			} else {
				inventory.SetActive (true);
				recalculateItems ();
			}
		}
		if (inventory.activeSelf && Input.GetKeyUp (KeyCode.Escape)) {
			inventory.SetActive (false);
		}
	}

	void recalculateItems(){
		int count = list.Count;
		for (int i = 0; i < count; i++) {
			Item it = list [i];
			if (inventory.transform.childCount >= i) {
				GameObject img = Instantiate (container);
				img.transform.SetParent (inventory.transform.GetChild (i).transform);
				img.GetComponent<Image> ().sprite = Resources.Load<Sprite>(it.sprite);
				img.GetComponent<Drag>().item = it;
			} else break;
		}
	}

	void remove(Drag drag){
		Debug.Log (drag.item);
		Item it = drag.item;
		GameObject newo = Instantiate<GameObject> (Resources.Load<GameObject> (it.prefab));
		newo.transform.position = transform.position + transform.forward;
		Destroy (drag.gameObject);
		list.Remove (it);
	}

	public void removeFromList(Drag drag){
		Item it = drag.item;
		list.Remove (it);
	}

	public void addToList(Drag drag){
		Item it = drag.item;
		list.Add (it);
	}
}
