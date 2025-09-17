using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Testing
{
    public partial class frmMessage : Form
    {
        Int64 mMessageId = 0;
        Int64 mAttachmentId = 0;
        public frmMessage()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mMessageId = 0;
            cboMessageType.Enabled = true;
            txtRequest.Enabled = true;
            txtReason.Enabled = true;
            cmdSave.Enabled = true;
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            MessageService.MessageWS MessageWS = new MessageService.MessageWS();
            MessageService.MessageData MessageData = new MessageService.MessageData();

            try
            {
                MessageData.MessageId = mMessageId;
                MessageData.PatientId = Convert.ToInt64(nudPatientId.Value);
                switch (cboMessageType.Text)
                {
                    case "Appointment Request":
                        MessageData.MessageTypeId = 1;
                        break;
                    case "Medication Refill":
                        MessageData.MessageTypeId = 2;
                        break;
                    case "Billing Message":
                        MessageData.MessageTypeId = 3;
                        break;
                    case "General Inquiry":
                        MessageData.MessageTypeId = 4;
                        break;
                    case "Referral Message":
                        MessageData.MessageTypeId = 5;
                        break;
                }
                MessageData.FacilityId = Convert.ToInt64(nudFacilityId.Value);
                MessageData.MessageStatusId = 1;
                MessageData.MessageDetailId = 0;
                MessageData.ProviderId_To = Convert.ToInt64(nudProviderId.Value);
                MessageData.ProviderId_From = 0;
                MessageData.MessageRequest = txtRequest.Text;
                MessageData.MessageResponse = txtResponse.Text;
                MessageData.MessageResponseTypeId = 1;
                MessageData.PreferredPeriod = "111";
                MessageData.PreferredTime = "";
                MessageData.PreferredWeekDay = "1100000";
                MessageData.VisitReason = txtReason.Text;
                MessageData.MessageUrgency = false;
                MessageData.AppointmentStart = Convert.ToDateTime("1/1/1900");
                MessageData.AppointmentEnd = Convert.ToDateTime("1/1/1900"); 
                MessageData.ProviderId_Appointment = 0;
                MessageData.MedicationNDC = "";
                MessageData.MedicationName = "";
                MessageData.NoOfRefills = 0;
                MessageData.MedicationStatus = 0;
                MessageData.PharmacyName = "";
                MessageData.PharmacyAddress = "";
                MessageData.AttachmentName = txtFileName.Text;

                if (MessageData.AttachmentName != "")
                {
                    byte[] bytes = DiskToBytes(txtFullName.Text);
                    MessageData.Attachment = bytes;
                }

                MessageWS.SaveMessageData(MessageData, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);


                cboMessageType.Enabled = false;
                txtRequest.Enabled = false;
                txtReason.Enabled = false;
                cmdSave.Enabled = false;
                cmdCancel.Enabled = false;

                mMessageId = 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Saving");
            }
        }

        private void cmdLoad_Click(object sender, EventArgs e)
        {
            MessageService.MessageWS MessageWS = new MessageService.MessageWS();
            MessageService.MessageParms MessageParms = new MessageService.MessageParms();
            MessageService.MessageTableData MessageData = new MessageService.MessageTableData();

            MessageParms.PatientId = Convert.ToInt64(nudPatientId.Value);

            MessageData = MessageWS.GetMessageList(MessageParms, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);
            if (MessageData.Valid)
            {
                BindingSource bs = new BindingSource();
                bs.DataSource = MessageData.dt;
                dgMessages.DataSource = bs;
            }

            MessageService.MessageUnread UnreadData = new MessageService.MessageUnread();
            UnreadData = MessageWS.GetUnreadMessageData(MessageParms, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);
            if (UnreadData.Valid)
            {
                nudMessages.Value = UnreadData.TotalMessages;
            }
        }

        private void dgMessages_Click(object sender, EventArgs e)
        {
            if (dgMessages.Rows.Count > 0)
            {
                if (dgMessages.CurrentRow != null)
                {
                    mMessageId = Convert.ToInt64(dgMessages.CurrentRow.Cells["MessageId"].Value);

                    MessageService.MessageWS MessageWS = new MessageService.MessageWS();
                    MessageService.MessageParms MessageParms = new MessageService.MessageParms();
                    MessageService.MessageData MessageData = new MessageService.MessageData();

                    MessageParms.MessageId = mMessageId;

                    MessageData = MessageWS.GetMessageData(MessageParms, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);
                    if (MessageData.Valid)
                    {
                        switch (MessageData.MessageTypeId)
                        {
                            case 1:
                                cboMessageType.Text = "Appointment Request";
                                break;
                            case 2:
                                cboMessageType.Text = "Medication Refill";
                                break;
                            case 3:
                                cboMessageType.Text = "Billing Message";
                                break;
                            case 4:
                                cboMessageType.Text = "General Inquiry";
                                break;
                            case 5:
                                cboMessageType.Text = "Referral Message";
                                break;
                        }


                        BindingSource bs = new BindingSource();
                        bs.DataSource = MessageData.dtDetails;
                        dgDetails.DataSource = bs;

                        if (MessageData.MessageStatusId == 3)
                            cmdCancel.Enabled = true;
                        else
                            cmdCancel.Enabled = false;

                        
                    }
                }
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            MessageService.MessageWS MessageWS = new MessageService.MessageWS();
            MessageService.MessageData MessageData = new MessageService.MessageData();

            try
            {
                MessageData.MessageId = mMessageId;
               

                MessageWS.CancelMessage(MessageData, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);


                cboMessageType.Enabled = false;
                txtRequest.Enabled = false;
                txtReason.Enabled = false;
                cmdSave.Enabled = false;
                cmdCancel.Enabled = false;
                cmdAttachment.Enabled = false;

                mMessageId = 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Saving");
            }
        }

        private void cmdOpenFile_Click(object sender, EventArgs e)
        {
            // Show the dialog and get result.
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                txtFileName.Text = openFileDialog1.SafeFileName;
                txtFullName.Text = openFileDialog1.FileName;

            }
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

        private void cmdAttachment_Click(object sender, EventArgs e)
        {
            MessageService.MessageWS MessageWS = new MessageService.MessageWS();
            MessageService.MessageAttachmentData MessageData = new MessageService.MessageAttachmentData();

            MessageData.MessageAttachmentId = mAttachmentId;
            MessageData = MessageWS.GetMessageAttachmentData(MessageData, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);

            MemoryStream ms = new MemoryStream(MessageData.DocumentImage);
            pictureBox1.BackgroundImage = System.Drawing.Image.FromStream(ms);
        }

        private void dgDetails_Click(object sender, EventArgs e)
        {
            if (dgDetails.Rows.Count > 0)
            {
                if (dgDetails.CurrentRow != null)
                {

                    if (dgDetails.CurrentRow.Cells["AttachmentId"].Value != null && dgDetails.CurrentRow.Cells["AttachmentId"].Value != "")
                    {
                        mAttachmentId = Convert.ToInt64(dgDetails.CurrentRow.Cells["AttachmentId"].Value);
                        cmdAttachment.Enabled = true;
                    }
                    else
                    {
                        mAttachmentId = 0;
                        cmdAttachment.Enabled = false;
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
             MessageService.MessageWS MessageWS = new MessageService.MessageWS();
            MessageService.MessageParms MessageParms = new MessageService.MessageParms();
            MessageService.MessageTableData MessageData = new MessageService.MessageTableData();

            MessageParms.PatientId = Convert.ToInt64(nudPatientId.Value);
            MessageParms.Option = 0;
            MessageData = MessageWS.GetMessageDetailReceivedList(MessageParms, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);
            if (MessageData.Valid)
            {

            }
        }


    }
}
