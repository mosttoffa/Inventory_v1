﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace Inventory_v1.Models
{
    public class BaseEquipment
    {
       public int EquipmentID { get; set; }
       public string EquipmentName { get; set; }
        public int Quantity { get; set; }
        public int Stock { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime ReceiveDate { get; set; }

        public List<BaseEquipment> ListEquipment()                                       // function er name ValidateMember
        {
            List<BaseEquipment> ListEquipment = new List<BaseEquipment>();               // list object create kora hoise

            string Connstring = ConfigurationManager.ConnectionStrings["Connstring"].ToString();    // Connection string ta amra web.config file theke nitechi

            //sql connection
            SqlConnection sqlConnection = new SqlConnection(Connstring);  // sql connection object create kora hoise and connection string pass kora hoise
            sqlConnection.Open();                                         // connection open kora hoise 

            // sokol prokar query, calculation amra stored procedure e korbo, database level e korbo  

            //string CommandText = "sp_LstEquipments";   // stored procedure e korte caile 

            string CommandText = "select * from Equipments";
            //sql command
            SqlCommand cmd = new SqlCommand(CommandText, sqlConnection);  // sql command object create kora hoise
            cmd.CommandTimeout = 0;                                       // command timeout 0 kora hoise 
            cmd.CommandType = CommandType.Text;                           // command type text kora hoise
            cmd.Parameters.Clear();                                       // parameter clear kora hoise 

            // List akare nite caile 
            SqlDataReader reader = cmd.ExecuteReader();                   // sql data reader object create kora hoise 
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    // amra jodi kono kaj korte chai tahole ei block e kaj korbo
                    BaseEquipment objBaseEquipment = new BaseEquipment();       // base member class er object create kora hoise
                    objBaseEquipment.EquipmentID = Convert.ToInt32(reader["EquipmentID"].ToString());     // EquipmentID er value set kora hoise
                    objBaseEquipment.EquipmentName = reader["EquipmentName"].ToString();                  // EquipmentName er value set kora hoise
                    objBaseEquipment.Quantity = Convert.ToInt32(reader["Quantity"].ToString());           // Quantity er value set kora hoise
                    objBaseEquipment.Stock = Convert.ToInt32(reader["Stock"].ToString());                            // Stock er value set kora hoise
                    objBaseEquipment.EntryDate = Convert.ToDateTime(String.IsNullOrEmpty(reader["EntryDate"].ToString()) ? "1900-01-01" : reader["EntryDate"].ToString());   // EntryDate er value set kora hoise
                    objBaseEquipment.ReceiveDate = Convert.ToDateTime(String.IsNullOrEmpty(reader["ReceiveDate"].ToString()) ? "1900-01-01" : reader["ReceiveDate"].ToString());  // ReceiveDate er value set kora hoise , eivabe code korle empty value thakleo error asbena 

                    ListEquipment.Add(objBaseEquipment);
                }
            }

            cmd.Dispose();          // command dispose kora hoise
            sqlConnection.Close();  // connection close kora hoise


            return ListEquipment;  // username and password admin na hole false return korbe

        }


    }
}