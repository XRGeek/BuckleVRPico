using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.XR;
public class ScoreManager : MonoBehaviour
{

	public TextMesh scoreText;
	public TextMesh finalScoreText;
	public static int score;
	[SerializeField]
	float fadeSpeed;
	public Image scorePanel;
	public Image finalScorePanel;

	private bool isGameEnded;

	// Use this for initialization
	void Start()
	{
		finalScorePanel.gameObject.SetActive(false);
		StartCoroutine(FadeEffect.FadeIn(scoreText, fadeSpeed));
		StartCoroutine(FadeEffect.FadeIn(scorePanel, fadeSpeed));
		isGameEnded = false;
	}

	// Update is called once per frame
	void Update()
	{
		// ✅ Update score text
		if (scoreText != null)
		{
			scoreText.text = score.ToString();
		}

		// ✅ Check right controller trigger press (for Quest 3S)
		var rightHandDevices = new List<InputDevice>();
		InputDevices.GetDevicesAtXRNode(XRNode.RightHand, rightHandDevices);

		foreach (var device in rightHandDevices)
		{
			if (device.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerPressed) && triggerPressed)
			{
				if (isGameEnded)
				{
					Debug.Log("Restarting");
					SceneCreator.instance.RestartGame();

					isGameEnded = false; // Reset if needed
				}
			}
		}


	}
	public void ShowFinalScore()
	{
		finalScoreText.text = score + "!";
		isGameEnded = true;
	}
	
}
