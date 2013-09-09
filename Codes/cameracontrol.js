
 var headposition : Vector3;
 var valueset : boolean = false;
 var objectdepth : double = -20.0;
 var socketScript;
 var player : GameObject;
 var ratio3Dreal = 0.1;


 function Start(){
    getHeadPosition(0,0,10,20);
    headposition = getHeadPosition();
    socketScript = GameObject.Find("Main Camera").GetComponent("Server");
    player = GameObject.Find("First Person Controller");
 }
 function Update () {
 	var speed = 0.3;
 	
 	
 	/*
    if (Input.GetKey ("k")){
       //print ("'i': finds difference translates appropriately");
        headposition = new Vector3(headposition.x, headposition.y, -speed + headposition.z);
	}
    if (Input.GetKey ("l")){
       //print ("'j': finds position, sets position directly");
       //transform.position = getHeadPosition();	
        headposition = new Vector3(speed + headposition.x, headposition.y, headposition.z);
    }
    if (Input.GetKey ("i")){
       //print ("'k': finds difference translates appropriately");
        headposition = new Vector3(headposition.x, headposition.y, speed + headposition.z);
	}
	if (Input.GetKey ("j")){
       //print ("'l': finds difference translates appropriately");
	    //transform.Translate(headposition.x - transform.position.x, headposition.y - transform.position.y,headposition.z - transform.position.z);
	    headposition = new Vector3(-speed + headposition.x, headposition.y, headposition.z);

    }if (Input.GetKey ("o")){
       //print ("'l': finds difference translates appropriately");
        //transform.Translate(headposition.x - transform.position.x, headposition.y - transform.position.y,headposition.z - transform.position.z);
        headposition = new Vector3(headposition.x, speed + headposition.y, headposition.z);

    }if (Input.GetKey ("u")){
       //print ("'l': finds difference translates appropriately");
        //transform.Translate(headposition.x - transform.position.x, headposition.y - transform.position.y,headposition.z - transform.position.z);
        headposition = new Vector3(headposition.x, -speed + headposition.y, headposition.z);
    }if (Input.GetKey ("t")){
       //print ("'l': finds difference translates appropriately");
        //transform.Translate(headposition.x - transform.position.x, headposition.y - transform.position.y,headposition.z - transform.position.z);
		objectdepth += 0.3;
    }if (Input.GetKey ("y")){
       //print ("'l': finds difference translates appropriately");
        //transform.Translate(headposition.x - transform.position.x, headposition.y - transform.position.y,headposition.z - transform.position.z);
		objectdepth -= 0.3;
    }
    */
    
    
    
    
    
    if (Input.GetKey ("1")){
       //print ("'l': finds difference translates appropriately");
        //transform.Translate(headposition.x - transform.position.x, headposition.y - transform.position.y,headposition.z - transform.position.z);
		objectdepth -= 0.3;
    }if (Input.GetKey ("2")){
       //print ("'l': finds difference translates appropriately");
        //transform.Translate(headposition.x - transform.position.x, headposition.y - transform.position.y,headposition.z - transform.position.z);
		objectdepth += 0.3;
    }
    if (Input.GetKey ("3")){
       //print ("'l': finds difference translates appropriately");
        //transform.Translate(headposition.x - transform.position.x, headposition.y - transform.position.y,headposition.z - transform.position.z);
		ratio3Dreal -= 0.01;
    }if (Input.GetKey ("4")){
       //print ("'l': finds difference translates appropriately");
        //transform.Translate(headposition.x - transform.position.x, headposition.y - transform.position.y,headposition.z - transform.position.z);
		ratio3Dreal += 0.01;
    }
    var prevposition = headposition;
    var msg = socketScript.getMessage();
    if(msg.length>0){
  		var msgarray = msg.Split(","[0]);
  		//print(msg + "   " +  msg.length + " " + msgarray.ToString());
	 	var x1 = parseInt(msgarray[1]);
		var y1 = parseInt(msgarray[2]);
		var x2 = parseInt(msgarray[3]);
		var y2 = parseInt(msgarray[4]);
		headposition = getHeadPosition(x1, y1, x2, y2);
    }
    
    //player.transform.position = headposition;
  
    transform.localEulerAngles = calculateRotation();
    setCameraPosition();
    
   
    //transform.TransformDirection(new UnityEngine.Vector3(50,50,0));
    //transform.Translate(30, 30, 0);
}

function calculateRotation(){
	var windowRadius = 5.0;
    var yangle = 180*Mathf.Atan(headposition.x/(headposition.z+objectdepth))/Mathf.PI;
    var distance = Mathf.Sqrt((headposition.z+objectdepth)*(headposition.z+objectdepth) + (headposition.x+objectdepth)*(headposition.x+objectdepth));
    var xangle = 180*Mathf.Atan(headposition.y/distance)/Mathf.PI;
    var a = (headposition-new Vector3(-windowRadius,0,0)).sqrMagnitude;
    var b = (headposition-new Vector3(windowRadius,0,0)).sqrMagnitude;
    //Camera.main.fieldOfView = 180 * Mathf.Acos( (a+b-Mathf.Pow(2*windowRadius,2))/(2*Mathf.Sqrt(a*b))) / Mathf.PI;
    //print("a:" + a + "b: " + b + "fov:" + fieldOfView);
    //fieldOfView = field;
    
    return new Vector3(xangle,yangle,0);
}

function getHeadPosition(){
    if(!valueset){
        valueset = true;
        return new Vector3(0,0,-3);
    }else{
        return headposition;
    }
}

//Calculates the headposition based on the pixel xy coordinates of 2 light sources
function getHeadPosition(x1 : int, y1 : int, x2 : int, y2 : int){
    //Data in cm gathered from testing 
    var actualwidth = 21.0;
    var distance0 = 50.0;
    //var distance0 = 80.0;
    var height0 = 10.0;
    //Perceived width in pixels (distance of the two light positions) with the above settings
    var percwidth0 = 357.0;
    //var percwidth0 = 325.0;
    
    var pixelx = 1024;
    var pixely = 768;
    //The ratio of units in the real world and game units
    
    
    var positionx = -(actualwidth/percwidth0) * (x1 + x2 - pixelx)/2;
    var positiony = (actualwidth/percwidth0) * (y1 + y2 - pixely)/2;
	
	
    var percwidth = Mathf.Sqrt((x1-x2)*(x1-x2) + (y1-y2) * (y1-y2));
    var distance =  percwidth * distance0/percwidth0;
    //objectdepth = -100.0 * distance / 3.0;
    //print("Coordinates:" + x1 + "," + y1 + ";" +  x2 + "," +y2);
    //print("Calculated Headposition:" + positionx + "," +  positiony + "," + distance);
    return new Vector3(ratio3Dreal * positionx, ratio3Dreal * (positiony + height0), ratio3Dreal * distance -10);
}

function setCameraPosition(){
	//var ratio = 1.0 * objectdepth / (objectdepth + headposition.z);
	//print("z:" + headposition.z + ", ratio:" + ratio + ", deno:" + objectdepth + "," + headposition.z );
	if(headposition.z < -1){
		transform.position = headposition;
	}
	//transform.position = new Vector3(ratio*headposition.x, ratio*headposition.y, 0);
}