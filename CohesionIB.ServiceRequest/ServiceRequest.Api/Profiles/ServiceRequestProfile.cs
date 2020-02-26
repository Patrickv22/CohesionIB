namespace ServiceRequest.Api.Profiles
{
    public class ServiceRequestProfile : AutoMapper.Profile
    {
        public ServiceRequestProfile()
        {
            CreateMap<Data.ServiceRequest, Models.ServiceRequest>();
            CreateMap<Data.CurrentStatusEnum, Models.CurrentStatusEnum>();
            CreateMap<Models.ServiceRequest, Data.ServiceRequest>();
            CreateMap<Models.CurrentStatusEnum, Data.CurrentStatusEnum>();
        }
    }
}
