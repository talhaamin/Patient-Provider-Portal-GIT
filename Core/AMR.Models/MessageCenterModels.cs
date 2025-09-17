using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using AMR.Data;

namespace AMR.Models
{
  
    public class MessageCenterModel
    {
        public List<PatientMessageModel> Messages;
        public List<ProviderMessageModel> ProviderMessages;
        public List<ProviderMessageModel> ProviderSentMessages;
        public MessageModel Message;
        public List<ProviderModel> Providers;
        public List<FacilityModel> Facilities;
        public List<FacilityModel> MessageFacilities;
        public List<MessageTypeModel> MessageType;
        public List<MessageUrgency> MessageUrgencyType;
        public List<PatientListModel> PatientList;
        public MessageAttachmentModel MessageAttachment;
        public List<MessageStatusModel> MessageStatus;
        public List<MedicationListModel> MedicationList;
        public MessageUnreadModel UnreadMessages;
        public List<PatientMedicationModel> Medications;
        public List<PatientPharmacyModel> PharmciesForRefill;
        public List<SecurityQuestionModel> SecurityQuestion;
        public MessageCenterModel()
        {
            Messages = new List<PatientMessageModel>();
            ProviderMessages = new List<ProviderMessageModel>();
            ProviderSentMessages = new List<ProviderMessageModel>();
            Message = new MessageModel();
            Providers = new List<ProviderModel>();
            Facilities = new List<FacilityModel>();
            MessageFacilities = new List<FacilityModel>();
            MessageType = new List<MessageTypeModel>();
            MessageUrgencyType = new List<MessageUrgency>();
            PatientList = new List<PatientListModel>();
            MessageAttachment = new MessageAttachmentModel();
            MessageStatus = new List<MessageStatusModel>();
            MedicationList = new List<MedicationListModel>();
            UnreadMessages = new MessageUnreadModel();
            Medications = new List<PatientMedicationModel>();
            PharmciesForRefill = new List<PatientPharmacyModel>();
            SecurityQuestion = new List<SecurityQuestionModel>();
        }

    }
    
}
