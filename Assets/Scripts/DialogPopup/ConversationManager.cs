using UnityEngine;
using System.Collections;

public class ConversationManager : Manager {

	/////////////////////  BOOTSTRAP  /////////////////////
	
	private static Manager instance;
	
	public static ConversationManager getInstance() 
	{
		return getInstance(ref instance, "ConversationManager") as ConversationManager;
	}



	public void PlayConversation1() {
		StartCoroutine(Convo1());
	}

	IEnumerator Convo1() {
		DialogManager.getInstance().ShowDialog("DuckDialog","Quack...");
		yield return new WaitForSeconds(1f);
		DialogManager.getInstance().ShowDialog("HumanDialog","WTF...?");
		yield return new WaitForSeconds(1f);
		DialogManager.getInstance().DismissDialog();
	}
}
