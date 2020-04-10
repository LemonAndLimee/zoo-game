using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class WorldData
{
    public string[] names;
    public float[] x_positions;
    public float[] y_positions;
    public float[] sizes;

    public WorldData (WorldManagement worldScript)
    {
        names = worldScript.names.ToArray();
        x_positions = worldScript.x_positions.ToArray();
        y_positions = worldScript.y_positions.ToArray();
        sizes = worldScript.sizes.ToArray();
    }
}
