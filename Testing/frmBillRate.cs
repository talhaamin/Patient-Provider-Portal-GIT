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
    public partial class frmBillRate : Form
    {
        public frmBillRate()
        {
            InitializeComponent();
        }

        private void cmdRead_Click(object sender, EventArgs e)
        {
            PatientService.PatientWS PatientWS = new PatientService.PatientWS();
            PatientService.BillRateParms BillRateParms = new PatientService.BillRateParms();
            PatientService.BillRateData BillRateData = new PatientService.BillRateData();

            if (Convert.ToInt32(nudBillRateId.Value) > 0)
            {
                BillRateParms.BillRateId = Convert.ToInt32(nudBillRateId.Value);

                BillRateData = PatientWS.GetBillRateData(BillRateParms, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);
            }
            else if (txtPromoCode.Text != "")
            {
                BillRateParms.PromoCode = txtPromoCode.Text;

                BillRateData = PatientWS.GetBillRateDataByPromo(BillRateParms, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);
            }
            else
            {
                BillRateData.Valid = false;
            }
            if (BillRateData.Valid)
            {
                nudBillRateId.Value = BillRateData.BillRateId;
                txtPromoCode.Text = BillRateData.PromoCode;
                chkActive.Checked = BillRateData.Active;
                chkIsFree.Checked = BillRateData.IsFree;
                nudRate.Value = BillRateData.Rate;
                nudRenewal.Value = BillRateData.Renewal;
                nudResidual.Value = BillRateData.Residual;
                nudFreeMonth.Value = BillRateData.FreeMonths;
            }
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            PatientService.PatientWS PatientWS = new PatientService.PatientWS();
            PatientService.BillRateData BillRateData = new PatientService.BillRateData();

            try
            {
                BillRateData.BillRateId = Convert.ToInt32(nudBillRateId.Value);
                BillRateData.PromoCode = txtPromoCode.Text;
                BillRateData.Active = chkActive.Checked;
                BillRateData.IsFree = chkIsFree.Checked;
                BillRateData.Rate = nudRate.Value;
                BillRateData.Renewal = nudRenewal.Value;
                BillRateData.Residual = nudResidual.Value;
                BillRateData.FreeMonths = Convert.ToInt32(nudFreeMonth.Value);
                BillRateData = PatientWS.SaveBillRateData(BillRateData, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);

                if (BillRateData.Valid == false)
                    MessageBox.Show(BillRateData.ErrorMessage);
                else
                    nudBillRateId.Value = BillRateData.BillRateId;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Saving");
            }
        }
    }
}
