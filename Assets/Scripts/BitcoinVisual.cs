using UnityEngine;

public class BitcoinVisual : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private float animationSpeed = 1f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        animator.speed = animationSpeed;
    }
}
