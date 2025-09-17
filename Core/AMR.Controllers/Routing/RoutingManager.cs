using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AMR.Controllers.Routing
{
    public static class RoutingManager
    {
         public static void RegisterDefaultRoutesPatients(RouteCollection routes)
         {
             routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
             routes.IgnoreRoute("{resource}.aspx/{*pathInfo}");
             routes.IgnoreRoute("{resource}.ico");
           //  routes.MapRoute("AllergyWidget", "AllergyWidget", new { controller = "Home", action = "AllergyWidget" });
             routes.MapRoute("Share-Hide-Appointments", "Share-Hide-Appointments", new { controller = "Home", action = "AppointmentShareHide" });
             routes.MapRoute("Share-Hide-SocialHistory", "Share-Hide-SocialHistory", new { controller = "Home", action = "SocialHistoryShareHide" });
             routes.MapRoute("Share-Hide-FamilyHistory", "Share-Hide-FamilyHistory", new { controller = "Home", action = "FamilyHistoryShareHide" });
             routes.MapRoute("Share-Hide-Visits", "Share-Hide-Visits", new { controller = "Home", action = "VisitsShareHide" });
             routes.MapRoute("Share-Hide-Medication", "Share-Hide-Medication", new { controller = "Home", action = "MedicationShareHide" });
             routes.MapRoute("Share-Hide-PastMedicalHistory", "Share-Hide-PastMedicalHistory", new { controller = "Home", action = "PastMedicalHistoryShareHide" });
             routes.MapRoute("Share-Hide-Problems", "Share-Hide-Problems", new { controller = "Home", action = "ProblemsShareHide" });
             routes.MapRoute("Share-Hide-Insurance", "Share-Hide-Insurance", new { controller = "Home", action = "InsuranceShareHide" });
             routes.MapRoute("Share-Hide-Immunization", "Share-Hide-Immunization", new { controller = "Home", action = "ImmunizationShareHide" });
             routes.MapRoute("Share-Hide-Vitals", "Share-Hide-Vitals", new { controller = "Home", action = "VitalsShareHide" });
             routes.MapRoute("Share-Hide-Documents", "Share-Hide-Documents", new { controller = "Home", action = "DocumentsShareHide" });
             routes.MapRoute("Share-Hide-Allergies", "Share-Hide-Allergies", new { controller = "Home", action = "AllergiesShareHide" });
             routes.MapRoute("Share-Hide-POC", "Share-Hide-POC", new { controller = "Home", action = "POCShareHide" });
             routes.MapRoute("Share-Hide-Procedures", "Share-Hide-Procedures", new { controller = "Home", action = "ProceduresShareHide" });
             routes.MapRoute("index-WidgetId-Delete", "index-WidgetId-Delete", new { controller = "Home", action = "WidgetIdDelete" });

             
            
             routes.MapRoute("home-security-save", "home-security-save", new { controller = "Home", action = "SecuritySave" });
             routes.MapRoute("appointment-index", "appointments-index", new { controller = "Message", action = "AppointmentIndex" });

             routes.MapRoute("referral-index", "referrals-index", new { controller = "Message", action = "ReferralIndex" });

              routes.MapRoute("account-index", "accounts-index", new { controller = "Account", action = "AccountIndex" });
              routes.MapRoute("ProblemsIndex", "ProblemsIndex", new { controller = "Home", action = "ProblemsIndex" });

              routes.MapRoute("account-password-change", "account-password-change", new { controller = "Account", action = "PasswordSave" });
             
             routes.MapRoute("Terms", "Terms", new { controller = "Home", action = "Terms" });

             routes.MapRoute("signup", "signup", new { controller = "Home", action = "SignUp" });
             routes.MapRoute("PaymentInfo", "PaymentInfo", new { controller = "Home", action = "PaymentInfo" });
             routes.MapRoute("Confirmation", "Confirmation", new { controller = "Home", action = "Confirmation" });
             routes.MapRoute("PaymentTransfer", "PaymentTransfer", new { controller = "Home", action = "PaymentTransfer" });
             routes.MapRoute("PremiumPay", "PremiumPay", new { controller = "Home", action = "PremiumPay" });

             

             routes.MapRoute("forgotpassword", "forgotpassword", new { controller = "Home", action = "ForgotPassword" });
             routes.MapRoute("SocialHistorySelf", "SocialHistorySelf", new { controller = "Home", action = "SocialHistorySelf" });
             routes.MapRoute("social-history-self-save", "social-history-self-save", new { controller = "Home", action = "SocialHistorySelfSave" });
             routes.MapRoute("Clincial-social-history-self-save", "Clincial-social-history-self-save", new { controller = "ClinicalSummary", action = "SocialHistorySelfSave" });

           
             
             routes.MapRoute("security-question", "security-question", new { controller = "Home", action = "SecurityQuestion" });
             routes.MapRoute("license-agreement", "license-agreement", new { controller = "Home", action = "LicenseAgreement" });
             routes.MapRoute("download-amrApp", "download-amrApp", new { controller = "Home", action = "DownloadAMRApp" });

             routes.MapRoute("reset-password", "reset-password", new { controller = "Home", action = "ResetPassword" });


           //  routes.MapRoute("preactive_pw_resend", "preactive_pw_resend", new { controller = "Home", action = "PreActivePassResend" });


             routes.MapRoute("preactive_pw_resend", "Home/ResetPassword", new { controller = "Home", action = "ResetPassword" });

             routes.MapRoute("patient-signup", "patient-signup", new { controller = "Home", action = "PatientRegister" });
             routes.MapRoute("AllergyWidget", "AllergyWidget", new { controller = "Home", action = "AllergyWidget" });

             

             routes.MapRoute("UpgradePremium", "UpgradePremium", new { controller = "Home", action = "UpgradePremium" });
             

             routes.MapRoute("Medical-Summary-Report-Login", "Medical-Summary-Report-Login", new { controller = "Premium", action = "MedicalSummaryReportLogin" });

             routes.MapRoute("Medical-Summary-Report-Index", "Medical-Summary-Report-Index", new { controller = "Premium", action = "MedicalSummaryReportIndex" });
             

             routes.MapRoute("Care-Provider-login", "Care-Provider-login", new { controller = "Premium", action = "CareProviderLogin" });

             routes.MapRoute("Care-Provider-logout", "Care-Provider-logout", new { controller = "Premium", action = "CareProviderLogOut" });

             routes.MapRoute("Care-Provider-Index", "Care-Provider-Index", new { controller = "Premium", action = "CareProviderIndex" });
             routes.MapRoute("login", "login", new { controller = "Home", action = "Login" });
             routes.MapRoute("another-session", "another-session", new { controller = "Home", action = "AnotherSession" });
             routes.MapRoute("EditPatientDataLogin", "EditPatientDataLogin", new { controller = "Home", action = "EditPatientDataLogin" });
             routes.MapRoute("Allergies", "Allergies", new { controller = "Home", action = "Allergies" });

             

             routes.MapRoute("logout", "logout", new { controller = "Home", action = "LogOff" });



             routes.MapRoute("CCDHtmlDownload", "CCDHtmlDownload", new { controller = "Home", action = "CCDHtmlDownload" });
             routes.MapRoute("CCD-View", "CCD-View", new { controller = "Home", action = "CCDView" });
             routes.MapRoute("CCDLocation", "CCDLocation", new { controller = "Home", action = "CCDLocation" });
             routes.MapRoute("CustomizeCCD", "CustomizeCCD", new { controller = "Home", action = "CustomizeCCD" });
            
             
             routes.MapRoute("index", "", new { controller = "Home", action = "Index"});
             routes.MapRoute("CCDLogin", "Accessibility", new { Controller = "Home", action = "CCDLogin" });
             routes.MapRoute("ccd", "ccd", new { controller = "Home", action = "CCD" });
             routes.MapRoute("Get-CCD-Data", "Get-CCD-Data", new { controller = "Home", action = "GetCCDData" });
             routes.MapRoute("Get-CCD-Data-XML", "Get-CCD-Data-XML", new { controller = "Home", action = "GetCCDDataXML" });
             routes.MapRoute("index-med-save-current", "medications-save", new { controller = "Home", action = "MedicationSave" });

            // routes.MapRoute("index-medicationlist-get", "index-medicationslist-get", new { controller = "Home", action = "GetMedicationList" });
             routes.MapRoute("index-WidgetId-Save", "index-WidgetId-Save", new { controller = "Home", action = "WidgetIdSave" });
             routes.MapRoute("index-vital-save", "index-vitals-save", new { controller = "Home", action = "VitalSave" });

             routes.MapRoute("index-family-save", "index-families-save", new { controller = "Home", action = "FamilySave" });

             routes.MapRoute("index-visit-save", "index-visits-save", new { controller = "Home", action = "VisitSave" });

             routes.MapRoute("index-social-save", "index-socials-save", new { controller = "Home", action = "SocialHistorySave" });

             routes.MapRoute("index-past-save", "index-pasts-save", new { controller = "Home", action = "PastMedicalSave" });

             routes.MapRoute("index-lab-save", "index-labs-save", new { controller = "Home", action = "LabTestSave" });

             routes.MapRoute("summary-labs-result", "summary-lab-result", new { controller = "ClinicalSummary", action = "LabResultDetails" });
             routes.MapRoute("index-patients-CCD", "index-patient-CCD", new { controller = "Home", action = "GetCCDRun" });
             routes.MapRoute("index-patients-CCD-Send", "index-patient-CCD-Send", new { controller = "Home", action = "SendCCD" });
             routes.MapRoute("index-immunization-save", "index-immunizations-save", new { controller = "Home", action = "ImmunizationSave" });

             routes.MapRoute("index-problem-save", "index-problems-save", new { controller = "Home", action = "ProblemSave" });

             routes.MapRoute("index-problemlist-get", "index-problemslist-get", new { controller = "Home", action = "GetProblemList" });

             routes.MapRoute("index-medicationlist-get", "index-medicationlists-get", new { controller = "Home", action = "GetMedicationList" });

             routes.MapRoute("index-immunizationlist-get", "index-immunizationlists-get", new { controller = "Home", action = "GetImmunizationList" });

             routes.MapRoute("index-Appointment-save", "index-Appointment-save", new { controller = "Home", action = "AppointmentSave" });
             routes.MapRoute("Appointment-delete", "Appointment-delete", new { controller = "Home", action = "AppointmentDelete" });

             
             routes.MapRoute("index-RequestReferral-save", "index-RequestReferral-save", new { controller = "Home", action = "RequestReferralSave" });

        

             routes.MapRoute("medication-print", "medications-print", new { controller = "Medication", action = "MedicationPrint", Status = UrlParameter.Optional});

             routes.MapRoute("medication-refill-save", "medications-refill-save", new { controller = "Medication", action = "MedicationRefillSave" });

             routes.MapRoute("Get-Medication-from-provider", "Get-Medication-from-provider", new { controller = "Home", action = "GetPatientMedicationList" });

             routes.MapRoute("medication-save-current", "medications-save-current", new { controller = "Medication", action = "MedicationSave" });

             routes.MapRoute("medication-delete-current", "medications-delete-current", new { controller = "Medication", action = "MedicationDelete" });

             routes.MapRoute("pharmacy-save", "pharmacies-save", new { controller = "Medication", action = "PharmacySave" });

             routes.MapRoute("pharmacy-delete", "pharmacies-delete", new { controller = "Medication", action = "PharmacyDelete" });

             routes.MapRoute("allergy-delete", "allergies-delete", new { controller = "ClinicalSummary", action = "AllergyDelete" });

             routes.MapRoute("socialhistory-delete", "socialhistories-delete", new { controller = "ClinicalSummary", action = "SocialDelete" });

             routes.MapRoute("familyhistory-delete", "familyhistories-delete", new { controller = "ClinicalSummary", action = "FamilyDelete" });

             routes.MapRoute("immunization-delete", "immunizations-delete", new { controller = "ClinicalSummary", action = "ImmunizationDelete" });

             routes.MapRoute("pastmedicalhistory-delete", "pastmedicalhistories-delete", new { controller = "ClinicalSummary", action = "PastMedicalDelete" });
             
             routes.MapRoute("clinical-summary-index", "clinical-summary", new { controller = "ClinicalSummary", action = "ClinicalSummaryIndex" });

             routes.MapRoute("Patient-Info-Index", "Patient-Info", new { controller = "ClinicalSummary", action = "PatientInfoIndex" });

             routes.MapRoute("clinical-summary-POC-save", "clinical-summary-POC-save", new { controller = "Home", action = "SavePOCData" });
             routes.MapRoute("clinical-summary-ClinicalInstruction-save", "clinical-summary-ClinicalInstruction-save", new { controller = "Home", action = "SaveClinicalInstructionData" });

             routes.MapRoute("Procedure-delete", "Procedure-delete", new { controller = "ClinicalSummary", action = "ProcedureDelete" });
             routes.MapRoute("POC-delete", "POC-delete", new { controller = "ClinicalSummary", action = "POCDelete" });
             routes.MapRoute("ClinicalInstruction-delete", "ClinicalInstruction-delete", new { controller = "ClinicalSummary", action = "ClinicalInstructionDelete" });

             
             
             routes.MapRoute("problem-delete", "problems-delete", new { controller = "ClinicalSummary", action = "ProblemDelete" });

             routes.MapRoute("vital-delete", "vitals-delete", new { controller = "ClinicalSummary", action = "vitalDelete" });

             routes.MapRoute("medication-index", "medication-index", new { controller = "Medication", action = "MedicationIndex" });

             routes.MapRoute("Get-Provider-Facility-Data", "Get-Provider-Facility-Data", new { controller = "Home", action = "GetProviderFacilityData" });

             routes.MapRoute("clinical-summary-print", "clinical-summarries-print", new { controller = "ClinicalSummary", action = "ClinicalSummaryPrint" });
             routes.MapRoute("summary-clinical", "summary-clinical", new { controller = "ClinicalSummary", action = "ClinicalSummary" });

             routes.MapRoute("clinical-summary-vitals-save", "clinical-summary-vitals-save", new { controller = "Home", action = "VitalSave" });

             routes.MapRoute("clinical-summary-lab-save", "clinical-summary-labs-save", new { controller = "Home", action = "LabTestSave" });
             
             routes.MapRoute("clinical-summary-social-save", "clinical-summary-socials-save", new { controller = "Home", action = "SocialHistorySave" });

             routes.MapRoute("clinical-summary-family-save", "clinical-summary-families-save", new { controller = "Home", action = "FamilySave" });

             routes.MapRoute("clinical-summary-past-save", "clinical-summary-pasts-save", new { controller = "Home", action = "PastMedicalSave" });

             routes.MapRoute("clinical-summary-immunization-save", "clinical-summary-immunizations-save", new { controller = "Home", action = "ImmunizationSave" });

             routes.MapRoute("clinical-summary-problem-save", "clinical-summary-problems-save", new { controller = "Home", action = "ProblemSave" });

             routes.MapRoute("clinical-summary-problemlist-get", "clinical-summary-problemslist-get", new { controller = "Home", action = "GetProblemList" });

             routes.MapRoute("clinical-summary-immunizationlist-get", "clinical-summary-immunizationlists-get", new { controller = "Home", action = "GetImmunizationList" });

             routes.MapRoute("clinical-summary-medication-save", "clinical-summary-medications-save", new { controller = "Home", action = "MedicationSave" });

             routes.MapRoute("clinical-summary-allergy-save", "clinical-summary-allergies-save", new { controller = "Home", action = "AllergiesSave" });

             routes.MapRoute("clinical-summary-patientSummary-save", "clinical-summary-patientSummary-save", new { controller = "Home", action = "PatientSummarySave" });
           
             routes.MapRoute("clinical-summary-patientEmergency-save", "clinical-summary-patientEmergency-save", new { controller = "Home", action = "PatientEmergencySave" });

             routes.MapRoute("clinical-summary-patientAndOrganData-save", "clinical-summary-patientAndOrganData-save", new { controller = "Home", action = "patientAndOrganDataSave" });
            
             routes.MapRoute("clinical-summary-immunization-filter", "clinical-summary-immunizations-filter", new { controller = "ClinicalSummary", action = "FilterImmunizationData" });

             routes.MapRoute("downloadhealthrecords-visit-dropdown", "downloadhealthrecords-visit-dropdown", new { controller = "Home", action = "FilterDownloadHealthRecord" });
             routes.MapRoute("sendhealthrecords-visit-dropdown", "sendhealthrecords-visit-dropdown", new { controller = "Home", action = "FilterSendHealthRecord" });
             
             routes.MapRoute("clinical-summary-allergies-widg-filter", "clinical-summary-allergies-widg-filter", new { controller = "Home", action = "FilterAllergyWidgData" });
             routes.MapRoute("clinical-summary-allergy-filter", "clinical-summary-allergies-filter", new { controller = "ClinicalSummary", action = "FilterAllergyData" });
             routes.MapRoute("clinical-summary-lab-filter", "clinical-summary-labs-filter", new { controller = "ClinicalSummary", action = "FilterLabData" });
             routes.MapRoute("clinical-summary-visit-filter", "clinical-summary-visits-filter", new { controller = "ClinicalSummary", action = "FilterVisitData" });
             routes.MapRoute("clinical-summary-problem-filter", "clinical-summary-problems-filter", new { controller = "ClinicalSummary", action = "FilterProblemData" });
             routes.MapRoute("clinical-summary-vital-filter", "clinical-summary-vitals-filter", new { controller = "ClinicalSummary", action = "FilterVitalData" });
             routes.MapRoute("clinical-summary-document-filter", "clinical-summary-documents-filter", new { controller = "ClinicalSummary", action = "FilterDocumentData" });

             routes.MapRoute("clinical-summary-lab-widg-filter", "clinical-summary-labs-widg-filter", new { controller = "Home", action = "FilterLabWidgData" });
             routes.MapRoute("clinical-summary-vital-widg-filter", "clinical-summary-vitals-widg-filter", new { controller = "Home", action = "FilterVitalWidgData" });
             routes.MapRoute("clinical-summary-immunization-widg-filter", "clinical-summary-immunizations-widg-filter", new { controller = "Home", action = "FilterImmunizationWidgData" });
             routes.MapRoute("clinical-summary-visit-widg-filter", "clinical-summary-visits-widg-filter", new { controller = "Home", action = "FilterVisitWidgData" });
             routes.MapRoute("clinical-summary-past-widg-filter", "clinical-summary-past-widg-filter", new { controller = "Home", action = "FilterPastWidgData" });
             routes.MapRoute("clinical-summary-family-widg-filter", "clinical-summary-family-widg-filter", new { controller = "Home", action = "FilterFamilyWidgData" });
             routes.MapRoute("clinical-summary-social-widg-filter", "clinical-summary-social-widg-filter", new { controller = "Home", action = "FilterSocialWidgData" });
             routes.MapRoute("clinical-summary-problem-widg-filter", "clinical-summary-problems-widg-filter", new { controller = "Home", action = "FilterProblemWidgData" });
             routes.MapRoute("clinical-summary-medications-widg-filterr", "clinical-summary-medications-widg-filter", new { controller = "Home", action = "FilterMedicationWidgData" });
             
             routes.MapRoute("Documents", "Documents", new { controller = "ClinicalSummary", action = "Documents" });
             
             routes.MapRoute("clinical-summary-social-filter", "clinical-summary-social-filter", new { controller = "ClinicalSummary", action = "FilterSocialData" });
             routes.MapRoute("clinical-summary-family-filter", "clinical-summary-family-filter", new { controller = "ClinicalSummary", action = "FilterFamilyData" });
             routes.MapRoute("clinical-summary-past-filter", "clinical-summary-past-filter", new { controller = "ClinicalSummary", action = "FilterPastData" });
             routes.MapRoute("clinical-summary-Procdure-filter", "clinical-summary-Procdure-filter", new { controller = "ClinicalSummary", action = "FilterProcedureData" });
             routes.MapRoute("clinical-summary-Insurance-filter", "clinical-summary-Insurance-filter", new { controller = "ClinicalSummary", action = "FilterInsuranceData" });
             routes.MapRoute("clinical-summary-ClinicalInstructions-filter", "clinical-summary-ClinicalInstructions-filter", new { controller = "ClinicalSummary", action = "FilterClinicalInstructionsData" });
             routes.MapRoute("clinical-summary-Procedures-save", "clinical-summary-Procedures-save", new { controller = "ClinicalSummary", action = "SaveProceduresData" });
             routes.MapRoute("clinical-summary-PlanOFCare-dropdown", "clinical-summary-PlanOFCare-dropdown", new { controller = "ClinicalSummary", action = "FilterPlanOFCaredropdownData" });
             routes.MapRoute("clinical-summary-Insurance-dropdown", "clinical-summary-Insurance-dropdown", new { controller = "ClinicalSummary", action = "FilterInsurancedropdownData" });

             
             routes.MapRoute("medication-current-medication-filter", "medication-current-medications-filter", new { controller = "Medication", action = "FilterCurrentMedicationData" });
             routes.MapRoute("medication-past-medication-filter", "medication-past-medications-filter", new { controller = "Medication", action = "FilterPastMedicationData" });
       
           routes.MapRoute("clinical-summary-pocs-widg-filter", "clinical-summary-poc-widg-filter", new { controller = "ClinicalSummary", action = "FilterPOCData" });
             routes.MapRoute("Medication-Data", "Medication-Data", new { controller = "Medication", action = "MedicationPharmaciesData" });

             routes.MapRoute("medications-show", "medications-show", new { controller = "Medication", action = "MedicationShow" });
          
             routes.MapRoute("master-image-save", "master-images-save", new { controller = "Home", action = "PatientImageSave" });
             routes.MapRoute("master-patient-get", "master-patient-get", new { controller = "Home", action = "GetPatientImg" });
             //routes.MapRoute("clinical-summary-visits-widg-filter", "clinical-summary-visits-widg-filter", new { controller = "Home", action = "FilterVisitWidgData" });

             routes.MapRoute("clinical-summary-Procedure-dropdown", "clinical-summary-Procedure-dropdown", new { controller = "ClinicalSummary", action = "FillProcedureDropDown" });
             routes.MapRoute("clinical-summary-PlanOfCare-filter", "clinical-summary-PlanOfCare-filter", new { controller = "ClinicalSummary", action = "FilterPlanOfCareData" });

             
             routes.MapRoute("clinical-summary-visit-dropdown", "clinical-summary-visit-dropdown", new { controller = "ClinicalSummary", action = "FillVisitDropDown" });
             routes.MapRoute("Get-Messages-Counter", "Get-Messages-Counter", new { controller = "Message", action = "GetMessageCounter" });
             routes.MapRoute("Update-Message-Read-Flag", "Update-Message-Read-Flag", new { controller = "Message", action = "UpdateMessageReadFlag" });
             routes.MapRoute("message-index", "message-center", new { controller = "Message", action = "MessageIndex" });
             routes.MapRoute("message-compose", "messages-compose", new { controller = "Message", action = "MessageCompose" });
             routes.MapRoute("message-reply", "messages-reply", new { controller = "Message", action = "MessageReply" });
             routes.MapRoute("message-refill-detail", "messages-refill-detail", new { controller = "Message", action = "MessageRefillDetails" });
             routes.MapRoute("message-send-data", "message-send-data", new { controller = "Message", action = "MessageSendData" });
             
             routes.MapRoute("message-inbox-GridDetail", "messages-inbox-GridDetail", new { controller = "Message", action = "MessageInboxGridDetails" });
             routes.MapRoute("messages-Send-GridDetail", "messages-Send-GridDetail", new { controller = "Message", action = "MessageSendGridDetails" });
            
             
             routes.MapRoute("message-inbox-Detail", "messages-inbox-Detail", new { controller = "Message", action = "MessageInboxDetails" });
             routes.MapRoute("message-sent-Detail", "messages-sent-Detail", new { controller = "Message", action = "MessageSentDetails" });
             routes.MapRoute("message-Request-Referral-detail", "message-Request-Referral-detail", new { controller = "Message", action = "MessageRequestReferralDetail" });
             routes.MapRoute("Appointment-Cancel", "Appointment-Cancel", new { controller = "Message", action = "AppointmentCancel" });
             routes.MapRoute("inbox-delete", "inboxs-delete", new { controller = "Message", action = "InboxDelete" });
             routes.MapRoute("message-Appointment-Request-detail", "message-Appointment-Request-detail", new { controller = "Message", action = "MessageAppointmentRequestDetail" });
             routes.MapRoute("message-tabs-data", "message-tabs-data", new { controller = "Message", action = "MessageTabsData" });
             routes.MapRoute("message-Attachment", "message-Attachment", new { controller = "Message", action = "MessageAttachment" });
             routes.MapRoute("account-patientrepresentative-save", "account-patientrepresentative-save", new { controller = "Account", action = "PatientRepSave" });
             routes.MapRoute("Linked-Patient-Id-Save", "Linked-Patient-Id-Save", new { controller = "Account", action = "LinkedPatientId" });
             routes.MapRoute("Linked-Patient-Record", "Linked-Patient-Record", new { controller = "Account", action = "LinkedPatientRecords" });
             routes.MapRoute("Account-Link-delete", "Account-Link-delete", new { controller = "Account", action = "AccountLinkdelete" });
             routes.MapRoute("password-check", "password-check", new { controller = "Account", action = "PasswordCheck" });
             routes.MapRoute("password-check-edit", "password-check-edit", new { controller = "Account", action = "PasswordCheckEdit" });
             routes.MapRoute("Account-Notification-Save", "Account-Notification-Save", new { controller = "Account", action = "AccountNotificationSave" });
             routes.MapRoute("doc-visit-share", "doc-visit-share", new { controller = "ClinicalSummary", action = "ShareHideDocumentData" });
            
             


             #region Static Routes : Emergency Contact...
             //( premium prefix with route name and pattern is due to controller name)
             routes.MapRoute("premium-emergency-contact-index", "premium-emergency-contact", new { controller = "Premium", action = "EmergencyContactIndex" });
             routes.MapRoute("premium-emergency-contact-save", "premium-emergency-contact-save", new { controller = "Premium", action = "EmergencyContactSave" });
             routes.MapRoute("premium-emergency-contact-delete", "premium-emergency-contact-delete", new { controller = "Premium", action = "EmergencyContactDelete" });
             #endregion

             #region Static Routes : Manage Doctor...
             routes.MapRoute("premium-manage-doctor-index", "premium-manage-doctor", new { controller = "Premium", action = "ManageDoctorIndex" });
             routes.MapRoute("premium-manage-doctor-save", "premium-manage-doctor-save", new { controller = "Premium", action = "ManageDoctorSave" });
             routes.MapRoute("premium-manage-doctor-delete", "premium-manage-doctor-delete", new { controller = "Premium", action = "ManageDoctorDelete" });
             #endregion

             #region Static Routes : Medical Portfolio...
             routes.MapRoute("premium-medical-portfolio-index", "premium-medical-portfolio", new { controller = "Premium", action = "MedicalPortfolioIndex" });
             //Patient Visit Routes...
             routes.MapRoute("prem-mp-visit-share", "prem-mp-visit-share", new { controller = "Premium", action = "MedicalPortfolioVisitShare" });

             //Patient Outside Doctor Routes...
             routes.MapRoute("prem-mp-doctor-share", "prem-mp-doctor-share", new { controller = "Premium", action = "MedicalPortfolioDoctorShare" });
             routes.MapRoute("prem-mp-doctor-attachment", "prem-mp-doctor-attachment", new { controller = "Premium", action = "MedicalPortfolioDoctorAttachment" });

             //Patient Medical Document Routes...
             routes.MapRoute("prem-mp-document-share", "prem-mp-document-share", new { controller = "Premium", action = "MedicalPortfolioDocumentShare" });
             routes.MapRoute("prem-mp-document-attachment", "prem-mp-document-attachment", new { controller = "Premium", action = "MedicalPortfolioDocumentAttachment" });

             routes.MapRoute("prem-mp-document-upload", "prem-mp-document-upload", new { controller = "Premium", action = "MedicalPortfolioDocumentFileUpload" });
             routes.MapRoute("prem-mp-document-delete", "prem-mp-document-delete", new { controller = "Premium", action = "MedicalPortfolioDocumentDelete" });
             #endregion

             #region Static Routes : General Document...

             routes.MapRoute("premium-general-document-index", "premium-general-document", new { controller = "Premium", action = "GeneralDocumentIndex" });
             routes.MapRoute("premium-general-document-delete", "premium-general-document-delete", new { controller = "Premium", action = "GeneralDocumentDelete" });
             routes.MapRoute("premium-general-document-upload", "premium-general-document-upload", new { controller = "Premium", action = "GeneralDocumentFileUploader" });
             routes.MapRoute("premium-general-document-share", "premium-general-document-share", new { controller = "Premium", action = "GeneralDocumentShare" });
             routes.MapRoute("premium-general-document-attachment", "premium-general-document-attachment", new { controller = "Premium", action = "GeneralDocumentAttachment" });

             #endregion 

             #region Static Routes : Insurance Policy...

             routes.MapRoute("premium-insurance-policy-save", "premium-insurance-policy-save", new { controller = "Premium", action = "InsurancePolicySave" });
             routes.MapRoute("premium-insurance-policy-delete", "premium-insurance-policy-delete", new { controller = "Premium", action = "InsurancePolicyDelete" });
             
             #endregion 

             #region Static Routes : Professional Advisor...
                                                                   
             routes.MapRoute("premium-professional-advisor-save", "premium-professional-advisor-save", new { controller = "Premium", action = "ProfessionalAdvisorSave" });
             routes.MapRoute("premium-professional-advisor-delete", "premium-professional-advisor-delete", new { controller = "Premium", action = "ProfessionalAdvisorDelete" });

             #endregion 

             #region Share My Records Routes
             routes.MapRoute("premium-ShareMyRecords", "premium-ShareMyRecords", new { controller = "Premium", action = "ShareMyRecordsIndex" });
             routes.MapRoute("premium-passcode-save", "premium-passcode-save", new { controller = "Premium", action = "PasscodeSave" });
             routes.MapRoute("premium-send-email", "premium-send-email", new { controller = "Premium", action = "SendEmail" });
             
             #endregion

             #region Static Routes : Consolidate Call Home Widgets Filter...
             routes.MapRoute("HomeWidgetsFilter", "home-widgets-filter", new { controller = "Home", action = "ConsolidateCallHomeWidgetsFilter" });
             #endregion

             routes.MapRoute("CCDXMLOpener", "ccd-xml-opener", new { controller = "Home", action = "GetCCDXML" });

             //routes.MapRoute("default","{controller}/{action}/{id}", new { controller = "core", action = "unknownrequest", id = UrlParameter.Optional });
            // routes.MapRoute("unknown", "{*catchall}", new { controller = "Home", action = "Index" });
             //routes.MapRoute("unknown", "{*catchall}", new { controller = "core", action = "unknownrequest" });
             routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
         }
         public static void RegisterDefaultRoutesProvider(RouteCollection routes)
         {
             routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            // routes.IgnoreRoute("{resource}.aspx/{*pathInfo}");
             routes.IgnoreRoute("{resource}.ico");



             routes.MapRoute("report-index", "report-index", new { controller = "Report", action = "ReportIndex" });
             routes.MapRoute("ProviderLogin", "ProviderLogin", new { controller = "Home", action = "ProviderLogin" });
             routes.MapRoute("logout", "logout", new { controller = "Home", action = "LogOffProviderPortal" });
             routes.MapRoute("provider-signup", "provider-signup", new { controller = "Home", action = "ProviderRegister" });
             routes.MapRoute("reset-password-provider", "reset-password-provider", new { controller = "Home", action = "ResetPasswordProvider" });
             routes.MapRoute("security-question-provider", "security-question-provider", new { controller = "Home", action = "SecurityQuestionProvider" });
             routes.MapRoute("forgotpassword-provider", "forgotpassword-provider", new { controller = "Home", action = "ForgotPasswordProvider" });
             routes.MapRoute("another-session", "another-session", new { controller = "Home", action = "AnotherSession" });
             routes.MapRoute("reset-password", "reset-password", new { controller = "Home", action = "ResetPasswordProvider" });
             routes.MapRoute("security-question", "security-question", new { controller = "Home", action = "SecurityQuestionProvider" });
             routes.MapRoute("home-security-save", "home-security-save", new { controller = "Home", action = "SecuritySaveProvider" });

             routes.MapRoute("account-password-change", "account-password-change", new { controller = "Account", action = "PasswordSave" });
             routes.MapRoute("account-index", "accounts-index", new { controller = "Account", action = "AccountIndex" });

             routes.MapRoute("preactive_pw_resend", "Home/ResetPassword", new { controller = "Home", action = "ResetPasswordProviderDirectly" });
             routes.MapRoute("index", "", new { controller = "Message", action = "MsgIndex" });
             routes.MapRoute("Update-Message-Read-Flag", "Update-Message-Read-Flag", new { controller = "Message", action = "UpdateMessageReadFlag" });
             routes.MapRoute("Get-Messages-Counter", "Get-Messages-Counter", new { controller = "Message", action = "GetMessageCounterProvider" });
             routes.MapRoute("message-compose", "messages-compose", new { controller = "Message", action = "MessageCompose" });
             routes.MapRoute("message-inbox-GridDetail", "messages-inbox-GridDetail", new { controller = "Message", action = "MessageInboxGridDetails" });
             routes.MapRoute("message-inbox-Detail", "messages-inbox-Detail", new { controller = "Message", action = "MessageInboxDetails" });
             routes.MapRoute("message-sent-Detail", "messages-sent-Detail", new { controller = "Message", action = "MessageSentDetails" });
             routes.MapRoute("message-tabs-data", "message-tabs-data", new { controller = "Message", action = "MessageTabsDataProvider" });
             routes.MapRoute("message-reply", "messages-reply", new { controller = "Message", action = "MessageReplyProvider" });
             routes.MapRoute("message-Patientlist-get", "message-Patientlist-get", new { controller = "Message", action = "GetPatientList" });
             routes.MapRoute("message-Appointment-Request-detail", "message-Appointment-Request-detail", new { controller = "Message", action = "MessageAppointmentRequestDetail" });
             routes.MapRoute("messages-attach-reply", "messages-attach-reply", new { controller = "Message", action = "MessageAttachmentReply" });
             routes.MapRoute("messages-request-refill-reply", "messages-request-refill-reply", new { controller = "Message", action = "MessageRequestRefillReply" });
             routes.MapRoute("messages-request-referral-reply", "messages-request-referral-reply", new { controller = "Message", action = "MessageRequestReferralReply" });
             routes.MapRoute("index-medicationlist-get", "index-medicationlists-get", new { controller = "Home", action = "GetMedicationList" });
             routes.MapRoute("report-parameters", "report-parameters", new { controller = "Report", action = "ReportParameters" });
             routes.MapRoute("message-Attachment", "message-Attachment", new { controller = "Message", action = "MessageAttachment" });
             routes.MapRoute("master-image-save", "master-images-save", new { controller = "Home", action = "ProviderImageSave" });
             routes.MapRoute("master-provider-get", "master-provider-get", new { controller = "Home", action = "GetProviderImg" });
             routes.MapRoute("inbox-delete", "inboxs-delete", new { controller = "Message", action = "InboxDeleteProvider" });
             routes.MapRoute("Appointment-Cancel-Provider", "Appointment-Cancel-Provider", new { controller = "Message", action = "AppointmentCancelProvider" });
             //Custom route for reports
             routes.MapRoute(
              "ReportRoute",                         // Route name
              "Reports/{reportname}",                // URL
              "~/Reports/{reportname}.aspx"   // File
              );
             }
         public static void RegisterDefaultRoutesAdmin(RouteCollection routes) 
         {
             routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
             routes.IgnoreRoute("{resource}.aspx/{*pathInfo}");
             routes.IgnoreRoute("{resource}.ico");
             routes.MapRoute("Login", "Login", new { controller = "Admin", action = "Login" });
             routes.MapRoute("LogOffAdminPortal", "LogOffAdminPortal", new { controller = "Admin", action = "LogOffAdminPortal" });

             routes.MapRoute("Admin-Linked-Patient-Id-Save", "Admin-Linked-Patient-Id-Save", new { controller = "Admin", action = "LinkedPatientId" });
             routes.MapRoute("Index", "Index", new { controller = "Admin", action = "Index" });
             routes.MapRoute("ProviderSearchIndex", "ProviderSearchIndex", new { controller = "Admin", action = "ProviderSearchIndex" });

             routes.MapRoute("Organization-Save-Data", "Organization-Save-Data", new { controller = "Admin", action = "OrganizationSaveData" });
             routes.MapRoute("another-session", "another-session", new { controller = "Admin", action = "AnotherSession" });
             routes.MapRoute("Practice-Save-Data", "Practice-Save-Data", new { controller = "Admin", action = "PracticeSaveData" });
             routes.MapRoute("Facility-Save-Data", "Facility-Save-Data", new { controller = "Admin", action = "FacilitySaveData" });
             routes.MapRoute("logout", "logout", new { controller = "Admin", action = "Logout" });
             routes.MapRoute("report-index", "report-index", new { controller = "Report", action = "AdminReportIndex" });
             routes.MapRoute(
              "ReportRoute",                         // Route name
              "Reports/{reportname}",                // URL
              "~/Reports/{reportname}.aspx"   // File
              );

             routes.MapRoute("report-parameters", "report-parameters", new { controller = "Report", action = "AdminReportParameters" });
             
             
             routes.MapRoute("Facility-Setup", "Facility-Setup", new { controller = "Admin", action = "FaciltySetupIndex" });
             routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Admin", action = "Index", id = UrlParameter.Optional }
            );

             
         }
    }
}
