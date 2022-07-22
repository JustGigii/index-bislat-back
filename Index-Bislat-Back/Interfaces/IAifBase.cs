using index_bislatContext;

namespace Index_Bislat_Back.Interfaces
{
    public interface IAifBase
    {
        ICollection<Aifbase> GetAllBase();


        bool AddBase(Aifbase aifbase);

        bool RemoveBase(Aifbase aifbase);

        bool UpDateBase(Aifbase aifbase);



    }
}
