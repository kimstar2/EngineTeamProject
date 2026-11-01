namespace Members.KJY.Scripts.Utils
{
    public interface IToggleable
    {
        bool Toggled { get; }
        void Toggle();
    }
}