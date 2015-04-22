<h1>Small Chat application for Needletail.Mvc</h1>
<p>Update the Needletail.MVC package before run the project.</p>
<p>The sample uses angular.js</p>
<h3>Points of interest.</h3>
<p>Look how the SendMessageTo method on ChatController makes javasript call</p>
<pre>
<code>
public TwoWayResult SendMessageTo(string messageTo, string message)
{
	dynamic call = new ClientCall { ClientId = messageTo, CallerId = User.Identity.Name };
	//make the remote call
	call.messageReceived(User.Identity.Name, message , true);
	return new TwoWayResult(call);
}
</code>
</pre>
<p>The "messageReceived" function is defined in the Chat.js file, you can call js funcions defined anywhere in your javascript, you can also make calls to functions
contained in a namespace like this:</p>
<pre>
<code>
	dynamic call = new ClientCall { ClientId = messageTo, CallerId = User.Identity.Name };
	//make the remote call
	call.sample.of.namespace.call(parameter1, parameter2, etc);
	return new TwoWayResult(call);
</code>
</pre>

<p>You can call javascript code anywhere in your server's code, i.e. you don't need to call as an ActionResult inside a controller</p>
<p>Take a look to To the RemoteController, there is a method in there that is called from the UserHelper class when a user logs in or logs out</p>
<strong>The code that makes client calls does not need to be in the RemoteController</strong>

<pre>
<code>
public static void ReloadUserList()
{
	dynamic reload = new ClientCall { CallerId = string.Empty, ClientId = string.Empty };
	reload.getUsers();
	RemoteExecution.BroadcastExecuteOnClient(reload);
}		       
<code>
</pre>

<p>The "getUsers" method is defined also in the Chat.js file.</p>
