using UnityEngine;
using UnityEngine.UI;

public class Player_Controller : MonoBehaviour {

    public Text m_cube_tranform_text;

    public int m_PlayerNumber = 1;
    public float m_Speed = 12f;
    public float m_TurnSpeed = 180f;

    private string m_MovementAxisName;
    private string m_TurnAxisName;
    private Rigidbody m_Rigidbody;
    private Transform m_cube_transform;
    private float m_MovementInputValue;
    private float m_TurnInputValue;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_cube_transform = GetComponent<Transform>();
    }

    private void OnEnable()
    {
        m_Rigidbody.isKinematic = false;
        m_MovementInputValue = 0f;
        m_TurnInputValue = 0f;
    }

    private void OnDisable()
    {
        m_Rigidbody.isKinematic = true;
    }

    private void Start()
    {
        m_MovementAxisName = "Vertical";
        m_TurnAxisName = "Horizontal";

        Input.gyro.enabled = true;
    }

    private void Update()
    {
        Gyroscope gyro = Input.gyro;
        if (gyro != null)
        {
            m_MovementInputValue = gyro.gravity.x;
            m_TurnInputValue = gyro.gravity.y;
            return;
        }
        m_MovementInputValue = Input.GetAxis(m_MovementAxisName);
        m_TurnInputValue = Input.GetAxis(m_TurnAxisName);
    }

    private void FixedUpdate()
    {
        Move();
        Turn();
        m_cube_tranform_text.text = string.Format("[{0} : {1} : {2}]", (float)System.Math.Round(m_cube_transform.position.x, 2), (float)System.Math.Round(m_cube_transform.position.y, 2), (float)System.Math.Round(m_cube_transform.position.z, 2));
    }

    private void Move()
    {
        // Adjust the position of the tank based on the player's input.
        Vector3 movement = transform.forward * m_MovementInputValue * m_Speed * Time.deltaTime;

        m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
    }


    private void Turn()
    {
        // Adjust the rotation of the tank based on the player's input.
        float turn = m_TurnInputValue * m_TurnSpeed * Time.deltaTime;

        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);

        m_Rigidbody.MoveRotation(m_Rigidbody.rotation * turnRotation);
    }
}
