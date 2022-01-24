using UnityEngine;
using System.Collections;

namespace SaveSystem.Core
{
// Placeholder for UniqueIdDrawer script
    public class UniqueIdentifierAttribute : PropertyAttribute
    {
    }

    public class UniqueId : MonoBehaviour
    {
        [UniqueIdentifier] public string uniqueId;
    }
}