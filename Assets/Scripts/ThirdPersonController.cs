using UnityEngine;
using TMPro;
using System.Collections;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

/* Note: animations are called via the controller for both the character and capsule using animator null checks
 */
namespace StarterAssets
{
    [RequireComponent(typeof(CharacterController))]
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
    [RequireComponent(typeof(PlayerInput))]
#endif
    public class ThirdPersonController : MonoBehaviour
    {
       
        [Header("Player")]
        [Tooltip("Move speed of the character in m/s")]
        public float MoveSpeed = 2.0f;

        [Tooltip("Sprint speed of the character in m/s")]
        public float SprintSpeed = 5.335f;

        [Tooltip("How fast the character turns to face movement direction")]
        [Range(0.0f, 0.3f)]
        public float RotationSmoothTime = 0.12f;

        [Tooltip("Acceleration and deceleration")]
        public float SpeedChangeRate = 10.0f;

        public AudioClip LandingAudioClip;
        public AudioClip[] FootstepAudioClips;
        [Range(0, 1)] public float FootstepAudioVolume = 0.5f;

        [Space(10)]
        [Tooltip("The height the player can jump")]
        public float JumpHeight = 1.2f;

        [Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
        public float Gravity = -15.0f;

        [Space(10)]
        [Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
        public float JumpTimeout = 0.50f;

        [Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
        public float FallTimeout = 0.15f;

        [Header("Player Grounded")]
        [Tooltip("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
        public bool Grounded = true;

        [Tooltip("Useful for rough ground")]
        public float GroundedOffset = -0.14f;

        [Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
        public float GroundedRadius = 0.28f;

        [Tooltip("What layers the character uses as ground")]
        public LayerMask GroundLayers;

        [Header("Cinemachine")]
        [Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
        public GameObject CinemachineCameraTarget;

        [Tooltip("How far in degrees can you move the camera up")]
        public float TopClamp = 70.0f;

        [Tooltip("How far in degrees can you move the camera down")]
        public float BottomClamp = -30.0f;

        [Tooltip("Additional degress to override the camera. Useful for fine tuning camera position when locked")]
        public float CameraAngleOverride = 0.0f;

        [Tooltip("For locking the camera position on all axis")]
        public bool LockCameraPosition = false;


       ChangeRotateAndShake changeRotateAndShakeScript;

        // cinemachine
        private float _cinemachineTargetYaw;
        private float _cinemachineTargetPitch;

        // player

        private GameObject _pickable;
        public LayerMask enemy;
        private bool pickable;

        private bool enemyInSightRange;
        public float sightRangeforEnemy;

        [SerializeField] takeDamage _takeDamage;
         HealthBarScript _healtBarPlayer,_healthBarEnemy;

        [SerializeField] public   TMP_Text _text; 
        public static  float _numDiamond=0;
        private bool bobbing;
        private bool back;
        private bool dodgeRight;
        private bool dodgeLeft;
        private bool forward;
        private bool idle;
        public bool atack;
        public bool _damaged;


        


        public float playerHealth = 120;
        

        private float _speed;
        private float _animationBlend;
        private float _targetRotation = 0.0f;
        private float _rotationVelocity;
        private float _verticalVelocity;
        private float _terminalVelocity = 53.0f;

       

        // timeout deltatime
        private float _jumpTimeoutDelta;
        private float _fallTimeoutDelta;

        // animation IDs
        private int _animIDSpeed;
        private int _animIDGrounded;
        private int _animIDJump;
        private int _animIDFreeFall;
        private int _animIDMotionSpeed;

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
        private PlayerInput _playerInput;
#endif
        public Animator _animator;
        private CharacterController _controller;
        private StarterAssetsInputs _input;
        private GameObject _mainCamera;

        private const float _threshold = 0.01f;

        private bool _hasAnimator;

        private bool IsCurrentDeviceMouse
        {
            get
            {
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
                return _playerInput.currentControlScheme == "KeyboardMouse";
#else
				return false;
#endif
            }
        }


        private Vector3 Dodge;
        private CapsuleCollider _capsuleCollider;

        private void Awake()
        {
            _healtBarPlayer = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<HealthBarScript>();
            _healthBarEnemy = GameObject.FindGameObjectWithTag("EnemyHealthBar").GetComponent<HealthBarScript>();
            _healtBarPlayer.SetMaxValue((int)playerHealth); 

            // get a reference to our main camera
            if (_mainCamera == null)
            {
                _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            }
            _capsuleCollider = GetComponent<CapsuleCollider>();
            
            changeRotateAndShakeScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ChangeRotateAndShake>();
        }

        private void Start()
        {
            _text.text = "x" + (_numDiamond).ToString();

            _cinemachineTargetYaw = CinemachineCameraTarget.transform.rotation.eulerAngles.y;
            
            _hasAnimator = TryGetComponent(out _animator);
            _controller = GetComponent<CharacterController>();
            _input = GetComponent<StarterAssetsInputs>();
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
            _playerInput = GetComponent<PlayerInput>();
#else
			Debug.LogError( "Starter Assets package is missing dependencies. Please use Tools/Starter Assets/Reinstall Dependencies to fix it");
#endif

            AssignAnimationIDs();

            // reset our timeouts on start
            _jumpTimeoutDelta = JumpTimeout;
            _fallTimeoutDelta = FallTimeout;


            

            
        }
        
 


        private void Update()
        {
            _hasAnimator = TryGetComponent(out _animator);

            JumpAndGravity();
            GroundedCheck();
            Move();

            enemyInSightRange = Physics.CheckSphere(transform.position, sightRangeforEnemy, enemy);

            if (enemyInSightRange)
            {
                _healtBarPlayer.GetComponent<RectTransform>().localScale = new Vector3(1, 0.5f, 1);
                _healthBarEnemy.GetComponent<RectTransform>().localScale = new Vector3(1, 0.5f, 1);
            }
            if (!enemyInSightRange)
            {
               _healtBarPlayer.GetComponent<RectTransform>().localScale = new Vector3(0, 0, 0);
                _healthBarEnemy.GetComponent<RectTransform>().localScale = new Vector3(0, 0, 0);
            }


            if (atack == false)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0) && Grounded == true && _input.sprint == false) //Damage alma animasyonu oynamasın.
                {
                    atack = true;
                    _animator.SetBool("Atack", atack);
                    if (atack == true)
                    {

                        Invoke(nameof(AktifHaleGetirme), .3f);
                        if (_takeDamage.getHit == false)
                        {
                            Invoke(nameof(TekrardanPasifHaleGetirme), .7f); //17 de olabilir.
                        }


                        if (_takeDamage.getHit == true)
                        {
                            Invoke(nameof(TekrardanPasifHaleGetirme), .03f);
                        }

                    }


                    Invoke(nameof(AtackPasifleme), 1f);


                    StartCoroutine(changeRotateAndShakeScript.shakeAttackEffect(changeRotateAndShakeScript.shakeAttackTime));
                    _damaged = false;
                    MoveSpeed = 0.0f;
                    SprintSpeed = 0.0f;
                    //_animator.ResetTrigger("Attack");
                    StartCoroutine(BeforeSpeed(1.15f));
                }
            }

            
            if (dodgeLeft == false)
            {
                if ((Input.GetKeyDown(KeyCode.LeftAlt) && Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.A)) && Grounded == true && !Input.GetKeyDown(KeyCode.Mouse0))
                {

                    if (atack == false)
                    {
                        StartCoroutine(LeftAyarlama(.2f));
                        CharacterMoveLeft();

                    }

                }
            }
            
            if (dodgeRight == false)
            {
                if ((Input.GetKeyDown(KeyCode.LeftAlt) && Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.D)) && Grounded == true && !Input.GetKeyDown(KeyCode.Mouse0))
                {
                    if (atack == false)
                    {
                        StartCoroutine(RightAyarlama(.2f));
                        CharacterMoveRight();  //Animasyon çok kısa old. için move'u sonradan atmaya gerek yok zaten her türlü oynar.

                    }

                }
            }
           
            if (forward == false)
            {
                if ((Input.GetKeyDown(KeyCode.LeftControl) && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.W)) && Grounded == true && !Input.GetKeyDown(KeyCode.Mouse0))
                {

                    if (atack == false)
                    {
                        StartCoroutine(ForwardAyarlama(.2f));

                        //     Invoke(nameof(CharacterMoveForwardBack), .2f); //Sıkıntı şu : Move biraz sonra çalışsa sorun kalacağını düşünmüyorum.

                    }
                }
            }
            if (back == false)
            {
                if ((Input.GetKeyDown(KeyCode.LeftAlt) && Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.S)) && Grounded == true && !Input.GetKeyDown(KeyCode.Mouse0))
                {

                    if (atack == false)
                    {
                        StartCoroutine(BackAyarlama(.2f));
                        CharacterMoveForwardBack();
                    }

                }
            }
            
            
               
                if (pickable==true && Input.GetKeyDown(KeyCode.E) && Grounded == true && !Input.GetKeyDown(KeyCode.Mouse0) && _input.sprint == false)
                {
                    _animator.SetTrigger("Take");
                    _numDiamond += 1;   //İkili topluyor. 0.5 YAPTIM. İlerleyen zamanalarda değiştir. Belki trigger'dan kaynaklanıyordur. Sonradan bak.
                    _text.text = "x" + (_numDiamond).ToString();
                    Destroy(_pickable.gameObject, 1f);
                    Invoke(nameof(PickableFalselama), .001f);
                }

        }



