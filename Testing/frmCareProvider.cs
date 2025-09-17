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
    public partial class frmCareProvider : Form
    {
        public frmCareProvider()
        {
            InitializeComponent();
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            AuthenticationService.AuthenticationWS PatinetWS = new AuthenticationService.AuthenticationWS();

            AuthenticationService.AuthenticationResponse ResponseData = new AuthenticationService.AuthenticationResponse();

            try
            {
                ResponseData = PatinetWS.AuthenticateCareProvider(txtLogin.Text, txtFullName.Text, txtPassword.Text);

                if (ResponseData.Valid)
                {
                    txtToken.Text = ResponseData.Token;
                    GlobalVar.UserId = ResponseData.UserId;
                    GlobalVar.Token = ResponseData.Token;

                }
                else
                {
                    txtToken.Text = "";
                    MessageBox.Show(ResponseData.ErrorMessage);
                }
            }
            catch
            {
            }
        }
    }
}
