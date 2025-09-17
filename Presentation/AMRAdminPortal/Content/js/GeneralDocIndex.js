$(document).ready(function () {
    $("#tbl-GeneralDocuments").fixedHeaderTable();
    $("#tbl-InsurancePolicy").fixedHeaderTable();
    $("#tbl-ProfessionalAdvisor").fixedHeaderTable();
});

jQuery(function ($) {

    $("#txtIPPhone").mask("(999) 999-9999");

    $("#txtPAOfficephone").mask("(999) 999-9999");
    $("#txtPACellphone").mask("(999) 999-9999");

    $("#txtIPFax").mask("(999) 999-9999");
    $("#txtPAFax").mask("(999) 999-9999");
    $("#txtPAZip").mask("99999");

});


function deleteGDocument(generalDocId) {

    //Testing...
    //alert($('#' + doctorID).val());

    //Showing Loader...
    ShowLoader();

    try {



        var requestData = {
            generalDocumentId: $.trim(generalDocId)
        };

        $.ajax({
            type: 'POST',
            url: 'premium-general-document-delete',
            data: JSON.stringify(requestData),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                // if success

                $("#GeneralDocuments-portlet").empty();
                $("#GeneralDocuments-portlet").html(data);
                $("#tbl-GeneralDocuments").fixedHeaderTable();
                HideLoader();

            },
            error: function (xhr, ajaxOptions, thrownError) {
                //if error

                alert('Error : ' + xhr.message);
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

function deleteInsurancePolicy(patientPolicyId) {
    //Testing...
    //alert($('#' + doctorID).val());

    //Showing Loader...
    ShowLoader();

    try {



        var requestData = {
            patientPolicyId: $.trim(patientPolicyId)
        };

        $.ajax({
            type: 'POST',
            url: 'premium-insurance-policy-delete',
            data: JSON.stringify(requestData),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                // if success

                $("#InsurancePolicy-portlet").empty();
                $("#InsurancePolicy-portlet").html(data);
                $("#tbl-InsurancePolicy").fixedHeaderTable();
                HideLoader();

            },
            error: function (xhr, ajaxOptions, thrownError) {
                //if error

                alert('Error : ' + xhr.message);
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

function deleteProfessionalAdvisor(patientAdvisorId) {
    //Testing...
    //alert($('#' + doctorID).val());

    //Showing Loader...
    ShowLoader();

    try {



        var requestData = {
            patientAdvisorId: $.trim(patientAdvisorId)
        };

        $.ajax({
            type: 'POST',
            url: 'premium-professional-advisor-delete',
            data: JSON.stringify(requestData),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                // if success

                $("#ProfessionalAdvisor-portlet").empty();
                $("#ProfessionalAdvisor-portlet").html(data);
                $("#tbl-ProfessionalAdvisor").fixedHeaderTable();

                HideLoader();

            },
            error: function (xhr, ajaxOptions, thrownError) {
                //if error

                alert('Error : ' + xhr.message);
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

function shareHideGeneralDocument(option) {
    //Testing...
    //alert(option);

    ShowLoader();

    try {

        var shareHide = StringToBoolean(option.split('|')[0]);
        var gDocumentId = option.split('|')[1];

        var requestData = {
            generalDocumentId: gDocumentId,
            doShareHide: shareHide
        };

        $.ajax({
            type: 'POST',
            url: 'premium-general-document-share',
            data: JSON.stringify(requestData),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',

            success: function (data) {
                // if success
                // alert("Success : " + data);


                $("#GeneralDocuments-portlet").empty();
                $("#GeneralDocuments-portlet").html(data);
                $("#tbl-GeneralDocuments").fixedHeaderTable();
                //$('#chk_1.CheckAll').checked = false;

                HideLoader();

            },
            error: function (xhr, ajaxOptions, thrownError) {
                //if error

                alert('Error : ' + xhr.message);
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

function processingComplete() {
    $('#page_loader1').hide();
    $('#page_loader2').hide();
    HideLoaderReportViewer();
}

function ShowLoaderReportViewer() {
    ShowLoader();
}
function HideLoaderReportViewer() {

    setTimeout(function () {

        HideLoader();

    }, 5000);
}


function ViewGDocumentAttachment(generalDocumentId) {


    var requestURL = "";

    //alert(generalDocumentId);
    var docType = generalDocumentId.split('|')[1];
    //alert(docType);

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
                       .html('<div id="page_loader2" style="display: block;"><div id="apDiv1" align="center"><table width="200" border="0" cellspacing="0" cellpadding="0"><tr><td height="70px;">&nbsp;</td></tr><tr><td align="center"><img src="Content/img/image_559926.gif" width="128" height="64" /></td></tr><tr><td style="text-align: center;" height="70px;">Please wait ...</td></tr></table></div></div><iframe id="iframeViewDoctorAttachment" onload="processingComplete();" style="border: 0px; " src="' + requestURL + '" width="100%" height="100%"></iframe>')
                       .dialog({
                           autoOpen: false,
                           show: {
                               effect: "blind",
                               duration: 1000
                           },
                           hide: {
                               effect: "blind",
                               duration: 1000
                           },
                           modal: true,
                           height: 600,
                           width: 1050,
                           title: "View General Document Attachment"
                       });
        $dialog.dialog('open');

    }
}

function ShowTextBoxITOther() {

    //alert($.trim($('#cmbInsuranceTypes').text()));

    //alert();

    if ($.trim($('#cmbIPInsuranceTypes').val()) == "-1") {
        //alert('Hi Raza');
        $('#showOther1').show();
        $('#txtITOther').show();
        $('#txtITOther').val("");
        $('#txtITOther').removeAttr("disabled");
        //$('#txtITOther').prop("disabled", false);
    }
    else {
        $('#showOther1').hide();
        //alert('Hi Raza...');
        $('#txtITOther').hide();
        $('#txtITOther').val("");
        $('#txtITOther').attr("disabled", "disabled");
        //$('#txtITOther').prop("disabled", true);
    }
}

function ShowTextBoxPAOther() {

    //alert($.trim($('#cmbInsuranceTypes').text()));

    //alert();

    if ($.trim($('#cmbPAAdvisorTypes').val()) == "-1") {
        //alert('Hi Raza');

        $('#showOther').show();
        $('#txtPAOther').show();
        $('#txtPAOther').val("");
        $('#txtPAOther').removeAttr("disabled");
        //$('#txtITOther').prop("disabled", false);
    }
    else {
        //alert('Hi Raza...');

        $('#showOther').hide();
        $('#txtPAOther').hide();
        $('#txtPAOther').val("");
        $('#txtPAOther').attr("disabled", "disabled");
        //$('#txtITOther').prop("disabled", true);
    }
}

function StringToBoolean(string) {

    switch (string.toLowerCase()) {
        case "true": case "yes": case "1": return true;
        case "false": case "no": case "0": case null: return false;
        default: return Boolean(string);
    }
}

function ClearGDocFields() {

    $('#fileName').text("");
    $('#txtGDDocumentDescription').val("");
    $('#txtGDNotes').val("");
    $('#chkGDViewable').prop('checked', false);
}

function initAttachFileUpload() {
    'use strict';
    // my - simple - upload
    $('#file_upload').fileupload({
        url: 'premium-general-document-upload',
        dataType: 'json',
        add: function (e, data) {

            jqXHRData = data
            $.each(data.files, function (index, file) {
                //alert(file.name);
                $('#fileName').text(file.name);
            });

        },
        done: function (event, data) {

            $("#GeneralDocuments-portlet").empty();
            $("#GeneralDocuments-portlet").html(data.result.Msghtml);
            $("#tbl-GeneralDocuments").fixedHeaderTable();
            HideLoader();
        },
        submit: function (e, data) {
            var $this = $(this);


            data.formData = { GeneralDocData: $('#hdnGeneralDocumentData').val() };

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

//<!-- General Document Form Initialization -->
$(function () {

    initAttachFileUpload();

    $('#btnBrowseFile').click(function () { $('#file_upload').click(); });


    var docDescription = $("#txtGDDocumentDescription"),
        notes = $("#txtGDNotes"),
        allFields = $([]).add(docDescription).add(notes),
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

    $("#form-save-general-document").dialog({
        autoOpen: false,
        show: {
            effect: "blind",
            duration: 1000
        },
        hide: {
            effect: "blind",
            duration: 1000
        },
        height: 340,
        width: 580,
        modal: true,
        buttons: {
            Upload: function () {

                var bValid = true;

                allFields.removeClass("ui-state-error");
                bValid = bValid && checkLength(docDescription, "Document Type", 1, 50);
                bValid = bValid && checkLength(notes, "Comments", 0, 256);

                if (bValid) {

                    //ajax 
                    try {



                        if (jqXHRData) {

                            var isStartUpload = true;
                            var uploadFile = jqXHRData.files[0];

                            if (!(/\.(jpg|jpeg|gif|png|doc|docx|xls|xlsx|pdf)$/i).test(uploadFile.name)) {
                                alert('You must select only image or word document or pdf file.');
                                isStartUpload = false;
                                return false;
                            } else if (uploadFile.size > 4000000) { // 4mb
                                alert('Please upload a smaller image, max size is 4 MB');
                                isStartUpload = false;
                                return false;
                            }

                            var requestData = {
                                DocumentDescription: $('#txtGDDocumentDescription').val(),
                                Notes: $('#txtGDNotes').val(),
                                Viewable: $('#chkGDViewable').is(":checked") ? "true" : "false",
                            };


                            $('#hdnGeneralDocumentData').val(JSON.stringify(requestData));

                            if (isStartUpload) {
                                jqXHRData.submit();
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
                    $(".validateTips").css('display', 'none');
                    $(this).dialog("close");
                    ShowLoader();
                    //   end ajax
                }
            },
            Close: function () {
                $(".validateTips").css('display', 'none');
                $(this).dialog("close");
            }

        }

    });

});

//<!-- Open General Document Form -->
function OpenGeneralDocumentForm() {
    $('#fileName').val("");
    $('#btnBrowseFile').val("Browse...");
    $("#form-save-general-document").dialog("open");
}

//<!-- Edit General Document Form Initialization -->
$(function () {

    var docDescription = $("#txtGDEditDocumentDescription"),
        notes = $("#txtGDEditNotes"),
        allFields = $([]).add(docDescription).add(notes),
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

    $("#form-edit-general-document").dialog({
        autoOpen: false,
        show: {
            effect: "blind",
            duration: 1000
        },
        hide: {
            effect: "blind",
            duration: 1000
        },
        height: 320,
        width: 520,
        modal: true,
        buttons: {
            Save: function () {

                var bValid = true;

                allFields.removeClass("ui-state-error");
                //bValid = bValid && checkLength(docDescription, "Document Type", 1, 50);
                //bValid = bValid && checkLength(notes, "Comments", 5, 50);

                if (bValid) {

                    //ajax 
                    try {

                        var requestData = {
                            DocumentCntr: $('#hdnGeneralDocumentId').val(),
                            DocumentDescription: $('#txtGDEditDocumentDescription').val(),
                            Notes: $('#txtGDEditNotes').val(),
                            Viewable: $('#chkGDEditViewable').is(":checked") ? "true" : "false",
                        };


                        //alert(JSON.stringify(requestData));
                        var jsonString = JSON.stringify(requestData);


                        $.ajax({
                            type: 'POST',
                            url: 'premium-general-document-upload/?GeneralDocData=' + jsonString,
                            dataType: 'json',
                            contentType: 'application/json; charset=utf-8',
                            success: function (data) {
                                // if success

                                //alert("Hi Raza!");
                                //alert(data);
                                //alert(data.Msghtml);

                                $("#GeneralDocuments-portlet").empty();
                                $("#GeneralDocuments-portlet").html(data.Msghtml);
                                $("#tbl-GeneralDocuments").fixedHeaderTable();
                                HideLoader();

                            },
                            error: function (xhr, ajaxOptions, thrownError) {
                                //if error

                                alert('Error : ' + xhr.message);
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
                    catch (err) {

                        if (err && err !== "") {
                            alert(err.message);
                            HideLoader();
                        }
                    }

                    $(".validateTips").css('display', 'none');
                    $(this).dialog("close");
                    ShowLoader();
                    //   end ajax
                }
            },
            Close: function () {
                $(".validateTips").css('display', 'none');
                $(this).dialog("close");
            }

        }

    });

});

//<!-- Open Edit General Document Form -->
function OpenEditGeneralDocumentForm(generalDocumentId) {

    //Testing...
    //alert($('#' + generalDocumentId).val());

    //string to Object Converstion...
    var obj = $.parseJSON($('#' + generalDocumentId).val());

    //Filling Data To Form Fields To Modify...
    $('#hdnGeneralDocumentId').val(generalDocumentId);
    $('#txtGDEditDocumentDescription').val(obj.DocumentDescription);
    $('#txtGDEditNotes').val(obj.Notes);
    $('#chkGDEditViewable').prop('checked', StringToBoolean($.trim(obj.Viewable)));

    //Opening and Setting Dynamic Title of Form...
    $("#form-edit-general-document").dialog('option', 'title', 'Edit Patient General Document');
    $("#form-edit-general-document").dialog("open");

    $("#form-edit-general-document").removeClass('validateTips ui-state-highlight ui-corner-all');
    $(".validateTips").empty();

}

//<!-- Insurance Policy Form Initialization -->
$(function () {


    var PolicyTypeName = $('#txtITOther'),
        Company = $('#txtIPCompany'),
        PolicyNo = $('#txtIPPolicyNo'),
        GroupNumber = $('#txtIPGroupNo'),
        PlanNumber = $('#txtIPPlanNo'),
        Agent = $('#txtIPAgent'),
        AgentPhone = $('#txtIPPhone'),
        AgentFax = $('#txtIPFax'),
        Notes = $('#txtIPNotes'),
        allFields = $([]).add(PolicyTypeName).add(Company).add(PolicyNo).add(GroupNumber).add(PlanNumber).add(Agent).add(AgentPhone).add(AgentFax).add(Notes),
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

    $("#form-save-insurance-policy").dialog
    ({
        autoOpen: false,
        show: {
            effect: "blind",
            duration: 1000
        },
        hide: {
            effect: "blind",
            duration: 1000
        },
        height: 500,
        width: 520,
        modal: true,
        buttons: {
            Save: function () {

                var bValid = true;

                allFields.removeClass("ui-state-error");
                bValid = bValid && ($('#txtITOther').is(':disabled') ? true : checkLength(PolicyTypeName, "Other", 1, 50));
                bValid = bValid && checkLength(Company, "Company", 1, 50);
                bValid = bValid && checkLength(PolicyNo, "Policy Number", 1, 50);
                bValid = bValid && checkLength(GroupNumber, "Group Number", 0, 50);
                bValid = bValid && checkLength(PlanNumber, "Plan Number", 0, 50);
                bValid = bValid && checkLength(Agent, "Agent", 0, 50);
                bValid = bValid && checkLength(AgentPhone, "Phone Number", 1, 15);
                bValid = bValid && checkLength(AgentFax, "Fax Number", 0, 15);
                bValid = bValid && checkLength(Notes, "Comments", 0, 256);

                if (bValid) {

                    try {



                        var requestData = {
                            PatientPolicyId: ($('#hdnPatientPolicyId').val().length == 0) ? 0 : $('#hdnPatientPolicyId').val(),
                            PolicyTypeId: $('#cmbIPInsuranceTypes').val(),
                            PolicyTypeName: $('#txtITOther').val(),
                            Company: $('#txtIPCompany').val(),
                            PolicyNo: $('#txtIPPolicyNo').val(),
                            GroupNumber: $('#txtIPGroupNo').val(),
                            PlanNumber: $('#txtIPPlanNo').val(),
                            Agent: $('#txtIPAgent').val(),
                            AgentPhone: $('#txtIPPhone').val(),
                            AgentFax: $('#txtIPFax').val(),
                            Notes: $('#txtIPNotes').val()
                        };



                        $.ajax({
                            type: 'POST',
                            url: 'premium-insurance-policy-save',
                            data: JSON.stringify(requestData),
                            contentType: 'application/json; charset=utf-8',
                            dataType: 'json',

                            success: function (data) {
                                // if success
                                // alert("Success : " + data);


                                $("#InsurancePolicy-portlet").empty();
                                $("#InsurancePolicy-portlet").html(data);
                                $("#tbl-InsurancePolicy").fixedHeaderTable();
                                //$('#chk_1.CheckAll').checked = false;

                                HideLoader();

                            },
                            error: function (xhr, ajaxOptions, thrownError) {
                                //if error

                                alert('Error : ' + xhr.message);
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

                    $(".validateTips").css('display', 'none');
                    $(this).dialog("close");
                    ShowLoader();
                }
            },
            Close: function () {
                $(".validateTips").css('display', 'none');
                $(this).dialog("close");
            }

        }

    });

});

//<!-- Open Insurance Policy Form -->
function OpenInsurancePolicyForm() {
    $('#showOther1').hide();
    $('#txtITOther').hide();
    $('#hdnPatientPolicyId').val("");
    $('#txtITOther').attr('disabled', 'disabled');
    $('#txtITOther').val("");


    //Opening and Setting Dynamic Title of Form...
    $("#form-save-insurance-policy").dialog('option', 'title', 'Add Patient Insurance Policy');
    $("#form-save-insurance-policy").dialog("open");

    $("#form-save-insurance-policy").removeClass('validateTips ui-state-highlight ui-corner-all');
    $(".validateTips").empty();

}

//<!-- Open Edit Insurance Policy Form -->
function OpenEditInsurancePolicyForm(patientPolicyId) {

    //Testing...
    //alert($('#' + generalDocumentId).val());

    //string to Object Converstion...
    var obj = $.parseJSON($('#' + patientPolicyId).val());

    //Filling Data To Form Fields To Modify...
    $('#hdnPatientPolicyId').val(patientPolicyId);
    $('#cmbIPInsuranceTypes').val(obj.PolicyTypeId);
    $('#txtITOther').val(obj.PolicyTypeName);
    if (parseInt(obj.PolicyTypeId) == -1) {
        $('#txtITOther').removeAttr('disabled');
    }
    else {
        $('#txtITOther').attr('disabled', 'disabled');
        $('#txtITOther').val("");
    }
    $('#txtIPCompany').val(obj.Company);
    $('#txtIPPolicyNo').val(obj.PolicyNo);
    $('#txtIPGroupNo').val(obj.GroupNumber);
    $('#txtIPPlanNo').val(obj.PlanNumber);
    $('#txtIPAgent').val(obj.Agent);
    $('#txtIPPhone').val(obj.AgentPhone);
    $('#txtIPFax').val(obj.AgentFax);
    $('#txtIPNotes').val(obj.Notes);

    //Opening and Setting Dynamic Title of Form...
    $("#form-save-insurance-policy").dialog('option', 'title', 'Edit Patient Insurance Policy');
    $("#form-save-insurance-policy").dialog("open");

    $("#form-save-insurance-policy").removeClass('validateTips ui-state-highlight ui-corner-all');
    $(".validateTips").empty();

}

//<!-- Professional Advisor Form Initialization -->
$(function () {

    var AdvisorDesc = $('#txtPAOther'),
        Name = $('#txtPAAdvisorName'),
        ContactAgent = $('#txtPAAgent'),
        Address1 = $('#txtPAAddress1'),
        City = $('#txtPACity'),
        State = $('#cmbPAStates'),
        PostalCode = $('#txtPAZip'),
        CountryCode = $('#cmbPACountry'),
        WorkPhone = $('#txtPAOfficephone'),
        MobilePhone = $('#txtPACellphone'),
        Fax = $('#txtPAFax'),
        EMail = $('#txtPAEmail'),
        Notes = $('#txtPANotes'),
        allFields = $([]).add(AdvisorDesc).add(Name).add(ContactAgent).add(Address1).add(City).add(State).add(PostalCode).add(CountryCode).add(WorkPhone).add(MobilePhone).add(Fax).add(EMail).add(Notes),
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

    $("#form-save-professional-advisor").dialog
    ({
        autoOpen: false,
        show: {
            effect: "blind",
            duration: 1000
        },
        hide: {
            effect: "blind",
            duration: 1000
        },
        height: 635,
        width: 520,
        modal: true,
        buttons: {
            Save: function () {

                var bValid = true;

                allFields.removeClass("ui-state-error");
                bValid = bValid && ($('#txtPAOther').is(':disabled') ? true : checkLength(AdvisorDesc, "Other", 1, 50));
                bValid = bValid && checkLength(Name, "Name", 1, 50);
                bValid = bValid && checkLength(ContactAgent, "Agent", 0, 50);
                bValid = bValid && checkLength(Address1, "Address 1", 5, 50);
                bValid = bValid && checkLength(City, "City", 1, 30);
                bValid = bValid && checkLength(State, "States", 3, 3);
                bValid = bValid && checkLength(PostalCode, "Zip", 5, 12);
                bValid = bValid && checkLength(CountryCode, "Country", 0, 3);
                bValid = bValid && checkLength(WorkPhone, "Office Phone", 8, 15);
                bValid = bValid && checkLength(MobilePhone, "Cell Phone", 0, 15);
                bValid = bValid && checkLength(Fax, "Fax", 0, 15);
                bValid = bValid && checkLength(EMail, "EMail", 0, 50);
                bValid = bValid && checkLength(Notes, "Comments", 0, 256);

                if (bValid) {


                    try {

                        //alert("Hi Raza");
                        //alert($('#txtPAOther').val());


                        var requestData = {
                            PatientAdvisorId: ($('#hdnPatientAdvisorId').val().length == 0) ? 0 : $('#hdnPatientAdvisorId').val(),
                            AdvisorId: $('#cmbPAAdvisorTypes').val(),
                            AdvisorDesc: $('#txtPAOther').val(),
                            Name: $('#txtPAAdvisorName').val(),
                            ContactAgent: $('#txtPAAgent').val(),
                            Address1: $('#txtPAAddress1').val(),
                            Address2: $('#txtPAAddress2').val(),
                            City: $('#txtPACity').val(),
                            State: $('#cmbPAStates').val(),
                            PostalCode: $('#txtPAZip').val(),
                            CountryCode: $('#cmbPACountry').val(),
                            WorkPhone: $('#txtPAOfficephone').val(),
                            MobilePhone: $('#txtPACellphone').val(),
                            Fax: $('#txtPAFax').val(),
                            EMail: $('#txtPAEmail').val(),
                            Notes: $('#txtPANotes').val(),
                        };


                        //alert("Hi Raza, Request Data", JSON.stringify(requestData));

                        $.ajax({
                            type: 'POST',
                            url: 'premium-professional-advisor-save',
                            data: JSON.stringify(requestData),
                            contentType: 'application/json; charset=utf-8',
                            dataType: 'json',

                            success: function (data) {
                                // if success
                                // alert("Success : " + data);


                                $("#ProfessionalAdvisor-portlet").empty();
                                $("#ProfessionalAdvisor-portlet").html(data);
                                $("#tbl-ProfessionalAdvisor").fixedHeaderTable();
                                //$('#chk_1.CheckAll').checked = false;

                                HideLoader();

                            },
                            error: function (xhr, ajaxOptions, thrownError) {
                                //if error

                                alert('Error : ' + xhr.message);
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
                    $(".validateTips").css('display', 'none');
                    $(this).dialog("close");
                    ShowLoader();
                }

            },
            Close: function () {
                $(".validateTips").css('display', 'none');
                $(this).dialog("close");
            }

        }

    });

});

//<!-- Open Professional Advisor Form -->
function OpenProfessionalAdvisorForm() {
    $('#showOther').hide();
    $('#txtPAOther').hide();
    $('#cmbPACountry').val('USA');
    $('#hdnPatientAdvisorId').val("");
    $('#txtPAOther').attr('disabled', 'disabled');
    $('#txtPAOther').val("");

    //Opening and Setting Dynamic Title of Form...
    $("#form-save-professional-advisor").dialog('option', 'title', 'Add Patient Professional Advisor');
    $("#form-save-professional-advisor").dialog("open");

    $("#form-save-professional-advisor").removeClass('validateTips ui-state-highlight ui-corner-all');
    $(".validateTips").empty();

}

//<!-- Open Edit Professional Advisor Form -->
function OpenEditProfessionalAdvisorForm(patientAdvisorId) {

    //Testing...
    // alert($('#' + patientAdvisorId).val());

    //string to Object Converstion...
    var obj = $.parseJSON($('#' + patientAdvisorId).val());

    //Filling Data To Form Fields To Modify...
    $('#hdnPatientAdvisorId').val(patientAdvisorId);
    $('#cmbPAAdvisorTypes').val(obj.AdvisorId);
    if (parseInt(obj.AdvisorId) == -1) {
        $('#txtPAOther').removeAttr('disabled');
    }
    else {
        $('#txtPAOther').attr('disabled', 'disabled');
        $('#txtPAOther').val("");
    }

    // alert(obj.AdvisorId);
    $('#txtPAOther').val(obj.AdvisorDesc);
    //  alert(obj.AdvisorDesc);
    $('#txtPAAdvisorName').val(obj.Name);
    $('#txtPAAgent').val(obj.ContactAgent);
    $('#txtPAAddress1').val(obj.Address1);
    $('#txtPAAddress2').val(obj.Address2);
    $('#txtPACity').val(obj.City);
    $('#cmbPAStates').val(obj.State);
    $('#txtPAZip').val(obj.PostalCode);
    $('#cmbPACountry').val(obj.CountryCode);
    $('#txtPAOfficephone').val(obj.WorkPhone);
    $('#txtPACellphone').val(obj.MobilePhone);
    $('#txtPAFax').val(obj.Fax);
    $('#txtPAEmail').val(obj.EMail);
    $('#txtPANotes').val(obj.Notes);



    //Opening and Setting Dynamic Title of Form...
    $("#form-save-professional-advisor").dialog('option', 'title', 'Edit Patient Professional Advisor');
    $("#form-save-professional-advisor").dialog("open");

    $("#form-save-professional-advisor").removeClass('validateTips ui-state-highlight ui-corner-all');
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
    //$('#MyMedicalDocumentRecords').removeClass('ui-state-hover ');

}

function removing_class1() {
    //$('#MyMedicalDocumentRecords').removeClass('ui-state-hover ');

}