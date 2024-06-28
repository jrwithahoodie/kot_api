using AutoMapper;
using Entities.Entities;
using BusinessLogic.DTO;

namespace BusinessLogic.MappingProfiles
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            #region Teams mapping
            CreateMap<Entities.Entities.Team, TeamRequestResponseDTO>()
                .ForMember(dest => dest.EditionName, opt => opt.MapFrom(src => src.Edition.Name))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.TeamPlayers, opt => opt.Ignore());

            CreateMap<TeamRequestInputDTO, Entities.Entities.Team>()
                .ForMember(dest => dest.Category, opt => opt.Ignore())
                .ForMember(dest => dest.Wins, opt => opt.Ignore())
                .ForMember(dest => dest.Defeats, opt => opt.Ignore())
                .ForMember(dest => dest.Points_diff, opt => opt.Ignore())
                .ForMember(dest => dest.Classification_points, opt => opt.Ignore())
                .ForMember(dest => dest.Group, opt => opt.Ignore())
                .ForMember(dest => dest.Edition, opt => opt.Ignore());

            CreateMap<TeamWithPlayersRequestInputDTO, Entities.Entities.Team>()
                .ForMember(dest => dest.Category, opt => opt.Ignore())
                .ForMember(dest => dest.Wins, opt => opt.Ignore())
                .ForMember(dest => dest.Defeats, opt => opt.Ignore())
                .ForMember(dest => dest.Points_diff, opt => opt.Ignore())
                .ForMember(dest => dest.Classification_points, opt => opt.Ignore())
                .ForMember(dest => dest.Group, opt => opt.Ignore())
                .ForMember(dest => dest.Edition, opt => opt.Ignore());

            CreateMap<TeamUpdateRequestInputDTO, Entities.Entities.Team>()
                .ForMember(dest => dest.Category, opt => opt.Ignore())
                .ForMember(dest => dest.Edition, opt => opt.Ignore())
                .ForMember(dest => dest.Group, opt => opt.Ignore());

            CreateMap<TeamRequestResponseDTO, Entities.Entities.Team>();
            #endregion 
        
            #region Players mapping
            CreateMap<Entities.Entities.Player, PlayerRequestResponseDTO>()
                .ForMember(dest => dest.TeamName, opt => opt.MapFrom(src => src.Team.Name))
                .ForMember(dest => dest.EditionName, opt => opt.MapFrom(src => src.Team.Edition.Name))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Team.Category.Name));
            
            CreateMap<PlayerRequestInputDTO, Entities.Entities.Player>()
                .ForMember(dest => dest.Team, opt => opt.Ignore());
            #endregion
        }
    }
}