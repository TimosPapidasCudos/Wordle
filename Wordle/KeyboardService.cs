public class KeyboardService
{
    public event Action<char>? OnKeyPressed;

    public void KeyPressed(char key)
    {
        OnKeyPressed?.Invoke(key);
    }
}
