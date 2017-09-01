using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI_CRUD.CustomResult;
using WebAPI_CRUD.Models;

namespace WebAPI_CRUD.Controllers.api
{
    public class StudController : ApiController
    {
        [HttpGet]
        [Route("FindStudent")]
        public HttpResponseMessage Get(int id)
        {
            Student student = new Student();
            using (var context = new StudentDbEntities())
            {
                student = context.Students.Where(s => s.Id == id).FirstOrDefault();
            }
            if (student == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, id);
            }
            return Request.CreateResponse(HttpStatusCode.OK, student);
        }
        [HttpGet]
        [Route("CheckStudent")]
        public IHttpActionResult GetStudent(int id)
        {
            Student student = new Student();
            using (var context = new StudentDbEntities())
            {
                student = context.Students.Where(s => s.Id == id).FirstOrDefault();
            }
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }
        [HttpGet]
        [Route("CustomStudent")]
        public MyCustomResult GetStudentUsingId(int id)
        {
            Student student = new Student();
            using (var context = new StudentDbEntities())
            {
                student = context.Students.Where(s => s.Id == id).FirstOrDefault();
            }
            if (student == null)
            {
                return new MyCustomResult("not found", Request, HttpStatusCode.NotFound);
            }
            return new MyCustomResult(student.Name, Request,HttpStatusCode.OK);
        }
    }
}