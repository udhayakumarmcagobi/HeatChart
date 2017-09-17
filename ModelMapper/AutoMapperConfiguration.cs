using AutoMapper;
using ModelMapper.DomainToViewModel;
using ModelMapper.ViewModelToDataset;
using ModelMapper.ViewModelToDomain;

namespace ModelMapper
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(x => 
            {
                x.AddProfile<DomainToViewModelMappingProfile>();
                x.AddProfile<ViewModelToDomainMappingProfile>();
                x.AddProfile<ViewModelToDatasetMappingProfile>();
            });
        }
    }
}
