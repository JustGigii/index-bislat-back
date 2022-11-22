namespace Index_Bislat_Back.Dto
{
    public class ChoisetableDto
    {
        /// <summary>
        /// מחזור מיון
        /// </summary>
        public string Title { get; set; } = null!;
        public string Gender { get; set; } = null!;
        /// <summary>
        /// ת.ז
        /// </summary>
        public string FullName { get; set; } = null!;
        /// <summary>
        /// מסגרת מיון
        /// </summary>
        public string Id { get; set; } = null!;
        /// <summary>
        /// שם מלא של החייל
        /// </summary>
        public int SortFrame { get; set; }
        /// <summary>
        /// עדיפות ראשונה
        /// </summary>
        public string First { get; set; } = null!;
        public string? Resonef { get; set; }
      /// <summary>
        /// עדיפות שנייה
        /// </summary>
        public string Second { get; set; } = null!;
        public string? Resones { get; set; }
/// <summary>
        /// עדיפות שלישית
        /// </summary>
        public string Third { get; set; } = null!;
        public string? Resonet { get; set; }
}
}
