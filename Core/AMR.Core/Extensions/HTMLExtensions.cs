using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMR.Models;
using System.Web.Script.Serialization;
using System.Globalization;



namespace AMR.Core.Extensions
{
    public static class HTMLExtensions  
      {
        public static string ReplaceQuote(this  string input)
        {
            string target = ""; ;
            if (input != null)
            {
             target = input.Replace("'", "`");
            }
            return target;
            }
        
            
        public static string GetPatienProblemSNOMEDModelListHTMLForDashboard(this  List<ProblemSNOMEDModel> collection)
        {
            StringBuilder htmlString = new StringBuilder();
            htmlString.Append("<select name=\"cmbProblemSNOMED\" id=\"cmbProblemSNOMED\" class=\"txt\">");
                   
            foreach (var item in collection.Select((x, i) => new { ProblemSNOMEDModel = x, Index = i + 1 }))
            {
                htmlString.Append("<option value=\"" + item.ProblemSNOMEDModel.ProblemID + "\">" + item.ProblemSNOMEDModel.Value + "</option>");
            }

            htmlString.Append("</select>");
            return htmlString.ToString();
        }
           



    // 
        public static string GetPatientProblemModelListHTMLForDashboard(this  List<ProblemModel> collection)
        {
            StringBuilder htmlString = new StringBuilder();
            htmlString.Append("<table id=\"box-table-a\" summary=\"Appointments\">");
            htmlString.Append("<thead>");
            htmlString.Append("<tr>");
            htmlString.Append("<th scope=\"col\">Effective Date</th>");
            htmlString.Append("<th scope=\"col\">Condition</th>");
            
            htmlString.Append("</tr>");
            htmlString.Append("</thead>");
            htmlString.Append("<tbody>");
            foreach (var item in collection.Select((x, i) => new { ProblemModel = x, Index = i + 1 }))
            {

                htmlString.Append("<tr class=\"" + ((item.Index % 2 == 1) ? "r0" : "r1") + "\">");
                DateTime dtexDate;
                if (item.ProblemModel.EffectiveDate.Length == 8 && DateTime.TryParseExact(item.ProblemModel.EffectiveDate, "yyyyMMdd",CultureInfo.InvariantCulture, DateTimeStyles.None,out dtexDate))
                {
                    DateTime dtEff = DateTime.ParseExact(item.ProblemModel.EffectiveDate, "yyyyMMdd",null);
                    string s = dtEff.ToString("MM/dd/yyyy");
                
                    htmlString.Append("<td>" + s + "</td>");
                }



                else if ((item.ProblemModel.EffectiveDate).Trim().Length == 6)
                {
                    string strYear = item.ProblemModel.EffectiveDate.Substring(0, 4);
                    string strMonth = item.ProblemModel.EffectiveDate.Substring(4, 2);
                    string strEffectiveDate = strMonth + "/" + strYear;
                    htmlString.Append("<td>" + strEffectiveDate + "</td>");
                }
                else
                {
                    htmlString.Append("<td>" + item.ProblemModel.EffectiveDate + "</td>");
                }
                
                htmlString.Append("<td>" + item.ProblemModel.Condition + "</td>");
               
                htmlString.Append("</tr>");
            }


            htmlString.Append("</tbody>");
            htmlString.Append(" </table>");
            return htmlString.ToString();
        }

        public static string GetPatientPOCModelListHTMLForDashboard(this  List<POCModel> collection)
        {
            StringBuilder htmlString = new StringBuilder();
            htmlString.Append("<table id=\"box-table-a\" summary=\"Appointments\">");
            htmlString.Append("<thead>");
            htmlString.Append("<tr>");
            htmlString.Append("<th scope=\"col\">Instruction Type</th>");
            htmlString.Append("<th scope=\"col\">Instruction</th>");
            htmlString.Append("<th scope=\"col\">Goal Type</th>");
            htmlString.Append("<th scope=\"col\">Note</th>");
            htmlString.Append("</tr>");
            htmlString.Append("</thead>");
            htmlString.Append("<tbody>");
            foreach (var item in collection.Select((x, i) => new { POCModel = x, Index = i + 1 }))
            {

                htmlString.Append("<tr class=\"" + ((item.Index % 2 == 1) ? "r0" : "r1") + "\">");
                htmlString.Append("<td>" + item.POCModel.InstructionType + "</td>");
                htmlString.Append("<td>" + item.POCModel.Instruction + "</td>");
                htmlString.Append("<td>" + item.POCModel.Goal + "</td>");
                htmlString.Append("<td>" + item.POCModel.Note + "</td>");
                htmlString.Append("</tr>");
            }


            htmlString.Append("</tbody>");
            htmlString.Append(" </table>");
            return htmlString.ToString();
        }




     //
     public static string GetPatientProblemModelListHTMLForTab(this  List<ProblemModel> collection)
     {
                StringBuilder htmlString = new StringBuilder();
                htmlString.Append("<table id=\"box-table-a\" summary=\"Appointments\">");
                htmlString.Append("<thead>");
                htmlString.Append("<tr>");
                htmlString.Append("<th scope=\"col\" width=\"25%\">Onset Date</th>");
                htmlString.Append("<th scope=\"col\" width=\"25%\">Description</th>");
                htmlString.Append("<th scope=\"col\">Note</th>");
                htmlString.Append("<th scope=\"col\">Status</th>");

                htmlString.Append("<th scope=\"col\" style=\"text-align:center;\">Edit</th>");
                htmlString.Append("<th scope=\"col\" style=\"text-align:center;\">Details</th>");
                htmlString.Append("<th scope=\"col\" style=\"text-align:center;\">Delete</th>");
                htmlString.Append("</tr>");
                htmlString.Append("</thead>");
                htmlString.Append("<tbody>");
               foreach (var item in collection.Select((x, i) => new { ProblemModel = x, Index = i + 1 }))
            {
                 StringBuilder jsonStringProblem= new StringBuilder();
                 jsonStringProblem.Append(" { ");
                 jsonStringProblem.Append("\"EffectiveDate\": \"" + item.ProblemModel.EffectiveDate.ReplaceQuote() + "\",");
                 jsonStringProblem.Append("\"LastChangeDate\": \"" + item.ProblemModel.LastChangeDate.ReplaceQuote() + "\",");
                 jsonStringProblem.Append("\"Note\": \"" + item.ProblemModel.Note.ReplaceQuote() + "\",");
                 jsonStringProblem.Append("\"CodeValue\":  \"" + item.ProblemModel.CodeValue.ReplaceQuote() + "\",");
                 jsonStringProblem.Append("\"ConditionStatusId\":  \"" + item.ProblemModel.ConditionStatusId + "\",");
                 jsonStringProblem.Append("\"Condition\":  \"" + item.ProblemModel.Condition.ReplaceQuote() + "\"");
                 jsonStringProblem.Append("}");
               
                htmlString.Append("<tr class=\"" + ((item.Index % 2 == 1) ? "r0" : "r1") + "\">");
                DateTime dtexDate;
                if (item.ProblemModel.EffectiveDate.Length == 8 && DateTime.TryParseExact(item.ProblemModel.EffectiveDate, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtexDate))
                {
                    DateTime dtEff = DateTime.ParseExact(item.ProblemModel.EffectiveDate, "yyyyMMdd", null);
                    string s = dtEff.ToString("MM/dd/yyyy");
                                        
                    htmlString.Append("<td><input type=\"hidden\" id=\"hdProblemTab" + item.ProblemModel.PatientProblemCntr + "\" value=\'" + jsonStringProblem.ToString() + "\' />" + s + "</td>");
                }



                else if ((item.ProblemModel.EffectiveDate).Trim().Length == 6)
                {
                    string strYear = item.ProblemModel.EffectiveDate.Substring(0, 4);
                    string strMonth = item.ProblemModel.EffectiveDate.Substring(4, 2);
                    string strEffectiveDate = strMonth + "/" + strYear;
                    htmlString.Append("<td><input type=\"hidden\" id=\"hdProblemTab" + item.ProblemModel.PatientProblemCntr + "\" value=\'" + jsonStringProblem.ToString() + "\' />" + strEffectiveDate + "</td>");
                }
                else
                {
                    htmlString.Append("<td><input type=\"hidden\" id=\"hdProblemTab" + item.ProblemModel.PatientProblemCntr + "\" value=\'" + jsonStringProblem.ToString() + "\' />" + item.ProblemModel.EffectiveDate + "</td>");
                }

                
                 htmlString.Append("<td width=\"20%\">" + item.ProblemModel.Condition +"</td>");
                 htmlString.Append("<td>"+item.ProblemModel.Note+ "</td>");
                 htmlString.Append("<td>" + item.ProblemModel.ConditionStatus + "</td>");
                 
                 htmlString.Append("<td align=\"center\">  <img onclick=\"problemEdit("+@item.ProblemModel.PatientProblemCntr + ");\"   src=\"Content/img/edit.png\" /> </td>");
                 htmlString.Append("<td align=\"center\">  <img  onclick=\"problemDetails("+@item.ProblemModel.PatientProblemCntr+ "); \"   src=\"Content/img/details.png\" />  </td>");
                 htmlString.Append("<td align=\"center\">  <img  onclick=\"problemDelete("+@item.ProblemModel.PatientProblemCntr+"); \"   src=\"Content/img/delete.png\" />  </td>");
                 htmlString.Append("</tr>");
                                            }
              

         htmlString.Append("</tbody>");
         htmlString.Append(" </table>");
         return htmlString.ToString();
     }    








    
        public static string GetPatientFamilyHistoryModelListHTMLForDashboard(this  List<FamilyHistoryModel> collection)
        {
            StringBuilder htmlString = new StringBuilder();
            htmlString.Append("<table id=\"box-table-a\" summary=\"Appointments\">");
            htmlString.Append("<thead>");
            htmlString.Append("<tr>");
            htmlString.Append("<th scope=\"col\">Relationship</th>");
            htmlString.Append("<th scope=\"col\">Condition Description</th>");
            htmlString.Append("<th scope=\"col\">Notes</th>");

            htmlString.Append("</tr>");
            htmlString.Append("</thead>");
            htmlString.Append("<tbody>");
            foreach (var item in collection.Select((x, i) => new { FamilyHistoryModel = x, Index = i + 1 }))
            {

                htmlString.Append("<tr class=\"" + ((item.Index % 2 == 1) ? "r0" : "r1") + "\">");
                htmlString.Append("<td>" + item.FamilyHistoryModel.FamilyMember + "</td>");
                htmlString.Append("<td>" + item.FamilyHistoryModel.Description + "</td>");
                htmlString.Append("<td>" + item.FamilyHistoryModel.Note + "</td>");
                htmlString.Append("</tr>");
            }


            htmlString.Append("</tbody>");
            htmlString.Append(" </table>");
            return htmlString.ToString();
        }

        public static string GetPatientLabResutltModelListHTMLForDashboard(this  List<LabResultTestModel> collection)
        {
            StringBuilder htmlString = new StringBuilder();
            htmlString.Append("<table id=\"box-table-a\" style=\"font-family: Lucida Sans Unicod Lucida Grande Sans-Serif;  font-size: 12px; margin-top: 1px; width: 100%; text-align: left; -moz-border-radius: 5px;-webkit-border-radius: 5px;border-radius: 5px;\" summary=\"Appointments\">");
            htmlString.Append("<thead>");
            htmlString.Append("<tr>");
            htmlString.Append("<th scope=\"col\" style=\"font-size: 13px; font-weight: normal; padding: 2px; background: #b9c9fe; border-top: 1px solid #aabcfe; border-bottom: 1px solid #fff; color: #039; \">Component</th>");
            htmlString.Append("<th scope=\"col\" style=\"font-size: 13px; font-weight: normal; padding: 2px; background: #b9c9fe; border-top: 1px solid #aabcfe; border-bottom: 1px solid #fff; color: #039; \">Result</th>");
            htmlString.Append("<th scope=\"col\" style=\"font-size: 13px; font-weight: normal; padding: 2px; background: #b9c9fe; border-top: 1px solid #aabcfe; border-bottom: 1px solid #fff; color: #039; \">Ref Range</th>");
            htmlString.Append("<th scope=\"col\" style=\"font-size: 13px; font-weight: normal; padding: 2px; background: #b9c9fe; border-top: 1px solid #aabcfe; border-bottom: 1px solid #fff; color: #039; \">Units</th>");
            htmlString.Append("<th scope=\"col\" style=\"font-size: 13px; font-weight: normal; padding: 2px; background: #b9c9fe; border-top: 1px solid #aabcfe; border-bottom: 1px solid #fff; color: #039; \">Abnormal</th>");
            

            htmlString.Append("</tr>");
            htmlString.Append("</thead>");
            htmlString.Append("<tbody>");
            foreach (var item in collection.Select((x, i) => new { LabResultTestModel = x, Index = i + 1 }))
            {

               // htmlString.Append("<tr class=\"" + ((item.Index % 2 == 1) ? "r0" : "r1") + "\">");
                htmlString.Append("<tr style=\"" + ((item.Index % 2 == 1) ? "padding: 2px; background: #DDE4FF; border-bottom: 1px solid #fff; color: #669; border-top: 1px solid transparent; cursor: pointer;" : "padding: 2px; background: #F2F5FF; border-bottom: 1px solid #fff; color: #669; border-top: 1px solid transparent; cursor: pointer;") + "\">");
                htmlString.Append("<td style=\"padding:2px;\">" + item.LabResultTestModel.Component + "</td>");
                htmlString.Append("<td style=\"padding:2px;\">" + item.LabResultTestModel.Result + "</td>");
                htmlString.Append("<td style=\"padding:2px;\">" + item.LabResultTestModel.RefRange + "</td>");
                htmlString.Append("<td style=\"padding:2px;\">" + item.LabResultTestModel.Units + "</td>");
                htmlString.Append("<td style=\"padding:2px;\">" + item.LabResultTestModel.AbNormal + "</td>");
                

                htmlString.Append("</tr>");
            }


            htmlString.Append("</tbody>");
            htmlString.Append(" </table>");
            return htmlString.ToString();
        }


        public static string GetPatientRepresentativeModelListHTMLForDashboard(this  PatientRepModel Model)
        {
            StringBuilder htmlString = new StringBuilder();
            htmlString.Append("<table id=\"box-table-a\" summary=\"Appointments\">");
            htmlString.Append("<thead>");
            htmlString.Append("<tr>");
            htmlString.Append("<th scope=\"col\">First Name</th>");
            htmlString.Append("<th scope=\"col\">Last name</th>");
            htmlString.Append("<th scope=\"col\">Email</th>");

            htmlString.Append("</tr>");
            htmlString.Append("</thead>");
            htmlString.Append("<tbody>");
                                    StringBuilder jsonStringPatientRep = new StringBuilder();
                                    jsonStringPatientRep.Append(" { ");
                                    jsonStringPatientRep.Append("\"UserId\": \"" + Model.UserId + "\",");
                                    jsonStringPatientRep.Append("\"FirstName\": \"" + Model.FirstName + "\",");
                                    jsonStringPatientRep.Append("\"LastName\": \"" + Model.LastName + "\",");
                                    jsonStringPatientRep.Append("\"EMail\":  \"" + Model.EMail + "\",");
                                    jsonStringPatientRep.Append("\"Demographics\":  \"" + Model.Demographics + "\",");
                                    jsonStringPatientRep.Append("\"Allergy\":  \"" + Model.Allergy+ "\",");
                                    jsonStringPatientRep.Append("\"FamilyHistory\":  \"" + Model.FamilyHistory + "\",");
                                    jsonStringPatientRep.Append("\"LabResult\":  \"" + Model.LabResults+ "\","); 
                                    jsonStringPatientRep.Append("\"MedicalHistory\":  \"" + Model.MedicalHistory + "\",");
                                    jsonStringPatientRep.Append("\"Medication\":  \"" + Model.Medication+ "\","); 
                                    jsonStringPatientRep.Append("\"Problem\":  \"" + Model.Problem + "\",");
                                    jsonStringPatientRep.Append("\"Emergency\":  \"" + Model.EmergencyContact+ "\","); 
                                    jsonStringPatientRep.Append("\"SocialHistory\":  \"" + Model.SocialHistory + "\",");
                                    jsonStringPatientRep.Append("\"SurgicalHistory\":  \"" + Model.SurgicalHistory+ "\","); 
                                    jsonStringPatientRep.Append("\"VitalSigns\":  \"" + Model.VitalSigns + "\",");
                                    jsonStringPatientRep.Append("\"Immunization\":  \"" + Model.Immunization+ "\","); 
                                    jsonStringPatientRep.Append("\"Organ\":  \"" + Model.Organ + "\",");
                                    jsonStringPatientRep.Append("\"ClinicalDocs\":  \"" + Model.ClinicalDoc+ "\","); 
                                    jsonStringPatientRep.Append("\"Insurance\":  \"" + Model.Insurance + "\",");
                                    jsonStringPatientRep.Append("\"ClinicalSummary\":  \"" + Model.ClinicalSummary+ "\",");  
                                    jsonStringPatientRep.Append("\"Appointment\":  \"" + Model.Appointment + "\",");
                                    jsonStringPatientRep.Append("\"Visit\":  \"" + Model.Visit+ "\","); 
                                    jsonStringPatientRep.Append("\"UploadDocs\":  \"" + Model.UploadDocs + "\",");
                                    jsonStringPatientRep.Append("\"PlaneOfCare\":  \"" + Model.PlanOfCare+ "\","); 
                                    jsonStringPatientRep.Append("\"Messaging\":  \"" + Model.Messaging + "\",");
                                    jsonStringPatientRep.Append("\"Download\":  \"" + Model.DownloadTransmit+ "\",");
                                    jsonStringPatientRep.Append("\"Procedure\":  \"" + Model.Procedure + "\",");
                                    jsonStringPatientRep.Append("\"Enabled\":  \"" + Model.Enabled+ "\"");
                                    jsonStringPatientRep.Append("\"Provider\":  \"" + Model.Provider + "\"");  // SJF Added 1/29/2015
                                    jsonStringPatientRep.Append("}"); 
                                   


              htmlString.Append("<tr class=\"r0\" \">");
                                    htmlString.Append("<input type=\"hidden\" id=\"patrepid\" value=\'" + @jsonStringPatientRep.ToString() + "\' />");
                htmlString.Append("<td>" + Model.FirstName + "</td>");
                htmlString.Append("<td>" + Model.LastName + "</td>");
                htmlString.Append("<td>" + Model.EMail + "</td>");
                htmlString.Append("</tr>");
           


            htmlString.Append("</tbody>");
            htmlString.Append(" </table>");
            return htmlString.ToString();
        }



   // 
    public static string GetPatientFamilyHistoryModelListHTMLForTab(this  List<FamilyHistoryModel> collection)
    {
        StringBuilder htmlString = new StringBuilder();


            htmlString.Append("<table id=\"box-table-a\" summary=\"Appointments\">");
            htmlString.Append("<thead>");
            htmlString.Append("<tr>");
            htmlString.Append("<th scope=\"col\">Relationship</th>");
            htmlString.Append("<th scope=\"col\">Condition Description</th>");
           
            htmlString.Append("<th scope=\"col\">Notes</th>");
            htmlString.Append("<th scope=\"col\" style=\"text-align:center;\">Edit</th>");
            htmlString.Append("<th scope=\"col\" style=\"text-align:center;\">Details</th>");
            htmlString.Append("<th scope=\"col\" style=\"text-align:center;\">Delete</th>");
            htmlString.Append(" </tr>");
            htmlString.Append("</thead>");
            htmlString.Append("<tbody>");
          
            foreach (var item in collection.Select((x, i) => new { FamilyHistoryModel = x, Index = i + 1 }))
           {
              string desc =  item.FamilyHistoryModel.Description.Replace("'", "`");
               StringBuilder jsonStringFamily = new StringBuilder();
               jsonStringFamily.Append(" { ");
               jsonStringFamily.Append("\"PatFamilyHistCntr\": \"" + item.FamilyHistoryModel.PatFamilyHistCntr + "\",");
               jsonStringFamily.Append("\"RelationshipId\": \"" + item.FamilyHistoryModel.RelationshipId + "\",");
               jsonStringFamily.Append("\"Description\":  \"" + desc + "\",");
               jsonStringFamily.Append("\"CodeValue\":  \"" + item.FamilyHistoryModel.CodeValue + "\",");
               jsonStringFamily.Append("\"ConditionStatusId\": \"" + item.FamilyHistoryModel.ConditionStatusId + "\",");
               jsonStringFamily.Append("\"DateReported\":  \"" + item.FamilyHistoryModel.DateReported.ReplaceQuote() + "\",");
               jsonStringFamily.Append("\"Note\":  \"" + item.FamilyHistoryModel.Note.ReplaceQuote() + "\",");
               jsonStringFamily.Append("\"AgeAtOnset\":  \"" + item.FamilyHistoryModel.AgeAtOnset + "\",");
               jsonStringFamily.Append("\"DiseasedAge\":  \"" + (item.FamilyHistoryModel.DiseasedAge) + "\",");
               jsonStringFamily.Append("\"Diseased\":  \"" + (item.FamilyHistoryModel.Diseased) + "\",");
               jsonStringFamily.Append("\"FamilyMember\":  \"" + item.FamilyHistoryModel.SNOMEDCode + "\"");
               jsonStringFamily.Append("}");
                                

            htmlString.Append("<tr class=\"" + ((item.Index % 2 == 1) ? "r0" : "r1") + "\">");
            htmlString.Append("<td><input type=\"hidden\" id=\"hdFamilyHistoryTab" + item.FamilyHistoryModel.PatFamilyHistCntr + "\" value=\'" + jsonStringFamily.ToString() + "\' />" + item.FamilyHistoryModel.FamilyMember + "</td>");
            htmlString.Append("<td>" + item.FamilyHistoryModel.Description + "</td>");
    
            htmlString.Append("<td title=\"" + item.FamilyHistoryModel.Note + "\">" + item.FamilyHistoryModel.Note + "</td>");
            htmlString.Append("<td align=\"center\">  <img onclick=\"familyHistoryEdit(" + @item.FamilyHistoryModel.PatFamilyHistCntr + ");\"   src=\"Content/img/edit.png\" /> </td>");
            htmlString.Append("<td align=\"center\">  <img  onclick=\"familyHistoryDetails(" + @item.FamilyHistoryModel.PatFamilyHistCntr + "); \"   src=\"Content/img/details.png\" />  </td>");
            htmlString.Append("<td align=\"center\">  <img  onclick=\"familyHistoryDelete(" + @item.FamilyHistoryModel.PatFamilyHistCntr + "); \"   src=\"Content/img/delete.png\" />  </td>");
                 
            htmlString.Append("</tr>");
                                }
         
            htmlString.Append("</tbody>");
            htmlString.Append(" </table>");
            return htmlString.ToString();

    }





    public static string GetPharmacyDropDownListHTML(this  List<PatientPharmacyModel> collection)
    {
        StringBuilder htmlString = new StringBuilder();
        htmlString.Append("<select style=\"width:200px;\" name=\"txtPharmacy\" id=\"txtPharmacy\" class=\"txt  ui-corner-all\">");

        foreach (var item in collection.Select((x, i) => new { PatientPharmacyModel = x, Index = i + 1 }))
        {
            htmlString.Append("<option value=\"" + item.PatientPharmacyModel.PharmacyCntr + "\">" + item.PatientPharmacyModel.PharmacyName + "</option>");
        }

        htmlString.Append("</select>");
        return htmlString.ToString();
    }
           




        
        public static string GetPatientPharmacyModelListHTML(this  List<PatientPharmacyModel> collection)
        {
            StringBuilder htmlString = new StringBuilder();


            htmlString.Append("<table id=\"box-table-a\" summary=\"Pharmacies\">");
            htmlString.Append("<thead>");
            htmlString.Append("<tr>");
            htmlString.Append("<th scope=\"col\">Name</th>");
            htmlString.Append("<th scope=\"col\">Address</th>");
            htmlString.Append("<th scope=\"col\">City</th>");
            htmlString.Append("<th scope=\"col\">State</th>");
            htmlString.Append("<th scope=\"col\">Zipcode</th>");
            htmlString.Append("<th scope=\"col\">Notes</th>");
            htmlString.Append("<th scope=\"col\">Edit</th>");
            htmlString.Append("<th scope=\"col\">Details</th>");
            htmlString.Append("<th scope=\"col\">Delete</th>");
            htmlString.Append("</tr>");
            htmlString.Append("</thead>");
            htmlString.Append("<tbody>");
            foreach (var item in collection.Select((x, i) => new { PatientPharmacyModel = x, Index = i + 1 }))
            {
                StringBuilder jsonString = new StringBuilder();

                jsonString.Append(" { ");
                jsonString.Append("\"ID\": \"" + item.PatientPharmacyModel.PharmacyCntr + "\",");
                jsonString.Append("\"PharmacyName\": \"" + item.PatientPharmacyModel.PharmacyName + "\",");
                jsonString.Append("\"Address1\":  \"" + item.PatientPharmacyModel.Address1 + "\",");
                jsonString.Append("\"City\":  \"" + (item.PatientPharmacyModel.City) + "\",");
                jsonString.Append("\"State\":  \"" + item.PatientPharmacyModel.State + "\",");
                jsonString.Append("\"ZipCode\":  \"" + item.PatientPharmacyModel.PostalCode + "\",");
                jsonString.Append("\"Note\":  \"" + item.PatientPharmacyModel.Note + "\" ");


                jsonString.Append("}");


                htmlString.Append("<tr class=\"" + ((item.Index % 2 == 1) ? "r0" : "r1") + "\">");
                htmlString.Append("<td><input type=\"hidden\" id=\"hdPharmJson" + item.PatientPharmacyModel.PharmacyCntr + "\" value='" + jsonString.ToString() + "' />" + item.PatientPharmacyModel.PharmacyName + "</td>");
                htmlString.Append("<td>" + item.PatientPharmacyModel.Address1 + "</td>");
                htmlString.Append("<td>" + item.PatientPharmacyModel.City + "</td>");
                htmlString.Append("<td>" + item.PatientPharmacyModel.State + "</td>");
                htmlString.Append("<td>" + item.PatientPharmacyModel.PostalCode + "</td>");
                htmlString.Append("<td>" + item.PatientPharmacyModel.Note + "</td>");
                htmlString.Append("<td align=\"center\" style=\"cursor: pointer; width: auto; height: 20px;\">");
                htmlString.Append("<div onmousemove=\"removing_class();\" onmouseover=\"removing_class(); \" onmousedown=\"removing_class1();\" onclick=\"removing_class1();editPharmacy(" + item.PatientPharmacyModel.PharmacyCntr + "); \" onmouseout=\"removing_class1();\" onmouseup=\"removing_class1();\" onfocus=\"removing_class1();\" id=\"edit-pharmacy\">");
                htmlString.Append("<img src=\"Content/img/edit.png\" />");
                htmlString.Append("<span class=\"anc\" style=\"padding-right: 10px;\"></span>");
                htmlString.Append("</div>");
                htmlString.Append("</td>");
                htmlString.Append("<td align=\"center\" style=\"cursor: pointer; width: auto; height: 20px;\">");
                htmlString.Append("<div onmousemove=\"removing_class();\" onmouseover=\"removing_class(); \" onmousedown=\"removing_class1();\" onclick=\"removing_class1();detailPharmacy(" + item.PatientPharmacyModel.PharmacyCntr + "); \" onmouseout=\"removing_class1();\" onmouseup=\"removing_class1();\" onfocus=\"removing_class1();\" id=\"detail-pharmacy\">");
                htmlString.Append("<img src=\"Content/img/details.png\" />");
                htmlString.Append("<span class=\"anc\" style=\"padding-right: 4px;\"></span>");
                htmlString.Append("</div>");
                htmlString.Append("</td>");
                htmlString.Append("<td align=\"center\" style=\"cursor: pointer; width: auto; height: 20px;\">");
                htmlString.Append("<div onmousemove=\"removing_class();\" onmouseover=\"removing_class(); \" onmousedown=\"removing_class1();\" onclick=\"removing_class1();deletePharmacy(" + item.PatientPharmacyModel.PharmacyCntr + "); \" onmouseout=\"removing_class1();\" onmouseup=\"removing_class1();\" onfocus=\"removing_class1();\" id=\"delete-pharmacy\">");
                htmlString.Append("<img src=\"Content/img/delete.png\" />");
                htmlString.Append("<span class=\"anc\" style=\"padding-right: 4px;\"></span>");
                htmlString.Append("</div>");
                htmlString.Append("</td>");


                htmlString.Append("</tr>");
            }
            htmlString.Append("</tbody>");
            htmlString.Append("</table>");

            return htmlString.ToString();
        }
        
        


