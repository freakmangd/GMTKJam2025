using System;
using UnityEngine;

public class Door : MonoBehaviour
{
    public float minOpen = -90f;
    public float maxOpen = -220f;

    bool opening;
    float openTimer;

    void Update()
    {
        if (opening && openTimer <= 1f)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, Mathf.Lerp(minOpen, maxOpen, openTimer), transform.eulerAngles.z);
            openTimer += Time.deltaTime;
        }
    }

    public void Open()
    {
        opening = true;
    }
}
