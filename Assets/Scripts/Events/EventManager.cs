using UnityEngine;
using System.Collections;

public interface IEventListener 
{
    bool HandleEvent(IEvent evt);
}

public interface IEvent 
{
    string GetName();
    object GetData();
}

public class EventManager : Manager 
{
	/////////////////////  BOOTSTRAP  /////////////////////
	
	private static Manager instance;

	public static EventManager getInstance() 
	{
		return getInstance(ref instance, "EventManager") as EventManager;
	}
	
	////////////////////////////////////////////////////////

    private static bool appQuitting			= false;
   
    private Hashtable listenerTable			= new Hashtable();
    private Queue eventQueue				= new Queue();
    
    public bool limitQueueProcesing			= false;
    public float queueProcessTime			= 0.0f;

    ///////////////////////////////////////////////////////////

    /**
     * Add a listener to the event manager that will receive any events of the
     * supplied event name.
     */
    public static bool AddListener(IEventListener listener, string eventName) {

        if (!appQuitting) {
	        if (listener == null || eventName == null) {
	            Debug.Log("[EVENTMANAGER]: Failed to add listener: listener["+(listener == null ? "listener argument cannot be null" : listener.GetType().ToString())+"] eventName["+(eventName == null ? "eventName argument cannot be null" : eventName)+"]");
	            return false;
	        }

	        EventManager em = EventManager.getInstance();
	        if (!em.listenerTable.ContainsKey(eventName)) {
	        	//Debug.Log("adding listener for event ["+eventName+"]. listener ["+listener+"]");
	            em.listenerTable.Add(eventName, new ArrayList());
	        }
	
	        ArrayList listenerList = em.listenerTable[eventName] as ArrayList;
	        if (listenerList.Contains(listener)) {
	            Debug.Log("[EVENTMANAGER]: listener["+(listener.GetType().ToString())+"] is already in list for eventName["+eventName+"]");
	            return false;
	        }
	
	        // Debug.Log("[EVENTMANAGER]: Added listener["+(listener.GetType().ToString())+"]");
	        listenerList.Add(listener);
	    }
/*
	    else {
	        Debug.Log("[EVENTMANAGER]: Addition of listener["+(listener.GetType().ToString())+"] skipped since app is quitting");
	    }
*/
	    return false;
    }

    /**
     * Remove a listener from the specified event.
     */
    public static bool DetachListener(IEventListener listener, string eventName) {
    	if (!appQuitting) {

	        EventManager em = EventManager.getInstance();

	        if (!em.listenerTable.ContainsKey(eventName)) {
	            return false;
	        }
	
	        ArrayList listenerList = em.listenerTable[eventName] as ArrayList;
	        if (!listenerList.Contains(listener)) {
	            return false;
	        }
	
	        //Debug.Log("[EVENTMANAGER]: Removed listener["+(listener.GetType().ToString())+"]");
	        listenerList.Remove(listener);
	    }
/*
	    else {
	        Debug.Log("[EVENTMANAGER]: Removal of listener["+(listener.GetType().ToString())+"] skipped since app is quitting");
	    }
*/
        return true;
    }

    /**
     * Trigger an event instantly.
     */
    public static bool TriggerEvent(IEvent evt) {
     	if (!appQuitting) {

	        EventManager em = EventManager.getInstance();

	     	string eventName = evt.GetName();
	        if (!em.listenerTable.ContainsKey(eventName)) {
	            // Debug.LogWarning("[EVENTMANAGER]: Triggered event["+eventName+"] has no listeners!");
	            return false;
	        }

	        ArrayList listenerList = em.listenerTable[eventName] as ArrayList;
	        for (int i = 0; i < listenerList.Count; i ++){
	        	IEventListener listener = listenerList[i] as IEventListener;
	        	 if (listener.HandleEvent(evt)) {
	                break;
	            }
	        }
	        /*
	        foreach (IEventListener listener in listenerList) {
	            if (listener.HandleEvent(evt)) {
	                return true;
	            }
	        }
	        */
	    }
/*
	    else {
	        Debug.Log("[EVENTMANAGER]: Trigger of eventName["+evt.GetName()+"] skipped since app is quitting");
	    }
*/
        return true;
    }

    /**
     * Trigger the event after the specified time.
     */
    public static void TriggerEventAfter(IEvent evt, float delay) {
     	if (!appQuitting) {
	        EventManager em = EventManager.getInstance();
	        em.TriggerEventAfterDelay(evt, delay);
	        
	    }
/*
	    else {
	        Debug.Log("[EVENTMANAGER]: Trigger of eventName["+evt.GetName()+"] skipped since app is quitting");
	    }
*/
    }

    public void TriggerEventAfterDelay(IEvent evt, float delay) {
	    StartCoroutine(TriggerEventAfterDelayInternal(evt, delay));
	}
    
    public IEnumerator TriggerEventAfterDelayInternal(IEvent evt, float delay) {
		yield return new WaitForSeconds(delay * Time.timeScale);
		TriggerEvent(evt);
    }

    /**
     * Insert an event into the current queue.
     */
    public static bool QueueEvent(IEvent evt) {
     	if (!appQuitting) {
	        EventManager em = EventManager.getInstance();
	        if (!em.listenerTable.ContainsKey(evt.GetName())) {
	            // Debug.Log("[EVENTMANAGER]: No listeners exist for event["+evt.GetName()+"]");
	            return false;
	        }
	
	        em.eventQueue.Enqueue(evt);
	    }
/*
	    else {
	        Debug.Log("[EVENTMANAGER]: Queuing of eventName["+evt.GetName()+"] skipped since app is quitting");
	    }
*/
        return true;
    }

    /**
     * Process the queue on every Update cycle.
     *
     * If the queue processing is limited, a maximum processing time per update
     * can be set after which the events will be deferred until the next Update
     * loop.
     */
    void Update() {
    	if (!appQuitting) {
	        float timer = 0.0f;
	        while (eventQueue.Count > 0) {
	            if (limitQueueProcesing) {
	                if (timer > queueProcessTime) {
	                    return;
	                }
	            }
	
	            IEvent evt = eventQueue.Dequeue() as IEvent;
	            if (!TriggerEvent(evt)) {
	                Debug.Log("[EVENTMANAGER]: Error processing event["+evt.GetName()+"]");
	            }
	            
	            if (limitQueueProcesing) {
	                timer += Time.deltaTime;
	            }
	        }
	    }
    }

    public void OnApplicationQuit() {
    	appQuitting = true;
 
/*
     	foreach(string eventName in listenerTable.Keys) {
	        foreach (IEventListener listener in (ArrayList)listenerTable[eventName]) {
	        	this.Log("[EVENTMANAGER]: listener["+listener.GetType().ToString()+"] for event["+eventName+"] still registered BEFORE cleanup");
	        }
        }
*/
        listenerTable.Clear();
        eventQueue.Clear();
        
        // this.Log("[EVENTMANAGER]: Done cleaning up");
    }
}
