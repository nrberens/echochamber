using System.Collections;
using UnityEngine;
using DentedPixel;

public class Floor: MonoBehaviour {

	public Material glowMat;
	public Material currentMat;
	public Color opaqueColor;
	public Color fadeColor;
	MeshRenderer meshRenderer;
	public bool fading;
	public float alphaPercent;
	public float fadeSpeed;

	// Use this for initialization
	void Start () {
		meshRenderer = GetComponent<MeshRenderer>();
		fading = false;
		FadeMat(0f);	
	}
	
	// Update is called once per frame
	void Update () {
		if(fading) {
			if(alphaPercent > 0f) {
				alphaPercent -= fadeSpeed * Time.deltaTime;
				Color lerpingColor = Color.Lerp(fadeColor, opaqueColor, alphaPercent);
				Material[] mats = meshRenderer.materials;
				currentMat = mats[0];
				currentMat.color = lerpingColor;
				mats[0] = currentMat;
				meshRenderer.materials = mats;
			} else {
				fading = false;
				alphaPercent = 1.0f;
			}
		}
		
	}

	public void Nudge(Vector3 dest, float distance) {
		//use distance to determine delay
		LeanTween.move(gameObject, dest, 0.3f).setEase(LeanTweenType.easeInOutQuad).setLoopPingPong(1).setDelay(distance*0.05f);
	}

	public void GlowMat(float distance) {
		Material[] mats = meshRenderer.materials;
		mats[0] = glowMat;
		Color opaqueColor = mats[0].color;
		opaqueColor.a = 255f;
		meshRenderer.materials = mats;
		LeanTween.color(gameObject, opaqueColor, 1.5f).setDelay(distance*0.3f);
	}
	// public void FadeMat(float distance) {
	// 	Material[] mats = meshRenderer.materials;
	// 	Color initColor = glowMat.color;
	// 	initColor.a = 1 / distance;
	// 	glowMat.SetColor("Albedo", initColor);
	// 	mats[0] = glowMat;
	// 	Color fadeColor = mats[0].color;
	// 	fadeColor.a = 0f;
	// 	meshRenderer.materials = mats;
	// 	LeanTween.color(gameObject, fadeColor, 1.5f).setDelay(distance*0.3f);
	// }

	public void FadeMat(float distance) {
		alphaPercent = 1.0f;
		fading = true;
	}
}
