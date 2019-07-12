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
	  incident address.  Running low on time, I had planned to add markers to the
	  map for the 'apparatus' object positions as well.
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

- What I was unable to do:
	- Retrieve meaningful data from the Parcel API.  When ever I apply a geometry parameter, be it two coordinates
	  separated by a comma or a JSON geometry object, the result is always an empty JSON object.  If I move the 
	  geometry object to the inSR or outSR parameter, then I still receive an empty object with a large list of
	  'features' having only 'circles'.  In combing through the documentation, invoking a query seems straight 
	  forward.  But unfortunately, without being able to retrieve any results, I am unable to do demonstrate an ability
	  to do anything useful with them, such as draw shapes on the map API.

	- I can invoke the end point programatically and retrieve an empty response, but after exhausting the api reference
	  Google/Stack/Youtube and various message boards, I was unable to obtain anything useful from the api.

**How much time did I spend on it?**

- Start to finish, this took me about 7 hours between last night and tonight.  Unfortunately, I lost some 
  time initially building the C# business objects to de-serialize the JSON file into.  One of 
  the attributes has an illegal name in C#(~), so I had to experiment with ways to re-map it 
  to an alternative name.  Also, the date format in the text files were not natively parsed by 
  the deserializers that I tried and I sunk some more time finding a solution for that.  In 
  hind-sight, for this particular exercise, there was not really much value added to actually 
  having the JSON file as objects on the server side since nothing was done with them but 
  sending them back to the JavaScript as a JSON object.  Just a bit of a stack exercise.