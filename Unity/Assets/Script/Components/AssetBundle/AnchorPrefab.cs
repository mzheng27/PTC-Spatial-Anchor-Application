using System;
using UnityEngine;

[Serializable]
public class AnchorPrefab
{
	public string filepath_audio;
	public string filepath_image;
	public string filepath_text;
	public string color;
	public string location;
	public string rotation;
	public string priority;

	[NonSerialized]
	public Vector3 Position;

	[NonSerialized]
	public Color Colour;

	[NonSerialized]
	public Quaternion Rotation;

	

	public void Init ()
	{
		string[] loc = location.Split (',');
		if (loc.Length != 3) {
			Position = Vector3.zero;
		} else {
			Position = new Vector3 (float.Parse (loc [0]), float.Parse (loc [1]), float.Parse (loc [2]));
		}

		string[] rot = rotation.Split(',');
		if (rot.Length != 3)
		{
			Rotation.x = 0;
			Rotation.y = 0;
			Rotation.z = 0;
		}
		else
		{
			Rotation.x = float.Parse(rot[0]);
			Rotation.y = float.Parse(rot[1]);
			Rotation.z = float.Parse(rot[2]);
		}

		Colour = Color.clear;
		if (!string.IsNullOrEmpty (color)) {
			ColorUtility.TryParseHtmlString (color, out Colour);
		}
	}
}
