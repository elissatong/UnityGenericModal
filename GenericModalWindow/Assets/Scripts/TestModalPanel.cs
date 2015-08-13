using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;


public class TestModalPanel : MonoBehaviour {

	public GameObject background;
	public Texture [] textures;
	public Texture2D icon;
	public Text coins;

	private ModalPanel modalPanel;
	private UnityAction myYesAction;
	private UnityAction myNoAction;
	private UnityAction myCancelAction;
	private UnityAction myOkAction;

	private Rect SPRITE_RECT = new Rect(0.0f, 0.0f, 32.0f, 320.0f);
	private Vector2 SPRITE_PIVOT = new Vector2(0.5f, 0.5f);
	private Sprite sIcon;

	private int currentTexture = 0;
	private int numCoins = 0;

	void Awake() {
		modalPanel = ModalPanel.Instance();
		myYesAction = new UnityAction(TestYesFunction);
		myNoAction = new UnityAction(TestNoFunction);
		myCancelAction = new UnityAction(TestCancelFunction);
		myOkAction = new UnityAction(TestOkFunction);
	}

	public void TestYesNoCancel() {
		modalPanel.YesNoCancelModal("Do you want to change background?", myYesAction, myNoAction, myCancelAction);
	}

	public void Test3Buttons() {
		UnityAction [] actions = {myYesAction, myNoAction, myCancelAction};

		Sprite sIcon = Sprite.Create(icon, SPRITE_RECT, SPRITE_PIVOT);
		modalPanel.EnableModal("Do you want to change background?", actions, sIcon);
	}

	public void Test2Buttons() {
		UnityAction [] actions = {myYesAction, myNoAction};
		modalPanel.EnableModal("Do you want to change background?", actions);
	}

	public void Test1Buttons() {
		UnityAction [] actions = {myOkAction};
		Sprite sIcon = Sprite.Create(icon, SPRITE_RECT, SPRITE_PIVOT);
		modalPanel.EnableModal("You have been rewarded 100 coins.", actions, sIcon);
	}

	void TestYesFunction() {
		//Debug.Log("TestYesFunction");
		MeshRenderer meshRenderer = background.GetComponent<MeshRenderer>();
		if (meshRenderer != null) {

			currentTexture = (++currentTexture) % textures.Length;

			meshRenderer.materials[0].mainTexture = textures[currentTexture];
		}
	}

	void TestNoFunction() {
		// add logic
	}

	void TestCancelFunction() {
		// add logic
	}

	void TestOkFunction() {

		//Debug.Assert (coins != null, "Text Coins can't be null. Assign a GameObject.");

		//if (coins != null) 
		{
			numCoins += 100;
			coins.text = "100 coins";
		}
	}
}