        public static string GetPatientMedicalHistoryModelListHTMLForDashboard(this  List<MedicalHistoryModel> collection)
        {
            StringBuilder htmlString = new StringBuilder();


            htmlString.Append("<table id=\"box-table-a\" summary=\"Appointments\">");
            htmlString.Append("<thead>");
            htmlString.Append("<tr>");
            htmlString.Append("<th scope=\"col\">Occurance</th>");
            htmlString.Append("<th scope=\"col\">Diagnonsis/Disease</th>");
            htmlString.Append("<th scope=\"col\">Note</th>");
            htmlString.Append("</tr>");
            htmlString.Append("</thead>");
            htmlString.Append("<tbody>");
            foreach (var item in collection.Select((x, i) => new { MedicalHistoryModel = x, Index = i + 1 }))
            {

                htmlString.Append("<tr class=\"" + ((item.Index % 2 == 1) ? "r0" : "r1") + "\">");
                DateTime dtexDate;
                if (item.MedicalHistoryModel.DateOfOccurance.Length == 8 && DateTime.TryParseExact(item.MedicalHistoryModel.DateOfOccurance, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtexDate))
                {
                    DateTime dtOcc = DateTime.ParseExact(item.MedicalHistoryModel.DateOfOccurance, "yyyyMMdd", null);
                    string s = dtOcc.ToString("MM/dd/yyyy");

                    htmlString.Append("<td>" + s + "</td>");
                }



                else if ((item.MedicalHistoryModel.DateOfOccurance).Trim().Length == 6)
                {
                    string strYear = item.MedicalHistoryModel.DateOfOccurance.Substring(0, 4);
                    string strMonth = item.MedicalHistoryModel.DateOfOccurance.Substring(4, 2);
                    string strOccDate = strMonth + "/" + strYear;
                    htmlString.Append("<td>" + strOccDate + "</td>");
                }
                else
                {
                    htmlString.Append("<td>" + item.MedicalHistoryModel.DateOfOccurance + "</td>");
                }                                           
                
                htmlString.Append("<td>" + item.MedicalHistoryModel.Description + "</td>");

                htmlString.Append("<td title=\"" + item.MedicalHistoryModel.Note + "\">" + item.MedicalHistoryModel.Note + "</td>");
                htmlString.Append("</tr>");
            }


            htmlString.Append("</tbody>");
            htmlString.Append(" </table>");


            return htmlString.ToString();
        }




public static string GetPatientMedicalHistoryModelListHTMLForTab(this  List<MedicalHistoryModel> collection)
{
    StringBuilder htmlString = new StringBuilder();

      htmlString.Append("<table id=\"box-table-a\" summary=\"Appointments\">");
      htmlString.Append("<thead>");
      htmlString.Append("<tr>");
      htmlString.Append("<th scope=\"col\">Date</th>");
      htmlString.Append("<th scope=\"col\">Diagnosis/Disease</th>");
      htmlString.Append("<th scope=\"col\">Note</th>");
      htmlString.Append("<th scope=\"col\" style=\"text-align:center;\">Edit</th>");
      htmlString.Append("<th scope=\"col\" style=\"text-align:center;\">Details</th>");
      htmlString.Append("<th scope=\"col\" style=\"text-align:center;\">Delete</th>");
      htmlString.Append(" </tr>");
      htmlString.Append("</thead>");
      htmlString.Append("<tbody>");
      foreach (var item in collection.Select((x, i) => new { MedicalHistoryModel = x, Index = i + 1 }))
                    {
                        StringBuilder jsonStringPast = new StringBuilder();

                        jsonStringPast.Append(" { ");
                        jsonStringPast.Append("\"PatMedicalHistCntr\": \"" + item.MedicalHistoryModel.PatMedicalHistCntr + "\",");
                        jsonStringPast.Append("\"DateOfOccurance\": \"" + item.MedicalHistoryModel.DateOfOccurance.ReplaceQuote() + "\",");
                        jsonStringPast.Append("\"Description\":  \"" + item.MedicalHistoryModel.Description.ReplaceQuote() + "\",");
                        jsonStringPast.Append("\"Note\":  \"" + (item.MedicalHistoryModel.Note.ReplaceQuote()) + "\"");
                        jsonStringPast.Append("}");
           
                 htmlString.Append("<tr class=\"" + ((item.Index % 2 == 1) ? "r0" : "r1") + "\">");
                 DateTime dtexDate;
                 if (item.MedicalHistoryModel.DateOfOccurance.Length == 8 && DateTime.TryParseExact(item.MedicalHistoryModel.DateOfOccurance, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtexDate))
                 {
                     DateTime dtEff = DateTime.ParseExact(item.MedicalHistoryModel.DateOfOccurance, "yyyyMMdd", null);
                     string s = dtEff.ToString("MM/dd/yyyy");
                                         
                     htmlString.Append("<td><input type=\"hidden\" id=\"hdPastHistoryTab" + item.MedicalHistoryModel.PatMedicalHistCntr + "\" value=\'" + jsonStringPast.ToString() + "\' />" + s + "</td>");
                 }



                 else if ((item.MedicalHistoryModel.DateOfOccurance).Trim().Length == 6)
                 {
                     string strYear = item.MedicalHistoryModel.DateOfOccurance.Substring(0, 4);
                     string strMonth = item.MedicalHistoryModel.DateOfOccurance.Substring(4, 2);
                     string strOccDate = strMonth + "/" + strYear;                     
                     htmlString.Append("<td><input type=\"hidden\" id=\"hdPastHistoryTab" + item.MedicalHistoryModel.PatMedicalHistCntr + "\" value=\'" + jsonStringPast.ToString() + "\' />" + strOccDate + "</td>");
                     
                 }
                 else
                 {
                     htmlString.Append("<td><input type=\"hidden\" id=\"hdPastHistoryTab" + item.MedicalHistoryModel.PatMedicalHistCntr + "\" value=\'" + jsonStringPast.ToString() + "\' />" + item.MedicalHistoryModel.DateOfOccurance + "</td>");
                 }
                               
                                                   
                                                 
               
                 htmlString.Append("<td>" +item.MedicalHistoryModel.Description + "</td>");
                 htmlString.Append("<td title=\"" + item.MedicalHistoryModel.Note + "\">" +item.MedicalHistoryModel.Note + "</td>");
                 htmlString.Append("<td align=\"center\">  <img onclick=\"pastHistoryEdit(" + @item.MedicalHistoryModel.PatMedicalHistCntr + ");\"   src=\"Content/img/edit.png\" /> </td>");
                 htmlString.Append("<td align=\"center\">  <img  onclick=\"pastHistoryDetails(" + @item.MedicalHistoryModel.PatMedicalHistCntr + "); \"   src=\"Content/img/details.png\" />  </td>");
                 htmlString.Append("<td align=\"center\">  <img  onclick=\"pastHistoryDelete(" + @item.MedicalHistoryModel.PatMedicalHistCntr + "); \"   src=\"Content/img/delete.png\" />  </td>");
                 
                 htmlString.Append("</tr>");
                                }
                 htmlString.Append("</tbody>");
                 htmlString.Append(" </table>");
       
                  return htmlString.ToString();
     }




        
        public static string GetPatientSocialHistoryModelListHTMLForDashboard(this  List<SocialHistoryModel> collection)
        {
            StringBuilder htmlString = new StringBuilder();


            htmlString.Append("<table id=\"box-table-a\" summary=\"Appointments\">");
            htmlString.Append("<thead>");
            htmlString.Append("<tr>");
            htmlString.Append("<th scope=\"col\">Description</th>");            
            htmlString.Append("<th scope=\"col\">Start Date</th>");
            htmlString.Append("<th scope=\"col\">End Date</th>");
            htmlString.Append("<th scope=\"col\">Notes</th>");
            htmlString.Append("</tr>");
            htmlString.Append("</thead>");
            htmlString.Append("<tbody>");
            foreach (var item in collection.Select((x, i) => new { SocialHistoryModel = x, Index = i + 1 }))
            {

                htmlString.Append("<tr class=\"" + ((item.Index % 2 == 1) ? "r0" : "r1") + "\">");
                htmlString.Append("<td>" + item.SocialHistoryModel.Description + "</td>");                
                DateTime dtexDate;
                if (item.SocialHistoryModel.BeginDate.Length == 8 && DateTime.TryParseExact(item.SocialHistoryModel.BeginDate, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtexDate))
                {
                    DateTime dtBd = DateTime.ParseExact(item.SocialHistoryModel.BeginDate, "yyyyMMdd", null);
                    string s = dtBd.ToString("MM/dd/yyyy");

                    htmlString.Append("<td>" + s + "</td>");
                }



                else if ((item.SocialHistoryModel.BeginDate).Trim().Length == 6)
                {
                    string strYear = item.SocialHistoryModel.BeginDate.Substring(0, 4);
                    string strMonth = item.SocialHistoryModel.BeginDate.Substring(4, 2);
                    string strBDate = strMonth + "/" + strYear;
                    htmlString.Append("<td>" + strBDate + "</td>");
                }
                else
                {
                    htmlString.Append("<td>" + item.SocialHistoryModel.BeginDate + "</td>");
                }

                DateTime dtEndDate;
                if (item.SocialHistoryModel.EndDate.Length == 8 && DateTime.TryParseExact(item.SocialHistoryModel.EndDate, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtEndDate))
                {
                    DateTime dtEd = DateTime.ParseExact(item.SocialHistoryModel.EndDate, "yyyyMMdd", null);
                    string s = dtEd.ToString("MM/dd/yyyy");

                    htmlString.Append("<td>" + s + "</td>");
                }



                else if ((item.SocialHistoryModel.BeginDate).Trim().Length == 6)
                {
                    string strYear = item.SocialHistoryModel.EndDate.Substring(0, 4);
                    string strMonth = item.SocialHistoryModel.EndDate.Substring(4, 2);
                    string strEDate = strMonth + "/" + strYear;
                    htmlString.Append("<td>" + strEDate + "</td>");
                }
                else
                {
                    htmlString.Append("<td>" + item.SocialHistoryModel.EndDate + "</td>");
                }                              
                

                                                
                                                   
                htmlString.Append("<td title=\"" + item.SocialHistoryModel.Note + "\">" + item.SocialHistoryModel.Note + "</td>");
                htmlString.Append("</tr>");
            }


            htmlString.Append("</tbody>");
            htmlString.Append(" </table>");


            return htmlString.ToString();
        }

        
        public static string GetPatientSocialHistoryModelListHTMLForTab(this  List<SocialHistoryModel> collection)
        {
            StringBuilder htmlString = new StringBuilder();


            htmlString.Append("<table id=\"box-table-a\" summary=\"Appointments\">");
            htmlString.Append("<thead>");
            htmlString.Append("<tr>");
            htmlString.Append("<th scope=\"col\">Description</th>");            
            htmlString.Append("<th scope=\"col\">Qualifier</th>");
            htmlString.Append("<th scope=\"col\">Code Value</th>");
            htmlString.Append("<th scope=\"col\">Start Date</th>");
            htmlString.Append("<th scope=\"col\">End Date</th>");
            htmlString.Append("<th scope=\"col\">Notes</th>");
            htmlString.Append("<th> </th>");  
            htmlString.Append("<th scope=\"col\" style=\"text-align:center;\">Edit</th>");
            htmlString.Append("<th scope=\"col\" style=\"text-align:center;\">Details</th>");
            htmlString.Append("<th scope=\"col\" style=\"text-align:center;\">Delete</th>");
            htmlString.Append("</tr>");
            htmlString.Append("</thead>");
            htmlString.Append("<tbody>");
            foreach (var item in collection.Select((x, i) => new { SocialHistoryModel = x, Index = i + 1 }))
            {
               

                StringBuilder jsonStringSocial = new StringBuilder();

                jsonStringSocial.Append(" { ");
                jsonStringSocial.Append("\"PatSocialHistCntr\": \"" + item.SocialHistoryModel.PatSocialHistCntr + "\",");
                jsonStringSocial.Append("\"Description\": \"" + item.SocialHistoryModel.Description.ReplaceQuote() + "\",");
                jsonStringSocial.Append("\"Qualifier\":  \"" + item.SocialHistoryModel.Qualifier.ReplaceQuote() + "\",");
                jsonStringSocial.Append("\"CodeValue\":  \"" + item.SocialHistoryModel.CodeValue + "\",");
                jsonStringSocial.Append("\"BeginDate\":  \"" + (item.SocialHistoryModel.BeginDate.ReplaceQuote()) + "\",");
                jsonStringSocial.Append("\"EndDate\":  \"" + item.SocialHistoryModel.EndDate.ReplaceQuote() + "\",");
                jsonStringSocial.Append("\"Note\":  \"" + item.SocialHistoryModel.Note.ReplaceQuote() + "\"");
                //jsonStringSocial.Append("\"Source\":  \"" + item.SocialHistoryModel.DateModified + "\"");

                jsonStringSocial.Append("}");




                htmlString.Append("<tr class=\"" + ((item.Index % 2 == 1) ? "r0" : "r1") + "\">");
                htmlString.Append("<td><input type=\"hidden\" id=\"hdSocialHistoryTab" + item.SocialHistoryModel.PatSocialHistCntr + "\" value=\'" + jsonStringSocial.ToString() + "\' />" + item.SocialHistoryModel.Description + "</td>");                
                htmlString.Append("<td>" + item.SocialHistoryModel.Qualifier + "</td>");
                htmlString.Append("<td>" + item.SocialHistoryModel.CodeValue + "</td>");
                DateTime dtexDate;
                if (item.SocialHistoryModel.BeginDate.Length == 8 && DateTime.TryParseExact(item.SocialHistoryModel.BeginDate, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtexDate))
                {
                    DateTime dtBd = DateTime.ParseExact(item.SocialHistoryModel.BeginDate, "yyyyMMdd", null);
                    string s = dtBd.ToString("MM/dd/yyyy");

                    htmlString.Append("<td>" + s + "</td>");
                }



                else if ((item.SocialHistoryModel.BeginDate).Trim().Length == 6)
                {
                    string strYear = item.SocialHistoryModel.BeginDate.Substring(0, 4);
                    string strMonth = item.SocialHistoryModel.BeginDate.Substring(4, 2);
                    string strBDate = strMonth + "/" + strYear;
                    htmlString.Append("<td>" + strBDate + "</td>");
                }
                else
                {
                    htmlString.Append("<td>" + item.SocialHistoryModel.BeginDate + "</td>");
                }

                DateTime dtEndDate;
                if (item.SocialHistoryModel.EndDate.Length == 8 && DateTime.TryParseExact(item.SocialHistoryModel.EndDate, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtEndDate))
                {
                    DateTime dtEd = DateTime.ParseExact(item.SocialHistoryModel.EndDate, "yyyyMMdd", null);
                    string s = dtEd.ToString("MM/dd/yyyy");

                    htmlString.Append("<td>" + s + "</td>");
                }



                else if ((item.SocialHistoryModel.BeginDate).Trim().Length == 6)
                {
                    string strYear = item.SocialHistoryModel.EndDate.Substring(0, 4);
                    string strMonth = item.SocialHistoryModel.EndDate.Substring(4, 2);
                    string strEDate = strMonth + "/" + strYear;
                    htmlString.Append("<td>" + strEDate + "</td>");
                }
                else
                {
                    htmlString.Append("<td>" + item.SocialHistoryModel.EndDate + "</td>");
                }  
                htmlString.Append("<td>" + item.SocialHistoryModel.Note + "</td>");
                htmlString.Append("<td> </td>");                
                htmlString.Append("<td align=\"center\">  <img onclick=\"socialHistoryEdit(" + @item.SocialHistoryModel.PatSocialHistCntr + ");\"   src=\"Content/img/edit.png\" /> </td>");
                htmlString.Append("<td align=\"center\">  <img  onclick=\"socialHistoryDetails(" + @item.SocialHistoryModel.PatSocialHistCntr + "); \"   src=\"Content/img/details.png\" />  </td>");
                htmlString.Append("<td align=\"center\">  <img  onclick=\"socialHistoryDelete(" + @item.SocialHistoryModel.PatSocialHistCntr + "); \"   src=\"Content/img/delete.png\" />  </td>");
                 
                htmlString.Append("</tr>");
            }


            htmlString.Append("</tbody>");
            htmlString.Append(" </table>");


            return htmlString.ToString();
        }




          
        




          
          public static string GetPatienAllergyModelListHTMLForTab(this  List<AllergyModel> collection)
          {
              StringBuilder htmlString = new StringBuilder();

              htmlString.Append("<table id=\"box-table-a\" summary=\"Appointments\">");
              htmlString.Append("<thead>");
              htmlString.Append("<tr>");
              htmlString.Append("<th scope=\"col\">Effective Date</th>");
              htmlString.Append("<th scope=\"col\">Allergen Type</th>");
              htmlString.Append("<th scope=\"col\">Allergen</th>");
              htmlString.Append("<th scope=\"col\">Reaction</th>");
              htmlString.Append("<th scope=\"col\">Notes</th>");
              htmlString.Append("<th scope=\"col\">Status</th>");
              htmlString.Append(" <th scope=\"col\" align=\"center\">Edit</th>");
              htmlString.Append("<th scope=\"col\" align=\"center\">Details</th>");
              htmlString.Append("<th scope=\"col\" align=\"center\">Delete</th>");
              htmlString.Append("</tr>");
              htmlString.Append("</thead>");
              htmlString.Append("<tbody>");
                                           // <tr class="r0" style="height: 50px;">
                                               
           foreach (var item in collection.Select((x, i) => new { AllergyModel = x, Index = i + 1 }))
              {
                  StringBuilder jsonStringAllergy = new StringBuilder();

                  jsonStringAllergy.Append(" { ");
                  jsonStringAllergy.Append("\"PatFamilyHistCntr\": \"" + item.AllergyModel.PatientAllergyCntr + "\",");
                  jsonStringAllergy.Append("\"EffectiveDate\": \"" + item.AllergyModel.EffectiveDate.ReplaceQuote() + "\",");
                  jsonStringAllergy.Append("\"AllergenType\":  \"" + item.AllergyModel.AllergenType.ReplaceQuote() + "\",");
                  jsonStringAllergy.Append("\"Allergen\":  \"" + item.AllergyModel.Allergen.ReplaceQuote() + "\",");
                  jsonStringAllergy.Append("\"Reaction\":  \"" + item.AllergyModel.Reaction.ReplaceQuote() + "\",");
                  jsonStringAllergy.Append("\"Note\":  \"" + item.AllergyModel.Note.ReplaceQuote() + "\",");
                  jsonStringAllergy.Append("\"ConditionStatusId\":  \"" + (item.AllergyModel.ConditionStatus.ReplaceQuote()) + "\"");
                  jsonStringAllergy.Append("}");
                 


                  htmlString.Append("<tr class=\"" + ((item.Index % 2 == 1) ? "r0" : "r1") + "\">");
                  DateTime dtexDate;
                  if (item.AllergyModel.EffectiveDate.Length == 8 && DateTime.TryParseExact(item.AllergyModel.EffectiveDate, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtexDate))
                  {
                      DateTime dtEff = DateTime.ParseExact(item.AllergyModel.EffectiveDate, "yyyyMMdd", null);
                      string s = dtEff.ToString("MM/dd/yyyy");

                      htmlString.Append("<td><input type=\"hidden\" id=\"hdProblemTab" + item.AllergyModel.PatientAllergyCntr + "\" value=\'" + jsonStringAllergy.ToString() + "\' />" + s + "</td>");
                  }



                  else if ((item.AllergyModel.EffectiveDate).Trim().Length == 6)
                  {
                      string strYear = item.AllergyModel.EffectiveDate.Substring(0, 4);
                      string strMonth = item.AllergyModel.EffectiveDate.Substring(4, 2);
                      string strEffectiveDate = strMonth + "/" + strYear;
                      htmlString.Append("<td><input type=\"hidden\" id=\"hdProblemTab" + item.AllergyModel.PatientAllergyCntr + "\" value=\'" + jsonStringAllergy.ToString() + "\' />" + strEffectiveDate + "</td>");
                  }
                  else
                  {
                      htmlString.Append("<td><input type=\"hidden\" id=\"hdProblemTab" + item.AllergyModel.PatientAllergyCntr + "\" value=\'" + jsonStringAllergy.ToString() + "\' />" + item.AllergyModel.EffectiveDate + "</td>");
                  }                  
                  htmlString.Append("<td>" + item.AllergyModel.AllergenType+ "</td>");
                  htmlString.Append("<td>" + item.AllergyModel.Allergen+ "</td>");
                  htmlString.Append("<td>" + item.AllergyModel.Reaction+ "</td>");
                  htmlString.Append("<td>" + item.AllergyModel.Note+ "</td>");
                  htmlString.Append("<td>" + item.AllergyModel.ConditionStatus+ "</td>");
                  htmlString.Append("<td align=\"center\">  <img onclick=\"allergyEdit(" + @item.AllergyModel.PatientAllergyCntr + ");\"   src=\"Content/img/edit.png\" /> </td>");
                  htmlString.Append("<td align=\"center\">  <img  onclick=\"allergyDetails(" + @item.AllergyModel.PatientAllergyCntr + "); \"   src=\"Content/img/details.png\" />  </td>");
                  htmlString.Append("<td align=\"center\">  <img  onclick=\"allergyDelete(" + @item.AllergyModel.PatientAllergyCntr + "); \"   src=\"Content/img/delete.png\" />  </td>");
                 


                  htmlString.Append("</tr>");
              }


              htmlString.Append("</tbody>");
              htmlString.Append(" </table>");


              return htmlString.ToString();
          }



          public static string GetPatienAllergyModelListHTMLForDashboard(this  List<AllergyModel> collection)
          {
              StringBuilder htmlString = new StringBuilder();

              htmlString.Append("<table id=\"box-table-a\" summary=\"Appointments\">");
              htmlString.Append("<thead>");
              htmlString.Append("<tr>");
              htmlString.Append("<th scope=\"col\">Effective Date</th>");
              htmlString.Append("<th scope=\"col\">Allergen Type</th>");
              htmlString.Append("<th scope=\"col\">Allergen</th>");
              htmlString.Append("<th scope=\"col\">Reaction</th>");
              htmlString.Append("<th scope=\"col\">Notes</th>");
              htmlString.Append("<th scope=\"col\">Status</th>");
             
              htmlString.Append("</tr>");
              htmlString.Append("</thead>");
              htmlString.Append("<tbody>");
             

              foreach (var item in collection.Select((x, i) => new { AllergyModel = x, Index = i + 1 }))
              {
                  StringBuilder jsonStringAllergy = new StringBuilder();

                  jsonStringAllergy.Append(" { ");
                  jsonStringAllergy.Append("\"PatFamilyHistCntr\": \"" + item.AllergyModel.PatientAllergyCntr + "\",");
                  jsonStringAllergy.Append("\"EffectiveDate\": \"" + item.AllergyModel.EffectiveDate + "\",");
                  jsonStringAllergy.Append("\"AllergenType\":  \"" + item.AllergyModel.AllergenType + "\",");
                  jsonStringAllergy.Append("\"Allergen\":  \"" + item.AllergyModel.Allergen + "\",");
                  jsonStringAllergy.Append("\"Reaction\":  \"" + item.AllergyModel.Reaction + "\",");
                  jsonStringAllergy.Append("\"Note\":  \"" + item.AllergyModel.Note + "\",");
                  jsonStringAllergy.Append("\"ConditionStatusId\":  \"" + (item.AllergyModel.ConditionStatusId) + "\"");
                  jsonStringAllergy.Append("}");

                  htmlString.Append("<tr class=\"" + ((item.Index % 2 == 1) ? "r0" : "r1") + "\">");
                  DateTime dtexDate;
                  if (item.AllergyModel.EffectiveDate.Length == 8 && DateTime.TryParseExact(item.AllergyModel.EffectiveDate, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtexDate))
                  {
                      DateTime dtEff = DateTime.ParseExact(item.AllergyModel.EffectiveDate, "yyyyMMdd", null);
                      string s = dtEff.ToString("MM/dd/yyyy");

                      htmlString.Append("<td><input type=\"hidden\" id=\"hdProblemTab" + item.AllergyModel.PatientAllergyCntr + "\" value=\'" + jsonStringAllergy.ToString() + "\' />" + s + "</td>");
                  }



                  else if ((item.AllergyModel.EffectiveDate).Trim().Length == 6)
                  {
                      string strYear = item.AllergyModel.EffectiveDate.Substring(0, 4);
                      string strMonth = item.AllergyModel.EffectiveDate.Substring(4, 2);
                      string strEffectiveDate = strMonth + "/" + strYear;
                      htmlString.Append("<td><input type=\"hidden\" id=\"hdProblemTab" + item.AllergyModel.PatientAllergyCntr + "\" value=\'" + jsonStringAllergy.ToString() + "\' />" + strEffectiveDate + "</td>");
                  }
                  else
                  {
                      htmlString.Append("<td><input type=\"hidden\" id=\"hdProblemTab" + item.AllergyModel.PatientAllergyCntr + "\" value=\'" + jsonStringAllergy.ToString() + "\' />" + item.AllergyModel.EffectiveDate + "</td>");
                  }                 
                  
                  htmlString.Append("<td>" + item.AllergyModel.AllergenType + "</td>");
                  htmlString.Append("<td>" + item.AllergyModel.Allergen + "</td>");
                  htmlString.Append("<td>" + item.AllergyModel.Reaction + "</td>");
                  htmlString.Append("<td>" + item.AllergyModel.Note + "</td>");
                  htmlString.Append("<td>" + item.AllergyModel.ConditionStatus + "</td>");
                  
                  htmlString.Append("</tr>");
              }   
                 htmlString.Append("</tbody>");
                 htmlString.Append(" </table>");


                 return htmlString.ToString();
          }



          
       
        


          public static string GetPatientVisitModelListHTMLForTab(this  List<VisitModel> collection)
          {
              StringBuilder htmlString = new StringBuilder();

              htmlString.Append("<table id=\"box-table-a\" summary=\"Appointments\">");
              htmlString.Append("<thead>");
              htmlString.Append("<tr>");
              htmlString.Append("<th scope=\"col\">Date</th>");
              htmlString.Append("<th scope=\"col\">Visit Reason</th>");
              htmlString.Append("<th scope=\"col\">Provider</th>");
              htmlString.Append("<th scope=\"col\">Location</th>");
              htmlString.Append("<th scope=\"col\">Details</th>");
              htmlString.Append("</tr>");
              htmlString.Append("</thead>");
              htmlString.Append("<tbody>");

              foreach (var item in collection.Select((x, i) => new { VisitModel = x, Index = i + 1 }))
              {

                  htmlString.Append("<tr class=\"" + ((item.Index % 2 == 1) ? "r0" : "r1") + "\">");
                  htmlString.Append("<td>" + item.VisitModel.VisitDate.ToShortDateString() + "</td>");
                  htmlString.Append("<td>" + item.VisitModel.VisitReason + "</td>");
                  htmlString.Append("<td>" + item.VisitModel.ProviderName + "</td>");
                  htmlString.Append("<td>" + item.VisitModel.FacilityName + "</td>");
                  htmlString.Append("<td align=\"center\">  <img  onclick=\"visitDetails(" + @item.VisitModel.VisitID + "); \"   src=\"Content/img/details.png\" />  </td>");
                  htmlString.Append("</tr>");
              }


              htmlString.Append("</tbody>");
              htmlString.Append(" </table>");


              return htmlString.ToString();
          }


