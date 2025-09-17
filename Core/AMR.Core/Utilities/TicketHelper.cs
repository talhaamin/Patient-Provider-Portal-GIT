using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace AMR.Core.Utilities
{
    public static class TicketHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userData"></param>
        /// <param name="persistent"></param>
        /// <returns></returns>
        public static void CreateAuthCookie(string userName, string userData, bool persistent)
        {
            DateTime issued = DateTime.Now;

            HttpCookie AMRAuthCookieCookie = FormsAuthentication.GetAuthCookie(userName, true);
            int formsTimeout = Convert.ToInt32((AMRAuthCookieCookie.Expires - DateTime.Now).TotalMinutes);

            DateTime expiration = DateTime.Now.AddMinutes(formsTimeout);
            string cookiePath = FormsAuthentication.FormsCookiePath;

            var ticket = new FormsAuthenticationTicket(0, userName, issued, expiration, true, userData, cookiePath);
            HttpCookie cookie = CreateAuthCookie(ticket, expiration, persistent);
            if (HttpContext.Current != null)
            {
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
            
        }
        public static GlobalVar SetPatientInformationAndGet(string data)
        {
            GlobalVar objGlobalVar = new GlobalVar();
            if (HttpContext.Current != null)
            {
                if (HttpContext.Current.Request.IsAuthenticated)
                {
                    var cookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
                    if (cookie == null)
                    {
                        return null;
                    }
                    else
                    {


                        var decrypted = FormsAuthentication.Decrypt(cookie.Value);
                        if (!string.IsNullOrEmpty(decrypted.UserData))
                        {
                            objGlobalVar.SetData(decrypted.UserData);
                            objGlobalVar.PatientId = Convert.ToInt64(data.Split(',')[0]);
                            objGlobalVar.PatientName = data.Split(',')[1];
                            if (data.Split(',').Length > 2)
                            {
                                objGlobalVar.PremiumFlag = Convert.ToBoolean(data.Split(',')[2]);
                            }
                            if (data.Split(',').Length > 3)
                            {
                                objGlobalVar.UserLogin = data.Split(',')[3];
                            }
                            if (data.Split(',').Length > 4)
                            {
                                objGlobalVar.FirstLogin = Convert.ToBoolean(data.Split(',')[4]);
                            }
                            if (data.Split(',').Length > 5)
                            {
                                objGlobalVar.ResetPasswordFlag = Convert.ToBoolean(data.Split(',')[5]);
                            }
                            if(data.Split(',').Length >6)
                            {
                            objGlobalVar.UserLoginEx= data.Split(',')[6];
                            }
                            // Store UserData inside the Forms Ticket with all the attributes
                            // in sync with the web.config
                            var newticket = new FormsAuthenticationTicket(decrypted.Version,
                                                  decrypted.Name,
                                                  decrypted.IssueDate,
                                                  decrypted.Expiration,
                                                  true, // always persistent
                                                  objGlobalVar.GetData(),
                                                  decrypted.CookiePath);

                            // Encrypt the ticket and store it in the cookie
                            cookie.Value = FormsAuthentication.Encrypt(newticket);
                            cookie.Expires = newticket.Expiration.AddHours(1);
                            HttpContext.Current.Response.Cookies.Set(cookie);
                              var decrypted1 = FormsAuthentication.Decrypt(cookie.Value);
                              if (!string.IsNullOrEmpty(decrypted1.UserData))
                              {
                                  objGlobalVar.SetData(decrypted1.UserData);
                                  
                                  return objGlobalVar;
                              }
                        }
                    }

                }
            }
            return null;
        }

        public static GlobalVar SetProviderInformationAndGet(string data)
        {
            GlobalVar objGlobalVar = new GlobalVar();
            if (HttpContext.Current != null)
            {
                if (HttpContext.Current.Request.IsAuthenticated)
                {
                    var cookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
                    if (cookie == null)
                    {
                        return null;
                    }
                    else
                    {


                        var decrypted = FormsAuthentication.Decrypt(cookie.Value);
                        if (!string.IsNullOrEmpty(decrypted.UserData))
                        {
                            objGlobalVar.SetData(decrypted.UserData);
                            objGlobalVar.PatientId = Convert.ToInt64(data.Split(',')[0]);
                            objGlobalVar.PatientName = data.Split(',')[1];
                            if (data.Split(',').Length > 3)
                            {
                                objGlobalVar.FirstLogin = Convert.ToBoolean(data.Split(',')[3]);
                            }
                            if (data.Split(',').Length > 4)
                            {
                                objGlobalVar.ResetPasswordFlag = Convert.ToBoolean(data.Split(',')[4]);
                            }
                            // Store UserData inside the Forms Ticket with all the attributes
                            // in sync with the web.config
                            var newticket = new FormsAuthenticationTicket(decrypted.Version,
                                                  decrypted.Name,
                                                  decrypted.IssueDate,
                                                  decrypted.Expiration,
                                                  true, // always persistent
                                                  objGlobalVar.GetData(),
                                                  decrypted.CookiePath);

                            // Encrypt the ticket and store it in the cookie
                            cookie.Value = FormsAuthentication.Encrypt(newticket);
                            cookie.Expires = newticket.Expiration.AddHours(1);
                            HttpContext.Current.Response.Cookies.Set(cookie);
                            var decrypted1 = FormsAuthentication.Decrypt(cookie.Value);
                            if (!string.IsNullOrEmpty(decrypted1.UserData))
                            {
                                objGlobalVar.SetData(decrypted1.UserData);

                                return objGlobalVar;
                            }
                        }
                    }

                }
            }
            return null;
        }
        public static GlobalVar GetUserData()
        {
            GlobalVar objGlobalVar = new GlobalVar();
            if (HttpContext.Current != null)
            {
                if (HttpContext.Current.Request.IsAuthenticated)
                {
                    var cookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
                    if (cookie == null)
                    {
                        return null;
                    }
                    else
                    {
                        var decrypted = FormsAuthentication.Decrypt(cookie.Value);
                        if (!string.IsNullOrEmpty(decrypted.UserData))
                        {
                            objGlobalVar.SetData(decrypted.UserData);
                            return objGlobalVar;
                        }
                    }

                }
            }
            return null;
        }
        public static HttpCookie CreateAuthCookie(FormsAuthenticationTicket ticket, DateTime expiration, bool persistent)
        {
            string Filling = FormsAuthentication.Encrypt(ticket);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, Filling)
                             {
                                 Domain = FormsAuthentication.CookieDomain,
                                 Path = FormsAuthentication.FormsCookiePath
                             };
            if (persistent)
            {
                cookie.Expires = expiration;
            }

            return cookie;
        }
        public static void CreateCareProviderAuthCookie(string userName, string userData, bool persistent,string cookiename)
        {
            DateTime issued = DateTime.Now;

            HttpCookie AMRAuthCookieCookie = FormsAuthentication.GetAuthCookie(userName, true);
            int formsTimeout = Convert.ToInt32((AMRAuthCookieCookie.Expires - DateTime.Now).TotalMinutes);

            DateTime expiration = DateTime.Now.AddMinutes(formsTimeout);
            string cookiePath = FormsAuthentication.FormsCookiePath;

            var ticket = new FormsAuthenticationTicket(0, userName, issued, expiration, true, userData, cookiePath);
            HttpCookie cookie = CreateAuthCookie(ticket, expiration, persistent);
            cookie.Name = cookiename;
            if (HttpContext.Current != null)
            {
                HttpContext.Current.Response.Cookies.Add(cookie);
            }

        }
        public static GlobalVar GetCareProviderUserData()
        {
            GlobalVar objGlobalVar = new GlobalVar();
            if (HttpContext.Current != null)
            {
               // if (HttpContext.Current.Request.IsAuthenticated)
              //  {
                var cookie = HttpContext.Current.Request.Cookies["CareProvider"];
                    if (cookie == null)
                    {
                        return null;
                    }
                    else
                    {
                        var decrypted = FormsAuthentication.Decrypt(cookie.Value);
                        if (!string.IsNullOrEmpty(decrypted.UserData))
                        {
                            objGlobalVar.SetData(decrypted.UserData);
                            return objGlobalVar;
                        }
                   }

               // }
            }
            return null;
        }
        public static GlobalVar GetMedicalSummaryUserData()
        {
            GlobalVar objGlobalVar = new GlobalVar();
            if (HttpContext.Current != null)
            {
                // if (HttpContext.Current.Request.IsAuthenticated)
                //  {
                var cookie = HttpContext.Current.Request.Cookies["MedicalSummary"];
                if (cookie == null)
                {
                    return null;
                }
                else
                {
                    var decrypted = FormsAuthentication.Decrypt(cookie.Value);
                    if (!string.IsNullOrEmpty(decrypted.UserData))
                    {
                        objGlobalVar.SetData(decrypted.UserData);
                        return objGlobalVar;
                    }
                }

                // }
            }
            return null;
        }

        public static GlobalVar GetAdminUserData()
        {
            GlobalVar objGlobalVar = new GlobalVar();
            if (HttpContext.Current != null)
            {
                // if (HttpContext.Current.Request.IsAuthenticated)
                //  {
                var cookie = HttpContext.Current.Request.Cookies[".Admin"];
                if (cookie == null)
                {
                    return null;
                }
                else
                {
                    var decrypted = FormsAuthentication.Decrypt(cookie.Value);
                    if (!string.IsNullOrEmpty(decrypted.UserData))
                    {
                        objGlobalVar.SetData(decrypted.UserData);
                        return objGlobalVar;
                    }
                }

                // }
            }
            return null;
        }
        public static void CreateAuthCookiePatient(string userName, string userData, bool persistent)
        {
            DateTime issued = DateTime.Now;

            HttpCookie AMRAuthCookieCookie = FormsAuthentication.GetAuthCookie(userName, true);
            int formsTimeout = Convert.ToInt32((AMRAuthCookieCookie.Expires - DateTime.Now).TotalMinutes);

            DateTime expiration = DateTime.Now.AddMinutes(formsTimeout);
            string cookiePath = FormsAuthentication.FormsCookiePath;

            var ticket = new FormsAuthenticationTicket(0, userName, issued, expiration, true, userData, cookiePath);
            
            HttpCookie cookie = CreateAuthCookie(ticket, expiration, persistent);
            cookie.Name = ".Patient";
            if (HttpContext.Current != null)
            {
                HttpContext.Current.Response.Cookies.Add(cookie);
            }

        }
    }
}
