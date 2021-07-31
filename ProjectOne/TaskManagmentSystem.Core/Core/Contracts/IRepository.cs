using System;
using System.Collections;
using System.Collections.Generic;
using TaskManagmentSystem.Models.Contracts;

namespace TaskManagmentSystem.Core.Contracts
{
    public interface IRepository
    {
        IList Users { get; }
        IList Teams { get; }

        ITeam CreateTeam(string teamName);
        IMember CreateUser(string username);
        IBoardItem CreateTask(Type type, string title, string description);
        IBoard CreateBoard(string name);

        ITeam FindTeamById(int id);
        ITeam FindTeamByName(string name);
        IMember LoggedUser { get; set; }
        IMember FindUserById(int id);
        IMember FindUserByName(string name);
        IList<Type> CoreClassTypes { get; }
        IList<Type> ModelsClassTypes { get; }

    }
}
