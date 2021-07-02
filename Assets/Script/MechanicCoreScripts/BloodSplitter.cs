using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSplitter : MonoBehaviour
{
    [SerializeField] GameObject[] blood;
    bool bloodFXOffset = false;
    public IEnumerator BloodSplatter()
    {
        yield return new WaitForSeconds(1f);
        bloodFXOffset = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //collision.transform.position;
        print("Levou na piunha");
        if (bloodFXOffset) return;

        bloodFXOffset = true;
        StartCoroutine(BloodSplatter());

        // Find the line from the gun to the point that was clicked.
        Vector3 incomingVec = collision.transform.position - collision.collider.transform.position;

        // Use the point's normal to calculate the reflection vector.
        Vector3 reflectVec = Vector3.Reflect(incomingVec, Vector3.Normalize(collision.collider.transform.localPosition));

        // Activate a random blood 
        //Instantiate(blood[Random.Range(0, blood.Length)].gameObject, reflectVec, collision.transform.rotation, gameObject.transform); ///.gameObject.SetActive(true);

        //// Draw lines to show the incoming "beam" and the reflection.
        //Debug.DrawLine(gunObj.position, hit.point, Color.red);
        //Debug.DrawRay(hit.point, reflectVec, Color.green);

        //Instantiate(blood, hit.point, Quaternion.Euler(reflectVec));

        Instantiate(blood[Random.Range(0, blood.Length)].gameObject, collision.transform.position + reflectVec * Vector3.Angle(collision.transform.position, Vector3.back), collision.transform.rotation, gameObject.transform);

        //blood.transform.rotation = collision.transform.rotation;


        //blood.gameObject.SetActive(false);
        //blood.gameObject.SetActive(true);

        print("Called");
    }
}
