using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using AMR.Models;
using System.Data;


namespace AMR.Core.Extensions
{
    public static class ModelExtensions
    {
        public static List<PatientSearchModel> ToPatientSearchList(this System.Data.DataTable table)
        {
            List<PatientSearchModel> Tlist = new List<PatientSearchModel>();
            foreach (System.Data.DataRow row in table.Rows)
            {
                Tlist.Add(new PatientSearchModel
                {
                    PatientId = Convert.ToInt64(row["PatientId"]),
                    FirstName = Convert.ToString(row["FirstName"]),
                    LastName = Convert.ToString(row["LastName"]),
                    Address1 = Convert.ToString(row["Address1"]),
                    Address2 = Convert.ToString(row["Address2"]),
                    City = Convert.ToString(row["City"]),
                    State = Convert.ToString(row["State"]),
                    Zip = Convert.ToString(row["Zip"]),
                    CountryCode = Convert.ToString(row["CountryCode"]),
                    Country = Convert.ToString(row["CountryName"]),
                    HomePhone = Convert.ToString(row["HomePhone"]),
                    EMail = Convert.ToString(row["EMail"]),
                    dob = row["DOB"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["DOB"]),
                 //   dob =Convert.ToDateTime(row["DOB"]),
                });
            }
            return Tlist;
        }

        public static List<ProviderSearchModel> ToProviderSearchList(this System.Data.DataTable table)
        {
            List<ProviderSearchModel> Tlist = new List<ProviderSearchModel>();
            foreach (System.Data.DataRow row in table.Rows)
            {
                Tlist.Add(new ProviderSearchModel
                {
                    ProviderId = Convert.ToInt64(row["ProviderId"]),
                    FirstName = Convert.ToString(row["FirstName"]),
                    LastName = Convert.ToString(row["LastName"]),
                    License = Convert.ToString(row["License"]),
                    Phone = Convert.ToString(row["Phone"]),
                    EMail = Convert.ToString(row["Email"]),
                
                    //   dob =Convert.ToDateTime(row["DOB"]),
                });
            }
            return Tlist;
        
        }
        public static int ToUserlList(this  System.Data.DataTable table)
        {
            int RoleId = 0;
            List<UserModel> TList = new List<UserModel>();
            foreach (System.Data.DataRow row in table.Rows)
            {

                RoleId = Convert.ToInt32(row["UserRoleId"]);
                  // RoleName = Convert.ToString(row["RoleName"])
                
            }
            return RoleId;
        }
        public static List<ProviderInfoModel> ToProivderInfoList(this System.Data.DataTable table)
        {
            List<ProviderInfoModel> Tlist = new List<ProviderInfoModel>();
            foreach (System.Data.DataRow row in table.Rows)
            {
                Tlist.Add(new ProviderInfoModel {
                Name = Convert.ToString(row["Name"]),
                FacilityName = Convert.ToString(row["FacilityName"]),
                Phone = Convert.ToString(row["Phone"]),
                Email = Convert.ToString(row["Email"]),
                Address = Convert.ToString(row["Address"]),
                CityStateZip = Convert.ToString(row["CityStateZip"]),
                OfficeFax = Convert.ToString(row["OfficeFax"]),
                OfficePhone = Convert.ToString(row["OfficePhone"]),
                });
            }
            return Tlist;
        }
        public static List<MedicationListModel> ToMedicationlList(this  System.Data.DataTable table)
        {
            List<MedicationListModel> TList = new List<MedicationListModel>();
            foreach (System.Data.DataRow row in table.Rows)
            {
                TList.Add(new MedicationListModel
                {
                    RxNormId = Convert.ToInt64(row["RxNormId"]),
                    Description = Convert.ToString(row["Description"])

                });
            }
            return TList;
        }
        
        public static List<CCDActivityLogModel> ToCddActivityLog(this System.Data.DataTable table)
    {
        List<CCDActivityLogModel> TList = new List<CCDActivityLogModel>();
        foreach (System.Data.DataRow row in table.Rows)
        {
            TList.Add(new CCDActivityLogModel{
                AuditId= Convert.ToInt64(row["AuditId"]), 
                PatientId= Convert.ToInt64(row["PatientId"]),
                Method = Convert.ToString(row["Method"]),
                 UserId= Convert.ToInt64(row["UserId"]),
                 Name=Convert.ToString(row["Name"]),
                TDStamp = Convert.ToDateTime(row["TDStamp"])
            
            
            });
        }    
            return TList;
    }
        public static List<ProblemSNOMEDModel> ToProblemModeSNOMEDlList(this  System.Data.DataTable table)
        {
            List<ProblemSNOMEDModel> TList = new List<ProblemSNOMEDModel>();
            foreach (System.Data.DataRow row in table.Rows)
            {
                TList.Add(new ProblemSNOMEDModel
                {
                    ProblemID = Convert.ToInt64(row["ProblemId"]),
                    Value = Convert.ToString(row["Value"])
                    
                });
            }
            return TList;
        }
            
        public static List<ProblemModel> ToPatientProblemModelList(this  System.Data.DataTable table)
        {
            List<ProblemModel> TList = new List<ProblemModel>();
            foreach (System.Data.DataRow row in table.Rows)
            {
                TList.Add(new ProblemModel
                {
                    PatientProblemCntr = Convert.ToInt64(row["PatientProblemCntr"]),
                    CodeValue = Convert.ToString(row["CodeValue"]),
                    CodeSystemId = Convert.ToInt32(row["CodeSystemId"]),
                    Condition = row["Condition"].ToString(),
                    EffectiveDate = row["EffectiveDate"].ToString(),
                    LastChangeDate = row["LastChangeDate"].ToString(),
                    ConditionStatusId = Convert.ToInt16(row["ConditionStatusId"]),
                    ConditionStatus = Convert.ToString(row["ConditionStatus"]),
                    Note = row["Note"].ToString()
                    
                });
            }
            return TList;
        }
           
        public static List<POCModel> ToPOCModelList(this  System.Data.DataTable table)
        {
            List<POCModel> TList = new List<POCModel>();
            foreach (System.Data.DataRow row in table.Rows)
            {
                  

                TList.Add(new POCModel
                {

                    PlanCntr = Convert.ToInt64(row["PlanCntr"]),
                    CodeValue = Convert.ToString(row["CodeValue"]),
                    CodeSystem = Convert.ToString(row["CodeSystem"]),
                    InstructionTypeId = Convert.ToInt16(row["InstructionTypeId"]),
                    Instruction = row["Instruction"].ToString(),
                    Goal = row["Goal"].ToString(),
                    Note = row["Note"].ToString(),
                    InstructionType = row["InstructionType"].ToString(),
                    AppointmentDateTime = row["AppointmentDateTime"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["AppointmentDateTime"]),
                    ProviderId = Convert.ToInt64(row["ProviderId"])
                 
                });
            }
            return TList;
        }



           
           public static List<ProcedureModel> ToProcedureModelList(this  System.Data.DataTable table)
           {
               List<ProcedureModel> TList = new List<ProcedureModel>();
               foreach (System.Data.DataRow row in table.Rows)
               {

                   TList.Add(new ProcedureModel
                   {                 
                                          



                       PatProcedureCntr = Convert.ToInt64(row["PatProcedureCntr"]),
                       Description = row["Description"].ToString(),
                       CodeValue = row["CodeValue"].ToString(),
                       CodeSystem = row["CodeSystem"].ToString(),
                       CodeSystemId = Convert.ToInt32(row["CodeSystemId"]),
                       Diagnosis = row["Diagnosis"].ToString(),
                       PerformedBy = row["PerformedBy"].ToString(),
                       ServiceLocation = row["ServiceLocation"].ToString(),
                       ServiceDate = row["ServiceDate"].ToString(),
                       Note = row["Note"].ToString(),
                       DateModified = Convert.ToDateTime(row["DateModified"])




                   });
               }
               return TList;
           }
           public static List<MedicalPortfolioBaseModel> ToMedicalPortfolioBaseModel(this  System.Data.DataTable table)
           {
               List<MedicalPortfolioBaseModel> TList = new List<MedicalPortfolioBaseModel>();
               foreach (System.Data.DataRow row in table.Rows)
               {

                   TList.Add(new MedicalPortfolioBaseModel
                   {




                       DocumentCntr = Convert.ToInt64(row["DocumentCntr"]),
                       DocumentDescription = row["DocumentDescription"].ToString(),
                       DateCreated = Convert.ToDateTime( row["DateCreated"]),
                       FacilityName = row["FacilityName"].ToString(),
                       FacilityId = Convert.ToInt64(row["FacilityId"]),
                       VisitDate = Convert.ToDateTime(row["VisitDate"]),
                       VisitId = Convert.ToInt64( row["VisitId"]),
                       Viewable = Convert.ToBoolean( row["Viewable"]),
                       DocumentFormat = Convert.ToString(row["DocumentFormat"]),
                       //ServiceDate = row["ServiceDate"].ToString(),
                       //Note = row["Note"].ToString(),
                       //DateModified = Convert.ToDateTime(row["DateModified"])




                   });
               }
               return TList;
           }



        

           public static List<CarrierModel> ToCarrierModelList(this System.Data.DataTable table)
           {
               List<CarrierModel> TList = new List<CarrierModel>();
               foreach (System.Data.DataRow row in table.Rows)
               {

                   TList.Add(new CarrierModel
                   {
                       CarrierId = Convert.ToInt16(row["CarrierId"]),
                       CarrierName=Convert.ToString(row["CarrierName"])
                   });
               }
               return TList;
           }


        public static List<FamilyHistoryModel> ToPatientFamilyHistoryModelList(this  System.Data.DataTable table)
        {
            List<FamilyHistoryModel> TList = new List<FamilyHistoryModel>();
            foreach (System.Data.DataRow row in table.Rows)
            {
                TList.Add(new FamilyHistoryModel
                {
                    PatFamilyHistCntr = Convert.ToInt64(row["PatFamilyHistCntr"]),
                    SNOMEDCode = row["SNOMEDCode"].ToString(),
                    RelationshipId = Convert.ToInt64(row["RelationshipId"]),
                    FamilyMember = row["FamilyMember"].ToString(),
                    Description = row["Description"].ToString(),
                    Qualifier = row["Qualifier"].ToString(),
                    CodeValue = row["CodeValue"].ToString(),
                    CodeSystemId = Convert.ToInt32(row["CodeSystemId"]),
                    ConditionStatusId = Convert.ToInt16(row["ConditionStatusId"]),
                    DiseasedAge = Convert.ToInt32(row["DiseasedAge"]),
                    AgeAtOnset = Convert.ToInt32(row["AgeAtOnset"]),
                    DateReported = row["DateModified"].ToString(),
                    Diseased = Convert.ToBoolean(row["Diseased"]),
                    Note = row["Note"].ToString()
                });
            }
            return TList;
        }



        
        public static List<MedicalHistoryModel> ToPatientMedicalHistoryModelList(this  System.Data.DataTable table)
        {
            List<MedicalHistoryModel> TList = new List<MedicalHistoryModel>();
            foreach (System.Data.DataRow row in table.Rows)
            {
                TList.Add(new MedicalHistoryModel
                {
                    PatMedicalHistCntr = Convert.ToInt64(row["PatMedicalHistCntr"]),
                    Description = Convert.ToString(row["Description"]),
                    DateOfOccurance = Convert.ToString(row["DateOfOccurance"]),
                    Note = Convert.ToString(row["Note"]),
                    OnCard = Convert.ToBoolean(row["OnCard"]),
                    OnKeys = Convert.ToBoolean(row["OnKeys"]),
                    DateModified = Convert.ToDateTime(row["DateModified"])

                });
            }
            return TList;
        }

        public static List<PatientAccountLinkModel> ToPatientAccountLinkModel(this System.Data.DataTable table)
        {
            List<PatientAccountLinkModel> list = new List<PatientAccountLinkModel>();
            foreach (System.Data.DataRow row in table.Rows)
            {
                list.Add(new PatientAccountLinkModel
                {
                PatientId=Convert.ToInt64(row["PatientId"]),
                PatientName = row["PatientName"].ToString(),
             PatientId_Linked=Convert.ToInt64(row["PatientId_Linked"]),
                 DateApproved=Convert.ToDateTime(row["DateApproved"])
                });
            }
            return list;
            }

