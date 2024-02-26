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
        public IActionResult AllTeams()
        {
           List<Teams> teamsList = _unitOfWork.Teams.GetAll().ToList();
            return View(teamsList);
        }
        //GET
        public IActionResult Upsert(int? id)
        {
            //TeamVM teamVM = new()
            //{
            //    Teams = new(),
            //    TeamsList = _unitOfWork.Teams.GetAll().Select(i => new SelectListItem
            //    {
            //        Text = i.Team_Name,
            //        Value = i.Team_Id.ToString()
            //    }),



            //};



            if (id == null || id == 0)
            {
                return View(new Teams());
            }
            else
            {
                Teams teamsobj = _unitOfWork.Teams.GetFirstOrDefault(u => u.Team_Id == id);
                return View(teamsobj);

                //update Tasks
            }


        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Teams obj)
        {

            if (ModelState.IsValid)
            {

                if (obj.Team_Id == 0)
                {
                    _unitOfWork.Teams.Add(obj);
                }
                else
                {
                    _unitOfWork.Teams.Update(obj);
                }
                _unitOfWork.Save();
                TempData["success"] = "Task created successfully";
                return RedirectToAction("AllTeams");
            }
            else
            {
                return View(obj);
            }
        }

        public IActionResult Delete(int? id)
        {
            var obj = _unitOfWork.Teams.GetFirstOrDefault(u => u.Team_Id == id);
            if (obj == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _unitOfWork.Teams.Remove(obj);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Teams> teamsList = _unitOfWork.Teams.GetAll().ToList();
            return Json(new { data = teamsList });
        }

        //POST
        [HttpDelete]
        public IActionResult Delete1(int? id)
        {
            var obj = _unitOfWork.Teams.GetFirstOrDefault(u => u.Team_Id == id);
            if (obj == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _unitOfWork.Teams.Remove(obj);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }
        #endregion
    }

}
 