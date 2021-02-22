using UnityEngine;

[RequireComponent(typeof(Animation))]
public class BadPlatformPart : PlatformPart
{
    [SerializeField] private Material _crackedMateial;

    private Animation _badAnimation;

    private void Start()
    {
        _badAnimation = GetComponent<Animation>();
    }

    protected override void Collision()
    {
        if (--Durable <= 0)
            Platform.GameOver();

        _badAnimation.Play();
        Platform.PlayAudio(false);

        SetMaterial(_crackedMateial);
    }
}
