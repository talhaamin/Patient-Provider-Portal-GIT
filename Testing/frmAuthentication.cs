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
    public partial class frmAuthentication : Form
    {
        public frmAuthentication()
        {
            InitializeComponent();
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            AuthenticationService.AuthenticationWS PatinetWS = new AuthenticationService.AuthenticationWS();
            GlobalVar.FacilityId = 4;
            Int64 FacilityId = 4;
            int LoginType = 0;

            AuthenticationService.AuthenticationResponse ResponseData = new AuthenticationService.AuthenticationResponse();

            try
            {
                ResponseData = PatinetWS.AuthenticateInterface(FacilityId, txtLogin.Text, txtPassword.Text);

                if (ResponseData.Valid)
                {
                    txtToken.Text = ResponseData.Token;
                    GlobalVar.UserId = ResponseData.UserId;
                    GlobalVar.Token = ResponseData.Token;
                    LoginType = ResponseData.LoginType;
                    if (LoginType == 5)
                        GlobalVar.FacilityId = 0;
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

    }
}
