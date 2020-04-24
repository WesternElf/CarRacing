using UnityEngine;

namespace Extensions
{
    public static class ExtensionMethods
    {
        public static void RemoveCloneFromName(this GameObject gameObject)
        {
            gameObject.name = gameObject.name.Replace("(Clone)", string.Empty);
        }
    }
}
