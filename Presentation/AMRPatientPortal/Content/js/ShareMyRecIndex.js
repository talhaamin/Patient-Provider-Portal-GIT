$(document).ready(function () {
    $('#tabs').tabs();
    document.getElementById('tabs').style.display = 'block';
    var $tabs = $('#tabs').tabs();
    $("#tabs").tabs({
        activate: function (event, ui) {
            var selected = $tabs.tabs('option', 'active');

            switch (selected) {
                case 0:



                    break;
                case 1:


                    break;
                case 2:
                    checkMedical();

                    $("#tbl-SharedAllergies").fixedHeaderTable();
                    $("#tbl-SharedUploadDocs").fixedHeaderTable();
                    $("#tbl-SharedEmergency").fixedHeaderTable();
                    $("#tbl-SharedDocuments").fixedHeaderTable();
                    $("#tbl-SharedHealthRecord").fixedHeaderTable();
                    $("#tbl-SharedMedications").fixedHeaderTable();
                    $("#tbl-SharedPatientMedDocument").fixedHeaderTable();
                    $("#tbl-SharedPolicy").fixedHeaderTable();
                    $("#tbl-SharedProblems").fixedHeaderTable();

                    break;
                case 3:


                    break;
                case 4:



                    break;

            }
        }
    });


});

$("#tabs").tabs();
$(function () {
    $("input[type=submit], a, button")
    .button()
    .click(function (event) {
        // event.preventDefault();
    });
});
$(document).ready(function () {

    // Call horizontalNav on the navigations wrapping element
    $('.full-width').horizontalNav({});

});
function SavePassCode() {
    ShowLoader();
    var pass = $.trim($('#passcode2').val());
    var requestData = {
        Password: $.trim($('#passcode2').val())

    }
    $.ajax({
        type: 'POST',
        url: 'premium-passcode-save',
        data: JSON.stringify(requestData),
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {

            // if success
            debugger;
            $('#ProviderPassword').val(data);
            $('#careProviderPassword').text(data);
            HideLoader();

        },
        error: function (xhr, ajaxOptions, thrownError) {
            //if error
            var myCookie = getCookie(".Patient");

            if (myCookie == null) {
                // do cookie doesn't exist stuff;
                alert('Not logged in. Please log in to continue');
                window.location = '/Login'
                //xhr.getResponseHeader('Location');
            }
            else {

                alert('Error : ' + xhr.message);
            }
            HideLoader();
        },
        complete: function (data) {
            // if completed
            debugger;
            HideLoader();

        },
        async: true,
        processData: false
    });
}

function checkProviderLabel() {

    var Password = $('#ProviderPassword').val();
    if (Password == '') {
        $('#ProviderLabelHide').show();
        $('#ProviderLabelShow').hide();
    }
    else {
        $('#ProviderLabelShow').show();
        $('#ProviderLabelHide').hide();
    }
}
function checkMedical() {
    var trial = $('#PatientImage').attr('src');
    $('#summImg').attr("src", trial);

}

function SendEmail() {
    ShowLoader();
    var requestData = {
        EmailAddress: $.trim($('#emailto').val()),
        Password: $.trim($('#ProviderPassword').val())
    }

    $.ajax({
        type: 'POST',
        url: 'premium-send-email',
        data: JSON.stringify(requestData),
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            // if success
            HideLoader();

        },
        error: function (xhr, ajaxOptions, thrownError) {
            //if error
            var myCookie = getCookie(".Patient");

            if (myCookie == null) {
                // do cookie doesn't exist stuff;
                alert('Not logged in. Please log in to continue');
                window.location = '/Login'
                //xhr.getResponseHeader('Location');
            }
            else {

                alert('Error : ' + xhr.message);
            }
            HideLoader();
        },
        complete: function (data) {
            // if completed
            HideLoader();

        },
        async: true,
        processData: false
    });
}

function processingComplete() {
    $('#page_loader1').hide();
    $('#page_loader2').hide();
    $('#page_loader3').hide();
    $('#page_loader4').hide();
}

function ViewDoctorAttachment(outsideDoctorId) {

    chkAccess();
    var requestURL = "";

    //alert(outsideDoctorId);
    var docType = outsideDoctorId.split('|')[1];
    //alert(docType);

    requestURL = "prem-mp-doctor-attachment/?outsideDoctorId=" + outsideDoctorId.split('|')[0];

    var isOfficeDocRequest = false;

    switch (docType.toLowerCase()) {
        case "doc":
            isOfficeDocRequest = true;
            break;

        case "docx":
            isOfficeDocRequest = true;
            break;

        case "xls":
            isOfficeDocRequest = true;
            break;

        case "xlsx":
            isOfficeDocRequest = true;
            break;

        default:
            isOfficeDocRequest = false;
            break;
    }

    if (isOfficeDocRequest) {


        try {

            //alert($('#divOfficeDocIframe').val());
            ShowLoaderReportViewer();

            $('#iframeViewAttachment').attr('src', requestURL);
            $('#iframeViewAttachment').load();

        } catch (err) {

            if (err && err !== "") {
                alert(err.message);
                HideLoaderReportViewer();
            }
        }



    }
    else {

        $('#clinical-print').remove();
        var $dialog = $('<div id="clinical-print" style="width:1000px;"></div>')
                       .html('<div id="page_loader2" style="display: block;"><div id="apDiv1" align="center"><table width="200" border="0" cellspacing="0" cellpadding="0"><tr><td height="70px;">&nbsp;</td></tr><tr><td align="center"><img src="Content/img/image_559926.gif" width="128" height="64" /></td></tr><tr><td style="text-align: center;" height="70px;">Please wait ...</td></tr></table></div></div><iframe id="iframeViewDoctorAttachment" onload="processingComplete();" style="border: 0px; " src="' + requestURL + '" width="100%" height="100%"></iframe>')
                       .dialog({
                           autoOpen: false,
                           show: {
                               effect: "blind",
                               duration: 100
                           },
                           hide: {
                               effect: "blind",
                               duration: 100
                           },
                           modal: true,
                           height: 600,
                           width: 1050,
                           title: "View Doctor Document Attachment"
                       });
        $dialog.dialog('open');

    }
}

