using UnityEngine;

public class BlackSphereController : MonoBehaviour
{

    [HideInInspector]
    public Animator animator;
    public static BlackSphereController Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Prevent duplicate singletons
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Optional: persists across scenes
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.Play("Fadeout");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
