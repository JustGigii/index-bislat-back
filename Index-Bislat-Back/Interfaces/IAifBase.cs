using index_bislatContext;

namespace Index_Bislat_Back.Interfaces
{
    public interface IAifBase
    {
        Task<ICollection<Aifbase>> GetAllBase();
        Task<Aifbase> GetAifbaseDetails(string aifbase);
        Task<bool> Isexsit(string aifbase);
        Task<bool> AddBase(Aifbase aifbase);
       // Task<int> AddBase(string aifbase);
        Task<int> getidofCourse(string aifbase);
        Task<bool> RemoveBase(Aifbase aifbase);




    }
}
