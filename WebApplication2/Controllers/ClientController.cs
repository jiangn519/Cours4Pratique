using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class ClientController : Controller
    {
        // GET: Client
        public ActionResult Index()
        {
            string chaine = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=DemoClient;Integrated Security=True";
            SqlConnection sqlConnection1 = new SqlConnection(chaine);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;


            cmd.CommandText = "getListeClients";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();

            reader = cmd.ExecuteReader();
            List<Client> result = new List<Client>();
            while (reader.Read())
            {
                Client c = new Client()
                {
                    ID = Convert.ToInt32(reader["id"].ToString()),
                    Nom = reader["nom"].ToString(),

                };
                result.Add(c);
            }
            sqlConnection1.Close();
            return View(result);
        }

        // GET: Client/Details/5
        public ActionResult Details(int id)
        {
            string chaine = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=DemoClient;Integrated Security=True";
            SqlConnection sqlConnection1 = new SqlConnection(chaine);
            SqlCommand cmd = new SqlCommand("detailClient",sqlConnection1);
            SqlDataReader reader;
            

            
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@id", id));

            
            sqlConnection1.Open();

            reader = cmd.ExecuteReader();

            Client result = new Client();
            if (reader.HasRows)
            {
               
                reader.Read();
                result.ID = Convert.ToInt32(reader["id"].ToString());
                result.Nom= reader["nom"].ToString();
                sqlConnection1.Close();
                return View(result);

            }
            else
            {
                sqlConnection1.Close();
                return RedirectToAction("Index");
            }
            


           
        }

        // GET: Client/Create
        public ActionResult Create()
        {
            Client c = new Client();
            return View(c);
        }

        // POST: Client/Create
        [HttpPost]
        public ActionResult Create(Client c)
        {
            try
            {
                string chaine = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=DemoClient;Integrated Security=True";
                SqlConnection sqlConnection1 = new SqlConnection(chaine);
                SqlCommand cmd = new SqlCommand("newClient", sqlConnection1);

                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.Add(new SqlParameter("@id", c.ID));
                cmd.Parameters.Add(new SqlParameter("@nom", c.Nom));

                sqlConnection1.Open();

                int nb = cmd.ExecuteNonQuery();

                return RedirectToAction("Index");


            }
            catch
            {
                return View();
            }
        }

        // GET: Client/Edit/5
        public ActionResult Edit(int id)
        {
            string chaine = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=DemoClient;Integrated Security=True";
            SqlConnection sqlConnection1 = new SqlConnection(chaine);
            SqlCommand cmd = new SqlCommand("detailClient", sqlConnection1);
            SqlDataReader reader;



            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@id", id));


            sqlConnection1.Open();

            reader = cmd.ExecuteReader();

            Client result = new Client();
            if (reader.HasRows)
            {

                reader.Read();
                result.ID = Convert.ToInt32(reader["id"].ToString());
                result.Nom = reader["nom"].ToString();
                sqlConnection1.Close();
                return View(result);

            }
            else
            {
                sqlConnection1.Close();
                return RedirectToAction("Index");
            }


        }

        // POST: Client/Edit/5
        [HttpPost]
        public ActionResult Edit(Client c)
        {
            
            try
            {
                string chaine = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=DemoClient;Integrated Security=True";
                SqlConnection sqlConnection1 = new SqlConnection(chaine);
                SqlCommand cmd = new SqlCommand("updateClient", sqlConnection1);
                
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id", c.ID));
                cmd.Parameters.Add(new SqlParameter("@nom", c.Nom));

                sqlConnection1.Open();

                int nb = cmd.ExecuteNonQuery();

                return RedirectToAction("Index");


            }
            catch
            {
                return View();
            }
        }

        // GET: Client/Delete/5
        public ActionResult Delete(int id)
        {

            Client c = new Client();
            return View(c);
        }

        // POST: Client/Delete/5
        [HttpPost]
        public ActionResult Delete(Client c)
        {
            try
            {
                string chaine = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=DemoClient;Integrated Security=True";
                SqlConnection sqlConnection1 = new SqlConnection(chaine);
                SqlCommand cmd = new SqlCommand("deleteClient", sqlConnection1);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id", c.ID));
                

                sqlConnection1.Open();

                int nb = cmd.ExecuteNonQuery();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
