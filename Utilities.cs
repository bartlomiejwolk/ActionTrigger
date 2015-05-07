using UnityEngine;

namespace ActionTrigger {

    public enum InfoType {

        Warning,
        Error

    }

    public static class Utilities {

        /// <summary>
        ///     Log info about missing reference.
        /// </summary>
        /// <param name="sourceCo">
        ///     MonoBehaviour from which this method was called.
        /// </param>
        /// <param name="fieldName">
        ///     Name of the field that the reference is missing.
        /// </param>
        /// <param name="detailedInfo">Additional custom information.</param>
        /// <param name="type">
        ///     Type of the Debug.Log() used to output the message.
        /// </param>
        public static void MissingReference(
            MonoBehaviour sourceCo,
            string fieldName,
            string detailedInfo = "",
            InfoType type = InfoType.Error) {

            // Message to display.
            // todo use StringBuilder
            var message = "Component reference is missing in: "
                          + sourceCo.transform.root
                          + " / "
                          + sourceCo.gameObject.name
                          + " '"
                          + sourceCo.GetType()
                          + "'"
                          + " / "
                          + "Field name: " + fieldName
                          + " / "
                          + "Message: " + detailedInfo;

            switch (type) {
                case InfoType.Warning:
                    Debug.LogWarning(message, sourceCo.transform);
                    break;

                case InfoType.Error:
                    Debug.LogError(message, sourceCo.transform);
                    break;
            }
        }

    }

}