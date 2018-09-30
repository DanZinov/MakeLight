using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	PieceConnector pieceConnector;
	Transform firstParent;
	Vector3 firstPosition;
	TouchCamera cameraScript;
	public static GameObject itemBeingDragged;
	public static Vector3 startPosition;
	Camera cam;
	public static Transform startParent;
	Vector3 touchPos;
	private float startValue;
	private int defaultHeight = 250;
	private int defaultWidth = 450;
	AudioControl audioControl;

	void Start(){
		firstParent = transform.parent;
		firstPosition = transform.GetComponent<RectTransform> ().anchoredPosition;
		pieceConnector = GameObject.FindGameObjectWithTag ("UsableSlots").GetComponent<PieceConnector> ();
		cameraScript = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<TouchCamera> ();
		cam = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera> ();
		audioControl = GameObject.FindGameObjectWithTag ("AudioControl").GetComponent<AudioControl> ();
	}
	#region IBeginDragHandler implementation
	public void OnBeginDrag (PointerEventData eventData)
	{
		cameraScript.isBuilding = true;
		itemBeingDragged = gameObject;
		startPosition = transform.position;
		startParent = transform.parent;
		if (transform.parent.tag != "Inventory") {
			transform.parent = GameObject.FindGameObjectWithTag ("Last Slot").transform;
		}	
		GetComponent<CanvasGroup> ().blocksRaycasts = false;
	}
	#endregion

	#region IDragHandler implementation

	public void OnDrag (PointerEventData eventData)
	{
		if (Input.touchCount > 0) {
			Touch touch = Input.GetTouch (0);
			touchPos = new Vector3 (cam.ScreenToWorldPoint (touch.position).x, cam.ScreenToWorldPoint (touch.position).y, 0);
			transform.position = touchPos;
		}
	}

	#endregion

	#region IEndDragHandler implementation

	public void OnEndDrag (PointerEventData eventData)
	{
		itemBeingDragged = null;
		GetComponent<CanvasGroup> ().blocksRaycasts = true;
		cameraScript.isBuilding = false;
		if (transform.parent == startParent) {
			transform.position = startPosition;
			audioControl.OnDropSound ();
		} else if (transform.parent.tag == "Garbage") {
			Debug.Log (firstPosition.ToString());
			transform.SetParent(firstParent);
			transform.GetComponent<RectTransform>().anchoredPosition = firstPosition;
			transform.GetComponent<RectTransform>().sizeDelta = new Vector2(defaultWidth, defaultHeight);
			pieceConnector.CheckLight ();
			pieceConnector.CountItems ();
			audioControl.OnDropSound ();
		}
	}

	#endregion

}
