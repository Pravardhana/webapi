using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class ShopController : ApiController
    {
        private DbModel db = new DbModel();

        public IEnumerable<shop> Get()
        {
            return db.shops.ToList();
        }

        public HttpResponseMessage Get(int id)
        {
            var entity = db.shops.FirstOrDefault(e => e.Id == id);
            if (entity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, entity);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "product with Id " + id.ToString() + "not found");
            }
        }
        
        public HttpResponseMessage Post([FromBody]shop sh)
        {
            try
            {
                db.shops.Add(sh);
                db.SaveChanges();
                var message = Request.CreateResponse(HttpStatusCode.Created, sh);
                return message;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        public HttpResponseMessage Delete(int id)
        {
            try
            {
                var entity = db.shops.FirstOrDefault(e => e.Id == id);
                if (entity == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "product with Id " + id.ToString() + "not found");
                }
                else
                {
                    db.shops.Remove(entity);
                    db.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }

            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Put(int id, [FromBody]shop sh)
        {
            try
            {
                var entity = db.shops.FirstOrDefault(e => e.Id == id);
                if (entity == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "product with Id " + id.ToString() + "not found to update");

                }
                else
                {

                    entity.Name = sh.Name;
                    entity.Description = sh.Description;
                    entity.Price = sh.Price;
                    db.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                }
               catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }


        }


        }

    }


        




