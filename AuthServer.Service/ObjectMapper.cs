using AutoMapper;

namespace AuthServer.Service
{
    public static class ObjectMapper
    {
        //Lazy loading: ihtiyaç olduğunda kullansın çünkü uygulama ayağa kalktığında objectmapper içerisindeki data memorye biz istediğimiz zaman yüklensin
      //uygulama ayağa kalktığında memoryde olmayacak
        private static readonly Lazy<IMapper> lazy = new Lazy<IMapper>(() =>
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<DtoMapper>();
                });
                return config.CreateMapper();
            });
        public static IMapper Mapper => lazy.Value;
       
    }
}
