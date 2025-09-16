using UnityEngine;
using Unity.XR.CoreUtils; // para acceder a XR Origin

[RequireComponent(typeof(CharacterController))]
public class XROriginGravity : MonoBehaviour
{
    public float gravity = -9.81f;
    public float fallSpeed = 0f;

    private CharacterController controller;
    private XROrigin xrOrigin;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        xrOrigin = GetComponent<XROrigin>();
    }

    void Update()
    {
        // Ajustar el CharacterController a la altura real del usuario
        controller.height = xrOrigin.CameraInOriginSpaceHeight;
        controller.center = new Vector3(xrOrigin.CameraInOriginSpacePos.x,
                                        controller.height / 2f,
                                        xrOrigin.CameraInOriginSpacePos.z);

        // Aplicar gravedad
        if (controller.isGrounded && fallSpeed < 0)
            fallSpeed = -2f; // pegado al suelo
        else
            fallSpeed += gravity * Time.deltaTime;

        Vector3 move = new Vector3(0, fallSpeed, 0);
        controller.Move(move * Time.deltaTime);
    }
}
