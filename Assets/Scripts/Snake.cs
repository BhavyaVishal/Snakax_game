using System.Collections.Generic;
using UnityEngine;

 

public class Snake : MonoBehaviour
{
    
    private List<Transform> _segments = new List<Transform>();
    public Transform segmentPrefab;
    public int initialSize = 4;
    public Vector2 direction = Vector2.right;
    private void Start()
    {
        ResetState();
    }
    private void Update()
    {
    if (this.direction.x != 0.0f)
        {
           if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
                this.direction = Vector2.up;
            } else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
                this.direction = Vector2.down;
            }
        }
         
        else if (this.direction.y != 0.0f)
        {  
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
                this.direction = Vector2.right;
            } else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
                this.direction = Vector2.left;
            }
        }
    }

    private void FixedUpdate()
    {
        for (int i = _segments.Count - 1; i > 0; i--) {
            _segments[i].position = _segments[i - 1].position;
        }
 
        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + this.direction.x,
            Mathf.Round(this.transform.position.y) + this.direction.y);
    }

    public void Grow()
    {
        Transform segment = Instantiate(this.segmentPrefab);
        segment.position = _segments[_segments.Count - 1].position;
        _segments.Add(segment);
    }

    public void ResetState()
    {
       
        this.direction = Vector2.right;
        this.transform.position = Vector3.zero;
 
        for (int i = 1; i < _segments.Count; i++) {
            Destroy(_segments[i].gameObject);
        }
  
        _segments.Clear();
        _segments.Add(this.transform);

      
        for (int i = 0; i < this.initialSize - 1; i++) {
            Grow();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Food")
        {
            Grow();
        }
        else if (other.tag == "Obstacle")
        {   
            ResetState();
        }
    }

}
