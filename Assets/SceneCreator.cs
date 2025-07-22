using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneCreator : MonoBehaviour
{
    public GameObject VRMenu;
    public GameObject GamePlay;
    public GameObject roof;


    private GameObject m_VRMenu;
    private GameObject m_GamePlay;

    public static SceneCreator instance;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            MenuManager.instance.StartGame();
       
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            MenuManager.instance.Begin();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        SetActiveMenu(true);
    }

    public void SetActiveMenu(bool status)
    {
        if (status)
        {
            if (m_VRMenu != null)
            {
                return;
            }
            m_VRMenu = Instantiate(VRMenu);
        }
        else
        {
            if (m_VRMenu != null)
            {
                Destroy(m_VRMenu);
            }
            
        }
    }

    public void SetActiveGamePlay(bool status)
    {
        if (status)
        {
            if (m_GamePlay != null)
            {
                return;
            }
            m_GamePlay = Instantiate(GamePlay);
        }
        else
        {
            if (m_GamePlay != null)
            {
                Destroy(m_GamePlay);
            }

        }
    }



    public void RestartGame()
    {
        BlackSphereController.Instance.animator.Play("FadeIn");
		StartCoroutine(RestartGameDelay());
    }

    IEnumerator RestartGameDelay()
	{
		Debug.Log("Waiting for 2 seconds...");
		yield return new WaitForSeconds(4f); // Delay for 2 seconds
        BlackSphereController.Instance.animator.Play("Fadeout");
		SceneCreator.instance.SetActiveMenu(true);
		SceneCreator.instance.roof.SetActive(true);
		SceneCreator.instance.SetActiveGamePlay(false);
		Debug.Log("2 seconds passed!");
		
    }
}
