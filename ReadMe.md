## ZenCache

This is a basic self-hosted concurrent cache for a coding interview.

The application will run at localhost:9000/cache.

In order to fetch a key, do a GET with the key appended to route, like:

    curl -s localhost:9000/cache/key
  
will retrieve the value associated with the key "key", if any, or return an empty string.

In order to set a key, do a POST with the key appended to route, then value in route, like:

    curl -s -X POST --data " " localhost:9000/cache/key/value

will set the key "key" to the value "value".

The current requirement for a non-empty body is a bug due to some default I haven't figured out.

In order to delete a key, do a DELETE with the key appended to route, like:

    curl -s -X DELETE localhost:9000/cache/key

There's an HTML-formatted view of the whole cache contents available with a GET directly on localhost:9000/cache ; this is intended for internal / debugging use as a convenience.

---

## Automatic cache expiration

The code has a given minimum number of minutes which it will keep an item in cache. This can be configured at compile time.

It is located in the Cache.cs file and is the field "minutesToHold".

The code always keep an item in cache for at least that many minutes.

After that many minutes, the code may remove the items from the cache at any time, but is not guaranteed to do so.

If items are being frequently added to the cache, items will typically be removed from the cache between *minutesToHold* and 2\**minutesToHold* minutes.

---

This code was written in Microsoft Visual Studio Community 2017.

In order to run this code, any version of Microsoft Visual Studio should work. Open the .sln file in the IDE.

The first time, the IDE needs to be told explicitly to "restore" the Nuget packages to pull in the dependencies. Right click in the "Solution Explorer",
 select "Manage NuGet Packages", and click "Restore" in the top-right of the window that opens.
 
After you have done this, you should be able to build and run the program like normal with the green "start" button or pressing "F5". When run, the program
 will open a blank console window and run the server. You can use the server as described above. Console output will display as keys are set, fetched, or deleted.
 It will also display a message when the contents are swapped (part of the automatic cache expiration process).
 
When you are done running the program, press "enter" in the console window and it will stop running.

---

This project is based on a skeleton for an OWIN self-hosted Web API provided by Microsoft:
https://github.com/aspnet/samples/tree/master/samples/aspnet/WebApi/OwinSelfhostSample

For more information about OWIN, please see
http://www.asp.net/vnext/overview/owin-and-katana/an-overview-of-project-katana 
