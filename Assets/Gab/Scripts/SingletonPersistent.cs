using UnityEngine;

/// <summary>
/// You can derive from this class to make an object that:
/// 1. will keep only one instance
/// 2. you can access through ClassName.Instance
/// 3. will persist via DontDestroyOnLoad
/// 4. will destroy self on awake if there is already an instance (see 1)
/// </summary>
/// <example>
/// // Derive like this:
/// public class YourClassName : SingletonPersistent<YourClassName> {
/// // Override Awake like this:
///     protected override void Awake() {
///         base.Awake();
///         //additional code
///     }
/// }
/// </example>
/// <typeparam name="T"></typeparam>
namespace Gab.Scripts
{
    public abstract class SingletonPersistent<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = GameObject.FindObjectOfType<T>();
                    if (_instance == null)
                    {
                        _instance = new GameObject(typeof(T) + " (singleton)").AddComponent<T>();
                    }
                }

                return _instance;
            }
        }

        /// <summary>
        /// If there is already an instance when this one is attempting
        /// to Awake, destroy self!
        /// Keep this object persistent with DontDestroyOnLoad
        /// </summary>
        protected virtual void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                DontDestroyOnLoad(gameObject);
            }
        }
    }
}