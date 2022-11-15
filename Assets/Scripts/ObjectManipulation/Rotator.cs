using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public enum RotationAxis
	{
		All,
		Y,
		X,
		Z
	}

    
	public RotationAxis axis;
	public float speedRot = 0.3f;

    // Update is called once per frame
    void Update()
    {
		float rot = Time.deltaTime * speedRot;

		switch( axis )
		{
			case RotationAxis.All:
				transform.Rotate( new Vector3( rot, rot, rot ) );
				break;

			case RotationAxis.X:
				transform.Rotate( new Vector3( rot, 0f, 0f ) );
				break;

			case RotationAxis.Y:
				transform.Rotate( new Vector3( 0f, rot, 0f ) );
				break;

			case RotationAxis.Z:
				transform.Rotate( new Vector3( 0f, 0f, rot ) );
				break;
            default:
                break;
		}
    }
}
