using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class EventButtonDetails {
	public string buttonTitle;
	public Sprite buttonBkgd;
	public UnityAction action;

	public EventButtonDetails(string ti, Sprite bkgd, UnityAction ac) {
		buttonTitle = ti;
		buttonBkgd = bkgd;
		action = ac;
	}
}

public class ModalPanelDetails {
	public string title;
	public string context;
	public Sprite iconImage;
	public Sprite panelBkgdImage;
	public EventButtonDetails button1;
	public EventButtonDetails button2;
	public EventButtonDetails button3;

	public ModalPanelDetails() {
		title = "<TITLE>";
		context = "<CONTEXT>";
		iconImage = null;
		panelBkgdImage = null;
		button1 = null;
		button2 = null;
		button3 = null;
	}

	public ModalPanelDetails(string ti, string co, Sprite ic, Sprite bkgd = null) {
		title = ti;
		context = co;
		iconImage = ic;
		panelBkgdImage = bkgd;
		button1 = null;
		button2 = null;
		button3 = null;
	}

}

public class ModalPanel : MonoBehaviour {

	public Text questions;
	public Image icon;
	public Button [] buttons;
	public GameObject modalPanel;

//	private Rect SPRITE_RECT = new Rect(0.0f, 0.0f, 32.0f, 32.0f);
//	private Vector2 SPRITE_PIVOT = new Vector2(0.5f, 0.5f);
//	private Sprite sIcon;


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

	void Awake() {

		//sIcon = Sprite.Create(texIcon, SPRITE_RECT, SPRITE_PIVOT);
	}

	// Implement a Yes/No/Cancel panel
	public void YesNoCancelModal(string question, Sprite icon, UnityAction yesEvent, UnityAction noEvent, UnityAction cancelEvent) {

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
			this.icon.sprite = icon;
			this.icon.gameObject.SetActive(true);
			this.buttons[0].gameObject.SetActive(true);
			this.buttons[1].gameObject.SetActive(true);
			this.buttons[2].gameObject.SetActive(true);
		}
	}

	public void EnableModal(string question, UnityAction [] events, string [] buttonText, Sprite icon = null) {

		modalPanel.SetActive(true);

		// Check if there is enough buttons for the requested events
		int numButtons = buttons.Length;

		for (int i = 0; i < events.Length; ++i) {

			if (i < numButtons) {
				buttons[i].onClick.RemoveAllListeners();
				buttons[i].onClick.AddListener(events[i]);
				buttons[i].onClick.AddListener(ClosePanel);
				buttons[i].gameObject.SetActive(true);

				Text gtext = buttons[i].GetComponentInChildren<Text>();
				gtext.text = buttonText[i];
			}

		}

		// Deactivate any unused buttons
		for (int i = events.Length; i < buttons.Length; ++i)
		{
			buttons[i].gameObject.SetActive(false);
		}

		this.questions.text = question;
		
		if (icon != null) 
		{
			this.icon.sprite = icon;
			this.icon.gameObject.SetActive(true);
		}
		else 
		{
			this.icon.gameObject.SetActive(false);
		}

	}

	public void EnableModal(ModalPanelDetails details) {

		modalPanel.SetActive(true);

		Debug.Assert(buttons.Length >= 3, "Modal panel requires 3+ buttons.");
		if (buttons.Length >= 3) {
			SetButtonDetails(0, ref details.button1);
			SetButtonDetails(1, ref details.button2);
			SetButtonDetails(2, ref details.button3);
		}

		this.questions.text = details.context;
		
		if (details.iconImage != null) 
		{
			this.icon.sprite = details.iconImage;
			this.icon.gameObject.SetActive(true);
		}
		else
		{
			this.icon.gameObject.SetActive(false);
		}
	}

	void SetButtonDetails(int index, ref EventButtonDetails button) {

		if (button != null) {
			buttons[index].onClick.RemoveAllListeners();
			buttons[index].onClick.AddListener(button.action);
			buttons[index].onClick.AddListener(ClosePanel);
			buttons[index].gameObject.SetActive(true);
			Text gtext = buttons[index].GetComponentInChildren<Text>();
			gtext.text = button.buttonTitle;
		}
		else {
			buttons[index].gameObject.SetActive(false);
		}
	}

	void ClosePanel() {
		modalPanel.SetActive(false);
	}

}
