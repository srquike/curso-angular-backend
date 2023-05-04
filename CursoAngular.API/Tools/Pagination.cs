namespace CursoAngular.API.Tools
{
    public class Pagination
    {
        private int itemsToDisplay = 10;
        private int MaxItemsToDisplay { get; } = 50;

        public int PageNumber { get; set; } = 1;
        public int ItemsToDisplay { get => itemsToDisplay; set => itemsToDisplay = (value > MaxItemsToDisplay) ? MaxItemsToDisplay : value; }

        public int GetSkipCount()
        {
            return (PageNumber - 1) * ItemsToDisplay;
        }
    }
}
