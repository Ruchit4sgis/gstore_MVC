using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.OleDb;
using System.Data.Odbc;
using System.ComponentModel;

namespace gstore.Controllers
{
    public class HomeController : Controller
    {
        //Models.gstoredb context = new Models.gstoredb();
        public ActionResult Index()
        {
            return View();
        }
        //[HttpPost]
        //public ActionResult Index(string username, string password)
        //{
        //    if (ModelState.IsValid) // this is check validity
        //    {
        //        using (Models.gstoredb dc = new Models.gstoredb())
        //        {
        //            var v = dc.Login.Where(a => a.Username.Equals(username) && a.Password.Equals(password)).FirstOrDefault();
        //            if (v != null)
        //            {
        //                Session["LogedUserID"] = v.Username.ToString();
        //                Session["LogedUserFullname"] = v.Username.ToString();
        //                return RedirectToAction("Home");
        //            }
        //        }
        //    }
        //    return View();
        //}

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }


        public ActionResult Student_Store_Statement()
        {
        //    if (Session["LogedUserID"] != null)
        //    {
                return View();
            //}
            //else
            //{
            //    return RedirectToAction("Index");
            //}

        }

        [HttpPost]
        public ActionResult Student_Store_Statement(string mid, string room, string cup)
        {
            if (Request.Form["Member_search"] != null)
            {
                getDebitRecords_get(mid);
                getCreditRecords_get(mid);
                getstudentinfo(mid);
            }
            else if (Request.Form["Rooom_search"] != null)
            {
                getstudentinfo_byMID(room, cup);
            }
            else if (Request.Form["Room_Print"] != null)
            {
                loop_room(room, cup);
            }
            return View();

        }

        string dbfolder = @"\\192.168.1.54\d\";

