$(document).ready(function () {
    $('#tabs').tabs();
    document.getElementById('tabs').style.display = 'block';
    $("#tbl-EmergencyContacts").fixedHeaderTable();
});

jQuery(function ($) {

    $("#txtEWorkPlacephone").mask("(999) 999-9999");
    $("#txtEHomephone").mask("(999) 999-9999");
    $("#txtEMobphone").mask("(999) 999-9999");
    $("#txtEZip").mask("99999");
});
//<--------- Jquery Call : Tabs Creation ---->
$("#tabs").tabs();

//<--------- CRUD Operations ------------>
//<--------- Start Emergency Contact ---->
function addEmergencyContact() {

    //Just open the dialog with empty fields...
    //Opening and Setting Dynamic Title of Form...
    //Enable Fields...
    enableFields();

    //Clear Hidden Current Patient Emergency ID...
    $('#hdnCurrentPatientEmergencyID').val("");
    $('#cmbECountry').val('USA');
    $("#emergency-contact-form").dialog('option', 'title', 'Add Emergency');
    $('#emergency-contact-form').dialog("open");
}
function editEmergencyContact(patEmergencyID) {

    //Testing...
    //alert($('#' + patEmergencyID).val());

    //string to Object Converstion...
    var obj = $.parseJSON($('#' + patEmergencyID).val());

    //Filling Data To Form Fields To Modify...
    $('#hdnCurrentPatientEmergencyID').val($.trim(patEmergencyID));

    $('#txtEname').val($.trim(obj.FirstName));

    $('#txtEAdd1').val($.trim(obj.Address1));
    $('#txtEAdd2').val($.trim(obj.Address2));
    $('#txtEmail').val($.trim(obj.Email));

    $('#txtECity').val($.trim(obj.City));
    $('#cmbEStates').val($.trim(obj.State));
    $('#cmbECountry').val($.trim(obj.CountryCode));
    $('#txtEZip').val($.trim(obj.PostalCode));

    $('#txtEHomephone').val($.trim(obj.HomePhone));
    $('#txtEMobphone').val($.trim(obj.MobilePhone));
    $('#txtEWorkPlacephone').val($.trim(obj.WorkPhone));

    $('#cmbERel').val($.trim(obj.RelationshipId));
    $('#chkIsPrimary').prop('checked', StringToBoolean($.trim(obj.IsPrimary)));
    $('#chkOnCard').prop('checked', StringToBoolean($.trim(obj.OnCard)));


    //Enable Fields...
    enableFields();

    //Opening and Setting Dynamic Title of Form...
    $("#emergency-contact-form").dialog('option', 'title', 'Edit Emergency Contact');
    $('#emergency-contact-form').dialog("open");

}
function detailsEmergencyContact(patEmergencyID) {

    //Testing...
    //alert($('#' + patEmergencyID).val());

    //string to Object Converstion...
    var obj = $.parseJSON($('#' + patEmergencyID).val());


    //Filling Data To Form Fields To Modify...

    $('#txtEname').val($.trim(obj.FirstName));

    $('#txtEAdd1').val($.trim(obj.Address1));
    $('#txtEAdd2').val($.trim(obj.Address2));
    $('#txtEmail').val($.trim(obj.Email));

    $('#txtECity').val($.trim(obj.City));
    $('#cmbEStates').val($.trim(obj.State));
    $('#cmbECountry').val($.trim(obj.CountryCode));
    $('#txtEZip').val($.trim(obj.PostalCode));

    $('#txtEHomephone').val($.trim(obj.HomePhone));
    $('#txtEMobphone').val($.trim(obj.MobilePhone));
    $('#txtEWorkPlacephone').val($.trim(obj.WorkPhone));

    $('#cmbERel').val($.trim(obj.RelationshipId));
    $('#chkIsPrimary').prop('checked', StringToBoolean($.trim(obj.IsPrimary)));
    $('#chkOnCard').prop('checked', StringToBoolean($.trim(obj.OnCard)));

    //Disable Fields...
    disableFields();


    //Opening and Setting Dynamic Title of Form...
    $("#emergency-contact-form").dialog('option', 'title', 'Details Emergency');
    $('#emergency-contact-form').dialog("open");
}
function deleteEmergencyContact(patEmergencyID) {

    //Testing...
    //alert($('#' + patEmergencyID).val());

    //Showing Loader...
    ShowLoader();

    try {



        var requestData = {
            PatientEmergencyId: $.trim(patEmergencyID)
        };

        $.ajax({
            type: 'POST',
            url: 'premium-emergency-contact-delete',
            data: JSON.stringify(requestData),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                // if success

                $("#divEmergencyContactList").empty();
                $("#divEmergencyContactList").html(data);
                $("#tbl-EmergencyContacts").fixedHeaderTable();
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
function disableFields() {
    $('#txtEname').prop('disabled', 'disabled');

    $('#txtEAdd1').prop('disabled', 'disabled');
    $('#txtEAdd2').prop('disabled', 'disabled');
    $('#txtEmail').prop('disabled', 'disabled');

    $('#txtECity').prop('disabled', 'disabled');
    $('#cmbEStates').prop('disabled', 'disabled');
    $('#cmbECountry').prop('disabled', 'disabled');
    $('#txtEZip').prop('disabled', 'disabled');

    $('#txtEHomephone').prop('disabled', 'disabled');
    $('#txtEMobphone').prop('disabled', 'disabled');
    $('#txtEWorkPlacephone').prop('disabled', 'disabled');

    $('#cmbERel').prop('disabled', 'disabled');
    $('#chkIsPrimary').prop('disabled', 'disabled');
    $('#chkOnCard').prop('disabled', 'disabled');

    //disable save button...
    $('#button-save').prop('disabled', 'disabled');
}
function enableFields() {

    $('#txtEname').removeAttr('disabled', 'disabled');

    $('#txtEAdd1').removeAttr('disabled', 'disabled');
    $('#txtEAdd2').removeAttr('disabled', 'disabled');
    $('#txtEmail').removeAttr('disabled', 'disabled');

    $('#txtECity').removeAttr('disabled', 'disabled');
    $('#cmbEStates').removeAttr('disabled', 'disabled');
    $('#cmbECountry').removeAttr('disabled', 'disabled');
    $('#txtEZip').removeAttr('disabled', 'disabled');

    $('#txtEHomephone').removeAttr('disabled', 'disabled');
    $('#txtEMobphone').removeAttr('disabled', 'disabled');
    $('#txtEWorkPlacephone').removeAttr('disabled', 'disabled');

    $('#cmbERel').removeAttr('disabled', 'disabled');
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



    //var name = $("#txtEname"),
    //    add1 = $("#txtEAdd1"),
    //    city = $("#txtECity"),
    //    zip = $("#txtEZip"),
    //    Email = $("#txtEmail"),
    //    allFields = $([]).add(name).add(add1).add(city).add(zip).add(Email),
    var name = $("#txtEname"),
    add1 = $("#txtEAdd1"),
    city = $("#txtECity"),
    zip = $("#txtEZip"),
    Email = $("#txtEmail"),
    homephone = $("#txtEHomephone"),
    relationship = $("#cmbERel"),
    allFields = $([]).add(name).add(add1).add(city).add(zip).add(Email).add(homephone).add(relationship),
        tips = $(".validateTips");
    function updateTips(t) {
        tips
        .text(t)
        .addClass("ui-state-highlight");
        setTimeout(function () {
            //tips.removeClass( "ui-state-highlight", 1500 );
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

    function checkHomePhone(o, n) {
        if (o.val() == "") {

            o.addClass("ui-state-error");
            updateTips(n);
            return false;
        }
        else {
            return true;
        }

    }

    function checkSelectForRelationShip(o, n) {

        if (o.val() == -1) {
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
    $("#emergency-contact-form").dialog({
        autoOpen: false,
        show: {
            effect: "blind",
            duration: 100
        },
        hide: {
            effect: "blind",
            duration: 100
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

                    //Form validation...
                    chkAccess();
                    allFields.removeClass("ui-state-error");
                    bValid = bValid && checkLength(name, "Contact Name", 1, 50);
                    bValid = bValid && checkLength(add1, "address1", 5, 50);
                    bValid = bValid && checkLength(city, "City", 1, 30);
                    bValid = bValid && checkLength(zip, "ZipCode", 1, 30);
                    bValid = bValid && checkLength(Email, "Email", 1, 60);
                    bValid = bValid && checkHomePhone(homephone, "Enter Home Phone Number");
                    bValid = bValid && checkSelectForRelationShip(relationship, "Please Select RelationShip");
                    //bValid = bValid && checkRegexp(Email, /^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$/i, "eg. abc@example.com");

                    if (bValid) {


                        try {


                            var requestData = {

                                PatientEmergencyId: ($.trim($('#hdnCurrentPatientEmergencyID').val()) != "") ? $.trim($('#hdnCurrentPatientEmergencyID').val()) : null,
                                FirstName: $.trim($('#txtEname').val()),

                                Address1: $.trim($('#txtEAdd1').val()),
                                Address2: $.trim($('#txtEAdd2').val()),

                                HomePhone: $.trim($('#txtEHomephone').val()),
                                MobilePhone: $.trim($('#txtEMobphone').val()),
                                WorkPhone: $.trim($('#txtEWorkPlacephone').val()),
                                Email: $.trim($('#txtEmail').val()),
                                City: $.trim($('#txtECity').val()),
                                State: $.trim($('#cmbEStates').val()),
                                CountryCode: $.trim($('#cmbECountry').val()),
                                PostalCode: $.trim($('#txtEZip').val()),

                                //EmergencyCountryName: $.trim($('#cmbECountry').val()),
                                //EmergencyRelationship: $.trim($('#cmbERel').val()),
                                //EmergencyWorkPhone: $.trim($('#cmbStates').val()),

                                RelationshipId: $.trim($('#cmbERel').val()),
                                IsPrimary: $("#chkIsPrimary").is(":checked") ? "true" : "false",
                                OnCard: $("#chkOnCard").is(":checked") ? "true" : "false"

                            };

                            //Testing...
                            //alert("Hi Raza, just b4 ajax post");

                            $.ajax({
                                type: 'POST',
                                url: 'premium-emergency-contact-save',
                                data: JSON.stringify(requestData),
                                contentType: 'application/json; charset=utf-8',
                                dataType: 'json',
                                success: function (data) {
                                    // if success

                                    $("#divEmergencyContactList").empty();

                                    $("#divEmergencyContactList").html(data);
                                    $("#tbl-EmergencyContacts").fixedHeaderTable();
                                    HideLoader();
                                    //alret('Record has been saved successfully.');

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
