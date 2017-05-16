using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drop : MonoBehaviour, IDropHandler {
	
	public bool fastCell;
	public Inventory inv;
	public void OnDrop (PointerEventData eventData)
	{
		Drag drag = eventData.pointerDrag.GetComponent<Drag> ();
		if (drag != null) {
			if (transform.childCount > 0) {
				transform.GetChild (0).SetParent (drag.old);
			}
			drag.transform.SetParent (transform);
			if (fastCell) {
				drag.fastCell = true;
				Debug.Log (drag.item);
				inv.removeFromList (drag);
			} else if (!fastCell && drag.fastCell) {
				drag.fastCell = false;
				inv.addToList (drag);
			}
		}
	}
		
}