        //---------------------------------
        //can delecte
        public void getDebitRecords()
        {
            string conn = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source =" + @"D:\student_store_record.accdb; Persist Security Info = False";
            OleDbConnection connection = new OleDbConnection();
            connection.ConnectionString = conn;
            string code = "";
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                //INSERT INTo test(name) values('" + input1 + "');
                string query = "SELECT * from data";
                command.CommandText = query;
                command.ExecuteNonQuery();
                OleDbDataReader reader = command.ExecuteReader();
                string tablehead = @"<table class='table table-hover' id='test'><thead><tr><th>ID</th><th>Member ID</th><th>Name</th><th>Date</th><th>Product Code</th><th>Product Name</th><th>QTY</th><th>Rate</th><th>Total Amout</th></tr></thead>
                    <tbody>";
                string talbeend = "</tbody></table>";

                while (reader.Read())
                {
                    code = code + "<tr>" + "<td>" + reader["idd"].ToString() + "</td>" + "<td>" + reader["Member_Id"].ToString() + "</td>" + "<td>" + reader["Name"].ToString() + "</td>" + "<td  class='date'>" + reader["Datee"].ToString() + "</td>" + "<td>" + reader["product_id"].ToString() + "</td>" + "<td>" + reader["Item"].ToString() + "</td>" + "<td>" + reader["Qty"].ToString() + "</td>" + "<td>" + reader["per_itm"].ToString() + "</td>" + "<td class='val'>" + reader["Amount_debit"].ToString() + "</td>" + "</tr>";

                }
                connection.Close();
                ViewBag.debit = tablehead + code + talbeend;
            }
            catch (Exception ex)
            {
                ViewBag.erroe = ex.ToString();
            }
        }
        //can delecte
        //-----------------------------

        public void getDebitRecords_get(string mid)
        {

            string conn = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source =" + dbfolder + "student_store_record.accdb; Persist Security Info = False";
            OleDbConnection connection = new OleDbConnection();
            connection.ConnectionString = conn;
            string code = "";
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                //INSERT INTo test(name) values('" + input1 + "');
                string query = "SELECT * from data where Member_Id = '" + mid + "' ;";
                command.CommandText = query;
                command.ExecuteNonQuery();
                OleDbDataReader reader = command.ExecuteReader();
                string tablehead = @"<table style='float:left;' class='table table-hover' id='test'><thead><tr><th>ID</th><th>Member ID</th><th>Name</th><th>Date</th><th>Particular</th><th>Amout</th></tr></thead>
                    <tbody>";
                string talbeend = "</tbody></table>";

                while (reader.Read())
                {
                    code = code + "<tr>" + "<td>" + reader["idd"].ToString() + "</td>" + "<td>" + reader["Member_Id"].ToString() + "</td>" + "<td>" + reader["Name"].ToString() + "</td>" + "<td  class='date'>" + reader["Datee"].ToString() + "</td>" + "<td>" + reader["Item"].ToString() + "</td>" + "<td class='val'>" + reader["Amount_debit"].ToString() + " &#x20b9;</td>" + "</tr>";

                }
                connection.Close();
                ViewBag.debit_get = tablehead + code + talbeend;
            }
            catch (Exception ex)
            {
                ViewBag.erroe = ex.ToString();
            }
        }
        public void getCreditRecords_get(string mid)
        {
            string conn = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source =" + dbfolder + "student_credit_record.accdb; Persist Security Info = False";
            OleDbConnection connection = new OleDbConnection();
            connection.ConnectionString = conn;
            string code1 = "";
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                //INSERT INTo test(name) values('" + input1 + "');
                string query = "SELECT * from data where mid = '" + mid + "' ;";
                command.CommandText = query;
                command.ExecuteNonQuery();
                OleDbDataReader reader = command.ExecuteReader();
                string tablehead = @"<table style='float:right;padding-right:16px' class='table table-hover' id = 'test1'><thead><tr><th>Member ID</th><th>Name</th><th>Class</th><th>Date</th><th>Particular</th><th>Amount</th><th>Recepit</th></tr></thead><tbody>";
                string talbeend = "</tbody></table>";

                while (reader.Read())
                {
                    code1 = code1 + "<tr>" + "<td>" + reader["mid"].ToString() + "</td>" + "<td>" + reader["Name"].ToString() + "</td>" + "<td>" + reader["Class"].ToString() + "</td>" + "<td  class='date'>" + reader["Date"].ToString() + "</td>" + "<td>" + reader["via"].ToString() + "<td>" + reader["amount"].ToString() + " &#x20b9;</td>" + "<td>" + reader["reciept"].ToString() + "</td>" + "</tr>";

                }
                connection.Close();
                ViewBag.credit = tablehead + code1 + talbeend;
            }
            catch (Exception ex)
            {
                ViewBag.erroe = ex.ToString();
            }
        }
        public void getstudentinfo_byMID(string room, string cup)
        {
            string room_cup = room + "_" + cup;
            string conn = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source =" + dbfolder + "Database_current.accdb; Persist Security Info = False";
            OleDbConnection connection = new OleDbConnection();
            connection.ConnectionString = conn;
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                //INSERT INTo test(name) values('" + input1 + "');
                string query = "SELECT * from data where Room_cup = '" + room_cup + "' ;";  //Member_Id,Name,Room_cup,Class,room,cup,id
                command.CommandText = query;
                command.ExecuteNonQuery();
                OleDbDataReader reader = command.ExecuteReader();
                string mid = "";
                string name = "";
                string Room_Cup = "";
                string Class = "";
                string Room = "";
                string Cup = "";

                while (reader.Read())
                {
                    mid = reader["Member_Id"].ToString();
                    name = reader["Name"].ToString();
                    Room_Cup = reader["Room_cup"].ToString();
                    Class = reader["Class"].ToString();
                    Room = reader["room"].ToString();
                    Cup = reader["cup"].ToString();

                }
                connection.Close();
                getCreditRecords_get(mid);
                getDebitRecords_get(mid);
                ViewBag.Member_id = mid;
                ViewBag.Name = name;
                ViewBag.Room_Cup = Room_Cup;
                ViewBag.Class = Class;
                ViewBag.Room = Room;
                ViewBag.Cup = Cup;
            }
            catch (Exception ex)
            {
                ViewBag.erroe = ex.ToString();
            }
        }
        public void getstudentinfo(string mid)
        {
            string conn = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source =" + dbfolder + "Database_current.accdb; Persist Security Info = False";
            OleDbConnection connection = new OleDbConnection();
            connection.ConnectionString = conn;
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                //INSERT INTo test(name) values('" + input1 + "');
                string query = "SELECT * from data where Member_Id = '" + mid + "' ;";
                command.CommandText = query;
                command.ExecuteNonQuery();
                OleDbDataReader reader = command.ExecuteReader();
                string name = "";
                string Room_Cup = "";
                string Class = "";
                string Room = "";
                string Cup = "";
                while (reader.Read())
                {
                    mid = reader["Member_Id"].ToString();
                    name = reader["Name"].ToString();
                    Room_Cup = reader["Room_cup"].ToString();
                    Class = reader["Class"].ToString();
                    Room = reader["room"].ToString();
                    Cup = reader["cup"].ToString();
                }
                connection.Close();
                getCreditRecords_get(mid);
                getDebitRecords_get(mid);
                ViewBag.Member_id = mid;
                ViewBag.Name = name;
                ViewBag.Room_Cup = Room_Cup;
                ViewBag.Class = Class;
                ViewBag.Room = Room;
                ViewBag.Cup = Cup;
                connection.Close();
            }
            catch (Exception ex)
            {
                ViewBag.erroe = ex.ToString();
            }
        }
        public void loop_room(string room, string cup)
        {
            string room_cup = room + "_" + cup;
            string conn = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source =" + dbfolder + "Database_current.accdb; Persist Security Info = False";
            OleDbConnection connection = new OleDbConnection();
            connection.ConnectionString = conn;
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                //INSERT INTo test(name) values('" + input1 + "');
                string query = "SELECT * from data where Room_cup = '" + room_cup + "' ;";  //Member_Id,Name,Room_cup,Class,room,cup,id
                command.CommandText = query;
                command.ExecuteNonQuery();
                OleDbDataReader reader = command.ExecuteReader();
                string mid = "";
                string name = "";
                string Room_Cup = "";
                string Class = "";
                string Room = "";
                string Cup = "";

                while (reader.Read())
                {
                    mid = reader["Member_Id"].ToString();
                    name = reader["Name"].ToString();
                    Room_Cup = reader["Room_cup"].ToString();
                    Class = reader["Class"].ToString();
                    Room = reader["room"].ToString();
                    Cup = reader["cup"].ToString();

                }
                connection.Close();
                getCreditRecords_get(mid);
                getDebitRecords_get(mid);
                int Cupbord = Int32.Parse(Cup) + 1;
                int Roomno = Int32.Parse(Room);
                if (Cupbord > 16)
                {
                    Cupbord = 1;
                    if (Roomno == 114 || Roomno == 214 || Roomno == 314 || Roomno == 414)
                    {
                        Roomno = Roomno + 87;
                    }
                    else
                    {
                        Roomno = Int32.Parse(Room) + 1;
                    }

                }
                ViewBag.Member_id = mid;
                ViewBag.Name = name;
                ViewBag.Room_Cup = Room_Cup;
                ViewBag.Class = Class;
                ViewBag.Room = Roomno.ToString();
                ViewBag.Cup = Cupbord.ToString();
            }
            catch (Exception ex)
            {
                ViewBag.erroe = ex.ToString();
            }
        }


        public ActionResult Gurukulite_Store_Balance()
        {
            //if (Session["LogedUserID"] != null)
            //{
                return View();
            //}
            //else
            //{
              //  return View("Index");
            //}

        }

        [HttpPost]
        public ActionResult Gurukulite_Store_Balance(string room)
        {

            return View();
        }

        public ActionResult Student_Store_Statement_css()
        {
            //if (Session["LogedUserID"] != null)
            //{
                return View();
            //}
            //else
            //{
            //    return View("Index");
            //}
        }
        [HttpPost]
        public ActionResult Student_Store_Statement_css(string mid, string room, string cup)
        {
            if (Request.Form["Member_search"] != null)
            {
                getDebitRecords_get(mid);
                getCreditRecords_get(mid);
                getstudentinfo(mid);
            }
            else if (Request.Form["Rooom_search"] != null)
            {
                getstudentinfo_byMID(room, cup);
            }
            else if (Request.Form["Room_Print"] != null)
            {
                loop_room(room, cup);
            }
            return View();
        }


        public ActionResult Home()
        {
            //if (Session["LogedUserID"] != null)
            //{
                return View();
            //}
            //else
            //{
                //return View("Index");
            //}
        }




        public void getDebitRecords_get_balance()
        {

            string conn = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source =" + dbfolder + "student_store_record.accdb; Persist Security Info = False";
            OleDbConnection connection = new OleDbConnection();
            connection.ConnectionString = conn;
            string code = "";
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                //INSERT INTo test(name) values('" + input1 + "');
                string query = "SELECT * from data;";
                command.CommandText = query;
                command.ExecuteNonQuery();
                OleDbDataReader reader = command.ExecuteReader();
                string tablehead = @"<table style='float:left;' class='table table-hover' id='test'><thead><tr><th>ID</th><th>Member ID</th><th>Name</th><th>Date</th><th>Product Name</th><th>Total Amount</th></tr></thead>
                    <tbody>";
                string talbeend = "</tbody></table>";

                while (reader.Read())
                {
                    code = code + "<tr>" + "<td>" + reader["idd"].ToString() + "</td>" + "<td>" + reader["Member_Id"].ToString() + "</td>" + "<td>" + reader["Name"].ToString() + "</td>" + "<td  class='date'>" + reader["Datee"].ToString() + "</td>" + "<td>" + reader["Item"].ToString() + "</td>" + "<td class='val'>" + reader["Amount_debit"].ToString()+"</td>" + "</tr>";

                }
                connection.Close();
                ViewBag.debit_get = tablehead + code + talbeend;
            }
            catch (Exception ex)
            {
                ViewBag.erroe = ex.ToString();
            }
        }
        public void getCreditRecords_get_balance()
        {
            string conn = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source =" + dbfolder + "student_credit_record.accdb; Persist Security Info = False";
            OleDbConnection connection = new OleDbConnection();
            connection.ConnectionString = conn;
            string code1 = "";
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                //INSERT INTo test(name) values('" + input1 + "');
                string query = "SELECT * from data;";
                command.CommandText = query;
                command.ExecuteNonQuery();
                OleDbDataReader reader = command.ExecuteReader();
                string tablehead = @"<table style='float:right;padding-right:16px' class='table table-hover' id = 'test1'><thead><tr><th>Member ID</th><th>Name</th><th>Class</th><th>Date</th><th>amount</th><th>via</th><th>Recepit</th></tr></thead><tbody>";
                string talbeend = "</tbody></table>";

                while (reader.Read())
                {
                    code1 = code1 + "<tr>" + "<td>" + reader["mid"].ToString() + "</td>" + "<td>" + reader["Name"].ToString() + "</td>" + "<td>" + reader["Class"].ToString() + "</td>" + "<td  class='date'>" + reader["Date"].ToString() + "</td>" + "<td>" + reader["amount"].ToString() + "</td>" + "<td>" + reader["via"].ToString() + "</td>" + "<td>" + reader["reciept"].ToString() + "</td>" + "</tr>";

                }
                connection.Close();
                ViewBag.credit = tablehead + code1 + talbeend;
            }
            catch (Exception ex)
            {
                ViewBag.erroe = ex.ToString();
            }
        }


        public ActionResult balance()
        {
            getDebitRecords_get_balance();
            getCreditRecords_get_balance();
            return View();
        }

    }
}
