namespace HelloWorld.Models
{
    public class MDEntry
    {
        public string BackColor { get; set; }

        /// <summary>
        /// Источник
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// Цена
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Объем
        /// </summary>
        public string Volume { get; set; }


        private EntryType currentType;
        /// <summary>
        /// Направление
        /// </summary>
        public EntryType Type { get { return currentType; } set { currentType = value; CheckColor(); } }

        public string EntryTypeString => Type.ToString();
        public MDEntry()
        {
            CheckColor();
        }

        private void CheckColor()
        {
            if (Type == EntryType.Bid)
                BackColor = "#57F358";
            else
                BackColor = "#CF433C";
        }
    }

    public enum EntryType
    {
        Bid,
        Offer
    }
}