        private void CharacterMoveForwardBack()
        {
            Dodge.z = 30 * Time.deltaTime;
            _controller.Move(Dodge);
        }
        private void CharacterMoveLeft()
        {
            Dodge.x = 20 * Time.deltaTime;
            _controller.Move(Dodge);
        }
        private void CharacterMoveRight()
        {
            Dodge.x = 20 * Time.deltaTime;
            _controller.Move(Dodge);
        }

        private void DodgeBackAktif()
        {
            back = true;
            _animator.SetBool("Back", back);
        }
        private void DodgeBackPasif()
        {
            back = false;
            _animator.SetBool("Back", back);
        }
        private void forwardTruelama()
        {
            forward = true;
            _animator.SetBool("forward",forward);
        }
        private void forwardFalselama()
        {
            forward = false;
            _animator.SetBool("forward", forward);
        }
        IEnumerator ForwardAyarlama(float time)
        {
            forwardTruelama();
            yield return new WaitForSeconds(time);
            CharacterMoveForwardBack();
            yield return new WaitForSeconds(.3f);
            forwardFalselama();
        }
        IEnumerator BackAyarlama(float time)
        {
            DodgeBackAktif();
            yield return new WaitForSeconds(time);
            yield return new WaitForSeconds(.1f);
            DodgeBackPasif();
        }
        IEnumerator RightAyarlama(float time)
        {
            DodgeRightAktif();
            yield return new WaitForSeconds(time);
            yield return new WaitForSeconds(.1f);
            DodgeRightPasif();
        }
        IEnumerator LeftAyarlama(float time)
        {
            DodgeLeftAktif();
            yield return new WaitForSeconds(time);
            yield return new WaitForSeconds(.1f);
            DodgeLeftPasif();
        }

