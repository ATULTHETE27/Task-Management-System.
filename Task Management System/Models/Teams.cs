using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Task_Management_System.Models
{
    public class Teams //: IEnumerable
    {
        [Key]
        public int Team_Id { get; set; }
        public string? Team_Name { get; set; }

        //public IEnumerable<SelectListItem> TeamsList { get; set; }

        ////public Teams()
        ////{
        ////    teamsList = new List<Teams>();
        ////    // Initialize the teamsList as needed
        ////}

        ////public void AddTeam(Teams team)
        ////{
        ////    teamsList.Add(team);
        ////}
        //public IEnumerator GetEnumerator()
        //{
        //    return TeamsList.GetEnumerator();
        //}
    }
}
