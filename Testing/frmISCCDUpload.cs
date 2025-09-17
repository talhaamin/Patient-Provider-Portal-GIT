using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

using System.Xml;
using AMR.Data;

namespace Testing
{
    public partial class frmISCCDUpload : Form
    {
        public frmISCCDUpload()
        {
            InitializeComponent();
        }

        private void frmISVisitUpload_Load(object sender, EventArgs e)
        {
            //GlobalVar.UserId = 2;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            //string FileName = @"C:\Transfer\CCDEmerge-1-Mod.xml";

            //ISPatientService.PatientWS PatinetWS = new ISPatientService.PatientWS();
            //ISPatientService.CCDPostData CCDData = new ISPatientService.CCDPostData();
            ISPatientService.CCDPostResponse ResponseData = new ISPatientService.CCDPostResponse();
            //try
            //{
            //    // Load Basic Visit Info
            //    CCDData.AMRPatientId = Convert.ToInt64(nudPatientId.Value);

            //    byte[] bytes = DiskToBytes(FileName);
            //    CCDData.Document = bytes;

            //    ResponseData = PatinetWS.CCDPost(GlobalVar.FacilityId, GlobalVar.UserId, GlobalVar.Token, CCDData);
            //    if (ResponseData.Valid == false)
            //        MessageBox.Show(ResponseData.ErrorMessage);
            //}
            //catch
            //{

            //}


            //string ccdPath = @"C:\Transfer\Heather_Green.CCD";
            //string ccdPath = @"C:\Transfer\Frank_French.CCD";
            //string ccdPath = @"C:\Transfer\100090_test_Aaab.txt";
            //string ccdPath = @"C:\Transfer\Kravitz_Kelly.txt";
            //string ccdPath = @"C:\Transfer\Kosser_Vince.txt";
            //string ccdPath = @"C:\Transfer\Rita_Ortenza.xml";
            //string ccdPath = @"C:\Transfer\ccda-eric-english-249.xml";
            //string ccdPath = @"C:\Transfer\Isabella_Jones.xml";
            string ccdPath = @"C:\Transfer\IsabellaJonesTransitionOfCareClaimTrak.xml";
            //string ccdPath = @"C:\Users\Steve\Downloads\CCD 102.xml";

            //byte[] ccdBytes = File.ReadAllBytes(ccdPath);
            
            FileStream fs = new FileStream(ccdPath, FileMode.Open, FileAccess.Read);
            byte[] filebytes = new byte[fs.Length];
            fs.Read(filebytes, 0, Convert.ToInt32(fs.Length));
            //string ccdBytes = Convert.ToBase64String(filebytes, Base64FormattingOptions.InsertLineBreaks);


            ISPatientService.CCDPostData ccdPostData = new ISPatientService.CCDPostData();
            ccdPostData.AMRPatientId = Convert.ToInt64(nudPatientId.Value);
            ccdPostData.Document = filebytes;

            ISPatientService.PatientWS patientService = new ISPatientService.PatientWS();
            ISPatientService.CCDPostResponse ccdPostResponse = patientService.CCDPost(GlobalVar.FacilityId, GlobalVar.UserId, GlobalVar.Token, ccdPostData);

            if (ccdPostResponse.Valid == false)
                MessageBox.Show(ccdPostResponse.ErrorMessage);

            //consoleMsg = string.Format("CCDPost Response: {0} {1}", ccdPostResponse.Valid.ToString(), ccdPostResponse.ErrorMessage);
            //Console.WriteLine(consoleMsg);

            //Console.ReadKey();


        }


        static void postCCD(string practiceNameinURL, string practicePassword, long patientId, long? visitId, long patientExternalId2)
        {

            //OfflineFileSyncProgram.picassoPatientService.PatientService picassoPatientService = new OfflineFileSyncProgram.picassoPatientService.PatientService();



            //string errorMessage = "";

            //string ccdStr = picassoPatientService.GetCCD_NoSession(practicePassword, patientId, visitId, out errorMessage);



            //if (errorMessage != "")

            //    throw new Exception(errorMessage);



            //if (ccdStr == "")
            //{

            //    throw new Exception("Picasso PatientService.GetCCD_NoSession() returned empty string.");

            //}



            //byte[] ccdBytes = GetBytes(ccdStr);

            //CCDPostData ccdPostData = new CCDPostData();

            //ccdPostData.AMRPatientId = patientExternalId2;

            //ccdPostData.Document = ccdBytes;



            //PatientWS amrPatientService = new PatientWS();

            //CCDPostResponse ccdPostResponse = amrPatientService.CCDPost(facilityId, userId, token, ccdPostData);



            //if (!ccdPostResponse.Valid)

            //    throw new Exception(ccdPostResponse.ErrorMessage);

        }





        static byte[] GetBytes(string str)
        {

            return System.Text.Encoding.UTF32.GetBytes(str);



            //byte[] bytes = new byte[str.Length * sizeof(char)];

            //System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);

            //return bytes;

        }



        private static byte[] DiskToBytes(string filename)
        {
            try
            {
                FileStream stream = new FileStream(filename, FileMode.Open);
                byte[] bytes = new byte[stream.Length];
                stream.Read(bytes, 0, (int)stream.Length);
                stream.Close();
                return bytes;
            }
            catch (Exception ex)
            {
                //EventLogger.WriteEntry("MX2", ex.Message, EventLogEntryType.Error, 742);
                return new byte[0];
            }
        }

    }
}
