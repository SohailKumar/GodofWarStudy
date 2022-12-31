using UnityEngine;

public class HumanoidLandController : MonoBehaviour
{
    public Transform CameraFollow;

    Rigidbody rb;
    
    [SerializeField] HumanoidLandInput input;

    Vector3 playerMoveInput;

    Vector3 playerLookInput;
    Vector3 previousPlayerLookInput;
    float cameraPitch;
    [SerializeField] float playerLookInputLerpTime = 0.35f;

    [Header("Movement")]
    [SerializeField] float movementMultiplier = 30.0f;
    [SerializeField] float rotationSpeedMultiplier = 180.0f;
    [SerializeField] float pitchSpeedMultiplier = 180.0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        playerLookInput = GetLookInput();
        PlayerLook();
        PitchCamera();

        playerMoveInput = GetMoveInput();
        PlayerMove();
        rb.AddRelativeForce(playerMoveInput, ForceMode.Force);
    }

    private Vector3 GetLookInput()
    {
        previousPlayerLookInput = playerLookInput;
        playerLookInput = new Vector3(input.lookInput.x, (input.invertMouseY ? -input.lookInput.y: input.lookInput.y), 0.0f);
        return Vector3.Lerp(previousPlayerLookInput, playerLookInput * Time.deltaTime, playerLookInputLerpTime);
    }

    private void PlayerLook()
    {
        rb.rotation = Quaternion.Euler(0.0f, rb.rotation.eulerAngles.y + (playerLookInput.x * rotationSpeedMultiplier), 0.0f);
    }

    private void PitchCamera()
    {
        cameraPitch += playerLookInput.y * pitchSpeedMultiplier;
        cameraPitch = Mathf.Clamp(cameraPitch, -89.9f, 89.9f);

        CameraFollow.rotation = Quaternion.Euler(cameraPitch, CameraFollow.rotation.eulerAngles.y, CameraFollow.rotation.eulerAngles.z);
    }


    private Vector3 GetMoveInput()
    {
        return new Vector3(input.moveInput.x, 0.0f, input.moveInput.y);
    }

    private void PlayerMove()
    {
        playerMoveInput = (new Vector3(playerMoveInput.x * movementMultiplier,
                                       playerMoveInput.y,
                                       playerMoveInput.z * movementMultiplier));
    }
}
