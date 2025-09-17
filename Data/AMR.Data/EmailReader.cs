// Service Name  : EmailReader
// Date Created  : 08/11/2014
// Written By    : Stephen Farkas
// Version       : 2014.01
// Description   : Receive an email file and break it into the appropriate parts.
// MM/DD/YYYY XXX Description               
// 
//------------------------------------------------------------------------
//  Set Options & Imports
//------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace AMR.Data
{
    public class EmailReader
    {
        //------------------------------------------------------------------------
        //  Private Variables
        //------------------------------------------------------------------------
        private string mstrXSender;
        private List<string> mlstXReceivers = new List<string>();
        private string mstrReceived;
        private string mstrMimeVersion;
        private string mstrFrom;
        private string mstrTo;
        private string mstrCC;
        private DateTime mdteDate = DateTime.MinValue;
        private string mstrSubject;
        private string mstrContentType;
        private string mstrContentTransferEncoding;
        private string mstrContentDisposition;
        private string mstrReturnPath;
        private string mstrMessageID;
        private DateTime mdteXOriginalArrivalTime = DateTime.MinValue;
        private string mstrBody;
        private string mstrHTMLBody;

        private int mintAttachmentCnt;
        private string[] mstrAttachment = new string[99];
        private string[] mstrAttachmentEncoding = new string[99];

        //------------------------------------------------------------------------
        //  Public Variables
        //------------------------------------------------------------------------
        
        public string X_Sender
        {
            get { return mstrXSender; }
        }
        public string[] X_Receivers
        {
            get
            {
                string[] X_ReceiversA = new string[mlstXReceivers.Count];
                mlstXReceivers.CopyTo(X_ReceiversA);
                return X_ReceiversA;
            }
        }
        public string Received
        {
            get { return mstrReceived; }
        }
        public string Mime_Version
        {
            get { return mstrMimeVersion; }
        }
        public string From
        {
            get { return mstrFrom; }
        }
        public string To
        {
            get { return mstrTo; }
        }
        public string CC
        {
            get { return mstrCC; }
        }
        public DateTime Date
        {
            get { return mdteDate; }
        }
        public string Subject
        {
            get { return mstrSubject; }
        }
        public string Content_Type
        {
            get { return mstrContentType; }
        }
        public string Content_Transfer_Encoding
        {
            get { return mstrContentTransferEncoding; }
        }
        public string Content_Disposition
        {
            get { return mstrContentDisposition; }
        }
        public string Return_Path
        {
            get { return mstrReturnPath; }
        }
        public string Message_ID
        {
            get { return mstrMessageID; }
        }
        public DateTime X_OriginalArrivalTime
        {
            get { return mdteXOriginalArrivalTime; }
        }
        public string Body
        {
            get { return mstrBody; }
        }
        public string HTMLBody
        {
            get { return mstrHTMLBody; }
        }
        public int AttachmentCntr
        {
            get { return mintAttachmentCnt; }
        }
        public string[] Attachment
        {
            get { return mstrAttachment; }
        }
        public string[] AttachmentEncoding
        {
            get { return mstrAttachmentEncoding; }
        }


        //------------------------------------------------------------------------
        //  ParseEmail
        //------------------------------------------------------------------------


        public EmailReader(FileStream fsEML)
        {
            ParseEML(fsEML);
        }

        private void ParseEML(FileStream fsEML)
        {
            mintAttachmentCnt = 0;
            mstrHTMLBody = string.Empty;
            mstrBody = string.Empty;
            int iStartBody = -1;
            string strContentType = "";

            StreamReader sr = new StreamReader(fsEML);
            string sLine;
            List<string> listAll = new List<string>();

            // Move the data from string reader into a list.
            while ((sLine = sr.ReadLine()) != null)
            {
                listAll.Add(sLine);
            }

            // Convert list into a string array
            string[] saAll = new string[listAll.Count];
            listAll.CopyTo(saAll);

            // Loop through email (now in a string array) parsing out header elements until a blank line is hit.

            for (int i = 0; i < saAll.Length; i++)
            {
                if (saAll[i] == string.Empty)
                 if (saAll[i] == string.Empty)
                {
                    iStartBody = i;
                    break;
                }

                string sFullValue = saAll[i];
                GetFullValue(saAll, ref i, ref sFullValue);

                string[] saHdr = Split(sFullValue);
                if (saHdr == null)  // not a valid header
                    continue;

                switch (saHdr[0].ToLower())
                {
                    case "x-sender":
                        mstrXSender = saHdr[1];
                        break;
                    case "x-receiver":
                        string[] mlstXReceivers = new string[1];
                        mlstXReceivers[0] = saHdr[1];
                        break;
                    case "received":
                        mstrReceived = saHdr[1];
                        break;
                    case "mime-version":
                        mstrMimeVersion = saHdr[1];
                        break;
                    case "from":
                        mstrFrom = saHdr[1];
                        break;
                    case "to":
                        mstrTo = saHdr[1];
                        break;
                    case "cc":
                        mstrCC = saHdr[1];
                        break;
                    case "date":
                        mdteDate = DateTime.Parse(saHdr[1]);
                        break;
                    case "subject":
                        mstrSubject = saHdr[1];
                        break;
                    case "content-type":
                        mstrContentType = saHdr[1];
                        break;
                    case "content-transfer-encoding":
                        mstrContentTransferEncoding = saHdr[1];
                        break;
                    case "content-disposition":
                        mstrContentDisposition = saHdr[1];
                        break;
                    case "return-path":
                        mstrReturnPath = saHdr[1];
                        break;
                    case "message-id":
                        mstrMessageID = saHdr[1];
                        break;
                    case "x-originalarrivaltime":
                        int ix = saHdr[1].IndexOf("FILETIME");
                        if (ix != -1)
                        {
                            string sOAT = saHdr[1].Substring(0, ix);
                            sOAT = sOAT.Replace("(UTC)", "-0000");
                            mdteXOriginalArrivalTime = DateTime.Parse(sOAT);
                        }
                        break;
                }

            }

            if (iStartBody == -1)   // no body ?
                return;




            // Check for attachment only and no body.


            if (mstrContentDisposition != null && mstrContentDisposition.ToLower().Contains("attachment"))
            {
                int AttPosn = 0;
                mintAttachmentCnt = 1;
                string strAttachment = "";

                for (int i = iStartBody + 1; i < saAll.Length; i++)
                {
                    //if (saAll[i] == string.Empty)
                    //{
                    //    // On blank line, now in next section.
                    //    iStartBody = i + 1;
                    //    break;
                    //}
                    AttPosn++;
                    if (saAll[i] != string.Empty)
                        strAttachment = strAttachment + saAll[i];

                }
                mstrAttachment[0] = strAttachment;
            }
            else    // loop through body breaking out each section.
            {
                Int16 ContentType = 0;
                bool Started = false;

                for (int i = iStartBody; i < saAll.Length; i++)
                {
                    if (saAll[i].ToLower().StartsWith("content-type"))
                    {
                        if (saAll[i].ToLower().Contains("text/html"))
                        {
                            ContentType = 1;
                            Started = false;
                        }
                        else if (saAll[i].ToLower().Contains("text/plain"))
                        {
                            ContentType = 2;
                            Started = false;
                        }
                    }
                    else if (saAll[i].ToLower().StartsWith("content-transfer-encoding"))
                    {
                        string[] saParts = Split(saAll[i]);
                        strContentType = saParts[1];
                        if (saParts != null & mintAttachmentCnt > 0)  // Only pull enoding for the attachments.
                            mstrAttachmentEncoding[mintAttachmentCnt - 1] = saParts[1];
                    }
                    else if (saAll[i].ToLower().StartsWith("content-disposition"))
                    {
                        if (saAll[i].ToLower().Contains("attachment"))
                        {
                            mintAttachmentCnt++;
                            mstrAttachment[mintAttachmentCnt - 1] = string.Empty;
                            ContentType = 3;
                            Started = false;

                            if (mstrAttachmentEncoding[mintAttachmentCnt - 1] == null)
                                mstrAttachmentEncoding[mintAttachmentCnt - 1] = strContentType;
                        }
                    }
                    else if (saAll[i].ToLower().StartsWith("------=_nextpart"))
                    {
                        // End Of Content
                        ContentType = 0;
                        Started = false;
                    }
                    else
                    {
                        // Looping through data
                        if (ContentType > 0)
                        {
                            if (!Started)
                            {
                                // Data will begin after a blank line
                                if (saAll[i] == string.Empty)
                                    Started = true;
                            }
                            else
                            {
                                // Add data to appropriate section
                                if (ContentType == 1)
                                    mstrHTMLBody = mstrHTMLBody + saAll[i];
                                else if (ContentType == 2)
                                {
                                    mstrBody = mstrBody + saAll[i];
                                    // Remove = at the end of the lines
                                    if (mstrBody.Length > 0 && mstrBody.Substring(mstrBody.Length - 1, 1) == "=")
                                        mstrBody = mstrBody.Substring(0, mstrBody.Length - 1);
                                }
                                else if (ContentType == 3)
                                {
                                    if (saAll[i] == string.Empty)
                                    {
                                        if (mstrAttachment[mintAttachmentCnt - 1] != string.Empty)
                                        //if (mstrAttachment != string.Empty)
                                        {
                                            Started = false;
                                            ContentType = 0;
                                        }
                                    }
                                    else
                                    {
                                        mstrAttachment[mintAttachmentCnt - 1] = mstrAttachment[mintAttachmentCnt - 1] + saAll[i];
                                        //mstrAttachment = mstrAttachment + saAll[i];
                                    }
                                }

                            }
                        }
                    }
                }

            }
            
        }


        private void GetFullValue(string[] sa, ref int i, ref string sValue)
        {
            if (i + 1 < sa.Length && sa[i + 1] != string.Empty && char.IsWhiteSpace(sa[i + 1], 0))   // spec says line's that begin with white space are continuation lines
            {
                i++;
                sValue += " " + sa[i].Trim();

                GetFullValue(sa, ref i, ref sValue);
            }
        }

        private string[] Split(string sHeader)  // because string.Split won't work here...
        {
            int ix;
            if((ix = sHeader.IndexOf(':')) == -1)
                return null;

            return new string[] { sHeader.Substring(0, ix).Trim(), sHeader.Substring(ix + 1).Trim() };
        }
    }
}
