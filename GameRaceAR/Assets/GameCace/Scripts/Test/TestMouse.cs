using UnityEngine;

public class TestMouse : MonoBehaviour {

    public bool visible = true;
    public float x, y;
	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        Cursor.visible = true;
        Cursor.SetCursor(null, new Vector2(x, y), CursorMode.Auto);
        Touch fake = new Touch();
        fake.phase = TouchPhase.Began;
        fake.deltaPosition = new Vector2(0, 0);
        fake.position = new Vector2(10, 10);
        fake.fingerId = 0;

        if (Input.GetMouseButton(0))
        {

        }
    }
}