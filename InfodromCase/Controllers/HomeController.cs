using InfodromCase.Entities;
using InfodromCase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.SqlClient;
using System.Diagnostics;

namespace InfodromCase.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        static string connectionString = @"Server=DESKTOP-DN78T3M;Database=InfodromCase;Trusted_Connection=true;TrustServerCertificate=True"; 
        SqlConnection connection = new SqlConnection(connectionString);
        SqlCommand command = new SqlCommand();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            var staffList = new List<HomeIndexVM>();
            try
            {
                var sqlList = @"select s.Id,s.RegistrationNumber,s.Name,s.Surname,s.DepartmentId,d.Name                                  [DepartmentName]
                                             from staff s 
                                             join department d on d.Id = s.DepartmentId";

                command.Connection = connection;
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = sqlList;
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var staff = new HomeIndexVM
                    {
                        StaffId = (int)reader["Id"],
                        StaffRegistrationNumber = (int)reader["RegistrationNumber"],
                        StaffName = reader["Name"].ToString(),
                        StaffSurname = reader["Surname"].ToString(),
                        StaffDepartmentId = (int)reader["DepartmentId"],
                        DepartmentName = reader["DepartmentName"].ToString(),

                    };
                    staffList.Add(staff);
                }

                return View(staffList);
            }
            catch (Exception)
            {
                return View();
            }
            finally { connection.Close(); }
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            StaffCreateVM staffCreateVM = new StaffCreateVM();
            staffCreateVM.departmentDropodowns = await GetDepartments();
            return View(staffCreateVM);
        }


        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(StaffCreateVM staffCreateVM)
        {
            if (!ModelState.IsValid)
            {
                @ViewBag.departmentDropodowns = await GetDepartments();

                return View(staffCreateVM);
            }

            string insert_Staff = "Insert INTO Staff (RegistrationNumber,Name,Surname,DepartmentId) VALUES(@registrationNumber,@name,@surname,@departmentId)";

            command.Connection = connection;
            command.CommandText = insert_Staff;
            command.CommandType = System.Data.CommandType.Text;

            command.Parameters.AddWithValue("@registrationNumber", staffCreateVM.RegistrationNumber);
            command.Parameters.AddWithValue("@name", staffCreateVM.Name);
            command.Parameters.AddWithValue("@surname", staffCreateVM.Surname);
            command.Parameters.AddWithValue("@departmentId", staffCreateVM.DepartmentId);

            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }
                int isAdded = command.ExecuteNonQuery();
                if (isAdded > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }
            }
            catch (Exception)
            {
                return View();
            }
            finally { connection.Close(); }

        }


        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {

            var sqlStafRaw = "SELECT * FROM Staff WHERE Id = @id";
            command.Connection = connection;
            command.CommandText = sqlStafRaw;
            command.CommandType = System.Data.CommandType.Text;
            command.Parameters.AddWithValue("@Id", id);
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
            var reader = command.ExecuteReader();

            var staf = new StaffUpdateVM();
            while (reader.Read())
            {
                var stafff = new StaffUpdateVM
                {
                    StaffId = (int)reader["Id"],
                    StaffRegistrationNumber = (int)reader["RegistrationNumber"],
                    StaffName = reader["Name"].ToString(),
                    StaffSurname = reader["Surname"].ToString(),
                    StaffDepartmentId = (int)reader["DepartmentId"],
                };
                ViewBag.departmentDropodowns = await GetDepartments();
                staf = stafff;
            }
            return View(staf);
        }


        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public async Task<IActionResult> Update(StaffUpdateVM staffUpdateVM)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                string sqlDelete = "Delete Staff Where Id = @id";
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = sqlDelete;
                command.Parameters.AddWithValue("@id", id);
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }
                command.ExecuteNonQuery();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View();
            }
            finally { connection.Close(); }
        }

        [NonAction]
        public async Task<List<SelectListItem>> GetDepartments()
        {
            var departmentList = new List<DepartmentDropodown>();
            var departmetSelectList = new List<SelectListItem>();

            var sqlList = @"SELECT Id,Name FROM Department";

            command.Connection = connection;
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = sqlList;
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var department = new DepartmentDropodown
                {
                    Id = (int)reader["Id"],
                    Name = reader["Name"].ToString(),
                };
                departmentList.Add(department);
            };

            foreach (var item in departmentList)
            {
                SelectListItem listItem = new SelectListItem()
                {
                    Value = item.Id.ToString(),
                    Text = item.Name.ToString()
                };
                departmetSelectList.Add(listItem);
            }
            return departmetSelectList;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}