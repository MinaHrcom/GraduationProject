namespace HRCom.Domain.BaseTypes
{
    public class EnumLocalizationAttribute : Attribute
    {
        internal EnumLocalizationAttribute(string nameEn, string nameAr)
        {
            NameEn = nameEn;
            NameAr = nameAr;
        }
        public string NameEn { get; private set; }
        public string NameAr { get; private set; }
    }
}
