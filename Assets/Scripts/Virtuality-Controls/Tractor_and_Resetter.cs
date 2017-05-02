using UnityEngine;
using System.Collections;

public class Tractor_and_Resetter : MonoBehaviour
{
    // manipulated object setup //
    public GameObject manipulated_object;
    Vector3 manipulated_object_starting_position;
    Quaternion manipulated_object_starting_rotation;
    void Start()
    {
        manipulated_object_starting_position = manipulated_object.transform.position;
        manipulated_object_starting_rotation = manipulated_object.transform.rotation;
    }

    // tracting speed values //
    static float tracting_speed_base = 0f, tracting_speed_max = 20f, tracting_speed_increment = .3f;
    float tracting_speed = tracting_speed_base;

    // updating //
    void Update()
    {
        // pulling the manipulated object to the controller //
        if (GetComponent<Controller>().touchpad_pressed())     // touchpad is pressed //
        {
            if (GetComponent<Controller>().touchpad_pressing())     // touchpad is pressing //
                tracting_speed = tracting_speed_base;
            else if ((tracting_speed + tracting_speed_increment) <= tracting_speed_max)
                tracting_speed += tracting_speed_increment;
            else
                tracting_speed = tracting_speed_max;
            manipulated_object.transform.position = Vector3.MoveTowards(manipulated_object.transform.position, transform.position, tracting_speed * Time.deltaTime);
            manipulated_object.GetComponent<Rigidbody>().useGravity = false;
        }
        else if (GetComponent<Controller>().touchpad_unpressing())     // touchpad is unpressing //
        {
            manipulated_object.GetComponent<Rigidbody>().useGravity = true;
        }

        // resetting the manipulated object's position //
        if (GetComponent<Controller>().menu_button_pressed())
        {
            // resetting the manipulated object to its starting transform's position and rotation //
            manipulated_object.transform.position = manipulated_object_starting_position;
            manipulated_object.transform.rotation = manipulated_object_starting_rotation;
            // zeroing the manipulated object's velocity //
            manipulated_object.GetComponent<Rigidbody>().velocity = Vector3.zero;
            manipulated_object.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            // destroying and unsetting any joint on the manipulated object to this controller //
            if ((GetComponent<Holdable_Holder>().joint != null) && GetComponent<Holdable_Holder>().joint.gameObject.tag == "Ball")
            {
                Destroy(GetComponent<Holdable_Holder>().joint);
                GetComponent<Holdable_Holder>().joint = null;
            }
            // destroying and unsetting any joint on the manipulated object to the other controller //
            if (GetComponent<Controller>().other_controller != null)
            {
                FixedJoint other_controller_joint = GetComponent<Controller>().other_controller.GetComponent<Holdable_Holder>().joint;
                if ((other_controller_joint != null) && other_controller_joint.gameObject.tag == "Ball")
                {
                    Destroy(other_controller_joint);
                    other_controller_joint = null;
                }
            }
            // rebaseing the tracting speed //
            tracting_speed = tracting_speed_base;
        }
    }
}
