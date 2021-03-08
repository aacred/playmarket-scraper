using AutoMapper;
using Domain;
using InputApi.Model;
using InputApi.Model.Dto;

namespace InputApi.Repository.Mapper
{
    public class DataMapper : IDataMapper
    {
        private readonly AutoMapper.Mapper _mapper;

        public DataMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ApplicationDetails, ApplicationDetailsDto>();
            });
            
            _mapper = new AutoMapper.Mapper(config);
        }

        public AutoMapper.Mapper GetMapper()
        {
            return _mapper;
        }
    }
}