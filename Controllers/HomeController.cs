using curdoperation.Context;
using curdoperation.Models;
using Dapper;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MySqlX.XDevAPI.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace curdoperation.Controllers
{




    public class HomeController : Controller
    {

        HttpClientHandler clientHandler = new HttpClientHandler();
        Curd curd1 = new Curd();
        List<Curd> curds = new List<Curd>();

        private readonly IDbConnection db2;

        Adio_CRUD_DAL dbContex = new Adio_CRUD_DAL();

        readonly masterContext _auc;
        readonly masterContext db = new masterContext();
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration configuration;
        public HomeController(IConfiguration config, masterContext auc, ILogger<HomeController> logger, IConfiguration configuration)
        {
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            db2 = new SqlConnection(configuration.GetConnectionString("connection"));

            configuration = config;
            _auc = auc;
            _logger = logger;
        }


        //[HttpGet]
        //public IActionResult Index()
        //{
        //    var sql = "SELECT * FROM Curd";
        //    List<Curd> result = db2.Query<Curd>(sql).ToList();
        //    return View(result);
        //}

        Context.Adio_CRUD_DAL dbop = new Context.Adio_CRUD_DAL();

        //[HttpGet]

        //////bestone
        //public IActionResult Index()
        //{
        //    var getdata = new Adio_CRUD_DAL();

        //    List<Curd> result = dbContex.GetAllStudent().ToList();

        //    return View(result);
        //}

        [HttpGet]

        public async Task<IActionResult> IndexAsync()
        {
            curds = new List<Curd>();
            using (var httpClient = new HttpClient(clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:44350/api/Todo"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    List<Curd> result = JsonConvert.DeserializeObject<List<Curd>>(apiResponse).ToList();


                    return View(result);
                }


            }

        }




        public JsonResult GetCountrylist()
        {
            List<county> result = dbContex.GetCountry().ToList();


            return Json(result);
        }



        //public JsonResult GetCountrylist()
        //{
        //    DataSet ds = dbop.GetCountry();
        //    List<SelectListItem> list = new List<SelectListItem>();
        //    foreach (DataRow dr in ds.Tables[0].Rows)
        //    {
        //        list.Add(new SelectListItem { Text = dr["countryname"].ToString(), Value = dr["countryid"].ToString() });
        //    }

        //    return Json(list);
        //}
        //public JsonResult GetStatelist(int cid)
        //{
        //    DataSet ds = dbop.GetState(cid);
        //    List<SelectListItem> list = new List<SelectListItem>();
        //    foreach (DataRow dr in ds.Tables[0].Rows)
        //    {
        //        list.Add(new SelectListItem { Text = dr["State_name"].ToString(), Value = dr["State_id"].ToString() });
        //    }
        //    return Json(list);
        //}

        public JsonResult GetState(int countryid)
        {

            List<crdstate> result = dbContex.GetState(countryid).ToList();

            return Json(result);
        }









        [HttpPost]
        public IActionResult Newent(Curd crd)
        {
            _auc.Add(crd);
            _auc.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Getcurd()
        {
            int? Id = HttpContext.Session.GetInt32("Newid");
            if (Id == null)
            {
                Id = Convert.ToInt32(Request.Cookies["Newid"]);
            }
            Curd crd = db.Curds.FirstOrDefault(x => x.Newid == Id);
            return new JsonResult(crd);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id)
        {
            if (id == null) return NotFound();
            Curd curd = dbContex.GetStudentById(id);
            if (curd == null)
            {
                return NotFound();
            }
            return View(curd);

        }







        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateCurd(int sd, Curd curd)
        {
            try
            {

                if (sd != null)
                {
                    curd1 = new Curd();


                    using (var httpClient = new HttpClient(clientHandler))
                    {
                        StringContent content = new StringContent(JsonConvert.SerializeObject(curd), Encoding.UTF8, "application/json");

                        using (var response = await httpClient.PostAsync("https://localhost:44350/api/Todo", content))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            curd1 = JsonConvert.DeserializeObject<Curd>(apiResponse);


                            return RedirectToAction("Index", "Home");
                        }


                    }


                    //dbContex.CreateStudent(curd);
                    //return RedirectToAction("Index", "Home");
                }

                //else
                //{
                //    sd = curd.Newid;
                //    curd.Newid = sd;
                //    dbContex.UpdateStudent(curd);
                //    return RedirectToAction("Index", "Home");

                //}

                //else
                //{
                //    sd = curd.Newid;
                //    curd.Newid = sd;
                //    dbContex.UpdateStudent(curd);
                //    return RedirectToAction("Index", "Home");


                //}
                return View("Index");

            }

            catch (Exception cd)
            {
                HttpClientHandler clientHandler = new HttpClientHandler();


                curd1 = new Curd();

                using (var httpClient1 = new HttpClient(clientHandler))
                {
                    StringContent content1 = new StringContent(JsonConvert.SerializeObject(curd), Encoding.UTF8, "application/json");


                    try
                    {

                        using (var response1 = await httpClient1.PostAsync("https://localhost:44350/api/Todo/" + curd.Newid, content1))
                        {
                            string apiResponse = await response1.Content.ReadAsStringAsync();
                            curd1 = JsonConvert.DeserializeObject<Curd>(apiResponse);

                            return RedirectToAction("Index", "Home");

                        }


                    }

                    catch (Exception cd1)
                    {
                        return null;
                    }

                }



                









                //dbContex.UpdateStudent(curd);
                //return RedirectToAction("Index", "Home");


                //db.SaveChanges();
                //return RedirectToAction("Index", "Home");
            }

        }

        [HttpPost]
        public IActionResult UiipdateCurd(Curd curd)
        {
            Curd u = db.Curds.FirstOrDefault(x=> x.Newid == curd.Newid);
            if (u != null)
            {
                u.Firstname = curd.Firstname;
                u.Lastname = curd.Lastname;
                u.Mobileno = curd.Mobileno;
                u.Zodiac = curd.Zodiac;
                u.Birthdate = curd.Birthdate;
                u.Email = curd.Email;
                u.Address = curd.Address;
                u.countryid = curd.countryid;
                u.curdstateid = curd.curdstateid;
                var result = db.Curds.Update(u);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                _auc.Add(curd);
                _auc.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult adddata(int id)
        {

            try
            {


                if (id == 0)
                {
                    ViewBag.Message = "Adduser";
                    return View();
                }
                else
                {
                    ViewBag.Message = "Edit";

                    Curd obj = db.Curds.Where(x => x.Newid == id).FirstOrDefault();

                    ViewBag.ctrid = obj.countryid;

                    ViewBag.sttid = obj.curdstateid;

                    return View(obj);

                     
                }
            }
            catch (Exception cd)
            {
                return null;
            }
        }


        [HttpGet]
        public IActionResult delt(int id)
        {
            try
            {
                Curd obj = db.Curds.Where(x => x.Newid == id).FirstOrDefault();
                return View(obj);
            }

            catch (Exception cd)
            {
                return null;
            }
        }


        //[HttpPost]
        //public IActionResult Delt(Curd id)
        //{
        //    try
        //    {
        //        var sql = "DELETE FROM Curd WHERE newid = @Newid";

        //       db2.Execute(sql, new { @Id = id });

        //        return RedirectToAction("Index");
        //    }

        //    catch (Exception cd)
        //    {
        //        return View("Index");
        //    }
        //}




        [HttpPost]

        public async Task<IActionResult> Delt(Curd del)
        {
            try
            {
                using(var httpClient = new HttpClient(clientHandler))
                {
                    using(var response = await httpClient.DeleteAsync("https://localhost:44350/api/Todo/"+del.Newid))
                    {
                        return RedirectToAction("Index");
                    }

                }




                //dbContex.DeleteStudent(del.Newid);
                return RedirectToAction("Index");
            }
            catch (Exception cd)
            {
                return View();
            }

        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
