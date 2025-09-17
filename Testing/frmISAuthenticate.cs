using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Testing
{
    public partial class frmISAuthenticate : Form
    {
        public frmISAuthenticate()
        {
            InitializeComponent();
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            ISAuthenticationService.AuthenticationWS PatinetWS = new ISAuthenticationService.AuthenticationWS();
            Int64 FacilityId = Convert.ToInt64(nudFacilityId.Value);
            GlobalVar.FacilityId = Convert.ToInt64(nudFacilityId.Value);
            GlobalVar.UserId = Convert.ToInt64(nudUserId.Value);

            ISAuthenticationService.AuthenticationResponse ResponseData = new ISAuthenticationService.AuthenticationResponse();
            try
            {
                ResponseData = PatinetWS.AuthenticateInterface(FacilityId, txtLogin.Text, txtPassword.Text);

                if (ResponseData.Valid)
                {
                    txtToken.Text = ResponseData.Token;
                    GlobalVar.Token = ResponseData.Token;
                }
                else
                {
                    txtToken.Text = "";
                    MessageBox.Show(ResponseData.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Saving");
            }

       }

        static string consolMsg;
        static long facilityId = 6;
        static string userLogin = "DocTest";
        static int userId = 31;
        static string password = "pass";
        static string token;


        private void button1_Click(object sender, EventArgs e)
        {
            ISAuthenticationService.AuthenticationWS authenticationService = new ISAuthenticationService.AuthenticationWS();
            ISAuthenticationService.AuthenticationResponse authenticationResponse = authenticationService.AuthenticateInterface(facilityId,userLogin, password);

            token = authenticationResponse.Token;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtLogin.Text = "DocTest";
            nudFacilityId.Value = 6;
            nudUserId.Value = 31;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            txtLogin.Text = "EclipseInterface";
            nudFacilityId.Value = 131;
            nudUserId.Value = 118;

            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            txtLogin.Text = "ClaimTrakInterface";
            nudFacilityId.Value = 20;
            nudUserId.Value = 112;
        }
    }
}
