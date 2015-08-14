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

//	private Rect SPRITE_RECT = new Rect(0.0f, 0.0f, 32.0f, 32.0f);
//	private Vector2 SPRITE_PIVOT = new Vector2(0.5f, 0.5f);
//	private Sprite sIcon;

	private int currentTexture = 0;
	private int numCoins = 0;

	private string ok = "Ok";
	private string yes = "Yes";
	private string no = "No";
	private string cancel = "Cancel";

	void Awake() {
		modalPanel = ModalPanel.Instance();
	}

	public void TestYesNoCancel() {
		modalPanel.YesNoCancelModal("Do you want to change background?", TestYesFunction, TestNoFunction, TestCancelFunction);
	}

	public void Test3Buttons() {
		UnityAction [] actions = {TestYesFunction, TestNoFunction, TestCancelFunction};
		string [] buttonText = {yes, no, cancel};
		//Sprite sIcon = Sprite.Create(icon, SPRITE_RECT, SPRITE_PIVOT);
		modalPanel.EnableModal("Do you want to change background?", actions, buttonText);
	}

	public void Test2Buttons() {
		UnityAction [] actions = {TestYesFunction, TestNoFunction};
		string [] buttonText = {yes, no};
		modalPanel.EnableModal("Do you want to change background?", actions, buttonText);
	}

	public void Test1Buttons() {
		UnityAction [] actions = {TestOkFunction};
		string [] buttonText = {ok};
		//Sprite sIcon = Sprite.Create(icon, SPRITE_RECT, SPRITE_PIVOT);
		modalPanel.EnableModal("You have been rewarded 100 coins.", actions, buttonText);
	}

	void TestOkFunction() {
		
		Debug.Assert (coins != null, "Text Coins can't be null. Assign a GameObject.");
		if (coins != null) 
		{
			numCoins += 100;
			coins.text = "100 coins";
		}
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

}