          public static string GetPatientVisitModelListHTMLForDashboard(this  List<VisitModel> collection)
          {
              StringBuilder htmlString = new StringBuilder();

              htmlString.Append("<table id=\"box-table-a\" summary=\"Appointments\">");
              htmlString.Append("<thead>");
              htmlString.Append("<tr>");
              htmlString.Append("<th scope=\"col\">Date</th>");
              htmlString.Append("<th scope=\"col\">Visit Reason</th>");
              htmlString.Append("<th scope=\"col\">Location</th>");
              htmlString.Append("<th scope=\"col\">Note</th>");
              htmlString.Append("</tr>");
              htmlString.Append("</thead>");
              htmlString.Append("<tbody>");

              foreach (var item in collection.Select((x, i) => new { VisitModel = x, Index = i + 1 }))
              {

                  htmlString.Append("<tr class=\"" + ((item.Index % 2 == 1) ? "r0" : "r1") + "\">");
                  htmlString.Append("<td>" + item.VisitModel.VisitDate.ToShortDateString() + "</td>");
                  htmlString.Append("<td>" + item.VisitModel.VisitReason + "</td>");
                  htmlString.Append("<td>" + item.VisitModel.FacilityName + "</td>");
                  htmlString.Append("<td > </td>");
                  htmlString.Append("</tr>");
              }


              htmlString.Append("</tbody>");
              htmlString.Append(" </table>");


              return htmlString.ToString();
          }

          public static string GetPatientVisitDropDown(this  List<VisitModel> collection,string extId,string toggleFunct,string filterFunct)
          {
              StringBuilder htmlString = new StringBuilder();
              htmlString.Append("<select name=\""+extId  + "\" id=\"" + extId + "\" style = \"width:100%;\" class=\"txt\" onchange=\" " + filterFunct + " " + toggleFunct +" \">");

            foreach (var item in collection.Select((x, i) => new { VisitModel = x, Index = i + 1 }))
            {
                htmlString.Append("<option value=\"" + item.VisitModel.VisitID + "\">" + item.VisitModel.VisitDate.ToShortDateString() + "</option>");
            }

            htmlString.Append("</select>");
            return htmlString.ToString();

          }


        public static string GetPatientImmunizationModelListHTMLForDashboard(this  List<ImmunizationModel> collection)
        {
            StringBuilder htmlString = new StringBuilder();


            htmlString.Append("<table id=\"box-table-a\" summary=\"Appointments\">");
            htmlString.Append("<thead>");
            htmlString.Append("<tr>");
            htmlString.Append("<th scope=\"col\">Immunization Date</th>");
            htmlString.Append("<th scope=\"col\">Vaccine Administered</th>");
      
            htmlString.Append("</tr>");
            htmlString.Append("</thead>");
            htmlString.Append("<tbody>");
            foreach (var item in collection.Select((x, i) => new { ImmunizationModel = x, Index = i + 1 }))
            {

                htmlString.Append("<tr class=\"" + ((item.Index % 2 == 1) ? "r0" : "r1") + "\">");
                htmlString.Append("<td>" + item.ImmunizationModel.ImmunizationDate + "</td>");
                htmlString.Append("<td>" + item.ImmunizationModel.Vaccine + "</td>");
              
                htmlString.Append("</tr>");
            }


            htmlString.Append("</tbody>");
            htmlString.Append(" </table>");


            return htmlString.ToString();
        }

         
         public static string GetPatientImmunizationModelListHTMLForTab(this  List<ImmunizationModel> collection)
         {
             StringBuilder htmlString = new StringBuilder();
               htmlString.Append("<table id=\"box-table-a\" summary=\"Appointments\">");
               htmlString.Append("<thead>");
               htmlString.Append("<tr>");
               htmlString.Append("<th scope=\"col\" width=\"20%\">Immunization Date</th>");
               htmlString.Append("<th scope=\"col\">Vaccine Administered</th>");
               htmlString.Append("<th scope=\"col\">Edit</th>");
               htmlString.Append("<th scope=\"col\">Details</th>");
               htmlString.Append("<th scope=\"col\">Delete</th>");
               htmlString.Append("</tr>");
               htmlString.Append("</thead>");
               htmlString.Append("<tbody>");
                 foreach (var item in collection.Select((x, i) => new { ImmunizationModel = x, Index = i + 1 }))
                 {
                     StringBuilder jsonStringImmunization = new StringBuilder();

                     jsonStringImmunization.Append(" { ");
                     jsonStringImmunization.Append("\"PatientImmunizationCntr\": \"" + item.ImmunizationModel.PatientImmunizationCntr + "\",");
                     jsonStringImmunization.Append("\"ImmunizationDate\": \"" + item.ImmunizationModel.ImmunizationDate.ReplaceQuote() + "\",");
                     jsonStringImmunization.Append("\"Time\": \"" + item.ImmunizationModel.Time.ReplaceQuote() + "\",");
                     jsonStringImmunization.Append("\"Vaccine\":  \"" + (item.ImmunizationModel.Vaccine.ReplaceQuote()) + "\",");
                     jsonStringImmunization.Append("\"CodeValue\": \"" + item.ImmunizationModel.CodeValue.ReplaceQuote() + "\",");
                     jsonStringImmunization.Append("\"ImmunizationDate\": \"" + item.ImmunizationModel.ImmunizationDate.ReplaceQuote() + "\",");
                     jsonStringImmunization.Append("\"Amount\":  \"" + (item.ImmunizationModel.Amount.ReplaceQuote()) + "\",");
                     jsonStringImmunization.Append("\"Note\": \"" + item.ImmunizationModel.Note.ReplaceQuote() + "\",");
                     jsonStringImmunization.Append("\"Route\": \"" + item.ImmunizationModel.Route.ReplaceQuote() + "\",");
                     jsonStringImmunization.Append("\"Site\":  \"" + (item.ImmunizationModel.Site.ReplaceQuote()) + "\",");
                     jsonStringImmunization.Append("\"Sequence\": \"" + item.ImmunizationModel.Sequence.ReplaceQuote() + "\",");
                     jsonStringImmunization.Append("\"ExpirationDate\": \"" + item.ImmunizationModel.ExpirationDate.ToShortDateString() + "\",");
                     jsonStringImmunization.Append("\"LotNumber\":  \"" + (item.ImmunizationModel.LotNumber.ReplaceQuote()) + "\",");
                     jsonStringImmunization.Append("\"Manufacturer\":  \"" + (item.ImmunizationModel.Manufacturer.ReplaceQuote()) + "\"");
                     jsonStringImmunization.Append("}");
                       

                 htmlString.Append("<tr class=\"" + ((item.Index % 2 == 1) ? "r0" : "r1") + "\">");
                 htmlString.Append("<td><input type=\"hidden\" id=\"hdImmunizationTab" + item.ImmunizationModel.PatientImmunizationCntr + "\" value=\'" + jsonStringImmunization.ToString() + "\' />" + item.ImmunizationModel.ImmunizationDate + "</td>");
                 htmlString.Append("<td>" + item.ImmunizationModel.Vaccine + "</td>");
                 htmlString.Append("<td align=\"center\">  <img onclick=\"immunizationEdit(" + @item.ImmunizationModel.PatientImmunizationCntr + ");\"   src=\"Content/img/edit.png\" /> </td>");
                 htmlString.Append("<td align=\"center\">  <img  onclick=\"immunizationDetails(" + @item.ImmunizationModel.PatientImmunizationCntr + "); \"   src=\"Content/img/details.png\" />  </td>");
                 htmlString.Append("<td align=\"center\">  <img  onclick=\"immunizationDelete(" + @item.ImmunizationModel.PatientImmunizationCntr + "); \"   src=\"Content/img/delete.png\" />  </td>");
                 

                 htmlString.Append("</tr>");          
                            }
                 htmlString.Append("</tbody>");
                 htmlString.Append(" </table>");


             return htmlString.ToString();
         }




            
        public static string GetPatientVitalSignModelListHTMLForDashboard(this  List<PatientVitalSignModel> collection)
        {
            StringBuilder htmlString = new StringBuilder();

            htmlString.Append("<table id=\"box-table-a\" summary=\"Appointments\">");
            htmlString.Append("<thead>");
            htmlString.Append("<tr>");
            htmlString.Append("<th scope=\"col\">Observation Date</th>");
            htmlString.Append("<th scope=\"col\">Blood Pressure</th>");
            htmlString.Append("<th scope=\"col\">Weight (lb)</th>");
            htmlString.Append("<th scope=\"col\">Height</th>");
            htmlString.Append("<th scope=\"col\">BMI</th>");
            htmlString.Append("</tr>");
            htmlString.Append("</thead>");
            htmlString.Append("<tbody>");
            foreach (var item in collection.Select((x, i) => new { PatientVitalSignModel = x, Index = i + 1 }))
            {
                decimal feet =  Math.Round(Convert.ToDecimal((item.PatientVitalSignModel.HeightIn)) / 12,1);
                string[] st_array = feet.ToString().Split('.');
                string height_string = String.Empty;
                if (st_array.Length > 1)
                {
                    height_string = st_array[0] + "\' " + st_array[1] + "\"";
                }
                else if (st_array.Length == 1)
                {
                    height_string = st_array[0] + "\' ";
                }
                                               
                decimal calc = item.PatientVitalSignModel.HeightIn * item.PatientVitalSignModel.HeightIn;
                htmlString.Append("<tr class=\"" + ((item.Index % 2 == 1) ? "r0" : "r1") + "\">");
                htmlString.Append("<td>" + item.PatientVitalSignModel.VitalDate.ToShortDateString() + "</td>");
                htmlString.Append("<td>" + item.PatientVitalSignModel.BloodPressurePosn + "</td>");
                htmlString.Append("<td>" + item.PatientVitalSignModel.WeightLb.ToString() + "</td>");
                htmlString.Append("<td>" + AMR.Core.Utilities.utility.inchToFeet(item.PatientVitalSignModel.HeightIn) + "</td>");
                if (calc != 0)
                {
                    htmlString.Append("<td>" + (Math.Round(Convert.ToDecimal((item.PatientVitalSignModel.WeightLb / (calc))) * 703, 1)) + "</td>");
                }
                else
                {
                    htmlString.Append("<td>" + 0 + "</td>");
                }
                htmlString.Append("</tr>");
            }


            htmlString.Append("</tbody>");
            htmlString.Append(" </table>");


            return htmlString.ToString();
        }

          //  
            public static string GetPatientVitalSignModelListHTMLForTab(this  List<PatientVitalSignModel> collection)
            {
                StringBuilder htmlString = new StringBuilder();

                 htmlString.Append("<table id=\"box-table-a\" summary=\"Appointments\">");
                 htmlString.Append("<thead>");
                  htmlString.Append("<tr>");
                 htmlString.Append("<th scope=\"col\">Observation Date</th>");
                 htmlString.Append("<th scope=\"col\">Blood Pressure</th>");
                 htmlString.Append("<th scope=\"col\">Weight (lb)</th>");
                 htmlString.Append("<th scope=\"col\">Height</th>");
                 htmlString.Append("<th scope=\"col\">BMI</th>");
                 htmlString.Append("<th scope=\"col\">Edit</th>");
                 htmlString.Append("<th scope=\"col\">Details</th>");
                 htmlString.Append("<th scope=\"col\">Delete</th>");
                 htmlString.Append("</tr>");
                 htmlString.Append("</thead>");
                 htmlString.Append("<tbody>");
                                          
                foreach (var item in collection.Select((x, i) => new { PatientVitalSignModel = x, Index = i + 1 }))
                {
                   
                    StringBuilder jsonStringVital = new StringBuilder();

                    string ht1 = "";
                    string ht2 = "0";
                  //  decimal feet = Math.Round(Convert.ToDecimal((item.PatientVitalSignModel.HeightIn)) / 12, 1);
                    string ftinch = AMR.Core.Utilities.utility.inchToFeetSeporate(item.PatientVitalSignModel.HeightIn);
                    string[] st_array = ftinch.Split('.');
                   // string[] st_array = feet.ToString().Split('.');
                    string height_string = String.Empty;
                    if (st_array.Length > 1)
                    {
                        height_string = st_array[0] + "\' " + st_array[1] + "\"";
                        ht1 = st_array[0];
                        ht2 = st_array[1];

                    }
                    else if (st_array.Length == 1)
                    {
                        height_string = st_array[0] + "\' ";
                        ht1 = st_array[0];
                    }
                    decimal calc = item.PatientVitalSignModel.HeightIn * item.PatientVitalSignModel.HeightIn;


                    var bPressure = item.PatientVitalSignModel.BloodPressurePosn;
                    string[] bpressure_array = bPressure.ToString().Split('/');
                                            

                    jsonStringVital.Append(" { ");
                    jsonStringVital.Append("\"PatientVitalCntr\": \"" + item.PatientVitalSignModel.PatientVitalCntr + "\",");
                    jsonStringVital.Append("\"VitalDate\": \"" + item.PatientVitalSignModel.VitalDate.ToShortDateString() + "\",");
                    jsonStringVital.Append("\"BloodPressurePosn\":  \"" + bpressure_array[0].ReplaceQuote() + "\",");
                    jsonStringVital.Append("\"BloodPressurePosn1\":  \"" + bpressure_array[1].ReplaceQuote() + "\",");
                    jsonStringVital.Append("\"WeightLb\":  \"" + item.PatientVitalSignModel.WeightLb + "\",");
                    jsonStringVital.Append("\"HeightIn\":  \"" + ht2.ReplaceQuote() + "\",");
                    jsonStringVital.Append("\"HeightFt\":  \"" + ht1.ReplaceQuote() + "\",");
                    jsonStringVital.Append("\"Systolic\":  \"" + item.PatientVitalSignModel.Systolic + "\",");
                    jsonStringVital.Append("\"Diastolic\":  \"" + item.PatientVitalSignModel.Diastolic + "\",");
                    jsonStringVital.Append("\"Temperature\":  \"" + item.PatientVitalSignModel.Temperature + "\",");
                    jsonStringVital.Append("\"Pulse\":  \"" + item.PatientVitalSignModel.Pulse + "\",");
                    jsonStringVital.Append("\"Respiration\":  \"" + (item.PatientVitalSignModel.Respiration) + "\"");
                    jsonStringVital.Append("}");
                    htmlString.Append("<tr class=\"" + ((item.Index % 2 == 1) ? "r0" : "r1") + "\">");
                     htmlString.Append("<td><input type=\"hidden\" id=\"hdVitalTab"+ item.PatientVitalSignModel.PatientVitalCntr + "\" value=\'"+ jsonStringVital.ToString() + "\' />" + item.PatientVitalSignModel.VitalDate.ToShortDateString() + "</td>");
                   
                    htmlString.Append("<td>" + item.PatientVitalSignModel.BloodPressurePosn + "</td>");
                    htmlString.Append("<td>" + item.PatientVitalSignModel.WeightLb.ToString() + "</td>");
                    htmlString.Append("<td>" + AMR.Core.Utilities.utility.inchToFeet(item.PatientVitalSignModel.HeightIn) + "</td>");
                    if (calc != 0)
                    {
                        htmlString.Append("<td>" + (Math.Round(Convert.ToDecimal((item.PatientVitalSignModel.WeightLb / (calc))) * 703, 1)) + "</td>");
                    }
                    else
                    {
                        htmlString.Append("<td>" + 0 + "</td>");
                    }
                    htmlString.Append("<td align=\"center\">  <img onclick=\"vitalEdit(" + @item.PatientVitalSignModel.PatientVitalCntr + ");\"   src=\"Content/img/edit.png\" /> </td>");
                    htmlString.Append("<td align=\"center\">  <img  onclick=\"vitalDetails(" + @item.PatientVitalSignModel.PatientVitalCntr + "); \"   src=\"Content/img/details.png\" />  </td>");
                    htmlString.Append(" <td align=\"center\">  <img  onclick=\"vitalDelete(" + @item.PatientVitalSignModel.PatientVitalCntr + "); \"   src=\"Content/img/delete.png\" />  </td>");
                    htmlString.Append("</tr>");
                }


                htmlString.Append("</tbody>");
                htmlString.Append(" </table>");


                return htmlString.ToString();
            }





        
        public static string GetPatientCurrentMedicationModelListHTML(this  List<PatientMedicationModel> collection)
        {
            StringBuilder htmlString = new StringBuilder();
               
            htmlString.Append("<table class=\"box-table-a\" id=\"tbl-CurrentMedication\" summary=\"Medications\">");
            htmlString.Append("<thead>");
            htmlString.Append("<tr>");
            htmlString.Append("<th scope=\"col\">Medicine Name</th>");
            htmlString.Append("<th scope=\"col\">Quantity</th>");
            htmlString.Append("<th scope=\"col\">Expire Date</th>");
            htmlString.Append("<th scope=\"col\">Refills</th>");
            htmlString.Append("<th scope=\"col\">Pharmacy</th>");
            htmlString.Append("<th scope=\"col\">Notes</th>");
            htmlString.Append("<th scope=\"col\">Edit</th>");
            htmlString.Append("<th scope=\"col\">Details</th>");
            htmlString.Append("<th scope=\"col\">Delete</th>");
            htmlString.Append("</tr>");
            htmlString.Append("</thead>");
            htmlString.Append("<tbody>");
            foreach (var item in collection.Select((x, i) => new { PatientMedicationModel = x, Index = i + 1 }))
            {
                StringBuilder jsonString = new StringBuilder();
                var route1 = (item.PatientMedicationModel.Route != null) ? item.PatientMedicationModel.Route.ToString() : "";
                jsonString.Append(" { ");
                jsonString.Append("\"ID\": \"" + item.PatientMedicationModel.PatientMedicationCntr + "\",");
                jsonString.Append("\"MedicationName\": \"" + item.PatientMedicationModel.MedicationName + "\",");
                jsonString.Append("\"ExpireDate\":  \"" + item.PatientMedicationModel.ExpireDate.ToShortDateString() + "\",");
                jsonString.Append("\"Status\":  \"" + (item.PatientMedicationModel.Active ? "1" : "0") + "\",");
                jsonString.Append("\"Pharmacy\":  \"" + item.PatientMedicationModel.Pharmacy + "\",");
                jsonString.Append("\"Note\":  \"" + item.PatientMedicationModel.Note + "\",");
                jsonString.Append("\"Days\":  \"" + item.PatientMedicationModel.Days.ToString() + "\",");
                jsonString.Append("\"Quantity\": \"" + item.PatientMedicationModel.Quantity.ToString() + "\",");
                jsonString.Append("\"Refills\":  \"" + item.PatientMedicationModel.Refills + "\",");
                jsonString.Append("\"StartDate\":  \"" + item.PatientMedicationModel.StartDate.ToShortDateString() + "\",");
                jsonString.Append("\"Frequency\": \"" + item.PatientMedicationModel.Frequency.ToString() + "\",");
                jsonString.Append("\"Route\":  \"" + route1 + "\",");  
                jsonString.Append("\"Diagnosis\":  \"" + item.PatientMedicationModel.Diagnosis + "\"");

                jsonString.Append("}");

                htmlString.Append("<tr class=\"" + ((item.Index % 2 == 1) ? "r0" : "r1") + "\">");
                htmlString.Append("<td><input type=\"hidden\" id=\"hdCurJson" + item.PatientMedicationModel.PatientMedicationCntr + "\" value='" + jsonString.ToString() + "' />" + item.PatientMedicationModel.MedicationName + "</td>");
                htmlString.Append("<td>" + item.PatientMedicationModel.Quantity + "</td>");
                htmlString.Append("<td>" + item.PatientMedicationModel.ExpireDate.ToShortDateString() + "</td>");
                htmlString.Append("<td>" + item.PatientMedicationModel.Refills + "</td>");
                htmlString.Append("<td>" + item.PatientMedicationModel.Pharmacy + "</td>");
                htmlString.Append("<td>" + item.PatientMedicationModel.Note + "</td>");
                htmlString.Append("<td align=\"center\" style=\"cursor: pointer; width: auto; height: 20px;\">");
                htmlString.Append("<div onmousemove=\"removing_class();\" onmouseover=\"removing_class(); \" onmousedown=\"removing_class1();\" onclick=\"removing_class1();editCurrentMedication(" + item.PatientMedicationModel.PatientMedicationCntr + "); \" onmouseout=\"removing_class1();\" onmouseup=\"removing_class1();\" onfocus=\"removing_class1();\" id=\"edit-current-medication\">");
                htmlString.Append("<img src=\"Content/img/edit.png\" />");
                htmlString.Append("<span class=\"anc\" style=\"padding-right: 10px;\"></span>");
                htmlString.Append("</div>");
                htmlString.Append("</td>");
                htmlString.Append("<td align=\"center\" style=\"cursor: pointer; width: auto; height: 20px;\">");
                htmlString.Append("<div onmousemove=\"removing_class();\" onmouseover=\"removing_class(); \" onmousedown=\"removing_class1();\" onclick=\"removing_class1();detailCurrentMedication(" + item.PatientMedicationModel.PatientMedicationCntr + "); \" onmouseout=\"removing_class1();\" onmouseup=\"removing_class1();\" onfocus=\"removing_class1();\" id=\"detail-current-medication\">");
                htmlString.Append("<img src=\"Content/img/details.png\" />");
                htmlString.Append("<span class=\"anc\" style=\"padding-right: 4px;\"></span>");
                htmlString.Append("</div>");
                htmlString.Append("</td>");
                htmlString.Append("<td align=\"center\" style=\"cursor: pointer; width: auto; height: 20px;\">");
                htmlString.Append("<div onmousemove=\"removing_class();\" onmouseover=\"removing_class(); \" onmousedown=\"removing_class1();\" onclick=\"removing_class1();deleteCurrentMedication(" + item.PatientMedicationModel.PatientMedicationCntr + "); \" onmouseout=\"removing_class1();\" onmouseup=\"removing_class1();\" onfocus=\"removing_class1();\" id=\"delete-current-medication\">");
                htmlString.Append("<img src=\"Content/img/delete.png\" />");
                htmlString.Append("<span class=\"anc\" style=\"padding-right: 4px;\"></span>");
                htmlString.Append("</div>");
                htmlString.Append("</td>");

                htmlString.Append("</tr>");
            }
            htmlString.Append("</tbody>");
            htmlString.Append("</table>");

            return htmlString.ToString();
        }

        
        public static string GetPatientPastMedicationModelListHTML(this  List<PatientMedicationModel> collection)
        {
            StringBuilder htmlString = new StringBuilder();


            htmlString.Append("<table class=\"box-table-a\" id=\"tbl-PastMedication\" summary=\"Medications\">");
            htmlString.Append("<thead>");
            htmlString.Append("<tr>");
            htmlString.Append("<th scope=\"col\">Medicine Name</th>");
            htmlString.Append("<th scope=\"col\">Quantity</th>");
            htmlString.Append("<th scope=\"col\">Expire Date</th>");
            htmlString.Append("<th scope=\"col\">Refills</th>");
            htmlString.Append("<th scope=\"col\">Pharmacy</th>");
            htmlString.Append("<th scope=\"col\">Notes</th>");
            htmlString.Append("<th scope=\"col\">Edit</th>");
            htmlString.Append("<th scope=\"col\">Details</th>");
            htmlString.Append("<th scope=\"col\">Delete</th>");
            htmlString.Append("</tr>");
            htmlString.Append("</thead>");
            htmlString.Append("<tbody>");
            foreach (var item in collection.Select((x, i) => new { PatientMedicationModel = x, Index = i + 1 }))
            {
                StringBuilder jsonString = new StringBuilder();
                var route = (item.PatientMedicationModel.Route != null) ? item.PatientMedicationModel.Route.ToString() : "";
                jsonString.Append(" { ");
                jsonString.Append("\"ID\": \"" + item.PatientMedicationModel.PatientMedicationCntr + "\",");
                jsonString.Append("\"MedicationName\": \"" + item.PatientMedicationModel.MedicationName + "\",");
                jsonString.Append("\"ExpireDate\":  \"" + item.PatientMedicationModel.ExpireDate.ToShortDateString() + "\",");
                jsonString.Append("\"Status\":  \"" + (item.PatientMedicationModel.Active ? "1" : "0") + "\",");
                jsonString.Append("\"Pharmacy\":  \"" + item.PatientMedicationModel.Pharmacy + "\",");
                jsonString.Append("\"Note\":  \"" + item.PatientMedicationModel.Note + "\",");
                jsonString.Append("\"Days\":  \"" + item.PatientMedicationModel.Days.ToString() + "\",");
                jsonString.Append("\"Quantity\": \"" + item.PatientMedicationModel.Quantity.ToString() + "\",");
                jsonString.Append("\"Refills\":  \"" + item.PatientMedicationModel.Refills + "\",");
                jsonString.Append("\"StartDate\":  \"" + item.PatientMedicationModel.StartDate.ToShortDateString() + "\",");
                jsonString.Append("\"Frequency\": \"" + item.PatientMedicationModel.Frequency .ToString() + "\",");
                jsonString.Append("\"Route\":  \"" + route + "\",");  
                jsonString.Append("\"Diagnosis\":  \"" + item.PatientMedicationModel.Diagnosis + "\"");

                jsonString.Append("}");


                htmlString.Append("<tr class=\"" + ((item.Index % 2 == 1) ? "r0" : "r1") + "\">");
                htmlString.Append("<td><input type=\"hidden\" id=\"hdPasJson" + item.PatientMedicationModel.PatientMedicationCntr + "\" value='" + jsonString.ToString() + "' />" + item.PatientMedicationModel.MedicationName + "</td>");
                htmlString.Append("<td>" + item.PatientMedicationModel.Quantity + "</td>");
                htmlString.Append("<td>" + item.PatientMedicationModel.ExpireDate.ToShortDateString() + "</td>");
                htmlString.Append("<td>" + item.PatientMedicationModel.Refills + "</td>");
                htmlString.Append("<td>" + item.PatientMedicationModel.Pharmacy + "</td>");
                htmlString.Append("<td>" + item.PatientMedicationModel.Note + "</td>");
                htmlString.Append("<td align=\"center\" style=\"cursor: pointer; width: auto; height: 20px;\">");
                htmlString.Append("<div onmousemove=\"removing_class();\" onmouseover=\"removing_class(); \" onmousedown=\"removing_class1();\" onclick=\"removing_class1();editPastMedication(" + item.PatientMedicationModel.PatientMedicationCntr + "); \" onmouseout=\"removing_class1();\" onmouseup=\"removing_class1();\" onfocus=\"removing_class1();\" id=\"edit-past-medication\">");
                htmlString.Append("<img src=\"Content/img/edit.png\" />");
                htmlString.Append("<span class=\"anc\" style=\"padding-right: 10px;\"></span>");
                htmlString.Append("</div>");
                htmlString.Append("</td>");
                htmlString.Append("<td align=\"center\" style=\"cursor: pointer; width: auto; height: 20px;\">");
                htmlString.Append("<div onmousemove=\"removing_class();\" onmouseover=\"removing_class(); \" onmousedown=\"removing_class1();\" onclick=\"removing_class1();detailPastMedication(" + item.PatientMedicationModel.PatientMedicationCntr + "); \" onmouseout=\"removing_class1();\" onmouseup=\"removing_class1();\" onfocus=\"removing_class1();\" id=\"detail-past-medication\">");
                htmlString.Append("<img src=\"Content/img/details.png\" />");
                htmlString.Append("<span class=\"anc\" style=\"padding-right: 4px;\"></span>");
                htmlString.Append("</div>");
                htmlString.Append("</td>");
                htmlString.Append("<td align=\"center\" style=\"cursor: pointer; width: auto; height: 20px;\">");
                htmlString.Append("<div onmousemove=\"removing_class();\" onmouseover=\"removing_class(); \" onmousedown=\"removing_class1();\" onclick=\"removing_class1();deletePastMedication(" + item.PatientMedicationModel.PatientMedicationCntr + "); \" onmouseout=\"removing_class1();\" onmouseup=\"removing_class1();\" onfocus=\"removing_class1();\" id=\"delete-past-medication\">");
                htmlString.Append("<img src=\"Content/img/delete.png\" />");
                htmlString.Append("<span class=\"anc\" style=\"padding-right: 4px;\"></span>");
                htmlString.Append("</div>");
                htmlString.Append("</td>");


                htmlString.Append("</tr>");
            }
            htmlString.Append("</tbody>");
            htmlString.Append("</table>");

            return htmlString.ToString();
        }

