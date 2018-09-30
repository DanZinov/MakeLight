using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class SlotHandler : MonoBehaviour, IDropHandler {
	public PieceConnector pieceConnector;
	public AudioControl audioControl;

	public GameObject item{
		get{ 
			if (transform.childCount > 1) {
				return transform.GetChild (0).gameObject;
			}
			return null;
		}
	}

	#region IDropHandler implementation

	public void OnDrop (PointerEventData eventData)
	{
		if (!item) {
			if (transform.childCount == 0) {
				DragHandler.itemBeingDragged.transform.SetParent (transform);
				DragHandler.itemBeingDragged.transform.GetComponent<RectTransform>().localScale = new Vector3(0.5f, 0.5f, 0.5f);
				pieceConnector.CheckLight ();
				pieceConnector.CountItems ();
				audioControl.OnDropSound ();
			}
		}
	}

	#endregion
}