function ViewDocumentAttachment(patientDocumentId) {

    chkAccess();
    var requestURL = "";

    // alert(patientDocumentId);
    var docType = patientDocumentId.split('|')[1];
    //alert(docType);

    requestURL = "prem-mp-document-attachment/?patientDocumentId=" + patientDocumentId.split('|')[0];

    var isOfficeDocRequest = false;
    switch (docType.toLowerCase()) {
        case "doc":
            isOfficeDocRequest = true;
            break;

        case "docx":
            isOfficeDocRequest = true;
            break;

        case "xls":
            isOfficeDocRequest = true;
            break;

        case "xlsx":
            isOfficeDocRequest = true;
            break;

        default:
            isOfficeDocRequest = false;
            break;
    }

    if (isOfficeDocRequest) {


        try {

            //alert($('#divOfficeDocIframe').val());
            ShowLoaderReportViewer();

            $('#iframeViewAttachment').attr('src', requestURL);
            $('#iframeViewAttachment').load();

        } catch (err) {

            if (err && err !== "") {
                alert(err.message);
                HideLoaderReportViewer();
            }
        }



    }
    else {

        $('#clinical-print').remove();
        var $dialog = $('<div id="clinical-print" style="width:1000px;"></div>')
                       .html('<div id="page_loader3" style="display: block;"><div id="apDiv1" align="center"><table width="200" border="0" cellspacing="0" cellpadding="0"><tr><td height="70px;">&nbsp;</td></tr><tr><td align="center"><img src="Content/img/image_559926.gif" width="128" height="64" /></td></tr><tr><td style="text-align: center;" height="70px;">Please wait ...</td></tr></table></div></div><iframe id="iframeViewPatientAttachment" onload="processingComplete();" style="border: 0px; " src="' + requestURL + '" width="100%" height="100%"></iframe>')
                       .dialog({
                           autoOpen: false,
                           show: {
                               effect: "blind",
                               duration: 100
                           },
                           hide: {
                               effect: "blind",
                               duration: 100
                           },
                           modal: true,
                           height: 600,
                           width: 1050,
                           title: "View Patient Document Attachment"
                       });
        $dialog.dialog('open');


    }
}

function ViewGDocumentAttachment(generalDocumentId) {

    chkAccess();
    var requestURL = "";

    //alert(generalDocumentId);
    var docType = generalDocumentId.split('|')[1];
    alert(docType);

    requestURL = "premium-general-document-attachment/?patientDocumentId=" + generalDocumentId.split('|')[0];

    var isOfficeDocRequest = false;

    switch (docType.toLowerCase()) {
        case "doc":
            isOfficeDocRequest = true;
            break;

        case "docx":
            isOfficeDocRequest = true;
            break;

        case "xls":
            isOfficeDocRequest = true;
            break;

        case "xlsx":
            isOfficeDocRequest = true;
            break;

        default:
            isOfficeDocRequest = false;
            break;
    }

    if (isOfficeDocRequest) {


        try {


            ShowLoaderReportViewer();

            $('#iframeViewAttachment').attr('src', requestURL);
            $('#iframeViewAttachment').load();

        } catch (err) {

            if (err && err !== "") {
                alert(err.message);
                HideLoaderReportViewer();
            }
        }



    }
    else {

        $('#clinical-print').remove();
        var $dialog = $('<div id="clinical-print" style="width:1000px;"></div>')
                       .html('<div id="page_loader4" style="display: block;"><div id="apDiv1" align="center"><table width="200" border="0" cellspacing="0" cellpadding="0"><tr><td height="70px;">&nbsp;</td></tr><tr><td align="center"><img src="Content/img/image_559926.gif" width="128" height="64" /></td></tr><tr><td style="text-align: center;" height="70px;">Please wait ...</td></tr></table></div></div><iframe id="iframeViewDoctorAttachment" onload="processingComplete();" style="border: 0px; " src="' + requestURL + '" width="100%" height="100%"></iframe>')
                       .dialog({
                           autoOpen: false,
                           show: {
                               effect: "blind",
                               duration: 100
                           },
                           hide: {
                               effect: "blind",
                               duration: 100
                           },
                           modal: true,
                           height: 600,
                           width: 1050,
                           title: "View General Document Attachment"
                       });
        $dialog.dialog('open');

    }
}