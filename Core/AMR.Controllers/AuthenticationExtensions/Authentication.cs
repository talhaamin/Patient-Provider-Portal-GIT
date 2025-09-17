// Class Name    : Authentication.cs
// Date Created  : 01/09/2014
// Written By    : Stephen Farkas
// Version       : 2013.01
// Description   : Used to call authentication web services
// MM/DD/YYYY XXX Description
//
//------------------------------------------------------------------------
//  Set Options & Imports
//------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AMR.Controllers.AuthenticationExtensions
{
    public class Authentication
    {
        public void LogOff(Int64 UserId)
        {
            DataAuthenticationService.AuthenticationResponse ResponseData = new DataAuthenticationService.AuthenticationResponse();

            try
            {
                using (var service = new DataAuthenticationService.AuthenticationWSSoapClient())
                {
                    ResponseData = service.LogoutUser(0, UserId);

                }
            }

            catch (Exception ex)
            {

            }

        }


    }
}
