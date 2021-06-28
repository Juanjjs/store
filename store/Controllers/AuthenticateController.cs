using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using store.Class;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using store.Class;

namespace store.Controllers
{
    [Route("api/[Controller]/[Action]")]
   
    public class AuthenticateController : Controller
    {
        public runquery runquery;
        public IConfiguration Configuration { get; }
       
        public string defaultConnection;
        public AuthenticateController(IConfiguration _config) {
            Configuration = _config;
            defaultConnection = Configuration.GetConnectionString("DefaultConnection");
            this.runquery = new runquery();
            this.runquery.defaultConnection = defaultConnection;

        }

        [HttpPost]
        public async Task<IActionResult> personalInformation(Int64 userId)
        {
            Object objError = null;
            String dataError;
            Object data = null;
            try
            {
                string query = "SELECT * FROM [dbo].[personal_information] WHERE [user_id]=" + userId.ToString();
                data = this.runquery.result(query);
                return Ok(data);
            }
            catch (Exception e)
            {
                dataError = "{\"error\":true,\"message\":\"Error al traer la informacion del usuario\"}";
                objError = JsonConvert.DeserializeObject(dataError);
                return Unauthorized(objError);
            }
        }
        [HttpPost]
        public async Task<IActionResult> listProducts()
        {
            Object objError = null;
            String dataError;
            Object data = null;
            try
            {
                string query = "SELECT * FROM [dbo].[products]";
                data = this.runquery.result(query);
                return Ok(data);
            }
            catch (Exception e)
            {
                dataError = "{\"error\":true,\"message\":\"Error al traer la lista de productos\"}";
                objError = JsonConvert.DeserializeObject(dataError);
                return Unauthorized(objError);
            }
        }
        [HttpPost]
        public async Task<IActionResult> addToCart(Int64 shoppingCartId, Int64 productId, Int64 userId, Int64 units)
        {
            Object data = null;
            string json = "";
            bool operationResult;
            try
            {
                shoppingCartId = this.runquery.addToCart(shoppingCartId, productId, userId, units);
                operationResult = (shoppingCartId > 0);
            }
            catch (Exception e)
            {
                operationResult = false;
            }
            if (operationResult)
            {
                json = "{\"error\":false,\"message\":\"Producto agregado al carrito!\",\"shoppingCartId\":" + shoppingCartId.ToString() + "}";
            }
            else
            {
                json = "{\"error\":true,\"message\":\"Error al agregar el producto en el carrito\",\"shoppingCartId\":" + shoppingCartId.ToString() + "}";
            }
            data = JsonConvert.DeserializeObject(json); ;
            return Ok(data);
        }
        [HttpPost]
        public async Task<IActionResult> getCart(Int64 shoppingCartId, Int64 userId)
        {
            Object objError = null;
            String dataError;
            Object data = null;
            try
            {
                string query = "SELECT SC.product_id,P.product_name,SC.units,SC.creation_date FROM [dbo].[shopping_cart] SC INNER JOIN [dbo].[products] P ON SC.product_id=P.product_id WHERE SC.shopping_cart_id="+shoppingCartId.ToString()+" AND SC.[user_id]=" + userId.ToString();
                data = this.runquery.result(query);
                return Ok(data);
            }
            catch (Exception e)
            {
                dataError = "{\"error\":true,\"message\":\"Error al traer el carrito de compra\"}";
                objError = JsonConvert.DeserializeObject(dataError);
                return Unauthorized(objError);
            }
        }
        [HttpPost]
        public async Task<IActionResult> processOrder(Int64 shoppingCartId, Int64 userId)
        {
            Int64 orderId = 0;
            Object data = null;
            string json = "";
            bool operationResult;
            try
            {
                orderId = this.runquery.processOrder(shoppingCartId, userId);
                operationResult = (orderId > 0);
            }
            catch (Exception e)
            {
                operationResult = false;
            }
            if (operationResult)
            {
                json = "{\"error\":false,\"message\":\"Orden existosa!\",\"numberOrder\":" + orderId.ToString() + "}";
            }
            else
            {
                json = "{\"error\":true,\"message\":\"Error al momento de procesar la orden\",\"numberOrder\":" + orderId.ToString() + "}";
            }
            data = JsonConvert.DeserializeObject(json); ;
            return Ok(data);
        }
        [HttpPost]
        public async Task<IActionResult> orderDetails(Int64 orderId)
        {
            Object objError = null;
            String dataError;
            Object data = null;
            try
            {
                string query = "SELECT DISTINCT O.orders_id, O.[authorization], O.amount,P.username,P.shipping_address,P.payment_method,P.email,P.card_number,O.creation_date FROM [dbo].[orders] O INNER JOIN [dbo].[shopping_cart] SC ON O.shopping_cart_id=SC.shopping_cart_id INNER JOIN [dbo].[personal_information] P ON SC.[user_id]=P.[user_id] WHERE orders_id="+ orderId.ToString();
                data = this.runquery.result(query);
                return Ok(data);
            }
            catch (Exception e)
            {
                dataError = "{\"error\":true,\"message\":\"Error al traer los detalles del pedido\"}";
                objError = JsonConvert.DeserializeObject(dataError);
                return Unauthorized(objError);
            }
        }
    }
}