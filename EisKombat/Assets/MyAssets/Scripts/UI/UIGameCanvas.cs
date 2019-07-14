using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameCanvas : MonoBehaviour
{
    public Text altitudeText;
    public Text fuelText;
    public Text rotationText;
    public GameObject plane;
    PlaneController p;

    void Start()
    {
        p = plane.GetComponent<PlaneController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (plane)
        {
            altitudeText.text = (int)plane.transform.position.y + "mtrs";
            fuelText.text = p.fuel + "ml";
            rotationText.text = (int)plane.transform.eulerAngles.x + "°x " + (int)plane.transform.eulerAngles.y + "°y " + (int)plane.transform.eulerAngles.z + "°z ";
        }
    }
}
