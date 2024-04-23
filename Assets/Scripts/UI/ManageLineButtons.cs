using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using DG.Tweening;

public class ManageLineButtons : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler, IPointerUpHandler,IPointerDownHandler
{

	[SerializeField]
	private PayoutCalculation payManager;
	[SerializeField]
	private GameObject _ConnectedLine;
	internal bool isEnabled = true;

#if UNITY_EDITOR
	public void OnPointerEnter(PointerEventData eventData)
	{
		if (isEnabled)
		{
			payManager.ResetLines();
			if (_ConnectedLine) _ConnectedLine.SetActive(true);
		}
	}
	public void OnPointerExit(PointerEventData eventData)
	{
		if (isEnabled)
		{
			if (_ConnectedLine) _ConnectedLine.SetActive(false);
		}
	}
	public void OnPointerDown(PointerEventData eventData)
	{
	}
	public void OnPointerUp(PointerEventData eventData)
	{
	}

#else
	public void OnPointerEnter(PointerEventData eventData)
	{
		if (Application.platform == RuntimePlatform.WebGLPlayer && !Application.isMobilePlatform)
        {
            if (isEnabled)
			{
				payManager.ResetLines();
				if (_ConnectedLine) _ConnectedLine.SetActive(true);
			}
        }
    }
	public void OnPointerExit(PointerEventData eventData)
	{
        if (Application.platform == RuntimePlatform.WebGLPlayer && !Application.isMobilePlatform)
        {
            if (isEnabled)
			{
			if (_ConnectedLine) _ConnectedLine.SetActive(false);
			}
		}
	}
	public void OnPointerDown(PointerEventData eventData)
	{
		if (Application.platform == RuntimePlatform.WebGLPlayer && Application.isMobilePlatform && isEnabled)
		{
			payManager.ResetLines();
			this.gameObject.GetComponent<Button>().Select();
			if (_ConnectedLine) _ConnectedLine.SetActive(true);
		}
	}
	public void OnPointerUp(PointerEventData eventData)
	{
		if (Application.platform == RuntimePlatform.WebGLPlayer && Application.isMobilePlatform && isEnabled)
		{
			if (_ConnectedLine) _ConnectedLine.SetActive(false);
			DOVirtual.DelayedCall(0.1f, () =>
			{
				this.gameObject.GetComponent<Button>().spriteState = default;
				EventSystem.current.SetSelectedGameObject(null);
			 });
		}
	}
#endif
}
