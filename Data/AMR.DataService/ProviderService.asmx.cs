// Service Name  : ProviderService
// Date Created  : 10/26/2013
// Written By    : Stephen Farkas
// Version       : 2013.01
// Description   : Web Service For Provider Information
// MM/DD/YYYY XXX Description                
// 02/03/2014 SJF Added Provider Image
// 12/11/2014 SJF Added Provider Search 
// 12/11/2014 SJF Added ActivateProvider & DeactivateProvider
// 01/05/2015 SJF Added GetProviderAdminData
// 01/28/2015 SJF Added functionality to Patient Search
//------------------------------------------------------------------------
//  Set Options & Imports
//------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using AMR.Data;

namespace AMR.DataService
{
    /// <summary>
    /// Summary description for ProviderService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/", Name = "ProviderWS")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ProviderService : System.Web.Services.WebService
    {
        #region Data Structure
        //------------------------------------------------------------------------
        // Define Data Structure
        //------------------------------------------------------------------------

        public struct ProviderData
        {
            public bool Valid;
            public string ErrorMessage;
            public Int64 ProviderId;
            public Int64 PracticeId;
            public Int64 UserId;
            public string Title;
            public string FirstName;
            public string MiddleName;
            public string LastName;
            public string DEA;
            public string License;
            public string Phone;
            public string Email;
        }

        public struct ProviderAdminData
        {
            public bool Valid;
            public string ErrorMessage;
            public Int64 ProviderId;
            public Int64 PracticeId;
            public string PracticeName;
            public string Title;
            public string FirstName;
            public string MiddleName;
            public string LastName;
            public string DEA;
            public string License;
            public string Phone;
            public string Email;
            public string Password;
            public bool Active;  //added by Talha
        }
        public struct ProviderParms
        {
            public int Option;
            public Int64 ProviderId;
            public Int64 FacilityId;
        }
        public struct ProviderResponse
        {
            public bool Valid;
            public string ErrorMessage;
        }
        public struct ProviderTableData
        {
            public bool Valid;
            public string ErrorMessage;
            public DataTable dt;
            public Int64 count;
        }

        public struct ProviderImageData
        {
            public bool Valid;
            public string ErrorMessage;
            public Int64 ProviderId;
            public byte[] Image;
            public string ImageFormat;
        }

        public struct ProviderSearchParms
        {
            public Int64 ProviderId;
            public string FirstName;
            public string LastName;
            public string License;
            public string Phone;
            public string EMail;
            public Nullable<System.Int32> FacilityId; 
            public Nullable<System.Int32> EMRSystemId;
            public Nullable<System.Int32> PageNumber; 
            public Nullable<System.Int32> PageSize;
            public Nullable<System.Int64> TotalRecord;
        }
        #endregion

        #region Get Provider Data
        //------------------------------------------------------------------------
        // Get Provider Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get Provider Data")]
        public ProviderData GetProviderData(ProviderParms Parms, string Token, Int64 UserId, Int64 FacilityId)
        {
            ProviderData Provider = new ProviderData();
            Provider.Valid = true;
            Provider.ErrorMessage = "";

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())
                    {
                        Provider results = db.Providers.First(p => p.ProviderId == Parms.ProviderId);

                        if (results != null)
                        {
                            Provider.ProviderId = results.ProviderId;
                            Provider.Valid = true;
                            Provider.ErrorMessage = "";
                            Provider.Title = results.Title;
                            Provider.ProviderId = results.ProviderId;
                            Provider.FirstName = results.FirstName;
                            Provider.MiddleName = results.MiddleName;
                            Provider.LastName = results.LastName;
                            Provider.DEA = results.DEA;
                            Provider.License = results.License;
                            Provider.Phone = results.Phone;
                            Provider.Email = results.Email;
                        }
                        else
                        {
                            Provider.Valid = false;
                            Provider.ErrorMessage = "Could not read record.";
                        }
                    }
                }
                catch (Exception ex)
                {
                    Provider.Valid = false;
                    Provider.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                Provider.Valid = false;
                Provider.ErrorMessage = "Invalid Token";
            }
            return Provider;
        }
        #endregion

        #region Get Provider Admin Data
        //------------------------------------------------------------------------
        // Get Provider Admin Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get Provider Admin Data")]
        public ProviderAdminData GetProviderAdminData(ProviderParms Parms, string Token, Int64 UserId, Int64 FacilityId)
        {
            ProviderAdminData Provider = new ProviderAdminData();
            Provider.Valid = true;
            Provider.ErrorMessage = "";

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())
                    {
                        Provider results = db.Providers.First(p => p.ProviderId == Parms.ProviderId);

                        if (results != null)
                        {
                            Provider.ProviderId = results.ProviderId;
                            Provider.Valid = true;
                            Provider.ErrorMessage = "";
                            Provider.Title = results.Title;
                            Provider.ProviderId = results.ProviderId;
                            Provider.PracticeId = results.PracticeId;
                            Provider.FirstName = results.FirstName;
                            Provider.MiddleName = results.MiddleName;
                            Provider.LastName = results.LastName;
                            Provider.Title = results.Title;
                            Provider.DEA = results.DEA;
                            Provider.License = results.License;
                            Provider.Phone = results.Phone;
                            Provider.Email = results.Email;

                            Practice pResult = db.Practices.FirstOrDefault(c => c.PracticeId == Provider.PracticeId);
                            if (pResult != null)
                                Provider.PracticeName = pResult.PracticeName;
                            else
                                Provider.PracticeName = "";

                            User uResults = db.Users.FirstOrDefault(u => u.UserRoleLink == Parms.ProviderId && u.UserRoleId == 4);

                            if (uResults != null)
                            {
                                Provider.Active =Convert.ToBoolean( uResults.Enabled); ///add by Talha
                                Provider.Password = clsEncryption.Decrypt(uResults.Password, "AMRP@ss");
                            }
                        }
                        else
                        {
                            Provider.Valid = false;
                            Provider.ErrorMessage = "Could not read record.";
                        }
                    }
                }
                catch (Exception ex)
                {
                    Provider.Valid = false;
                    Provider.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                Provider.Valid = false;
                Provider.ErrorMessage = "Invalid Token";
            }
            return Provider;
        }
        #endregion

        #region Save Provider Data
        //------------------------------------------------------------------------
        // Save Provider Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Save Provider Data")]
        public ProviderResponse SaveProviderData(ProviderData ProviderData, string Token, Int64 UserId, Int64 FacilityId)
        {
            ProviderResponse ProvResponse = new ProviderResponse();
            ProvResponse.Valid = true;
            ProvResponse.ErrorMessage = "";

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())
                    {
                        if (ProviderData.ProviderId == 0)
                        {
                            // Add Provider
                            var NewProvider = new Provider()
                            {
                                PracticeId = ProviderData.PracticeId,
                                UserId = 0,         // Add This Back After Creating User
                                Title = ProviderData.Title,
                                FirstName = ProviderData.FirstName,
                                MiddleName = ProviderData.MiddleName,
                                LastName = ProviderData.LastName,
                                DEA = ProviderData.DEA,
                                License = ProviderData.License,
                                Phone = ProviderData.Phone,
                                Email = ProviderData.Email,
                                UserId_Created = UserId,
                                DateCreated = System.DateTime.Now,
                                UserId_Modified = UserId,
                                DateModified = System.DateTime.Now,
                            };

                            db.Providers.Add(NewProvider);

                            db.SaveChanges();

                            ProviderData.ProviderId = NewProvider.ProviderId;  // Copy Back Newly Generated ProviderId

                            // Generate a random password and Encrypt it
                            Random randomNumber = new Random();
                            string passclear = string.Empty;
                            for (int i = 0; i < 8; i++)
                            {
                                passclear += Convert.ToChar(Convert.ToInt32(Math.Floor(26 * randomNumber.NextDouble() + 65))).ToString();

                            }
                            string passencr = clsEncryption.Encrypt(passclear, "AMRP@ss");

                            // Add User
                            var NewUser = new User()
                            {

                                UserLogin = ProviderData.Email,
                                UserEmail = ProviderData.Email,
                                Password = passencr,
                                UserRoleId = 4,
                                UserRoleLink = NewProvider.ProviderId,
                                Enabled = true,
                                Locked = false,
                                ResetPassword = true,
                            };
                            db.Users.Add(NewUser);

                            db.SaveChanges();

                            // Add Provider Web Settings - First Use
                            var NewWebSetting = new ProviderWebSetting()
                            {

                                ProviderId = NewProvider.ProviderId,
                                EmailNotifyNewMessage = false,
                                PictureLocation = "",
                            };
                            db.ProviderWebSettings.Add(NewWebSetting);
                            db.SaveChanges();

                            Provider ProviderResp = db.Providers.FirstOrDefault(p => p.ProviderId == ProviderData.ProviderId);

                            if (ProviderResp != null)
                            {
                                ProviderResp.UserId = NewUser.UserId;
                                db.SaveChanges();
                            }
                        }
                        else
                        {
                            Provider ProviderResp = db.Providers.First(p => p.ProviderId == ProviderData.ProviderId);

                            if (ProviderResp != null)
                            {
                                ProviderResp.Title = ProviderData.Title;
                                ProviderResp.FirstName = ProviderData.FirstName;
                                ProviderResp.MiddleName = ProviderData.MiddleName;
                                ProviderResp.LastName = ProviderData.LastName;
                                ProviderResp.DEA = ProviderData.DEA;
                                ProviderResp.License = ProviderData.License;
                                ProviderResp.Phone = ProviderData.Phone;
                                ProviderResp.Email = ProviderData.Email;
                                ProviderResp.UserId_Modified = UserId;
                                ProviderResp.DateModified = System.DateTime.Now;

                                db.SaveChanges();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    ProvResponse.Valid = false;
                    ProvResponse.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                ProvResponse.Valid = false;
                ProvResponse.ErrorMessage = "Invalid Token";
            }
            return ProvResponse;
        }
        #endregion

        #region Get List Of Providers For Practice
        //------------------------------------------------------------------------
        // Get List Of Providers For Practice
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get List Of Providers For Practice")]
        public ProviderTableData GetPracticeList(Int64 PracticeId, string Token, Int64 UserId, Int64 FacilityId)
        {
            ProviderTableData ProviderList = new ProviderTableData();

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())
                    {
                        var results = from p in db.Providers
                                      where p.PracticeId == PracticeId
                                      select new
                                      {
                                          p.ProviderId,
                                          p.FirstName,
                                          p.LastName
                                      };
                        ProviderList.dt = clsTableConverter.ToDataTable(results);

                        ProviderList.dt.TableName = "Providers";

                        ProviderList.Valid = true;
                        ProviderList.ErrorMessage = "";
                    }
                }
                catch (Exception ex)
                {
                    ProviderList.Valid = false;
                    ProviderList.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                ProviderList.Valid = false;
                ProviderList.ErrorMessage = "Invalid Token";
            }
            return ProviderList;
        }
        #endregion

        #region Get List Of Providers For Facility
        //------------------------------------------------------------------------
        // Get List Of Providers For Facility
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get List Of Providers For Facility")]
        public ProviderTableData GetFacilityList(Int64 CkFacilityId, string Token, Int64 UserId, Int64 FacilityId)
        {
            ProviderTableData ProviderList = new ProviderTableData();

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())
                    {
                        var results = from p in db.vwProvidersForFacilities
                                      where p.FacilityId == CkFacilityId
                                      select new
                                      {
                                          p.ProviderId,
                                          p.FirstName,
                                          p.LastName
                                      };
                        ProviderList.dt = clsTableConverter.ToDataTable(results);

                        ProviderList.dt.TableName = "Providers";

                        ProviderList.Valid = true;
                        ProviderList.ErrorMessage = "";
                    }
                }
                catch (Exception ex)
                {
                    ProviderList.Valid = false;
                    ProviderList.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                ProviderList.Valid = false;
                ProviderList.ErrorMessage = "Invalid Token";
            }
            return ProviderList;
        }
        #endregion


        //Added By Talha
        #region Get List Of Facilities For Providers
        //------------------------------------------------------------------------
        // Get List Of Providers For Facility
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get List Of Facilities For Providers")]
        public ProviderTableData GetFacilityListForProviders(Int64 ProviderId, string Token, Int64 UserId, Int64 FacilityId)
        {
            ProviderTableData ProviderList = new ProviderTableData();

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())
                    {
                        var results = from p in db.Providers
                                      join l in db.ProviderFacilityLinks on p.ProviderId equals l.ProviderId
                                      join f in db.Facilities on l.FacilityId equals f.FacilityId
                                      
                                      where p.ProviderId == ProviderId
                                      select new
                                      {
                                          f.FacilityName,
                                          f.FacilityId,
                                          p.PracticeId,
                                          p.ProviderId
                                      };
                        ProviderList.dt = clsTableConverter.ToDataTable(results);

                        ProviderList.dt.TableName = "Providers";

                        ProviderList.Valid = true;
                        ProviderList.ErrorMessage = "";
                    }
                }
                catch (Exception ex)
                {
                    ProviderList.Valid = false;
                    ProviderList.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                ProviderList.Valid = false;
                ProviderList.ErrorMessage = "Invalid Token";
            }
            return ProviderList;
        }
        #endregion

        #region Get List Of Providers For Patient
        //------------------------------------------------------------------------
        // Get List Of Providers For Patient
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get List Of Providers For Patient")]
        public ProviderTableData GetProvidersForPatientList(Int64 PatientId, string Token, Int64 UserId, Int64 FacilityId)
        {
            ProviderTableData ProviderList = new ProviderTableData();

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())
                    {
                        var results = from l in db.PatientProviderLinks
                                      join p in db.Providers on l.ProviderId equals p.ProviderId
                                      where l.PatientId == PatientId
                                      select new
                                      {
                                          p.ProviderId,
                                          Name = p.FirstName + " " + p.LastName
                                      };
                        
                        ProviderList.dt = clsTableConverter.ToDataTable(results);

                        ProviderList.dt.TableName = "Providers";

                        ProviderList.Valid = true;
                        ProviderList.ErrorMessage = "";
                    }
                }
                catch (Exception ex)
                {
                    ProviderList.Valid = false;
                    ProviderList.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                ProviderList.Valid = false;
                ProviderList.ErrorMessage = "Invalid Token";
            }
            return ProviderList;
        }
        #endregion


        //Added By Talha
        #region Get List Of Patient For Providers
        //------------------------------------------------------------------------
        // Get List Of Providers For Patient
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get List Of Providers For Patient")]
        public ProviderTableData GetPatientForProviderList(Int64 ProviderId, string Token, Int64 UserId, Int64 FacilityId)
        {
            ProviderTableData ProviderList = new ProviderTableData();

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())
                    {
                        var results = from l in db.PatientProviderLinks
                                      join p in db.Patients on l.PatientId equals p.PatientId
                                      where l.ProviderId == ProviderId
                                      select new
                                      {
                                          p.PatientId,
                                          Name = p.FirstName + " " + p.LastName
                                      };

                        ProviderList.dt = clsTableConverter.ToDataTable(results);

                        ProviderList.dt.TableName = "Providers";

                        ProviderList.Valid = true;
                        ProviderList.ErrorMessage = "";
                    }
                }
                catch (Exception ex)
                {
                    ProviderList.Valid = false;
                    ProviderList.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                ProviderList.Valid = false;
                ProviderList.ErrorMessage = "Invalid Token";
            }
            return ProviderList;
        }
        #endregion


        #region Get Provider Image
        //------------------------------------------------------------------------
        // Get Provider Image
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get Provider Image")]
        public ProviderImageData GetProviderImageData(ProviderParms Parms, string Token, Int64 UserId, Int64 FacilityId)
        {
            // Initialize Dataset To Return
            ProviderImageData ImageData = new ProviderImageData();

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {

                    using (var db = new AMREntities())
                    {
                        ProviderWebSetting results = db.ProviderWebSettings.FirstOrDefault(p => p.ProviderId == Parms.ProviderId);

                        if (results != null)
                        {
                            string Filename = results.PictureLocation;

                            if (Filename != "" && Filename != null)
                            {
                                ImageData.Image = FileHelper.DiskToBytes(Filename);
                                ImageData.Valid = true;
                            }
                            else
                            {
                                ImageData.Valid = false;
                                ImageData.ErrorMessage = "No Image Available.";
                            }
                        }
                        else
                        {
                            ImageData.Valid = false;
                            ImageData.ErrorMessage = "Could not read record.";
                        }
                    }
                }
                catch (Exception ex)
                {
                    ImageData.Valid = false;
                    ImageData.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                ImageData.Valid = false;
                ImageData.ErrorMessage = "Invalid Token";
            }
            return ImageData;
        }
        #endregion

        #region Save Provider Image
        //------------------------------------------------------------------------
        // Save Provider Image
        //------------------------------------------------------------------------

        [WebMethod(Description = "Save Provider Image")]
        public ProviderImageData SaveProvidermageData(ProviderImageData ImageData, string Token, Int64 UserId, Int64 FacilityId)
        {
            ImageData.Valid = true;
            ImageData.ErrorMessage = "";

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())
                    {
                        // Check if image already exists 
                        ProviderWebSetting results = db.ProviderWebSettings.FirstOrDefault(p => p.ProviderId == ImageData.ProviderId);

                        if (results != null)
                        {
                            string Filename = results.PictureLocation;

                            if (Filename != "" && Filename != null)
                            {
                                // Already has an image, replace at the current location.

                                // Delete Old Image
                                FileHelper.DeleteFile(Filename);

                                // Check that extension is correct

                                int posn = Filename.IndexOf(".");
                                if (posn > 0)
                                    Filename = Filename.Substring(0, posn + 1) + ImageData.ImageFormat;

                            }
                            else
                            {
                                // New Image, get file location

                                // Get Attachment Folder Info & Update Counters
                                ConfigAttachment Config = db.ConfigAttachments.FirstOrDefault(c => c.ConfigAttachmentId == 7);

                                // Update Attachment Counter
                                if (Config.CurrentFolderDocCntr > 1000)
                                {
                                    Config.AttachmentFolderCntr++;
                                    Config.CurrentFolderDocCntr = 0;
                                }
                                Config.CurrentFolderDocCntr++;
                                db.SaveChanges();
                                Filename = Config.AttachmentLocation + "\\" + Config.AttachmentFolderCntr + "\\" + ImageData.ProviderId + "." + ImageData.ImageFormat;
                                // Make sure folder exists
                                FileHelper.CheckOrCreateDirectory(Config.AttachmentLocation + "\\" + Config.AttachmentFolderCntr);

                                results.PictureLocation = Filename;
                                db.SaveChanges();
                            }

                            // Write the document to the hard disk

                            FileHelper.BytesToDisk(ImageData.Image, Filename);
                        }
                    }
                }
                catch (Exception ex)
                {
                    ImageData.Valid = false;
                    ImageData.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                ImageData.Valid = false;
                ImageData.ErrorMessage = "Invalid Token";
            }
            // Clear picture before returning response
            ImageData.Image = null;
            return ImageData;
        }
        #endregion

        #region Get List Of Patient For Providers
        //------------------------------------------------------------------------
        // Get List Of Providers For Patient
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get List Of Provider Information For Patient")]
        public ProviderTableData GetPatientProviderInfoList(Int64 CkPatientId, Int64 CkFacilityId, string Token, Int64 UserId, Int64 FacilityId)
        {
            ProviderTableData ProviderList = new ProviderTableData();

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())
                    {
                        var results = from l in db.PatientProviderLinks
                                      join l2 in db.ProviderFacilityLinks on l.ProviderId equals l2.ProviderId
                                      join f in db.Facilities on l2.FacilityId equals f.FacilityId
                                      join p in db.Providers on l.ProviderId equals p.ProviderId
                                      where l.PatientId == CkPatientId
                                      && l2.FacilityId == CkFacilityId
                                      select new
                                      {
                                          Name = p.FirstName + " " + p.LastName,
                                          f.FacilityName,//Added by talha
                                          p.Phone,
                                          p.Email,
                                          Address = f.Address1,
                                          CityStateZip = f.City + ", " + f.State + " " + f.PostalCode,
                                          f.CountryCode,
                                          OfficePhone = f.Phone,
                                          OfficeFax = f.Fax,
                                      };

                        ProviderList.dt = clsTableConverter.ToDataTable(results);

                        ProviderList.dt.TableName = "Providers";

                        ProviderList.Valid = true;
                        ProviderList.ErrorMessage = "";
                    }
                }
                catch (Exception ex)
                {
                    ProviderList.Valid = false;
                    ProviderList.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                ProviderList.Valid = false;
                ProviderList.ErrorMessage = "Invalid Token";
            }
            return ProviderList;
        }
        #endregion

        #region Provider Email Search
        //Added by Talha

        //------------------------------------------------------------------------
        // Provider Email Search 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Provider Email Search")]
        public ProviderTableData ProviderEmailSearch(string EMail, string Token, Int64 UserId, Int64 FacilityId)
        {
            ProviderTableData Provider = new ProviderTableData();
            Provider.Valid = true;
            Provider.ErrorMessage = "";

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())
                    {
                        var results = from p in db.Providers
                                      where p.Email == EMail
                                      select new
                                      {
                                          p.ProviderId,
                                          p.FirstName,
                                          p.LastName,
                                          p.Email,
                                      };
                        Provider.dt = clsTableConverter.ToDataTable(results);
                        Provider.dt.TableName = "Provider";

                        Provider.Valid = true;
                        Provider.ErrorMessage = "";
                    }
                }
                catch (Exception ex)
                {
                    Provider.Valid = false;
                    Provider.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                Provider.Valid = false;
                Provider.ErrorMessage = "Invalid Token";
            }
            return Provider;
        }
        #endregion

        #region Provider Search
        //------------------------------------------------------------------------
        // Provider Search 
        //------------------------------------------------------------------------
        [WebMethod(Description = "Provider Search")]
        public ProviderTableData ProviderSearch(ProviderSearchParms Parms, string Token, Int64 UserId, Int64 FacilityId)
        {
            ProviderTableData Provider = new ProviderTableData();
            Provider.Valid = true;
            Provider.ErrorMessage = "";

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    int skip = Int32.Parse((Parms.PageNumber * Parms.PageSize).ToString());
                    int PageSize = Int32.Parse((Parms.PageSize).ToString());

                    using (var db = new AMREntities())
                    {
                        if (Parms.ProviderId > 0)
                        {
                            var results = from p in db.Providers
                                          where p.ProviderId == Parms.ProviderId
                                          select new
                                          {
                                              p.ProviderId,
                                              p.FirstName,
                                              p.LastName,
                                              p.License,
                                              p.Phone,
                                              p.Email,
                                          };
                            Provider.count = results.Count();
                            Provider.dt = clsTableConverter.ToDataTable(results);
                            Provider.dt.TableName = "Providers";

                            Provider.Valid = true;
                            Provider.ErrorMessage = "";
                        }
                        else if (Parms.License != null)
                        {

                            var results = (from p in db.Providers
                                           where p.License == Parms.License
                                           orderby p.LastName
                                           select new
                                           {
                                               p.ProviderId,
                                               p.FirstName,
                                               p.LastName,
                                               p.License,
                                               p.Phone,
                                               p.Email,
                                           });
                            Provider.count = results.Count();
                            Provider.dt = clsTableConverter.ToDataTable(results); 
                            Provider.dt.TableName = "Providers";

                            Provider.Valid = true;
                            Provider.ErrorMessage = "";

                        }
                        else if (Parms.EMRSystemId != null)
                        {
                            var results = (from p in db.Providers
                                           join l in db.ProviderFacilityLinks on p.ProviderId equals l.ProviderId
                                           join f in db.Facilities on l.FacilityId equals f.FacilityId
                                           where f.EMRSystemId == Parms.EMRSystemId
                                               && p.FirstName.StartsWith(Parms.FirstName)
                                               && p.LastName.StartsWith(Parms.LastName)
                                               && p.Phone.StartsWith(Parms.Phone)
                                               && p.Email.StartsWith(Parms.EMail)

                                           orderby p.LastName
                                           select new
                                           {
                                               p.ProviderId,
                                               p.FirstName,
                                               p.LastName,
                                               p.License,
                                               p.Phone,
                                               p.Email,
                                           });

                            Provider.count = results.Count();
                            Provider.dt = clsTableConverter.ToDataTable(results.Skip(skip).Take(PageSize));
                            Provider.dt.TableName = "Providers";

                            Provider.Valid = true;
                            Provider.ErrorMessage = "";
                        }
                        else if (Parms.FacilityId != null)
                        {
                            var results = (from p in db.Providers
                                           join l in db.ProviderFacilityLinks on p.ProviderId equals l.ProviderId
                                           where l.FacilityId == Parms.FacilityId
                                               && p.FirstName.StartsWith(Parms.FirstName)
                                               && p.LastName.StartsWith(Parms.LastName)
                                               && p.Phone.StartsWith(Parms.Phone)
                                               && p.Email.StartsWith(Parms.EMail)

                                           orderby p.LastName
                                           select new
                                           {
                                               p.ProviderId,
                                               p.FirstName,
                                               p.LastName,
                                               p.License,
                                               p.Phone,
                                               p.Email,
                                           });

                            Provider.count = results.Count();
                            Provider.dt = clsTableConverter.ToDataTable(results.Skip(skip).Take(PageSize));
                            Provider.dt.TableName = "Providers";

                            Provider.Valid = true;
                            Provider.ErrorMessage = "";
                        }
                        else
                        {
                            var results = (from p in db.Providers
                                           where p.FirstName.StartsWith(Parms.FirstName)
                                               && p.LastName.StartsWith(Parms.LastName)
                                               && p.Phone.StartsWith(Parms.Phone)
                                               && p.Email.StartsWith(Parms.EMail)

                                           orderby p.LastName
                                           select new
                                           {
                                               p.ProviderId,
                                               p.FirstName,
                                               p.LastName,
                                               p.License,
                                               p.Phone,
                                               p.Email,
                                           });

                            Provider.count = results.Count();
                            Provider.dt = clsTableConverter.ToDataTable(results.Skip(skip).Take(PageSize));
                            Provider.dt.TableName = "Providers";

                            Provider.Valid = true;
                            Provider.ErrorMessage = "";
                        }


                    }
                }
                catch (Exception ex)
                {
                    Provider.Valid = false;
                    Provider.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                Provider.Valid = false;
                Provider.ErrorMessage = "Invalid Token";
            }
            return Provider;
        }
        #endregion

        #region Activate Provider
        //------------------------------------------------------------------------
        // Activate Provider  
        //------------------------------------------------------------------------

        [WebMethod(Description = "Activate Provider")]
        public ProviderResponse ActivateProvider(Int64 ProviderId, string Token, Int64 UserId, Int64 FacilityId)
        {
            ProviderResponse RespData = new ProviderResponse();
            RespData.Valid = true;
            RespData.ErrorMessage = "";

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())
                    {
                        User UserResp = db.Users.FirstOrDefault(u => u.UserRoleLink == ProviderId && u.UserRoleId == 4);
                        if (UserResp != null)
                        {
                            UserResp.Enabled = true;
                        }
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    RespData.Valid = false;
                    RespData.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                RespData.Valid = false;
                RespData.ErrorMessage = "Invalid Token";
            }
            return RespData;
        }
        #endregion

        #region Deactivate Provider
        //------------------------------------------------------------------------
        // Deactivate Provider  
        //------------------------------------------------------------------------

        [WebMethod(Description = "Deactivate Provider")]
        public ProviderResponse DeactivateProvider(Int64 ProviderId, string Token, Int64 UserId, Int64 FacilityId)
        {
            ProviderResponse RespData = new ProviderResponse();
            RespData.Valid = true;
            RespData.ErrorMessage = "";

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())
                    {
                        User UserResp = db.Users.FirstOrDefault(u => u.UserRoleLink == ProviderId && u.UserRoleId == 4);
                        if (UserResp != null)
                        {
                            UserResp.Enabled = false;
                        }
                        db.SaveChanges();
                     }
                }
                catch (Exception ex)
                {
                    RespData.Valid = false;
                    RespData.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                RespData.Valid = false;
                RespData.ErrorMessage = "Invalid Token";
            }
            return RespData;
        }
        #endregion

        #region Change Provider Email
        //------------------------------------------------------------------------
        // Change Provider Email  
        //------------------------------------------------------------------------

        [WebMethod(Description = "Change Provider Email")]
        public ProviderResponse ChangeProviderEmail(Int64 ProviderId, string Email, string Token, Int64 UserId, Int64 FacilityId)
        {
            ProviderResponse RespData = new ProviderResponse();
            RespData.Valid = true;
            RespData.ErrorMessage = "";

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())
                    {
                        Provider ProviderResp = db.Providers.FirstOrDefault(p => p.ProviderId == ProviderId);

                        if (ProviderResp != null)
                        {
                            ProviderResp.Email = Email;
                            db.SaveChanges();
                        }
                        

                        User UserResp = db.Users.FirstOrDefault(u => u.UserRoleId == 4 && u.UserRoleLink == ProviderId);

                        if (UserResp != null)
                        {
                            UserResp.UserEmail = Email;

                            if (UserResp.UserLogin.Contains("@"))
                            {
                                UserResp.UserLogin = Email;
                            }
                            db.SaveChanges();
                        }
                        
                    }
                }
                catch (Exception ex)
                {
                    RespData.Valid = false;
                    RespData.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                RespData.Valid = false;
                RespData.ErrorMessage = "Invalid Token";
            }
            return RespData;
        }
        #endregion
    }
}
