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
    public partial class frmBill : Form
    {
        public frmBill()
        {
            InitializeComponent();
        }

        private void cmdRead_Click(object sender, EventArgs e)
        {
            PatientService.PatientWS PatientWS = new PatientService.PatientWS();
            PatientService.BillPaymentParms BillPaymentParms = new PatientService.BillPaymentParms();
            PatientService.BillPaymentData BillPaymentData = new PatientService.BillPaymentData();

            if (Convert.ToInt32(nudBillPmtId.Value) > 0)
            {
                BillPaymentParms.BillPaymentId = Convert.ToInt32(nudBillPmtId.Value);

                BillPaymentData = PatientWS.GetBillPaymentData(BillPaymentParms, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);
            }
            
            if (BillPaymentData.Valid)
            {
                nudBillPmtId.Value = BillPaymentData.BillPaymentId;
                nudPatientId.Value = BillPaymentData.PatientId;
                nudBillRateId.Value = BillPaymentData.BillRateId;
                txtName.Text = BillPaymentData.AccountHolderName;
                nudRate.Value = BillPaymentData.Amount;

            }
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            PatientService.PatientWS PatientWS = new PatientService.PatientWS();
            PatientService.BillPaymentData BillPaymentData = new PatientService.BillPaymentData();

            try
            {
                BillPaymentData.BillPaymentId = Convert.ToInt64(nudBillPmtId.Value);
                BillPaymentData.PatientId = Convert.ToInt64(nudPatientId.Value);
                BillPaymentData.BillRateId = Convert.ToInt32(nudBillRateId.Value);
                BillPaymentData.TransactionDate = System.DateTime.Now.ToString();
                BillPaymentData.StartDate = System.DateTime.Now.ToString();
                BillPaymentData.Response = "00";
                BillPaymentData.PaymentType = "CC";
                BillPaymentData.AccountHolderName = txtName.Text;
                BillPaymentData.Amount = nudRate.Value;

                BillPaymentData = PatientWS.SaveBillPaymentData(BillPaymentData, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);

                if (BillPaymentData.Valid == false)
                    MessageBox.Show(BillPaymentData.ErrorMessage);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Saving");
            }
        }
    }
}
