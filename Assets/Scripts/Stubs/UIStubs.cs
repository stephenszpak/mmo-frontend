using UnityEngine;

#if !UNITY_5_3_OR_NEWER
namespace UnityEngine.UI
{
    public class Canvas : MonoBehaviour
    {
        public RenderMode renderMode;
    }

    public enum RenderMode
    {
        ScreenSpaceOverlay
    }

    public class CanvasScaler : MonoBehaviour { }
    public class GraphicRaycaster : MonoBehaviour { }

    public class Text : MonoBehaviour
    {
        public string text;
        public Font font;
        public TextAnchor alignment;
        public new bool enabled;
    }

    public class Slider : MonoBehaviour
    {
        public float value;
    }
}
#endif
