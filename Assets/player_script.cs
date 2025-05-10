using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_script : MonoBehaviour
{
    private LineRenderer lr;
    public Material beam_charge_color;
    public Material beam_fire_color;

    float original_beam_width;

    private shop_script ss;
    public bool ship_rotation_locked;

    bool firing;

    public int line_length;
    public float charge_duration; // Duration in seconds for the angle change

    private float elapsedTime = 0f; // Elapsed time since the start

    public GameObject GrapplingHook;
    string grappling_hook_state;
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.material = beam_charge_color;
        lr.useWorldSpace = false;
        original_beam_width = lr.startWidth;
        lr.positionCount = 4;

        ss = GameObject.FindObjectOfType<shop_script>(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (!ship_rotation_locked)
        {
            RotateTowardsMouse();
        }

        if (ss.upgrade_dictionary["regularbeam"].purchased)
        {

            HandleBeamCharge();

        }
        else
        {
            HandleGrapplingHook();
        }




        Vector3[] positions = new Vector3[lr.positionCount];
        lr.GetPositions(positions);

        Vector2 startPoint = lr.GetPosition(0);
        Vector2 endPoint = lr.GetPosition(1);



        // Cast a ray from the start point to the end point
        RaycastHit2D[] hits = Physics2D.RaycastAll(startPoint, new Vector2(-1, 0), (endPoint - startPoint).magnitude);

        Debug.DrawRay(startPoint, new Vector2(1, 0), Color.yellow);

    }

    void HandleBeamCharge()
    {

        if (ss.upgrade_dictionary["charge upgrade"].purchased)
        {
            charge_duration = 0.35f;
        }


        if (Input.GetMouseButton(0) && !firing)
        {
            lr.enabled = true;



            elapsedTime += Time.deltaTime;

            // Ensure elapsed time doesn't exceed duration
            if (elapsedTime > charge_duration)
                elapsedTime = charge_duration;

            float angle1 = Mathf.Lerp(0f, 90f, elapsedTime / charge_duration); // Adjust Time.time as needed
            float angle2 = Mathf.Lerp(180f, 90f, elapsedTime / charge_duration); // Adjust Time.time as needed


            Vector3 line1destinationPoint = AngleToPoint(angle1);
            Vector3 line2DestinationPoint = AngleToPoint(angle2);

            // Update the line renderer points
            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, line1destinationPoint);

            lr.SetPosition(2, transform.position);
            lr.SetPosition(3, line2DestinationPoint);

        }

        if (Input.GetMouseButtonUp(0))
        {

            if (elapsedTime >= charge_duration)
            {
                FireBeam();
            }
            else
            {
                lr.enabled = false;
                elapsedTime = 0;
            }
        }
    }


    void HandleGrapplingHook()
    {
        if (Input.GetMouseButton(0))
        {
            if (!firing)
            {
                grappling_hook_state = "going out";
            }
             

            
            firing = true;
            GrapplingHook.SetActive(true);
        }
        if (firing)
        {
            if (grappling_hook_state == "going out") { 
                if (Vector2.Distance(GrapplingHook.transform.position,transform.position)>12f)
   
                {
                    grappling_hook_state = "coming back";
                }
            
                GrapplingHook.transform.position += transform.up *12*Time.deltaTime;
            }
            else if (grappling_hook_state == "coming back")
            {
                GrapplingHook.transform.position -= transform.up * 12 * Time.deltaTime;
            }
            ship_rotation_locked = true;

            lr.enabled = true;
  
            // Update the line renderer points
            /*
            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, line1destinationPoint);

            lr.SetPosition(2, transform.position);
            lr.SetPosition(3, line2DestinationPoint);
            */

        }
    }

    void FireBeam()
    {





        firing = true;
        lr.positionCount = 2;
        lr.SetPosition(0, transform.position);

        lr.SetPosition(1, AngleToPoint(90));

        lr.material = beam_fire_color;

        //lr.startWidth = .1f;
        //lr.endWidth = .1f;

        Vector3[] positions = new Vector3[lr.positionCount];
        lr.GetPositions(positions);



        Vector2 startPoint = lr.transform.TransformPoint(lr.GetPosition(0));
        Vector2 endPoint = lr.transform.TransformPoint(lr.GetPosition(1));

        // Cast a ray from the start point to the end point
        if (ss.upgrade_dictionary["phasebeam"].purchased)
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(startPoint, endPoint - startPoint, (endPoint - startPoint).magnitude);
            // Process each hit
            foreach (RaycastHit2D hit in hits)
            {
                package_script ps = hit.collider.GetComponent<package_script>();
                if (ps != null)
                {
                    ps.grabbed = true;
                }
            }
        }
        else
        {
            int layer_mask = LayerMask.GetMask("packages");
            RaycastHit2D hit = Physics2D.Raycast(startPoint, endPoint - startPoint, (endPoint - startPoint).magnitude, layer_mask);
            Debug.Log(hit.ToString() + " " + hit.collider);
            if (hit.collider != null)
            {
                package_script ps = hit.collider.GetComponent<package_script>();
                if (ps != null)
                {
                    ps.grabbed = true;
                }
            }
        }



        // Debug.DrawRay(startPoint, endPoint - startPoint, Color.yellow);









        StartCoroutine(StopFiring());

    }

    IEnumerator StopFiring()
    {
        yield return new WaitForSeconds(.05f);

        lr.material = beam_charge_color;
        lr.positionCount = 4;
        firing = false;
        lr.enabled = false;
        elapsedTime = 0;
        lr.startWidth = original_beam_width;
        lr.endWidth = original_beam_width;

    }

    Vector3 AngleToPoint(float angle)
    {
        // Convert angle from degrees to radians
        float angleRad = angle * Mathf.Deg2Rad;

        // Calculate the destination point based on the angle and line length
        return transform.position + new Vector3(Mathf.Cos(angleRad) * line_length, Mathf.Sin(angleRad) * line_length, 0);
    }

    void RotateTowardsMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector3 direction = targetPosition - transform.position;
        direction.z = 0f; // Ensure the object stays in the 2D plane

        // Calculate the angle in degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Rotate the object towards the mouse position
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = rotation;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == GrapplingHook.name)
        {
            grappling_hook_state = "reset";
            GrapplingHook.SetActive(false);
            firing = false;
            ship_rotation_locked = false;
        }

        Debug.Log(collision);
    }
}