        public static List<PatientAccountLinkModel> ToParentAccountLinkModel(this System.Data.DataTable table)
        {
            List<PatientAccountLinkModel> list = new List<PatientAccountLinkModel>();
            foreach (System.Data.DataRow row in table.Rows)
            {
                list.Add(new PatientAccountLinkModel
                {
                    PatientId = Convert.ToInt64(row["PatientId"]),
                    PatientName = row["PatientName"].ToString(),
                    DateApproved = Convert.ToDateTime(row["DateApproved"])
                });
            }
            return list;
        }
      //  
        public static List<LabResultModel> ToPatientLabResultModelList(this  System.Data.DataTable table)
        {
            List<LabResultModel> TList = new List<LabResultModel>();
            foreach (System.Data.DataRow row in table.Rows)
            {
                TList.Add(new LabResultModel
                {
                    LabResultCntr = Convert.ToInt64(row["LabResultCntr"]),
                    ProviderId_Requested = Convert.ToInt64(row["ProviderId_Requested"]),
                    ProviderName = Convert.ToString(row["ProviderName"]),
                    LabName = Convert.ToString(row["LabName"]),
                    FacilityName = Convert.ToString(row["FacilityName"]),
                    OrderDate = row["OrderDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["OrderDate"]),
                    CollectionDate = row["CollectionDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["CollectionDate"]),
                    Requisiton = Convert.ToString(row["Requisiton"]),
                    Specimen = Convert.ToString(row["Specimen"]),
                    SpecimenSource = Convert.ToString(row["SpecimenSource"]),
                    ReportDate = Convert.ToDateTime(row["ReportDate"]),
                    ReviewDate = row["ReviewDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["ReviewDate"]),
                    Reviewer = Convert.ToString(row["Reviewer"])

                });
            }
            return TList;
        }


        //  
        public static List<AllergyModel> ToPatientAllergyModelList(this  System.Data.DataTable table)
        {
            List<AllergyModel> TList = new List<AllergyModel>();
            foreach (System.Data.DataRow row in table.Rows)
            {
                TList.Add(new AllergyModel
                {
                    PatientAllergyCntr = Convert.ToInt64(row["PatientAllergyCntr"]),
                    CodeValue_Allergen = Convert.ToString(row["CodeValue_Allergen"]),
                    CodeSystemId_Allergen = Convert.ToInt32(row["CodeSystemId_Allergen"]),
                    Allergen = Convert.ToString(row["Allergen"]),
                    AllergenType = Convert.ToString(row["AllergenType"]),
                    CodeValue_Reaction = Convert.ToString(row["CodeValue_Reaction"]),
                    CodeSystemId_Reaction = Convert.ToInt32(row["CodeSystemId_Reaction"]),
                    Reaction = Convert.ToString(row["Reaction"]),
                    EffectiveDate = Convert.ToString(row["EffectiveDate"]),
                    ConditionStatusId = Convert.ToInt16(row["ConditionStatusId"]),
                    Note = Convert.ToString(row["Note"]),
                    OnCard = Convert.ToBoolean(row["OnCard"]),
                    OnKeys = Convert.ToBoolean(row["OnKeys"]),
                    DateModified = Convert.ToDateTime(row["DateModified"]),
                    ConditionStatus = Convert.ToString(row["ConditionStatus"]),
                
                });
            }
            return TList;
        }




        
        public static List<ImmunizationModel> ToPatientImmunizationModelList(this  System.Data.DataTable table)
        {
            List<ImmunizationModel> TList = new List<ImmunizationModel>();
            foreach (System.Data.DataRow row in table.Rows)
            {
                string strTime;
                string strDate;
                if (Convert.ToString(row["ImmunizationDate"]).Trim() == "" || Convert.ToString(row["ImmunizationDate"]) == null || Convert.ToString(row["ImmunizationDate"]) == "00010101")
                {
                    strDate = "";
                }
                else
                {
                    var VaccineDate = DateTime.ParseExact(Convert.ToString(row["ImmunizationDate"]), "yyyyMMdd", null);
                    strDate = VaccineDate.ToString("MM/dd/yyyy");
                }

                if (Convert.ToString(row["ImmunizationTime"]).Trim() == "" || Convert.ToString(row["ImmunizationTime"]) == null)
                {
                    strTime = "";
                }
                else
                {
                    var VaccineTime = DateTime.ParseExact(Convert.ToString(row["ImmunizationTime"]), "HHmm", null);
                    strTime = VaccineTime.ToString("hh:mm tt");
                }
                TList.Add(new ImmunizationModel
                {
                    
                   

                    PatientImmunizationCntr = Convert.ToInt64(row["PatientImmunizationCntr"]),
                    ImmunizationDate = strDate,
                    Vaccine = Convert.ToString(row["Vaccine"]),
                    Note = Convert.ToString(row["Note"]),
                    Amount = Convert.ToString(row["Amount"]),
                    Route = Convert.ToString(row["Route"]),
                    Site = Convert.ToString(row["Site"]),
                    Sequence = Convert.ToString(row["Sequence"]),
                    ExpirationDate = Convert.ToDateTime(row["ExpirationDate"]),
                    LotNumber = Convert.ToString(row["LotNumber"]),
                    Time = strTime,
                    Manufacturer = Convert.ToString(row["Manufacturer"])

                });
            }
            return TList;
        }
        
        public static List<PatientVitalSignModel> ToPatientVitalSignModelList(this  System.Data.DataTable table)
        {
            List<PatientVitalSignModel> TList = new List<PatientVitalSignModel>();
            foreach (System.Data.DataRow row in table.Rows)
            {
                TList.Add(new PatientVitalSignModel
                {
                    BloodPressurePosn = Convert.ToString(row["BloodPressurePosn"]),
                    Diastolic = Convert.ToInt32(row["Diastolic"]),
                    Systolic = Convert.ToInt32(row["Systolic"]),
                    Pulse = Convert.ToInt32(row["Pulse"]),
                    Respiration = Convert.ToInt32(row["Respiration"]),
                    VitalDate = Convert.ToDateTime(row["VitalDate"]),
                    HeightIn = Convert.ToInt32(row["HeightIn"]),
                    WeightLb = Convert.ToInt32(row["WeightLb"]),
                    PatientVitalCntr =Convert.ToInt32(row["PatientVitalCntr"]),
                    Temperature = Convert.ToDecimal(row["Temperature"])
                });
            }
            return TList;
        }
        
        public static List<LabResultModel> ToLabResultModelList(this  System.Data.DataTable table)
        {
            List<LabResultModel> TList = new List<LabResultModel>();
            foreach (System.Data.DataRow row in table.Rows)
            {
                TList.Add(new LabResultModel
                {
                    // Valid = Convert.ToBoolean(row["Valid"]),
                    //  ErrorMessage = Convert.ToString(row["ErrorMessage"]),
                    //  PatientId = Convert.ToInt64(row["PatientId"]),
                    // VisitId = Convert.ToInt64(row["VisitId"]),
                    LabResultCntr = Convert.ToInt64(row["LabResultCntr"]),
                    // FacilityId = Convert.ToInt64(row["FacilityId"]),
                    ProviderId_Requested = Convert.ToInt64(row["ProviderId_Requested"]),
                    LabName = Convert.ToString(row["LabName"]),
                    FacilityName=Convert.ToString(row["FacilityName"]),
                    ProviderName = Convert.ToString(row["ProviderName"]),
                    OrderDate = (!string.IsNullOrEmpty(Convert.ToString(row["OrderDate"])))?Convert.ToDateTime(row["OrderDate"]) : new DateTime(1900,1,1),
                    CollectionDate = (!string.IsNullOrEmpty(Convert.ToString(row["CollectionDate"]))) ? Convert.ToDateTime(row["CollectionDate"]) : new DateTime(1900, 1, 1),
                    Requisiton = Convert.ToString(row["Requisiton"]),
                    Specimen = Convert.ToString(row["Specimen"]),
                    SpecimenSource = Convert.ToString(row["SpecimenSource"]),
                    ReportDate = (!string.IsNullOrEmpty(Convert.ToString(row["ReportDate"]))) ? Convert.ToDateTime(row["ReportDate"]) : new DateTime(1900, 1, 1),
                    ReviewDate = (!string.IsNullOrEmpty(Convert.ToString(row["ReviewDate"]))) ? Convert.ToDateTime(row["ReviewDate"]) : new DateTime(1900, 1, 1),
                    Reviewer = Convert.ToString(row["Reviewer"]),
                    Result=Convert.ToString(row["Result"]),
                    RefRange = Convert.ToString(row["RefRange"]),
                    Abnormal = Convert.ToString(row["Abnormal"]),
                    CodeValue = Convert.ToString(row["CodeValue"]),
                    CodeSystemId = Convert.ToString(row["CodeSystemId"])


                });
            }
            return TList;
        }

        public static List<LabResultModel> ToLabModelList(this  System.Data.DataTable table)
        {
            List<LabResultModel> TList = new List<LabResultModel>();
            foreach (System.Data.DataRow row in table.Rows)
            {
                TList.Add(new LabResultModel
                {
                    // Valid = Convert.ToBoolean(row["Valid"]),
                    //  ErrorMessage = Convert.ToString(row["ErrorMessage"]),
                    //  PatientId = Convert.ToInt64(row["PatientId"]),
                    // VisitId = Convert.ToInt64(row["VisitId"]),
                    LabResultCntr = Convert.ToInt64(row["LabResultCntr"]),
                    // FacilityId = Convert.ToInt64(row["FacilityId"]),
                    ProviderId_Requested = Convert.ToInt64(row["ProviderId_Requested"]),
                    LabName = Convert.ToString(row["LabName"]),
                    FacilityName = Convert.ToString(row["FacilityName"]),
                    ProviderName = Convert.ToString(row["ProviderName"]),
                    OrderDate = (!string.IsNullOrEmpty(Convert.ToString(row["OrderDate"]))) ? Convert.ToDateTime(row["OrderDate"]) : new DateTime(1900, 1, 1),
                    CollectionDate = (!string.IsNullOrEmpty(Convert.ToString(row["CollectionDate"]))) ? Convert.ToDateTime(row["CollectionDate"]) : new DateTime(1900, 1, 1),
                    Requisiton = Convert.ToString(row["Requisiton"]),
                    Specimen = Convert.ToString(row["Specimen"]),
                    SpecimenSource = Convert.ToString(row["SpecimenSource"]),
                    ReportDate = (!string.IsNullOrEmpty(Convert.ToString(row["ReportDate"]))) ? Convert.ToDateTime(row["ReportDate"]) : new DateTime(1900, 1, 1),
                    ReviewDate = (!string.IsNullOrEmpty(Convert.ToString(row["ReviewDate"]))) ? Convert.ToDateTime(row["ReviewDate"]) : new DateTime(1900, 1, 1),
                    Reviewer = Convert.ToString(row["Reviewer"])
                  


                });
            }
            return TList;
        }



        public static List<LabResultTestModel> ToLabResultTestModelList(this  System.Data.DataTable table)
        {
            List<LabResultTestModel> TList = new List<LabResultTestModel>();
            foreach (System.Data.DataRow row in table.Rows)
            {
                TList.Add(new LabResultTestModel
                {
                    // Valid = Convert.ToBoolean(row["Valid"]),
                    //  ErrorMessage = Convert.ToString(row["ErrorMessage"]),
                    //  PatientId = Convert.ToInt64(row["PatientId"]),
                    // VisitId = Convert.ToInt64(row["VisitId"]),
                     LabResultCntr = Convert.ToInt64(row["LabResultCntr"]),
                    TestCntr = Convert.ToInt64(row["TestCntr"]),
                     CodeValue = row["CodeValue"] == DBNull.Value ? "" : Convert.ToString(row["CodeValue"]),
                    //CodeValue = Convert.ToString(row["CodeValue"]),
                     CodeSystemId = row["CodeSystemId"] == DBNull.Value ? 0 : Convert.ToInt64(row["CodeSystemId"]),
                    //CodeSystemId = Convert.ToInt64(row["CodeSystemId"]),
                    Component = Convert.ToString(row["Component"]),
                    Result = Convert.ToString(row["Result"]),
                    RefRange = Convert.ToString(row["RefRange"]),
                    Units = Convert.ToString(row["Units"]),
                    AbNormal = Convert.ToString(row["Abnormal"]),
                    ResultStatus = Convert.ToString(row["ResultStatus"])
                   

                  
                });
            }
            return TList;
        }




        public static List<ProblemModel> ToProblemModelList(this  System.Data.DataTable table)
        {
            List<ProblemModel> TList = new List<ProblemModel>();
            foreach (System.Data.DataRow row in table.Rows)
            {
                TList.Add(new ProblemModel
                {
                    ///  Valid = Convert.ToBoolean(row["Valid"]),
                    //ErrorMessage = Convert.ToString(row["ErrorMessage"]),
                    //PatientId = Convert.ToInt64(row["PatientId"]),
                    //VisitId = Convert.ToInt64(row["VisitId"]),
                    PatientProblemCntr = Convert.ToInt64(row["PatientProblemCntr"]),
                    //FacilityId = Convert.ToInt64(row["FacilityId"]),
                    CodeValue = Convert.ToString(row["CodeValue"]),
                    CodeSystemId = Convert.ToInt32(row["CodeSystemId"]),
                    Condition = Convert.ToString(row["Condition"]),
                    ConditionStatus = Convert.ToString(row["ConditionStatus"]),
                    EffectiveDate = Convert.ToString(row["EffectiveDate"]),
                    LastChangeDate = Convert.ToString(row["LastChangeDate"]),
                    ConditionStatusId = Convert.ToInt16(row["ConditionStatusId"]),
                    Note = Convert.ToString(row["Note"])

                });
            }
            return TList;
        }


        
        public static List<MessageTypeModel> ToMessageTypeModelList(this System.Data.DataTable table)
        {
            List<MessageTypeModel> TList = new List<MessageTypeModel>();
              foreach (System.Data.DataRow row in table.Rows)
            {
                TList.Add(new MessageTypeModel
             {
                 MessageTypeId = Convert.ToInt32(row["MessageTypeId"]),
                 Value = Convert.ToString(row["value"])
             });
              }
              return TList;
        }

        
        public static List<MessageUrgency> ToMessageUrgencyList(this System.Data.DataTable table)
        {
            List<MessageUrgency> TList = new List<MessageUrgency>();
            foreach (System.Data.DataRow row in table.Rows)
            {
                TList.Add(new MessageUrgency
                {
                    MessageUrgencyId = Convert.ToInt32(row["MessageUrgencyId"]),
                    Value = Convert.ToString(row["Value"])

                });
            }
            return TList;
        }

        public static List<MessageUnreadModel> ToUnreadMessageList(this System.Data.DataTable table)
        {
            List<MessageUnreadModel> TList = new List<MessageUnreadModel>();
            foreach (System.Data.DataRow row in table.Rows)
            {
                TList.Add(new MessageUnreadModel
                {
                    AppointmentMessages = Convert.ToInt32(row["AppointmentMessages"]),
                    GeneralMessages = Convert.ToInt32(row["GeneralMessages"]),
                    MedicationMessages = Convert.ToInt32(row["MedicationMessages"]),
                    ReferralMessages = Convert.ToInt32(row["ReferralMessages"]),
                    TotalMessages = Convert.ToInt32(row["TotalMessages"])
                }); 
            }
            return TList;
        }

        public static IEnumerable<SelectListItem> ToMessageTypeModelSelectList(this  List<MessageTypeModel> collection)
        {
            List<SelectListItem> TList = collection.Select(x => new SelectListItem { Text = x.Value, Value = x.MessageTypeId.ToString() }).ToList();
            return TList;
        }

        public static IEnumerable<SelectListItem> ToPatientDoctorModelSelectList(this  List<PatientDoctorModel> collection)
        {
            List<SelectListItem> TList = collection.Select(x => new SelectListItem { Text = x.Name, Value = x.PatientDoctorId.ToString() }).ToList();
            return TList;
        }

        
        public static IEnumerable<SelectListItem> ToMessageUrgencySelectList(this  List<MessageUrgency> collection)
        {
            List<SelectListItem> TList = collection.Select(x => new SelectListItem { Text = x.Value, Value = x.MessageUrgencyId.ToString() }).ToList();
            return TList;
        }

        public static IEnumerable<SelectListItem> ToMessageStatusSelectList(this  List<MessageStatusModel> collection)
        {
            List<SelectListItem> TList = collection.Select(x => new SelectListItem { Text = x.Value, Value = x.MessageStatusId.ToString() }).ToList();
            return TList;
        }
        //
        public static List<PatientMessageModel> ToAppointmentMessageModelList(this  System.Data.DataTable table)
        {
            List<PatientMessageModel> TList = new List<PatientMessageModel>();
            foreach (System.Data.DataRow row in table.Rows)
            {
                TList.Add(new PatientMessageModel
                {
                    // PatientId = Convert.ToInt64(row["PatientId"]),
                    FacilityName = Convert.ToString(row["FacilityName"]),
                    MessageId = Convert.ToInt64(row["MessageId"]),
                    MessageStatus = Convert.ToString(row["MessageStatus"]),
                    MessageType = Convert.ToString(row["MessageType"]),

                    MedicationName = Convert.ToString(row["MedicationName"]),
                    MessageRequest = Convert.ToString(row["MessageRequest"]),
                    PharmacyName = Convert.ToString(row["PharmacyName"]),
                    PreferredPeriod = Convert.ToString(row["PreferredPeriod"]),
                    PreferredTime = Convert.ToString(row["PreferredTime"]),
                    PreferredWeekDay = Convert.ToString(row["PreferredWeekDay"]),
                    ProviderName_From = Convert.ToString(row["ProviderName_From"]),
                    VisitReason = Convert.ToString(row["VisitReason"]),

                    MessageTypeId = Convert.ToString(row["MessageTypeId"]),
                    MessageStatusId = Convert.ToString(row["MessageStatusId"]),
                    PatientId = Convert.ToInt64(row["PatientId"]),
                    //   MessageUrgencyId= Convert.ToString(row["MessageUrgencyId"]),
                    MessageUrgency = Convert.ToBoolean(row["MessageUrgency"]),
                    ProviderId_From = Convert.ToString(row["ProviderId_From"]),
                    ProviderId_To = Convert.ToString(row["ProviderId_To"]),
                    ProviderName_To = Convert.ToString(row["ProviderName_To"]),

                    UserId_Created = Convert.ToString(row["UserId_Created"]),
                    CreatedByName = Convert.ToString(row["CreatedByName"]),
                    MessageResponse = Convert.ToString(row["MessageResponse"]),
                    MessageResponseTypeId = Convert.ToString(row["MessageResponseTypeId"]),
                    AppointmentStart = Convert.ToDateTime(row["AppointmentStart"]),
                    AppointmentEnd = Convert.ToDateTime(row["AppointmentEnd"]),
                    ProviderId_Appointment = Convert.ToString(row["ProviderId_Appointment"]),
                    MedicationNDC = Convert.ToString(row["MedicationNDC"]),


                    NoOfRefills = Convert.ToString(row["NoOfRefills"]),
                    MedicationStatus = Convert.ToString(row["MedicationStatus"]),
                    PharmacyAddress = Convert.ToString(row["PharmacyAddress"]),
                    AttachmentId = Convert.ToString(row["AttachmentId"]),
                    MessageUnRead = Convert.ToInt64(row["MessageUnRead"]),
                    DateCreated = Convert.ToDateTime(row["DateCreated"]),
                    PharmacyPhone = Convert.ToString(row["PharmacyPhone"]),
                    ProviderApprDate = Convert.ToDateTime(row["ProviderApprDate"]),
                    ProviderApprTime = Convert.ToString(row["ProviderApprTime"])
                });
            }
            return TList;
        }
        public static List<PatientMessageModel> ToMessageModelList(this  System.Data.DataTable table)
        {
            List<PatientMessageModel> TList = new List<PatientMessageModel>();
            foreach (System.Data.DataRow row in table.Rows)
            {
                TList.Add(new PatientMessageModel
                {
                    // PatientId = Convert.ToInt64(row["PatientId"]),
                    FacilityName = Convert.ToString(row["FacilityName"]),
                    MessageId = Convert.ToInt64(row["MessageId"]),
                    MessageStatus = Convert.ToString(row["MessageStatus"]),
                    MessageType = Convert.ToString(row["MessageType"]),

                    MedicationName = Convert.ToString(row["MedicationName"]),
                    MessageRequest = Convert.ToString(row["MessageRequest"]),
                    PharmacyName = Convert.ToString(row["PharmacyName"]),
                    PreferredPeriod = Convert.ToString(row["PreferredPeriod"]),
                    PreferredTime = Convert.ToString(row["PreferredTime"]),
                    PreferredWeekDay = Convert.ToString(row["PreferredWeekDay"]),
                    ProviderName_From = Convert.ToString(row["ProviderName_From"]),
                    VisitReason = Convert.ToString(row["VisitReason"]),

                    MessageTypeId = Convert.ToString(row["MessageTypeId"]),
                    MessageStatusId = Convert.ToString(row["MessageStatusId"]),
                    PatientId = Convert.ToInt64(row["PatientId"]),
                 //   MessageUrgencyId= Convert.ToString(row["MessageUrgencyId"]),
                    MessageUrgency= Convert.ToBoolean(row["MessageUrgency"]),
                    ProviderId_From = Convert.ToString(row["ProviderId_From"]),
                    ProviderId_To= Convert.ToString(row["ProviderId_To"]),
                    ProviderName_To = Convert.ToString(row["ProviderName_To"]),

                    UserId_Created = Convert.ToString(row["UserId_Created"]),
                    CreatedByName = Convert.ToString(row["CreatedByName"]),
                    MessageResponse = Convert.ToString(row["MessageResponse"]),
                    MessageResponseTypeId = Convert.ToString(row["MessageResponseTypeId"]),
                    AppointmentStart = Convert.ToDateTime(row["AppointmentStart"]),
                    AppointmentEnd = Convert.ToDateTime(row["AppointmentEnd"]),
                    ProviderId_Appointment = Convert.ToString(row["ProviderId_Appointment"]),
                    MedicationNDC = Convert.ToString(row["MedicationNDC"]),


                    NoOfRefills = Convert.ToString(row["NoOfRefills"]),
                    MedicationStatus = Convert.ToString(row["MedicationStatus"]),
                    PharmacyAddress = Convert.ToString(row["PharmacyAddress"]),
                    AttachmentId = Convert.ToString(row["AttachmentId"]),
                    MessageUnRead = Convert.ToInt64(row["MessageUnRead"]),
                    DateCreated = Convert.ToDateTime(row["DateCreated"]),
                    PharmacyPhone = Convert.ToString(row["PharmacyPhone"])

                });
            }
            return TList;
        }
        public static List<PatientMessageModel> ToMessageModelSendList(this  System.Data.DataTable table)
        {
            List<PatientMessageModel> TList = new List<PatientMessageModel>();
            foreach (System.Data.DataRow row in table.Rows)
            {
                TList.Add(new PatientMessageModel
                {
                   // PatientId = Convert.ToInt64(row["PatientId"]),
                    MessageId = Convert.ToInt64(row["MessageId"]),
                    MessageDetailId = Convert.ToInt64(row["MessageDetailId"]),
                    PatientId = Convert.ToInt64(row["PatientId"]),
                    MessageTypeId = Convert.ToString(row["MessageTypeId"]),
                    MessageType = Convert.ToString(row["MessageType"]),
                    FacilityId = Convert.ToInt64(row["FacilityId"]),
                    FacilityName = Convert.ToString(row["FacilityName"]),
                    MessageStatusId = Convert.ToString(row["MessageStatusId"]),
                    MessageStatus = Convert.ToString(row["MessageStatus"]),
                    ProviderId_To = Convert.ToString(row["ProviderId_To"]),
                    ProviderTo = Convert.ToString(row["ProviderTo"]),
                    ProviderId_From = Convert.ToString(row["ProviderId_From"]),
                    ProviderFrom = Convert.ToString(row["ProviderFrom"]),
                    MessageRequest = Convert.ToString(row["MessageRequest"]),
                    MessageResponse = Convert.ToString(row["MessageResponse"]),
                    MessageResponseTypeId = Convert.ToString(row["MessageResponseTypeId"]),
                    MessageResponseType = Convert.ToString(row["MessageResponseType"]),
                    PreferredTime = Convert.ToString(row["PreferredTime"]),
                    PreferredPeriod = Convert.ToString(row["PreferredPeriod"]),
                    PreferredWeekDay = Convert.ToString(row["PreferredWeekDay"]),
                    VisitReason = Convert.ToString(row["VisitReason"]),
                    MessageUrgency = Convert.ToBoolean(row["MessageUrgency"]),
                    AppointmentStart = Convert.ToDateTime(row["AppointmentStart"]),
                    AppointmentEnd = Convert.ToDateTime(row["AppointmentEnd"]),
                    ProviderId_Appointment = Convert.ToString(row["ProviderId_Appointment"]),
                    ProviderAppointment = Convert.ToString(row["ProviderAppointment"]),
                    MedicationNDC = Convert.ToString(row["MedicationNDC"]),
                    MedicationName = Convert.ToString(row["MedicationName"]),
                    NoOfRefills = Convert.ToString(row["NoOfRefills"]),
                    MedicationStatus = Convert.ToString(row["MedicationStatus"]),
                    PharmacyName = Convert.ToString(row["PharmacyName"]),
                    PharmacyAddress = Convert.ToString(row["PharmacyAddress"]),
                    PharmacyPhone = Convert.ToString(row["PharmacyPhone"]),
                    DateCreated = Convert.ToDateTime(row["DateCreated"]),
                    MessageRead = Convert.ToBoolean(row["MessageRead"]),
                  PatientName = Convert.ToString(row["PatientName"]),
                  //  MessageAttachmentId = Convert.ToInt64(row["MessageAttachmentId"]),
                    MessageAttachmentId = row["MessageAttachmentId"] == DBNull.Value ? (Int64?)null : Convert.ToInt64(row["MessageAttachmentId"]),
                    DocumentFormat = Convert.ToString(row["DocumentFormat"]),

                  //  ProviderName_From = Convert.ToString(row["ProviderName_From"]),
                    

                  //  MessageTypeId = Convert.ToString(row["MessageTypeId"]),
               //     MessageStatusId = Convert.ToString(row["MessageStatusId"]),
                    //   MessageUrgencyId= Convert.ToString(row["MessageUrgencyId"]),
                   // ProviderId_From = Convert.ToString(row["ProviderId_From"]),
                   // ProviderName_To = Convert.ToString(row["ProviderName_To"]),

                  //  UserId_Created = Convert.ToString(row["UserId_Created"]),
                  //  CreatedByName = Convert.ToString(row["CreatedByName"]),


                  //  AttachmentId = Convert.ToString(row["AttachmentId"]),

                });
            }
            return TList;
        }

        public static List<ProviderMessageModel> ToProviderMessageModelList(this  System.Data.DataTable table)
        {
            List<ProviderMessageModel> TList = new List<ProviderMessageModel>();
            foreach (System.Data.DataRow row in table.Rows)
            {
                ProviderMessageModel objPMessage =new ProviderMessageModel();

                objPMessage.PatientName = Convert.ToString(row["PatientName"]);
                objPMessage.FacilityId = Convert.ToInt64(row["FacilityId"]);
                objPMessage.FacilityName = Convert.ToString(row["FacilityName"]);
                objPMessage.MessageId = Convert.ToInt64(row["MessageId"]);
                objPMessage.MessageDetailId = Convert.ToInt64(row["MessageDetailId"]);
                objPMessage.MessageStatus = Convert.ToString(row["MessageStatus"]);
                objPMessage.MessageType = Convert.ToString(row["MessageType"]);

                objPMessage.MedicationName = Convert.ToString(row["MedicationName"]);
                objPMessage.MessageRequest = Convert.ToString(row["MessageRequest"]);
                objPMessage.PharmacyName = Convert.ToString(row["PharmacyName"]);
                objPMessage.PreferredPeriod = Convert.ToString(row["PreferredPeriod"]);
                objPMessage.PreferredTime = Convert.ToString(row["PreferredTime"]);
                objPMessage.PreferredWeekDay = Convert.ToString(row["PreferredWeekDay"]);
                    //ProviderName_From = Convert.ToString(row["ProviderName_From"]);
                objPMessage.VisitReason = Convert.ToString(row["VisitReason"]);

                objPMessage.MessageTypeId = Convert.ToInt32(row["MessageTypeId"]);
                objPMessage.MessageStatusId = Convert.ToInt32(row["MessageStatusId"]);
                objPMessage.PatientId = Convert.ToInt64(row["PatientId"]);
                objPMessage.MessageUrgency = Convert.ToBoolean(row["MessageUrgency"]);
                objPMessage.ProviderId_From = Convert.ToInt64(row["ProviderId_From"]);
                objPMessage.ProviderFrom = Convert.ToString(row["ProviderFrom"]);
                objPMessage.ProviderId_To = Convert.ToInt64(row["ProviderId_To"]);
                objPMessage.ProviderTo = Convert.ToString(row["ProviderTo"]);
                    //ProviderName_To = Convert.ToString(row["ProviderName_To"]);

                    //UserId_Created = Convert.ToString(row["UserId_Created"]);
                    //CreatedByName = Convert.ToString(row["CreatedByName"]);
                objPMessage.MessageResponse = Convert.ToString(row["MessageResponse"]);
                objPMessage.MessageResponseTypeId =row["MessageResponseTypeId"]== DBNull.Value ? (Int32?)null : Convert.ToInt32(row["MessageResponseTypeId"]);
                objPMessage.MessageResponseType = Convert.ToString(row["MessageResponseType"]);
                objPMessage.AppointmentStart = Convert.ToDateTime(row["AppointmentStart"]);
                objPMessage.AppointmentEnd = Convert.ToDateTime(row["AppointmentEnd"]);
                objPMessage.ProviderId_Appointment = Convert.ToInt64(row["ProviderId_Appointment"]);
                objPMessage.ProviderAppointment = Convert.ToString(row["ProviderAppointment"]);
                objPMessage.MedicationNDC = Convert.ToString(row["MedicationNDC"]);

                objPMessage.MessageRead = Convert.ToBoolean(row["MessageRead"]);
                objPMessage.NoOfRefills = Convert.ToInt32(row["NoOfRefills"]);
                objPMessage.MedicationStatus = Convert.ToInt32(row["MedicationStatus"]);
                objPMessage.PharmacyAddress = Convert.ToString(row["PharmacyAddress"]);
                    //AttachmentId = Convert.ToString(row["AttachmentId"]);
                    //MessageUnRead = Convert.ToInt64(row["MessageUnRead"]);
                objPMessage.DateCreated = Convert.ToDateTime(row["DateCreated"]);
                objPMessage.PharmacyPhone = Convert.ToString(row["PharmacyPhone"]);
                objPMessage.MessageAttachmentId = row["MessageAttachmentId"] == DBNull.Value ? (Int64?)null : Convert.ToInt64(row["MessageAttachmentId"]);
                objPMessage.DocumentFormat = Convert.ToString(row["DocumentFormat"]);
                
                        TList.Add(objPMessage);
            }
            return TList;
        }
        //public static List<ProviderMessageModel> ToProviderMessageModelList(this  System.Data.DataTable table)
        //{
        //    List<ProviderMessageModel> TList = new List<ProviderMessageModel>();
        //    foreach (System.Data.DataRow row in table.Rows)
        //    {
        //        TList.Add(new ProviderMessageModel
        //        {
        //            PatientName = Convert.ToString(row["PatientName"]),
        //            FacilityId = Convert.ToInt64(row["FacilityId"]),
        //            FacilityName = Convert.ToString(row["FacilityName"]),
        //            MessageId = Convert.ToInt64(row["MessageId"]),
        //            MessageDetailId = Convert.ToInt64(row["MessageDetailId"]),
        //            MessageStatus = Convert.ToString(row["MessageStatus"]),
        //            MessageType = Convert.ToString(row["MessageType"]),

        //            MedicationName = Convert.ToString(row["MedicationName"]),
        //            MessageRequest = Convert.ToString(row["MessageRequest"]),
        //            PharmacyName = Convert.ToString(row["PharmacyName"]),
        //            PreferredPeriod = Convert.ToString(row["PreferredPeriod"]),
        //            PreferredTime = Convert.ToString(row["PreferredTime"]),
        //            PreferredWeekDay = Convert.ToString(row["PreferredWeekDay"]),
        //            //ProviderName_From = Convert.ToString(row["ProviderName_From"]),
        //            VisitReason = Convert.ToString(row["VisitReason"]),

        //            MessageTypeId = Convert.ToInt32(row["MessageTypeId"]),
        //            MessageStatusId = Convert.ToInt32(row["MessageStatusId"]),
        //            PatientId = Convert.ToInt64(row["PatientId"]),
        //            MessageUrgency = Convert.ToBoolean(row["MessageUrgency"]),
        //            ProviderId_From = Convert.ToInt64(row["ProviderId_From"]),
        //            ProviderFrom = Convert.ToString(row["ProviderFrom"]),
        //            ProviderId_To = Convert.ToInt64(row["ProviderId_To"]),
        //            ProviderTo = Convert.ToString(row["ProviderTo"]),
        //            //ProviderName_To = Convert.ToString(row["ProviderName_To"]),

        //            //UserId_Created = Convert.ToString(row["UserId_Created"]),
        //            //CreatedByName = Convert.ToString(row["CreatedByName"]),
        //            MessageResponse = Convert.ToString(row["MessageResponse"]),
        //            MessageResponseTypeId = Convert.ToInt32(row["MessageResponseTypeId"]),
        //            MessageResponseType = Convert.ToString(row["MessageResponseType"]),
        //            AppointmentStart = Convert.ToDateTime(row["AppointmentStart"]),
        //            AppointmentEnd = Convert.ToDateTime(row["AppointmentEnd"]),
        //            ProviderId_Appointment = Convert.ToInt64(row["ProviderId_Appointment"]),
        //            ProviderAppointment = Convert.ToString(row["ProviderAppointment"]),
        //            MedicationNDC = Convert.ToString(row["MedicationNDC"]),

        //            MessageRead = Convert.ToBoolean(row["MessageRead"]),
        //            NoOfRefills = Convert.ToInt32(row["NoOfRefills"]),
        //            MedicationStatus = Convert.ToInt32(row["MedicationStatus"]),
        //            PharmacyAddress = Convert.ToString(row["PharmacyAddress"]),
        //            //AttachmentId = Convert.ToString(row["AttachmentId"]),
        //            //MessageUnRead = Convert.ToInt64(row["MessageUnRead"]),
        //            DateCreated = Convert.ToDateTime(row["DateCreated"]),
        //            PharmacyPhone = Convert.ToString(row["PharmacyPhone"]),
        //            MessageAttachmentId = row["MessageAttachmentId"] == DBNull.Value ? (Int64?)null : Convert.ToInt64(row["MessageAttachmentId"]),
        //            DocumentFormat = Convert.ToString(row["DocumentFormat"])
        //        });
        //    }
        //    return TList;
        //}
        
        
        public static List<PatientMessageModel> ToMessageModelDetailInboxList(this  System.Data.DataTable table)
        {
            List<PatientMessageModel> TList = new List<PatientMessageModel>();
            foreach (System.Data.DataRow row in table.Rows)
            {
                TList.Add(new PatientMessageModel
                {
                    MedicationName = Convert.ToString(row["MedicationName"]),
                    MessageRequest = Convert.ToString(row["MessageRequest"]),
                    PharmacyName = Convert.ToString(row["PharmacyName"]),
                    PreferredPeriod = Convert.ToString(row["PreferredPeriod"]),
                    PreferredTime = Convert.ToString(row["PreferredTime"]),
                    PreferredWeekDay = Convert.ToString(row["PreferredWeekDay"]),
                    ProviderName_From = Convert.ToString(row["ProviderName_From"]),
                    VisitReason = Convert.ToString(row["VisitReason"]),

                  
                //    MessageUrgencyId = Convert.ToString(row["MessageUrgencyId"]),
                    MessageUrgency = Convert.ToBoolean(row["MessageUrgency"]),
                    ProviderId_From = Convert.ToString(row["ProviderId_From"]),
                    ProviderId_To = Convert.ToString(row["ProviderId_To"]),
                    ProviderName_To = Convert.ToString(row["ProviderName_To"]),

                    MessageResponse = Convert.ToString(row["MessageResponse"]),
                    MessageType= Convert.ToString(row["MessageType"]),
                    MessageTypeId = Convert.ToString(row["MessageTypeId"]),
                    FacilityId = Convert.ToInt64(row["FacilityId"]),
                    FacilityName = Convert.ToString(row["FacilityName"]),
                    AppointmentStart = Convert.ToDateTime(row["AppointmentStart"]),
                    AppointmentEnd = Convert.ToDateTime(row["AppointmentEnd"]),
                    ProviderId_Appointment = Convert.ToString(row["ProviderId_Appointment"]),
                    MedicationNDC = Convert.ToString(row["MedicationNDC"]),


                    NoOfRefills = Convert.ToString(row["NoOfRefills"]),
                    MedicationStatus = Convert.ToString(row["MedicationStatus"]),
                    PharmacyAddress = Convert.ToString(row["PharmacyAddress"]),
                    MessageDetailId =Convert.ToInt64(row["MessageDetailId"]),
                    AttachmentId = Convert.ToString(row["AttachmentId"]),
                    DocumentFormat = Convert.ToString(row["DocumentFormat"]),
                    CreatedByName = Convert.ToString(row["CreatedByName"]),
                    MessageRead = Convert.ToBoolean(row["MessageRead"]),
                    MessageId = Convert.ToInt64(row["MessageId"]),
                    DateCreated = Convert.ToDateTime(row["DateCreated"]),
                    PharmacyPhone = Convert.ToString(row["PharmacyPhone"])

                });
            }
            return TList;
        }


        
        public static List<MessageModel> ToMessageModelDetailList(this  System.Data.DataTable table)
        {
            List<MessageModel> TList = new List<MessageModel>();
            foreach (System.Data.DataRow row in table.Rows)
            {
                TList.Add(new MessageModel
                {
      

                    AppointmentEnd = Convert.ToDateTime(row["AppointmentEnd"]),
                    AppointmentStart = Convert.ToDateTime(row["AppointmentStart"]),
                    AttachmentId = Convert.ToString(row["AttachmentId"]),
            
                    MedicationName = Convert.ToString(row["MedicationName"]),
                    MedicationNDC = Convert.ToString(row["MedicationNDC"]),
                    MedicationStatus = Convert.ToInt32(row["MedicationStatus"]),
                    MessageDetailId = Convert.ToInt32(row["MessageDetailId"]),

              
                    MessageRequest = Convert.ToString(row["MessageRequest"]),
                    MessageResponse = Convert.ToString(row["MessageResponse"]),
                    MessageResponseTypeId = Convert.ToInt32(row["MessageResponseTypeId"]),

                    MessageUrgency = Convert.ToBoolean(row["MessageUrgency"]),
                    NoOfRefills = Convert.ToInt32(row["NoOfRefills"]),
             
                    PharmacyAddress = Convert.ToString(row["PharmacyAddress"]),
                    PharmacyName = Convert.ToString(row["PharmacyName"]),
                    PreferredPeriod = Convert.ToString(row["PreferredPeriod"]),
                    PreferredTime = Convert.ToString(row["PreferredTime"]),
                    PreferredWeekDay = Convert.ToString(row["PreferredWeekDay"]),
                    ProviderId_Appointment = Convert.ToInt64(row["ProviderId_Appointment"]),
                    ProviderId_From = Convert.ToInt64(row["ProviderId_From"]),
                    ProviderId_To = Convert.ToInt64(row["ProviderId_To"]),
                    VisitReason = Convert.ToString(row["VisitReason"])

                });
            }
            return TList;
        }

        
        public static List<ImmunizationModel> ToImmunizationModelList(this  System.Data.DataTable table)
        {
            List<ImmunizationModel> TList = new List<ImmunizationModel>();
            foreach (System.Data.DataRow row in table.Rows)
            {
                string strTime;
                string strDate;
                if (Convert.ToString(row["ImmunizationDate"]).Trim() == "" || Convert.ToString(row["ImmunizationDate"]) == null || Convert.ToString(row["ImmunizationDate"]) == "00010101")
                {
                    strDate = "";
                }
                else
                {
                    var VaccineDate = DateTime.ParseExact(Convert.ToString(row["ImmunizationDate"]), "yyyyMMdd", null);
                    strDate = VaccineDate.ToString("MM/dd/yyyy");
                }
                if (Convert.ToString(row["ImmunizationTime"]).Trim() == "" || Convert.ToString(row["ImmunizationTime"]) == null )
                {
                    strTime = "";
                }
                else
                {
                    var VaccineTime = DateTime.ParseExact(Convert.ToString(row["ImmunizationTime"]), "HHmm", null);
                    strTime = VaccineTime.ToString("hh:mm tt");
                }
                
               

                TList.Add(new ImmunizationModel
                {
                    
                    // Valid = Convert.ToBoolean(row["Valid"]),
                    //ErrorMessage = Convert.ToString(row["ErrorMessage"]),
                    //PatientId = Convert.ToInt64(row["PatientId"]),
                    //VisitId = Convert.ToInt64(row["VisitId"]),
                    PatientImmunizationCntr = Convert.ToInt64(row["PatientImmunizationCntr"]),
                    //FacilityId = Convert.ToInt64(row["FacilityId"]),
                    Vaccine = Convert.ToString(row["Vaccine"]),
                    Amount = Convert.ToString(row["Amount"]),
                    Route = Convert.ToString(row["Route"]),
                    Site = Convert.ToString(row["Site"]),
                    Sequence = Convert.ToString(row["Sequence"]),
                    ExpirationDate = Convert.ToDateTime(row["ExpirationDate"]),
                    LotNumber = Convert.ToString(row["LotNumber"]),
                    Manufacturer = Convert.ToString(row["Manufacturer"]),
                    Note = Convert.ToString(row["Note"]),
                    Time =   strTime,
                    ImmunizationDate = strDate

                });
            }
            return TList;
        }

        
        public static List<AllergyModel> ToAllergyModelList(this System.Data.DataTable table)
        {
            List<AllergyModel> TList = new List<AllergyModel>();
            foreach (System.Data.DataRow row in table.Rows)
            {
                TList.Add(new AllergyModel
                {
                    PatientAllergyCntr = Convert.ToInt64(row["PatientAllergyCntr"]),
                    Allergen = Convert.ToString(row["Allergen"]),
                    AllergenType = Convert.ToString(row["AllergenType"]),
                    CodeValue_Allergen = Convert.ToString(row["CodeValue_Allergen"]),
                    Reaction = Convert.ToString(row["Reaction"]),
                    Note = Convert.ToString(row["Note"]),
                    EffectiveDate = Convert.ToString(row["EffectiveDate"]),
                    ConditionStatus = Convert.ToString(row["ConditionStatus"]),
                    ConditionStatusId = Convert.ToInt16(row["ConditionStatusId"])

                });
            }
            return TList;
        }

       // 
        public static List<PatientVitalSignModel> ToVitalsignModelList(this  System.Data.DataTable table)
        {
            List<PatientVitalSignModel> TList = new List<PatientVitalSignModel>();
            foreach (System.Data.DataRow row in table.Rows)
            {
                TList.Add(new PatientVitalSignModel
                {
                    // Valid = Convert.ToBoolean(row["Valid"]),
                    // ErrorMessage = Convert.ToString(row["ErrorMessage"]),
                    // PatientId = Convert.ToInt64(row["PatientId"]),
                    //VisitId = Convert.ToInt64(row["VisitId"]),
                    PatientVitalCntr = Convert.ToInt64(row["PatientVitalCntr"]),
                    //FacilityId = Convert.ToInt64(row["FacilityId"]),
                    BloodPressurePosn = Convert.ToString(row["BloodPressurePosn"]),
                    HeightIn = Convert.ToInt32(row["HeightIn"]),
                    WeightLb = Convert.ToInt32(row["WeightLb"]),
                    Systolic = Convert.ToInt32(row["Systolic"]),
                    Diastolic = Convert.ToInt32(row["Diastolic"]),
                    Pulse = Convert.ToInt32(row["Pulse"]),
                    Respiration = Convert.ToInt32(row["Respiration"]),
                    VitalDate = Convert.ToDateTime(row["VitalDate"]),
                    Temperature = Convert.ToDecimal(row["Temperature"])


                });
            }
            return TList;
        }


        
        public static List<MedicalHistoryModel> ToMedicalHistoryModelList(this  System.Data.DataTable table)
        {
            List<MedicalHistoryModel> TList = new List<MedicalHistoryModel>();
            foreach (System.Data.DataRow row in table.Rows)
            {
                TList.Add(new MedicalHistoryModel
                {
                    PatMedicalHistCntr = Convert.ToInt64(row["PatMedicalHistCntr"]),
                    DateModified = Convert.ToDateTime(row["DateModified"]),
                    DateOfOccurance = row["DateOfOccurance"].ToString(),
                    Description = row["Description"].ToString(),
                    OnCard = Convert.ToBoolean(row["OnCard"]),
                    OnKeys = Convert.ToBoolean(row["OnKeys"]),
                    Note = row["Note"].ToString()


                });
            }
            return TList;
        }

        public static List<MedicalHistoryModel> ToMedicalHistoryClinicalModelList(this  System.Data.DataTable table)
        {
            List<MedicalHistoryModel> TList = new List<MedicalHistoryModel>();
            foreach (System.Data.DataRow row in table.Rows)
            {
                TList.Add(new MedicalHistoryModel
                {
                    PatMedicalHistCntr = Convert.ToInt64(row["PatMedicalHistCntr"]),
                    DateOfOccurance = row["DateOfOccurance"].ToString(),
                    Description = row["Description"].ToString(),
                    Note = row["Note"].ToString()


                });
            }
            return TList;
        }

        public static List<SocialHistoryModel> ToSocialHistoryModelList(this  System.Data.DataTable table)
        {
            List<SocialHistoryModel> TList = new List<SocialHistoryModel>();
            foreach (System.Data.DataRow row in table.Rows)
            {
                TList.Add(new SocialHistoryModel
                {
                    PatSocialHistCntr = Convert.ToInt64(row["PatSocialHistCntr"]),
                    BeginDate = row["BeginDate"].ToString(),
                    CodeSystemId = Convert.ToInt64(row["CodeSystemId"].ToString()),
                    CodeValue = row["CodeValue"].ToString(),
                    CodeSystem = row["CodeSystem"].ToString(),
                    DateModified = Convert.ToDateTime(row["DateModified"].ToString()),
                    Description = row["Description"].ToString(),
                    EndDate = row["EndDate"].ToString(),
                    Note = row["Note"].ToString(),
                    Qualifier = row["Qualifier"].ToString()

                });
            }
            return TList;
        }

        
        public static List<PatientPharmacyModel> ToPatientPharmacyModellList(this  System.Data.DataTable table)
        {
            List<PatientPharmacyModel> TList = new List<PatientPharmacyModel>();
            foreach (System.Data.DataRow row in table.Rows)
            {
                TList.Add(new PatientPharmacyModel
                {
                    PharmacyCntr = Convert.ToInt64(row["PharmacyCntr"]),
                    PharmacyName = row["PharmacyName"].ToString(),
                    Address1 = row["Address1"].ToString(),
                    Address2 = row["Address2"].ToString(),
                    Address3 = row["Address3"].ToString(),
                    City = row["City"].ToString(),
                    Note = row["Note"].ToString(),
                    State = row["State"].ToString(),
                    PostalCode = row["PostalCode"].ToString(),
                    CountryCode = row["CountryCode"].ToString(),
                    Preferred = Convert.ToBoolean(row["Preferred"].ToString()),
                    Phone=Convert.ToString(row["Phone"]),
                    //  PatientId = Convert.ToInt64(row["PatientId"].ToString())


                });
            }
            return TList;
        }


      
        public static List<PatientModel> ToPatientModel(this System.Data.DataTable table){
            List<PatientModel> TList = new List<PatientModel>();
            foreach (System.Data.DataRow row in table.Rows)
            {
                TList.Add(new PatientModel
                {

                    PatientId = Convert.ToInt64(row["PatientId"]),
                    FirstName = row["FirstName"].ToString(),
                    LastName = row["LastName"].ToString(),
                    EMail=row["Email"].ToString()
                

                });
            }
            return TList;
        
        }

        public static List<ProviderModel> ToProviderModel(this System.Data.DataTable table)
        {
            List<ProviderModel> TList = new List<ProviderModel>();
            foreach (System.Data.DataRow row in table.Rows)
            {
                TList.Add(new ProviderModel
                {

                    ProviderId = Convert.ToInt64(row["ProviderId"]),
                    FirstName = row["FirstName"].ToString(),
                    LastName = row["LastName"].ToString(),
                    Email = row["Email"].ToString()


                });
            }
            return TList;

        }
        public static List<PatientMedicationModel> ToMedicationHistoryModelList(this  System.Data.DataTable table)
        {
            List<PatientMedicationModel> TList = new List<PatientMedicationModel>();
            foreach (System.Data.DataRow row in table.Rows)
            {
                TList.Add(new PatientMedicationModel
                {
                    CodeValue = row["CodeValue"].ToString(),
                    Active = Convert.ToBoolean(row["Active"]),
                    DateModified = Convert.ToDateTime(row["DateModified"].ToString()),
                    //Days = Convert.ToInt32(row["Days"].ToString()),
                    Diagnosis = row["Diagnosis"].ToString(),
                    ExpireDate = Convert.ToDateTime(row["ExpireDate"].ToString()),
                    StartDate = Convert.ToDateTime(row["StartDate"].ToString()),
                    Frequency = row["Frequency"].ToString(),
                    MedicationName = row["MedicationName"].ToString(),
                  //  NDC = row["NDC"].ToString(),
                    Note = row["Note"].ToString(),
                    PatientMedicationCntr = row["PatientMedicationCntr"].ToString(),
                    Pharmacy = row["Pharmacy"].ToString(),
                  //  Status = row["Status"].ToString(),
                    Quantity = Convert.ToInt32(row["Quantity"].ToString()),
                    Refills = Convert.ToInt32(row["Refills"].ToString()),
                    Route = row["RouteId"].ToString(),
                    Sig = row["Sig"].ToString(),
                    duringvisit=Convert.ToBoolean(row["DuringVisit"]),
                    // Status = row["Status"].ToString(),
                    Dose = Convert.ToInt32(row["Dose"]),
                    DoseUnit = Convert.ToString(row["DoseUnit"])
                });
            }
            return TList;
        }

        
        public static List<PatientPharmacyModel> ToPatientPhramcyModelList(this  System.Data.DataTable table)
        {
            List<PatientPharmacyModel> TList = new List<PatientPharmacyModel>();
            foreach (System.Data.DataRow row in table.Rows)
            {
                TList.Add(new PatientPharmacyModel
                {
                    Preferred = Convert.ToBoolean(row["Preferred"]),
                    //DateModified = Convert.ToDateTime(row["DateModified"].ToString()),
                    PharmacyName = row["PharmacyName"].ToString(),
                    Address1 = row["Address1"].ToString(),
                    Address2 = row["Address2"].ToString(),
                    Address3 = row["Address3"].ToString(),
                    State = row["State"].ToString(),
                    City = row["City"].ToString(),
                    CountryCode = row["CountryCode"].ToString(),
                    PharmacyCntr = Convert.ToInt64(row["PharmacyCntr"].ToString()),
                    PostalCode = row["PostalCode"].ToString(),
                    Note = row["Note"].ToString(),
                    Phone= row["Phone"].ToString(),


                });
            }
            return TList;
        }
        
        public static List<FamilyHistoryModel> ToFamilyHistoryModelList(this  System.Data.DataTable table)
        {
            List<FamilyHistoryModel> TList = new List<FamilyHistoryModel>();
            foreach (System.Data.DataRow row in table.Rows)
            {
                TList.Add(new FamilyHistoryModel
                {
                    PatFamilyHistCntr = Convert.ToInt64(row["PatFamilyHistCntr"]),
                    SNOMEDCode = row["SNOMEDCode"].ToString(),
                    AgeAtOnset=Convert.ToInt32(row["AgeAtOnSet"]),
                    DiseasedAge = Convert.ToInt32(row["DiseasedAge"]),
                    ConditionStatusId = Convert.ToInt16(row["ConditionStatusId"]),
                    RelationshipId = Convert.ToInt16(row["RelationshipId"]),
                    CodeSystemId = Convert.ToInt32(row["CodeSystemId"].ToString()),
                    CodeValue = row["CodeValue"].ToString(),
                    DateModified = Convert.ToDateTime(row["DateModified"].ToString()),
                    Description = row["Description"].ToString(),
                //  DateReported = row["DateReported"].ToString(),
                    Note = row["Note"].ToString(),
                    Qualifier = row["Qualifier"].ToString(),
                    FamilyMember = row["FamilyMember"].ToString(),
                    Diseased = Convert.ToBoolean(row["Diseased"])
                                    });
            }
            return TList;
        }

        public static List<FamilyHistoryModel> ToFamilyHistoryClinicalModelList(this  System.Data.DataTable table)
        {
            List<FamilyHistoryModel> TList = new List<FamilyHistoryModel>();
            foreach (System.Data.DataRow row in table.Rows)
            {
                TList.Add(new FamilyHistoryModel
                {
                    PatFamilyHistCntr = Convert.ToInt64(row["PatFamilyHistCntr"]),
                    RelationshipId = Convert.ToInt16(row["RelationshipId"]),
                    CodeSystemId = Convert.ToInt32(row["CodeSystemId"].ToString()),
                    CodeValue = row["CodeValue"].ToString(),
                    Description = row["Description"].ToString(),
                    Relationship = row["Relationship"].ToString(),
                    CodeSystem = row["CodeSystem"].ToString(),
                    Note = row["Note"].ToString(),
                });
            }
            return TList;
        }

        public static List<FacilityModel> ToFacilityModelList(this  System.Data.DataTable table)
        {
            List<FacilityModel> TList = new List<FacilityModel>();
            foreach (System.Data.DataRow row in table.Rows)
            {
                TList.Add(new FacilityModel { FacilityID = Convert.ToInt64(row["FacilityId"]), FacilityName = row["FacilityName"].ToString() });
            }
            return TList;
        }
        
        public static List<ProviderModel> ToProviderModelList(this  System.Data.DataTable table)
        {
            List<ProviderModel> TList = new List<ProviderModel>();
            foreach (System.Data.DataRow row in table.Rows)
            {
                TList.Add(new ProviderModel { ProviderId = Convert.ToInt64(row["ProviderId"]), FullName = row["Name"].ToString() });
            }
            return TList;
        }
        public static List<ProviderModel> ToProviderFacilityModelList(this  System.Data.DataTable table)
        {
            List<ProviderModel> TList = new List<ProviderModel>();
            foreach (System.Data.DataRow row in table.Rows)
            {
                TList.Add(new ProviderModel { ProviderId = Convert.ToInt64(row["ProviderId"]), FullName = row["FirstName"].ToString() + " " + row["LastName"].ToString() });
            }
            return TList;
        }

        public static List<PatientListModel> ToPatientModelList(this  System.Data.DataTable table)
        {
            List<PatientListModel> TList = new List<PatientListModel>();
            foreach (System.Data.DataRow row in table.Rows)
            {
                TList.Add(new PatientListModel { PatientId = Convert.ToInt64(row["PatientId"]), Name = row["Name"].ToString() });
            }
            return TList;
        }

        public static List<MessageStatusModel> ToMessageStatusModelList(this  System.Data.DataTable table)
        {
            List<MessageStatusModel> TList = new List<MessageStatusModel>();
            foreach (System.Data.DataRow row in table.Rows)
            {
                TList.Add(new MessageStatusModel { MessageStatusId = Convert.ToInt64(row["MessageStatusId"]), Value = row["Value"].ToString() });
            }
            return TList;
        }

        public static List<PatientListModel> ToPatientModel1List(this  System.Data.DataTable table)
        {
            List<PatientListModel> TList = new List<PatientListModel>();
            foreach (System.Data.DataRow row in table.Rows)
            {
                TList.Add(new PatientListModel { PatientId = Convert.ToInt64(row["PatientId"]), Name = row["FirstName"].ToString() +" "+ row["LastName"].ToString() });
            }
            return TList;
        }

        public static List<SocialModel> ToSocialHistModelList(this  System.Data.DataTable table)
        {
            List<SocialModel> TList = new List<SocialModel>();
            foreach (System.Data.DataRow row in table.Rows)
            {
                TList.Add(new SocialModel { SNOMED_Social = Convert.ToInt64(row["SNOMED_Social"]), Value = row["Value"].ToString() });
            }
            return TList;
        }

        public static IEnumerable<SelectListItem> ToProviderModelSelectList(this  List<ProviderModel> collection)
        {
            List<SelectListItem> TList = collection.Select(x => new SelectListItem { Text = x.FullName, Value = x.ProviderId.ToString() }).ToList();
            return TList;
        }

        public static IEnumerable<SelectListItem> ToPatientModelSelectList(this  List<PatientListModel> collection)
        {
            List<SelectListItem> TList = collection.Select(x => new SelectListItem { Text = x.Name, Value = x.PatientId.ToString() }).ToList();
            return TList;
        }

        public static IEnumerable<SelectListItem> ToSocialHistModelSelectList(this  List<SocialModel> collection)
        {
            List<SelectListItem> TList = collection.Select(x => new SelectListItem { Text = x.Value, Value = x.SNOMED_Social.ToString() }).ToList();
            return TList;
        }

        public static IEnumerable<SelectListItem> ToMedicalModelSelectList(this  List<PatientMedicationModel> collection)
        {
            List<SelectListItem> TList = collection.Select(x => new SelectListItem { Text = x.MedicationName, Value = x.PatientMedicationCntr.ToString() }).ToList();
            return TList;
        }
        
        public static IEnumerable<SelectListItem> ToProblemSNOMEDModelSelectList(this  List<ProblemSNOMEDModel> collection)
        {
            List<SelectListItem> TList = collection.Select(x => new SelectListItem { Text = x.Value , Value = x.ProblemID.ToString() }).ToList();
            return TList;
        }
        
        public static IEnumerable<SelectListItem> ToPharmacyModelSelectList(this  List<PatientPharmacyModel> collection)
        {
            List<SelectListItem> TList = collection.Select(x => new SelectListItem { Text = x.PharmacyName, Value = x.PharmacyCntr.ToString() }).ToList();
            return TList;
        }

        public static IEnumerable<SelectListItem> ToPharmacyModelForRefillSelectList(this  List<PatientPharmacyModel> collection)
        {
            List<SelectListItem> TList = collection.Select(x => new SelectListItem { Text = x.PharmacyName, Value = x.PharmacyCntr.ToString() + "|" + x.Address1 + " " + x.City + " " + x.State + " " + x.PostalCode + "|" + x.Phone }).ToList();
            return TList;
        }
        public static IEnumerable<SelectListItem> ToFacilityModelSelectList(this  List<FacilityModel> collection)
        {
            List<SelectListItem> TList = collection.Select(x => new SelectListItem { Text = x.FacilityName, Value = x.FacilityID.ToString() }).ToList();
            return TList;
        }
       // 
        public static IEnumerable<SelectListItem> ToFacilityModelWithoutPatientEnteredSelectList(this  List<FacilityModel> collection)
        {
            List<SelectListItem> TList = collection.Where(j => j.FacilityName!="Patient Entered").Select(x => new SelectListItem { Text = x.FacilityName, Value = x.FacilityID.ToString() }).ToList();
            return TList;
        }


       
       
        public static List<VisitModel> ToVisitModelList(this  System.Data.DataTable table)
        {
            List<VisitModel> TList = new List<VisitModel>();
            foreach (System.Data.DataRow row in table.Rows)
            {
                VisitModel visit = new VisitModel();
               
                    visit.VisitID = Convert.ToInt64(row["VisitId"]);
                     visit.VisitDate = Convert.ToDateTime(row["VisitDate"]);
                     visit.FacilityName = row["FacilityName"].ToString();
                     visit.FacilityAddress = row["FacilityAddress"].ToString();
                     visit.FacilityCityStatePostal = row["FacilityCityStatePostal"].ToString();
                     visit.ProviderName = row["ProviderName"].ToString();
                     visit.VisitReason = row["VisitReason"].ToString();
                     visit.PatientId = Convert.ToInt64(row["PatientId"].ToString());
                     visit.FacilityId = Convert.ToInt64(row["FacilityId"].ToString());
                     visit.ProviderId = Convert.ToInt64(row["ProviderId"].ToString());

               
               TList.Add(visit);
            }
            return TList;
        }

        public static List<PatientEmergency> ToEmergencyModelList(this  System.Data.DataTable table)
        {
            List<PatientEmergency> TList = new List<PatientEmergency>();
            foreach (System.Data.DataRow row in table.Rows)
            {
                PatientEmergency emergency = new PatientEmergency();

                emergency.PatientEmergencyId = Convert.ToInt64(row["PatientEmergencyId"]);
                emergency.Name = row["Name"].ToString();

                emergency.Address1 = row["Address1"].ToString();
                emergency.Address2 = row["Address2"].ToString();
                emergency.Address3 = row["Address3"].ToString();

                emergency.City = row["City"].ToString();
                emergency.State = row["State"].ToString();
                emergency.PostalCode = row["PostalCode"].ToString();
                emergency.CountryCode = row["CountryCode"].ToString();

                emergency.Relationship = row["Relationship"].ToString();
                emergency.HomePhone = row["HomePhone"].ToString();
                emergency.MobilePhone = row["MobilePhone"].ToString();
                emergency.WorkPhone = row["WorkPhone"].ToString();

                emergency.IsPrimary = Convert.ToBoolean(row["IsPrimary"]);
                emergency.OnCard = Convert.ToBoolean(row["OnCard"]);
                emergency.RelationshipId = Convert.ToInt32(row["RelationshipId"]);


                TList.Add(emergency);
            }
            return TList;
        }

        public static List<DoctorUploadModel> ToDoctorUploadModelList(this  System.Data.DataTable table)
        {
            List<DoctorUploadModel> TList = new List<DoctorUploadModel>();
            foreach (System.Data.DataRow row in table.Rows)
            {
                DoctorUploadModel drUpload = new DoctorUploadModel();

                drUpload.DocumentCntr = Convert.ToInt64(row["DocumentCntr"]);
                drUpload.DocumentDescription = row["DocumentDescription"].ToString();
                drUpload.DoctorName = row["DoctorName"].ToString();
                drUpload.DateCreated = Convert.ToDateTime(row["DateCreated"]);
                drUpload.DocumentFormat = Convert.ToString(row["DocumentFormat"]);
                drUpload.Notes = row["Notes"].ToString();
                TList.Add(drUpload);
            }
            return TList;
        }

        public static List<PatientUploadModel> ToPatientUploadModelList(this  System.Data.DataTable table)
        {
            List<PatientUploadModel> TList = new List<PatientUploadModel>();
            foreach (System.Data.DataRow row in table.Rows)
            {
                PatientUploadModel ptUpload = new PatientUploadModel();
                ptUpload.DocumentCntr = Convert.ToInt64(row["DocumentCntr"]);
                ptUpload.DocumentDescription = row["DocumentDescription"].ToString();
                ptUpload.FacilityName = row["FacilityName"].ToString();
                ptUpload.DoctorName = row["DoctorName"].ToString();
                ptUpload.DateCreated = Convert.ToDateTime(row["DateCreated"]);
                ptUpload.DocumentFormat = Convert.ToString(row["DocumentFormat"]);
                ptUpload.Notes = row["Notes"].ToString();
                TList.Add(ptUpload);
            }
            return TList;
        }

        public static List<PatientPolicyModel> ToPolicyModelList(this  System.Data.DataTable table)
        {
            List<PatientPolicyModel> TList = new List<PatientPolicyModel>();
            foreach (System.Data.DataRow row in table.Rows)
            {
                PatientPolicyModel policy = new PatientPolicyModel();

                policy.PatientPolicyId = Convert.ToInt64(row["PatientPolicyId"]);
                policy.PolicyTypeId = Convert.ToInt32(row["PolicyTypeId"]);
                policy.PolicyTypeName = row["PolicyTypeName"].ToString();
                policy.Company = row["Company"].ToString();
                policy.PolicyNo = row["PolicyNo"].ToString();
                policy.PlanNumber = row["PlanNumber"].ToString();
                policy.GroupNumber = row["GroupNumber"].ToString();
                policy.Agent = row["Agent"].ToString();
                policy.AgentPhone = row["AgentPhone"].ToString();
                policy.AgentFax = row["AgentFax"].ToString();
                policy.Notes = row["Notes"].ToString();

                TList.Add(policy);
            }
            return TList;
        }

        public static List<PatientInsuranceModel> ToInsuranceModelList(this  System.Data.DataTable table)
        {
            List<PatientInsuranceModel> TList = new List<PatientInsuranceModel>();
            foreach (System.Data.DataRow row in table.Rows)
            {
                PatientInsuranceModel policy = new PatientInsuranceModel();

                policy.PatientInsuranceId = Convert.ToInt64(row["PatientInsuranceId"]);
                policy.GroupNumber = row["GroupNumber"].ToString();
                policy.EffectiveDate = Convert.ToDateTime(row["EffectiveDate"]);
                policy.MemberNumber = row["MemberNumber"].ToString();
                policy.PlanName = row["PlanName"].ToString();
                policy.Relationship = row["Relationship"].ToString();
                policy.Subscriber = row["Subscriber"].ToString();

                TList.Add(policy);
            }
            return TList;
        }
        public static List<PatientMedicalDocumentModel> ToDocumentModelList(this  System.Data.DataTable table)
        {
            List<PatientMedicalDocumentModel> TList = new List<PatientMedicalDocumentModel>();
            foreach (System.Data.DataRow row in table.Rows)
            {
                PatientMedicalDocumentModel document = new PatientMedicalDocumentModel();

                document.DocumentCntr = Convert.ToInt64(row["DocumentCntr"]);
                document.DocumentDescription = row["DocumentDescription"].ToString();
                document.Notes = row["Notes"].ToString();
                document.DateCreated = Convert.ToDateTime(row["DateCreated"]);
                document.Viewable = Convert.ToBoolean(row["Viewable"]);
                document.DocumentFormat = row["DocumentFormat"].ToString();
                TList.Add(document);
            }
            return TList;
        }
        public static List<DocumentModelForAdmin> ToDocumentModelListAdmin(this  System.Data.DataTable table)
        {
            List<DocumentModelForAdmin> TList = new List<DocumentModelForAdmin>();
            foreach (System.Data.DataRow row in table.Rows)
            {
                DocumentModelForAdmin document = new DocumentModelForAdmin();

                document.DocumentCntr = Convert.ToInt64(row["DocumentCntr"]);
                document.DocumentDescription = row["DocumentDescription"].ToString();
                document.DocType = row["DocType"].ToString();
                document.PatientId = Convert.ToInt64(row["PatientId"]);
                document.FacilityId = Convert.ToInt64(row["FacilityId"]);
                document.VisitId = Convert.ToInt64(row["VisitId"]);
                TList.Add(document);
            }
            return TList;
        }


        public static List<PatientMedicalDocumentModel> ToConsolidatedCallDocumentModelList(this  System.Data.DataTable table)
        {
            List<PatientMedicalDocumentModel> TList = new List<PatientMedicalDocumentModel>();
               foreach (System.Data.DataRow row in table.Rows)
                {
                    PatientMedicalDocumentModel document = new PatientMedicalDocumentModel();

                    document.DocumentCntr = Convert.ToInt64(row["DocumentCntr"]);
                    document.DocumentDescription = row["DocumentDescription"].ToString();
                    document.DocumentFormat = row["DocumentFormat"].ToString();
                    document.Notes = row["Notes"].ToString();
                    document.DateCreated = Convert.ToDateTime(row["DateCreated"]);

                    TList.Add(document);
                }
            
            return TList;
        }


        public static IEnumerable<SelectListItem> ToVisitModelSelectList(this  List<VisitModel> collection)
        {
            List<SelectListItem> TList = collection.Select(x => new SelectListItem { Text = x.VisitDate.ToShortDateString(), Value = x.VisitID.ToString() }).ToList();
            return TList;
        }

        public static IEnumerable<SelectListItem> TocarrierModelSelectList(this  List<CarrierModel> collection)
        {
            List<SelectListItem> TList = collection.Select(x => new SelectListItem { Text = x.CarrierName.ToString(), Value = x.CarrierId.ToString() }).ToList();
            return TList;
        }

       

        public static IEnumerable<SelectListItem> ToGenderModelSelectList(this List<GenderModel> collection)
        {
            List<SelectListItem> TList = collection.Select(x => new SelectListItem { Text = x.Value, Value = x.GenderID.ToString() }).ToList();
            return TList;
        }

        
        public static List<GenderModel> ToGenderModelList(this System.Data.DataTable table)
        {
            List<GenderModel> TList = new List<GenderModel>();
            foreach (System.Data.DataRow row in table.Rows)
            {
                TList.Add(new GenderModel
                {
                    GenderID = Convert.ToInt64(row["GenderID"]),
                    Value = Convert.ToString(row["Value"]),
                });
            }
            return TList;
        }

        
        public static List<RelationshipModel> ToRelationshipModelList(this System.Data.DataTable table)
        {
            List<RelationshipModel> TList = new List<RelationshipModel>();
            foreach (System.Data.DataRow row in table.Rows)
            {
                TList.Add(new RelationshipModel
                {
                    RelationShipID = Convert.ToInt64(row["RelationShipId"]),
                    Value = Convert.ToString(row["Value"]),
                    SNOMED = Convert.ToString(row["SNOMED"])

                });
            }
            return TList;
        }

        public static List<VaccineModel> ToVaccineModelList(this System.Data.DataTable table)
        {
            List<VaccineModel> TList = new List<VaccineModel>();
            foreach (System.Data.DataRow row in table.Rows)
            {
                TList.Add(new VaccineModel
                {
                    CVX_Code = Convert.ToInt64(row["CVX_Code"]),
                    Description = Convert.ToString(row["Description"]),

                });
            }
            return TList;
        }

        public static List<SecurityQuestionModel> ToSecurityQuestionModelList(this System.Data.DataTable table)
        {
            List<SecurityQuestionModel> TList = new List<SecurityQuestionModel>();
            foreach (System.Data.DataRow row in table.Rows)
            {
                TList.Add(new SecurityQuestionModel
                {
                    SecurityQuestionId = Convert.ToInt64(row["SecurityQuestionId"]),
                    SecurityAnswer = Convert.ToString(row["Value"]),

                });
            }
            return TList;
        }

        public static IEnumerable<SelectListItem> ToSecurityQuestionModelSelectList(this  List<SecurityQuestionModel> collection)
        {
            List<SelectListItem> TList = collection.Select(x => new SelectListItem { Text = x.SecurityAnswer, Value = x.SecurityQuestionId.ToString() }).ToList();
            return TList;
        }

        public static List<PreferredLanguageModel> ToPreferredLanguageModelList(this System.Data.DataTable table)
        {
            List<PreferredLanguageModel> TList = new List<PreferredLanguageModel>();
            foreach (System.Data.DataRow row in table.Rows)
            {
                TList.Add(new PreferredLanguageModel
                {
                    PreferredLanguageId = Convert.ToInt64(row["PreferredLanguageId"]),
                    Value = Convert.ToString(row["Value"]),
                });
            }
            return TList;
        }

        
        public static IEnumerable<SelectListItem> ToPreferredLanguageModelSelectList(this List<PreferredLanguageModel> collection)
        {
            List<SelectListItem> TList = collection.Select(x => new SelectListItem { Text = x.Value, Value = x.PreferredLanguageId.ToString() }).ToList();
            return TList;
        }

        
        public static List<EthnicityModel> ToEthnicityModelList(this System.Data.DataTable table)
        {
            List<EthnicityModel> TList = new List<EthnicityModel>();
            foreach (System.Data.DataRow row in table.Rows)
            {
                TList.Add(new EthnicityModel
                {
                    EthnicityId = Convert.ToInt64(row["EthnicityId"]),
                    Value = Convert.ToString(row["Value"]),
                });
            }
            return TList;
        }

        
        public static IEnumerable<SelectListItem> ToEthnicityModelSelectList(this List<EthnicityModel> collection)
        {
            List<SelectListItem> TList = collection.Select(x => new SelectListItem { Text = x.Value, Value = x.EthnicityId.ToString() }).ToList();
            return TList;
        }

        
        public static List<CountryModel> ToCountryModelList(this System.Data.DataTable table)
        {
            List<CountryModel> TList = new List<CountryModel>();
            foreach (System.Data.DataRow row in table.Rows)
            {
                TList.Add(new CountryModel
                {
                    CountryId = Convert.ToString(row["CountryId"]),
                    Name = Convert.ToString(row["Name"]),
                });
            }
            return TList;
        }

        
        public static IEnumerable<SelectListItem> ToCountryModelSelectList(this List<CountryModel> collection)
        {
            List<SelectListItem> TList = collection.Select(x => new SelectListItem { Text = x.Name, Value = x.CountryId.ToString() }).ToList();
            return TList;
        }

        
        public static List<StatesModel> ToStatesModelList(this System.Data.DataTable table)
        {
            List<StatesModel> TList = new List<StatesModel>();
            foreach (System.Data.DataRow row in table.Rows)
            {
                TList.Add(new StatesModel
                {
                    StateId = Convert.ToString(row["StateId"]),
                    Name = Convert.ToString(row["Name"]),
                });
            }
            return TList;
        }
        
        public static IEnumerable<SelectListItem> ToStatesModelSelectList(this List<StatesModel> collection)
        {
            List<SelectListItem> TList = collection.Select(x => new SelectListItem { Text = x.Name, Value = x.StateId.ToString() }).ToList();
            return TList;
        }

        public static List<ExerciseFrequencyModel> ToExerciseFrequencyModelList(this System.Data.DataTable table)
        {

            List<ExerciseFrequencyModel> TList = new List<ExerciseFrequencyModel>();
            foreach (System.Data.DataRow row in table.Rows)
            {
                TList.Add(new ExerciseFrequencyModel
                {
                    ExerciseFrequencyId = Convert.ToInt64(row["ExerciseFrequencyId"]),
                    Value = Convert.ToString(row["Value"]),
                });
            }
            return TList;
        
        }

        public static IEnumerable<SelectListItem> ToExerciseFrequencyModelSelectList(this List<ExerciseFrequencyModel> collection)
        {
            List<SelectListItem> TList = collection.Select(x => new SelectListItem { Text = x.Value, Value = x.ExerciseFrequencyId.ToString() }).ToList();
            return TList;
        }

        public static List<ActivityLevelModel> ToActivityLevelModelList(this System.Data.DataTable table)
        {

            List<ActivityLevelModel> TList = new List<ActivityLevelModel>();
            foreach (System.Data.DataRow row in table.Rows)
            {
                TList.Add(new ActivityLevelModel
                {
                    ActivityLevelId = Convert.ToInt64(row["ActivityLevelId"]),
                    Value = Convert.ToString(row["Value"]),
                });
            }
            return TList;

        }
        public static IEnumerable<SelectListItem> ToActivityLevelModelSelectList(this List<ActivityLevelModel> collection)
        {
            List<SelectListItem> TList = collection.Select(x => new SelectListItem { Text = x.Value, Value = x.ActivityLevelId.ToString() }).ToList();
            return TList;
        }


        public static List<AlcoholFrequencyModel> ToAlcoholFrequencyModelList(this System.Data.DataTable table)
        {

            List<AlcoholFrequencyModel> TList = new List<AlcoholFrequencyModel>();
            foreach (System.Data.DataRow row in table.Rows)
            {
                TList.Add(new AlcoholFrequencyModel
                {
                    AlcoholFrequencyId = Convert.ToInt64(row["AlcoholFrequencyId"]),
                    Value = Convert.ToString(row["Value"]),
                });
            }
            return TList;

        }

        public static IEnumerable<SelectListItem> ToAlcoholFrequencyModelSelectList(this List<AlcoholFrequencyModel> collection)
        {
            List<SelectListItem> TList = collection.Select(x => new SelectListItem { Text = x.Value, Value = x.AlcoholFrequencyId.ToString() }).ToList();
            return TList;
        }
        public static List<EducationLevelModel> ToEducationLevelModelList(this System.Data.DataTable table)
        {

            List<EducationLevelModel> TList = new List<EducationLevelModel>();
            foreach (System.Data.DataRow row in table.Rows)
            {
                TList.Add(new EducationLevelModel
                {
                    EducationLevelId = Convert.ToInt64(row["EducationLevelId"]),
                    Value = Convert.ToString(row["Value"]),
                });
            }
            return TList;

        }
        public static IEnumerable<SelectListItem> ToEducationLevelModelSelectList(this List<EducationLevelModel> collection)
        {
            List<SelectListItem> TList = collection.Select(x => new SelectListItem { Text = x.Value, Value = x.EducationLevelId.ToString() }).ToList();
            return TList;
        }

        public static List<MartialStatusModel> ToMartialStatusModelList(this System.Data.DataTable table)
        {

            List<MartialStatusModel> TList = new List<MartialStatusModel>();
            foreach (System.Data.DataRow row in table.Rows)
            {
                TList.Add(new MartialStatusModel
                {
                    MaritalStatusId = Convert.ToInt64(row["MaritalStatusId"]),
                    Value = Convert.ToString(row["Value"]),
                });
            }
            return TList;

        }
        public static IEnumerable<SelectListItem> ToMartialStatusModelSelectList(this List<MartialStatusModel> collection)
        {
            List<SelectListItem> TList = collection.Select(x => new SelectListItem { Text = x.Value, Value = x.MaritalStatusId.ToString() }).ToList();
            return TList;
        }

        public static List<RoutesOfAdministrationModel> ToRoutesOfAdministrationModelList(this System.Data.DataTable table)
        {

            List<RoutesOfAdministrationModel> TList = new List<RoutesOfAdministrationModel>();
            foreach (System.Data.DataRow row in table.Rows)
            {
                TList.Add(new RoutesOfAdministrationModel
                {
                    RouteId = Convert.ToString(row["RouteId"]),
                    Value = Convert.ToString(row["Value"]),
                });
            }
            return TList;

        }
        public static IEnumerable<SelectListItem> ToRoutesOfAdministrationModelSelectList(this List<RoutesOfAdministrationModel> collection)
        {
            List<SelectListItem> TList = collection.Select(x => new SelectListItem { Text = x.Value, Value = x.RouteId }).ToList();
            return TList;
        }


        public static List<BloodTypeModel> ToBloodTypeModelList(this System.Data.DataTable table)
        {
            List<BloodTypeModel> TList = new List<BloodTypeModel>();
            foreach (System.Data.DataRow row in table.Rows)
            {
                TList.Add(new BloodTypeModel
                {
                    BloodTypeId = Convert.ToInt32(row["BloodTypeId"]),
                    Value = Convert.ToString(row["Value"]),
                });
            }
            return TList;
        }

        [DebuggerStepThrough]
        public static IEnumerable<SelectListItem> ToBloodTypeModelSelectList(this List<BloodTypeModel> collection)
        {
            List<SelectListItem> TList = collection.Select(x => new SelectListItem { Text = x.Value, Value = x.BloodTypeId.ToString() }).ToList();
            return TList;
        }

        [DebuggerStepThrough]
        public static List<ReligionModel> ToReligionModelList(this System.Data.DataTable table)
        {
            List<ReligionModel> TList = new List<ReligionModel>();
            foreach (System.Data.DataRow row in table.Rows)
            {
                TList.Add(new ReligionModel
                {
                    ReligionId = Convert.ToInt32(row["ReligionId"]),
                    Value = Convert.ToString(row["Value"]),
                });
            }
            return TList;
        }

        [DebuggerStepThrough]
        public static List<InstructionType> ToInstructionTypeModelList(this System.Data.DataTable table)
        {
            List<InstructionType> TList = new List<InstructionType>();
            foreach (System.Data.DataRow row in table.Rows)
            {
                TList.Add(new InstructionType
                {
                    InstructionTypeId = Convert.ToInt32(row["InstructionTypeId"]),
                    Value = Convert.ToString(row["Value"]),
                });
            }
            return TList;
        }
        [DebuggerStepThrough]
        public static IEnumerable<SelectListItem> ToInstructionTypeModelSelectList(this List<InstructionType> collection)
        {
            List<SelectListItem> TList = collection.Select(x => new SelectListItem { Text = x.Value, Value = x.InstructionTypeId.ToString() }).ToList();
            return TList;
        }
        [DebuggerStepThrough]
        public static IEnumerable<SelectListItem> ToReligionModelSelectList(this List<ReligionModel> collection)
        {
            List<SelectListItem> TList = collection.Select(x => new SelectListItem { Text = x.Value, Value = x.ReligionId.ToString() }).ToList();
            return TList;
        }

        [DebuggerStepThrough]
        public static List<RaceModel> ToRaceModelList(this System.Data.DataTable table)
        {
            List<RaceModel> TList = new List<RaceModel>();
            foreach (System.Data.DataRow row in table.Rows)
            {
                TList.Add(new RaceModel
                {
                    RaceId = Convert.ToInt64(row["RaceId"]),
                    Value = Convert.ToString(row["Value"]),
                });
            }
            return TList;
        }
        
        public static IEnumerable<SelectListItem> ToRaceModelSelectList(this List<RaceModel> collection)
        {
            List<SelectListItem> TList = collection.Select(x => new SelectListItem { Text = x.Value, Value = x.RaceId.ToString() }).ToList();
            return TList;
        }
        
        public static IEnumerable<SelectListItem> ToRelationshipModelSelectList(this List<RelationshipModel> collection)
        {
            List<SelectListItem> TList = collection.Select(x => new SelectListItem { Text = x.Value , Value = x.RelationShipID.ToString() + "|" + x.SNOMED.ToString() }).ToList();
            return TList;
        }

        public static IEnumerable<SelectListItem> ToRelationshipModelOnlyIDSelectList(this List<RelationshipModel> collection)
        {
            List<SelectListItem> TList = collection.Select(x => new SelectListItem { Text = x.Value, Value = x.RelationShipID.ToString()  }).ToList();
            return TList;
        }

        public static IEnumerable<SelectListItem> ToVaccineModelSelectList(this List<VaccineModel> collection)
        {
            List<SelectListItem> TList = collection.Select(x => new SelectListItem { Text = x.Description, Value = x.CVX_Code.ToString() }).ToList();
            return TList;
        }

        public static List<SmokingStatusModel> ToSmokingStatusModelList(this System.Data.DataTable table)
        {
            List<SmokingStatusModel> TList = new List<SmokingStatusModel>();
            foreach (System.Data.DataRow row in table.Rows)
            {
                TList.Add(new SmokingStatusModel
                {
                    SmokingStatusId = Convert.ToInt64(row["SmokingStatusId"]),
                    Value = Convert.ToString(row["Value"]),
                });
            }
            return TList;
        }
        
        public static IEnumerable<SelectListItem> ToSmokingStatusModelSelectList(this List<SmokingStatusModel> collection)
        {
            List<SelectListItem> TList = collection.Select(x => new SelectListItem { Text = x.Value, Value = x.SmokingStatusId.ToString() }).ToList();
            return TList;
        }



        // Added for Thirdparty list 07/08/2014
        public static List<ThirdPartyModel> ToThirdPartyModelList(this System.Data.DataTable table)
        {
            List<ThirdPartyModel> TList = new List<ThirdPartyModel>();
            foreach (System.Data.DataRow row in table.Rows)
            {
                TList.Add(new ThirdPartyModel
                {
                    ThirdPartyId = Convert.ToInt64(row["ThirdPartyId"]),
                    Value = Convert.ToString(row["Value"]),
                });
            }
            return TList;
        }

        public static List<PartnerModelForAdmin> ToPartnerModelList(this System.Data.DataTable table)
        {
            List<PartnerModelForAdmin> TList = new List<PartnerModelForAdmin>();
            foreach (System.Data.DataRow row in table.Rows)
            {
                TList.Add(new PartnerModelForAdmin
                {
                    EMRSystemId = Convert.ToInt64(row["EMRSystemId"]),
                    Value = Convert.ToString(row["Value"]),
                });
            }
            return TList;
        }


        //public static List<Organization> ToOrganizationModelList(this System.Data.DataTable table)
        //{
        //    List<Organization> TList = new List<Organization>();
        //    foreach (System.Data.DataRow row in table.Rows)
        //    {
        //        TList.Add(new Organization
        //        {
        //            OrganizationId = Convert.ToInt64(row["OrganizationId"]),
        //            OrganizationName = Convert.ToString(row["OrganizationName"]),
        //        });
        //    }
        //    return TList;
        //}

        public static List<PatientNotesModel> ToPatientNotesModelList(this System.Data.DataTable table)
        {
            List<PatientNotesModel> TList = new List<PatientNotesModel>();
            foreach (System.Data.DataRow row in table.Rows)
            {
                TList.Add(new PatientNotesModel
                {
                    PatientId = Convert.ToInt64(row["PatientId"]),
                    DateCreated = Convert.ToDateTime(row["DateCreated"]),
                    Note = Convert.ToString(row["Note"])
                });
            }
            return TList;
        }


        public static List<OrangizationModel> ToFacilityOranganization(this System.Data.DataTable table)
        {
            List<OrangizationModel> TList = new List<OrangizationModel>();
            foreach (System.Data.DataRow row in table.Rows)
            {
                TList.Add(new OrangizationModel
                {
                    OrganizationId = Convert.ToInt64(row["OrganizationId"]),
                    OrganizationName = Convert.ToString(row["OrganizationName"]),
                    Address1 = Convert.ToString(row["Address1"]),
                    Address2 = Convert.ToString(row["Address2"]),
                    Address3=Convert.ToString(row["Address3"]),
                    City = Convert.ToString(row["City"]),
                    State = Convert.ToString(row["State"]),
                    PostalCode = Convert.ToString(row["PostalCode"]),
                    CountryCode = Convert.ToString(row["CountryCode"]),
                });
            }
            return TList;
        }

        public static List<FacilityPracticeModel> ToFacilityPractice(this System.Data.DataTable table)
        {
            List<FacilityPracticeModel> TList = new List<FacilityPracticeModel>();
            foreach (System.Data.DataRow row in table.Rows)
            {
                TList.Add(new FacilityPracticeModel
                {
                    OrganizationId = Convert.ToInt64(row["OrganizationId"]),
                    PracticeName = Convert.ToString(row["PracticeName"]),
                    PracticeId = Convert.ToInt64(row["PracticeId"]),
                    Address1 = Convert.ToString(row["Address1"]),
                    Address2 = Convert.ToString(row["Address2"]),
                    Address3 = Convert.ToString(row["Address3"]),
                    City = Convert.ToString(row["City"]),
                    State = Convert.ToString(row["State"]),
                    PostalCode = Convert.ToString(row["PostalCode"]),
                    CountryCode = Convert.ToString(row["CountryCode"]),
                    OrganizationName = Convert.ToString(row["OrganizationName"]),
                });
            }
            return TList;
        }


        public static List<FacilitySetupPracticeModel> ToFacilitySetupPractice(this System.Data.DataTable table)
        {
            List<FacilitySetupPracticeModel> TList = new List<FacilitySetupPracticeModel>();
            foreach (System.Data.DataRow row in table.Rows)
            {
                TList.Add(new FacilitySetupPracticeModel
                {
                 //   PracticeId = Convert.ToInt64(row["PracticeId"]),
                    FacilityId = Convert.ToInt64(row["FacilityId"]),
                    Address1 = Convert.ToString(row["Address1"]),
                    Address2 = Convert.ToString(row["Address2"]),
                    Address3 = Convert.ToString(row["Address3"]),
                    City = Convert.ToString(row["City"]),
                    State = Convert.ToString(row["State"]),
                    PostalCode = Convert.ToString(row["PostalCode"]),
                    CountryCode = Convert.ToString(row["CountryCode"]),
                    FacilityName = Convert.ToString(row["FacilityName"]),
                    PracticeName = Convert.ToString(row["PracticeName"]),
                    Comment = Convert.ToString(row["Comment"]),
                });
            }
            return TList;
        }
            public static List<PracticeDataModel> ToPracticeDataModel(this System.Data.DataTable table)
        {
            List<PracticeDataModel> TList = new List<PracticeDataModel>();
            foreach (System.Data.DataRow row in table.Rows)
            {
                TList.Add(new PracticeDataModel
                {
                    PracticeId = Convert.ToInt64(row["PatientId"]),
                    OrganizationId = Convert.ToInt64(row["OrganizationId"]),
                    PracticeName = Convert.ToString(row["PracticeName"]),
                    Address1= Convert.ToString(row["Address1"]),
                     Address2= Convert.ToString(row["Address2"]),
                      Address3= Convert.ToString(row["Address3"]),
                      City =Convert.ToString(row["City"]),
                       State =Convert.ToString(row["State"]),
                        PostalCode =Convert.ToString(row["PostalCode"]),
                         CountryCode =Convert.ToString(row["CountryCode"])

                });
            }
            return TList;
        }
      


        public static IEnumerable<SelectListItem> ToThirdPartyModelSelectList(this  List<ThirdPartyModel> collection)
        {
            List<SelectListItem> TList = collection.Select(x => new SelectListItem { Text = x.Value, Value = x.Value.ToString() }).ToList();
            return TList;
        }

        public static IEnumerable<SelectListItem> ToPartnerModelSelectList(this  List<PartnerModelForAdmin> collection)
        {
            List<SelectListItem> TList = collection.Select(x => new SelectListItem { Text = x.Value, Value = x.EMRSystemId.ToString() }).ToList();
            return TList;
        }

        //public static IEnumerable<SelectListItem> ToOrganizationModelSelectList(this  List<Organization> collection)
        //{
        //    List<SelectListItem> TList = collection.Select(x => new SelectListItem { Text = x.OrganizationName, Value = x.OrganizationId.ToString() }).ToList();
        //    return TList;
        //}
        
        public static List<ConditionStatusModel> ToConditionStatusModelList(this System.Data.DataTable table)
        {
            List<ConditionStatusModel> TList = new List<ConditionStatusModel>();
            foreach (System.Data.DataRow row in table.Rows)
            {
                TList.Add(new ConditionStatusModel
                {
                    ConditionStatusId = Convert.ToInt32(row["ConditionStatusId"]),
                    Value = Convert.ToString(row["Value"]),
                });
            }
            return TList;
        }

        
        public static IEnumerable<SelectListItem> ToConditionStatusModelSelectList(this List<ConditionStatusModel> collection)
        {
            List<SelectListItem> TList = collection.Select(x => new SelectListItem { Text = x.Value, Value = x.ConditionStatusId.ToString() }).ToList();
            return TList;
        }

        public static List<PatientEmergencyModel> ToPatientEmergencyModelList(this DataTable dt)
        {
            if (dt != null)
            {
                return dt.AsEnumerable().Select(dr => new PatientEmergencyModel {

                    //PatientId          = long.Parse(dr["PatientId"].ToString()),
                    PatientEmergencyId = long.Parse(dr["PatientEmergencyId"].ToString()),
                    /*
                    Title              = dr["Title"].ToString(),
                    FirstName          = dr["FirstName"].ToString(),
                    MiddleName         = dr["MiddleName"].ToString(),
                    LastName           = dr["LastName"].ToString(),
                    */

                    FirstName          = dr["Name"].ToString(),

                    Address1           = dr["Address1"].ToString(),
                    Address2           = dr["Address2"].ToString(),
                    Address3           = dr["Address3"].ToString(),

                    Email              = dr["Email"].ToString(),

                    HomePhone          = dr["HomePhone"].ToString(),
                    WorkPhone          = dr["WorkPhone"].ToString(),
                    MobilePhone        = dr["MobilePhone"].ToString(),

                    City               = dr["City"].ToString(),
                    State              = dr["State"].ToString(),
                    PostalCode         = dr["PostalCode"].ToString(),
                    CountryCode        = dr["CountryCode"].ToString(),
                    //CountryName        = dr["CountryName"].ToString(),

                    IsPrimary          = bool.Parse(dr["IsPrimary"].ToString()),
                    OnCard             = bool.Parse(dr["OnCard"].ToString()),

                    RelationshipId     = long.Parse(dr["RelationshipId"].ToString())
                    
                }).ToList();

            }
            else
                return null;

        }

        public static List<PatientDoctorModel> ToPatientDoctorModelList(this DataTable dt)
        {
            if (dt != null )
            {
                return dt.AsEnumerable().Select(dr => new PatientDoctorModel
                {
                    PatientDoctorId = long.Parse(dr["PatientDoctorId"].ToString()),

                    DoctorTypeId = int.Parse(dr["DoctorTypeId"].ToString()),
                    DoctorTypeDesc = dr["DoctorTypeDesc"].ToString(),

                    Name = dr["Name"].ToString(),

                    Address1 = dr["Address1"].ToString(),
                    Address2 = dr["Address2"].ToString(),
                    Address3 = dr["Address3"].ToString(),

                    WorkPhone = dr["WorkPhone"].ToString(),     //office phone
                    MobilePhone = dr["MobilePhone"].ToString(), //Cell phone

                    City = dr["City"].ToString(),
                    State = dr["State"].ToString(),
                    PostalCode = dr["PostalCode"].ToString(),
                    CountryCode = dr["CountryCode"].ToString(),

                    Fax = dr["Fax"].ToString(),
                    Email = dr["Email"].ToString(),

                    IsPrimary = bool.Parse(dr["IsPrimary"].ToString()),
                    OnCard = bool.Parse(dr["OnCard"].ToString())

                }).ToList();

            }
            else
                return null;

        }

        public static List<DoctorSpecialityModel> ToDoctorSpecialityModelList(this DataTable dt)
        {
            if (dt != null )
            {
                return dt.AsEnumerable().Select(dr => new DoctorSpecialityModel
                {
                    DoctorSpecialityId = long.Parse(dr["DoctorTypeId"].ToString()),
                    Value              = dr["Value"].ToString()
                }).ToList();

            }
            else
                return null;
        }

        public static IEnumerable<SelectListItem> ToDoctorSpecialityModelSelectList(this List<DoctorSpecialityModel> collection)
        {
            List<SelectListItem> TList = collection.Select(x => new SelectListItem { Text = x.Value, Value = x.DoctorSpecialityId.ToString() }).ToList();
            
            return TList;
        }


        #region Medical Portfolio List Helpers...

        public static List<PatientVisitModel> ToPatientVisitModelList(this DataTable dt) 
        {
            if (dt != null )
            {
                return dt.AsEnumerable().Select(dr => new PatientVisitModel
                {
                    
                    PatientId = long.Parse(dr["PatientId"].ToString()),
                    FacilityId = long.Parse(dr["FacilityId"].ToString()),
                    FacilityName = dr["FacilityName"].ToString(),
                    VisitId = long.Parse(dr["VisitId"].ToString()),
                    VisitDate = DateTime.Parse(dr["VisitDate"].ToString()),
                    ProviderId = long.Parse(dr["ProviderId"].ToString()),
                    ProviderName = dr["ProviderName"].ToString(),
                    VisitReason = dr["VisitReason"].ToString(),
                    Viewable = bool.Parse(dr["Viewable"].ToString()),

                }).ToList();

            }
            else
                return null;

        }

        public static List<PatientOutsideDoctorModel> ToOutsideDoctorModelList(this DataTable dt)
        {
            if (dt != null)
            {
                return dt.AsEnumerable().Select(dr => new PatientOutsideDoctorModel
                {
                    DocumentCntr = long.Parse(dr["DocumentCntr"].ToString()),
                    DoctorName = dr["DoctorName"].ToString(),
                    DateCreated = DateTime.Parse(dr["DateCreated"].ToString()),
                    DocumentDescription = dr["DocumentDescription"].ToString(),
                    Notes = dr["Notes"].ToString(),
                    Viewable = bool.Parse(dr["Viewable"].ToString()),
                    DocumentFormat = dr["DocumentFormat"].ToString()
                }).ToList();
            }
            else
                return null;

        }

        public static List<PatientMedicalDocumentModel> ToPatientDocumentModelList(this DataTable dt)
        {
            if (dt != null )
            {
                return dt.AsEnumerable().Select(dr => new PatientMedicalDocumentModel
                {
                    //PatientId = long.Parse(dr["PatientId"].ToString()),
                    DocumentCntr = long.Parse(dr["DocumentCntr"].ToString()),
                    DoctorName = dr["DoctorName"].ToString(),
                    FacilityName = dr["FacilityName"].ToString(),
                    DateCreated = DateTime.Parse(dr["DateCreated"].ToString()),
                    DocumentDescription = dr["DocumentDescription"].ToString(),
                    Notes = dr["Notes"].ToString(),
                    Viewable = bool.Parse(dr["Viewable"].ToString()),
                    DocumentFormat = dr["DocumentFormat"].ToString()

                }).ToList();
            }
            else
                return null;

        }    
        #endregion 

        #region General Document List Helpers...
        public static List<GeneralDocumentModel> ToGeneralDocumentModelList(this DataTable dt)
        {
            if (dt != null )
            {
                return dt.AsEnumerable().Select(dr => new GeneralDocumentModel
                {
                    DocumentCntr = long.Parse(dr["DocumentCntr"].ToString()),
                    DocumentDescription = dr["DocumentDescription"].ToString(),
                    DateCreated = DateTime.Parse(dr["DateCreated"].ToString()),
                    Notes = dr["Notes"].ToString(),
                    Viewable = bool.Parse(dr["Viewable"].ToString()),
                    DocumentFormat = dr["DocumentFormat"].ToString()

                }).ToList();
            }
            else
                return null;

        }    
        #endregion

        #region Insurance Policy List Helpers...
        public static List<InsurancePolicyModel> ToInsurancePolicyModelList(this DataTable dt)
        {
            if (dt != null)
            {
                return dt.AsEnumerable().Select(dr => new InsurancePolicyModel
                {
                    
                    
                    PatientPolicyId = long.Parse(dr["PatientPolicyId"].ToString()),
                    PolicyTypeId =int.Parse(dr["PolicyTypeId"].ToString()),
                    Value        = dr["Value"].ToString(),
                    PolicyTypeName =dr["PolicyTypeName"].ToString(),
                    Company = dr["Company"].ToString(),
                    PolicyNo = dr["PolicyNo"].ToString(),
                    GroupNumber = dr["GroupNumber"].ToString(),
                    PlanNumber = dr["PlanNumber"].ToString(),
                    Agent = dr["Agent"].ToString(),
                    AgentPhone = dr["AgentPhone"].ToString(),
                    AgentFax = dr["AgentFax"].ToString(),
                    Notes = dr["Notes"].ToString() 
                }).ToList();
            }
            else
                return null;

        }
        public static List<PolicyTypeModel> ToPolicyTypeModelList(this System.Data.DataTable table)
        {
            List<PolicyTypeModel> TList = new List<PolicyTypeModel>();
            foreach (System.Data.DataRow row in table.Rows)
            {
                TList.Add(new PolicyTypeModel
                {
                    PolicyTypeId = short.Parse(row["PolicyTypeId"].ToString()),
                    Value = Convert.ToString(row["Value"]),
                });
            }
            return TList;
        }
        public static IEnumerable<SelectListItem> ToPolicyTypeModelSelectList(this List<PolicyTypeModel> collection)
        {
            List<SelectListItem> TList = collection.Select(x => new SelectListItem { Text = x.Value, Value = x.PolicyTypeId.ToString() }).ToList();

            return TList;
        } 
        #endregion

        #region Professional Advisor List Helpers...
        public static List<ProfessionalAdvisorModel> ToProfessionalAdvisorModelList(this DataTable dt)
        {
            
            

            if (dt != null )
            {
                return dt.AsEnumerable().Select(dr => new ProfessionalAdvisorModel
                {

                    PatientAdvisorId = long.Parse(dr["PatientAdvisorId"].ToString()),
                    AdvisorId = int.Parse(dr["AdvisorId"].ToString()),
                    Value  = dr["Value"].ToString(),
                    AdvisorDesc = dr["AdvisorDesc"].ToString(),
                    Name = dr["Name"].ToString(),
                    ContactAgent = dr["ContactAgent"].ToString(),
                    Address1 = dr["Address1"].ToString(),
                    Address2 = dr["Address2"].ToString(),
                    Address3 = dr["Address3"].ToString(),
                    City = dr["City"].ToString(),
                    State = dr["State"].ToString(),
                    PostalCode = dr["PostalCode"].ToString(),
                    CountryCode = dr["CountryCode"].ToString(),
                    WorkPhone = dr["WorkPhone"].ToString(),
                    MobilePhone = dr["MobilePhone"].ToString(),
                    Fax = dr["Fax"].ToString(),
                    EMail = dr["EMail"].ToString(),
                    Notes = dr["Notes"].ToString() 
                }).ToList();
            }
            else
                return null;
        }
        public static List<AdvisorTypeModel> ToAdvisorTypeModelList(this System.Data.DataTable table)
        {
            List<AdvisorTypeModel> TList = new List<AdvisorTypeModel>();
            foreach (System.Data.DataRow row in table.Rows)
            {
                TList.Add(new AdvisorTypeModel
                {
                    AdvisorTypeId = short.Parse(row["AdvisorId"].ToString()),
                    Value = Convert.ToString(row["Value"]),
                });
            }
            return TList;
        }
        public static IEnumerable<SelectListItem> ToAdvisorTypeModelSelectList(this List<AdvisorTypeModel> collection)
        {
            List<SelectListItem> TList = collection.Select(x => new SelectListItem { Text = x.Value, Value = x.AdvisorTypeId.ToString() }).ToList();

            return TList;
        } 
        #endregion
    }
}
