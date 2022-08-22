using Index_Bislat_Back.Dto;

namespace Index_Bislat_Back.Helper
{
    public interface IClaimService 
    {
        public string GetJson();
        public string CreateToken(object course);
        public string CreateToken(string name);
        public bool CheakCorretjwt(string jwt, string body);

    }
}
