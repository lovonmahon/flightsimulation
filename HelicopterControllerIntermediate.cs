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
        void Start()
        {
            _rb = GetComponent<Rigidbody>();
        }

        void FixedUpdate()
        {
            HoverAbility();
        }
        void Update()
        {
            HandleInput();
        }
        void HandleInput()
        {
            if(Input.GetAxis("Throttle") > 0) //map to T key
            {
                EnginePower += EngineLift;
            }
            if(Input.GetKey(KeyCode.L))
            {
                EnginePower -= EngineLift;
                if(EnginePower < 0)
                {
                    EnginePower = 0;
                }
            }
        }
        void HoverAbility()
        {
            float upforce = 1 - Mathf.Clamp(_rb.transform.position.y/_operationalHeight, 0, 1);
            //Lerp the upward force
            upforce = Mathf.Lerp(0, EnginePower, upforce) * _rb.mass;
            _rb.AddRelativeForce(Vector3.up * upforce);
        }
        
    }
}