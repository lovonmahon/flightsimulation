using UnityEngine;

namespace AirSimulator
{
    [RequireComponent(typeof(Rigidbody))]
    public class HelicopterControllerIntermediate : MonoBehaviour
    {
        Rigidbody _rb;
        public BladesRotorController MainBlade;
        public BladesRotorController TailBlade;
        
        
        public float EngineLift = 0.075f;
        public float _operationalHeight;
        [SerializeField] float _engineHoverPower;
        float _enginePower;
        public float EnginePower
        {
            get
            {
                return _enginePower;
            }
            set
            {
                MainBlade.BladeSpeed = value * 250;
                TailBlade.BladeSpeed = value * 500;
                _enginePower = value;
            }
        }

        public float ForwardForce;
        public float BackwardForce;
        public float ForwardTiltForce;
        public float TurningTiltForce;
        public float TurnForce;
        public float TurnModifier;
        float turning = 0f;
        Vector3 _movement = Vector3.zero;
        Vector3 _tilt = Vector3.zero;
        public LayerMask GroundLayer;
        float _distanceToGround;
        bool isGrounded;
        
        void Start()
        {
            _rb = GetComponent<Rigidbody>();
            isGrounded = true;
        }
        void FixedUpdate()
        {
            HoverAbility();
            HelicopterMovement();
            HelicopterTilt();
        }
        void Update()
        {
            HandleInput();
            GroundCheck();
        }
        void GroundCheck()
        {
            RaycastHit hit;
            Vector3 direction = transform.TransformDirection(Vector3.down);
            Ray ray = new Ray(transform.position, direction);
            
            if(Physics.Raycast(ray, out hit, GroundLayer))
            {
                _distanceToGround = hit.distance;
                
                if(_distanceToGround < 2)
                {
                    isGrounded = true;
                }
                else
                {
                    isGrounded = false;
                }
            }
        }
        void HandleInput()
        {
            if(!isGrounded)  //is there check needed here since it is in HelicopterMovement()? 
                _movement.x = Input.GetAxis("Horizontal");
                _movement.y = Input.GetAxis("Vertical");
                
                if(Input.GetKey(KeyCode.L))
                {
                    EnginePower -= EngineLift;
                    _engineHoverPower -= EngineLift;

                    if(EnginePower < 0)
                    {
                        EnginePower = 0;
                    }
                }
            
            if(Input.GetAxis("Throttle") > 0) //mapped to T key
            {
                EnginePower += EngineLift;
                _engineHoverPower += EngineLift;
                _engineHoverPower = Mathf.Max(12.7f);
            }
            //Hover
            else if(Input.GetAxis("Throttle") < 0.5f && !isGrounded)
            {
                EnginePower = Mathf.Lerp(EnginePower, _engineHoverPower, 0.2f);
            }
        }
        void HoverAbility()
        {
            float upforce = 1 - Mathf.Clamp(_rb.transform.position.y/_operationalHeight, 0, 1);
            //Lerp the upward force
            upforce = Mathf.Lerp(0, EnginePower, upforce) * _rb.mass;
            _rb.AddRelativeForce(Vector3.up * upforce);
        }
        void HelicopterMovement()
        {
            if(!isGrounded)    
            {
                if(Input.GetAxis("Vertical") > 0)
                {
                    _rb.AddRelativeForce(Vector3.forward * Mathf.Max(0f, _movement.y * ForwardForce * _rb.mass));
                }
                else if (Input.GetAxis("Vertical") < 0)
                {
                    _rb.AddRelativeForce(Vector3.back * Mathf.Max(0f, - _movement.y * BackwardForce * _rb.mass));
                }

                if(Input.GetAxis("Horizontal") > 0.4 && !isGrounded || Input.GetAxis("Horizontal") < 0 && !isGrounded )
                {
                    float turn = TurnForce * Mathf.Lerp(_movement.x, _movement.x * (TurnModifier - Mathf.Abs(_movement.y)), Mathf.Max(0f, _movement.y));
                    turning = Mathf.Lerp(turning, turn, Time.fixedDeltaTime * TurnForce);
                    _rb.AddRelativeTorque(0f, turning * _rb.mass, 0f);
                }
            }
        }
        void HelicopterTilt()
        {
            //Yaw
            _tilt.y = Mathf.Lerp(_tilt.y, _movement.y * ForwardTiltForce, Time.deltaTime);
            //Pitch
            _tilt.x = Mathf.Lerp(_tilt.x, _movement.x * TurningTiltForce, Time.deltaTime);

            _rb.transform.localRotation = Quaternion.Euler(_tilt.y, _rb.transform.localEulerAngles.y, -_tilt.x);
        }
    }
}