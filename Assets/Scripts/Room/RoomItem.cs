
public class RoomItem : SellableItem
{
    private void Start()
    {
        TryRender();
    }

    public void TryRender()
    {
        if (IsBuyed)
            gameObject.SetActive(true);
        else
            gameObject.SetActive(false);
    }
}
