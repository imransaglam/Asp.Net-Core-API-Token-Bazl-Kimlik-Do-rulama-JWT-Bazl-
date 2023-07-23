namespace SharedLibrary.Dtos
{
    //Tüm API'ların kullanacağı hata durumu
    public class ErrorDto
    {
        //Hataların listesi string olarak tutuluyor
        public List<string> Errors { get; private set; }  //sadece bu class üzerinden bu propertyler set edilsin.Dışarıdan bu propertyler set edilmesin
        //Hatanın kullanıcıya gösterilip gösterilmesiği bilgisi tutuluyor
        public bool IsShow { get; private set; }
        public ErrorDto()
        {
            Errors = new List<string>();//Error listesi oluşturma
        }
        public ErrorDto(string error,bool isShow)//bir tane hatayı göstermek istediğimizde
        {
            Errors.Add(error);
            isShow = true;
        }
        public ErrorDto(List<string> errors,bool isShow)//birden fazla hata göstermek istediğimizde
        {
            Errors=errors;
            IsShow = isShow;
        }
    }
}
