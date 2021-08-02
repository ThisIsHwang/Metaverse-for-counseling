using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;
using Firebase.Extensions;
using System;
using System.Globalization;
using System.IO;


public class MovingObject : MonoBehaviour
{
    public float speed;

    private Vector3 vector;

    public float runSpeed;
    private float applyRunSpeed;
    private bool applyRunFlag = false;

    private float x_co = 0;
    private float y_co = 0;
    FirebaseFirestore db;
    public int walkCount;
    private int currentWalkCount;

    private bool canMove = true;


    // Start is called before the first frame update
    void Start()
    {
        db = FirebaseFirestore.DefaultInstance;
        DocumentReference docRef = db.Collection("data").Document("two");
        docRef.Listen(snapshot => {
            Debug.Log("Callback received document snapshot.");
            Debug.Log(String.Format("Document data for {0} document:", snapshot.Id));
            Dictionary<string, object> city = snapshot.ToDictionary();
            foreach (KeyValuePair<string, object> pair in city)
            {
                Debug.Log(String.Format("update - {0}: {1}", pair.Key, pair.Value));
            }
        });
    }
    IEnumerator MoveCoroutine()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            applyRunSpeed = runSpeed;
            applyRunFlag = true;
        }
        else
        {
            applyRunSpeed = 0;
            applyRunFlag = false;
        }
        vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z);

        while (currentWalkCount < walkCount)
        {
            if (vector.x != 0)
            {
                transform.Translate(vector.x * (speed + applyRunSpeed), 0, 0);
            }
            else if (vector.y != 0)
            {
                transform.Translate(0, vector.y * (speed + applyRunSpeed), 0);
            }
            if (applyRunFlag = true)
            {
                ++currentWalkCount;
            }
            ++currentWalkCount;
            yield return new WaitForSeconds(0.01f);
        }
        currentWalkCount = 0;

        canMove = true;
    }


    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        if (canMove)
        {
            if (x != 0 || y != 0)
            {
                canMove = false;
                DocumentReference docRef = db.Collection("data").Document("two");
                //Debug.Log(docRef);

                x_co += x;
                y_co += y;
                Dictionary<string, float> docData = new Dictionary<string, float>
                {
                    { "x", x_co},
                    { "y", y_co},
                };
                docRef.SetAsync(docData);
                StartCoroutine(MoveCoroutine());
            }
        }

    }
}
