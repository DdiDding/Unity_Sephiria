using UnityEngine;

public class AnimationTest : MonoBehaviour
{
    Animator animator;
    AnimationClip[] clips;
    int currentIndex = 0;

    void Start()
    {
        animator = GetComponent<Animator>();
        RuntimeAnimatorController controller = animator.runtimeAnimatorController;

        // Animator Controller에 있는 모든 AnimationClip 가져오기
        clips = controller.animationClips;
        if (clips.Length > 0)
        {
            animator.Play(clips[0].name); // 첫 애니메이션 재생
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentIndex++;
            if (currentIndex >= clips.Length)
                currentIndex = 0;

            animator.Play(clips[currentIndex].name);
            Debug.Log("Playing: " + clips[currentIndex].name);
        }
    }
}