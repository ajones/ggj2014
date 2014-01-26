using UnityEngine;
using System.Collections;

public class ConversationManager : Manager {

	float dialogDelay =3f;

	/////////////////////  BOOTSTRAP  /////////////////////
	
	private static Manager instance;
	
	public static ConversationManager getInstance() 
	{
		return getInstance(ref instance, "ConversationManager") as ConversationManager;
	}



	public void PlayConversation1() {
		StartCoroutine(Convo1());
	}

	public void PlayConversation2() {
		StartCoroutine(Convo2());
	}

	public void PlayConversation3() {
		StartCoroutine(Convo3());
	}

	public void PlayConversation4() {
		StartCoroutine(Convo4());
	}

	public void PlayConversation5() {
		StartCoroutine(Convo5());
	}

	IEnumerator Convo1() {
		DialogManager.getInstance().ShowDialog("HumanDialog","Fine. Pick up your things tomorrow.");
		yield return new WaitForSeconds(dialogDelay);

		DialogManager.getInstance().DismissDialog();
	}

	IEnumerator Convo2() {
		DialogManager.getInstance().ShowDialog("HumanDialog","... right. Probably going to need this later.");
		yield return new WaitForSeconds(dialogDelay);
		DialogManager.getInstance().ShowDialog("DuckDialog","...");
		yield return new WaitForSeconds(dialogDelay);
		DialogManager.getInstance().DismissDialog();
	}

	IEnumerator Convo3() {
		DialogManager.getInstance().ShowDialog("HumanDialog","Beautiful... just like her.");
		yield return new WaitForSeconds(dialogDelay);
		DialogManager.getInstance().ShowDialog("DuckDialog","...");
		yield return new WaitForSeconds(dialogDelay);
		DialogManager.getInstance().DismissDialog();
	}

	IEnumerator Convo4() {
		DialogManager.getInstance().ShowDialog("HumanDialog","Happier times.");
		yield return new WaitForSeconds(dialogDelay);
		DialogManager.getInstance().ShowDialog("DuckDialog","...");
		yield return new WaitForSeconds(dialogDelay);
		DialogManager.getInstance().DismissDialog();
	}

	IEnumerator Convo5() {
		DialogManager.getInstance().ShowDialog("HumanDialog","Huh?");
		yield return new WaitForSeconds(dialogDelay);
		DialogManager.getInstance().ShowDialog("DuckDialog","...Quack!");
		SoundManager.getInstance().playSoundEffect("quack3");
		yield return new WaitForSeconds(dialogDelay);
		DialogManager.getInstance().DismissDialog();
	}
}
