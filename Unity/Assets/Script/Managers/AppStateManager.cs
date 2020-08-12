using UnityEngine;


public class AppStateManager : MonoBehaviour
{
    [System.NonSerialized]
    public Pose placementCursorPose; // used as a way to expose the placement and position of the spot where they raycast touches a surface

    [System.NonSerialized]
    public bool placementCursorIsSurface = false; // used to flag whether the raycast is hitting at least one surface
}
