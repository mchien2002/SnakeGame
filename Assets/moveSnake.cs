using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class moveSnake : MonoBehaviour
{
    private Vector2 _direction = Vector2.right;
    // Start is called before the first frame update
    private List<Transform> _segments;
    public Transform segmentPrefab;

    private void Start(){
        _segments = new List<Transform>();
        _segments.Add(this.transform);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("down") && _direction != Vector2.up){
            _direction = Vector2.down;
        }
        else if (Input.GetKey("up") && _direction != Vector2.down){
            _direction = Vector2.up;
        }
        else if (Input.GetKey("left") && _direction != Vector2.right){
            _direction = Vector2.left;
        }
        else if (Input.GetKey("right") && _direction != Vector2.left){
            _direction = Vector2.right;
        }
    }

    private void FixedUpdate(){

        for (int i = _segments.Count - 1; i > 0; i--){
            _segments[i].position = _segments[i - 1].position;
        }

        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + _direction.x,
            Mathf.Round(this.transform.position.y) + _direction.y,
            0.0f
        );
    }


    private void Grow(){
        Transform segment = Instantiate(this.segmentPrefab);
        segment.position = _segments[_segments.Count - 1].position;
        _segments.Add(segment);
    }
    
    private void OnTriggerEnter2D(Collider2D col){
        if (col.tag == "Food"){
            Grow();
        }
        if (col.tag == "Wall" || col.tag == "SnakeSegment"){
            SceneManager.LoadScene(0);
        }
    }
}
