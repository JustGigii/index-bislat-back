﻿using index_bislatContext;

namespace Index_Bislat_Back.Interfaces
{
    public interface IAifBase
    {
        ICollection<Aifbase> GetAllBase();

        bool Isexsit(string aifbase);
        bool AddBase(Aifbase aifbase);
        int AddBase(string aifbase);
        int getidofCourse(string aifbase);
        bool RemoveBase(Aifbase aifbase);

        bool UpDateBase(Aifbase aifbase);



    }
}