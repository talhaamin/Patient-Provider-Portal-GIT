$(document).ready(function () {
    $("#tbl-HealthCareDataExchange").fixedHeaderTable();
    $("#tbl-DoctorsRecord").fixedHeaderTable();
    $("#tbl-MedicalDocument").fixedHeaderTable();
});
function checkAll(obj) {

    //alert(obj.checked);
    //alert(obj.id); //id="chk_1"

    var IsChecked = obj.checked;
    var checkBoxId = "";
    if (obj.id.split('_')[1] == "1") {
        checkBoxId = "chk1";
    }
    else if (obj.id.split('_')[1] == "2") {
        checkBoxId = "chk2";
    }
    else if (obj.id.split('_')[1] == "3") {
        checkBoxId = "chk3";
    }

    var inputs = document.getElementsByTagName("input");

    for (var i = 0; i < inputs.length; i++) {

        if (inputs[i].type == "checkbox" && inputs[i].id == checkBoxId) {

            //alert("Raza");

            if (IsChecked) {
                inputs[i].checked = true;
            }
            else {
                inputs[i].checked = false;
            }
        }
    }
}

function shareHidePatientVisit(option) {




    ShowLoader();

    try {

        //alert("Hi Raza");
        /*
            var requestData = {
                FacilityName: sList,
                Viewable: shareHide
            };
        */

        //alert("Hi Raza," + option);

        var sList = option.split('|')[1] + "|" + option.split('|')[2];
        var shareHide = StringToBoolean(option.split('|')[0]);

        var requestData = {
            FacilityName: sList,
            Viewable: shareHide
        };

        $.ajax({
            type: 'POST',
            url: 'prem-mp-visit-share',
            data: JSON.stringify(requestData),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',

            success: function (data) {
                // if success
                // alert("Success : " + data);


                $("#HealthCareDataExchange-portlet").empty();
                $("#HealthCareDataExchange-portlet").html(data);
                $("#tbl-HealthCareDataExchange").fixedHeaderTable();
                //$('#chk_1.CheckAll').checked = false;

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

    } catch (err) {

        if (err && err !== "") {
            alert(err.message);
            HideLoader();
        }
    }
}
function shareHidePatientOutsideDoctor(option) {



    ShowLoader();

    try {

        //alert("Hi Raza");
        /*
        var requestData = {
            DoctorName: sList,
            Viewable: shareHide
        };
        */


        //alert(option);

        var sList = option.split('|')[1];
        var shareHide = StringToBoolean(option.split('|')[0]);

        var requestData = {
            DoctorName: sList,
            Viewable: shareHide
        };


        $.ajax({
            type: 'POST',
            url: 'prem-mp-doctor-share',
            data: JSON.stringify(requestData),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',

            success: function (data) {
                // if success
                // alert("Success : " + data);


                $("#DrsRecords-portlet").empty();
                $("#DrsRecords-portlet").html(data);
                $("#tbl-DoctorsRecord").fixedHeaderTable();
                //$('#chk_2').checked = false;

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

    } catch (err) {

        if (err && err !== "") {
            alert(err.message);
            HideLoader();
        }
    }
}
function shareHidePatientMedicalDocument(option) {
    //Testing...
    //alert(option);
    /*
    var shareHide = StringToBoolean(option);

    var sList = "";
    $('input[name="chk3[]"]').each(function () {

        if (this.checked) {
            //alert($(this).attr('data-myVal'));
            sList += $(this).attr('data-myVal') + ",";
        }
    });

    //Testing...
    //alert(sList);

    if (sList == "") {
        alert("Note : Please select record(s) first.");
        return;
    }
    */

    ShowLoader();

    try {

        //alert("Hi Raza");
        /*
        var requestData = {
            FacilityName: sList,
            Viewable: shareHide
        };
        */

        //alert(option);

        var sList = option.split('|')[1];
        var shareHide = StringToBoolean(option.split('|')[0]);

        var requestData = {
            FacilityName: sList,
            Viewable: shareHide
        };



        $.ajax({
            type: 'POST',
            url: 'prem-mp-document-share',
            data: JSON.stringify(requestData),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',

            success: function (data) {
                // if success
                // alert("Success : " + data);


                $("#PatientMedicalDocument-portlet").empty();
                $("#PatientMedicalDocument-portlet").html(data);
                $("#tbl-MedicalDocument").fixedHeaderTable();
                //$('#chk_3').checked = false;

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

    } catch (err) {

        if (err && err !== "") {
            alert(err.message);
            HideLoader();
        }
    }
}
function deletePatMedDocument(patientDocumentId) {
    chkAccess();

    ShowLoader();

    try {



        var requestData = {
            patienDocumentId: $.trim(patientDocumentId)
        };

        $.ajax({
            type: 'POST',
            url: 'prem-mp-document-delete',
            data: JSON.stringify(requestData),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                // if success

                $("#PatientMedicalDocument-portlet").empty();
                $("#PatientMedicalDocument-portlet").html(data);
                $("#tbl-MedicalDocument").fixedHeaderTable();
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

    } catch (err) {

        if (err && err !== "") {
            alert(err.message);
            HideLoader();
        }
    }

}

function StringToBoolean(string) {

    switch (string.toLowerCase()) {
        case "true": case "yes": case "1": return true;
        case "false": case "no": case "0": case null: return false;
        default: return Boolean(string);
    }
}

function processingComplete() {
    $('#page_loader1').hide();
    $('#page_loader2').hide();
    $('#page_loader3').hide();
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

    //alert(patientDocumentId);
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

function ShowLoaderReportViewer() {
    ShowLoader();
}
function HideLoaderReportViewer() {

    setTimeout(function () {

        HideLoader();

    }, 5000);
}

function initAttachFileUpload1() {
    'use strict';
    // my - simple - upload
    $('#file_upload').fileupload({
        url: 'prem-mp-document-upload',
        dataType: 'json',
        add: function (e, data) {

            jqXHRData = data
            $.each(data.files, function (index, file) {
                //alert(file.name);
                $('#fileName').text(file.name);
            });

        },
        done: function (event, data) {



            //alert("File Upload Done.");

            $("#PatientMedicalDocument-portlet").empty();
            $("#PatientMedicalDocument-portlet").html(data.result.Msghtml);
            $("#tbl-MedicalDocument").fixedHeaderTable();
            HideLoader();
        },
        submit: function (e, data) {
            var $this = $(this);


            data.formData = { PatientData: $('#hdnPatientDocumentData').val() };

            $this.fileupload('send', data);

            return false;
        },
        fail: function (event, data) {
            if (data.files[0].error) {
                alert(data.files[0].error);
            }

        }
    });
}

var jqXHRData;

//Model Form Initialization...
$(function () {

    var DocumentType = $("#txtDocumentDescription"),
      Facility = $("#txtFacilityName"),
      Physician = $("#txtDoctorName"),
      Comments = $("#txtNotes"),

      allFields = $([]).add(DocumentType).add(Facility).add(Physician).add(Comments),
      tips = $(".validateTips");
    function updateTips(t) {
        tips
        .text(t)
        .addClass("ui-state-highlight ui-corner-all");
        setTimeout(function () {
            //  tips.removeClass("ui-state-highlight", 1500);
        }, 500);
    }
    function checkLength(o, n, min, max) {
        if (o.val().length > max || o.val().length < min) {
            o.addClass("ui-state-error");
            updateTips("Length of " + n + " must be between " +
            min + " and " + max + ".");
            return false;
        } else {
            return true;
        }
    }
    function checkRegexp(o, regexp, n) {
        if (!(regexp.test(o.val()))) {
            o.addClass("ui-state-error");
            updateTips(n);
            return false;
        } else {
            return true;
        }
    }

    initAttachFileUpload1();



    $('#btnBrowseFile').click(function () { $('#file_upload').click(); });

    $("#form-Save-Medical-Document").dialog({
        autoOpen: false,
        show: {
            effect: "blind",
            duration: 100
        },
        hide: {
            effect: "blind",
            duration: 100
        },
        height: 370,
        width: 520,
        modal: true,
        buttons: {
            Upload: function () {
                chkAccess();
                var bValid = true;
                allFields.removeClass("ui-state-error");
                bValid = bValid && checkLength(DocumentType, "Document Type", 1, 50);
                //Commented by Ahmed as per Taylor's Request
                //bValid = bValid && checkLength(Facility, "Facility", 5, 50);
                //bValid = bValid && checkLength(Physician, "Physician", 5, 50);
                //bValid = bValid && checkLength(Comments, "Comments", 1, 256);


                if (bValid) {

                    //ajax 
                    try {



                        if (jqXHRData) {

                            var isStartUpload = true;
                            var uploadFile = jqXHRData.files[0];

                            if (!(/\.(gif|jpg|jpeg|png|doc|docx|xls|xlsx|pdf)$/i).test(uploadFile.name)) {
                                alert('You must select only image, word document or pdf file.');
                                isStartUpload = false;
                                return false;
                            } else if (uploadFile.size > 4000000) { // 4mb
                                alert('Please upload a smaller image, max size is 4 MB');
                                isStartUpload = false;
                                return false;
                            }

                            var requestData = {
                                DocumentDescription: $('#txtDocumentDescription').val(),
                                FacilityName: $('#txtFacilityName').val(),
                                DoctorName: $('#txtDoctorName').val(),
                                Notes: $('#txtNotes').val(),
                                Viewable: $('#chkViewable').is(":checked") ? "true" : "false",
                            };


                            $('#hdnPatientDocumentData').val(JSON.stringify(requestData));

                            if (isStartUpload) {
                                jqXHRData.submit();
                                $("#tbl-MedicalDocument").fixedHeaderTable();
                            }
                        }
                        else {
                            return false;
                        }

                    }
                    catch (err) {

                        if (err && err !== "") {
                            alert(err.message);
                            HideLoader();
                        }
                    }

                    allFields.val("").removeClass("ui-state-error");
                    $(".validateTips ").empty();
                    $("span").removeClass("ui-state-highlight");

                    $("#fileName ").empty();

                    $(this).dialog("close");
                    ShowLoader();
                }
                // bValid if end...
            },
            Close: function () {
                allFields.val("").removeClass("ui-state-error");
                $(".validateTips ").empty();
                $("#fileName ").empty();
                $("span").removeClass("ui-state-highlight");
                $(this).dialog("close");
            }

        }

    });

});
//Call to open form...
function SavePatientMedicalDocument() {
    $('#fileName').val("");
    $('#hdnPatientDocumentId').val("");

    $("#form-Save-Medical-Document").dialog("open");

}


//Edit Patient Medical Document...
//Model Form Initialization...
$(function () {

    var DocumentType = $("#txtEditDocumentDescription"),
      Facility = $("#txtEditFacilityName"),
      Physician = $("#txtEditDoctorName"),
      Comments = $("#txtEditNotes"),

      allFields = $([]).add(DocumentType).add(Facility).add(Physician).add(Comments),
      tips = $(".validateTips");
    function updateTips(t) {
        tips
        .text(t)
        .addClass("ui-state-highlight ui-corner-all");
        setTimeout(function () {
            //  tips.removeClass("ui-state-highlight", 1500);
        }, 500);
    }
    function checkLength(o, n, min, max) {
        if (o.val().length > max || o.val().length < min) {
            o.addClass("ui-state-error");
            updateTips("Length of " + n + " must be between " +
            min + " and " + max + ".");
            return false;
        } else {
            return true;
        }
    }
    function checkRegexp(o, regexp, n) {
        if (!(regexp.test(o.val()))) {
            o.addClass("ui-state-error");
            updateTips(n);
            return false;
        } else {
            return true;
        }
    }

    $("#form-Edit-Medical-Document").dialog({
        autoOpen: false,
        show: {
            effect: "blind",
            duration: 100
        },
        hide: {
            effect: "blind",
            duration: 100
        },
        height: 340,
        width: 520,
        modal: true,
        buttons: {
            Save: function () {
                chkAccess();
                var bValid = true;
                allFields.removeClass("ui-state-error");
                bValid = bValid && checkLength(DocumentType, "Document Type", 1, 50);
                bValid = bValid && checkLength(Facility, "Facility", 5, 50);
                bValid = bValid && checkLength(Physician, "Physician", 5, 50);
                bValid = bValid && checkLength(Comments, "Comments", 1, 256);


                if (bValid) {

                    try {

                        var requestData = {
                            DocumentCntr: $('#hdnPatientDocumentId').val(),
                            DocumentDescription: $('#txtEditDocumentDescription').val(),
                            FacilityName: $('#txtEditFacilityName').val(),
                            DoctorName: $('#txtEditDoctorName').val(),
                            Notes: $('#txtEditNotes').val(),
                            Viewable: $('#chkEditViewable').is(":checked") ? "true" : "false",
                        };



                        //alert(JSON.stringify(requestData));
                        var jsonString = JSON.stringify(requestData);


                        $.ajax({
                            type: 'POST',
                            url: 'prem-mp-document-upload/?patientData=' + jsonString,
                            dataType: 'json',
                            contentType: 'application/json; charset=utf-8',
                            success: function (data) {
                                // if success

                                //alert("Hi Raza!");
                                //alert(data);
                                //alert(data.Msghtml);

                                $("#PatientMedicalDocument-portlet").empty();
                                $("#PatientMedicalDocument-portlet").html(data.Msghtml);
                                $("#tbl-MedicalDocument").fixedHeaderTable();
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

                    } catch (err) {

                        if (err && err !== "") {
                            alert(err.message);
                            HideLoader();
                        }
                    }

                    allFields.val("").removeClass("ui-state-error");
                    $(".validateTips ").empty();
                    $("span").removeClass("ui-state-highlight");

                    $(this).dialog("close");
                    ShowLoader();
                }
            },
            Close: function () {
                allFields.val("").removeClass("ui-state-error");
                $(".validateTips ").empty();
                $("#fileName ").empty();
                $("span").removeClass("ui-state-highlight");
                $(this).dialog("close");
            }

        }

    });

});

function EditPatientMedicalDocument(pateintDocumentId) {

    //Testing...
    //alert($('#' + doctorID).val());

    //string to Object Converstion...
    var obj = $.parseJSON($('#' + pateintDocumentId).val());

    //Filling Data To Form Fields To Modify...
    $('#hdnPatientDocumentId').val($.trim(pateintDocumentId));

    $('#txtEditDocumentDescription').val($.trim(obj.DocumentDescription));
    $('#txtEditFacilityName').val($.trim(obj.FacilityName));

    $('#txtEditDoctorName').val($.trim(obj.DoctorName));
    $('#txtEditNotes').val($.trim(obj.Notes));

    $('#chkEditViewable').prop('checked', StringToBoolean($.trim(obj.Viewable)));

    //Opening and Setting Dynamic Title of Form...
    $("#form-Edit-Medical-Document").dialog('option', 'title', 'Edit Patient Medical Document');
    $("#form-Edit-Medical-Document").dialog("open");

    $("#form-Edit-Medical-Document").removeClass('validateTips ui-state-highlight ui-corner-all');
    $(".validateTips").empty();

}

$(function () {
    $("input[type=submit], a, button")
    .button()
    .click(function (event) {
        // event.preventDefault();
    });
});
$(document).ready(function () {


    // Call horizontalNav on the navigations wrapping element
    $('.full-width').horizontalNav(
        {
            responsive: false

        });


});
function removing_class() {
    $('#PatientMedicalDocument-portlet').removeClass('ui-state-hover ');
}
function removing_class1() {
    $('#PatientMedicalDocument-portlet').removeClass('ui-state-hover ');
}