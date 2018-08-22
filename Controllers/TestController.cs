using System;
using System.Web.Mvc;
using NHibernate;
using NHibernate.Linq;
using System.Linq;
using nHibernateApp.Models;
using System.Web.Http;
using nHibernateApp.Models.NHibernate;
using nHibernateApp.Models.Requests;

namespace nHibernateApp.Controllers
{
   [System.Web.Http.RoutePrefix("test/api")]
   public class TestController : ApiController
    {
     
      [System.Web.Http.HttpGet]
      [System.Web.Http.Route("list")]
      public IHttpActionResult GetEmployees(int numberOfEntries = 50)
      {
         using (ISession session = NHibernateSession.OpenSession())
         {
            var employees = session.Query<Employee>()
               .Take(numberOfEntries)
               .ToList();
            return Ok(employees);
         }
      }

      [System.Web.Http.HttpGet]
      [System.Web.Http.Route("getbyname")]
      public IHttpActionResult GetEmployeesByName(string firstName)
      {
         using (ISession session = NHibernateSession.OpenSession())
         {
            var employees = session.Query<Employee>().Where(e => e.FirstName == firstName)
               .ToList();
            return Ok(employees);
         }
      }

      [System.Web.Http.HttpPost]
      [System.Web.Http.Route("create")]
      public IHttpActionResult PostCreate(EmployeeCreate employeeCreate)
      {
         var employee = new Employee
         {
            FirstName = employeeCreate.FirstName,
            LastName = employeeCreate.LastName,
            Designation = employeeCreate.Designation
         };

         using (ISession session = NHibernateSession.OpenSession())
         {
            using (ITransaction transaction = session.BeginTransaction())
            {
               session.SaveOrUpdate(employee);
               transaction.Commit();
               return Ok();
            }
         }

      }

   }
}
