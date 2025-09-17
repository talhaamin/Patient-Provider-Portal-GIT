$(document).ready(function () {
    $('#tabs').tabs();
    document.getElementById('tabs').style.display = 'block';
    $("#tbl-ManageDoctors").fixedHeaderTable();
});

jQuery(function ($) {

    $("#txtDOfficephone").mask("(999) 999-9999");
    $("#txtDMobphone").mask("(999) 999-9999");
    $("#txtDFax").mask("(999) 999-9999");
    $("#txtDZip").mask("99999");
});
//<--------- Jquery Call : Tabs Creation ---->
$("#tabs").tabs();

//<--------- CRUD Operations ------------>
//<--------- Start Emergency Contact ---->
function addDoctor() {

    //Just open the dialog with empty fields...
    //Opening and Setting Dynamic Title of Form...
    //Enable Fields...
    enableFields();

    //Clear Hidden Current Patient Emergency ID...
    $('#hdnCurrentDoctorID').val("");

    $("#doctor-form").dialog('option', 'title', 'Add Doctor');

    $('#cmbDCountry').val('USA');
    $('#doctor-form').dialog("open");
    allFields.val("").removeClass("ui-state-error");

}
function editDoctor(doctorID) {

    //Testing...
    //alert($('#' + doctorID).val());

    //string to Object Converstion...
    var obj = $.parseJSON($('#' + doctorID).val());



    //Filling Data To Form Fields To Modify...
    $('#hdnCurrentDoctorID').val($.trim(doctorID));

    $('#txtDname').val($.trim(obj.Name));
    $('#cmbDSpeciality').val($.trim(obj.DoctorTypeId));

    $('#txtDAdd1').val($.trim(obj.Address1));
    $('#txtDAdd2').val($.trim(obj.Address2));

    $('#txtDCity').val($.trim(obj.City));
    $('#cmbDStates').val($.trim(obj.State));
    $('#cmbDCountry').val($.trim(obj.CountryCode));
    $('#txtDZip').val($.trim(obj.PostalCode));

    $('#txtDOfficephone').val($.trim(obj.WorkPhone));
    $('#txtDMobphone').val($.trim(obj.MobilePhone));


    $('#txtDFax').val($.trim(obj.Fax));
    $('#txtDEmail').val($.trim(obj.Email));

    $('#chkIsPrimary').prop('checked', StringToBoolean($.trim(obj.IsPrimary)));
    $('#chkOnCard').prop('checked', StringToBoolean($.trim(obj.OnCard)));


    //Enable Fields...
    enableFields();

    //Opening and Setting Dynamic Title of Form...
    $("#doctor-form").dialog('option', 'title', 'Edit Doctor');

    $('#doctor-form').dialog("open");
    $("#divAddDoctor").removeClass('validateTips ui-state-highlight ui-corner-all');
    $(".validateTips").empty();

}
function detailsDoctor(doctorID) {

    //Testing...
    //alert($('#' + doctorID).val());

    //string to Object Converstion...
    var obj = $.parseJSON($('#' + doctorID).val());


    //Filling Data To Form Fields To Display...

    $('#txtDname').val($.trim(obj.Name));
    $('#cmbDSpeciality').val($.trim(obj.DoctorTypeId));

    $('#txtDAdd1').val($.trim(obj.Address1));
    $('#txtDAdd2').val($.trim(obj.Address2));

    $('#txtDCity').val($.trim(obj.City));
    $('#cmbDStates').val($.trim(obj.State));
    $('#cmbDCountry').val($.trim(obj.CountryCode));
    $('#txtDZip').val($.trim(obj.PostalCode));

    $('#txtDOfficephone').val($.trim(obj.WorkPhone));
    $('#txtDMobphone').val($.trim(obj.MobilePhone));


    $('#txtDFax').val($.trim(obj.Fax));
    $('#txtDEmail').val($.trim(obj.Email));

    $('#chkIsPrimary').prop('checked', StringToBoolean($.trim(obj.IsPrimary)));
    $('#chkOnCard').prop('checked', StringToBoolean($.trim(obj.OnCard)));

    //Disable Fields...
    disableFields();


    //Opening and Setting Dynamic Title of Form...
    $("#doctor-form").dialog('option', 'title', 'Details Doctor');

    $('#doctor-form').dialog("open");
}
function deleteDoctor(doctorID) {

    //Testing...
    //alert($('#' + doctorID).val());

    //Showing Loader...
    ShowLoader();

    try {



        var requestData = {
            PatientDoctorId: $.trim(doctorID)
        };

        $.ajax({
            type: 'POST',
            url: 'premium-manage-doctor-delete',
            data: JSON.stringify(requestData),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                // if success

                $("#divPatientDoctorList").empty();
                $("#divPatientDoctorList").html(data);
                $("#tbl-ManageDoctors").fixedHeaderTable();
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
function disableFields() {


    $('#txtDname').prop('disabled', 'disabled');

    $('#cmbDSpeciality').prop('disabled', 'disabled');
    $('#txtDAdd1').prop('disabled', 'disabled');
    $('#txtDAdd2').prop('disabled', 'disabled');

    $('#txtDCity').prop('disabled', 'disabled');
    $('#cmbDStates').prop('disabled', 'disabled');
    $('#cmbDCountry').prop('disabled', 'disabled');
    $('#txtDZip').prop('disabled', 'disabled');


    $('#txtDOfficephone').prop('disabled', 'disabled');
    $('#txtDMobphone').prop('disabled', 'disabled');

    $('#txtDFax').prop('disabled', 'disabled');
    $('#txtDEmail').prop('disabled', 'disabled');

    $('#chkIsPrimary').prop('disabled', 'disabled');
    $('#chkOnCard').prop('disabled', 'disabled');



    //disable save button...
    $('#button-save').prop('disabled', 'disabled');
}
function enableFields() {


    $('#txtDname').removeAttr('disabled', 'disabled');

    $('#cmbDSpeciality').removeAttr('disabled', 'disabled');
    $('#txtDAdd1').removeAttr('disabled', 'disabled');
    $('#txtDAdd2').removeAttr('disabled', 'disabled');

    $('#txtDCity').removeAttr('disabled', 'disabled');
    $('#cmbDStates').removeAttr('disabled', 'disabled');
    $('#cmbDCountry').removeAttr('disabled', 'disabled');
    $('#txtDZip').removeAttr('disabled', 'disabled');


    $('#txtDOfficephone').removeAttr('disabled', 'disabled');
    $('#txtDMobphone').removeAttr('disabled', 'disabled');

    $('#txtDFax').removeAttr('disabled', 'disabled');
    $('#txtDEmail').removeAttr('disabled', 'disabled');

    $('#chkIsPrimary').removeAttr('disabled', 'disabled');
    $('#chkOnCard').removeAttr('disabled', 'disabled');

    //enable save button...
    $('#button-save').removeAttr('disabled', 'disabled');
}

function StringToBoolean(string) {

    switch (string.toLowerCase()) {
        case "true": case "yes": case "1": return true;
        case "false": case "no": case "0": case null: return false;
        default: return Boolean(string);
    }
}

//<--------- End Emergency Contact ---->

//<--------- Start Emergency Contact Form Pop up Script ---->
$(function () {

    var name = $("#txtDname"),
        speciality = $("#cmbDSpeciality"),
        add1 = $("#txtDAdd1"),
        city = $("#txtDCity"),
        zip = $("#txtDZip"),
        officephone = $("#txtDOfficephone"),
        Email = $("#txtDEmail"),
        allFields = $([]).add(name).add(speciality).add(add1).add(city).add(zip).add(officephone).add(Email),
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

    function checkofficeNumber(o, n) {
        if (o.val() == "") {
            o.addClass("ui-state-error");
            updateTips(n);
            return false;
        } else {
            return true;
        }
    }

    function checkSpeciality(o, n) {
        if (o.val() == 0) {
            o.addClass("ui-state-error");
            updateTips(n);
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

    //Form Emergency Contact Form(add/update)
    $("#doctor-form").dialog({
        autoOpen: false,
        show: {
            effect: "blind",
            duration: 1000
        },
        hide: {
            effect: "blind",
            duration: 1000
        },

        width: 600,
        height: 600,
        modal: true,
        buttons: [
            {
                id: "button-save",
                text: "Save",
                click: function () {
                    var bValid = true;

                    allFields.removeClass("ui-state-error");
                    bValid = bValid && checkLength(name, "Doctor's Name", 1, 50);
                    //bValid = bValid && checkLength(add1, "address1", 5, 50);
                    //bValid = bValid && checkLength(city, "City", 1, 30);
                    //bValid = bValid && checkLength(zip, "ZipCode", 1, 30);
                    bValid = bValid && checkSpeciality(speciality, "Please Select Speciality");
                    bValid = bValid && checkofficeNumber(officephone, "Please Enter Office Number");
                    bValid = bValid && checkLength(Email, "Email", 1, 60);
                    //bValid = bValid && checkRegexp(Email, /^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$/i, "eg. abc@example.com");


                    if (bValid) {

                        try {


                            var requestData = {

                                PatientDoctorId: ($.trim($('#hdnCurrentDoctorID').val()) != "") ? $.trim($('#hdnCurrentDoctorID').val()) : null,
                                DoctorTypeId: $.trim($('#cmbDSpeciality').val()),

                                Name: $.trim($('#txtDname').val()),

                                Address1: $.trim($('#txtDAdd1').val()),
                                Address2: $.trim($('#txtDAdd2').val()),

                                WorkPhone: $.trim($('#txtDOfficephone').val()),
                                MobilePhone: $.trim($('#txtDMobphone').val()),


                                City: $.trim($('#txtDCity').val()),
                                State: $.trim($('#cmbDStates').val()),
                                CountryCode: $.trim($('#cmbDCountry').val()),
                                PostalCode: $.trim($('#txtDZip').val()),

                                Fax: $.trim($('#txtDFax').val()),
                                Email: $.trim($('#txtDEmail').val()),


                                IsPrimary: $("#chkIsPrimary").is(":checked") ? "true" : "false",
                                OnCard: $("#chkOnCard").is(":checked") ? "true" : "false"

                            };

                            //Testing...
                            //alert("Hi Raza, just b4 ajax post");

                            $.ajax({
                                type: 'POST',
                                url: 'premium-manage-doctor-save',
                                data: JSON.stringify(requestData),
                                contentType: 'application/json; charset=utf-8',
                                dataType: 'json',
                                success: function (data) {
                                    // if success

                                    $("#divPatientDoctorList").empty();
                                    $("#divPatientDoctorList").html(data);
                                    $("#tbl-ManageDoctors").fixedHeaderTable();
                                    HideLoader();
                                    //alret('Record has been saved successfully.');

                                },
                                error: function (xhr, ajaxOptions, thrownError) {
                                    //if error

                                    HideLoader();

                                    alert('Error : ' + xhr.message);


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
                            }
                        }
                        allFields.val("").removeClass("ui-state-error");
                        $(".validateTips ").empty();
                        $("span").removeClass("ui-state-highlight");
                        $(this).dialog("close");

                        ShowLoader();
                    }
                }
            },
            {
                id: "button-cancel",
                text: "Cancel",
                click: function () {
                    allFields.val("").removeClass("ui-state-error");
                    $(".validateTips ").empty();
                    $("span").removeClass("ui-state-highlight");
                    $(this).dialog("close");

                }
            }

        ]
    });
});

$(function () {
    $("input[type=submit], a, button")
    .button()
    .click(function (event) {
        // event.preventDefault();
    });
});

$(document).ready(function () {
    $('#LabTestResult1').removeClass('ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only');
    $('#LabTestResult2').removeClass('ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only');

    // Call horizontalNav on the navigations wrapping element
    $('.full-width').horizontalNav(
        {
            responsive: false

        });


});

//<--------- Common Class ---->
function removing_class() {
    $('#LabTestResult1').removeClass('ui-state-hover ');
    $('#LabTestResult2').removeClass('ui-state-hover ');
    $('#createdemographics').removeClass('ui-state-hover ');
    $('#createpersonal').removeClass('ui-state-hover ');
    $('#createemergency').removeClass('ui-state-hover ');
    $('#createaddlab').removeClass('ui-state-hover ');
    $('#createaddvisits').removeClass('ui-state-hover ');
    $('#createaddmedications').removeClass('ui-state-hover ');
    $('#createaddproblems').removeClass('ui-state-hover ');
    $('#createaddvitals').removeClass('ui-state-hover ');
    $('#createaddsocial').removeClass('ui-state-hover ');
    $('#createaddfamily').removeClass('ui-state-hover ');
    $('#createaddpast').removeClass('ui-state-hover ');
    $('#createaddimmunizations').removeClass('ui-state-hover ');

    $('#createaddvisits_tab').removeClass('ui-state-hover ');
    $('#createaddimmunizations_tab').removeClass('ui-state-hover ');
    $('#createaddproblems_tab').removeClass('ui-state-hover ');
    $('#createaddallergies').removeClass('ui-state-hover ');
    $('#createaddallergies_tab').removeClass('ui-state-hover ');
    $('#createaddsocial_tab').removeClass('ui-state-hover ');
    $('#createaddlab_tab').removeClass('ui-state-hover ');

    $('#createaddpast-tab').removeClass('ui-state-hover ');
    $('#createaddfamily-tab').removeClass('ui-state-hover ');
    $('#createaddvitals_tab').removeClass('ui-state-hover ');
}
