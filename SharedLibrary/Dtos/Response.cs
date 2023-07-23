using System.Text.Json.Serialization;

namespace SharedLibrary.Dtos
{

    //Endpointe istek yapıldığında başarılı ya da başarısız dönüceğimiz model
    public class Response<T> where T : class
    {
        public T Data { get;private set; }//veri döndürmek
        public int StatusCode { get; private set; }//durum kodu döndürmek
        public ErrorDto Errors { get; private set; }//Hata döndürmek

        //JsonIgnore:Serilize edilmesini engellemek.Response sınıfı serilize edilirken bir json dataya dönüştüğünde IsSuccesful propertysi görmezden gelinecektir
        [JsonIgnore]
        public bool IsSuccessful { get; private set; }//Clientlar görmesin.Biz kendi iç yapımızda kullanıcaz.Responsların başarılı olup oladığını yazılımcı görmesi için
        
        public static Response<T> Success(T data,int statusCode) //istek başarılı olduğunda cevabında veri ve başarı durum kodu olsun örneğin ekleme işleminde
        { 
           return new Response<T> { Data = data, StatusCode = statusCode, IsSuccessful=true };
        }
        public static Response<T> Success(int statusCode) //istek başarılı olduğunda cevabında veri göstermesin ve başarı durum kodu(204 no content) olsun örneğin silme ve güncelleme işleminde
        {
            return new Response<T> { Data = default, StatusCode = statusCode, IsSuccessful = true };
        }

        public static Response<T> Fail(ErrorDto errors, int statusCode)//istek başarısızsa cevaben birden çok hata mesajı ve durum kodu döndürür
        {
            return new Response<T> { Errors = errors,StatusCode=statusCode, IsSuccessful = false };
        }
        public static Response<T> Fail(string errorMessage, int statusCode,bool isShow)//istek başarısızsa cevaben tek hata mesajı ve durum kodu döndürür
        {
            var errorDto = new ErrorDto(errorMessage, isShow);
            return new Response<T> { Errors = errorDto, StatusCode = statusCode , IsSuccessful = false };
        }
    }
}
