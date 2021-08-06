using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TaskManagmentSystem.Core.Contracts;
using TaskManagmentSystem.Models;
using TaskManagmentSystem.Models.Common;
using TaskManagmentSystem.Models.Contracts;

namespace TaskManagmentSystem.Core
{
    public class Repository : IRepository
    {
        private const string adminName = "superuser";
        private const string adminPass = "adminadmin";
        private int nextId;
        private IList<Type> coreClassTypes = new List<Type>();
        private IList<Type> modelsClassTypes = new List<Type>();
        private List<string> exsitingNames = new List<string>();
        private readonly IList<IMember> users = new List<IMember>();
        private readonly IList<ITeam> teams = new List<ITeam>();
        private readonly IList<IBoardItem> tasks = new List<IBoardItem>();

        public Repository(IList<Type> coreClassTypes, IList<Type> modelsClassTypes)
        {
            this.nextId = 0;
            this.coreClassTypes = coreClassTypes;
            this.modelsClassTypes = modelsClassTypes;
            CreateAdmin(); // ToDo: Maybe in static constructor
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

        private void CreateAdmin()
        {
            var admin = new Member(0, adminName, adminPass);
            admin.ChangeRole("root");
            this.users.Add(admin);
        }
        public IMember CreateUser(string username, string password)
        {
            var user = new Member(++nextId, username, password);
            this.users.Add(user);
            if (this.exsitingNames.Contains(username))
            {
                throw new UserInputException("Name must be unique");
            }
            this.exsitingNames.Add(username);
            return user;
        }
        public ITeam CreateTeam(string teamName)
        {
            var team = new Team(++nextId, teamName);
            team.AddMember(LoggedUser);
            team.AddAdministrator(LoggedUser);
            this.teams.Add(team);
            if (this.exsitingNames.Contains(teamName))
            {
                throw new UserInputException("Name must be unique");
            }
            this.exsitingNames.Add(teamName);
            return team;
        }
        public IBoardItem CreateTask(Type type, string title, string description)
        {
            var task = Activator.CreateInstance(type, ++nextId, title, description) as IBoardItem;
            tasks.Add(task);
            return task;
        }

        public IBoard CreateBoard(string name)
        {
            var board = new Board(++nextId, name);
            return board;
        }

        public IBoardItem FindTaskByID(int id)
        {
            return this.tasks.FirstOrDefault(x => x.Id == id);
        }
        public ITeam FindTeamById(int id)
        {
            return this.teams.FirstOrDefault(x => x.Id == id);
        }
        public ITeam FindTeamByName(string name)
        {
            return this.teams.FirstOrDefault(x => x.Name == name);
        }
        public IMember FindUserById(int id)
        {
            return this.users.FirstOrDefault(x => x.Id == id);
        }
        public IMember FindUserByName(string name)
        {
            return this.users.FirstOrDefault(x => x.Name == name);
        }
        public bool IsTeamMember(ITeam team, IMember user) //ToDo: Ask Kalin
        {
            if (team.Members.Any(x => x.Name == user.Name))
            {
                return true;
            }
            return false;
        }
        public IMember GetUser(string userIndicator)
        {
            IMember user;
            if (int.TryParse(userIndicator, out int userId))
            {
                user = this.FindUserById(userId);
            }
            else
            {
                user = this.FindUserByName(userIndicator);
            }

            return user ?? throw new UserInputException(string.Format(Constants.USER_DOESNT_EXSIST, userIndicator));

        }

        public ITeam GetTeam(string teamIdentificator)
        {
            ITeam team;
            if (int.TryParse(teamIdentificator, out int temaId))
            {
                team = this.FindTeamById(temaId);
            }
            else
            {
                team = this.FindTeamByName(teamIdentificator);
            }

            return team ?? throw new UserInputException(string.Format(Constants.TEAM_DOESNT_EXSIST, teamIdentificator));
        }
    }
}
