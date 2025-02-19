﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOTEL_MANAGMENT.Classes
{
    internal class CarLocation_Class
    {
        DateTime DateDebut { get; set; }
        DateTime DateFin { get; set; }
        Boolean isdisponible { get; set; }
       public int id_carR { get; set;}
      public  Car_Class car { get; set; }
        int nbrjour { get;set; }
        private static SqlCommand cmd;
        private static Connection_Classe cn;


        public CarLocation_Class(int id_carR,DateTime dateDebut, DateTime dateFin, bool isdisponible, Car_Class car)
        {
            DateDebut = dateDebut;
            DateFin = dateFin;
            this.isdisponible = isdisponible;
            this.id_carR = id_carR;
            this.car = car;
            int nbrjour = Math.Abs((DateFin - DateDebut).Days);
            
            cn = new Connection_Classe();
        }
        public CarLocation_Class( DateTime dateDebut, DateTime dateFin, bool isdisponible, Car_Class car)
        {
            DateDebut = dateDebut;
            DateFin = dateFin;
            this.isdisponible = isdisponible;
            MessageBox.Show("" + dateDebut);
            this.car = car;
            int nbrjour = Math.Abs((DateFin - DateDebut).Days);
            cn = new Connection_Classe();
        }

        public void AjouterCarLocationReservation()
        {
            SqlConnection cnx = cn.GetConnection();
            cnx.Open();
            string query = "insert into Car_Location (Debut_Location,Fin_Location,id_Car,disponibilite) values(@dateDebut,@dateFin,@id_Car,@disponibilite)";
            cmd = new SqlCommand(query, cnx);
            cmd.Parameters.AddWithValue("@dateDebut", this.DateDebut);
            cmd.Parameters.AddWithValue("@dateFin", this.DateFin);
            cmd.Parameters.AddWithValue("@id_Car", this.car.id);
            cmd.Parameters.AddWithValue("@disponibilite", 1);

            try
            {

                int row = cmd.ExecuteNonQuery();
                MessageBox.Show("Car successefuly added!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERREUR" + ex);

            }
            cnx.Close();
            updateId();
        }

         public void updateId()
        {

           
            try
            {
                cn = new Connection_Classe();
                SqlConnection cnx = cn.GetConnection();
                String query = "select id from Car_Location where Debut_Location=@Debut_Location and Fin_Location=@Fin_Location and id_Car=@id_Car";
                cmd = new SqlCommand(query, cnx);
                cmd.Parameters.AddWithValue("@Debut_Location", this.DateDebut);
                cmd.Parameters.AddWithValue("@Fin_Location", this.DateFin);
                cmd.Parameters.AddWithValue("@id_Car", this.car.id);

                cnx.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {


                    this.id_carR=int.Parse(reader["id"].ToString());

                }
                cnx.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERREUR Car " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          
        }
    }
}
