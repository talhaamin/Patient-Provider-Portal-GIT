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

namespace Testing
{
    public partial class frmEmail : Form
    {
        public frmEmail()
        {
            InitializeComponent();
        }

        private void cmdSend_Click(object sender, EventArgs e)
        {
            clsEmail objEmail = new clsEmail();

            objEmail.SendEmail(txtTo.Text, txtSubject.Text, txtMessage.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageService.MessageWS MessageWS = new MessageService.MessageWS();
            MessageService.EmailMessageData MessageData = new MessageService.EmailMessageData();

            try
            {
                MessageData.To = "";
                MessageData.Subject = "Test Message";
                MessageData.Body = "This is a test message";
                MessageData = MessageWS.SendEmailMessage(MessageData, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);

                if (MessageData.Valid == false)
                {
                    MessageBox.Show("Error Sending");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Sending");
            }
        }
    }
}
