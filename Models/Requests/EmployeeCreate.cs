using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace nHibernateApp.Models.Requests
{
   public class EmployeeCreate
   {
      public string FirstName { get; set; }
      public string LastName { get; set; }
      public string Designation { get; set; }
   }
}