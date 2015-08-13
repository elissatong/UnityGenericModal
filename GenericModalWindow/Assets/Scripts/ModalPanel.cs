using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class ModalPanel : MonoBehaviour {

	public Text questions;
	public Image icon;
	public Button [] buttons;
	public GameObject modalPanel;

	private static ModalPanel sModalPanel;

	public static ModalPanel Instance() {
		if (!sModalPanel) {
			sModalPanel = FindObjectOfType(typeof (ModalPanel)) as ModalPanel;

			if (!sModalPanel) {
				Debug.LogError("There needs to be at least one active ModalPanel script on a GameObject in your scene.");
			}
		}

		return sModalPanel;
	}

	// Implement a Yes/No/Cancel panel
	public void YesNoCancelModal(string question, UnityAction yesEvent, UnityAction noEvent, UnityAction cancelEvent) {

		modalPanel.SetActive(true);

		if (buttons.Length == 3)
		{
			buttons[0].onClick.RemoveAllListeners();
			buttons[0].onClick.AddListener(yesEvent);
			buttons[0].onClick.AddListener(ClosePanel);

			buttons[1].onClick.RemoveAllListeners();
			buttons[1].onClick.AddListener(noEvent);
			buttons[1].onClick.AddListener(ClosePanel);

			buttons[2].onClick.RemoveAllListeners();
			buttons[2].onClick.AddListener(cancelEvent);
			buttons[2].onClick.AddListener(ClosePanel);

			this.questions.text = question;
			this.icon.gameObject.SetActive(false);
			this.buttons[0].gameObject.SetActive(true);
			this.buttons[1].gameObject.SetActive(true);
			this.buttons[2].gameObject.SetActive(true);
		}
	}

	public void EnableModal(string question, UnityAction [] events, Sprite icon = null) {

		modalPanel.SetActive(true);

		this.questions.text = question;

		if (icon != null) 
		{
			this.icon.gameObject.SetActive(true);
			this.icon.sprite = icon;
		}

		// Check if there is enough buttons for the requested events
		int numButtons = buttons.Length;

		for (int i = 0; i < events.Length; ++i) {

			if (i < numButtons) {
				buttons[i].onClick.RemoveAllListeners();
				buttons[i].onClick.AddListener(events[i]);
				buttons[i].onClick.AddListener(ClosePanel);
				buttons[i].gameObject.SetActive(true);

				Text gtext = buttons[i].GetComponentInChildren<Text>();
				gtext.text = "Hello" + i;
			}

		}

		// Deactivate any unused buttons
		for (int i = events.Length; i < buttons.Length; ++i)
		{
			buttons[i].gameObject.SetActive(false);
		}

	}

	void ClosePanel() {
		modalPanel.SetActive(false);
	}

}
