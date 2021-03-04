using UnityEngine;
using UnityEngine.Events;

public class PlatformEventsHandler : MonoBehaviour
{
    public static event UnityAction<Platform> PlatformDestoyed;
    public static event UnityAction BadPlatformHit;
    public static event UnityAction BadPlatformDestroyed;

    public static void RaisePlatformDestroyed(Platform platform) => PlatformDestoyed?.Invoke(platform);
    public static void RaiseBadPlatformHit() => BadPlatformHit?.Invoke();
    public static void RaiseBadPlatformDestroyed() => BadPlatformDestroyed?.Invoke();
}
