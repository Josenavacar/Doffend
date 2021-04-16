using UnityEngine;

namespace Utility
{
    /// <summary>
    /// SingletonObject yoinked from jasper because i can't be bothered
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SingletonObject<T> : MonoBehaviour where T : MonoBehaviour
    {

        //-----------------------------------------------------------------------------
        // Variables
        //-----------------------------------------------------------------------------

        #region Variables

        protected static T instance;

        #endregion Variables

        //-----------------------------------------------------------------------------
        // Properties
        //-----------------------------------------------------------------------------

        #region Properties

        /// <summary>
        /// Return instance of this class.
        /// </summary>
        public static T Instance {

            get {

                // Check if instance does not exist
                if (instance == null)
                {

                    // Find instace
                    instance = (T)FindObjectOfType(typeof(T));

                    // If still not found
                    if (instance == null)
                    {

                        Debug.LogError("An instance of " + typeof(T) + " is needed in the scene, but there is none.");
                    }
                }

                return instance;
            }
        }

        #endregion Properties

        //-----------------------------------------------------------------------------
        // Functions
        //-----------------------------------------------------------------------------

        #region Functions

        /// <summary>
        /// Set the instance to input instance.
        /// </summary>
        public static void SetInstance(T i)
        {

            // Destroy if instance already exists
            if (instance != null && instance != i)
            {

                Destroy(i.gameObject);
            }

            // Set instance
            else
            {

                instance = i;
                DontDestroyOnLoad(i.gameObject);
            }
        }

        #endregion Functions
    }
}
