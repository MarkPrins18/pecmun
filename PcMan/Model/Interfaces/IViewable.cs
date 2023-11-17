namespace PcMan.Model.Interfaces
{
    public interface IViewable
    {
        public int GetTop();
        public int GetLeft();

        public string GetImage();

        public ConsoleColor GetColor();
        
    }
}