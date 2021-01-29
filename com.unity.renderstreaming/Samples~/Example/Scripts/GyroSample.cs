using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

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

        void Update()
        {
            var _value = Gyroscope.current?.angularVelocity.ReadValue();
            var value = _value.GetValueOrDefault();
            textVelocityX.text = value.x.ToString("f2");
            textVelocityY.text = value.y.ToString("f2");
            textVelocityZ.text = value.z.ToString("f2");
        }

        void SendOffer()
        {
            var connectionId = System.Guid.NewGuid().ToString("N");
            connection.CreateConnection(connectionId, true);
        }
    }
}
