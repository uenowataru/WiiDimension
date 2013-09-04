WiiDimension
============
This is a project for CS4475. I worked with Ben Nuttle.

Description:  Our final product is an interactive 3D model of Sagrada Familia that adjusts the viewer’s perspective by head tracking using a Wii Remote's camera.

Goals:   The goal of our final project was to create a modular and scalable tool which adapts a wiimote controller to control a computer with airborne gestures using the remote’s built in infrared camera. We then hoped to implement either an intuitive control schema for navigating and browsing pictures, or head tracking to provide a 3D viewing experience. 
As a team, we met this goal fairly well, and in my opinion replicated Johnny Lee’s demo that we critiqued on as close as anyone can get in with the level of our expertise, given our time and resource constraints. Although we were not able to implement control schema with additional gestures by finger tracking with the limited time, we were able to successfully implement the 3D transformations using IR LEDs and the Wii Remote’s camera. As the user moves his head along with the IR light emitter, the view of the model changes appropriately and emulates the properties of 3D object ‘layers’ in the real world. 

Learning Objectives: 
	- taking input from Wii Remote via bluetooth
	- data scrubbing the raw coordinate datas into appropriate workable data
- how to communicate between programs written in different languages
	- transforming 2D data into 3D space information
	- taking the 3D space information and reflecting it on the screen
	- figuring out how eyes’ perspective works
We met all these objectives. We feel like we learned a lot through this project in various subjects we did not expect. We had an unexpected hiccup in passing data between our respective programs, which led to a learning experience in servers and socketing. It was unexpected but very important lesson that I learned through working with this. 
In addition to this, Wataru learned the very powerful strength of geometric calculations that you can do from simple 9th grade Geometry class material. Transforming the 2D information to 3D information was solely done by calculations and formulas that he wrote and he was very excited when the 3D information was really accurate. 
Originally we thought that in order to make the 3D perspective work, we had to keep the virtual camera position in the 3D space stable and only rotate it. We were wrong and instead what we had to do was moving around the camera along with the head and having a focal point (where the camera always points to) for the camera rotation. 
We learned a good bit about some of the headaches associated with various OS’s handling of the bluetooth stack. After several long nights of working on the project we had yet to even get any data back from the remote, which was frustrating to say the least.
Going forward, Ben plans on doing a lot more with the wii remote in developing simple applications for his own use. Hacking has been a hobby of his for some time, and the ease of use of Visual Studio and the abundance of good documentation makes wii remote programming easy.

Coding: 
Coding was done in C# (Visual Studio) and Javascript (Unity).

Link to Unity Code and Javascript: https://drive.google.com/?authuser=0#folders/0B6czzWlkaStkMUNDUlNXQ2ZBYjA

Link to C# code with test server javascript and a trial unity application:
https://github.com/bennuttle/WiiD.git

Softwares used:
Visual Studio 2012: used for writing and editing C# application that tracks the head position using the Wii Remote. 
Unity: used for 3D visualization and data handling of coordinates passed in from head tracking. 

A flow diagram of major tasks in this project (AKA project pipeline):
Ben was mainly responsible of gathering data from the Wii Remote using C# and sending it to Unity application which is written in Javascript. I was responsible of using the data Ben gave to me 

Example workspaces:
Visual Studio for C# development
Unity workspace. Debugging with simple cases and simple models. 

Visual Studio 2012 development environment displaying custom code.
WiiD application running, with diagnostic window detailing raw Wii remote data in real time.
 
Sample Unity Workspace at the time of debugging.

Sample screenshot of the Unity application running. 

Sample Unity Final Workspace.

Workload: Wataru put in about 35 hours of work, writing . Ben put in around 40 hours, between tinkering with different libraries, hardware, and running to stores to purchase required goods.

Percentage distribution of the effort: Planning - 10%, Coding 3D transformation - 15%, Obtaining 3D model - 10%, Using Unity - 20%, Socketing between C# application and Unity - 30%, Coding Wii Remote head tracking - 15%

Images Captured: We did not need to capture any images or videos for this project during development. Images and video recorded for presentation are linked below.

Is this a reproducible artifact: Yes is a reproducible artifact. Anyone who has Windows can install our software and run it. Also anyone with Visual Studio 2012, Unity, and decent knowledge of coding can edit our project and modify it for their purposes. 

Other facts: Our project imports a First Person Control so other programmers looking to add features (such as creating a First Person Shooter game that implements head tracking) can do so very easily by editing the Unity project files. In addition, models can be switched very easily if you can export the 3D models in formats Unity recognizes for anyone wanting to look at other 3D models. 

Link to Final artifact: 
https://drive.google.com/folderview?id=0B6czzWlkaStkZWt1d1dpTTZzVTg&usp=sharing
Video of us demoing is in the folder with the name WiiDSagradaDemo.mp4
