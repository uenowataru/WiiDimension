#pragma strict

function Start () {

}

function Update () {
	var speed = 0.3;
  if (Input.GetKey ("k")){
       //print ("'i': finds difference translates appropriately");
        transform.position = new Vector3(transform.position.x, transform.position.y, -speed + transform.position.z);
	}
    if (Input.GetKey ("l")){
       //print ("'j': finds position, sets position directly");
       //transform.position = getHeadPosition();	
        transform.position = new Vector3(speed + transform.position.x, transform.position.y, transform.position.z);
    }
    if (Input.GetKey ("i")){
       //print ("'k': finds difference translates appropriately");
        transform.position = new Vector3(transform.position.x, transform.position.y, speed + transform.position.z);
	}
	if (Input.GetKey ("j")){
       //print ("'l': finds difference translates appropriately");
	    //transform.Translate(headposition.x - transform.position.x, headposition.y - transform.position.y,headposition.z - transform.position.z);
	    transform.position = new Vector3(-speed + transform.position.x, transform.position.y, transform.position.z);

    }if (Input.GetKey ("o")){
       //print ("'l': finds difference translates appropriately");
        //transform.Translate(headposition.x - transform.position.x, headposition.y - transform.position.y,headposition.z - transform.position.z);
        transform.position = new Vector3(transform.position.x, speed + transform.position.y, transform.position.z);

    }if (Input.GetKey ("u")){
       //print ("'l': finds difference translates appropriately");
        //transform.Translate(headposition.x - transform.position.x, headposition.y - transform.position.y,headposition.z - transform.position.z);
        transform.position = new Vector3(transform.position.x, -speed + transform.position.y, transform.position.z);
    }    
}