using UnityEngine;

public class ArchCenter : MonoBehaviour
{
    private const string ARCH_CENTER = "ArchCenter";

    [SerializeField] private float animSpeed = 0.3f;

    private Animation m_animation;

    private void Awake()
    {
        m_animation = GetComponent<Animation>();
    }

    private void Start()
    {
        m_animation[ARCH_CENTER].speed = animSpeed;
    }
}
