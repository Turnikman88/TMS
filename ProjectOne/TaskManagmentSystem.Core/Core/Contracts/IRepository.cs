using System;
using System.Collections;
using System.Collections.Generic;
using TaskManagmentSystem.Models.Contracts;

namespace TaskManagmentSystem.Core.Contracts
{
    public interface IRepository
    {
        IMember LoggedUser { get; set; }
        IList Users { get; }
        IList Teams { get; }
        IList<Type> CoreClassTypes { get; }
        IList<Type> ModelsClassTypes { get; }

        ITeam CreateTeam(string teamName);
        IMember CreateUser(string username, string password);
        IBoardItem CreateTask(Type type, string title, string description);
        IBoard CreateBoard(string name);

        IBoardItem FindTaskByID(int id);
        ITeam FindTeamById(int id);
        ITeam FindTeamByName(string name);
        IMember FindUserById(int id);
        IMember FindUserByName(string name);
        ITeam GetTeam(string teamIdentificator);
        IMember GetUser(string userIndicator);

        bool IsTeamMember(ITeam team, IMember user);


    }
}
