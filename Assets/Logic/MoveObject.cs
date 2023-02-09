using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    private Rigidbody2D myRidgidBody;
    private Vector2 mousePosition;
    public Vector3 originalPosition;
    public bool overCollider = false;
    private Collider2D newPositionCollider;

    public float returnSpeed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        myRidgidBody = GetComponent<Rigidbody2D>();
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    void OnMouseDrag()
    {
        Vector2 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 offset = currentMousePosition - mousePosition;
        transform.position = (Vector3)((Vector2)transform.position + offset);
        mousePosition = currentMousePosition;
    }

    void OnMouseUp()
    {
        if (overCollider)
        {
            transform.position = transform.position;
            StartCoroutine(MoveToDesiredPostion(newPositionCollider.bounds.center));
        }
        else
        {
            StartCoroutine(MoveToDesiredPostion(originalPosition));
        }
    }

    public IEnumerator MoveToDesiredPostion(Vector3 desiredPosition)
    {
        while (Vector3.Distance(transform.position, desiredPosition) > 0.1f)
        {
            transform.position = Vector3.Lerp(transform.position, desiredPosition, returnSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = desiredPosition;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "PlaceCard")
        {
            overCollider = true;
            newPositionCollider = collider;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        overCollider = false;
    }
}
