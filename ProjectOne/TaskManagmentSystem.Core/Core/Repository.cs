using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TaskManagmentSystem.Core.Commands;
using TaskManagmentSystem.Core.Contracts;
using TaskManagmentSystem.Models;
using TaskManagmentSystem.Models.Common;
using TaskManagmentSystem.Models.Contracts;

namespace TaskManagmentSystem.Core
{
    public class Repository : IRepository
    {
        private const string adminName = "superuser";
        private const string adminPass = "th1$i$4dmiN";
        private int nextId;
        private IList<Type> coreClassTypes = new List<Type>();
        private IList<Type> modelsClassTypes = new List<Type>();
        private List<string> exsitingNames = new List<string>();
        private readonly IList<IMember> users = new List<IMember>();
        private readonly IList<ITeam> teams = new List<ITeam>();
        private readonly IList<IBoardItem> tasks = new List<IBoardItem>();

        public Repository()
        {

            this.nextId = 0;
            this.coreClassTypes = GetCoreCommandTypes();
            this.modelsClassTypes = GetModelsClassTypes();
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
                throw new UserInputException(Constants.NAME_MUST_BE_UNIQUE);
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
                throw new UserInputException(Constants.NAME_MUST_BE_UNIQUE);
            }
            this.exsitingNames.Add(teamName);
            return team;
        }

        public IBoardItem CreateTask(Type type, string title, string description, params string[] parameters)
        {
            IBoardItem task = null;
            switch (type.Name)
            {
                case nameof(Bug):
                    task = Activator.CreateInstance(type, ++nextId, title, description, parameters.ToList()) as IBoardItem;
                    break;
                case nameof(Feedback):
                    task = Activator.CreateInstance(type, ++nextId, title, description, int.Parse(parameters[0])) as IBoardItem;
                    break;
                case nameof(Story):
                    task = Activator.CreateInstance(type, ++nextId, title, description) as IBoardItem;
                    break;
                default:
                    throw new UserInputException(string.Format(Constants.GIVEN_TYPE_ERR, type.Name));
            }
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
        public bool IsTeamMember(ITeam team, IMember user)
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

        private static List<Type> GetCoreCommandTypes()
        {
            return Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(x => x.FullName.Contains(Constants.CORE_ASSEMBLY_KEY)
                         && x.BaseType == typeof(BaseCommand)).ToList();
        }

        private static List<Type> GetModelsClassTypes()
        {
            return Assembly.GetExecutingAssembly()
                .GetReferencedAssemblies()
                .Select(x => Assembly.Load(x))
                .SelectMany(x => x.GetTypes())
                .Where(x => x.FullName.Contains(Constants.MODELS_ASSEMBLY_KEY)
                         && x.BaseType == typeof(BoardItem)).ToList();
        }
    }
}
