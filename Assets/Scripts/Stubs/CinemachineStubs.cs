using UnityEngine;

#if !UNITY_5_3_OR_NEWER
namespace Cinemachine
{
    public class CinemachineVirtualCamera : MonoBehaviour
    {
        public Transform Follow { get; set; }
    }
}
#endif
