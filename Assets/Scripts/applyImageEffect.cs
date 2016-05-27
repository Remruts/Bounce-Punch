using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class applyImageEffect : MonoBehaviour {

	public Material EffectMaterial;

	void OnRenderImage(RenderTexture src, RenderTexture dst){
		Graphics.Blit (src, dst, EffectMaterial);
	}

	public void setCutoff(float c){
		EffectMaterial.SetFloat ("_Cutoff", c);
	}

	public void setTranslationSpeed(float spdX, float spdY){
		EffectMaterial.SetVector ("_Translation", new Vector4 (spdX, spdY, 0));
	}
}
