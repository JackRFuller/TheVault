﻿using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class PlayerScanHandler : MonoBehaviour
{
	private Transform ScannerOrigin;
	public Material EffectMaterial;
	private float ScanDistance;
    [SerializeField]
    private float maxScanDistance;
	private Camera _camera;

    private SwitchableGeometry[] geometry;
    private int scanCount = 0;
	
	bool _scanning;
    //Scannable[] _scannables;

    void OnEnable()
    {
        _camera = GetComponent<Camera>();
        _camera.depthTextureMode = DepthTextureMode.Depth;
    }

    void Start()
	{
        ScannerOrigin = this.transform;
        geometry = FindObjectsOfType<SwitchableGeometry>();
    }

	void Update()
	{
		if (_scanning)
		{
            if(ScanDistance < maxScanDistance)
            {
                ScanDistance += Time.deltaTime * 50;
                for (int i = 0; i < geometry.Length; i++)
                {
                    if (Vector3.Distance(ScannerOrigin.position, geometry[i].transform.position) <= ScanDistance)
                    {
                        geometry[i].ToggleGeometry(scanCount);
                    }
                      
                }
            }
            else
            {
                _scanning = false;
                ScanDistance = 0;
            }
			
		}		

		if (Input.GetMouseButtonDown(0))
		{
            scanCount++;
			//ScannerOrigin.position = transform.position;
            _scanning = true;
            ScanDistance = 0;

			//if (Physics.Raycast(ray, out hit))
			//{
			//	_scanning = true;
			//	ScanDistance = 0;
			//	ScannerOrigin.position = hit.point;
			//}
		}
	}
	// End Demo Code

	

	[ImageEffectOpaque]
	void OnRenderImage(RenderTexture src, RenderTexture dst)
	{
		EffectMaterial.SetVector("_WorldSpaceScannerPos", ScannerOrigin.position);
		EffectMaterial.SetFloat("_ScanDistance", ScanDistance);
		RaycastCornerBlit(src, dst, EffectMaterial);
	}

	void RaycastCornerBlit(RenderTexture source, RenderTexture dest, Material mat)
	{
		// Compute Frustum Corners
		float camFar = _camera.farClipPlane;
		float camFov = _camera.fieldOfView;
		float camAspect = _camera.aspect;

		float fovWHalf = camFov * 0.5f;

		Vector3 toRight = _camera.transform.right * Mathf.Tan(fovWHalf * Mathf.Deg2Rad) * camAspect;
		Vector3 toTop = _camera.transform.up * Mathf.Tan(fovWHalf * Mathf.Deg2Rad);

		Vector3 topLeft = (_camera.transform.forward - toRight + toTop);
		float camScale = topLeft.magnitude * camFar;

		topLeft.Normalize();
		topLeft *= camScale;

		Vector3 topRight = (_camera.transform.forward + toRight + toTop);
		topRight.Normalize();
		topRight *= camScale;

		Vector3 bottomRight = (_camera.transform.forward + toRight - toTop);
		bottomRight.Normalize();
		bottomRight *= camScale;

		Vector3 bottomLeft = (_camera.transform.forward - toRight - toTop);
		bottomLeft.Normalize();
		bottomLeft *= camScale;

		// Custom Blit, encoding Frustum Corners as additional Texture Coordinates
		RenderTexture.active = dest;

		mat.SetTexture("_MainTex", source);

		GL.PushMatrix();
		GL.LoadOrtho();

		mat.SetPass(0);

		GL.Begin(GL.QUADS);

		GL.MultiTexCoord2(0, 0.0f, 0.0f);
		GL.MultiTexCoord(1, bottomLeft);
		GL.Vertex3(0.0f, 0.0f, 0.0f);

		GL.MultiTexCoord2(0, 1.0f, 0.0f);
		GL.MultiTexCoord(1, bottomRight);
		GL.Vertex3(1.0f, 0.0f, 0.0f);

		GL.MultiTexCoord2(0, 1.0f, 1.0f);
		GL.MultiTexCoord(1, topRight);
		GL.Vertex3(1.0f, 1.0f, 0.0f);

		GL.MultiTexCoord2(0, 0.0f, 1.0f);
		GL.MultiTexCoord(1, topLeft);
		GL.Vertex3(0.0f, 1.0f, 0.0f);

		GL.End();
		GL.PopMatrix();
	}
}