        public static string GetSentTabsDataHTML(List<PatientMessageModel> collection, string[] MessageType)
        {
            StringBuilder htmlString = new StringBuilder();
            htmlString.Append("<table id=\"box-table-a\" summary=\"Medications\">");
            htmlString.Append("<thead>");
            htmlString.Append("<tr>");
            htmlString.Append("<th scope=\"col\" style=\"margin-right: 10px;\">");
            htmlString.Append("<input name=\"check\" type=\"checkbox\" class=\"CheckAll\" onclick=\"checkAll('chk3');\"/>");
            htmlString.Append("</th>");

            htmlString.Append("<th scope=\"col\">");
            htmlString.Append("<div style=\"margin-left: 20px;\">");
            htmlString.Append("Provider");
            htmlString.Append("</div>");
            htmlString.Append("</th>");
            htmlString.Append("<th scope=\"col\">");
            htmlString.Append("<div style=\"margin-left: 20px;\">");
            htmlString.Append("Facility");
            htmlString.Append("</div>");
            htmlString.Append("</th>");
            htmlString.Append("<th scope=\"col\">");
            htmlString.Append("<div style=\"margin-left: 30px;\">");
            htmlString.Append("Request");
            htmlString.Append("</div>");
            htmlString.Append("</th>");
            htmlString.Append("<th scope=\"col\">");
            htmlString.Append("<div style=\"margin-left: 80px;\">");
            htmlString.Append("Status");
            htmlString.Append("</div>");
            htmlString.Append("</th>");
            htmlString.Append("<th scope=\"col\">");
            htmlString.Append("<div style=\"margin-left: 80px;\">");
            htmlString.Append("Message");
            htmlString.Append("</div>");
            htmlString.Append("</th>");

            htmlString.Append("</tr>");
            htmlString.Append("</thead>");
            htmlString.Append("<tbody>");

            foreach (var item in collection.Select((x, i) => new { PatientMessageModel = x, Index = i + 1 }))
            {

                if (item.PatientMessageModel.MessageTypeId == MessageType[0] || item.PatientMessageModel.MessageTypeId == MessageType[0])
                {
                    htmlString.Append("<tr class=\"" + ((item.Index % 2 == 1) ? "r0" : "r1") + "\" >");
                    htmlString.Append("<td>");
                    htmlString.Append("<input type=\"checkbox\" id=\"chk3\" value=\"1\" name=\"chk[]\" />");
                    htmlString.Append("</td>");

                    htmlString.Append("<td>");
                    htmlString.Append("<div style=\"margin-left: 20px;\">");
                    htmlString.Append(item.PatientMessageModel.ProviderName_To);
                    htmlString.Append("</div>");
                    htmlString.Append("</td>");
                    htmlString.Append("<td>");
                    htmlString.Append("<div style=\"margin-left: 20px;\">");
                    htmlString.Append(item.PatientMessageModel.FacilityName);
                    htmlString.Append("</div>");
                    htmlString.Append("</td>");

                    htmlString.Append("<td>");
                    htmlString.Append("<div style=\"margin-left: 30px;\">");
                    htmlString.Append(item.PatientMessageModel.MessageType);
                    htmlString.Append("</div>");
                    htmlString.Append("</td>");
                    htmlString.Append("<td>");
                    htmlString.Append("<div style=\"margin-left: 80px;\">");
                    htmlString.Append(item.PatientMessageModel.MessageStatus);
                    htmlString.Append("</div>");
                    htmlString.Append("</td>");
                    htmlString.Append("<td>");
                    htmlString.Append("<div style=\"margin-left: 80px;\">");
                    htmlString.Append(item.PatientMessageModel.MessageRequest);
                    htmlString.Append("</div>");
                    htmlString.Append("</td>");

                    htmlString.Append("</tr>");
                }
            }
            htmlString.Append("</tbody>");
            htmlString.Append("</table>");
            return htmlString.ToString();
        }

        public static string GetInboxTabsDataHTML(List<PatientMessageModel> collection, string[] MessageType)
        {
            StringBuilder htmlString = new StringBuilder();
            htmlString.Append("<table id=\"box-table-a\" summary=\"Medications\">");
            htmlString.Append("<thead>");
            htmlString.Append("<tr>");
            htmlString.Append("<th scope=\"col\" style=\"margin-right: 10px;\">");
            htmlString.Append("<input name=\"check\" type=\"checkbox\" class=\"CheckAll\" onclick=\"checkAll('chk');\"/>");
            htmlString.Append("</th>");

            htmlString.Append("<th scope=\"col\">");
            htmlString.Append("<div style=\"margin-left: 20px;\"> ");
            htmlString.Append("<img src=\"Content/img/i.png\" />"); 
            htmlString.Append("<th scope=\"col\">");
            htmlString.Append("<div style=\"margin-left: 20px;\">");
            htmlString.Append("From");
            htmlString.Append("</div>");
            htmlString.Append("</th>");
            htmlString.Append("<th scope=\"col\">");
            htmlString.Append("<div style=\"margin-left: 40px;\">");
            htmlString.Append("To");
            htmlString.Append("</div>");
            htmlString.Append("</th>");
            htmlString.Append("<th scope=\"col\">");
            htmlString.Append("<div style=\"margin-left: 70px;\">");
            htmlString.Append("Priority");
            htmlString.Append("</div>");
            htmlString.Append("</th>");
            htmlString.Append("<th scope=\"col\">");
            htmlString.Append("<div style=\"margin-left: 80px;\">");
            htmlString.Append("Subject");
            htmlString.Append("</div>");
            htmlString.Append("</th>");
            //htmlString.Append("<th scope=\"col\">");
            //htmlString.Append("<div style=\"margin-left: 90px;\">");
            //htmlString.Append("Detail");
            //htmlString.Append("</div>");
            //htmlString.Append("</th>");

            htmlString.Append("</tr>");
            htmlString.Append("</thead>");
            htmlString.Append("<tbody>");

            foreach (var item in collection.Select((x, i) => new { PatientMessageModel = x, Index = i + 1 }))
            {

                if (item.PatientMessageModel.MessageTypeId == MessageType[0] || item.PatientMessageModel.MessageTypeId == MessageType[1])
                {
                    htmlString.Append("<tr class=\"" + ((item.Index % 2 == 1) ? "r0" : "r1") + "\" >");
                    htmlString.Append("<td >");
                    htmlString.Append("<input type=\"hidden\" id=\"'" + item.PatientMessageModel.MessageId + "'\">");
                    htmlString.Append("<input type=\"checkbox\" id=\"chk3\" value=\"1\" name=\"chk[]\" class=\"'" + item.PatientMessageModel.MessageId + "'\" />");
                    htmlString.Append("</td>");
                    htmlString.Append("<td></td>");
                    htmlString.Append("<td>");
                    htmlString.Append("<div style=\"margin-left: 20px;\">");
                    if (item.PatientMessageModel.ProviderId_From == "0")
                                       {
                                           htmlString.Append(item.PatientMessageModel.CreatedByName);  
                                       }
                                       else
                                       {
                                           htmlString.Append(item.PatientMessageModel.ProviderName_From);  
                                       } 
                    htmlString.Append("</div>");
                    htmlString.Append("</td>");
                    htmlString.Append("<td>");
                    htmlString.Append("<div style=\"margin-left: 30px;\">");
                    if (item.PatientMessageModel.ProviderId_To == "0")
                                        {
                                             htmlString.Append(item.PatientMessageModel.CreatedByName);  
                                        }
                    htmlString.Append(item.PatientMessageModel.ProviderName_To);
                    htmlString.Append("</div>");
                    htmlString.Append("</td>");

                    htmlString.Append("<td>");
                    htmlString.Append("<div style=\"margin-left: 80px;\">");
                    htmlString.Append(item.PatientMessageModel.MessageUrgency);
                    htmlString.Append("</div>");
                    htmlString.Append("</td>");
                    htmlString.Append("<td>");
                    htmlString.Append("<div style=\"margin-left: 60px;\">");
                    htmlString.Append(item.PatientMessageModel.MessageType);
                    htmlString.Append("</div>");
                    htmlString.Append("</td>");
                    htmlString.Append("<td>");
                    htmlString.Append("<div style=\"margin-left: 88px;\">");
                    htmlString.Append("<img src=\"Content/img/details.png\" onclick=\"inboxDetail('" + item.PatientMessageModel.MessageId + "','" + MessageType[2] + "');\"/>");
                    htmlString.Append("</div>");
                    htmlString.Append("</td>");

                    htmlString.Append("</tr>");
                }
            }
            htmlString.Append("</tbody>");
            htmlString.Append("</table>");
            return htmlString.ToString();
        }

        public static string GetReferalRequestTabsDataHTML(List<PatientMessageModel> collection, string MessageType)
        {
            StringBuilder htmlString = new StringBuilder();
            htmlString.Append("<table id=\"box-table-a\" summary=\"Medications\">");
            htmlString.Append("<thead>");
            htmlString.Append("<tr>");
            htmlString.Append("<th scope=\"col\" style=\"margin-right: 10px;\">");
            htmlString.Append("<input name=\"check\" type=\"checkbox\" class=\"CheckAll\" onclick=\"checkAll('chk3');\"/>");
            htmlString.Append("</th>");

            htmlString.Append("<th scope=\"col\">");
            htmlString.Append("<div style=\"margin-left: 20px;\">");
            htmlString.Append("Provider");
            htmlString.Append("</div>");
            htmlString.Append("</th>");
            htmlString.Append("<th scope=\"col\">");
            htmlString.Append("<div style=\"margin-left: 20px;\">");
            htmlString.Append("Facility");
            htmlString.Append("</div>");
            htmlString.Append("</th>");
            htmlString.Append("<th scope=\"col\">");
            htmlString.Append("<div style=\"margin-left: 30px;\">");
            htmlString.Append("Request");
            htmlString.Append("</div>");
            htmlString.Append("</th>");
            htmlString.Append("<th scope=\"col\">");
            htmlString.Append("<div style=\"margin-left: 80px;\">");
            htmlString.Append("Status");
            htmlString.Append("</div>");
            htmlString.Append("</th>");
            htmlString.Append("<th scope=\"col\">");
            htmlString.Append("<div style=\"margin-left: 80px;\">");
            htmlString.Append("Message");
            htmlString.Append("</div>");
            htmlString.Append("</th>");
            htmlString.Append("<th scope=\"col\">");
            htmlString.Append("<div style=\"margin-left: 80px;\">");
            htmlString.Append("Details");
            htmlString.Append("</div>");
            htmlString.Append("</th>");

            htmlString.Append("</tr>");
            htmlString.Append("</thead>");
            htmlString.Append("<tbody>");

            foreach (var item in collection.Select((x, i) => new { PatientMessageModel = x, Index = i + 1 }))
            {
                
                if (item.PatientMessageModel.MessageType == MessageType)
                {
                    if (MessageType == "Referral Message")
                    {
                        htmlString.Append("<tr class=\"" + ((item.Index % 2 == 1) ? "r0" : "r1") + "\" >");
                    }
                    else
                    {
                        htmlString.Append("<tr class=\"" + ((item.Index % 2 == 1) ? "r0" : "r1") + "\" >");
                  
                    }

                    htmlString.Append("<td>");
                    htmlString.Append("<input type=\"checkbox\" id=\"chk3\" value=\"1\" name=\"chk[]\" />");
                    htmlString.Append("</td>");

                    htmlString.Append("<td>");
                    htmlString.Append("<div style=\"margin-left: 20px;\">");
                    htmlString.Append(item.PatientMessageModel.ProviderName_To);
                    htmlString.Append("</div>");
                    htmlString.Append("</td>");
                    htmlString.Append("<td>");
                    htmlString.Append("<div style=\"margin-left: 20px;\">");
                    htmlString.Append(item.PatientMessageModel.FacilityName);
                    htmlString.Append("</div>");
                    htmlString.Append("</td>");

                    htmlString.Append("<td>");
                    htmlString.Append("<div style=\"margin-left: 30px;\">");
                    htmlString.Append(item.PatientMessageModel.MessageType);
                    htmlString.Append("</div>");
                    htmlString.Append("</td>");
                    htmlString.Append("<td>");
                    htmlString.Append("<div style=\"margin-left: 80px;\">");
                    htmlString.Append(item.PatientMessageModel.MessageStatus);
                    htmlString.Append("</div>");
                    htmlString.Append("</td>");
                    htmlString.Append("<td>");
                    htmlString.Append("<div style=\"margin-left: 80px;\">");
                    htmlString.Append(item.PatientMessageModel.MessageRequest);
                    htmlString.Append("</div>");
                    htmlString.Append("</td>");
                    htmlString.Append("<td>");
                    if (MessageType == "Referral Message")
                    {
                        htmlString.Append("<img src=\"Content/img/details.png\" onclick=\"ReferralRequestDetail('" + item.PatientMessageModel.MessageId + "');\"/>");
                    }
                    else
                    {
                        htmlString.Append("<img src=\"Content/img/details.png\" onclick=\"medicationRefillDetail('" + item.PatientMessageModel.MessageId + "');\"/>");
                   
                    }

                    htmlString.Append("<td>");
                    htmlString.Append("</tr>");
                }
            }
            htmlString.Append("</tbody>");
            htmlString.Append("</table>");
            return htmlString.ToString();
        }

        public static string GetReferralRequestDetailHTML(this PatientMessageModel obj)
        {
            StringBuilder htmlString = new StringBuilder();
            // <!-- Reply Part-->
            
            htmlString.Append("<div id=\"Inform_Head\" class=\"ui-corner-all\">");
            htmlString.Append("<h4 style=\"padding-top: 10px; margin-left: 10px;\">");
            htmlString.Append("<b> " + obj.MessageType + "</b></h4>");
            htmlString.Append("<div style=\"float: right; margin-top: -30px; font-size: 12px; color: #999999;\">");
            htmlString.Append(DateTime.Now.ToString());
            htmlString.Append("</div>");
            htmlString.Append("<div style=\"margin-top: 1px; margin-left: 50px; font-size: 12px; color: #999999;\">");
            htmlString.Append("To:  " + obj.ProviderName_To);
            htmlString.Append("</div>");
            htmlString.Append("<div style=\"margin-top: 1px; margin-left: 90px; font-size: 12px; color: #999999;\">");
            htmlString.Append("From:  " + obj.PatientId.ToString());
            htmlString.Append("</div>");
            htmlString.Append("</div>");
            //<!--End of reply part-->
            htmlString.Append("<div id=\"reply\" style=\"height: 625px;\" class=\"ui-corner-all\">");
            htmlString.Append("<div style=\"margin-left: 30px;\">");
            htmlString.Append("        <br>");
            htmlString.Append("        <br>");
            htmlString.Append(obj.MessageType);
            htmlString.Append("                        <br>");
            htmlString.Append("Message Request :" + obj.MessageRequest);
            htmlString.Append("                        <br>");
            htmlString.Append("Facility Name :" + obj.FacilityName);
            htmlString.Append("                        <br>");
            htmlString.Append("    </div>");
            htmlString.Append("</div>");
            return htmlString.ToString();
        }

        public static string GetAppointmentTabsDataHTML(List<PatientMessageModel> collection, string MessageType)
        {
            StringBuilder htmlString = new StringBuilder();
            htmlString.Append("<table id=\"box-table-a\" summary=\"Medications\">");
                    htmlString.Append("<thead>");
                        htmlString.Append("<tr>");
                            htmlString.Append("<th scope=\"col\">");
                               htmlString.Append("<input name=\"check\" type=\"checkbox\" class=\"CheckAll\" onclick=\"checkAll('chk3');\"/>");
                            htmlString.Append("</th>");
                            
                            htmlString.Append("<th scope=\"col\">");
                               // htmlString.Append("<div style=\"margin-left: 20px;\">");
                                    htmlString.Append("Date");
                               // htmlString.Append("</div>");
                            htmlString.Append("</th>");
                            htmlString.Append("<th scope=\"col\">");
                             //   htmlString.Append("<div style=\"margin-left: 20px;\">");
                                    htmlString.Append("Time");
                             //   htmlString.Append("</div>");
                            htmlString.Append("</th>");
                            htmlString.Append("<th scope=\"col\">");
                          //      htmlString.Append("<div style=\"margin-left: 30px;\">");
                                    htmlString.Append("Visit Reason");
                            //    htmlString.Append("</div>");
                            htmlString.Append("</th>");
                            htmlString.Append("<th scope=\"col\">");
                              //  htmlString.Append("<div style=\"margin-left: 80px;\">");
                                htmlString.Append("Location");
                                //htmlString.Append("</div>");
                            htmlString.Append("</th>");
                            htmlString.Append("<th scope=\"col\">");
                                //htmlString.Append("<div style=\"margin-left: 80px;\">");
                                    htmlString.Append("Provider");
                                //htmlString.Append("</div>");
                            htmlString.Append("</th>");

                            htmlString.Append("<th scope=\"col\">");

                            htmlString.Append("Status");

                            htmlString.Append("</th>");
                            htmlString.Append("<th scope=\"col\">");

                            htmlString.Append("Details");

                            htmlString.Append("</th>");
                        htmlString.Append("</tr>");
                    htmlString.Append("</thead>");
                    htmlString.Append("<tbody>");

                    foreach (var item in collection.Select((x, i) => new { PatientMessageModel = x, Index = i + 1 }))
                    {
                        if (item.PatientMessageModel.MessageType == MessageType)
                        {
                           
                                htmlString.Append("<tr class=\"" + ((item.Index % 2 == 1) ? "r0" : "r1") + "\" >");
                                htmlString.Append("<td>");
                                htmlString.Append("<input type=\"checkbox\" id=\"chk3\" value=\"1\" name=\"chk3[]\" class='" + item.PatientMessageModel.MessageId + "'/>");
                                htmlString.Append("</td>");

                                htmlString.Append("<td>");
                               // htmlString.Append("<div style=\"margin-left: 20px;\">");
                                htmlString.Append(Convert.ToDateTime(item.PatientMessageModel.AppointmentStart).ToShortDateString());
                                //htmlString.Append("</div>");
                                htmlString.Append("</td>");
                                htmlString.Append("<td>");
                               // htmlString.Append("<div style=\"margin-left: 20px;\">");
                                htmlString.Append(item.PatientMessageModel.PreferredTime);
                               // htmlString.Append("</div>");
                                htmlString.Append("</td>");

                                htmlString.Append("<td>");
                               // htmlString.Append("<div style=\"margin-left: 30px;\">");
                                htmlString.Append(item.PatientMessageModel.VisitReason);
                                //htmlString.Append("</div>");
                                htmlString.Append("</td>");
                            htmlString.Append("<td>");

                            htmlString.Append(item.PatientMessageModel.FacilityName);
                            
                        htmlString.Append("</td>");
                                htmlString.Append("<td>");
                               // htmlString.Append("<div style=\"margin-left: 80px;\">");
                                htmlString.Append(item.PatientMessageModel.ProviderName_To);
                              //  htmlString.Append("</div>");
                                htmlString.Append("</td>");
                                htmlString.Append("<td>");

                                htmlString.Append(item.PatientMessageModel.MessageStatus);

                                htmlString.Append("</td>");
                             htmlString.Append("<td>");

                             htmlString.Append("<img src=\"Content/img/details.png\" onclick=\"AppointmentDetail('" + item.PatientMessageModel.MessageId +"');\"/>");
                                      
                                   
                                htmlString.Append("<td>");
                                htmlString.Append("</tr>");
                            
                        }
                    }
                    htmlString.Append("</tbody>");
                htmlString.Append("</table>");
            return htmlString.ToString();
        }

        public static string GetAppointmentIndexDataHTML(this List<PatientMessageModel> collection)
        {
            StringBuilder htmlString = new StringBuilder();
            htmlString.Append("<table id=\"box-table-a\" summary=\"AppointmentsGrid\">");
            htmlString.Append("<thead>");
            htmlString.Append("<tr>");
            //if (totRows > 1)
            //{
            //    htmlString.Append("<th scope=\"col\" style=\"margin-right: 10px;\">");
            //    htmlString.Append("<input name=\"check\" type=\"checkbox\" class=\"CheckAll\" onclick=\"checkAll('chk3');\"/>");

            //    htmlString.Append("</th>");
            //}
            htmlString.Append("<th scope=\"col\">");
           // htmlString.Append("<div style=\"margin-left: 20px;\">");
            htmlString.Append("Date");
          //  htmlString.Append("</div>");
            htmlString.Append("</th>");
            htmlString.Append("<th scope=\"col\">");
         //   htmlString.Append("<div style=\"margin-left: 20px;\">");
            htmlString.Append("Time");
         //   htmlString.Append("</div>");
            htmlString.Append("</th>");
            htmlString.Append("<th scope=\"col\">");
          //  htmlString.Append("<div style=\"margin-left: 30px;\">");
            htmlString.Append("Visit Reason");
         //   htmlString.Append("</div>");
            htmlString.Append("</th>");
            htmlString.Append("<th scope=\"col\">");
          //  htmlString.Append("<div style=\"margin-left: 80px;\">");
            htmlString.Append("Location");
         //   htmlString.Append("</div>");
            htmlString.Append("</th>");
            htmlString.Append("<th scope=\"col\">");
         //   htmlString.Append("<div style=\"margin-left: 80px;\">");
            htmlString.Append("Provider");
         //   htmlString.Append("</div>");
            htmlString.Append("</th>");
             htmlString.Append("<th scope=\"col\">");
                                
                                    htmlString.Append("Status");
                                
                            htmlString.Append("</th>");
            htmlString.Append("<th scope=\"col\">");
                               // htmlString.Append("<div style=\"margin-left: 60px; \">");
                                    htmlString.Append("Details");
                           //     htmlString.Append("</div>");
                            htmlString.Append("</th>");
                             htmlString.Append("<th scope=\"col\">");
                           //     htmlString.Append("<div style=\"margin-left: 60px; \">");
                                    htmlString.Append("Cancel");
                              //  htmlString.Append("</div>");
                            htmlString.Append("</th>");

            htmlString.Append("</tr>");
            htmlString.Append("</thead>");
            htmlString.Append("<tbody>");

            foreach (var item in collection.Select((x, i) => new { PatientMessageModel = x, Index = i + 1 }))
            {
                if (item.PatientMessageModel.MessageType == "Appointment Request")
                {
                    //if (item.PatientMessageModel.MessageStatusId != "5")
                    //{
                        StringBuilder jsonStringAppointment = new StringBuilder();

                        jsonStringAppointment.Append(" { ");
                        jsonStringAppointment.Append("\"MessageId\": \"" + item.PatientMessageModel.MessageId + "\",");
                        jsonStringAppointment.Append("\"FacilityId\": \"" + item.PatientMessageModel.FacilityId + "\",");
                        jsonStringAppointment.Append("\"ProviderId_To\": \"" + item.PatientMessageModel.ProviderId_To + "\",");
                        jsonStringAppointment.Append("\"PreferredPeriod\":  \"" + item.PatientMessageModel.PreferredPeriod + "\",");
                        jsonStringAppointment.Append("\"MessageUrgencyId\":  \"" + (item.PatientMessageModel.MessageUrgencyId) + "\",");
                        jsonStringAppointment.Append("\"PreferredWeekDay\":  \"" + item.PatientMessageModel.PreferredWeekDay + "\",");
                        jsonStringAppointment.Append("\"AppointmentStart\":  \"" + item.PatientMessageModel.AppointmentStart + "\",");
                        jsonStringAppointment.Append("\"AppointmentEnd\":  \"" + item.PatientMessageModel.AppointmentEnd + "\",");
                        jsonStringAppointment.Append("\"PreferredTime\":  \"" + item.PatientMessageModel.PreferredTime + "\",");
                        jsonStringAppointment.Append("\"VisitReason\":  \"" + item.PatientMessageModel.VisitReason + "\",");
                        jsonStringAppointment.Append("\"MessageRequest\":  \"" + item.PatientMessageModel.MessageRequest + "\"");
                        jsonStringAppointment.Append("}");

                        htmlString.Append("<tr class=\"" + ((item.Index % 2 == 1) ? "r0" : "r1") + "\">");

                        htmlString.Append("<td><input type=\"hidden\" id=\"hdAppointmentDetails@(item.PatientMessageModel.MessageId)\" value='" + jsonStringAppointment.ToString() + "' />");
                     //   htmlString.Append("<div style=\"margin-left: 20px;\">");
                        htmlString.Append(Convert.ToDateTime(item.PatientMessageModel.AppointmentStart).ToShortDateString());
                      //  htmlString.Append("</div>");
                        htmlString.Append("</td>");
                        htmlString.Append("<td>");
                     //   htmlString.Append("<div style=\"margin-left: 20px;\">");
                        htmlString.Append(item.PatientMessageModel.PreferredTime);
                     //   htmlString.Append("</div>");
                        htmlString.Append("</td>");

                        htmlString.Append("<td>");
                     //   htmlString.Append("<div style=\"margin-left: 30px;\">");
                        htmlString.Append(item.PatientMessageModel.VisitReason);
                     //   htmlString.Append("</div>");
                        htmlString.Append("</td>");
                        htmlString.Append("<td>");
                     //   htmlString.Append("<div style=\"margin-left: 80px;\">");
                        htmlString.Append(item.PatientMessageModel.FacilityName);
                     //   htmlString.Append("</div>");
                        htmlString.Append("</td>");
                        htmlString.Append("<td>");
                      //  htmlString.Append("<div style=\"margin-left: 80px;\">");
                        htmlString.Append(item.PatientMessageModel.ProviderName_To);
                     //   htmlString.Append("</div>");
                        htmlString.Append("</td>");
                     htmlString.Append("<td>");

                     htmlString.Append(item.PatientMessageModel.MessageStatus);
                           
                         htmlString.Append("</td>");
                        htmlString.Append("<td>");
                        htmlString.Append(" <img src=\"Content/img/details.png\" onclick=\"AppointmentDetail('" + item.PatientMessageModel.MessageId + "');\"/>");

                        //htmlString.Append("</div>");
                        htmlString.Append("</td>");
                        htmlString.Append("<td>");
                        htmlString.Append(" <img src=\"Content/img/cancel.png\" onclick=\"AppointmentDelete('" + item.PatientMessageModel.MessageId + "');\"/>");

                    //    htmlString.Append("</div>");
                        htmlString.Append("</td>");
                        htmlString.Append("</tr>");
                    //}
                }
            }
            htmlString.Append("</tbody>");
            htmlString.Append("</table>");
            return htmlString.ToString();
        }

