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

namespace Testing
{
    public partial class frmISMessage : Form
    {
        Int64 mMessageId = 0;

        public frmISMessage()
        {
            InitializeComponent();
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            ISMessageService.MessageWS MessageWS = new ISMessageService.MessageWS();
            ISMessageService.MessageData MessageData = new ISMessageService.MessageData();

            try
            {
                MessageData.MessageId = Convert.ToInt64(nudMessageId.Value);
                MessageData.ProviderId = Convert.ToInt64(nudProviderId.Value);
                MessageData.MessageResponse = txtResponse.Text;
                MessageData.AppointmentStart = dtpStartDate.Value.ToShortDateString() + " " + dtpStartTime.Value.ToShortTimeString();
                MessageData.AppointmentEnd = dtpEndDate.Value.ToShortDateString() + " " + dtpEndTime.Value.ToShortTimeString();
                MessageData.AppointmentProviderId = Convert.ToInt64(nudApptProv.Value);
                MessageData.MedicationNDC = "";
                MessageData.MedicationName = "";
                MessageData.NoOfRefills = 0;
                MessageData.MedicationStatus = 0;
                MessageData.PharmacyName = "";
                MessageData.PharmacyAddress = "";
                MessageData.AttachmentName = txtFileName.Text;

                if (MessageData.AttachmentName != "")
                {
                    MessageData.Attachment = DiskToBase64(txtFullName.Text);
                    //byte[] bytes = DiskToBytes(txtFullName.Text);
                    //MessageData.Attachment = bytes;
                }
                

                MessageWS.MessagePost(GlobalVar.FacilityId, GlobalVar.UserId, GlobalVar.Token, MessageData);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Saving");
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
        private static string DiskToBase64(string filename)
        {
            string base64String = "";
            try
            {
                FileStream stream = new FileStream(filename, FileMode.Open);
                byte[] bytes = new byte[stream.Length];
                stream.Read(bytes, 0, (int)stream.Length);
                stream.Close();
                base64String = Convert.ToBase64String(bytes);
                return base64String;
            }
            catch (Exception ex)
            {
                //EventLogger.WriteEntry("MX2", ex.Message, EventLogEntryType.Error, 742);
                return base64String;
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

        private void cmdLoad_Click(object sender, EventArgs e)
        {
            ISMessageService.MessageWS MessageWS = new ISMessageService.MessageWS();
            ISMessageService.MessageTableData MessageData = new ISMessageService.MessageTableData();

            try
            {
                MessageData = MessageWS.MessageGetWaiting(GlobalVar.FacilityId, GlobalVar.UserId, GlobalVar.Token);
                if (MessageData.Valid)
                {
                    BindingSource bs = new BindingSource();
                    bs.DataSource = MessageData.dt;
                    dgMessages.DataSource = bs;
                }
                else
                {
                    MessageBox.Show(MessageData.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving new messages.");
            }
        }

        private void dgMessages_Click(object sender, EventArgs e)
        {
            if (dgMessages.Rows.Count > 0)
            {
                if (dgMessages.CurrentRow != null)
                {
                    nudMessageId.Value = Convert.ToInt64(dgMessages.CurrentRow.Cells["MessageId"].Value);
                    nudProviderId.Value = Convert.ToInt64(dgMessages.CurrentRow.Cells["ToProvider"].Value);
                    nudPatientId.Value = Convert.ToInt64(dgMessages.CurrentRow.Cells["AMRPatientId"].Value);
                    nudMessageId.Value = Convert.ToInt64(dgMessages.CurrentRow.Cells["MessageId"].Value);
                    txtRequest.Text = dgMessages.CurrentRow.Cells["MessageRequest"].Value.ToString();
                    




                    //MessageData.ProviderId = Convert.ToInt64(nudProviderId.Value);
                    //MessageData.MessageResponse = ;
                    //MessageData.AppointmentStart = dtpStartDate.Value.ToShortDateString() + " " + dtpStartTime.Value.ToShortTimeString();
                    //MessageData.AppointmentEnd = dtpEndDate.Value.ToShortDateString() + " " + dtpEndTime.Value.ToShortTimeString();
                    //MessageData.AppointmentProviderId = Convert.ToInt64(nudApptProv.Value);
                    //MessageData.MedicationNDC = "";
                    //MessageData.MedicationName = "";
                    //MessageData.NoOfRefills = 0;
                    //MessageData.MedicationStatus = 0;
                    //MessageData.PharmacyName = "";
                    //MessageData.PharmacyAddress = "";
                    //MessageData.AttachmentName = txtFileName.Text;


                    //     m.MessageId,
                    //                          m.AMRPatientId,
                    //                          m.MessageTypeId,
                    //                          m.ToProvider,
                    //                          m.MessageRequest,
                    //                          m.PreferredResponseBy,
                    //                          m.PreferredPeriod,
                    //                          m.PreferredTime,
                    //                          m.PreferredDay,
                    //                          m.VisitReason,
                    //                          m.VisitUrgency,
                    //                          m.Medication,
                    //                          m.Pharmacy




                }

                  
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ISMessageService.MessageWS MessageWS = new ISMessageService.MessageWS();
            ISMessageService.MessageData messageData = new ISMessageService.MessageData();
            ISMessageService.MessagePostResult messageResponse = new ISMessageService.MessagePostResult();

            try
            {
                //MessageData.MessageId = 856;
                //MessageData.ProviderId = 3;
                //MessageData.MessageResponse = "No way will I do that";

                //messageData.MessageId = 574;
                //messageData.ProviderId = 3;
                //messageData.AppointmentProviderId = 3;
                //messageData.AppointmentStart = "5/12/14 10:00";
                //messageData.AppointmentEnd = "5/12/14 10:15";
                //messageData.MessageResponse = "can't see you for another week";
                
                messageData.MessageId = 575;
                messageData.ProviderId = 999;
                messageData.MedicationNDC = "00173-0727";
                messageData.MedicationName = "Lexiva";
                messageData.NoOfRefills = 1;
                messageData.MedicationStatus = 0;
                messageData.PharmacyName = "CVS";
                messageData.PharmacyAddress = "123 Broadway";
                messageData.MessageResponse = "response text";

                messageResponse = MessageWS.MessagePost(GlobalVar.FacilityId, GlobalVar.UserId, GlobalVar.Token, messageData);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Saving");
            }
        }

        private void cmdReceived_Click(object sender, EventArgs e)
        {
            ISMessageService.MessageWS MessageWS = new ISMessageService.MessageWS();
            ISMessageService.MessageReceived messageReceivedData = new ISMessageService.MessageReceived();
            ISMessageService.MessagePostResult messageResponse = new ISMessageService.MessagePostResult();

            DataTable dt = new DataTable();
            // Create Rows
            dt.Columns.Add("MessageId", typeof(Int64));

            dt.TableName = "ReceivedList";
            
            if (nudReceived1.Value > 0)
                dt.Rows.Add(nudReceived1.Value.ToString());
            if (nudReceived2.Value > 0)
                dt.Rows.Add(nudReceived2.Value.ToString());
            if (nudReceived3.Value > 0)
                dt.Rows.Add(nudReceived3.Value.ToString());
        
            //messageReceivedData.dt = dt;
            //messageResponse = MessageWS.MessagePostReceived(GlobalVar.FacilityId, GlobalVar.UserId, GlobalVar.Token, messageReceivedData);

            string messages = nudReceived1.Value.ToString();
            if (nudReceived2.Value > 0)
                messages = messages + "," + nudReceived2.Value.ToString();
            if (nudReceived3.Value > 0)
                messages = messages + "," + nudReceived3.Value.ToString();

            messageResponse = MessageWS.MessagePostReceivedList(GlobalVar.FacilityId, GlobalVar.UserId, GlobalVar.Token, messages);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ISMessageService.MessageWS MessageWS = new ISMessageService.MessageWS();
            ISMessageService.MessageTableData MessageData = new ISMessageService.MessageTableData();

            try
            {
                MessageData = MessageWS.MessageGetCancelled(GlobalVar.FacilityId, GlobalVar.UserId, GlobalVar.Token);
                if (MessageData.Valid)
                {
                    BindingSource bs = new BindingSource();
                    bs.DataSource = MessageData.dt;
                    dgMessages.DataSource = bs;
                }
                else
                {
                    MessageBox.Show(MessageData.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving new messages.");
            }
        }

        private void cmdCreate_Click(object sender, EventArgs e)
        {
            ISMessageService.MessageWS MessageWS = new ISMessageService.MessageWS();
            ISMessageService.NewMessageData MessageData = new ISMessageService.NewMessageData();
            ISMessageService.MessagePostResult Resp = new ISMessageService.MessagePostResult();

            try
            {
                //MessageData.FacilityId = GlobalVar.FacilityId;
                MessageData.AMRPatientIds = txtPatientIds.Text;
                MessageData.AMRProviderId = Convert.ToInt64(nudProviderId.Value);
                MessageData.MessageRequest = txtRequest.Text;
                MessageData.AttachmentName = txtFileName.Text;
                MessageData.AttachmentName = null;

                //if (MessageData.AttachmentName != "")
                //{
                //    MessageData.Attachment = DiskToBase64(txtFullName.Text);
                //    //byte[] bytes = DiskToBytes(txtFullName.Text);
                //    //MessageData.Attachment = bytes;
                //}


                Resp = MessageWS.NewMessagePost(GlobalVar.FacilityId, GlobalVar.UserId, GlobalVar.Token, MessageData);
                bool valid = Resp.Valid;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Saving");
            }
        }
    }
}
