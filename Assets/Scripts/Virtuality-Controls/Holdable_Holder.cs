using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class Holdable_Holder : MonoBehaviour
{
    public enum Button      // possible buttons for the button option
    {
        Trigger,
        Grip
    }
    public Button button;       // button option
    private delegate bool Buttoned();       // method delegation definition for: whether whatever chosen button is buttoned
    private Buttoned buttoned;      // the delegating method for: whether whatever chosen button is buttoned
    // method: delegation for the buttoned method //
    private void buttonedDelegation()
    {
        if (button.Equals(Button.Trigger))      // if the button to use is the trigger
        {
            buttoned = GetComponent<Controller>().trigger_shallowed;        // delegate the 'trigger_shallowed' method to the buttoned delegating method
        }
        else     // otherwise, the button to use is the grip
        {
            buttoned = GetComponent<Controller>().grip_pressed;        // delegate the 'grip_shallowed' method to the buttoned delegating method
        }
    }
    // on start (thus an initial button option has been loaded): delegation for the buttoned method //
    void Start()
    {
        buttonedDelegation();
    }
    // whenever the inspector is reloaded (thus an initial\different button option may have been loaded): delegation for the buttoned method //
    void OnValidate()
    {
        buttonedDelegation();
    }

    // whether or not to collate velocity on released holdables //
    public bool holdable_thrower = true;

    //// holding and releasing one object at most with the trigger ////
    // joint to create on the object to be held, and destroyed on the object and unset once the object is released //
    [HideInInspector] public FixedJoint joint = null;
    // grabbing objects //
    void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.GetComponent<Holdable>() != null)
        {
            // holding objects //
            if ((joint == null) && buttoned())     // joint is not set and trigger is shallowed //
            {
                // destroying and unsetting any joint from the other controller on the object //
                if ((GetComponent<Controller>().other_controller != null) && (GetComponent<Controller>().other_controller.GetComponent<Holdable_Holder>() != null))
                {
                    FixedJoint other_controller_joint = GetComponent<Controller>().other_controller.GetComponent<Holdable_Holder>().joint;
                    if ((other_controller_joint != null) && other_controller_joint.gameObject == collider.gameObject)
                    {
                        Destroy(other_controller_joint);
                        other_controller_joint = null;
                    }
                }
                // creating and connecting a joint on the object //
                joint = collider.gameObject.AddComponent<FixedJoint>();
                joint.connectedBody = GetComponent<Rigidbody>();
            }
            // releasing objects //
            else if ((joint != null) && !buttoned())        // joint is set and trigger is not shallowed //
            {
                // destroying and unsetting the joint //
                Destroy(joint);
                joint = null;
                // collating velocity on the object //
                if (holdable_thrower)
                    GetComponent<Controller>().collate_velocity(collider.attachedRigidbody);
            }
        }
    }
    
    void Update()
    {
        // releasing any objects that have not properly released when they should have been //
        if ((joint != null) && !buttoned())        // joint is set and trigger is not shallowed //
        {
            // finding the collider of the held object //
            Collider held_object_collider = joint.gameObject.GetComponent<Collider>();
            // destroying and unsetting the joint //
            Destroy(joint);
            joint = null;
            // collating velocity on the object //
            if (holdable_thrower)
                GetComponent<Controller>().collate_velocity(held_object_collider.attachedRigidbody);
        }
    }
}