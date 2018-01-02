using _1.UI.Code;
using _1.UI_2.Models;
using _2.BL;
using _4.Entities;
using _4.Entities.ExtentionMethods;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace _1.UI.Controllers
{
    public class CarsController : Controller
    {
        const string SUCCESS = "המכונית הוספה בהצלחה";
        const string FAILURE = "המכונית כבר קיימת במערכת";
        const string SEARCH_COOKIE = "searchCookie";

        CarManager carManager;
        BranchManager branchManager;
        OrderManager orderManager;

        public CarsController()
        {
            carManager = new CarManager();
            branchManager = new BranchManager();
            orderManager = new OrderManager();
        }
        // GET: Cars
        public ActionResult Index()
        {
            //var cars = carManager.GetAllCars();
            SessionManager.LastPage = new List<string>() { "Index", "Cars" };
            ViewBag.user = SessionManager.User;
            //CreatCCVM()
            var search = new List<CarVM>();
            var findCarVM = new FindCarVM(branchManager.Branches, carManager.Manufacturers, carManager.Groups, carManager.Gears, search);


            return View(findCarVM);
        }
        [Authorize(Roles = "Manager")]
        public ActionResult Manage()
        {

            SessionManager.LastPage = new List<string>() { "Manage", "Cars" };
            ViewBag.user = SessionManager.User;
            var findCarVM = new FindCarVM(branchManager.Branches, carManager.Manufacturers, carManager.Groups, carManager.Gears);


            return View(findCarVM);
        }

        [HttpGet]
        [Authorize(Roles = "Manager")]
        public ActionResult Create()
        {
            SessionManager.LastPage = new List<string>() { "Create", "Cars" };
            if (User.Identity.IsAuthenticated)
            {


                CreatCarVM CCVM = CreatCCVM();
                //CCVM.Branches = branchManager.Branches.Select(b => new SelectListItem() {Text=b.Name,Value=b.ID.ToString() }).ToList();
                //CCVM.Manufacturer = carManager.Manufacturers.Select(m => new SelectListItem() {Text=m.Name,Value=m.ID.ToString() }).ToList();
                //CCVM.Status = carManager.Statuses.Select(s => new SelectListItem() {Text=s.Name,Value=s.ID.ToString() }).ToList();
                //CCVM.Gear = carManager.Gears.Select(g => new SelectListItem() {Text=g.Name,Value=g.ID.ToString() }).ToList();


                ViewBag.user = SessionManager.User;


                return View(CCVM);

            }
            else
            {

                return RedirectToAction("Index", "Login", "");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CarVM newCar)
        {


            var car = Parse(newCar);
            bool b = carManager.Edit(car);

            return RedirectToAction("Manage");

        }

        public ActionResult Edit(int id)
        {
            SessionManager.LastPage = new List<string>() { "Edit", "Cars" };
            if (User.Identity.IsAuthenticated)
            {


                EditCarVM ECVM = EditCarVM(id);
                ViewBag.user = SessionManager.User;


                return View(ECVM);

            }
            else
            {

                return RedirectToAction("Index", "Login", "");
            }
        }

        public ActionResult Delete(int id)
        {
            SessionManager.LastPage = new List<string>() { "Manage", "Cars" };
            if (User.Identity.IsAuthenticated)
            {




                bool b =carManager.Delete(id);


                return RedirectToAction("Manage");

            }
            else
            {

                return RedirectToAction("Index", "Login", "");
            }
        }

        private List<string> SavePicture(HttpPostedFileBase file, string Serial)
        {
            List<string> result = new List<string>() { "", "" };
            if (file != null && file.ContentLength > 0)
                if (file.ContentLength <= 3145728)// 3MB= 3145728 Bytes
                {
                    // if needs to restrict type of the file
                    var type = file.ContentType.Split('/')[1];//         jpeg | vnd.openxmlformats-officedocument.wordprocessingml.document  |pdf|vnd.openxmlformats-officedocument.spreadsheetml.sheet
                    var type1 = file.FileName.Split('.').Last();//       JPG  | docx                                                         |pdf|xlsx
                    try
                    {
                        string path1 = @"C:\Users\User\Desktop\Test\" + file.FileName;
                        string fileName = Serial + "." + type;
                        string path = Path.Combine(Server.MapPath(@"~\Content\Images\Cars\"), Path.GetFileName(fileName));// <- to save in the mechin path

                        file.SaveAs(path);
                        result[0] = "S";
                        result[1] = fileName;

                    }
                    catch (Exception ex)
                    {
                        result[0] = "F";
                        result[1] = "ERROR:" + ex.Message.ToString();

                    }
                }
                else
                {
                    result[0] = "F";
                    result[1] = "Your File is more then 3 MB.";
                }
            else
            {
                result[0] = "F";
                result[1] = "You have not specified a file.";

            }
            return result;



        }

        private void SaveCookies(string search)
        {
            // create the cookie
            HttpCookie cookie = new HttpCookie(SEARCH_COOKIE);
            cookie.Value = search;
            Response.Cookies.Add(cookie);


        }

        public string[] ReadCookie()
        {

            if (Response.Cookies[SEARCH_COOKIE] != null)
            {
                HttpCookie cookie = Response.Cookies[SEARCH_COOKIE];
                return cookie.Value.Split(';');
            }
            else
                return new string[1]{""};

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CarVM newCar)
        {
            if (carManager.IsSerial(newCar.Serial))
            {
                var result = SavePicture(newCar.img, newCar.Serial.ToString());
                if (result[0] == "S")
                {
                    newCar.Image = result[1];// the img source

                    if (carManager.Create(Parse(newCar)))
                    {

                        ViewBag.user = SessionManager.User;
                        ViewBag.result = SUCCESS;
                        ViewBag.resultColor = "white";
                    }
                }
                else
                {
                    ViewBag.result = result[1];
                    ViewBag.resultColor = "red";
                }
            }
            else
            {
                ViewBag.result = FAILURE;
                ViewBag.resultColor = "red";


            }
            CreatCarVM CCVM = CreatCCVM();
            return View("Create", CCVM);
        }

        public ActionResult Test()
        {
            return View();
        }

        //public ActionResult Create(HttpPostedFileBase img)
        //{

        //   // SavePicture();
        //    ViewBag.user = SessionManager.User;
        //    return View("Create");
        //}

        public ActionResult GetAllCars()
        {
            var cars = carManager.GetAllCars();


            return Json(cars, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCarsByBranch(int id)
        {

            //  Branch tempBranch = branchManager.Branches.Where(b => b.ID == id).FirstOrDefault();

            var cars = CreateCarVM(carManager.GetCarsByBranch(id), carManager.Groups);
            return Json(cars, JsonRequestBehavior.AllowGet);


        }

        public JsonResult GetCarsSearch(int _branch, string _from, string _to, string _group, string _manufacturer, int? _min, int? _max)
        {
            
            List<CarVM> carsVM = new List<CarVM>();
            // var d = Convert.ToDateTime(_from);
            var cars = carManager.GetCarsByBranch(_branch);

            cars = orderManager.GetAvailableCars(cars, Convert.ToDateTime(_from), Convert.ToDateTime(_to));
            // creating the list of Branch's carsVM
            var tempCars = CreateCarVM(cars, carManager.Groups);
            tempCars = CreateCarVM(cars, carManager.Groups);
            // Group Filter
            if (_group != "" && _group != "undefined")
            {
                tempCars = tempCars.Where(c => c.Group == _group).ToList();
            }
            // Manufacturer Filter
            if (_manufacturer != "" && _manufacturer != "undefined")
            {
                var models = carManager.Manufacturers.Where(m => m.Name == _manufacturer).Select(m => m.Models.Select(mo => mo.Name)).FirstOrDefault();

                tempCars = tempCars.Where(c => models.Contains(c.Model)).ToList();
            }

            //if (_group != "")
            //{
            //    var group = carManager.Groups.Where(g => g.Name == _group).FirstOrDefault();
            //    tempCars = tempCars.Where(c => group.Name==c.Group).ToList();
            //}

            // Max Filter
            if (_max > 0)
            {
                var groups = carManager.Groups.Where(g => g.PerDay <= _max).Select(g => g.Name).ToList();

                tempCars = tempCars.Where(c => groups.Contains(c.Group)).ToList();
            }
            // Min Filter
            if (_min > 0)
            {
                var groups = carManager.Groups.Where(g => g.PerDay > _min).Select(g => g.Name).ToList();

                tempCars = tempCars.Where(c => groups.Contains(c.Group)).ToList();
            }
            StringBuilder sb = new StringBuilder();
            sb.Append("");
            foreach (var c in tempCars)
            {
                if(c!=tempCars.Last())
                sb.Append(c.Serial + ";");
                else
                    sb.Append(c.Serial);

            }
            string search = sb.ToString();
            SaveCookies(search);
            return Json(tempCars, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetManageCars(int _branch, string _group, string _manufacturer)
        {
            List<CarVM> carsVM = new List<CarVM>();
            // var d = Convert.ToDateTime(_from);
            var cars = carManager.GetCarsByBranch(_branch);

            // creating the list of Branch's carsVM
            var tempCars = CreateCarVM(cars, carManager.Groups);
            // Group Filter
            if (_group != "" && _group != "undefined")
            {
                tempCars = tempCars.Where(c => c.Group == _group).ToList();
            }
            // Manufacturer Filter
            if (_manufacturer != "" && _manufacturer != "undefined")
            {
                var models = carManager.Manufacturers.Where(m => m.Name == _manufacturer).Select(m => m.Models.Select(mo => mo.Name)).FirstOrDefault();

                tempCars = tempCars.Where(c => models.Contains(c.Model)).ToList();
            }

            return Json(tempCars, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllBranches()
        {
            var branches = branchManager.Branches;


            return Json(branches, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetAllManufacturerModels(string manufacturer)
        {
            // Gets Manufacturer ID
            var manufacturerID = carManager.Manufacturers.Where(m => m.Name == manufacturer).Select(m => m.ID).FirstOrDefault();
            // Gets all Manufacturer's Models
            var models = carManager.Models.Where(m => m.Manufacturer == manufacturerID).Select(m => new SelectListItem() { Text = m.Name, Value = m.ID.ToString() });


            return Json(models, JsonRequestBehavior.AllowGet);
        }

        private Car Parse(CarVM newCar)
        {
            Car car = new Car();
            car.Serial = newCar.Serial;
            car.Model = carManager.ModelID(newCar.Model);
            car.Kilometerage = newCar.Kilometerage;
            car.Image = newCar.Image;
            car.Status = carManager.StutusID(newCar.Status);
            car.Branch = branchManager.BranchID(newCar.Branch);
            car.Gear = carManager.GearID(newCar.Gear);





            return car;
        }

        private List<CarVM> CreateCarVM(List<Car> _cars, List<Group> _groups)
        {
            List<CarVM> cars = new List<CarVM>();
            foreach (var _car in _cars)
            {
                cars.Add(new CarVM()
                {
                    Serial = _car.Serial,
                    Model = _car.Model1.Name,
                    Kilometerage = _car.Kilometerage,
                    Image = _car.Image,
                    Status = _car.Status1.Name,
                    Branch = _car.Branch1.Name,
                    Gear = _car.Gear1.Name,
                    Group = _car.Model1.Group.Name,
                    Price = _groups.Where(p => p.Level == _car.Model1.Level).Select(p => p.PerDay).FirstOrDefault()
                });
            }

            return cars;


        }

        private CreatCarVM CreatCCVM()
        {

            CreatCarVM CCVM = new CreatCarVM();
            CCVM.Branches = branchManager.Branches.Select(b => new SelectListItem() { Text = b.Name, Value = b.ID.ToString() }).ToList();
            CCVM.Manufacturer = carManager.Manufacturers.Select(m => new SelectListItem() { Text = m.Name, Value = m.ID.ToString() }).ToList();
            CCVM.Status = carManager.Statuses.Select(s => new SelectListItem() { Text = s.Name, Value = s.ID.ToString() }).ToList();
            CCVM.Gear = carManager.Gears.Select(g => new SelectListItem() { Text = g.Name, Value = g.ID.ToString() }).ToList();


            return CCVM;

        }

        private EditCarVM EditCarVM(int car)
        {
            var _car = carManager.GetCarsID(car);

            var carVm = new CarVM()
            {
                Serial = _car.Serial,
                Model = _car.Model1.Name,
                Kilometerage = _car.Kilometerage,
                Image = _car.Image,
                Status = _car.Status1.Name,
                Branch = _car.Branch1.Name,
                Gear = _car.Gear1.Name

            };

            EditCarVM ECVM = new EditCarVM();
            ECVM.Car = carVm;
            ECVM.Branches = branchManager.Branches.Select(b => new SelectListItem() { Text = b.Name, Value = b.ID.ToString() }).ToList();
            ECVM.Statuses = carManager.Statuses.Select(s => new SelectListItem() { Text = s.Name, Value = s.ID.ToString() }).ToList();



            return ECVM;





        }

        private CreatCarVM CreatCCVM1(List<Branch> _branches, List<Manufacturer> _manufacturers)
        {
            CreatCarVM CCVM = new CreatCarVM();
            CCVM.Branches = new List<SelectListItem>();
            foreach (var branch in _branches)
            {
                CCVM.Branches.Add(new SelectListItem()
                {
                    Text = branch.Name,
                    Value = branch.ID.ToString()
                });
            }
            CCVM.Manufacturer = new List<SelectListItem>();
            foreach (var manufacturer in _manufacturers)
            {
                CCVM.Branches.Add(new SelectListItem()
                {
                    Text = manufacturer.Name,
                    Value = manufacturer.ID.ToString()
                });
            }
            return CCVM;





        }



    }
}