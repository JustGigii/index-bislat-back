using System;
using System.Collections.Generic;

namespace index_bislatContext
{
    public partial class Choisetable
    {
        public int Callid { get; set; }
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
        public int First { get; set; }
        /// <summary>
        /// עדיפות שנייה
        /// </summary>
        public int Second { get; set; }
        /// <summary>
        /// עדיפות שלישית
        /// </summary>
        public int Third { get; set; }
    }
}