        public static string GetAppointmentRequestDetailHTML(this PatientMessageModel obj)
        {
            StringBuilder htmlString = new StringBuilder();
            // <!-- Reply Part-->

            htmlString.Append("<div id=\"Inform_Head\" class=\"ui-corner-all\">");
            htmlString.Append("<h4 style=\"padding-top: 10px; margin-left: 10px;\">");
            htmlString.Append("<b> " + obj.MessageType + "</b></h4>");
            htmlString.Append("<div style=\"float: right; margin-top: -30px; font-size: 12px; color: #999999;\">");
            htmlString.Append(DateTime.Now.ToString());
            htmlString.Append("</div>");
            htmlString.Append("<div style=\"margin-top: 1px; margin-left: 50px; font-size: 12px; color: #999999;\">");
            htmlString.Append("To:  " + obj.ProviderName_To);
            htmlString.Append("</div>");
            htmlString.Append("<div style=\"margin-top: 1px; margin-left: 90px; font-size: 12px; color: #999999;\">");
            htmlString.Append("From:  " + obj.CreatedByName);
            htmlString.Append("</div>");
            htmlString.Append("</div>");
            //<!--End of reply part-->
            htmlString.Append("<div id=\"reply\" style=\"height: 625px;\" class=\"ui-corner-all\">");
            htmlString.Append("<div style=\"margin-left: 30px;\">");
            htmlString.Append("        <br>");
            htmlString.Append("        <br>");
            htmlString.Append(obj.MessageType);
            htmlString.Append("                        <br>");
            htmlString.Append("Message Request :" + obj.MessageRequest);
            htmlString.Append("                        <br>");
            htmlString.Append("Facility Name :" + obj.FacilityName);
            htmlString.Append("                        <br>");
           
            htmlString.Append("Preffered Period :" + obj.PreferredPeriod);
            htmlString.Append("                        <br>");
            htmlString.Append("Urgent :" + (obj.MessageUrgency? "Yes":"No"));
            htmlString.Append("                        <br>");
            htmlString.Append("Week Day :" + obj.PreferredWeekDay);
            htmlString.Append("                        <br>");
            htmlString.Append("Preffered Time :" + obj.PreferredTime);
            htmlString.Append("                        <br>");
            htmlString.Append("    </div>");
            htmlString.Append("</div>");
            return htmlString.ToString();
        }

        
        public static string GetMedicationRefillDetailHTML(this MessageModel obj)
        {   
            StringBuilder htmlString = new StringBuilder();
            // <!-- Reply Part-->
            htmlString.Append("<div id=\"Inform_Head\" class=\"ui-corner-all\">");
            htmlString.Append("<h4 style=\"padding-top: 10px; margin-left: 10px;\">");
            htmlString.Append("<b>Medication Refill Request</b></h4>");
            htmlString.Append("<div style=\"float: right; margin-top: -30px; font-size: 12px; color: #999999;\">");
            htmlString.Append(DateTime.Now.ToString());
            htmlString.Append("</div>");
            htmlString.Append("<div style=\"margin-top: 1px; margin-left: 50px; font-size: 12px; color: #999999;\">");
            htmlString.Append("To:  " + obj.ProviderId_From.ToString());
            htmlString.Append("</div>");
            htmlString.Append("<div style=\"margin-top: 1px; margin-left: 90px; font-size: 12px; color: #999999;\">");
            htmlString.Append("From:  " + obj.PatientId.ToString());
            htmlString.Append("</div>");
            htmlString.Append("</div>");
            //<!--End of reply part-->
            htmlString.Append("<div id=\"reply\" style=\"height: 625px;\" class=\"ui-corner-all\">");
            htmlString.Append("<div style=\"margin-left: 30px;\">");
            htmlString.Append("        <br>");
            htmlString.Append("        <br>");
            htmlString.Append("Medication Refill Request");
            htmlString.Append("                        <br>");
            htmlString.Append("Patient :" + obj.PatientId.ToString());
            htmlString.Append("                        <br>");
            htmlString.Append("Medication Name :" + obj.MedicationName);
            htmlString.Append("                        <br>");
            htmlString.Append("Provider :  " + obj.ProviderId_From);
            htmlString.Append("                        <br>");
            htmlString.Append("Pharmacy Name :" + obj.PharmacyName);
            htmlString.Append("                        <br>");
            htmlString.Append("Pharmacy Address :" + obj.PharmacyAddress);
            htmlString.Append("                        <br>");
            htmlString.Append("No. Of Refills :" + obj.NoOfRefills.ToString());
            htmlString.Append("                        <br>");
            htmlString.Append("Message Request: " + obj.MessageRequest + " ");
            htmlString.Append("                        <br>");
            htmlString.Append("                        <br>");

            htmlString.Append(" Response                       <br>");
            htmlString.Append("Message Response: " + obj.MessageResponse + " ");
            htmlString.Append("    </div>");
            htmlString.Append("</div>");

            return htmlString.ToString();
        }

        
      public static string GetMessageDetailGridHtml(this List<PatientMessageModel> collection) 
        {
           
            StringBuilder htmlString = new StringBuilder();
           htmlString.Append("<div id=\"MessageDetailGrid\"  name=\"MessageDetailGrid\" style=\"border: 1px solid; border-color: rgb(166, 201, 226);  height: 190px;\" class=\"ui-corner-all\">");
           htmlString.Append("<table class=\"box-table-a\" id=\"tbl-MessageGridDetail\" summary=\"Medications\">");
            htmlString.Append("<thead>");
            htmlString.Append("<tr>");
            //htmlString.Append("<th  scope=\"col\"><div style=\"margin-left:0px;\"></div></th>");

            htmlString.Append("<th  scope=\"col\"> <div style=\"margin-left: 10px;\">From</div></th>");
            htmlString.Append("<th  scope=\"col\"><div style=\"margin-left: 10px;\">To</div></th>");
            htmlString.Append("<th  scope=\"col\"><div style=\"margin-left: 10px;\">Date</div></th>");
            htmlString.Append("<th  scope=\"col\"><div style=\"margin-left: 30px;\">Priority</div></th>");
            htmlString.Append("<th  scope=\"col\"> <div style=\"margin-left: 20px;\">Subject</div></th>");
            htmlString.Append("<th  scope=\"col\"> <div style=\"margin-left: 10px;\">Attachments</div></th>");
            htmlString.Append("<th  scope=\"col\"><div style=\"margin-left:0px;\">Reply</div></th>");
            htmlString.Append("<th  scope=\"col\"><div style=\"margin-left:0px;\"></div></th>");

            //htmlString.Append("<th  scope=\"col\"> <div style=\"margin-left: 20px;\">Detail</div></th>");
            htmlString.Append("</tr>");
            htmlString.Append("</thead>");
            htmlString.Append("<tbody id=\"MsgDetail\">");
            int counter = 0;
            foreach (var item in collection.Select((x, i) => new { PatientMessageModel = x, Index = i + 1 }))
            {
                if (item.PatientMessageModel.MessageType == "Billing Message" || item.PatientMessageModel.MessageType == "General Inquiry")
                {
                    

                    StringBuilder jsonString = new StringBuilder();

                    jsonString.Append(" { ");
                    jsonString.Append("\"ID\": \"" + item.PatientMessageModel.MessageDetailId + "\",");
                    jsonString.Append("\"FacilityName\":  \"" + item.PatientMessageModel.FacilityName + "\",");
                    jsonString.Append("\"From\":  \"" + item.PatientMessageModel.ProviderName_From  + "\",");
                   // jsonString.Append("\"From\":  \"" + ((item.PatientMessageModel.CreatedByName != null && item.PatientMessageModel.CreatedByName.Length > 0) ? item.PatientMessageModel.CreatedByName : item.PatientMessageModel.ProviderName_From) + "\",");
                    jsonString.Append("\"To\":  \"" + item.PatientMessageModel.ProviderName_To + "\",");
                    jsonString.Append("\"MessageType\":  \"" + item.PatientMessageModel.MessageType + "\",");
                    var Resinput = item.PatientMessageModel.MessageResponse;
                    var Resoutput = Resinput.Replace("\'", "&#39;");
                    Resoutput.Replace("\\\"", "&#39;");
                    jsonString.Append("\"MessageResponse\":  \"" + Resoutput + "\",");
                    jsonString.Append("\"MessageId\":  \"" + item.PatientMessageModel.MessageId + "\",");
                    jsonString.Append("\"ProviderIdFrom\":  \"" + item.PatientMessageModel.ProviderId_From + "\",");
                    jsonString.Append("\"ProviderIdTo\":  \"" + item.PatientMessageModel.ProviderId_To + "\",");
                    jsonString.Append("\"MessageTypeId\":  \"" + item.PatientMessageModel.MessageTypeId + "\",");
                    jsonString.Append("\"FacilityId\":  \"" + item.PatientMessageModel.FacilityId + "\",");
                    jsonString.Append("\"MessageUrgencyId\": \"" + item.PatientMessageModel.MessageUrgencyId + "\",");
                    var Reqinput = item.PatientMessageModel.MessageRequest;
                    var Reqoutput = Reqinput.Replace("\'", "&#39;");
                    var reqout2 = Reqoutput.Replace("\\\"", "&#39;");
                    jsonString.Append("\"MessageRequest\": \"" + reqout2 + "\",");
                    jsonString.Append("\"HeaderID\": \"" + item.PatientMessageModel.MessageId + "\"");
                    jsonString.Append("}");

                    string classrow="";
                    if(item.Index % 2 == 1)
                        {
                            classrow = "r0";
                        }
        else{
            classrow = "r1";
    }

                    htmlString.Append("<tr class=\"" + classrow + "\" id=\"MessageDetail" + item.PatientMessageModel.MessageDetailId + "\"  onclick=\"messageInboxDetail(" + item.PatientMessageModel.MessageDetailId + ",'"+classrow+"');\">");

                    
                    if (AMR.Core.Utilities.RequestHelper.MyGlobalVar.PortalType == "Patient")
                    {
                        if (item.PatientMessageModel.MessageRead == false) //&& Convert.ToInt32(item.PatientMessageModel.ProviderId_From) != 0)
                        {
                            htmlString.Append("<td>  <div style=\"margin-left: 10px;width:80px;font-weight:bold;color:orange;\" class=\"unread\">" + item.PatientMessageModel.ProviderName_From + "</div></td>");//From
                            htmlString.Append("<td> <div style=\"margin-left: 10px;width:80px;font-weight:bold;color:orange;\" class=\"unread\">" + item.PatientMessageModel.ProviderName_To + "</div></td>");
                            htmlString.Append("<td> <div style=\"margin-left: 10px;width:170px;font-weight:bold;color:orange;\" class=\"unread\">" + item.PatientMessageModel.DateCreated + "</div></td>");
                            htmlString.Append("<td> <div style=\"margin-left: 30px;width:110px;font-weight:bold;color:orange;\" class=\"unread\">" + (item.PatientMessageModel.MessageUrgency ? "Yes" : "No") + "</div></td>");
                            htmlString.Append("<td> <div style=\"margin-left: 20px;width:110px;font-weight:bold;color:orange;\" class=\"unread\">" + item.PatientMessageModel.MessageType + "</div></td>");//subject
                        }
                        else
                        {
                            htmlString.Append("<td>  <div style=\"margin-left: 10px;width:80px;color:#666699;\">" + item.PatientMessageModel.ProviderName_From + "</div></td>");//From
                            htmlString.Append("<td> <div style=\"margin-left: 10px;width:80px;color:#666699;\">" + item.PatientMessageModel.ProviderName_To + "</div></td>");
                            htmlString.Append("<td> <div style=\"margin-left: 10px;width:170px;color:#666699;\">" + item.PatientMessageModel.DateCreated + "</div></td>");
                            htmlString.Append("<td> <div style=\"margin-left: 30px;width:110px;color:#666699;\">" + (item.PatientMessageModel.MessageUrgency ? "Yes" : "No") + "</div></td>");
                            htmlString.Append("<td> <div style=\"margin-left: 20px;width:110px;color:#666699;\">" + item.PatientMessageModel.MessageType + "</div></td>");//subject
                        }
                    }
                    else {
                        if (item.PatientMessageModel.MessageRead == false )//&& Convert.ToInt32(item.PatientMessageModel.ProviderId_To) != 0)
                        {
                            htmlString.Append("<td>  <div style=\"margin-left: 10px;width:80px;font-weight:bold; color:orange;\" class=\"unread\">" + item.PatientMessageModel.ProviderName_From + "</div></td>");//From
                            htmlString.Append("<td> <div style=\"margin-left: 10px;width:80px;font-weight:bold;color:orange;\" class=\"unread\">" + item.PatientMessageModel.ProviderName_To + "</div></td>");
                            htmlString.Append("<td> <div style=\"margin-left: 10px;width:170px;font-weight:bold;color:orange;\" class=\"unread\">" + item.PatientMessageModel.DateCreated + "</div></td>");
                            htmlString.Append("<td> <div style=\"margin-left: 30px;width:110px;font-weight:bold;color:orange;\" class=\"unread\">" + (item.PatientMessageModel.MessageUrgency ? "Yes" : "No") + "</div></td>");
                            htmlString.Append("<td> <div style=\"margin-left: 20px;width:110px;font-weight:bold;color:orange;\" class=\"unread\">" + item.PatientMessageModel.MessageType + "</div></td>");//subject   }
                        }
                        else
                        {
                            htmlString.Append("<td>  <div style=\"margin-left: 10px;width:80px;color:#666699;\">" + item.PatientMessageModel.ProviderName_From + "</div></td>");//From
                            htmlString.Append("<td> <div style=\"margin-left: 10px;width:80px;color:#666699;\">" + item.PatientMessageModel.ProviderName_To + "</div></td>");
                            htmlString.Append("<td> <div style=\"margin-left: 10px;width:170px;color:#666699;\">" + item.PatientMessageModel.DateCreated + "</div></td>");
                            htmlString.Append("<td> <div style=\"margin-left: 30px;width:110px;color:#666699;\">" + (item.PatientMessageModel.MessageUrgency ? "Yes" : "No") + "</div></td>");
                            htmlString.Append("<td> <div style=\"margin-left: 20px;width:110px;color:#666699;\">" + item.PatientMessageModel.MessageType + "</div></td>");//subject
                        }
                    }
                 
   
                    if (Convert.ToInt64(item.PatientMessageModel.AttachmentId) > 0)
                    {
                       
                        htmlString.Append("<td> <div style=\"margin-left: 50px;width:10px;\"><img src=\"Content/img/attach.gif\" style=\"margin-right: 50px;\" onclick=\"messageAttachmentDetail('" + item.PatientMessageModel.AttachmentId +"|"+item.PatientMessageModel.DocumentFormat + "');\"></div></td>");
                    }
                    else
                    {
                        htmlString.Append("<td> <div style=\"margin-left: 50px;width:10px;\">&nbsp;</div></td>");
                    }

                    if (AMR.Core.Utilities.RequestHelper.MyGlobalVar.PortalType == "Provider")
                    {
                        if (Convert.ToInt64(item.PatientMessageModel.ProviderId_From) == AMR.Core.Utilities.RequestHelper.MyGlobalVar.PatientId) //we will replace 1 with loggedin userid soon, IN GLOBALVAR WE ASSIGNED LOGGED IN PROVIDERID IN CASE OF PROVIDER PORTAL AND pATIENTID IN CASE OF PATIENT PORTAL
                        {
                            htmlString.Append("<td><input id=\"hdInboxDetailJson" + item.PatientMessageModel.MessageDetailId.ToString() + "\" type=\"hidden\" value='" + jsonString.ToString() + "'/> <div style=\"margin-left:0px;width:20px;\">&nbsp;</div></td>");
                        }
                        else
                        {
                            htmlString.Append("<td><input id=\"hdInboxDetailJson" + item.PatientMessageModel.MessageDetailId.ToString() + "\" type=\"hidden\" value='" + jsonString.ToString() + "'/> <div style=\"margin-left:0px;width:20px;\"><img src=\"Content/img/reply.png\" style=\"margin-right: 50px; width:20px; height:20px; \"  onclick=\"messageInboxReply(" + item.PatientMessageModel.MessageDetailId + ");\"></div></td>");
                        }
                    }

                    if (AMR.Core.Utilities.RequestHelper.MyGlobalVar.PortalType == "Patient")
                    {
                        if (item.PatientMessageModel.ProviderId_From == "0") //we will replace 1 with loggedin userid soon, IN GLOBALVAR WE ASSIGNED LOGGED IN PROVIDERID IN CASE OF PROVIDER PORTAL AND pATIENTID IN CASE OF PATIENT PORTAL
                        {
                            htmlString.Append("<td><input id=\"hdInboxDetailJson" + item.PatientMessageModel.MessageDetailId.ToString() + "\" type=\"hidden\" value='" + jsonString.ToString() + "'/> <div style=\"margin-left:0px;width:20px;\">&nbsp;</div></td>");
                        }
                        else
                        {
                            htmlString.Append("<td><input id=\"hdInboxDetailJson" + item.PatientMessageModel.MessageDetailId.ToString() + "\" type=\"hidden\" value='" + jsonString.ToString() + "'/> <div style=\"margin-left:0px;width:20px;\"><img src=\"Content/img/reply.png\" style=\"margin-right: 50px; width:20px; height:20px; \"  onclick=\"messageInboxReply(" + item.PatientMessageModel.MessageDetailId + ");\"></div></td>");
                        }
                    }

                    if (counter == 0)
                    {
                       // htmlString.Append("<td><input id=\"hdFirstDetail\" type=\"hidden\" value='" + item.PatientMessageModel.MessageDetailId + "'/><div style=\"margin-left: 30px;\"><img src=\"Content/img/details.png\" onclick=\"messageInboxDetail(" + item.PatientMessageModel.MessageDetailId + ");\"></div></td>");
                        htmlString.Append("<td class=\"tdhide\"><input id=\"hdFirstDetailInbox\" type=\"hidden\" value='" + item.PatientMessageModel.MessageDetailId + "'/></td>");
                    }
                  else
                   {
                       htmlString.Append("<td></td>");
                    }
                    htmlString.Append("</tr>");
                    counter++;
                }
            }
            htmlString.Append("</tbody>");
            htmlString.Append("</table>");
            htmlString.Append("</div>");
            return htmlString.ToString();

                    }

      public static string GetAppointmentDetailGridHtml(this List<PatientMessageModel> collection)
      {
          StringBuilder htmlString = new StringBuilder();
          htmlString.Append("<div id=\"MessageDetailGrid\" name=\"MessageDetailGrid\" style=\"border: 1px solid; border-color: rgb(166, 201, 226);  height: 190px; \" class=\"ui-corner-all\">");
          htmlString.Append("<table class=\"box-table-a\" id=\"tbl-AppointmentDetailGrid\" summary=\"Medications\">");
          htmlString.Append("<thead>");
          htmlString.Append("<tr>");
         

          htmlString.Append("<th  scope=\"col\"> <div style=\"margin-left: 10px;\">From</div></th>");
          htmlString.Append("<th  scope=\"col\"><div style=\"margin-left: 10px;\">To</div></th>");
          htmlString.Append("<th  scope=\"col\"><div style=\"margin-left: 10px;\">Date</div></th>");
          htmlString.Append("<th  scope=\"col\"><div style=\"margin-left: 30px;\">Priority</div></th>");
          htmlString.Append("<th  scope=\"col\"> <div style=\"margin-left: 20px;\">Subject</div></th>");
          htmlString.Append("<th  scope=\"col\"><div style=\"margin-left:0px;\">Reply</div></th>");
          htmlString.Append("<th  scope=\"col\"><div style=\"margin-left:0px;\"></div></th>");
          //htmlString.Append("<th  scope=\"col\"> <div style=\"margin-left: 20px;\">Detail</div></th>");
          htmlString.Append("</tr>");
          htmlString.Append("</thead>");
          htmlString.Append("<tbody id=\"MsgDetail\">");
          int counter = 0;
          foreach (var item in collection.Select((x, i) => new { PatientMessageModel = x, Index = i + 1 }))
          {
              if (item.PatientMessageModel.MessageType == "Appointment Request")
              {

                  StringBuilder jsonString = new StringBuilder();
                  

                  jsonString.Append(" { ");
                  jsonString.Append("\"ID\": \"" + item.PatientMessageModel.MessageDetailId + "\",");
                  jsonString.Append("\"FacilityName\":  \"" + item.PatientMessageModel.FacilityName + "\",");
                  jsonString.Append("\"From\":  \"" + item.PatientMessageModel.ProviderName_From + "\",");
                  // jsonString.Append("\"From\":  \"" + ((item.PatientMessageModel.CreatedByName != null && item.PatientMessageModel.CreatedByName.Length > 0) ? item.PatientMessageModel.CreatedByName : item.PatientMessageModel.ProviderName_From) + "\",");
                  jsonString.Append("\"To\":  \"" + item.PatientMessageModel.ProviderName_To + "\",");
                  jsonString.Append("\"MessageType\":  \"" + item.PatientMessageModel.MessageType + "\",");
                  var Resinput = item.PatientMessageModel.MessageResponse;
                  var Resoutput = Resinput.Replace("\'", "&#39;");
                  //Resoutput.Replace("\\\"", "&#34;");
                  jsonString.Append("\"MessageResponse\":  \"" + Resoutput + "\",");
                  jsonString.Append("\"MessageId\":  \"" + item.PatientMessageModel.MessageId + "\",");
                  jsonString.Append("\"ProviderIdFrom\":  \"" + item.PatientMessageModel.ProviderId_From + "\",");
                  jsonString.Append("\"ProviderIdTo\":  \"" + item.PatientMessageModel.ProviderId_To + "\",");
                  jsonString.Append("\"MessageTypeId\":  \"" + item.PatientMessageModel.MessageTypeId + "\",");
                  jsonString.Append("\"FacilityId\":  \"" + item.PatientMessageModel.FacilityId + "\",");
                  jsonString.Append("\"MessageUrgencyId\": \"" + item.PatientMessageModel.MessageUrgencyId + "\",");
                  var Reqinput = item.PatientMessageModel.MessageRequest;
                  var Reqoutput = Reqinput.Replace("\'", "&#39;");
                  Reqoutput.Replace("\\\"", "&#39;");
                  jsonString.Append("\"MessageRequest\": \"" + Reqoutput + "\",");
                  jsonString.Append("\"PreferredPeriod\": \"" + item.PatientMessageModel.PreferredPeriod + "\",");
                  jsonString.Append("\"MessageUrgency\": \"" + item.PatientMessageModel.MessageUrgency + "\",");
                  jsonString.Append("\"PreferredWeekDay\": \"" + item.PatientMessageModel.PreferredWeekDay + "\",");
                  jsonString.Append("\"AppointmentStart\": \"" + item.PatientMessageModel.AppointmentStart.ToShortDateString() + "\",");
                  jsonString.Append("\"AppointmentEnd\": \"" + item.PatientMessageModel.AppointmentEnd.ToShortDateString() + "\",");
                  jsonString.Append("\"PreferredTime\": \"" + item.PatientMessageModel.PreferredTime + "\",");
                  var Visinput = item.PatientMessageModel.VisitReason;
                  var Visoutput = Visinput.Replace("\'", "&#39;");
                  Visoutput.Replace("\\\"", "&#39;");
                  jsonString.Append("\"VisitReason\": \"" + Visoutput + "\",");
                  jsonString.Append("\"HeaderID\": \"" + item.PatientMessageModel.MessageId + "\"");
                  jsonString.Append("}");

                  string classrow = "";
                  if (item.Index % 2 == 1)
                  {
                      classrow = "r0";
                  }
                  else
                  {
                      classrow = "r1";
                  }
                  htmlString.Append("<tr class=\"" + classrow + "\" id=\"MessageDetail" + item.PatientMessageModel.MessageDetailId + "\" onclick=\"messageAppointmentDetail(" + item.PatientMessageModel.MessageDetailId + ",'" + classrow + "');\">");

                  //if (AMR.Core.Utilities.RequestHelper.MyGlobalVar.PortalType == "Provider")
                  //{
                  //    if (Convert.ToInt64(item.PatientMessageModel.ProviderId_From) == AMR.Core.Utilities.RequestHelper.MyGlobalVar.PatientId) //we will replace 1 with loggedin userid soon, IN GLOBALVAR WE ASSIGNED LOGGED IN PROVIDERID IN CASE OF PROVIDER PORTAL AND pATIENTID IN CASE OF PATIENT PORTAL
                  //    {
                  //        htmlString.Append("<td><input id=\"hdAppointmentDetailJson" + item.PatientMessageModel.MessageDetailId.ToString() + "\" type=\"hidden\" value='" + jsonString.ToString() + "'/> <div style=\"margin-left:0px;width:20px;\">&nbsp;</div></td>");
                  //    }
                  //    else
                  //    {
                  //        htmlString.Append("<td><input id=\"hdAppointmentDetailJson" + item.PatientMessageModel.MessageDetailId.ToString() + "\" type=\"hidden\" value='" + jsonString.ToString() + "'/> <div style=\"margin-left:0px;width:20px;\"><img src=\"Content/img/reply.png\" style=\"margin-right: 50px; width:20px; height:20px; \"  onclick=\"AppointmentReply(" + item.PatientMessageModel.MessageDetailId + ");\"></div></td>");
                  //    }
                  //}

                  
                  //((item.PatientMessageModel.CreatedByName != null && item.PatientMessageModel.CreatedByName.Length > 0) ? item.PatientMessageModel.CreatedByName : item.PatientMessageModel.ProviderName_From)
                  if (AMR.Core.Utilities.RequestHelper.MyGlobalVar.PortalType == "Patient")
                  {
                      if (item.PatientMessageModel.MessageRead == false )//&& Convert.ToInt32(item.PatientMessageModel.ProviderId_From) != 0)
                      {
                          htmlString.Append("<td>  <div style=\"margin-left: 10px;width:80px;font-weight:bold;color:orange;\" class=\"unread\">" + item.PatientMessageModel.ProviderName_From + "</div></td>");//From
                          htmlString.Append("<td> <div style=\"margin-left: 10px;width:80px;color:orange;font-weight:bold;\" class=\"unread\">" + item.PatientMessageModel.ProviderName_To + "</div></td>");
                          htmlString.Append("<td> <div style=\"margin-left: 10px;width:170px;color:orange;font-weight:bold;\" class=\"unread\">" + item.PatientMessageModel.DateCreated + "</div></td>");
                          htmlString.Append("<td> <div style=\"margin-left: 30px;width:110px;color:orange;font-weight:bold;\" class=\"unread\">" + (item.PatientMessageModel.MessageUrgency ? "Yes" : "No") + "</div></td>");
                          htmlString.Append("<td> <div style=\"margin-left: 20px;width:150px;color:orange;font-weight:bold;\" class=\"unread\">" + item.PatientMessageModel.MessageType + "</div></td>");//subject
                      }
                      else
                      {
                          htmlString.Append("<td>  <div style=\"margin-left: 10px;width:80px; color:#666699\">" + item.PatientMessageModel.ProviderName_From + "</div></td>");//From
                          htmlString.Append("<td> <div style=\"margin-left: 10px;width:80px; color:#666699\">" + item.PatientMessageModel.ProviderName_To + "</div></td>");
                          htmlString.Append("<td> <div style=\"margin-left: 10px;width:170px; color:#666699\">" + item.PatientMessageModel.DateCreated + "</div></td>");
                          htmlString.Append("<td> <div style=\"margin-left: 30px;width:110px; color:#666699\">" + (item.PatientMessageModel.MessageUrgency ? "Yes" : "No") + "</div></td>");
                          htmlString.Append("<td> <div style=\"margin-left: 20px;width:140px; color:#666699\">" + item.PatientMessageModel.MessageType + "</div></td>");//subject
                    
                      }
                  }
                  else
                  {
                      if (item.PatientMessageModel.MessageRead == false )//&& Convert.ToInt32(item.PatientMessageModel.ProviderId_To) != 0)
                      {
                          htmlString.Append("<td>  <div style=\"margin-left: 10px;width:80px;font-weight:bold;color:orange;\" class=\"unread\">" + item.PatientMessageModel.ProviderName_From + "</div></td>");//From
                          htmlString.Append("<td> <div style=\"margin-left: 10px;width:80px;font-weight:bold;color:orange;\" class=\"unread\">" + item.PatientMessageModel.ProviderName_To + "</div></td>");
                          htmlString.Append("<td> <div style=\"margin-left: 10px;width:170px;font-weight:bold;color:orange;\" class=\"unread\">" + item.PatientMessageModel.DateCreated + "</div></td>");
                          htmlString.Append("<td> <div style=\"margin-left: 30px;width:110px;font-weight:bold;color:orange;\" class=\"unread\">" + (item.PatientMessageModel.MessageUrgency ? "Yes" : "No") + "</div></td>");
                          htmlString.Append("<td> <div style=\"margin-left: 20px;width:150px;font-weight:bold;color:orange;\" class=\"unread\">" + item.PatientMessageModel.MessageType + "</div></td>");//subject
                      
                      }
                      else
                      {
                          htmlString.Append("<td>  <div style=\"margin-left: 10px;width:80px; color:#666699\">" + item.PatientMessageModel.ProviderName_From + "</div></td>");//From
                          htmlString.Append("<td> <div style=\"margin-left: 10px;width:80px; color:#666699\">" + item.PatientMessageModel.ProviderName_To + "</div></td>");
                          htmlString.Append("<td> <div style=\"margin-left: 10px;width:170px; color:#666699\">" + item.PatientMessageModel.DateCreated + "</div></td>");
                          htmlString.Append("<td> <div style=\"margin-left: 30px;width:110px; color:#666699\">" + (item.PatientMessageModel.MessageUrgency ? "Yes" : "No") + "</div></td>");
                          htmlString.Append("<td> <div style=\"margin-left: 20px;width:140px; color:#666699\">" + item.PatientMessageModel.MessageType + "</div></td>");//subject
                    
                      }
                  }
                  if (AMR.Core.Utilities.RequestHelper.MyGlobalVar.PortalType == "Provider")
                  {
                      if (Convert.ToInt64(item.PatientMessageModel.ProviderId_From) == AMR.Core.Utilities.RequestHelper.MyGlobalVar.PatientId) //we will replace 1 with loggedin userid soon, IN GLOBALVAR WE ASSIGNED LOGGED IN PROVIDERID IN CASE OF PROVIDER PORTAL AND pATIENTID IN CASE OF PATIENT PORTAL
                      {
                          htmlString.Append("<td><input id=\"hdAppointmentDetailJson" + item.PatientMessageModel.MessageDetailId.ToString() + "\" type=\"hidden\" value='" + jsonString.ToString() + "'/> <div style=\"margin-left:0px;width:20px;\">&nbsp;</div></td>");
                      }
                      else
                      {
                          htmlString.Append("<td><input id=\"hdAppointmentDetailJson" + item.PatientMessageModel.MessageDetailId.ToString() + "\" type=\"hidden\" value='" + jsonString.ToString() + "'/> <div style=\"margin-left:0px;\"><img src=\"Content/img/reply.png\" style=\"margin-right: 50px; width:20px; height:20px; \"  onclick=\"AppointmentReply(" + item.PatientMessageModel.MessageDetailId + ");\"></div></td>");
                      }
                  }
                  if (AMR.Core.Utilities.RequestHelper.MyGlobalVar.PortalType == "Patient")
                  {
                      //  if (item.PatientMessageModel.ProviderId_From == "0") //we will replace 1 with loggedin userid soon, IN GLOBALVAR WE ASSIGNED LOGGED IN PROVIDERID IN CASE OF PROVIDER PORTAL AND pATIENTID IN CASE OF PATIENT PORTAL
                      // {
                      htmlString.Append("<td><input id=\"hdAppointmentDetailJson" + item.PatientMessageModel.MessageDetailId.ToString() + "\" type=\"hidden\" value='" + jsonString.ToString() + "'/> <div style=\"margin-left:0px;\">&nbsp;</div></td>");
                      //  }
                      //else
                      //{
                      //    htmlString.Append("<td><input id=\"hdAppointmentDetailJson" + item.PatientMessageModel.MessageDetailId.ToString() + "\" type=\"hidden\" value='" + jsonString.ToString() + "'/> <div style=\"margin-left:0px;\"><img src=\"Content/img/reply.png\" style=\"margin-right: 50px; width:20px; height:20px; \"  onclick=\"AppointmentReply(" + item.PatientMessageModel.MessageDetailId + ");\"></div></td>");
                      //}
                  }
                  if (counter == 0)
                  {
                 //     htmlString.Append("<td><input id=\"hdFirstDetail\" type=\"hidden\" value='" + item.PatientMessageModel.MessageDetailId + "'/><div style=\"margin-left: 20px;\"><img src=\"Content/img/details.png\" onclick=\"messageAppointmentDetail(" + item.PatientMessageModel.MessageDetailId + ");\"></div></td>");
                      htmlString.Append("<td class=\"tdhide\"><input id=\"hdFirstDetailApp\" type=\"hidden\" value='" + item.PatientMessageModel.MessageDetailId + "'/></td>");
                  }
                  else
                 {
                     htmlString.Append("<td></td>");
                 }
                  htmlString.Append("</tr>");
                  counter++;
              }
          }
          htmlString.Append("</tbody>");
          htmlString.Append("</table>");
          htmlString.Append("</div>");
          return htmlString.ToString();

      }

