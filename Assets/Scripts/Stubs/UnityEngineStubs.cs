namespace UnityEngine
{
    public class Font {}

    public enum TextAnchor
    {
        MiddleCenter
    }

    public static class Resources
    {
        public static T GetBuiltinResource<T>(string path) where T : class => null;
    }
}
