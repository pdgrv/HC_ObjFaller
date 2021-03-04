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
            PlatformEventsHandler.RaiseBadPlatformDestroyed();

        PlatformEventsHandler.RaiseBadPlatformHit();

        _badAnimation.Play();
        SetMaterial(_crackedMateial);
    }
}
