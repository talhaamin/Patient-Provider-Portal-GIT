using AMR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Data;

namespace AMR.Core.Utilities
{
    public static class utility
    {
       
        public static string inchToFeet(long inches)
        {
            long feet = inches / 12;
            long inch = inches - (feet * 12);
            return feet.ToString() + "'" + inch.ToString() + "\"";
        }


        public static string inchToFeetSeporate(long inches)
        {
            long feet = inches / 12;
            long inch = inches - (feet * 12);
            return feet.ToString() + "." + inch.ToString();
        }

        public static List<HomeWidgetModel> ConvertToHTML(string Xml)
        {
             HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();
             htmlDoc.LoadHtml(Xml);
             //var Pattags = htmlDoc.DocumentNode.SelectNodes("//recordtarget");

             //PatientModel PatientData = new PatientModel();
             //if (Pattags != null)
             //{
             //    foreach (var pattags in Pattags)
             //    {
             //        HtmlDocument PatDoc = new HtmlDocument();
             //        PatDoc.LoadHtml(pattags.InnerHtml);
             //        PatientData.LastName = PatDoc.DocumentNode.SelectSingleNode("//family").InnerHtml;
             //        PatientData.FirstName = PatDoc.DocumentNode.SelectSingleNode("//given").InnerHtml;
             //        PatientData.Address1 = PatDoc.DocumentNode.SelectSingleNode("//streetaddressline").InnerHtml;
             //        PatientData.City = PatDoc.DocumentNode.SelectSingleNode("//city").InnerHtml;
             //        PatientData.State = PatDoc.DocumentNode.SelectSingleNode("//state").InnerHtml;
             //        PatientData.MailPostalCode = PatDoc.DocumentNode.SelectSingleNode("//postalcode").InnerHtml;
             //        PatientData.HomePhone = PatDoc.DocumentNode.SelectSingleNode("//telecom").Attributes[0].Value;
             //        PatientData.Gender = PatDoc.DocumentNode.SelectSingleNode("//administrativegendercode").Attributes[0].Value;
             //        PatientData.Birthday = PatDoc.DocumentNode.SelectSingleNode("//birthtime").Attributes[0].Value;
             //    }
             //}
             
             var aTags = htmlDoc.DocumentNode.SelectNodes("//section");
            List<HomeWidgetModel> HomeWidgetsList = new List<HomeWidgetModel>();

            //System.Web.Script.Serialization.JavaScriptSerializer oSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            //string sJSON = oSerializer.Serialize(PatientData);
            //HomeWidgetsList.Add(new HomeWidgetModel
            //{
            //    Id = "patientSummary",
            //    WidgetHtml = sJSON
            //});

             if (aTags != null)
             {
                 foreach (var aTag in aTags)
                 {
                     HtmlDocument divDoc = new HtmlDocument();
                     divDoc.LoadHtml(aTag.InnerHtml);
                     HomeWidgetsList.Add(new HomeWidgetModel
                     {
                         Id = divDoc.DocumentNode.SelectSingleNode("//title").InnerHtml,
                         WidgetHtml = divDoc.DocumentNode.SelectSingleNode("//text").InnerHtml
                     });
                 }
             }

            
            return HomeWidgetsList;
        }

        public static DataSet GetDataSetFromString(string input)
        {
            //Medication:1,2,3,4|SocialHistory:2,3,4|Immunization:1,2,3,4|POC:1-test,2-test1,3-test3
            DataSet ds = new DataSet("CCD");

            string[] splitOnPipe = input.Split('|');
            foreach (string item in splitOnPipe)
            {
                if (item != "")
                {
                    string[] splitOnColon = item.Split('~');

                    string TableName = splitOnColon[0];
                    //Create table here
                    DataTable dt = new DataTable();
                    dt.TableName = TableName;
                    if (TableName == "POC" || TableName == "Clinical")
                    {
                        dt.Columns.Add("ID", typeof(int));
                        dt.Columns.Add("CodeValue", typeof(string));
                        dt.Columns.Add("CodeSystem", typeof(string));
                        dt.Columns.Add("Plan Activity", typeof(string));
                        dt.Columns.Add("Comments", typeof(string));
                        dt.Columns.Add("Planned Date/Time", typeof(string));
                        dt.Columns.Add("Goal", typeof(string));
                    }
                    else if (TableName == "VisitReason")
                    {
                        dt.Columns.Add("ID", typeof(int));
                        dt.Columns.Add("VisitReason", typeof(string));
                       
                    }
                    else
                    {
                        dt.Columns.Add("ID", typeof(int));
                        
                    }
                    DataColumn[] keys=new DataColumn[1];
                    keys[0]=dt.Columns["ID"];
                    dt.PrimaryKey=keys;
                    string[] splitOnComma = splitOnColon[1].Split('*');

                    foreach (string item1 in splitOnComma)
                    {
                        if (item1 != "")
                        {
                            if (item != "")
                            {
                                if (TableName == "POC" || TableName == "Clinical")
                                {
                                    string[] str = item1.Split('^');
                                    dt.Rows.Add(Convert.ToInt64(str[0]), str[1], str[2], str[3], str[4], str[5], str[6]);
                                        //, str[3], str[4]);
                                }
                                else if (TableName == "VisitReason")
                                {
                                    string[] str = item1.Split('^');
                                    dt.Rows.Add(Convert.ToInt64(str[0]), str[1]);
                                }
                                else
                                {

                                    dt.Rows.Add(Convert.ToInt64(item1));
                                }
                            }
                        }
                    }
                    ds.Tables.Add(dt);
                }
            }

            return ds;
        }
    }
}
