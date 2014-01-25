//////////////////////////////////////////////////////////
//
//  myVegas Mobile
//
//  Started on Feburary 21, 2013
//  (c) 2013 PlayStudios, Inc.
//
//////////////////////////////////////////////////////////

using UnityEngine;

public class Manager : MonoBehaviour
{
	protected static Manager getInstance(ref Manager instance, string managerType) 
	{
		return Manager.getInstance(GameObject.Find("/Managers"), ref instance, managerType);
	}

	protected static Manager getInstance(GameObject managerContainer, ref Manager instance, string managerType) 
	{
	    if (instance == null)
	    {
        	if (managerContainer) 
        	{
        		// this.Log("managerContainer["+managerContainer.name+"] mangerType["+managerType+"]");
				instance = managerContainer.GetComponent(managerType) as Manager;
        		if (instance == null)
        		{
        			Transform managerTransform = managerContainer.transform.Find(managerType);
        			// this.Log("managerTransform["+managerContainer.name+"] mangerType["+managerType+"]");
        			if (managerTransform) 
        			{
        				instance = managerTransform.GetComponent(managerType) as Manager;
	        			// this.Log("FOUND manager! managerTransform["+managerContainer.name+"] mangerType["+managerType+"]");
        			}
				}
			}

			if (instance == null) 
			{
				// this.Log("Creating new manager instance of type["+managerType+"]");
				GameObject go = new GameObject(managerType);
				instance = go.AddComponent(managerType) as Manager;
				if (managerContainer) 
				{
					// this.Log("Appending manager of type["+managerType+"] to Manager container");
					go.transform.parent = managerContainer.transform;
				}
			}

			if (instance)
			{
				instance.Initialize();
			}
	    }

	    return instance;
	}
	
	protected virtual void Initialize()
	{
	}
}