      public static string GetMedicationRefillDetailGridHtml(this List<PatientMessageModel> collection)
      {
          StringBuilder htmlString = new StringBuilder();
          htmlString.Append("<div id=\"MessageDetailGrid\" name=\"MessageDetailGrid\" style=\"border: 1px solid; border-color: rgb(166, 201, 226);  height: 190px;\" class=\"ui-corner-all\">");
          htmlString.Append("<table class=\"box-table-a\" id=\"tbl-RefillDetailGrid\" summary=\"Medications\">");
          htmlString.Append("<thead>");
          htmlString.Append("<tr>");
          

          htmlString.Append("<th  scope=\"col\"> <div style=\"margin-left: 10px;\">From</div></th>");
          htmlString.Append("<th  scope=\"col\"><div style=\"margin-left: 10px;\">To</div></th>");
          htmlString.Append("<th  scope=\"col\"><div style=\"margin-left: 10px;\">Date</div></th>");
          htmlString.Append("<th  scope=\"col\"><div style=\"margin-left: 30px;\">Priority</div></th>");
          htmlString.Append("<th  scope=\"col\"> <div style=\"margin-left: 20px;\">Subject</div></th>");
          htmlString.Append("<th scope=\"col\"><div style=\"margin-left:0px;\"></div>Reply</th>");
          htmlString.Append("<th scope=\"col\"><div style=\"margin-left:0px;\"></div></th>");

          //htmlString.Append("<th  scope=\"col\"> <div style=\"margin-left: 20px;\">Detail</div></th>");
          htmlString.Append("</tr>");
          htmlString.Append("</thead>");
          htmlString.Append("<tbody id=\"MsgDetail\">");
          int counter = 0;
          foreach (var item in collection.Select((x, i) => new { PatientMessageModel = x, Index = i + 1 }))
          {
              if (item.PatientMessageModel.MessageType == "Medication Refill")
              {

                  StringBuilder jsonString = new StringBuilder();
                  
                  jsonString.Append(" { ");
                  jsonString.Append("\"ID\": \"" + item.PatientMessageModel.MessageDetailId + "\",");
                  jsonString.Append("\"FacilityName\":  \"" + item.PatientMessageModel.FacilityName + "\",");
                  jsonString.Append("\"From\":  \"" + item.PatientMessageModel.ProviderName_From + "\",");
                  // jsonString.Append("\"From\":  \"" + ((item.PatientMessageModel.CreatedByName != null && item.PatientMessageModel.CreatedByName.Length > 0) ? item.PatientMessageModel.CreatedByName : item.PatientMessageModel.ProviderName_From) + "\",");
                  jsonString.Append("\"To\":  \"" + item.PatientMessageModel.ProviderName_To + "\",");
                  jsonString.Append("\"MessageType\":  \"" + item.PatientMessageModel.MessageType + "\",");
                  var Resinput = item.PatientMessageModel.MessageResponse;
                  var Resoutput = Resinput.Replace("\'", "&#39;");
                  Resoutput.Replace("\\\"", "&#39;");
                  jsonString.Append("\"MessageResponse\":  \"" + Resoutput + "\",");
                  jsonString.Append("\"MessageId\":  \"" + item.PatientMessageModel.MessageId + "\",");
                  jsonString.Append("\"ProviderIdFrom\":  \"" + item.PatientMessageModel.ProviderId_From + "\",");
                  jsonString.Append("\"ProviderIdTo\":  \"" + item.PatientMessageModel.ProviderId_To + "\",");
                  jsonString.Append("\"MessageTypeId\":  \"" + item.PatientMessageModel.MessageTypeId + "\",");
                  jsonString.Append("\"FacilityId\":  \"" + item.PatientMessageModel.FacilityId + "\",");
                  jsonString.Append("\"MessageUrgencyId\": \"" + item.PatientMessageModel.MessageUrgencyId + "\",");
                  var Reqinput = item.PatientMessageModel.MessageRequest;
                  var Reqoutput = Reqinput.Replace("\'", "&#39;");
                  Reqoutput.Replace("\\\"", "&#39;");
                  jsonString.Append("\"MessageRequest\": \"" + Reqoutput + "\",");
                  jsonString.Append("\"PreferredPeriod\": \"" + item.PatientMessageModel.PreferredPeriod + "\",");
                  jsonString.Append("\"MessageUrgency\": \"" + item.PatientMessageModel.MessageUrgency + "\",");
                  jsonString.Append("\"PreferredWeekDay\": \"" + item.PatientMessageModel.PreferredWeekDay + "\",");
                  jsonString.Append("\"AppointmentStart\": \"" + item.PatientMessageModel.AppointmentStart + "\",");
                  jsonString.Append("\"PreferredTime\": \"" + item.PatientMessageModel.PreferredTime + "\",");
                  jsonString.Append("\"CreatedByName\": \"" + item.PatientMessageModel.CreatedByName + "\",");
                  jsonString.Append("\"MedicationName\": \"" + item.PatientMessageModel.MedicationName + "\",");
                  jsonString.Append("\"PharmacyName\": \"" + item.PatientMessageModel.PharmacyName + "\",");
                  jsonString.Append("\"PharmacyAddress\": \"" + item.PatientMessageModel.PharmacyAddress + "\",");
                  jsonString.Append("\"NoOfRefills\": \"" + item.PatientMessageModel.NoOfRefills.ToString() + "\",");
                  var Visinput = item.PatientMessageModel.VisitReason;
                  var Visoutput = Visinput.Replace("\\\"", "&#39;");
                  jsonString.Append("\"VisitReason\": \"" + Visoutput + "\",");
                  jsonString.Append("\"PharmacyPhone\": \"" + item.PatientMessageModel.PharmacyPhone + "\",");
                  jsonString.Append("\"HeaderID\": \"" + item.PatientMessageModel.MessageId + "\"");
                  jsonString.Append("}");

                  string classrow = "";
                  if (item.Index % 2 == 1)
                  {
                      classrow = "r0";
                  }
                  else
                  {
                      classrow = "r1";
                  }
                  htmlString.Append("<tr class=\"" + classrow + "\" id=\"MessageDetail" + item.PatientMessageModel.MessageDetailId + "\" onclick=\"messageRequestRefillDetail(" + item.PatientMessageModel.MessageDetailId + ",'" + classrow + "');\">");

                  
                  //((item.PatientMessageModel.CreatedByName != null && item.PatientMessageModel.CreatedByName.Length > 0) ? item.PatientMessageModel.CreatedByName : item.PatientMessageModel.ProviderName_From)
                  if (AMR.Core.Utilities.RequestHelper.MyGlobalVar.PortalType == "Patient")
                  {
                      if (item.PatientMessageModel.MessageRead == false )//&& Convert.ToInt32(item.PatientMessageModel.ProviderId_From) != 0)
                      {
                          htmlString.Append("<td>  <div style=\"margin-left: 10px;width:80px;color:orange;font-weight:bold;\" class=\"unread\">" + item.PatientMessageModel.ProviderName_From + "</div></td>");//From
                          htmlString.Append("<td> <div style=\"margin-left: 10px;width:80px;color:orange;font-weight:bold;\" class=\"unread\">" + item.PatientMessageModel.ProviderName_To + "</div></td>");
                          htmlString.Append("<td> <div style=\"margin-left: 10px;width:170px;color:orange;font-weight:bold;\" class=\"unread\">" + item.PatientMessageModel.DateCreated + "</div></td>");
                          htmlString.Append("<td> <div style=\"margin-left: 30px;width:110px;color:orange;font-weight:bold;\" class=\"unread\">" + (item.PatientMessageModel.MessageUrgency ? "Yes" : "No") + "</div></td>");
                          htmlString.Append("<td> <div style=\"margin-left: 20px;width:150px;color:orange;font-weight:bold;\" class=\"unread\">" + item.PatientMessageModel.MessageType + "</div></td>");//subject
                      }
                      else
                      {
                          htmlString.Append("<td>  <div style=\"margin-left: 10px;width:80px; color:#666699\">" + item.PatientMessageModel.ProviderName_From + "</div></td>");//From
                          htmlString.Append("<td> <div style=\"margin-left: 10px;width:80px; color:#666699\">" + item.PatientMessageModel.ProviderName_To + "</div></td>");
                          htmlString.Append("<td> <div style=\"margin-left: 10px;width:170px; color:#666699\">" + item.PatientMessageModel.DateCreated + "</div></td>");
                          htmlString.Append("<td> <div style=\"margin-left: 30px;width:110px; color:#666699\">" + (item.PatientMessageModel.MessageUrgency ? "Yes" : "No") + "</div></td>");
                          htmlString.Append("<td> <div style=\"margin-left: 20px;width:140px; color:#666699\">" + item.PatientMessageModel.MessageType + "</div></td>");//subject
                      }
                  }
                  else
                  {
                      if (item.PatientMessageModel.MessageRead == false )//&& Convert.ToInt32(item.PatientMessageModel.ProviderId_To) != 0)
                      {
                          htmlString.Append("<td>  <div style=\"margin-left: 10px;width:80px;font-weight:bold;color:orange;\" class=\"unread\">" + item.PatientMessageModel.ProviderName_From + "</div></td>");//From
                          htmlString.Append("<td> <div style=\"margin-left: 10px;width:80px;font-weight:bold;color:orange;\" class=\"unread\">" + item.PatientMessageModel.ProviderName_To + "</div></td>");
                          htmlString.Append("<td> <div style=\"margin-left: 10px;width:170px;font-weight:bold;color:orange;\" class=\"unread\">" + item.PatientMessageModel.DateCreated + "</div></td>");
                          htmlString.Append("<td> <div style=\"margin-left: 30px;width:110px;font-weight:bold;color:orange;\" class=\"unread\">" + (item.PatientMessageModel.MessageUrgency ? "Yes" : "No") + "</div></td>");
                          htmlString.Append("<td> <div style=\"margin-left: 20px;width:150px;font-weight:bold;color:orange;\" class=\"unread\">" + item.PatientMessageModel.MessageType + "</div></td>");//subject
                      }
                      else
                      {
                          htmlString.Append("<td>  <div style=\"margin-left: 10px;width:80px; color:#666699\">" + item.PatientMessageModel.ProviderName_From + "</div></td>");//From
                          htmlString.Append("<td> <div style=\"margin-left: 10px;width:80px; color:#666699\">" + item.PatientMessageModel.ProviderName_To + "</div></td>");
                          htmlString.Append("<td> <div style=\"margin-left: 10px;width:170px; color:#666699\">" + item.PatientMessageModel.DateCreated + "</div></td>");
                          htmlString.Append("<td> <div style=\"margin-left: 30px;width:110px; color:#666699\">" + (item.PatientMessageModel.MessageUrgency ? "Yes" : "No") + "</div></td>");
                          htmlString.Append("<td> <div style=\"margin-left: 20px;width:140px; color:#666699\">" + item.PatientMessageModel.MessageType + "</div></td>");//subject
                      }
                  }
                  if (AMR.Core.Utilities.RequestHelper.MyGlobalVar.PortalType == "Provider")
                  {
                      if (Convert.ToInt64(item.PatientMessageModel.ProviderId_From) == AMR.Core.Utilities.RequestHelper.MyGlobalVar.PatientId) //we will replace 1 with loggedin userid soon, IN GLOBALVAR WE ASSIGNED LOGGED IN PROVIDERID IN CASE OF PROVIDER PORTAL AND pATIENTID IN CASE OF PATIENT PORTAL
                      {
                          htmlString.Append("<td><input id=\"hdRequestRefillDetailJson" + item.PatientMessageModel.MessageDetailId.ToString() + "\" type=\"hidden\" value='" + jsonString.ToString() + "'/> <div style=\"margin-left:0px;width:20px;\">&nbsp;</div></td>");
                      }
                      else
                      {
                          htmlString.Append("<td><input id=\"hdRequestRefillDetailJson" + item.PatientMessageModel.MessageDetailId.ToString() + "\" type=\"hidden\" value='" + jsonString.ToString() + "'/> <div style=\"margin-left:0px;width:20px;\"><img src=\"Content/img/reply.png\" style=\"margin-right: 50px; width:20px; height:20px; \"  onclick=\"RefillReply(" + item.PatientMessageModel.MessageDetailId + ");\"></div></td>");
                      }
                  }

                  if (AMR.Core.Utilities.RequestHelper.MyGlobalVar.PortalType == "Patient")
                  {
                      //  if (item.PatientMessageModel.ProviderId_From == "0") //we will replace 1 with loggedin userid soon, IN GLOBALVAR WE ASSIGNED LOGGED IN PROVIDERID IN CASE OF PROVIDER PORTAL AND pATIENTID IN CASE OF PATIENT PORTAL
                      // {
                      htmlString.Append("<td><input id=\"hdRequestRefillDetailJson" + item.PatientMessageModel.MessageDetailId.ToString() + "\" type=\"hidden\" value='" + jsonString.ToString() + "'/> <div style=\"margin-left:0px;width:20px;\">&nbsp;</div></td>");
                      //  }
                      //else
                      //{
                      //    htmlString.Append("<td><input id=\"hdAppointmentDetailJson" + item.PatientMessageModel.MessageDetailId.ToString() + "\" type=\"hidden\" value='" + jsonString.ToString() + "'/> <div style=\"margin-left:0px;\"><img src=\"Content/img/reply.png\" style=\"margin-right: 50px; width:20px; height:20px; \"  onclick=\"AppointmentReply(" + item.PatientMessageModel.MessageDetailId + ");\"></div></td>");
                      //}
                  }
                  if (counter == 0)
                  {
                 //     htmlString.Append("<td><input id=\"hdFirstDetail\" type=\"hidden\" value='" + item.PatientMessageModel.MessageDetailId + "'/><div style=\"margin-left: 20px;\"><img src=\"Content/img/details.png\" onclick=\"messageRequestRefillDetail(" + item.PatientMessageModel.MessageDetailId + ");\"></div></td>");
                      htmlString.Append("<td class=\"tdhide\"><input id=\"hdFirstDetailMed\" type=\"hidden\" value='" + item.PatientMessageModel.MessageDetailId + "'/></td>");
                  }
                 else {
                      htmlString.Append("<td></td>");

                 }
                  htmlString.Append("</tr>");
              }
          }
          htmlString.Append("</tbody>");
          htmlString.Append("</table>");
          htmlString.Append("</div>");
          return htmlString.ToString();

      }

      public static string GetReferralRequestDetailGridHtml(this List<PatientMessageModel> collection)
      {
          StringBuilder htmlString = new StringBuilder();
          htmlString.Append("<div id=\"MessageDetailGrid\" name=\"MessageDetailGrid\" style=\"border: 1px solid; border-color: rgb(166, 201, 226);  height: 190px;\" class=\"ui-corner-all\">");
          htmlString.Append("<table class=\"box-table-a\" id=\"tbl-ReferralDetailGrid\"summary=\"Medications\">");
          htmlString.Append("<thead>");
          htmlString.Append("<tr>");
          

          htmlString.Append("<th  scope=\"col\"> <div style=\"margin-left: 10px;\">From</div></th>");
          htmlString.Append("<th  scope=\"col\"><div style=\"margin-left: 10px;\">To</div></th>");
          htmlString.Append("<th  scope=\"col\"><div style=\"margin-left: 10px;\">Date</div></th>");
          htmlString.Append("<th  scope=\"col\"><div style=\"margin-left: 30px;\">Priority</div></th>");
          htmlString.Append("<th  scope=\"col\"> <div style=\"margin-left: 20px;\">Subject</div></th>");
          htmlString.Append("<th scope=\"col\"><div style=\"margin-left:0px;\">Reply</div></th>");
          htmlString.Append("<th scope=\"col\"></th>");
          //htmlString.Append("<th  scope=\"col\"> <div style=\"margin-left: 20px;\">Detail</div></th>");
          htmlString.Append("</tr>");
          htmlString.Append("</thead>");
          htmlString.Append("<tbody id=\"MsgDetail\">");
          int counter = 0;
          foreach (var item in collection.Select((x, i) => new { PatientMessageModel = x, Index = i + 1 }))
          {
              if (item.PatientMessageModel.MessageType == "Referral Message")
              {

                  StringBuilder jsonString = new StringBuilder();
                 
                  jsonString.Append(" { ");
                  jsonString.Append("\"ID\": \"" + item.PatientMessageModel.MessageDetailId + "\",");
                  jsonString.Append("\"FacilityName\":  \"" + item.PatientMessageModel.FacilityName + "\",");
                  jsonString.Append("\"From\":  \"" + item.PatientMessageModel.ProviderName_From + "\",");
                  // jsonString.Append("\"From\":  \"" + ((item.PatientMessageModel.CreatedByName != null && item.PatientMessageModel.CreatedByName.Length > 0) ? item.PatientMessageModel.CreatedByName : item.PatientMessageModel.ProviderName_From) + "\",");
                  jsonString.Append("\"To\":  \"" + item.PatientMessageModel.ProviderName_To + "\",");
                  jsonString.Append("\"MessageType\":  \"" + item.PatientMessageModel.MessageType + "\",");
                  var Resinput = item.PatientMessageModel.MessageResponse;
                  var Resoutput = Resinput.Replace("\'", "&#39;");
                  Resoutput.Replace("\\\"", "&#39;");
                  jsonString.Append("\"MessageResponse\":  \"" + Resoutput + "\",");
                  jsonString.Append("\"MessageId\":  \"" + item.PatientMessageModel.MessageId + "\",");
                  jsonString.Append("\"ProviderIdFrom\":  \"" + item.PatientMessageModel.ProviderId_From + "\",");
                  jsonString.Append("\"ProviderIdTo\":  \"" + item.PatientMessageModel.ProviderId_To + "\",");
                  jsonString.Append("\"MessageTypeId\":  \"" + item.PatientMessageModel.MessageTypeId + "\",");
                  jsonString.Append("\"FacilityId\":  \"" + item.PatientMessageModel.FacilityId + "\",");
                  jsonString.Append("\"MessageUrgencyId\": \"" + item.PatientMessageModel.MessageUrgencyId + "\",");
                  var Reqinput = item.PatientMessageModel.MessageRequest;
                  var Reqoutput = Reqinput.Replace("\'", "&#39;");
                  Reqoutput.Replace("\\\"", "&#39;");
                  jsonString.Append("\"MessageRequest\": \"" + Reqoutput + "\",");
                  jsonString.Append("\"PreferredPeriod\": \"" + item.PatientMessageModel.PreferredPeriod + "\",");
                  jsonString.Append("\"MessageUrgency\": \"" + item.PatientMessageModel.MessageUrgency + "\",");
                  jsonString.Append("\"PreferredWeekDay\": \"" + item.PatientMessageModel.PreferredWeekDay + "\",");
                  jsonString.Append("\"AppointmentStart\": \"" + item.PatientMessageModel.AppointmentStart + "\",");
                  jsonString.Append("\"PreferredTime\": \"" + item.PatientMessageModel.PreferredTime + "\",");
                  jsonString.Append("\"CreatedByName\": \"" + item.PatientMessageModel.CreatedByName + "\",");
                  jsonString.Append("\"MedicationName\": \"" + item.PatientMessageModel.MedicationName + "\",");
                  jsonString.Append("\"PharmacyName\": \"" + item.PatientMessageModel.PharmacyName + "\",");
                  jsonString.Append("\"PharmacyAddress\": \"" + item.PatientMessageModel.PharmacyAddress + "\",");
                  jsonString.Append("\"NoOfRefills\": \"" + item.PatientMessageModel.NoOfRefills.ToString() + "\",");
                  var Visinput = item.PatientMessageModel.VisitReason;
                  var Visoutput = Visinput.Replace("\'", "&#39;");
                  Visoutput.Replace("\\\"", "&#39;");
                  jsonString.Append("\"VisitReason\": \"" + Visoutput + "\",");
                  jsonString.Append("\"HeaderID\": \"" + item.PatientMessageModel.MessageId + "\"");
                  jsonString.Append("}");

                  string classrow = "";
                  if (item.Index % 2 == 1)
                  {
                      classrow = "r0";
                  }
                  else
                  {
                      classrow = "r1";
                  }
                  htmlString.Append("<tr class=\"" + classrow + "\" id=\"MessageDetail" + item.PatientMessageModel.MessageDetailId + "\" onclick=\"messageReferralRequestDetail(" + item.PatientMessageModel.MessageDetailId + ",'" + classrow + "');\">");


                  
                  //((item.PatientMessageModel.CreatedByName != null && item.PatientMessageModel.CreatedByName.Length > 0) ? item.PatientMessageModel.CreatedByName : item.PatientMessageModel.ProviderName_From)
                  if (AMR.Core.Utilities.RequestHelper.MyGlobalVar.PortalType == "Patient")
                  {
                      if (item.PatientMessageModel.MessageRead == false )//&& Convert.ToInt32(item.PatientMessageModel.ProviderId_From) != 0)
                      {
                          htmlString.Append("<td>  <div style=\"margin-left: 10px;width:80px;color:orange;font-weight:bold;\" class=\"unread\">" + item.PatientMessageModel.ProviderName_From + "</div></td>");//From
                          htmlString.Append("<td> <div style=\"margin-left: 10px;width:80px;color:orange;font-weight:bold;\" class=\"unread\">" + item.PatientMessageModel.ProviderName_To + "</div></td>");
                          htmlString.Append("<td> <div style=\"margin-left: 10px;width:170px;color:orange;font-weight:bold;\" class=\"unread\">" + item.PatientMessageModel.DateCreated + "</div></td>");
                          htmlString.Append("<td> <div style=\"margin-left: 30px;width:110px;color:orange;font-weight:bold;\" class=\"unread\">" + (item.PatientMessageModel.MessageUrgency ? "Yes" : "No") + "</div></td>");
                          htmlString.Append("<td> <div style=\"margin-left: 20px;width:150px;color:orange;font-weight:bold;\" class=\"unread\">" + item.PatientMessageModel.MessageType + "</div></td>");//subject
                      }
                      else
                      {
                          htmlString.Append("<td>  <div style=\"margin-left: 10px;width:80px; color:#666699\">" + item.PatientMessageModel.ProviderName_From + "</div></td>");//From
                          htmlString.Append("<td> <div style=\"margin-left: 10px;width:80px; color:#666699\">" + item.PatientMessageModel.ProviderName_To + "</div></td>");
                          htmlString.Append("<td> <div style=\"margin-left: 10px;width:170px; color:#666699\">" + item.PatientMessageModel.DateCreated + "</div></td>");
                          htmlString.Append("<td> <div style=\"margin-left: 30px;width:110px; color:#666699\">" + (item.PatientMessageModel.MessageUrgency ? "Yes" : "No") + "</div></td>");
                          htmlString.Append("<td> <div style=\"margin-left: 20px;width:140px; color:#666699\">" + item.PatientMessageModel.MessageType + "</div></td>");//subject
                      }
                  }
                  else
                  {
                      if (item.PatientMessageModel.MessageRead == false)// && Convert.ToInt32(item.PatientMessageModel.ProviderId_To) != 0)
                      {
                          htmlString.Append("<td>  <div style=\"margin-left: 10px;width:80px;font-weight:bold;color:orange;\" class=\"unread\">" + item.PatientMessageModel.ProviderName_From + "</div></td>");//From
                          htmlString.Append("<td> <div style=\"margin-left: 10px;width:80px;font-weight:bold;color:orange;\" class=\"unread\">" + item.PatientMessageModel.ProviderName_To + "</div></td>");
                          htmlString.Append("<td> <div style=\"margin-left: 10px;width:170px;font-weight:bold;color:orange;\" class=\"unread\">" + item.PatientMessageModel.DateCreated + "</div></td>");
                          htmlString.Append("<td> <div style=\"margin-left: 30px;width:110px;font-weight:bold;color:orange;\" class=\"unread\">" + (item.PatientMessageModel.MessageUrgency ? "Yes" : "No") + "</div></td>");
                          htmlString.Append("<td> <div style=\"margin-left: 20px;width:150px;font-weight:bold;color:orange;\" class=\"unread\">" + item.PatientMessageModel.MessageType + "</div></td>");//subject
                      }
                      else
                      {
                          htmlString.Append("<td>  <div style=\"margin-left: 10px;width:80px; color:#666699\">" + item.PatientMessageModel.ProviderName_From + "</div></td>");//From
                          htmlString.Append("<td> <div style=\"margin-left: 10px;width:80px; color:#666699\">" + item.PatientMessageModel.ProviderName_To + "</div></td>");
                          htmlString.Append("<td> <div style=\"margin-left: 10px;width:170px; color:#666699\">" + item.PatientMessageModel.DateCreated + "</div></td>");
                          htmlString.Append("<td> <div style=\"margin-left: 30px;width:110px; color:#666699\">" + (item.PatientMessageModel.MessageUrgency ? "Yes" : "No") + "</div></td>");
                          htmlString.Append("<td> <div style=\"margin-left: 20px;width:140px; color:#666699\">" + item.PatientMessageModel.MessageType + "</div></td>");//subject
                      }
                  }
                  if (AMR.Core.Utilities.RequestHelper.MyGlobalVar.PortalType == "Provider")
                  {
                      if (Convert.ToInt64(item.PatientMessageModel.ProviderId_From) == AMR.Core.Utilities.RequestHelper.MyGlobalVar.PatientId) //we will replace 1 with loggedin userid soon, IN GLOBALVAR WE ASSIGNED LOGGED IN PROVIDERID IN CASE OF PROVIDER PORTAL AND pATIENTID IN CASE OF PATIENT PORTAL
                      {
                          htmlString.Append("<td><input id=\"hdReferralRequestDetailJson" + item.PatientMessageModel.MessageDetailId.ToString() + "\" type=\"hidden\" value='" + jsonString.ToString() + "'/> <div style=\"margin-left:0px;width:20px;\">&nbsp;</div></td>");
                      }
                      else
                      {
                          htmlString.Append("<td><input id=\"hdReferralRequestDetailJson" + item.PatientMessageModel.MessageDetailId.ToString() + "\" type=\"hidden\" value='" + jsonString.ToString() + "'/> <div style=\"margin-left:0px;width:20px;\"><img src=\"Content/img/reply.png\" style=\"margin-right: 50px; width:20px; height:20px; \"  onclick=\"ReferralReply(" + item.PatientMessageModel.MessageDetailId + ");\"></div></td>");
                      }
                  }

                  if (AMR.Core.Utilities.RequestHelper.MyGlobalVar.PortalType == "Patient")
                  {
                      //  if (item.PatientMessageModel.ProviderId_From == "0") //we will replace 1 with loggedin userid soon, IN GLOBALVAR WE ASSIGNED LOGGED IN PROVIDERID IN CASE OF PROVIDER PORTAL AND pATIENTID IN CASE OF PATIENT PORTAL
                      // {
                      htmlString.Append("<td><input id=\"hdReferralRequestDetailJson" + item.PatientMessageModel.MessageDetailId.ToString() + "\" type=\"hidden\" value='" + jsonString.ToString() + "'/> <div style=\"margin-left:0px;width:20px;\">&nbsp;</div></td>");
                      //  }
                      //else
                      //{
                      //    htmlString.Append("<td><input id=\"hdAppointmentDetailJson" + item.PatientMessageModel.MessageDetailId.ToString() + "\" type=\"hidden\" value='" + jsonString.ToString() + "'/> <div style=\"margin-left:0px;\"><img src=\"Content/img/reply.png\" style=\"margin-right: 50px; width:20px; height:20px; \"  onclick=\"AppointmentReply(" + item.PatientMessageModel.MessageDetailId + ");\"></div></td>");
                      //}
                  }
                  if (counter == 0)
                  {
                      //htmlString.Append("<td><input id=\"hdFirstDetail\" type=\"hidden\" value='" + item.PatientMessageModel.MessageDetailId + "'/><div style=\"margin-left: 20px;\"><img src=\"Content/img/details.png\" onclick=\"messageReferralRequestDetail(" + item.PatientMessageModel.MessageDetailId + ");\"></div></td>");
                      htmlString.Append("<td class=\"tdhide\"><input id=\"hdFirstDetailReferral\" type=\"hidden\" value='" + item.PatientMessageModel.MessageDetailId + "'/></td>");
                  }
               else {
                      htmlString.Append("<td></td>");
                  }
                  htmlString.Append("</tr>");
                  counter++;
              }
          }
          htmlString.Append("</tbody>");
          htmlString.Append("</table>");
          htmlString.Append("</div>");
          return htmlString.ToString();

      }
     //   
        public static string GetMessageMasterGridHtml(this List<PatientMessageModel> collection)
        {
            StringBuilder htmlString = new StringBuilder();
            htmlString.Append("<table id=\"box-table-a\" summary=\"Message\">");
            htmlString.Append("<thead>");
            htmlString.Append("<tr>");
            htmlString.Append("<th scope=\"col\" style=\"margin-right: 10px;\"><input name=\"check\" type=\"checkbox\" class=\" CheckAll\" onclick=\"checkAll('chk');\" /></th>");
            htmlString.Append("<th scope=\"col\"> <div style=\"margin-left: 20px;\"><img src=\"Content/img/i.png\" /></div></th>");
            htmlString.Append("<th scope=\"col\"> <div style=\"margin-left: 20px;\">From</div></th>");
            htmlString.Append("<th scope=\"col\"><div style=\"margin-left: 30px;\">To</div></th>");
            htmlString.Append("<th scope=\"col\"><div style=\"margin-left: 80px;\">Priority</div></th>");
            htmlString.Append("<th scope=\"col\"> <div style=\"margin-left: 60px;\">Subject</div></th>");
            htmlString.Append("<th scope=\"col\"><div style=\"margin-left: 60px;\">Detail</div> </th>");
            htmlString.Append("</tr>");
            htmlString.Append("</thead>");
            htmlString.Append("<tbody>");
            
            foreach (var item in collection.Select((x, z) => new { PatientMessageModel = x, Index1 = z + 1 }))
            {
                if (item.PatientMessageModel.MessageType == "Billing Message" || item.PatientMessageModel.MessageType == "General Inquiry")
            {

                if (item.PatientMessageModel.MessageStatusId != "7")
                {

                    htmlString.Append("<tr class=\"" + ((item.Index1 % 2 == 1) ? "r1" : "r0") + "\"  >");
                    htmlString.Append("<td><input type=\"checkbox\" id=\"chk\" value=\"1\" name=\"chk[]\" /></td>");
                    htmlString.Append("<td>" + "" + "</td>");
                     if (item.PatientMessageModel.ProviderId_From == "0")
                                       {
                                     htmlString.Append("<td>  <div style=\"margin-left: 20px;\">" + item.PatientMessageModel.CreatedByName + "</div></td>");
                                       }
                                       else
                                       {
                          htmlString.Append("<td>  <div style=\"margin-left: 20px;\">" + item.PatientMessageModel.ProviderName_From + "</div></td>");

                                       }; //from
                      if (item.PatientMessageModel.ProviderId_To == "0")
                                        {
                          htmlString.Append("<td> <div style=\"margin-left: 30px;\">" + item.PatientMessageModel.CreatedByName + "</div></td>");
                                      
                                        }
                      else{
                                    htmlString.Append("<td> <div style=\"margin-left: 30px;\">" + item.PatientMessageModel.ProviderName_To + "</div></td>");

                      };//to
                    htmlString.Append("<td> <div style=\"margin-left: 80px;\">" + item.PatientMessageModel.MessageUrgency + "</div></td>");//priority
                    htmlString.Append("<td> <div style=\"margin-left: 60px;\">" + item.PatientMessageModel.MessageType + "</div></td>");//subject
                    htmlString.Append("<td> <div style=\"margin-left: 62px;\"> <img src=\"Content/img/details.png\"    onclick=\"inboxDetail(" + item.PatientMessageModel.MessageId + ",'I');\"/>  </div></td>");

                    htmlString.Append("</tr>");
                }
            }
            }
            htmlString.Append("</tbody>");
            htmlString.Append("</table>");

            return htmlString.ToString();

                    }


