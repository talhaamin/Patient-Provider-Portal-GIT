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
    public partial class frmReporting : Form
    {
        public frmReporting()
        {
            InitializeComponent();
        }

        private void cmdSecureMessage_Click(object sender, EventArgs e)
        {
            ISReportService.ReportWS ReportWS = new ISReportService.ReportWS();
            ISReportService.SecureMessageParms MessageParms = new ISReportService.SecureMessageParms();
            ISReportService.SecureMessageResponse MessageResp = new ISReportService.SecureMessageResponse();

            try
            {
                MessageParms.FacilityId = Convert.ToInt64(nudFacilityId.Value);
                MessageParms.LastCheckTD = dtpSynchDate.Value;
                MessageResp = ReportWS.SecureMessage(GlobalVar.FacilityId, GlobalVar.UserId, GlobalVar.Token, MessageParms);
                if (MessageResp.Valid)
                {
                    BindingSource bs = new BindingSource();
                    bs.DataSource = MessageResp.dtMessages;
                    dgMessages.DataSource = bs;
                }
                else
                {
                    MessageBox.Show(MessageResp.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving new messages.");
            }
        }

        private void cmdPatientLogin_Click(object sender, EventArgs e)
        {
            ISReportService.ReportWS ReportWS = new ISReportService.ReportWS();
            ISReportService.LoginLogParms MessageParms = new ISReportService.LoginLogParms();
            ISReportService.LoginLogResponse MessageResp = new ISReportService.LoginLogResponse();

            try
            {
                MessageParms.FacilityId = Convert.ToInt64(nudFacilityId.Value);
                MessageParms.LastCheckTD = dtpSynchDate.Value;
                MessageResp = ReportWS.PatientLogin(GlobalVar.FacilityId, GlobalVar.UserId, GlobalVar.Token, MessageParms);
                if (MessageResp.Valid)
                {
                    BindingSource bs = new BindingSource();
                    bs.DataSource = MessageResp.dtLogins;
                    dgMessages.DataSource = bs;
                }
                else
                {
                    MessageBox.Show(MessageResp.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving new messages.");
            }
        }
    }
}
