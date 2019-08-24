using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawLineScript : MonoBehaviour {

    private LineRenderer lineRenderer;
    private float distance;
    private float marginForError;

    public Transform Mover;
    public Transform node1;
    public Transform node2;
    public float startWidth = .45f, endWidth = .45f;
    
    public float baseSpeed;

	// Use this for initialization
	void Start () {
        marginForError = .001f;

        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = startWidth;
        lineRenderer.endWidth = endWidth;
        lineRenderer.SetPosition(0, node1.position);
        lineRenderer.SetPosition(1, node1.position);

        distance = Vector3.Distance(node1.position, node2.position);

        baseSpeed = .04f;
    }
	
    public void SpeedUpdate(int value)
    {
        baseSpeed = ((float)value) / 50;
    }

	// Update is called once per frame
	void Update () {
        if(node1.GetComponent<Toggle>().isOn)
        {
            if (distance > marginForError)
            {
                float toMove = baseSpeed * Time.deltaTime * GlobalVars._speedMultiplier;
                
                Vector3 oldPos = Mover.position;
                Mover.position = Vector3.MoveTowards(Mover.position, node2.position, toMove);
                GlobalVars._currentDistanceInWorld += Vector3.Distance(Mover.position, oldPos);

                lineRenderer.SetPosition(1, Mover.position);
                distance = Vector3.Distance(Mover.position, node2.position);
            }
            else
            {
                node2.GetComponent<Toggle>().isOn = true;
                node2.GetComponent<MeshRenderer>().enabled = true;
                if (Vector3.Distance(Mover.position, node2.position) > 0)
                {
                    GlobalVars._currentDistanceInWorld += Vector3.Distance(Mover.position, node2.position);
                    Mover.position = node2.position;
                }
            }
        }
        
	}
}
