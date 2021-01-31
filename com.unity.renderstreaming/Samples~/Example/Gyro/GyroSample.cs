using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using Gyroscope = UnityEngine.InputSystem.Gyroscope;

namespace Unity.RenderStreaming
{
    public class GyroSample : MonoBehaviour
    {
#pragma warning disable 0649
            [SerializeField] private Button sendOfferButton;
            [SerializeField] private RawImage remoteVideoImage;
            [SerializeField] private ReceiveVideoViewer receiveVideoViewer;
            [SerializeField] private SingleConnection connection;
            [SerializeField] private Text textVelocityX;
            [SerializeField] private Text textVelocityY;
            [SerializeField] private Text textVelocityZ;
            [SerializeField] private InputAction vector3Action;
#pragma warning restore 0649

        void Awake()
        {
            if(Gyroscope.current != null)
                InputSystem.EnableDevice(Gyroscope.current);
            else
                Debug.LogError("Gyroscope is not supported on this device.");
            sendOfferButton.onClick.AddListener(SendOffer);
            receiveVideoViewer.OnUpdateReceiveTexture += texture => remoteVideoImage.texture = texture;
        }


        void OnEnable()
        {
            vector3Action.Enable();
            vector3Action.performed += UpdateVector3;
            vector3Action.started += UpdateVector3;
            vector3Action.canceled += UpdateVector3;
        }

        void OnDisable()
        {
            vector3Action.Disable();
            vector3Action.performed -= UpdateVector3;
            vector3Action.started -= UpdateVector3;
            vector3Action.canceled -= UpdateVector3;
        }

        private void UpdateVector3(InputAction.CallbackContext context)
        {
            if (context.control is Vector3Control control)
            {
                Vector3 value = control.ReadValue();
                textVelocityX.text = value.x.ToString("f2");
                textVelocityX.text = value.y.ToString("f2");
                textVelocityX.text = value.z.ToString("f2");
            }
        }

        void SendOffer()
        {
            var connectionId = System.Guid.NewGuid().ToString("N");
            connection.CreateConnection(connectionId, true);
        }
    }
}
