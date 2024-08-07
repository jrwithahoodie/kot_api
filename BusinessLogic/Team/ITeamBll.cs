﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.DTO;

namespace BusinessLogic.Team
{
    public interface ITeamBll
    {
        IEnumerable<TeamRequestResponseDTO> Get();
        TeamRequestResponseDTO GetByName(string name);
        IEnumerable<AllGroupsClassificationResponseDTO> GetAllClassif(string editionName);
        IEnumerable<TeamRequestResponseDTO> GetClassif(string groupName);
        IEnumerable<TeamRequestResponseDTO> GetByGroup(string groupName);
        IEnumerable<TeamRequestResponseDTO> GetByEdition(string edition);
        IEnumerable<TeamRequestResponseDTO> GetByCategory(string category);
        TeamRequestResponseDTO Post(TeamRequestInputDTO newTeamData);
        TeamRequestResponseDTO PostWithPlayers(TeamWithPlayersRequestInputDTO newTeamWithPlayersData);
        TeamRequestResponseDTO UpdateTeamInfo(TeamUpdateRequestInputDTO updateTeamInfo);
        TeamRequestResponseDTO AsignTeamGroup (TeamGroupRequestInputDTO newTeamGroupData);
        TeamRequestResponseDTO DeleteTeam(string teamName, string editionName);
    }
}
