using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Mail;
using Task_Management_System.Context;
using Task_Management_System.Models;
using Task_Management_System.Repository;
using Task_Management_System.ViewModels;

namespace Task_Management_System.Repository
{
    public class TeamRepository : Repository<Teams>, ITeamRepository
    {
        private ApplicationDbContext _db;

        public TeamRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Add(TeamVM team)
        {
            var newTeams = new Teams()
            {
                Team_Name = team.Team_Name,
            };

            _db.Teams.Add(newTeams);
            _db.SaveChanges();
        }

        public async Task<IEnumerable<string>> GetAllTeams()
        {
            return (IEnumerable<string>)_db.Teams.ToListAsync();
        }


        public async Task<Teams> GetTeams(Guid team_GUId)
        {
            Teams teams = new Teams();
            teams = await _db.Teams.FindAsync(team_GUId);
            if (teams == null)
            {
                return new Teams();
            }
            return teams;
        }

        public Teams Update(Teams teams)
        {
            _db.Update(teams);
            _db.SaveChanges();
            return teams;
        }

        public void Delete(int Id)
        {
            Teams teams = _db.Teams.Find(Id);
            if (teams != null)
            {
                _db.Teams.Remove(teams);
                _db.SaveChanges();
            }
        }
    }
}