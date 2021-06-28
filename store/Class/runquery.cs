using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Security.Claims;
using System.Data;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Data.Common;

namespace store.Class{   
    public class runquery{
        public string defaultConnection;
        public runquery(){}
        public Object result(string query){
            SqlConnection conn = new SqlConnection(defaultConnection);
            SqlDataReader rdr = null;
            Object data = null;
            try{
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                rdr = cmd.ExecuteReader();
                var rows = this.ConvertToDictionary(rdr);
                data = JsonConvert.SerializeObject(rows, Formatting.Indented);
                if (rdr != null){
                    rdr.Close();
                }
            }
            finally{
                if (rdr != null){
                    rdr.Close();
                }
                if (conn != null){
                    conn.Close();
                }
            }
            return data;
        }
        private IEnumerable<Dictionary<string, object>> ConvertToDictionary(IDataReader reader){
            var columns = new List<string>();
            var rows = new List<Dictionary<string, object>>();
            for (var i = 0; i < reader.FieldCount; i++){
                columns.Add(reader.GetName(i));
            }
            while (reader.Read()){
                rows.Add(columns.ToDictionary(column => column, column => reader[column]));
            }
            return rows;
        }

        public Int64 addToCart(Int64 shoppingCartId, Int64 productId, Int64 userId, Int64 units)
        {
            Int64 unitsAvailable = 0;
            SqlConnection conn = new SqlConnection(defaultConnection);
            conn.Open();
            SqlCommand command = conn.CreateCommand();
            SqlTransaction transaction;
            StringBuilder s = new StringBuilder();
            transaction = conn.BeginTransaction("insertTransaction");
            command.Connection = conn;
            command.Transaction = transaction;
            try
            {
                command.CommandText = "SELECT units_available FROM [dbo].[products] WHERE product_id=" + productId.ToString();
                unitsAvailable = (Convert.ToInt64(command.ExecuteScalar()))- units;
                if (unitsAvailable >= 0)
                {
                    command.CommandText = "UPDATE [dbo].[products] SET units_available="+unitsAvailable.ToString()+" WHERE product_id="+ productId.ToString();
                    command.ExecuteNonQuery();
                    if (shoppingCartId == 0)
                    {
                        command.CommandText = "SELECT TOP 1 shopping_cart_id FROM [dbo].[shopping_cart] WHERE [user_id]=" + userId.ToString() + " ORDER BY shopping_cart_id DESC";
                        shoppingCartId = (Convert.ToInt64(command.ExecuteScalar()) + 1);
                    }
                    command.CommandText = "INSERT INTO [dbo].[shopping_cart] (shopping_cart_id,product_id,[user_id],units) VALUES(" + shoppingCartId.ToString() + "," + productId.ToString() + "," + userId.ToString() + "," + units.ToString() + ");";
                    command.ExecuteNonQuery();
                    transaction.Commit();
                }
            }
            catch (Exception e)
            {
                transaction.Rollback();
            }
            if (conn != null)
            {
                conn.Close();
            }
            return shoppingCartId;
        }
        public Int64 processOrder(Int64 shoppingCartId, Int64 userId)
        {
            Int64 orderId= 0;
            float amount=0;
            SqlConnection conn = new SqlConnection(defaultConnection);
            conn.Open();
            SqlCommand command = conn.CreateCommand();
            SqlTransaction transaction;
            StringBuilder s = new StringBuilder();
            transaction = conn.BeginTransaction("insertTransaction");
            command.Connection = conn;
            command.Transaction = transaction;
            try
            {
                command.CommandText = ("SELECT SC.product_id, SC.units,P.price,P.discounts FROM [dbo].[shopping_cart] SC  INNER JOIN [dbo].[products] P ON SC.product_id=P.product_id  WHERE SC.shopping_cart_id="+shoppingCartId.ToString()+" AND SC.[user_id]="+userId.ToString());
                command.CommandType = CommandType.Text;
                DbDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Int64 units = Int64.Parse((reader["units"]).ToString());
                        float price = float.Parse((reader["price"]).ToString());
                        int discount = int.Parse((reader["discounts"]).ToString());
                        amount += (price-price*(discount/100))*units;
                    }
                }
                reader.Close();
                string author = authorization();
                command.CommandText = "INSERT INTO [dbo].[orders] (shopping_cart_id,amount,[authorization],[status]) VALUES("+shoppingCartId.ToString()+","+amount.ToString()+",'"+ author + "',1) SELECT SCOPE_IDENTITY()";
                orderId = (Convert.ToInt64(command.ExecuteScalar()));
                transaction.Commit();
            }
            catch (Exception e)
            {
                transaction.Rollback();
            }
            if (conn != null)
            {
                conn.Close();
            }
            return orderId;
        }

        public string authorization()
        {
            Random rdn = new Random();
            string parameters = "QWERTYUIOPASDFGHJKLÑZXCVBNM1234567890";
            int l = parameters.Length;
            char letter;
            int authorizationLength = 6;
            string author = string.Empty;
            for (int i = 0; i < authorizationLength; i++)
            {
                letter = parameters[rdn.Next(l)];
                author += letter.ToString();
            }
            return author;
        }
    }
}