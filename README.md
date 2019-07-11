**Steps to Insall:**
- Since it is a .Net (core) solution, you will either need Visual Studio or VS Code to clone the 
  solution and run using IIS Express.  You should be able to install the github plugin
  for VS or VS Code, configure it with the URL to the solution, clone the solution, build and
  run.

- I've also deployed the solution to Azure App services, and you can navigate 
  to [The App on Azure](https://fullstackexercise.azurewebsites.net/) to see it working.

**Did I complete the project?**

- I was able to:
	- Allow the user to upload a data file, post it to the server, deserialize it into c# objects,
	  re-serialize it into a JSON Object and deliver it back to JavaScript.
	- The JavaScript then invokes the Google Maps API, centered on the coordinates of the
	  incident address.
	- Create a controller method to accept API calls from the JavaScript and invoke the 
	  darksky.net API described in the exercise and make much of the resulting data 
	  available to view by the user.
	- Create a controller method to accept API calls from the JavaScript and invoke the
	  Parcel service, but not being familiar with how the service works and how to query it
	  I was not able to retrieve useful data. I'm certain that this is something that I could
	  figure out if I had more free time to work on the solution.  Looking back, I probably
	  should have waited for the weekend after next to attempt the exercise as Syrus had offered.
	- Make some of the Additional attributes in the uploaded data file available for the user
	  to view.

**How much time did I spend on it?**

- Start to finish, this took me about 7 hours between last night and tonight.  Unfortunately, I lost some 
  time initially building the C# business objects to de-serialize the JSON file into.  One of 
  the attributes has an illegal name in C#(~), so I had to expirement with ways to re-map it 
  to an alternative name.  Also, the date format in the text files were not natively parsed by 
  the deserializers that I tried and I sunk some more time finding a solution for that.  In 
  hind-sight, for this particular exercise, there was not really much value added to actually 
  having the JSON file as objects on the server side since nothing was done with them but 
  sending them back to the JavaScript as a JSON object.  Just a bit of a stack exercise.