using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[ExecuteInEditMode]
public class transitionScript : MonoBehaviour {

	public Material EffectMaterial;
	public bool invert = false;
	public bool transitioning = false;
	public float transitionTime = 3f;

	public Texture[] transitionMasks;
	public static transitionScript transition;
	public string level = "";

	float cutoff = 0f;
	float translationFactor = 0f;

	void Awake(){
		transition = this;
	}

	void Start(){
		uint index = (uint) Mathf.Floor(Random.Range (0, transitionMasks.Length));
		EffectMaterial.SetTexture ("_MaskTex", transitionMasks [index]);
		cutoff = invert ? 1 : 0;
		EffectMaterial.SetFloat ("_Cutoff", cutoff);
	}

	void Update(){
		if (transitioning) {
			if (invert) {
				if (cutoff > 0) {
					cutoff -= Time.deltaTime / (transitionTime * Time.timeScale);
					if (cutoff < 0) {
						cutoff = 0f;
					}
				}
			} else {
				if (cutoff < 1) {
					cutoff += Time.deltaTime / (transitionTime * Time.timeScale);
					if (cutoff > 1) {
						cutoff = 1f;

						if (level != ""){
							SceneManager.LoadScene (level);
						}
					}
				}
			}

			EffectMaterial.SetFloat ("_Cutoff", cutoff);

			translationFactor += Time.fixedDeltaTime;
			setTranslationSpeed (translationFactor, translationFactor);
		}

	}

	void OnRenderImage(RenderTexture src, RenderTexture dst){
		Graphics.Blit (src, dst, EffectMaterial);
	}

	public void setCutoff(float c){
		EffectMaterial.SetFloat ("_Cutoff", c);
	}

	public void startTransition(float time){
		transitioning = true;
		transitionTime = time;
	}

	public void setTranslationSpeed(float spdX, float spdY){
		EffectMaterial.SetVector ("_Translation", new Vector4 (spdX, spdY, 0));
	}
}
