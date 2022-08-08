﻿using System;
using System.Collections.Generic;

namespace index_bislatContext
{
    public partial class Choisetable
    {
        public int Callid { get; set; }
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
        public int? Sortid { get; set; }

        public virtual SortCycle? Sort { get; set; }
    }
}
