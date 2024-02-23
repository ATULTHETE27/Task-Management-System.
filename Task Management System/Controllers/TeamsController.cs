using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Task_Management_System.Context;
using Task_Management_System.Models;
using Task_Management_System.Repository;
using Task_Management_System.ViewModels;

namespace Task_Management_System.Controllers
{
    public class TeamsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;


        public TeamsController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;

        }

        public async Task<IActionResult> Index()
        {
            var collages = await _unitOfWork.Teams.GetAllTeams();
            return View(collages);

        }

        [HttpGet]
        public IActionResult CreateCollage()
        {
            return View();

        }

        [HttpPost]
        public IActionResult CreateTeam(TeamVM team)
        {
            if (!ModelState.IsValid)
            {
                return View(team);
            }

            _unitOfWork.Teams.Add(team);
            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> EditCollage(Guid id, string querystringData)
        {
            Teams teams = await _unitOfWork.Teams.GetTeams(id);
            var data = new TeamVM()
            {
                Team_Name = teams.Team_Name,

            };
            return View(data);
        }


        [HttpPost]
        public async Task<IActionResult> EditCollage(TeamVM modifiedData)
        {

            if (!ModelState.IsValid)
            {
                return View(modifiedData);
            }
            Teams teams = await _unitOfWork.Teams.GetTeams(modifiedData.Team_GuId);
            teams.Team_Name = modifiedData.Team_Name;

            Teams updatedteams = _unitOfWork.Teams.Update(teams);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> GetTeam(Guid id)
        {
            Teams teams = await _unitOfWork.Teams.GetTeams(id);
            // return RedirectToAction("Index",collage);
            List<Teams> collages = new List<Teams>();
            collages.Add(teams);
            return View("Index", teams);
        }

        public IActionResult DeleteTeam(int id)
        {
            _unitOfWork.Teams.Delete(id);
            return RedirectToAction("Index");
        }
    }

}

