namespace Index_Bislat_Back.Dto
{
    public class ChoisetableDto
    {
        /// <summary>
        /// מחזור מיון
        /// </summary>
        public string Title { get; set; } = null!;
        /// <summary>
        /// ת.ז
        /// </summary>
        public string Id { get; set; } = null!;
        /// <summary>
        /// שם מלא של החייל
        /// </summary>
        public string FullName { get; set; } = null!;
        /// <summary>
        /// מסגרת מיון
        /// </summary>
        public int SortFrame { get; set; }
        /// <summary>
        /// עדיפות ראשונה
        /// </summary>
        public string First { get; set; } = null!;
        /// <summary>
        /// עדיפות שנייה
        /// </summary>
        public string Second { get; set; } = null!;
        /// <summary>
        /// עדיפות שלישית
        /// </summary>
        public string Third { get; set; } = null!;

    }
}
