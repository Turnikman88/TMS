using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TaskManagmentSystem.Core.Contracts;
using TaskManagmentSystem.Models;
using TaskManagmentSystem.Models.Contracts;

namespace TaskManagmentSystem.Core
{
    public class Repository : IRepository
    {
        private int nextId;
        private IList<Type> coreClassTypes = new List<Type>();
        private IList<Type> modelsClassTypes = new List<Type>();
        private readonly IList<IMember> users = new List<IMember>();
        private readonly IList<ITeam> teams = new List<ITeam>();
        public Repository(IList<Type> coreClassTypes, IList<Type> modelsClassTypes)
        {
            this.nextId = 0;
            this.coreClassTypes = coreClassTypes;
            this.modelsClassTypes = modelsClassTypes;
        }

        
        public IList Users        
            => new List<IMember>(users);
        
        public IList Teams
            => new List<ITeam>(teams);
        public IMember LoggedUser { get; set; }
        public IList<Type> CoreClassTypes
            => new List<Type>(coreClassTypes);
        public IList<Type> ModelsClassTypes
            => new List<Type>(modelsClassTypes);

        public IMember CreateUser(string username)
        {           
            var user = new Member(++nextId, username);
            this.users.Add(user);
            return user;
        }
        public ITeam CreateTeam(string teamName)
        {
            var team = new Team(++nextId, teamName);
            this.teams.Add(team);
            return team;
        }
        public IBoardItem CreateTask(Type type, string title, string description)
        {
            var task = Activator.CreateInstance(type, ++nextId, title, description) as IBoardItem;
            return task;
        }
        public IBoard CreateBoard(string name)
        {
            var board = new Board(++nextId, name);
            return board;
        }

        public ITeam FindTeamById(int id)
        {
            return this.teams.SingleOrDefault(x => x.Id == id);
        }
        public ITeam FindTeamByName(string name)
        {
            return this.teams.SingleOrDefault(x => x.Name == name);
        }

        public IMember FindUserByName(string name)
        {
            var user = this.users.SingleOrDefault(x => x.Name == name);
            return user;
        }
    }
}
