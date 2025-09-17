using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AMR.Data;

using System.IO;



namespace Testing
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            AuthenticationService.AuthenticationWS AuthenticationWS = new AuthenticationService.AuthenticationWS();

            AuthenticationWS.LogoutUser(GlobalVar.FacilityId, GlobalVar.UserId);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            frmPatientData DispForm = new frmPatientData();
            DispForm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            frmLab DispForm = new frmLab();
            DispForm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmPatient DispForm = new frmPatient();
            DispForm.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            frmISPatient  DispForm = new frmISPatient();
            DispForm.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            frmISProvider DispForm = new frmISProvider();
            DispForm.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            frmISAuthenticate DispForm = new frmISAuthenticate();
            DispForm.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            frmAuthentication DispForm = new frmAuthentication();
            DispForm.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            frmProvider DispForm = new frmProvider();
            DispForm.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //frmISCCDUpload DispForm = new frmISCCDUpload();
            frmISVisitUpload DispForm = new frmISVisitUpload();
            DispForm.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            frmMessage DispForm = new frmMessage();
            DispForm.Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            frmISMessage DispForm = new frmISMessage();
            DispForm.Show();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            frmISDocument DispForm = new frmISDocument();
            DispForm.Show();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            frmISLab DispForm = new frmISLab();
            DispForm.Show();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            frmPractice DispForm = new frmPractice();
            DispForm.Show();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            frmOrganization DispForm = new frmOrganization();
            DispForm.Show();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            frmFacility DispForm = new frmFacility();
            DispForm.Show();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            frmEmergency DispForm = new frmEmergency();
            DispForm.Show();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            frmDocument DispForm = new frmDocument();
            DispForm.Show();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            frmEmail DispForm = new frmEmail();
            DispForm.Show();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            frmISCCDUpload DispForm = new frmISCCDUpload();
            DispForm.Show();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            frmPatientLogin DispForm = new frmPatientLogin();
            DispForm.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button22_Click(object sender, EventArgs e)
        {
            frmCCDGenerate DispForm = new frmCCDGenerate();
            DispForm.Show();
        }

        private void button23_Click(object sender, EventArgs e)
        {
            frmCCDDirect DispForm = new frmCCDDirect();
            DispForm.Show();
        }

        private void button24_Click(object sender, EventArgs e)
        {
            frmAdvisor DispForm = new frmAdvisor();
            DispForm.Show();
        }

        private void button25_Click(object sender, EventArgs e)
        {
            frmDoctor DispForm = new frmDoctor();
            DispForm.Show();
        }

        private void button26_Click(object sender, EventArgs e)
        {
            frmCareProvider DispForm = new frmCareProvider();
            DispForm.Show();
        }

        private void button27_Click(object sender, EventArgs e)
        {
            frmMedicalPortfolio DispForm = new frmMedicalPortfolio();
            DispForm.Show();
        }

        private void button28_Click(object sender, EventArgs e)
        {
            frmBillRate DispForm = new frmBillRate();
            DispForm.Show();
        }

        private void button29_Click(object sender, EventArgs e)
        {
            frmBill DispForm = new frmBill();
            DispForm.Show();
        }

        private void button30_Click(object sender, EventArgs e)
        {
            frmReporting DispForm = new frmReporting();
            DispForm.Show();
        }

        private void button31_Click(object sender, EventArgs e)
        {
            frmSocialSelf DispForm = new frmSocialSelf();
            DispForm.Show();
        }

        private void button32_Click(object sender, EventArgs e)
        {
            frmISIntegration DispForm = new frmISIntegration();
            DispForm.Show();
        }

        private void button33_Click(object sender, EventArgs e)
        {
            frmDirectImport DispForm = new frmDirectImport();
            DispForm.Show();
        }

        private void button34_Click(object sender, EventArgs e)
        {
            frmISSendCDA DispForm = new frmISSendCDA();
            DispForm.Show();
        }

    }
}
