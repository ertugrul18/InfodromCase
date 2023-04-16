using InfodromCase.Entities;
using InfodromCase.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace InfodromCase.Controllers
{
    public class DepartmentController : Controller
    {
        static string connectionString = @"Server=DESKTOP-DN78T3M;Database=InfodromCase;Trusted_Connection=true;TrustServerCertificate=True";

        public IActionResult Index()
        {
            var menuItems = GetMenuItems();
            var menuTree = BuildMenuTree(menuItems);
            return View(menuTree);
        }



        private List<DepartmentIndexVM> GetMenuItems()
        {
            var departmentItems = new List<DepartmentIndexVM>();

            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("SELECT * FROM Department", connection);
                connection.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var department = new DepartmentIndexVM
                    {
                        Id = (int)reader["Id"],
                        Name = reader["Name"].ToString(),
                        ParentId = Convert.ToInt32(reader["ParentId"] == DBNull.Value ? null : (int?)reader["ParentId"]),

                    };
                    departmentItems.Add(department);
                }
            }

            return departmentItems;
        }
        private List<DepartmentIndexVM> BuildMenuTree(List<DepartmentIndexVM> menuItems)
        {
            var menuTree = new List<DepartmentIndexVM>();

            foreach (var menuItem in menuItems.Where(mi => mi.ParentId == null))
            {
                menuItem.Children = GetChildMenuItems(menuItems, menuItem.Id);
                menuTree.Add(menuItem);
            }

            return menuTree;
        }

        private List<DepartmentIndexVM> GetChildMenuItems(List<DepartmentIndexVM> menuItems, int parentId)
        {
            var childMenuItems = new List<DepartmentIndexVM>();

            foreach (var menuItem in menuItems.Where(mi => mi.ParentId == parentId))
            {
                menuItem.Children = GetChildMenuItems(menuItems, menuItem.Id);
                childMenuItems.Add(menuItem);

                if (menuItem.Children != null && menuItem.Children.Count > 0)
                {
                    foreach (var childMenuItem in menuItem.Children)
                    {
                        childMenuItems.AddRange(GetChildMenuItems(menuItems, childMenuItem.Id));
                    }
                }
            }
            return childMenuItems;
        }
    }
}