        public static string GetSentDetailHTML(this PatientMessageModel obj)
        {
            StringBuilder htmlString = new StringBuilder();
            // <!-- Reply Part-->
            htmlString.Append("<div id=\"Inform_Head\" class=\"ui-corner-all\">");
            htmlString.Append("<h4 style=\"padding-top: 10px; margin-left: 10px;\">");
            htmlString.Append("<b>"+ obj.MessageType +"</b></h4>");
            htmlString.Append("<div style=\"float: right; margin-top: -30px; font-size: 12px; color: #999999;\">");
            htmlString.Append(DateTime.Now.ToString());
            htmlString.Append("</div>");
            htmlString.Append("<div style=\"margin-top: 1px; margin-left: 50px; font-size: 12px; color: #999999;\">");
            htmlString.Append("To:  " + obj.ProviderName_To);
            htmlString.Append("</div>");
            htmlString.Append("<div style=\"margin-top: 1px; margin-left: 90px; font-size: 12px; color: #999999;\">");
            htmlString.Append("From:  " + obj.CreatedByName);
            htmlString.Append("</div>");
            htmlString.Append("</div>");
            //<!--End of reply part-->
            htmlString.Append("<div id=\"reply\" style=\"height: 625px;\" class=\"ui-corner-all\">");
            htmlString.Append("<div style=\"margin-left: 30px;\">");
            htmlString.Append("        <br>");
            htmlString.Append("        <br>");
            
            htmlString.Append("                        <br>");
            htmlString.Append("Facilty Name :" + obj.FacilityName);
            

            htmlString.Append("         <br>");
            if (obj.MessageRequest != "") {
             htmlString.Append("Message:" + obj.MessageRequest);
                }
                else {
                  htmlString.Append("Message:" + obj.MessageResponse);
                }
            htmlString.Append("    </div>");
            htmlString.Append("</div>");

            return htmlString.ToString();
        }




        
        public static string GetPatientMedicationModelListHTMLForDashboard(this  List<PatientMedicationModel> collection)
        {
            StringBuilder htmlString = new StringBuilder();

            htmlString.Append("<table id=\"box-table-a\" summary=\"Medications\">");
            htmlString.Append("<thead>");
            htmlString.Append("<tr>");
            htmlString.Append("<th scope=\"col\">Name</th>");
            htmlString.Append("<th scope=\"col\">Expire Date</th>");
            htmlString.Append("<th scope=\"col\">Pharmacy</th>");
            htmlString.Append("<th scope=\"col\">Source</th>");
            htmlString.Append("</tr>");
            htmlString.Append("</thead>");
            htmlString.Append("<tbody>");
            foreach (var item in collection.Select((x, i) => new { PatientMedicationModel = x, Index = i + 1 }))
            {
                htmlString.Append("<tr class=\"" + ((item.Index % 2 == 1) ? "r0" : "r1") + "\">");
                htmlString.Append("<td>" + item.PatientMedicationModel.MedicationName + "</td>");
                htmlString.Append("<td>" + item.PatientMedicationModel.ExpireDate + "</td>");
                htmlString.Append("<td>" + item.PatientMedicationModel.Pharmacy + "</td>");
                htmlString.Append("<td>" + "" + "</td>");

                htmlString.Append("</tr>");
            }
            htmlString.Append("</tbody>");
            htmlString.Append("</table>");

            return htmlString.ToString();
        }

        public static string GetAppointmentListHTMLForDashboard(this List<PatientMessageModel> collection)
        {
            StringBuilder htmlString = new StringBuilder();

             htmlString.Append("<table id=\"box-table-a\" summary=\"Appointments\">");
                    htmlString.Append("<thead>");
                        htmlString.Append("<tr>");
                            htmlString.Append("<th scope=\"col\">Date</th>");
                            htmlString.Append("<th scope=\"col\">Time</th>");
                            htmlString.Append("<th scope=\"col\">Visit Reason</th>");
                            htmlString.Append("<th scope=\"col\">Location</th>");
                            htmlString.Append("<th scope=\"col\">Provider</th>");
                            
                        htmlString.Append("</tr>");
                    htmlString.Append("</thead>");
                    htmlString.Append("<tbody>");
                    foreach (var item in collection.Select((x, i) => new { MessageModel = x, Index = i + 1 }))
                        {
        
                            if (item.MessageModel.MessageType == "Appointment Request")
                            {
                                 htmlString.Append("<tr class=\"" + ((item.Index % 2 == 1) ? "r0" : "r1") + "\">");
                                 htmlString.Append("<td> " + Convert.ToDateTime(item.MessageModel.AppointmentStart).ToShortDateString() + "</td>");
                                 htmlString.Append("<td>" + item.MessageModel.PreferredTime + "</td>"); 
                                htmlString.Append("<td>" + item.MessageModel.VisitReason + "</td>");
                                 htmlString.Append("<td>" + item.MessageModel.FacilityName + "</td>");
                                 htmlString.Append("<td>" + item.MessageModel.ProviderName_To + "</td>");
                                
                                htmlString.Append("</tr>");
                            }
                        }

                    htmlString.Append("</tbody>");
                htmlString.Append("</table>");
            return htmlString.ToString();
        }

        public static string GetPatientSummaryHTMLForDashBoard(PatientSummaryModel PatientSummary,PatientOrganModel PatientOrgan)
        {
            StringBuilder htmlString = new StringBuilder();
           // htmlString.Append("<div id=\"ChartSummary\" class=\"draggable ui-widget-content ui-corner-all\" style=\"position: relative; top: 0px; height: 380px; left: 0px; width: 100%;\">");
            htmlString.Append("<div id=\"cart1\" style=\"position: absolute; top: 1px; left: 0px; right: 0px;\" class=\"ui-corner-all\">");
            htmlString.Append("<h5 class=\"ui-dialog-titlebar ui-widget-header ui-corner-all ui-helper-clearfix\"");
            htmlString.Append("style=\"margin-top: -1px; font-weight: bold; padding: 3px;\">Chart Summary");
            htmlString.Append("<div onmousemove=\"removing_class();\" onmouseover=\"removing_class(); \" onmousedown=\"removing_class1();\" onclick=\"removing_class1();pop_up_print_clinicalsummary();\" onmouseout=\"removing_class1();\" onmouseup=\"removing_class1();\" onfocus=\"removing_class1();\" style=\"cursor: pointer; float: right; width: auto; height: 20px; margin-top: 0px;\">");
            htmlString.Append("<img src=\"Content/img/print.png\" />");
            htmlString.Append("<span class=\"anc\" style=\"padding-right: 4px;\">Print</span>");
            htmlString.Append("</div>");
            htmlString.Append(" </h5>");
            htmlString.Append("<div class=\"ui-widget-content\" style=\"position: relative; top: -9px; border: none; overflow-y: auto; height: 340px;\">");
            htmlString.Append("<table width=\"100%\">");
            htmlString.Append("<tr height=\"50px\">");
            htmlString.Append("<td style=\"width: 32px;\" align=\"center\">");
            htmlString.Append("<img src=\"Content/img/demographics.png\" />");
            htmlString.Append("</td>");
            htmlString.Append("<td>");
            htmlString.Append("<h3 id=\"createdemographics\" onmousemove=\"removing_class();\" onmouseover=\"removing_class(); \" onmousedown=\"removing_class();\" onclick=\"removing_class(); \" onmouseout=\"removing_class();\" onmouseup=\"removing_class();\" onfocus=\"removing_class();\"><span class=\"anc_clin\" style=\"padding-right: 4px; font-weight: bold;\">Demographics </span> <img src=\"Content/img/edit.png\"  style=\"cursor:pointer; \" onclick=\"editDemographics();\"></h3>");

           
            StringBuilder jsonString = new StringBuilder();

            jsonString.Append(" { ");
            jsonString.Append("\"ID\": \"" + PatientSummary.PatientId + "\",");
            jsonString.Append("\"FirstName\": \"" + PatientSummary.FirstName + "\",");
            jsonString.Append("\"MiddleName\": \"" + PatientSummary.MiddleName + "\",");
            jsonString.Append("\"LastName\": \"" + PatientSummary.LastName + "\",");
            jsonString.Append("\"Title\": \"" + PatientSummary.Title + "\",");
            jsonString.Append("\"GenderId\": \"" + PatientSummary.GenderId + "\",");
            jsonString.Append("\"PreferredLanguageId\": \"" + PatientSummary.PreferredLanguageId + "\",");
            jsonString.Append("\"EthnicityId\": \"" + PatientSummary.EthnicityId + "\",");
            jsonString.Append("\"PAddress\": \"" + PatientSummary.PAddress + "\",");

            jsonString.Append("\"HomePhone\": \"" + PatientSummary.HomePhone + "\",");
            jsonString.Append("\"MobilePhone\": \"" + PatientSummary.MobilePhone + "\",");
            jsonString.Append("\"WorkPhone\": \"" + PatientSummary.WorkPhone + "\",");
            jsonString.Append("\"EMail\": \"" + PatientSummary.EMail + "\",");
            jsonString.Append("\"DOB\": \"" + PatientSummary.DOB+ "\",");
            jsonString.Append("\"RaceId\": \"" + PatientSummary.RaceId + "\",");
            jsonString.Append("\"City\": \"" + PatientSummary.City + "\",");
            jsonString.Append("\"State\": \"" + PatientSummary.State + "\",");
            jsonString.Append("\"Zip\": \"" + PatientSummary.Zip + "\",");
            jsonString.Append("\"CountryCode\": \"" + PatientSummary.CountryCode + "\",");
            jsonString.Append("\"CountryName\": \"" + PatientSummary.CountryName + "\",");


            jsonString.Append("\"SmokingStatusId\": \"" + PatientSummary.SmokingStatusId + "\",");
            jsonString.Append("\"MailAddress1\": \"" + PatientSummary.MailAddress1 + "\",");
            jsonString.Append("\"MailAddress2\": \"" + PatientSummary.MailAddress2 + "\",");
            jsonString.Append("\"MailCity\": \"" + PatientSummary.MailCity + "\",");
            jsonString.Append("\"MailCountryCode\": \"" + PatientSummary.MailCountryCode + "\",");
            jsonString.Append("\"MailPostalCode\": \"" + PatientSummary.MailPostalCode + "\",");
            jsonString.Append("\"RaceId_NotAnswered\": \"" + PatientSummary.RaceId_NotAnswered + "\",");
            jsonString.Append("\"RaceId_Native\": \"" + PatientSummary.RaceId_Native + "\",");
            jsonString.Append("\"RaceId_Asian\": \"" + PatientSummary.RaceId_Asian + "\",");
            jsonString.Append("\"RaceId_Black\": \"" + PatientSummary.RaceId_Black + "\",");
            jsonString.Append("\"RaceId_Hawaiian\": \"" + PatientSummary.RaceId_Hawaiian + "\",");
            jsonString.Append("\"RaceId_White\": \"" + PatientSummary.RaceId_White + "\",");
            jsonString.Append("\"MailState\": \"" + PatientSummary.MailState + "\"");

            jsonString.Append("}");

          

            htmlString.Append("<input type=\"hidden\" value=\'" + jsonString.ToString() + "\' id=\"demographicsdata\" />");

            
            StringBuilder jsonEmergencyString = new StringBuilder();
            jsonEmergencyString.Append("{");
            jsonEmergencyString.Append("\"EmergencyName\": \"" + PatientSummary.EmergencyName + "\",");
            jsonEmergencyString.Append("\"EmergencyHomePhone\": \"" + PatientSummary.EmergencyHomePhone + "\",");
            jsonEmergencyString.Append("\"EmergencyAddress\": \"" + PatientSummary.EmergencyAddress + "\",");
            jsonEmergencyString.Append("\"EmergencyCity\": \"" + PatientSummary.EmergencyCity + "\",");
            jsonEmergencyString.Append("\"EmergencyCountryCode\": \"" + PatientSummary.EmergencyCountryCode + "\",");
            jsonEmergencyString.Append("\"EmergencyCountryName\": \"" + PatientSummary.EmergencyCountryName + "\",");
            jsonEmergencyString.Append("\"EmergencyMobilePhone\": \"" + PatientSummary.EmergencyMobilePhone + "\",");
            jsonEmergencyString.Append("\"EmergencyRelationshipId\": \"" + PatientSummary.EmergencyRelationshipId + "\",");
            jsonEmergencyString.Append("\"EmergencyState\": \"" + PatientSummary.EmergencyState + "\",");
            jsonEmergencyString.Append("\"EmergencyWorkPhone\": \"" + PatientSummary.EmergencyWorkPhone + "\",");
            jsonEmergencyString.Append("\"EmergencyZip\": \"" + PatientSummary.EmergencyZip + "\",");
            jsonEmergencyString.Append("\"IsPrimary\": \"" + PatientSummary.IsPrimary + "\",");
            jsonEmergencyString.Append("\"EmergencyRelationship\": \"" + PatientSummary.EmergencyRelationship + "\"");
            jsonEmergencyString.Append("}");

            htmlString.Append("<input type=\"hidden\" value=\'" + jsonEmergencyString.ToString()+"\' id=\"objEmergency\" />");

            StringBuilder jsonPersonalString = new StringBuilder();
            jsonPersonalString.Append("{");
            jsonPersonalString.Append("\"HeightFt\": \"" + PatientSummary.HeightFt + "\",");
            jsonPersonalString.Append("\"HeightIn\": \"" + PatientSummary.HeightIn + "\",");
            jsonPersonalString.Append("\"HairColor\": \"" + PatientSummary.HairColor + "\",");
            jsonPersonalString.Append("\"EyeColor\": \"" + PatientSummary.EyeColor + "\",");
            jsonPersonalString.Append("\"BloodTypeId\": \"" + PatientSummary.BloodTypeId + "\",");
            jsonPersonalString.Append("\"ReligionId\": \"" + PatientSummary.ReligionId + "\",");
            jsonPersonalString.Append("\"Comments\": \"" + PatientSummary.Comments + "\",");
            jsonPersonalString.Append("\"Weight\": \"" + PatientSummary.Weight + "\",");
            jsonPersonalString.Append("\"OrganDoner\": \"" + PatientSummary.OrganDoner + "\",");
            jsonPersonalString.Append("\"BoneMarrow\": \"" + PatientOrgan.BoneMarrow + "\",");
            jsonPersonalString.Append("\"ConnectiveTissue\": \"" + PatientOrgan.ConnectiveTissue + "\",");
            jsonPersonalString.Append("\"Cornea\": \"" + PatientOrgan.Cornea + "\",");
            jsonPersonalString.Append("\"Heart\": \"" + PatientOrgan.Heart + "\",");
            jsonPersonalString.Append("\"HeartValves\": \"" + PatientOrgan.HeartValves + "\",");
            jsonPersonalString.Append("\"Intestines\": \"" + PatientOrgan.Intestines + "\",");
            jsonPersonalString.Append("\"Kidneys\": \"" + PatientOrgan.Kidneys + "\",");
            jsonPersonalString.Append("\"Liver\": \"" + PatientOrgan.Liver + "\",");
            jsonPersonalString.Append("\"Lungs\": \"" + PatientOrgan.Lungs + "\",");
            jsonPersonalString.Append("\"Pancreas\": \"" + PatientOrgan.Pancreas + "\",");
            jsonPersonalString.Append("\"PatientId\": \"" + PatientOrgan.PatientId + "\",");
            jsonPersonalString.Append("\"Skin\": \"" + PatientOrgan.Skin + "\"");

            jsonPersonalString.Append("}");
            htmlString.Append("<input type=\"hidden\" value=\'" + jsonPersonalString.ToString() + "\' id=\"objPersonal\" />");
            //htmlString.Append("}");

            htmlString.Append("</td>");
            htmlString.Append("<td>&nbsp;</td>");
            htmlString.Append("<td style=\"width: 32px;\" align=\"center\">");
            htmlString.Append("<img src=\"Content/img/personal.png\" />&nbsp;");

            htmlString.Append("</td>");

            htmlString.Append("<td colspan=\"3\">");

            htmlString.Append("<h3 id=\"createpersonal\" onmousemove=\"removing_class();\" onmouseover=\"removing_class(); \" onmousedown=\"removing_class();\" onclick=\"removing_class() \" onmouseout=\"removing_class();\" onmouseup=\"removing_class();\" onfocus=\"removing_class();\"><span class=\"anc_clin\" style=\"padding-right: 4px; font-weight: bold;\">Personal Information</span><img src=\"Content/img/edit.png\" style=\"cursor:pointer; \" onclick=\"editPersonal();\"> </h3>");
           

            //htmlString.Append("<h3 class=\"anc\" id=\"createpersonal\" onmousemove=\"removing_class();\" onmouseover=\"removing_class(); \" onmousedown=\"removing_class();\" onclick=\"editDemographics();removing_class(); \"  onmouseout=\"removing_class();\" onmouseup=\"removing_class();\" onfocus=\"removing_class();\" />");
            //htmlString.Append("<span class=\"anc\" style=\"padding-right: 4px; font-weight: bold;\">Personal Information</span> </h1>");

            htmlString.Append("</td>");


            htmlString.Append("</tr>");
            htmlString.Append("<tr class=\"table_brder\">");
            htmlString.Append("<td></td>");
            htmlString.Append("<td width=\"16%\"><b>Address:</b> </td>");
            htmlString.Append("<td width=\"35%\">" + PatientSummary.PAddress + "<br>"+PatientSummary.City+","+PatientSummary.State+" "+PatientSummary.Zip+"</td>");
            htmlString.Append("<td></td>");
            htmlString.Append("<td width=\"16%\"><b>Blood Type:</b></td>");
            htmlString.Append("<td colspan=\"2\">" + PatientSummary.BloodType + "</td>");
            htmlString.Append("</tr>");
          
            
            
            
            htmlString.Append("<tr>");
            htmlString.Append("<td></td>");
            htmlString.Append("<td><b>Home Phone:</b></td>");
            htmlString.Append("<td>" + PatientSummary.HomePhone + " </td>");
            htmlString.Append("<td></td>");
            htmlString.Append("<td><b>Hair Color: </b></td>");
            htmlString.Append("<td colspan=\"2\">" + PatientSummary.HairColor + "</td>");
            htmlString.Append("</tr>");


            htmlString.Append("<tr class=\"table_brder\">");
            htmlString.Append("<td></td>");
            htmlString.Append("<td><b>Work Phone:</b> </td>");
            htmlString.Append("<td>" + PatientSummary.WorkPhone + "</td>");
            htmlString.Append("<td></td>");
            htmlString.Append("<td><b>Eye Color: </b></td>");
            htmlString.Append("<td colspan=\"2\">" + PatientSummary.EyeColor + "</td>");
            htmlString.Append("</tr>");

            htmlString.Append("<tr>");
            htmlString.Append("<td></td>");
            htmlString.Append("<td><b>Mobile Phone:</b> </td>");
            htmlString.Append("<td>" + PatientSummary.MobilePhone + "</td>");
            htmlString.Append("<td></td>");
            htmlString.Append("<td><b>Organ Donor: </b></td>");
            htmlString.Append("<td colspan=\"2\">" + PatientSummary.OrganDoner + "");

            htmlString.Append("</td>");

            htmlString.Append("</tr>");
            htmlString.Append("<tr class=\"table_brder\">");
            htmlString.Append("<td></td>");
            htmlString.Append("<td><b>Email:</b> </td>");
            htmlString.Append("<td>" + PatientSummary.EMail + "</td>");
            htmlString.Append("<td></td>");
            htmlString.Append("<td><b>Organs: </b></td>");
            htmlString.Append("<td colspan=\"3\">");
                if (PatientSummary.BoneMarrow == true){
                    htmlString.Append("BoneMarrow,                                                            ");
                } 
              if (PatientSummary.ConnectiveTissue == true){
                  htmlString.Append("ConnectiveTissue,                                                            ");
               } 
               if (PatientSummary.Cornea == true){
                   htmlString.Append("Cornea,                                                            ");
               }
               if (PatientSummary.Heart == true)
               {
                   htmlString.Append("Heart,                                                            ");
               }
               if (PatientSummary.HeartValves == true)
               { htmlString.Append("Heart Valves,                                                            "); }
              if (PatientSummary.Intestines == true){
                  htmlString.Append("Intestines,                                                            ");
              }
             if (PatientSummary.Kidneys == true){
                 htmlString.Append("Kidneys,                                                            ");
             }
             if (PatientSummary.Liver == true){
                 htmlString.Append("Liver,                                                            ");
             }
             if (PatientSummary.Lungs == true){
                 htmlString.Append("Lungs,                                                            ");
             }
            if (PatientSummary.Pancreas == true){
                htmlString.Append("Pancreas,                                                            ");
            }
             if (PatientSummary.Skin == true){
                 htmlString.Append("Skin                                                            ");
             }
                htmlString.Append("</td>");

            htmlString.Append("</tr>");
            htmlString.Append("<tr>");
            htmlString.Append("<td></td>");
            htmlString.Append("<td><b>Birth Date:</b></td>");
            htmlString.Append("<td>" + PatientSummary.DOB + "</td>");
            htmlString.Append("<td></td>");
            htmlString.Append("<td></td>");
            htmlString.Append("<td colspan=\"2\"></td>");



            htmlString.Append("</tr>");
            htmlString.Append("<tr class=\"table_brder\">");

            htmlString.Append("<td></td>");
            htmlString.Append("<td><b>Sex:</b> </td>");
            htmlString.Append("<td>" + PatientSummary.Gender + " </td>");
            htmlString.Append("<td style=\"width: 32px;\" align=\"center\">");
            htmlString.Append("<img src=\"Content/img/emergency.png\" /></td>");
            htmlString.Append("<td colspan=\"3\">");


            htmlString.Append("<h3 id=\"createemergency\" onmousemove=\"removing_class();\" onmouseover=\"removing_class(); \" onmousedown=\"removing_class();\" onclick=\"removing_class() \" onmouseout=\"removing_class();\" onmouseup=\"removing_class();\" onfocus=\"removing_class();\"><span class=\"anc_clin\" style=\"padding-right: 4px; font-weight: bold;\">Emergency Contact</span> <img src=\"Content/img/edit.png\" style=\"cursor:pointer; \" onclick=\"editEmergency();\"></h3>");
            htmlString.Append("</td>");

            htmlString.Append("</tr>");
            htmlString.Append("<tr>");
            htmlString.Append("<td></td>");
            htmlString.Append("<td><b>Preferred Language:</b> </td>");
            htmlString.Append("<td>" + PatientSummary.PreferredLanguage + " </td>");
            htmlString.Append("<td></td>");
            htmlString.Append("<td><b>Name: </b></td>");
            htmlString.Append("<td colspan=\"2\">");
            htmlString.Append("" + PatientSummary.EmergencyName + "");
            htmlString.Append("</td>");


            htmlString.Append("</tr>");
            htmlString.Append("<tr class=\"table_brder\">");
            htmlString.Append("<td></td>");
            htmlString.Append("<td><b>Race:</b> </td>");
            htmlString.Append("<td>" + PatientSummary.Race + " </td>");
            htmlString.Append("<td></td>");
            htmlString.Append("<td><b>Phone: </b></td>");
            htmlString.Append("<td colspan=\"2\">");
            htmlString.Append("" + PatientSummary.EmergencyHomePhone + "");
            htmlString.Append("</td>");

            htmlString.Append("</tr>");


            htmlString.Append("<tr>");
            htmlString.Append("<td></td>");
            htmlString.Append("<td><b>Ethnicity:</b> </td>");
            htmlString.Append("<td>" + PatientSummary.Ethnicity + "</td>");
            htmlString.Append("<td></td>");
            htmlString.Append("<td><b>Relation :</b></td>");
            htmlString.Append("<td colspan=\"2\">");
            htmlString.Append("" + PatientSummary.EmergencyRelationship + "");
            htmlString.Append("</td>");

            htmlString.Append("</tr>");
            htmlString.Append("<tr>");
            htmlString.Append("<td></td>");
            htmlString.Append("<td><b>Smoking Status:</b> </td>");
            htmlString.Append("<td>" + PatientSummary.SmokingStatus + "</td>");
            htmlString.Append("<td></td>");
            htmlString.Append("<td></td>");
            htmlString.Append("<td colspan=\"2\"></td>");

            htmlString.Append("</tr>");

            htmlString.Append("</table>");


            htmlString.Append("</div>");
            htmlString.Append("</div>");
           // htmlString.Append("</div>");

            //htmlString.Append("return htmlString.ToString();");
            //htmlString.Append("}");
            //htmlString.Append("}");
            //htmlString.Append("}");

            return htmlString.ToString();
        }

