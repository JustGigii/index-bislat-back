using index_bislatContext;

namespace Index_Bislat_Back.Interfaces
{
    public interface IChoisetable
    {
        Task<ICollection<Choisetable>> GetAllChoise(int sortid);
        Task<Choisetable> GetChoise(string id, int sortid);
        Task<bool> Isexsit(string id, int sortid);
        Task<bool> AddChoise(Choisetable choise);
        Task<bool> Removechoise(Choisetable choise);
        Task<int>  GetSortId(string name);

    }
}
