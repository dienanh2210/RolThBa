using UnityEngine;
using System.Collections;

public abstract class ProgressBar : MonoBehaviour {

	protected float progress = 0.01f;
	protected float unit = 0.01f;
	
	// Use this for initialization
	void Start () {
		InitProcessAndUnit();
	}

	protected abstract void InitProcessAndUnit();
	

	public void IncreaseProcess(int number) {
		if (progress < 1.0f) {
			progress = progress + number*unit;
			progress = Mathf.Min(progress, 1.0f);

			this.transform.localScale = new Vector3(progress, 
			                                        this.transform.localScale.y, 
			                                        this.transform.localScale.z);
		}
	}

	public void DecreaseProcess(int number) {
		if (progress > 0) {
			progress = progress - number*unit;
			progress = Mathf.Max(0, progress);
			this.transform.localScale = new Vector3(progress, 
			                                        this.transform.localScale.y, 
			                                        this.transform.localScale.z);
		}
	}

	public void SetProgress(int number) {
		progress = number*unit;
		this.transform.localScale = new Vector3(progress,
												this.transform.localScale.y, 
												this.transform.localScale.z);
	}

	public float GetProgress() {
		return progress;
	}

}