        private void AtackPasifleme()
        {
            atack = false;
            _animator.SetBool("Atack",atack);
            Invoke(nameof(idleTruelama), .1f);

        }
        IEnumerator BeforeSpeed(float time)
        {
            yield return new WaitForSeconds(time);
            MoveSpeed = 3.0f;
            SprintSpeed = 10f;
        }
       private void AktifHaleGetirme()
       {
           GameObject.FindGameObjectWithTag("baphomet").GetComponent<CapsuleCollider>().enabled = true;
       }
       private void TekrardanPasifHaleGetirme()
       {
           GameObject.FindGameObjectWithTag("baphomet").GetComponent<CapsuleCollider>().enabled = false;
       }

        private void DodgeRightAktif()
        {
            dodgeRight = true;
            _animator.SetBool("DodgeRight", dodgeRight);
        }
        private void DodgeRightPasif()
        {
            dodgeRight = false;
            _animator.SetBool("DodgeRight", dodgeRight);
        }
        private void DodgeLeftAktif()
        {
            dodgeLeft = true;
            _animator.SetBool("DodgeLeft", dodgeLeft);
        }
        private void DodgeLeftPasif()
        {
            dodgeLeft = false;
            _animator.SetBool("DodgeLeft", dodgeLeft);
        }


        private void OnTriggerEnter(Collider other)
        {
         
            if (other.gameObject.CompareTag("damage"))
            {
               
                damageTruelama();
                StartCoroutine(DamageYerkenDurma(1.0f)); //Buna bak.
                if (_damaged ==true && atack == true)
                {
                    _damaged = false;
                    _animator.SetBool("Damaged", _damaged);
                }
                Invoke(nameof(TrueFalseIdle), .03f);
                Invoke(nameof(damageFalselama),.4f);
                Invoke(nameof(idleFalselama), 0.01f);

            }
            
            if (other.gameObject.CompareTag("PickableObject"))
            {
                pickable = true;
                _pickable = other.gameObject;
            }

          
            IEnumerator DamageYerkenDurma(float time)
            {
                MoveSpeed = 0.0f;
                SprintSpeed = 0.0f;
                yield return new WaitForSeconds(time);
                MoveSpeed = 3.0f;
                SprintSpeed = 10.0f;
            }

        }

       
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("PickableObject"))
            {
                pickable = false;
            }
        }
        private void PickableFalselama()
        {
            pickable = false;
        }
        private void damageTruelama()
        {
            _damaged = true;
            _animator.SetBool("Damaged", _damaged);
        }
        private void damageFalselama()
        {
            _damaged = false;
            _animator.SetBool("Damaged", _damaged);
        }
       
       
        private void TrueFalseIdle() //Çalışıyo
        {
           
            if (atack == true )
            {
                idleFalselama();
            }
            else
            {
                StartCoroutine(idleAyarlama(1f));
            }
            
        }

        IEnumerator idleAyarlama(float time)
        {
            yield return new WaitForSeconds(time);
            idleTruelama();
            yield return new WaitForSeconds(.01f);
            idleFalselama();
        }
        private void idleTruelama() //Her damage yedikten sonra çalışması lazım.
        {
            idle = true;
            _animator.SetBool("idle", idle);
        }
        private void idleFalselama()
        {
            idle = false;
            _animator.SetBool("idle", idle);
        }


        private void LateUpdate()
        {
            CameraRotation();
        }

      

        private void AssignAnimationIDs()
        {
            _animIDSpeed = Animator.StringToHash("Speed");
            _animIDGrounded = Animator.StringToHash("Grounded");
            _animIDJump = Animator.StringToHash("Jump");
            _animIDFreeFall = Animator.StringToHash("FreeFall");
            _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
        }

        private void GroundedCheck()
        {
            // set sphere position, with offset
            Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset,
                transform.position.z);
            Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers,
                QueryTriggerInteraction.Ignore);

            // update animator if using character
            if (_hasAnimator)
            {
                _animator.SetBool(_animIDGrounded, Grounded);
            }
        }

        private void CameraRotation()
        {
            // if there is an input and camera position is not fixed
            if (_input.look.sqrMagnitude >= _threshold && !LockCameraPosition)
            {
                //Don't multiply mouse input by Time.deltaTime;
                float deltaTimeMultiplier = IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;

                _cinemachineTargetYaw += _input.look.x * deltaTimeMultiplier;
                _cinemachineTargetPitch += _input.look.y * deltaTimeMultiplier;
            }

            // clamp our rotations so our values are limited 360 degrees
            _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
            _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

            // Cinemachine will follow this target
            CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + CameraAngleOverride,
                _cinemachineTargetYaw, 0.0f);
        }

        private void Move()
        {
            
            // set target speed based on move speed, sprint speed and if sprint is pressed
            float targetSpeed = _input.sprint ? SprintSpeed : MoveSpeed;

            // a simplistic acceleration and deceleration designed to be easy to remove, replace, or iterate upon

            // note: Vector2's == operator uses approximation so is not floating point error prone, and is cheaper than magnitude
            // if there is no input, set the target speed to 0
            if (_input.move == Vector2.zero) targetSpeed = 0.0f;

            // a reference to the players current horizontal velocity
            float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;

            float speedOffset = 0.1f;
            float inputMagnitude = _input.analogMovement ? _input.move.magnitude : 1f;

            // accelerate or decelerate to target speed
            if (currentHorizontalSpeed < targetSpeed - speedOffset ||
                currentHorizontalSpeed > targetSpeed + speedOffset)
            {
                // creates curved result rather than a linear one giving a more organic speed change
                // note T in Lerp is clamped, so we don't need to clamp our speed
                _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude,
                    Time.deltaTime * SpeedChangeRate);

                // round speed to 3 decimal places
                _speed = Mathf.Round(_speed * 1000f) / 1000f;
            }
            else
            {
                _speed = targetSpeed;
            }

            _animationBlend = Mathf.Lerp(_animationBlend, targetSpeed, Time.deltaTime * SpeedChangeRate);
            if (_animationBlend < 0.01f) _animationBlend = 0f;

            // normalise input direction
            Vector3 inputDirection = new Vector3(_input.move.x, 0.0f, _input.move.y).normalized;

            // note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
            // if there is a move input rotate player when the player is moving
            if (_input.move != Vector2.zero)
            {
                _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg +
                                  _mainCamera.transform.eulerAngles.y;
                float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity,
                    RotationSmoothTime);

                // rotate to face input direction relative to camera position
                transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
            }


            Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;

            // move the player
            _controller.Move(targetDirection.normalized * (_speed * Time.deltaTime) +
                             new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
            

            // update animator if using character
            if (_hasAnimator)
            {
                _animator.SetFloat(_animIDSpeed, _animationBlend);
                _animator.SetFloat(_animIDMotionSpeed, inputMagnitude);

               
            }
        }

        private void JumpAndGravity()
        {
            if (Grounded)
            {
                // reset the fall timeout timer
                _fallTimeoutDelta = FallTimeout;

                // update animator if using character
                if (_hasAnimator )
                {
                    _animator.SetBool(_animIDJump, false);
                    _animator.SetBool(_animIDFreeFall, false);
                }

                // stop our velocity dropping infinitely when grounded
                if (_verticalVelocity < 0.0f)
                {
                    _verticalVelocity = -2f;
                }

                // Jump
                if (_input.jump && _jumpTimeoutDelta <= 0.0f)
                {
                    // the square root of H * -2 * G = how much velocity needed to reach desired height
                    _verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);

                    // update animator if using character
                    if (_hasAnimator)
                    {
                        _animator.SetBool(_animIDJump, true);
                    }
                }

                // jump timeout
                if (_jumpTimeoutDelta >= 0.0f)
                {
                    _jumpTimeoutDelta -= Time.deltaTime;
                }
            }
          
            else
            {
                // reset the jump timeout timer
                _jumpTimeoutDelta = JumpTimeout;

                // fall timeout
                if (_fallTimeoutDelta >= 0.0f)
                {
                    _fallTimeoutDelta -= Time.deltaTime;
                }
                else
                {
                    // update animator if using character
                    if (_hasAnimator)
                    {
                        _animator.SetBool(_animIDFreeFall, true);
                    }
                }

                // if we are not grounded, do not jump
                _input.jump = false;
            }

            // apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
            if (_verticalVelocity < _terminalVelocity)
            {
                _verticalVelocity += Gravity * Time.deltaTime;
            }
        }

        private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -360f) lfAngle += 360f;
            if (lfAngle > 360f) lfAngle -= 360f;
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }

        private void OnDrawGizmosSelected()
        {
            Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
            Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

            if (Grounded) Gizmos.color = transparentGreen;
            else Gizmos.color = transparentRed;

            // when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
            Gizmos.DrawSphere(
                new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z),
                GroundedRadius);
        }

        private void OnFootstep(AnimationEvent animationEvent)
        {
            if (animationEvent.animatorClipInfo.weight > 0.5f)
            {
                if (FootstepAudioClips.Length > 0)
                {
                    var index = Random.Range(0, FootstepAudioClips.Length);
                    AudioSource.PlayClipAtPoint(FootstepAudioClips[index], transform.TransformPoint(_controller.center), FootstepAudioVolume);
                }
            }
        }

        private void OnLand(AnimationEvent animationEvent)
        {
            if (animationEvent.animatorClipInfo.weight > 0.5f)
            {
                AudioSource.PlayClipAtPoint(LandingAudioClip, transform.TransformPoint(_controller.center), FootstepAudioVolume);
            }
        }
    }
}