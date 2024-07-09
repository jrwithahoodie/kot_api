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

            CreateMap<PlayerInTeamRequestInputDTO, Entities.Entities.Player>()
                .ForMember(dest => dest.Team, opt => opt.Ignore());
            #endregion

            #region Game mapping
            CreateMap<Entities.Entities.Game, GameInfoRequestResponseDTO>()
                .ForMember(dest => dest.Team1, opt => opt.MapFrom(src => src.Team1))
                .ForMember(dest => dest.Team1Score, opt => opt.MapFrom(src => src.Score1))
                .ForMember(dest => dest.Team2, opt => opt.MapFrom(src => src.Team2))
                .ForMember(dest => dest.Team2Score, opt => opt.MapFrom(src => src.Score2))
                .ForMember(dest => dest.Court, opt => opt.MapFrom(src => src.Court));
            
            CreateMap<NewGameRequestDTO, Entities.Entities.Game>()
                .ForMember(dest => dest.Team1Id, opt => opt.Ignore())
                .ForMember(dest => dest.Team2Id, opt => opt.Ignore())
                .ForMember(dest => dest.Score1, opt => opt.Ignore())
                .ForMember(dest => dest.Score2, opt => opt.Ignore())
                .ForMember(dest => dest.Score1Old, opt => opt.Ignore())
                .ForMember(dest => dest.Score2Old, opt => opt.Ignore())
                .ForMember(dest => dest.Schedule, opt => opt.Ignore())
                .ForMember(dest => dest.StaffId, opt => opt.Ignore());
            #endregion
        }
    }
}