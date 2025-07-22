using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class MenuRay : MonoBehaviour
{
	[SerializeField] private Transform m_End;
	[SerializeField] private float m_Damping = 0.5f;
	private const float k_DampingCoef = -20f;
	[SerializeField] private LineRenderer m_Flare;
	[SerializeField] private float m_DefaultLineLength = 70f;
	[SerializeField] Material defaultLaser;
	[SerializeField] Material selectLaser;

	bool isCharacterLocked;
	private bool isDeafaultLaser;
	RaycastHit hitInfo;
	Ray ray;
	GameObject currentRider;

	private bool previousTriggerState = false;

	void Start()
	{
		m_End.GetComponent<LineRenderer>().enabled = false;
		isCharacterLocked = false;
		isDeafaultLaser = true;
		float lineLength = m_DefaultLineLength;
		m_Flare.SetPosition(0, m_End.position);
		m_Flare.SetPosition(1, m_End.position + m_End.forward * lineLength);
	}

	void Update()
	{

		float lineLength = m_DefaultLineLength;
		m_End.GetComponent<LineRenderer>().SetPosition(0, m_End.position);
		m_End.GetComponent<LineRenderer>().SetPosition(1, m_End.position + m_End.forward * lineLength);

		m_Flare.SetPosition(0, m_End.position);
		m_Flare.SetPosition(1, m_End.position + m_End.forward * lineLength);

		ray = GetRay();
		if (Physics.Raycast(ray, out hitInfo))
		{

			if (hitInfo.collider.CompareTag("SelectBG") || hitInfo.collider.CompareTag("click1") || hitInfo.collider.CompareTag("click2") || hitInfo.collider.CompareTag("click3"))
			{
				Debug.Log("Testing");

				if (!isCharacterLocked)
				{
					if (Input.GetMouseButton(0))
					{
						CharacterselectedLaser();
						isCharacterLocked = true;
					}
					if (IsTriggerHeld())
					{
						CharacterselectedLaser();
						isCharacterLocked = true;
					}
				}

				currentRider = hitInfo.transform.parent.gameObject.transform.parent.gameObject;

				if (Input.GetMouseButton(0))
				{
					if (hitInfo.collider.tag == "click1")
						currentRider.GetComponent<Spawner>().EnableClick1();

					if (hitInfo.collider.tag == "click2" && currentRider.GetComponent<Spawner>().CheackClcik1())
						currentRider.GetComponent<Spawner>().EnableClick2();

					if (hitInfo.collider.tag == "click3" && currentRider.GetComponent<Spawner>().CheackClcik2())
					{
						currentRider.GetComponent<Spawner>().EnableClick3();
						MakeLaserNoCharacterSelected();
					}
				}

				if (IsTriggerHeld())
				{
					if (hitInfo.collider.tag == "click1")
						currentRider.GetComponent<Spawner>().EnableClick1();

					if (hitInfo.collider.tag == "click2" && currentRider.GetComponent<Spawner>().CheackClcik1())
						currentRider.GetComponent<Spawner>().EnableClick2();

					if (hitInfo.collider.tag == "click3" && currentRider.GetComponent<Spawner>().CheackClcik2())
					{
						currentRider.GetComponent<Spawner>().EnableClick3();
						MakeLaserNoCharacterSelected();
					}
				}
			}
			else
			{
				MakeLaserNoCharacterSelected();

				if (isDeafaultLaser)
				{
					isDeafaultLaser = false;
					setDefaultLaser();
				}
				if (currentRider != null)
				{
					currentRider.GetComponent<Spawner>().UnSelectAll();
				}
			}

			if (hitInfo.collider.tag == "Start")
			{
				if (!isDeafaultLaser)
				{
					selectedLaser();
					isDeafaultLaser = true;
				}

				if (IsTriggerPressed())
				{
					MenuManager.instance.StartGame();
				}

			}
			else if (hitInfo.collider.tag == "Begin")
			{
				if (!isDeafaultLaser)
				{
					selectedLaser();
					isDeafaultLaser = true;
				}

				if (IsTriggerPressed())
				{
					MenuManager.instance.Begin();
				}

			}
			else
			{
				setDefaultLaser();
			}

		}
		else
		{
			if (currentRider != null)
			{
				currentRider.GetComponent<Spawner>().UnSelectAll();
			}
			MakeLaserNoCharacterSelected();
			currentRider = null;
			if (isDeafaultLaser)
			{
				isDeafaultLaser = false;
				setDefaultLaser();
			}
		}
	}

	public void MakeLaserNoCharacterSelected()
	{
		isCharacterLocked = false;
		m_Flare.enabled = true;
		m_End.GetComponent<LineRenderer>().enabled = false;
	}

	public Ray GetRay()
	{
		return new Ray(m_End.position, m_End.forward);
	}

	void setDefaultLaser()
	{
		Debug.Log("Default Laser");
		m_Flare.material = defaultLaser;
		m_Flare.widthMultiplier = 0.3f;
	}

	void selectedLaser()
	{
		Debug.Log("Selected Laser");
		m_Flare.widthMultiplier = 0.8f;
		m_Flare.material = selectLaser;
	}

	public void CharacterselectedLaser()
	{
		Debug.Log("Character Selected Laser");
		m_Flare.enabled = false;
		m_End.GetComponent<LineRenderer>().enabled = true;
	}

	private bool IsTriggerHeld()
	{
		InputDevice device = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
		if (device.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerHeld))
			return triggerHeld;
		return false;
	}

	private bool IsTriggerPressed()
	{
		InputDevice device = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
		if (device.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerPressed))
		{
			bool wasPressed = !previousTriggerState && triggerPressed;
			previousTriggerState = triggerPressed;
			return wasPressed;
		}
		return false;
	}

	IEnumerator RestartGame()
	{
		Debug.Log("Waiting for 2 seconds...");
		yield return new WaitForSeconds(2f); // Delay for 2 seconds
		Debug.Log("2 seconds passed!");
		
    }
}