        public static string ToHTMLPatientEmergencyContactForDashBoard(this List<PatientEmergencyModel> emergencyContacts)
        {
            var htmlStr = "";

            if (emergencyContacts != null && emergencyContacts.Count > 0)
            {
                //Table...
                var sbTable = new StringBuilder("<table id='box-table-a' summary='Appointments'>");

                //Table header with defination...
                var sbTHead = new StringBuilder();
                sbTHead.Append("<thead><tr><th scope='col'>Name</th><th scope='col'>Mobile No</th><th scope='col'>Address</th><th scope='col'>Email</th><th scope='col'>City</th><th scope='col'>State</th><th scope='col'>Country</th><th scope='col' colspan='3'></th></tr></thead>");

                //Append to table...
                sbTable.Append(sbTHead.ToString());

                //Table body...
                var sbTBody = new StringBuilder("<tbody>");

                //Table body defination...
                foreach (var contact in emergencyContacts)
                {
                    var sbTBodyRow = new StringBuilder();

                    var jsonStr = new JavaScriptSerializer().Serialize(contact);
                    var hdnInput = string.Format("<input type='hidden' id='{0}'  value='{1}'/>", contact.PatientEmergencyId, jsonStr.Replace("\"PatientId\":0,", "").ToString());
                    
                    sbTBodyRow.Append("<tr class='r0'>");

                        //Append Input Field...
                        sbTBodyRow.Append(hdnInput);

                        sbTBodyRow.AppendFormat("<td>{0}</td>",contact.FirstName);
                        sbTBodyRow.AppendFormat("<td>{0}</td>", contact.MobilePhone);
                        sbTBodyRow.AppendFormat("<td>{0}</td>",contact.Address1);
                        sbTBodyRow.AppendFormat("<td>{0}</td>", contact.Email);
                        sbTBodyRow.AppendFormat("<td>{0}</td>",contact.City);
                        sbTBodyRow.AppendFormat("<td>{0}</td>",contact.State);
                        sbTBodyRow.AppendFormat("<td>{0}</td>",contact.CountryCode);

                        var imgEdit = "<img onclick='editEmergencyContact(" + contact.PatientEmergencyId + ");' id='imgEditEmergencyContact' src='Content/img/edit.png' />";
                        sbTBodyRow.AppendFormat("<td align='center'>{0}</td>",imgEdit.ToString());

                        var imgDetails = "<img onclick='detailsEmergencyContact(" + contact.PatientEmergencyId + ");' id='imgDetailsEmergencyContact'  src='Content/img/details.png' />";
                        sbTBodyRow.AppendFormat("<td align='center'>{0}</td>",imgDetails.ToString());

                        var imgDelete = "<img onclick='deleteEmergencyContact(" + contact.PatientEmergencyId + ");' id='imgDeleteEmergencyContact'  src='Content/img/delete.png' />";
                        sbTBodyRow.AppendFormat("<td align='center'>{0}</td>",imgDelete.ToString());
                    sbTBodyRow.Append("</tr>");

                    //Append Table Body Row...
                    sbTBody.Append(sbTBodyRow.ToString());
                }

                //Body Tag Close...
                sbTBody.Append("</tbody>");

                //Append to table...
                sbTable.Append(sbTBody.ToString());

                //Table Tag Close...
                sbTable.Append("</table>");

                htmlStr = sbTable.ToString();
            }

            return htmlStr;
        }

        public static string ToHTMLPatientDoctorForDashBoard(this List<PatientDoctorModel> patientDoctors)
        {
            var htmlStr = "";

            if (patientDoctors != null && patientDoctors.Count > 0)
            {
                //Table...
                var sbTable = new StringBuilder("<table id='box-table-a' summary='Appointments'>");

                //Table header with defination...
                var sbTHead = new StringBuilder();
                sbTHead.Append("<thead><tr><th scope='col'>Name</th><th scope='col'>Type</th><th scope='col'>Office No</th><th scope='col'>Primary</th><th scope='col' colspan='3'></th></tr></thead>");

                //Append to table...
                sbTable.Append(sbTHead.ToString());

                //Table body...
                var sbTBody = new StringBuilder("<tbody>");

                //Table body defination...
                foreach (var doctor in patientDoctors)
                {
                    var sbTBodyRow = new StringBuilder();

                    var jsonStr = new JavaScriptSerializer().Serialize(doctor);
                    var hdnInput = string.Format("<input type='hidden' id='{0}'  value='{1}'/>", doctor.PatientDoctorId, jsonStr.Replace("\"PatientId\":0,", "").ToString());

                    sbTBodyRow.Append("<tr class='r0'>");

                    //Append Input Field...
                    sbTBodyRow.Append(hdnInput);

                    sbTBodyRow.AppendFormat("<td>{0}</td>", doctor.Name);
                    sbTBodyRow.AppendFormat("<td>{0}</td>", doctor.DoctorTypeDesc);
                    sbTBodyRow.AppendFormat("<td>{0}</td>", doctor.WorkPhone);
                    sbTBodyRow.AppendFormat("<td>{0}</td>", (doctor.IsPrimary)?"Yes":"No");


                    var imgEdit = "<img onclick='editDoctor(" + doctor.PatientDoctorId + ");' id='imgEditEmergencyContact' src='Content/img/edit.png' />";
                    sbTBodyRow.AppendFormat("<td align='center'>{0}</td>", imgEdit.ToString());

                    var imgDetails = "<img onclick='detailsDoctor(" + doctor.PatientDoctorId + ");' id='imgDetailsEmergencyContact'  src='Content/img/details.png' />";
                    sbTBodyRow.AppendFormat("<td align='center'>{0}</td>", imgDetails.ToString());

                    var imgDelete = "<img onclick='deleteDoctor(" + doctor.PatientDoctorId + ");' id='imgDeleteEmergencyContact'  src='Content/img/delete.png' />";
                    sbTBodyRow.AppendFormat("<td align='center'>{0}</td>", imgDelete.ToString());
                    sbTBodyRow.Append("</tr>");

                    //Append Table Body Row...
                    sbTBody.Append(sbTBodyRow.ToString());
                }

                //Body Tag Close...
                sbTBody.Append("</tbody>");

                //Append to table...
                sbTable.Append(sbTBody.ToString());

                //Table Tag Close...
                sbTable.Append("</table>");

                htmlStr = sbTable.ToString();
            }

            return htmlStr;
        }

        public static string ToHTMLPatientVisitForDashBoard(this List<PatientVisitModel> patientVisits)
        {
            
            var htmlStr = "";

            if (patientVisits != null && patientVisits.Count > 0)
            {
                var sbTBody = new StringBuilder();

                int counter = 0;

                //Table body defination...
                foreach (var visit in patientVisits)
                {
                    var sbTBodyRow = new StringBuilder();

                    if(counter%2==0)
                        sbTBodyRow.Append("<tr class='r1'>");
                    else
                        sbTBodyRow.Append("<tr class='r0'>");
                    
                    //string chkInput = string.Format("<input type='checkbox' data-myVal='{0}' id='chk1'  name='chk1[]' />",(visit.VisitId + "|" + visit.FacilityId));

                    //sbTBodyRow.AppendFormat("<td>{0}</td>", chkInput);
                    sbTBodyRow.AppendFormat("<td><span  class='LinkHighlight hoverMe' onclick='ViewVisitRecord(\"{0}\");'>{1}</span></td>", visit.VisitId,visit.VisitDate.ToString("MM/dd/yyyy"));

                    sbTBodyRow.AppendFormat("<td>{0}</td>", visit.FacilityName);
                    sbTBodyRow.AppendFormat("<td>{0}</td>", visit.ProviderName);

                    sbTBodyRow.AppendFormat("<td>{0}</td>", (visit.VisitReason.Length>22)?visit.VisitReason.Substring(0,22) + "...":visit.VisitReason);
                    sbTBodyRow.AppendFormat("<td>{0}</td>", (visit.Viewable) ? "Shared" : "Hidden");

                    string shareHideLink="";

                    if (visit.Viewable)
                        shareHideLink = "<span class='LinkHighlight hoverMe' onclick='shareHidePatientVisit(\"0|" + visit.VisitId + "|" + visit.FacilityId +"\");'><img src='Content/img/hide.png' />Hide</span>";
                    else
                        shareHideLink = "<span class='LinkHighlight hoverMe' onclick='shareHidePatientVisit(\"1|" + visit.VisitId + "|" + visit.FacilityId + "\");'><img src='Content/img/share.png' />Share</span>";

                    sbTBodyRow.AppendFormat("<td>{0}</td>",shareHideLink);
                    
                    sbTBodyRow.Append("</tr>");

                    //Append Table Body Row...
                    sbTBody.Append(sbTBodyRow);

                    counter++;
                }

                htmlStr = sbTBody.ToString();
            }

            return htmlStr;
        }

        public static string ToHTMLPatientOutsideDoctorForDashBoard(this List<PatientOutsideDoctorModel> patientOutsideDoctors)
        {

            var htmlStr = "";

            if (patientOutsideDoctors != null && patientOutsideDoctors.Count > 0)
            {
                var sbTBody = new StringBuilder();

                int counter = 0;

                //Table body defination...
                foreach (var doctor in patientOutsideDoctors)
                {
                    var sbTBodyRow = new StringBuilder();

                    if (counter % 2 == 0)
                        sbTBodyRow.Append("<tr class='r1'>");
                    else
                        sbTBodyRow.Append("<tr class='r0'>");

                    //string chkInput = string.Format("<input type='checkbox' data-myVal='{0}' id='chk2'  name='chk2[]' />", doctor.DocumentCntr);

                    //sbTBodyRow.AppendFormat("<td>{0}</td>", chkInput);
                    sbTBodyRow.AppendFormat("<td>{0}</td>", doctor.DateCreated.ToString("MM/dd/yyyy"));
                    sbTBodyRow.AppendFormat("<td>{0}</td>", doctor.DoctorName);

                    string desc = (doctor.DocumentDescription.Length > 22) ? doctor.DocumentDescription.Substring(0, 22) + "..." : doctor.DocumentDescription;
                    desc = "<span class='LinkHighlight hoverMe' onclick='ViewDoctorAttachment(\"" + doctor.DocumentCntr + "|" + doctor.DocumentFormat + "\");'> " + desc + " </span>";

                    sbTBodyRow.AppendFormat("<td>{0}</td>", desc);



                    sbTBodyRow.AppendFormat("<td>{0}</td>", (doctor.Notes.Length > 22) ? doctor.Notes.Substring(0, 22) + "..." : doctor.Notes);
                    sbTBodyRow.AppendFormat("<td>{0}</td>", (doctor.Viewable) ? "Shared" : "Hidden");


                    string shareHideLink = "";

                    if (doctor.Viewable)
                        shareHideLink = "<span class='LinkHighlight hoverMe' onclick='shareHidePatientOutsideDoctor(\"0|" + doctor.DocumentCntr + "\");'><img src='Content/img/hide.png' />Hide</span>";
                    else
                        shareHideLink = "<span class='LinkHighlight hoverMe' onclick='shareHidePatientOutsideDoctor(\"1|" + doctor.DocumentCntr + "\");'><img src='Content/img/share.png' />Share</span>";


                    sbTBodyRow.AppendFormat("<td>{0}</td>", shareHideLink);

                    sbTBodyRow.Append("</tr>");

                    //Append Table Body Row...
                    sbTBody.Append(sbTBodyRow);

                    counter++;
                }

                htmlStr = sbTBody.ToString();
            }

            return htmlStr;
        }
        public static string ToHTMLPatientMedicalDocumentForDashBoard(this List<PatientMedicalDocumentModel> patientDocuments)
        {

            var htmlStr = "";

            if (patientDocuments != null && patientDocuments.Count > 0)
            {
                var sbTBody = new StringBuilder();

                int counter = 0;

                //Table body defination...
                foreach (var pDoc in patientDocuments)
                {
                    var sbTBodyRow = new StringBuilder();

                    if (counter % 2 == 0)
                        sbTBodyRow.Append("<tr class='r1'>");
                    else
                        sbTBodyRow.Append("<tr class='r0'>");

                    //string chkInput = string.Format("<input type='checkbox' data-myVal='{0}'  id='chk3'  name='chk3[]' />", pDoc.DocumentCntr);

                    //sbTBodyRow.AppendFormat("<td>{0}</td>", chkInput);

                    string jsonString = new JavaScriptSerializer().Serialize(pDoc);
                    string hdnInput = "<input type='hidden' id='" + pDoc.DocumentCntr + "' value='" + jsonString + "' />";

                    sbTBodyRow.AppendFormat("<td>{0}</td>", hdnInput);

                    sbTBodyRow.AppendFormat("<td>{0}</td>", pDoc.DateCreated.ToString("MM/dd/yyyy"));
                    sbTBodyRow.AppendFormat("<td>{0}</td>", pDoc.DoctorName);

                    string desc = (pDoc.DocumentDescription.Length>22)?pDoc.DocumentDescription.Substring(0,22) + "..." :pDoc.DocumentDescription;
                    desc = "<span class='LinkHighlight hoverMe' onclick='ViewDocumentAttachment(\"" + pDoc.DocumentCntr + "|" + pDoc.DocumentFormat + "\");'> " + desc + " </span>";

                    sbTBodyRow.AppendFormat("<td>{0}</td>", desc);

                    sbTBodyRow.AppendFormat("<td>{0}</td>", (pDoc.Notes.Length>22)?pDoc.Notes.Substring(0,22) + "...":pDoc.Notes);
                    sbTBodyRow.AppendFormat("<td>{0}</td>", (pDoc.Viewable) ? "Shared" : "Hidden");


                    string shareHideLink = "";

                    if (pDoc.Viewable)
                        shareHideLink = "<span class='LinkHighlight hoverMe' onclick='shareHidePatientMedicalDocument(\"0|" + pDoc.DocumentCntr + "\");'><img src='Content/img/hide.png' />Hide</span>";
                    else
                        shareHideLink = "<span class='LinkHighlight hoverMe' onclick='shareHidePatientMedicalDocument(\"1|" + pDoc.DocumentCntr + "\");'><img src='Content/img/share.png' />Share</span>";


                    sbTBodyRow.AppendFormat("<td>{0}</td>", shareHideLink);

                    string imgEdit = "<img onclick='EditPatientMedicalDocument(\"" + pDoc.DocumentCntr + "\");' id='imgEditPatMedDoc'  src='Content/img/edit.png' />"; 

                    sbTBodyRow.AppendFormat("<td align='center'>{0}</td>", imgEdit);

                    string imgDelete = "<img onclick='deletePatMedDocument(\""+pDoc.DocumentCntr+"\");' id='imgDeletePatMedDoc'  src='Content/img/delete.png' />";
                    
                    sbTBodyRow.AppendFormat("<td align='center'>{0}</td>", imgDelete);

                    sbTBodyRow.Append("</tr>");

                    //Append Table Body Row...
                    sbTBody.Append(sbTBodyRow);

                    counter++;
                }

                htmlStr = sbTBody.ToString();
            }

            return htmlStr;
        }

        public static string ToHTMLGeneralDocumentForDashBoard(this List<GeneralDocumentModel> generalDocuments)
        {

            var htmlStr = "";

            if (generalDocuments != null && generalDocuments.Count > 0)
            {
                var sbTBody = new StringBuilder();

                int counter = 0;

                //Table body defination...
                foreach (var pDoc in generalDocuments)
                {
                    var sbTBodyRow = new StringBuilder();

                    if (counter % 2 == 0)
                        sbTBodyRow.Append("<tr class='r1'>");
                    else
                        sbTBodyRow.Append("<tr class='r0'>");


                    string jsonString = new JavaScriptSerializer().Serialize(pDoc);
                    string hdnInput = "<input type='hidden' id='" + pDoc.DocumentCntr + "' value='" + jsonString + "' />";

                    sbTBodyRow.AppendFormat("<td>{0}</td>", hdnInput);


                    sbTBodyRow.AppendFormat("<td>{0}</td>", pDoc.DateCreated.ToString("MM/dd/yyyy"));

                    string desc = (pDoc.DocumentDescription.Length > 22) ? pDoc.DocumentDescription.Substring(0, 22) + "..." : pDoc.DocumentDescription;
                    desc = "<span class='LinkHighlight hoverMe' onclick='ViewGDocumentAttachment(\"" + pDoc.DocumentCntr + "|" + pDoc.DocumentFormat + "\");'> " + desc + " </span>";

                    sbTBodyRow.AppendFormat("<td>{0}</td>", desc);



                    sbTBodyRow.AppendFormat("<td>{0}</td>", (pDoc.Notes.Length > 22) ? pDoc.Notes.Substring(0, 22) + "..." : pDoc.Notes);

                    sbTBodyRow.AppendFormat("<td>{0}</td>",(pDoc.Viewable)? "Shared":"Hidden");

                    string shareHideLink = "";

                    if (pDoc.Viewable)
                        shareHideLink = "<span class='LinkHighlight hoverMe' onclick='shareHideGeneralDocument(\"0|" + pDoc.DocumentCntr + "\");'><img src='Content/img/hide.png' />Hide</span>";
                    else
                        shareHideLink = "<span class='LinkHighlight hoverMe' onclick='shareHideGeneralDocument(\"1|" + pDoc.DocumentCntr + "\");'><img src='Content/img/share.png' />Share</span>";


                    sbTBodyRow.AppendFormat("<td>{0}</td>", shareHideLink);


                    string imgEdit = "<img onclick='OpenEditGeneralDocumentForm(\"" + pDoc.DocumentCntr + "\");' id='imgEditGDoc'  src='Content/img/edit.png' />";

                    sbTBodyRow.AppendFormat("<td align='center'>{0}</td>", imgEdit);


                    string imgDelete = "<img onclick='deleteGDocument(\""+pDoc.DocumentCntr+"\");' id='imgDeleteGDoc'  src='Content/img/delete.png' />";
                    
                    sbTBodyRow.AppendFormat("<td align='center'>{0}</td>", imgDelete);

                    sbTBodyRow.Append("</tr>");

                    //Append Table Body Row...
                    sbTBody.Append(sbTBodyRow);

                    counter++;
                }

                htmlStr = sbTBody.ToString();
            }

            return htmlStr;
        }

        public static string ToHTMLInsurancePolicyForDashBoard(this List<InsurancePolicyModel> insurancePolicies)
        {

            var htmlStr = "";

            if (insurancePolicies != null && insurancePolicies.Count > 0)
            {
                var sbTBody = new StringBuilder();

                int counter = 0;

                //Table body defination...
                foreach (var policy in insurancePolicies)
                {
                    var sbTBodyRow = new StringBuilder();

                    if (counter % 2 == 0)
                        sbTBodyRow.Append("<tr class='r1'>");
                    else
                        sbTBodyRow.Append("<tr class='r0'>");


                    string jsonString = new JavaScriptSerializer().Serialize(policy);
                    string hdnInput = "<input type='hidden' id='" + policy.PatientPolicyId + "' value='" + jsonString + "' />";

                    sbTBodyRow.AppendFormat("<td>{0}</td>", hdnInput);

                    sbTBodyRow.AppendFormat("<td>{0}</td>", (!string.IsNullOrEmpty(policy.PolicyTypeName))?policy.PolicyTypeName : policy.Value);
                    sbTBodyRow.AppendFormat("<td>{0}</td>", policy.Company);

                    sbTBodyRow.AppendFormat("<td>{0}</td>", policy.Agent);
                    sbTBodyRow.AppendFormat("<td>{0}</td>", policy.AgentPhone);

                    string imgEdit = "<img onclick='OpenEditInsurancePolicyForm(\"" + policy.PatientPolicyId + "\");' id='imgEditInsurancePolicy'  src='Content/img/edit.png' />";

                    sbTBodyRow.AppendFormat("<td align='center'>{0}</td>", imgEdit);


                    string imgDelete = "<img onclick='deleteInsurancePolicy(\""+policy.PatientPolicyId+"\");' id='imgDeleteInsurancePolicy'  src='Content/img/delete.png' />";

                    sbTBodyRow.AppendFormat("<td align='center'>{0}</td>", imgDelete);

                    sbTBodyRow.Append("</tr>");

                    //Append Table Body Row...
                    sbTBody.Append(sbTBodyRow);

                    counter++;
                }

                htmlStr = sbTBody.ToString();
            }

            return htmlStr;
        }

        public static string ToHTMLProfessionalAdvisorForDashBoard(this List<ProfessionalAdvisorModel> professionalAdvisors)
        {

            var htmlStr = "";

            if (professionalAdvisors != null && professionalAdvisors.Count > 0)
            {
                var sbTBody = new StringBuilder();

                int counter = 0;

                //Table body defination...
                foreach (var advisor in professionalAdvisors)
                {
                    var sbTBodyRow = new StringBuilder();

                    if (counter % 2 == 0)
                        sbTBodyRow.Append("<tr class='r1'>");
                    else
                        sbTBodyRow.Append("<tr class='r0'>");

                    string jsonString = new JavaScriptSerializer().Serialize(advisor);
                    string hdnInput = "<input type='hidden' id='" + advisor.PatientAdvisorId + "' value='" + jsonString + "' />";

                    sbTBodyRow.AppendFormat("<td>{0}</td>", hdnInput);


                    sbTBodyRow.AppendFormat("<td>{0}</td>", (!string.IsNullOrEmpty(advisor.AdvisorDesc))?advisor.AdvisorDesc : advisor.Value);
                    sbTBodyRow.AppendFormat("<td>{0}</td>", advisor.Name);

                    sbTBodyRow.AppendFormat("<td>{0}</td>", advisor.ContactAgent);
                    sbTBodyRow.AppendFormat("<td>{0}</td>", advisor.WorkPhone);


                    string imgEdit = "<img onclick='OpenEditProfessionalAdvisorForm(\"" + advisor.PatientAdvisorId + "\");' id='imgEditProfessionalAdvisor'  src='Content/img/edit.png' />";

                    sbTBodyRow.AppendFormat("<td align='center'>{0}</td>", imgEdit);

                    string imgDelete = "<img onclick='deleteProfessionalAdvisor(\"" + advisor.PatientAdvisorId + "\");' id='imgDeleteProfessionalAdvisor'  src='Content/img/delete.png' />";

                    sbTBodyRow.AppendFormat("<td align='center'>{0}</td>", imgDelete);

                    sbTBodyRow.Append("</tr>");

                    //Append Table Body Row...
                    sbTBody.Append(sbTBodyRow);

                    counter++;
                }

                htmlStr = sbTBody.ToString();
            }

            return htmlStr;
        }
 
      }
}
