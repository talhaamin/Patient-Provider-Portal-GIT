function onloadcurrent() {
    ShowLoader();
    var requestData = {
        Current: false
    };
    $.ajax({
        type: 'POST',
        url: 'medications-show',
        data: JSON.stringify(requestData),
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            // if success

            $("#current-med-grid").html(data);
            $("#tbl-CurrentMedication").fixedHeaderTable();

        },
        error: function (xhr, ajaxOptions, thrownError) {
            //if error

            alert('Error : ' + xhr.message);
        },
        complete: function (data) {
            // if completed
            HideLoader();

        },
        async: true,
        processData: false
    });
}

function onloadpass() {
    ShowLoader();
    var requestData = {
        Current: true
    };
    $.ajax({
        type: 'POST',
        url: 'medications-show',
        data: JSON.stringify(requestData),
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            // if success

            $("#past-med-grid").html(data);
            $("#tbl-PastMedication").fixedHeaderTable();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            //if error

            alert('Error : ' + xhr.message);
        },
        complete: function (data) {
            // if completed
            HideLoader();

        },
        async: true,
        processData: false
    });
}

function medicationFilter(id, flag) {

    if (flag == 'visit')
    { sec_arg = $('#cmbFacilityHome option:selected').val(); }
    else if (flag == 'location') {
        sec_arg = id;
        id = $('#cmbVisitsHome option:selected').val();
    }

    var facilityOptions = document.getElementById('cmbFacilityHome').innerHTML;
    var visitOptions = document.getElementById('cmbVisitsHome').innerHTML;
    var facilitySelected = $('#cmbFacilityHome option:selected').val();
    var visitSelected = $('#cmbVisitsHome option:selected').val();

    try {
        ShowLoader();
    
        var requestData = {

            FacilityId: sec_arg,
            VisitId: id,
            facilityOptions: facilityOptions,
            visitOptions: visitOptions,
            facilitySelected: facilitySelected,
            visitSelected: visitSelected
        };


        $.ajax({
            type: 'POST',
            url: 'medication-current-medications-filter',
            data: JSON.stringify(requestData),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                // if success

                $("#current-med-grid").html(data); HideLoader();
                $("#tbl-CurrentMedication").fixedHeaderTable();
                $("#tbl-PastMedication").fixedHeaderTable();
            },
            error: function (xhr, ajaxOptions, thrownError) {
                //if error

                alert('Error : ' + xhr.message);
                HideLoader();
            },
            complete: function (data) {
                // if completed


            },
            async: false,
            processData: false
        });
    } catch (err) {

        if (err && err !== "") {
            alert(err.message);
        }
    }

    try {
        ShowLoader();
        var requestData = {
            FacilityId: sec_arg,
            VisitId: id
        };
        $.ajax({
            type: 'POST',
            url: 'medication-past-medications-filter',
            data: JSON.stringify(requestData),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                // if success
                $("#past-med-grid").html(data); HideLoader();
                $("#tbl-CurrentMedication").fixedHeaderTable();
                $("#tbl-PastMedication").fixedHeaderTable();
            },
            error: function (xhr, ajaxOptions, thrownError) {
                //if error

                alert('Error : ' + xhr.message);
                HideLoader();
            },
            complete: function (data) {
                // if completed


            },
            async: false,
            processData: false
        });
    } catch (err) {

        if (err && err !== "") {
            alert(err.message);
        }
    }

}

$(document).ready(function () {
    $("#txtMedicationName").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "index-medicationlists-get",
                type: "POST",
                dataType: "json",
                data: { term: request.term },
                success: function (data) {
                    response($.map(data, function (item) {

                        return { label: item.Description, value: item.Description };
                    }))

                }
            })
        },
        messages: {
            noResults: "", results: ""
        }
    });
});

$(document).ready(function () {
    $("#txtMedicationName1").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "index-medicationlists-get",
                type: "POST",
                dataType: "json",
                data: { term: request.term },
                success: function (data) {
                    response($.map(data, function (item) {

                        return { label: item.Description, value: item.Description };
                    }))

                }
            })
        },
        messages: {
            noResults: "", results: ""
        }
    });
});

$(function () {

    var name = $("#field1"),
    email = $("#email"),
    password = $("#password"),
    Pharmacy = $("#txtPharmacy"),
    Pharmacy1 = $("#txtPharmacy1"),
    allFields = $([]).add(name).add(email).add(password).add(Pharmacy).add(Pharmacy1),
    tips = $(".validateTips");
    function updateTips(t) {

        if (t.length > 0) {

            tips
            .text(t)
            .addClass("ui-state-highlight");
            setTimeout(function () {
                tips.removeClass("ui-state-highlight", 1500);
            }, 500);
        }
        else {
            tips.text(t);
        }
    }
    function checkLength(o, n, min, max) {
        if (o.value.length > max) {

            o.addClass("ui-state-error");

            updateTips("Length of " + n + " must be between " +
            min + " and " + max + ".");
            return false;
        }
        else if (o.value.length < min) {

                updateTips("Length of " + n + " must be between " + min + " and " + max + ".");
            return false;
        }
        else {

           
            return true;
        }
    }
    function checkLength1(o, n) {

        if (o.val() == -1) {
            o.addClass("ui-state-error");
            updateTips(n);
            return false;
        } else {
            return true;
        }
    }
    function checkLength2(o, n) {

        if (o.val() == -1) {
            o.addClass("ui-state-error");
            updateTips(n);
            return false;
        } else {
            return true;
        }
    }


    $("#dialog-form").dialog({
        autoOpen: false,
        show: {
            effect: "blind",
            duration: 1000
        },
        hide: {
            effect: "blind",
            duration: 1000
        },

        width: 700,
        modal: true,
        buttons: {
            "Save": function () {

                var bValid = true;
                allFields.removeClass("ui-state-error");

                bValid = bValid && checkLength(txtMedicationName, "Medication Name", 3, 50);
                // bValid = bValid && checkLength1(Pharmacy, "Please Select Pharmacy");

                if (bValid) {
                 
                    try {

                        if (($('#dtStartDate').val()) == "" && ($('#datepicker').val()) == "") {
                            $('#dtStartDate').val('01/01/1900');
                            $('#datepicker').val('01/01/1900');
                        }
                        else if (($('#dtStartDate').val()) == "") {
                            $('#dtStartDate').val('01/01/1900');
                        }

                        else if (($('#datepicker').val()) == "") {
                            $('#datepicker').val('01/01/1900');
                        }
                      
                        
                        var requestData = {
                            PatientMedicationCntr: $.trim($('#txtMedID').val()),
                            MedicationName: $.trim($('#txtMedicationName').val()),
                            StartDate: $.trim($('#dtStartDate').val()),
                            Route: $.trim($('#txtRoute').val()),
                            Frequency: $.trim($('#txtFrequency').val()),
                            ExpireDate: $.trim($('#datepicker').val()),
                            Status: $.trim($('#cmbTakingMedicine option:selected').val()),
                            Pharmacy: $.trim($('#txtPharmacy option:selected').text()),
                            Note: $.trim($('#txtInstructions').val()),
                            Days: $.trim($('#txtDays').val()),
                            Quantity: $.trim($('#txtQty').val()),
                            Refills: $.trim($('#txtRefills').val()),
                            Diagnosis: $.trim($('#txtDiagnosis').val()),
                            duringvisit: $.trim($('#chkMedicationAdministered').prop("checked")), 
                            Current: true,

                        };


                        $.ajax({
                            type: 'POST',
                            url: 'medications-save-current',
                            data: JSON.stringify(requestData),
                            dataType: 'json',
                            contentType: 'application/json; charset=utf-8',
                            success: function (data) {

                                if (data.Status == "Active" && requestData.PatientMedicationCntr == 0) {
                                    $("#current-med-grid").html(data.Medhtml);
                                    $("#tbl-CurrentMedication").fixedHeaderTable();
                                }
                                if (requestData.PatientMedicationCntr != 0) {
                                    $("#current-med-grid").html(data.Medhtml);
                                    $("#tbl-CurrentMedication").fixedHeaderTable();
                                }
                                HideLoader();

                            },
                            error: function (xhr, ajaxOptions, thrownError) {
                                //if error

                                alert('Error : ' + xhr.message);
                            },
                            complete: function (data) {
                             

                            },
                            async: true,
                            processData: false
                        });

                        $.ajax({
                            type: 'POST',
                            url: 'medications-show',
                            data: JSON.stringify(requestData),
                            dataType: 'json',
                            contentType: 'application/json; charset=utf-8',
                            success: function (data) {
                                // if success

                                $("#past-med-grid").html(data);
                                $("#tbl-PastMedication").fixedHeaderTable();

                            },
                            error: function (xhr, ajaxOptions, thrownError) {
                                //if error

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

            },
            Close: function () {
                allFields.val("").removeClass("ui-state-error");
                $(".validateTips ").empty();
                $("span").removeClass("ui-state-highlight");
                $(this).dialog("close");
            }
        },
        close: function () {
            allFields.val("").removeClass("ui-state-error");
        }
    });
    $("#createPresentMedication")
    .button()
    .click(function () {


        $('#txtMedicationName').attr("disabled", false);
        $('#datepicker').attr("disabled", false);
        $('#cmbTakingMedicine').attr("disabled", false);
        $('#txtPharmacy').attr("disabled", false);
        $('#txtInstructions').attr("disabled", false);
        $('#txtDays').attr("disabled", false);
        $('#txtQty').attr("disabled", false);
        $('#txtRefills').attr("disabled", false);
        $('#txtDiagnosis').attr("disabled", false);
        $('#dtStartDate').attr("disabled", false);
        $('#txtFrequency').attr("disabled", false);
        $('#txtRoute').attr("disabled", false);
        document.getElementById('chkMedicationAdministered').checked = false;

        $("#dialog-form").dialog("open");
    });
    function chkStr(value) {
        var str;
        if (value == true) {
            str = '1';
            return str;
        }
        else if (value == false) {
            str = '0';
            return str;
        }
    };


    


    $("#dialog-form1").dialog({
        autoOpen: false,
        show: {
            effect: "blind",
            duration: 1000
        },
        hide: {
            effect: "blind",
            duration: 1000
        },

        width: 700,
        modal: true,
        buttons: {
            "Save": function () {
              
                var duringvisit = $("#DuringVist1").prop("checked");
                
                        

                var bValid = true;
                allFields.removeClass("ui-state-error");

                bValid = bValid && checkLength(txtMedicationName1, "Medication Name", 3, 50);
                //  bValid = bValid && checkLength2(Pharmacy1, "Please Select Pharmacy");

                if (bValid) {
                    
                    ShowLoader();
                    try {
                      
                        if (($('#dtStartDatepst').val()) == "" && ($('#datepicker1').val()) == "") {
                            $('#dtStartDatepst').val('01/01/1900');
                            $('#datepicker1').val('01/01/1900');
                        }
                        else if (($('#dtStartDatepst').val()) == "") {
                            $('#dtStartDatepst').val('01/01/1900');
                        }

                        else if (($('#datepicker1').val()) == "") {
                            $('#datepicker1').val('01/01/1900');
                        }
                        var requestData = {
                            PatientMedicationCntr: $.trim($('#txtMedID1').val()),
                            MedicationName: $.trim($('#txtMedicationName1').val()),
                            StartDate: $.trim($('#dtStartDatepst').val()),
                            Route: $.trim($('#txtRoutepst').val()),
                            Frequency: $.trim($('#txtFrequencypst').val()),
                            ExpireDate: $.trim($('#datepicker1').val()),
                            Status: $.trim($('#cmbTakingMedicine1 option:selected').val()),
                            Pharmacy: $.trim($('#txtPharmacy1 option:selected').text()),
                            Note: $.trim($('#txtInstructions1').val()),
                            Days: $.trim($('#txtDays1').val()),
                            Quantity: $.trim($('#txtQty1').val()),
                            Refills: $.trim($('#txtRefills1').val()),
                            Diagnosis: $.trim($('#txtDiagnosis1').val()),
                            Current: false,
                            duringvisit:duringvisit,
                        };


                        $.ajax({
                            type: 'POST',
                            url: 'medications-save-current',
                            data: JSON.stringify(requestData),
                            dataType: 'json',
                            contentType: 'application/json; charset=utf-8',
                            success: function (data) {
                                // if success

                                if (data.Status == "InActive" && requestData.PatientMedicationCntr == 0) {
                                    $("#past-med-grid").html(data.Medhtml);
                                    $("#tbl-PastMedication").fixedHeaderTable();
                                }
                                if (requestData.PatientMedicationCntr != 0) {
                                    $("#past-med-grid").html(data.Medhtml);
                                    $("#tbl-PastMedication").fixedHeaderTable();
                                }
                                HideLoader();

                            },
                            error: function (xhr, ajaxOptions, thrownError) {
                                //if error

                                alert('Error : ' + xhr.message);
                                HideLoader();
                            },
                            complete: function (data) {
                                // if completed
                                //HideLoader();

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
                    //  ShowLoader();
                }

            },
            Close: function () {

                allFields.val("").removeClass("ui-state-error");
                $(".validateTips ").empty();
                $("span").removeClass("ui-state-highlight");
               

                $(this).dialog("close");

            }
        },
        close: function () {
            allFields.val("").removeClass("ui-state-error");
        }
    });
    $("#createPastMedication")
    .button()
    .click(function () {

        document.getElementById('DuringVist1').checked = false;
        $('#txtMedicationName1').attr("disabled", false);
        $('#datepicker1').attr("disabled", false);
        $('#cmbTakingMedicine1').attr("disabled", false);
        $('#txtPharmacy1').attr("disabled", false);
        $('#txtInstructions1').attr("disabled", false);
        $('#txtDays1').attr("disabled", false);
        $('#txtQty1').attr("disabled", false);
        $('#txtRefills1').attr("disabled", false);
        $('#txtDiagnosis1').attr("disabled", false);
        $('#dtStartDate').attr("disabled", false);
        $('#txtFrequency').attr("disabled", false);
        $('#txtRoute').attr("disabled", false);
        $('#dtStartDatepst').attr("disabled", false);
        $('#txtFrequencypst').attr("disabled", false);
        $('#txtRoutepst').attr("disabled", false);
        $('#DuringVist1').attr("disabled", false);
        $("#dialog-form1").dialog("open");
    });



});

function clearPastDialog() {
    $('#hdPasJson').val('');
    $('#txtMedID1').val('');
    $('#txtMedicationName1').val('');
    $('#datepicker1').val('');
    $('#cmbTakingMedicine1').val('');
    $('#txtPharmacy1').val('');
    $('#txtInstructions1').val('');
    $('#txtDays1').val('');
    $('#txtQty1').val('');
    $('#txtRefills1').val('');
    $('#txtDiagnosis1').val('');
    $('#txtFrequencypst').val('');
    $('#txtRoutepst').val('');
    $('#dtStartDatepst').val('');
}
function clearCurrentDialog() {
    $('#hdCurJson').val('');
    $('#txtMedID').val('');
    $('#txtMedicationName').val('');
    $('#datepicker').val('');
    $('#cmbTakingMedicine').val('');
    $('#txtPharmacy').val('');
    $('#txtInstructions').val('');
    $('#txtDays').val('');
    $('#txtQty').val('');
    $('#txtRefills').val('');
    $('#txtDiagnosis').val('');
    $('#txtFrequency').val('');
    $('#txtRoute').val('');
    $('#dtStartDate').val('');
}
function clearPharmacyDialog() {
    $('#txtPharmID').val('');
    $('#txtPharmacyName').val('');
    $('#txtPharmAddress1').val('');
    $('#txtPharmAddress2').val('');
    $('#txtPharmCity').val('');
    $('#txtPharmState').val('');
    $('#txtPharmZipCode').val('');
    $('#chkPref').prop('checked', false);
    $('txtPhone').val('');
    $('txtWebsite').val('');


    $('#txtPharmacyName').attr("disabled", false);
    $('#txtPharmAddress1').attr("disabled", false);
    $('#txtPharmCity').attr("disabled", false);
    $('#txtPharmState').attr("disabled", false);
    $('#txtPharmZipCode').attr("disabled", false);
    $('#txtPharmAddress2').attr("disabled", false);
    $('#chkPref').attr("disabled", false);
    $('#txtPhone').attr("disabled", false);
    $('#txtWebsite').attr("disabled", false);
}

function deletePastMedication(id) {

    ShowLoader();
    try {
        $('#txtMedID1').val(id);
        var requestData = {
            PatientMedicationCntr: $.trim($('#txtMedID1').val()),
            MedicationName: $.trim($('#txtMedicationName1').val()),
            ExpireDate: $.trim($('#datepicker1').val()),
            Status: 0,
            Pharmacy: $.trim($('#txtPharmacy1').val()),
            Note: $.trim($('#txtInstructions1').val()),
            Days: $.trim($('#txtDays1').val()),
            Quantity: $.trim($('#txtQty1').val()),
            Refills: $.trim($('#txtRefills1').val()),
            Diagnosis: $.trim($('#txtDiagnosis1').val()),

        };

        $.ajax({
            type: 'POST',
            url: 'medications-delete-current',
            data: JSON.stringify(requestData),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                 $("#past-med-grid").html(data);
                $("#tbl-PastMedication").fixedHeaderTable();
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

function detailPastMedication(id) {

    var obj = jQuery.parseJSON($("#hdPasJson" + id).val());
    $('#txtMedID1').val(id);
    $('#txtMedicationName1').val($.trim(obj.MedicationName));
    $('#datepicker1').val($.trim(obj.ExpireDate));
    $('#cmbTakingMedicine1 option:selected').val($.trim(obj.Status));
    $('#txtPharmacy1').val($.trim(obj.Pharmacy));
    $('#txtInstructions1').val($.trim(obj.Note));
    $('#txtDays1').val($.trim(obj.Days));
    $('#txtQty1').val($.trim(obj.Quantity));
    $('#txtRefills1').val($.trim(obj.Refills));
    $('#txtDiagnosis1').val($.trim(obj.Diagnosis));
    $('#dtStartDatepst').val($.trim(obj.StartDate));
    $('#txtFrequencypst').val($.trim(obj.Frequency));
    $('#txtRoutepst').val($.trim(obj.Route));


    $('#txtMedicationName1').attr("disabled", true);
    $('#datepicker1').attr("disabled", true);
    $('#cmbTakingMedicine1').attr("disabled", true);
    $('#txtPharmacy1').attr("disabled", true);
    $('#txtInstructions1').attr("disabled", true);
    $('#txtDays1').attr("disabled", true);
    $('#txtQty1').attr("disabled", true);
    $('#txtRefills1').attr("disabled", true);
    $('#txtDiagnosis1').attr("disabled", true);
    $('#dtStartDatepst').attr("disabled", true);
    $('#txtFrequencypst').attr("disabled", true);
    $('#txtRoutepst').attr("disabled", true);
    $('#DuringVist1').attr("disabled", true);
    

    $("#dialog-form1").dialog("option", "title", "Past Medication Details");

    $("#dialog-form1").dialog("open");
}
function editPastMedication(id) {

    var obj = jQuery.parseJSON($("#hdPasJson" + id).val());
   
    if ($.trim(obj.Duringvisit) == 'True') { document.getElementById('DuringVist1').checked = true; } else { document.getElementById('DuringVist1').checked = false; }
    $('#txtMedID1').val(id);
    $('#txtMedicationName1').val($.trim(obj.MedicationName));
    $('#datepicker1').val($.trim(obj.ExpireDate));
    $('#cmbTakingMedicine1').val($.trim(obj.Status));
    
    $('#txtPharmacy1').val($.trim(obj.Pharmacy));
    $('#txtInstructions1').val($.trim(obj.Note));
    $('#txtDays1').val($.trim(obj.Days));
    $('#txtQty1').val($.trim(obj.Quantity));
    $('#txtRefills1').val($.trim(obj.Refills));
    $('#txtDiagnosis1').val($.trim(obj.Diagnosis));
    $('#dtStartDatepst').val($.trim(obj.StartDate));
    $('#txtFrequencypst').val($.trim(obj.Frequency));
    $('#txtRoutepst').val($.trim(obj.Route));

    $('#txtMedicationName1').attr("disabled", false);
    $('#datepicker1').attr("disabled", false);
    $('#cmbTakingMedicine1').attr("disabled", false);
    $('#txtPharmacy1').attr("disabled", false);
    $('#txtInstructions1').attr("disabled", false);
    $('#txtDays1').attr("disabled", false);
    $('#txtQty1').attr("disabled", false);
    $('#txtRefills1').attr("disabled", false);
    $('#txtDiagnosis1').attr("disabled", false);
    $('#dtStartDatepst').attr("disabled", false);
    $('#txtFrequencypst').attr("disabled", false);
    $('#txtRoutepst').attr("disabled", false);
    $('#DuringVist1').attr("disabled", false);

    $("#dialog-form1").dialog("option", "title", "Edit Past Medication");
    $("#dialog-form1").dialog("open");
}
function deleteCurrentMedication(id) {

    ShowLoader();
    try {
        $('#txtMedID').val(id);
        var requestData = {
            PatientMedicationCntr: $.trim($('#txtMedID').val()),
            MedicationName: $.trim($('#txtMedicationName').val()),
            ExpireDate: $.trim($('#datepicker').val()),
            Status: 1,
            Pharmacy: $.trim($('#txtPharmacy').val()),
            Note: $.trim($('#txtInstructions').val()),
            Days: $.trim($('#txtDays').val()),
            Quantity: $.trim($('#txtQty').val()),
            Refills: $.trim($('#txtRefills').val()),
            Diagnosis: $.trim($('#txtDiagnosis').val()),

        };

        $.ajax({
            type: 'POST',
            url: 'medications-delete-current',
            data: JSON.stringify(requestData),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
               
                $("#current-med-grid").html(data);
                $("#tbl-CurrentMedication").fixedHeaderTable();

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

function detailCurrentMedication(id) {

    
    var obj = jQuery.parseJSON($("#hdCurJson" + id).val());
    var check = obj.duringvisit;
    $('#txtMedID').val(id);
    $('#txtMedicationName').val($.trim(obj.MedicationName));
    $('#datepicker').val($.trim(obj.ExpireDate));
    $('#cmbTakingMedicine').val($.trim(obj.Status));
    $("#txtPharmacy option:selected").text(obj.Pharmacy);
    $('#txtInstructions').val($.trim(obj.Note));
    $('#txtDays').val($.trim(obj.Days));
    $('#txtQty').val($.trim(obj.Quantity));
    $('#txtRefills').val($.trim(obj.Refills));
    $('#txtDiagnosis').val($.trim(obj.Diagnosis));
    $('#dtStartDate').val($.trim(obj.StartDate));
    $('#txtFrequency').val($.trim(obj.Frequency));
    $('#txtRoute').val($.trim(obj.Route));
    if (check == 'True') {
        document.getElementById('chkMedicationAdministered').checked = true;
    }
    else {
        document.getElementById('chkMedicationAdministered').checked = false;
    }

    $('#txtMedicationName').attr("disabled", true);
    $('#datepicker').attr("disabled", true);
    $('#cmbTakingMedicine').attr("disabled", true);
    $('#txtPharmacy').attr("disabled", true);
    $('#txtInstructions').attr("disabled", true);
    $('#txtDays').attr("disabled", true);
    $('#txtQty').attr("disabled", true);
    $('#txtRefills').attr("disabled", true);
    $('#txtDiagnosis').attr("disabled", true);
    $('#dtStartDate').attr("disabled", true);
    $('#txtFrequency').attr("disabled", true);
    $('#txtRoute').attr("disabled", true);
    $('#chkMedicationAdministered').attr("disabled", true);
    $("#dialog-form").dialog("option", "title", "A Medication Details");
    $("#dialog-form").dialog("open");
}
function editCurrentMedication(id) {
    
    var obj = jQuery.parseJSON($("#hdCurJson" + id).val());
   
    var check=obj.duringvisit;
    $('#txtMedID').val(id);

    $('#txtMedicationName').val($.trim(obj.MedicationName));
    $('#datepicker').val($.trim(obj.ExpireDate));

    $('#cmbTakingMedicine').val($.trim(obj.Status));
   
    $("#txtPharmacy option:selected").val(obj.Pharmacy);
    $('#txtInstructions').val($.trim(obj.Note));
    $('#txtDays').val($.trim(obj.Days));
    $('#txtQty').val($.trim(obj.Quantity));
    $('#txtRefills').val($.trim(obj.Refills));
    $('#txtDiagnosis').val($.trim(obj.Diagnosis));
    $('#dtStartDate').val($.trim(obj.StartDate));
    $('#txtFrequency').val($.trim(obj.Frequency));
    $('#txtRoute').val($.trim(obj.Route));
    
    if (check == 'True')
    {
        document.getElementById('chkMedicationAdministered').checked = true;
    }
    else {
        document.getElementById('chkMedicationAdministered').checked = false;
    }
    $('#txtMedicationName').attr("disabled", false);
    $('#datepicker').attr("disabled", false);
    $('#cmbTakingMedicine').attr("disabled", false);
    $('#txtPharmacy').attr("disabled", false);
    $('#txtInstructions').attr("disabled", false);
    $('#txtDays').attr("disabled", false);
    $('#txtQty').attr("disabled", false);
    $('#txtRefills').attr("disabled", false);
    $('#txtDiagnosis').attr("disabled", false);
    $('#dtStartDate').attr("disabled", false);
    $('#txtFrequency').attr("disabled", false);
    $('#txtRoute').attr("disabled", false);
    $('#chkMedicationAdministered').attr("disabled", false);
    $("#dialog-form").dialog("option", "title", "Edit Medication");
    $("#dialog-form").dialog("open");
}

$(function () {

    $("#dialog-form3").dialog
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
        height: 400,
        width: 400,
        modal: true,
        buttons: {
          
            Close: function () {
                $(this).dialog("close");
            }
        }

    });


    $("#createuser3")
    .button()
    .click(function () {
        $("#dialog-form3").dialog("open");
    });




    $("#createuser4")
    .button()
    .click(function () {
        $("#dialog-form3").dialog("open");
    });




    $("#createuser5")
    .button()
    .click(function () {
        $("#dialog-form3").dialog("open");
    });

    $("#createuser6")
    .button()
    .click(function () {
        $("#dialog-form3").dialog("open");
    });


});


$(function () {

    $("#dialog-pharmacy").dialog
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
        height: 600,
        width: 700,
        modal: true,
        buttons: {
            "Save": function () {

                var bValid = true;




                if (bValid) {
            
                    try {
                        var requestData = {
                            PharmacyCntr: $.trim($('#txtPharmID').val()),
                            PharmacyName: $.trim($('#txtPharmacyName').val()),
                            Address1: $.trim($('#txtPharmAddress1').val()),
                            City: $.trim($('#txtPharmCity').val()),
                            State: $.trim($('#txtPharmState').val()),
                            PostalCode: $.trim($('#txtPharmZipCode').val()),
                            Address2: $.trim($('#txtPharmAddress2').val()),
                            Preferred: $('#chkPref').prop('checked'),

                        };


                        $.ajax({
                            type: 'POST',
                            url: 'pharmacies-save',
                            data: JSON.stringify(requestData),
                            dataType: 'json',
                            contentType: 'application/json; charset=utf-8',
                            success: function (data) {
                                // if success
                                // alert("Success : " + data);
                                $("#pharmacy-grid").html(data);
                                $("#tbl-Pharmacies").fixedHeaderTable();
                            },
                            error: function (xhr, ajaxOptions, thrownError) {
                                //if error

                                alert('Error : ' + xhr.message);
                            },
                            complete: function (data) {
                                // if completed
                                HideLoader();

                            },
                            async: true,
                            processData: false
                        });

                        $.ajax({
                            type: 'POST',
                            url: 'Medication-Data',
                            // data: JSON.stringify(requestData),
                            dataType: 'json',
                            contentType: 'application/json; charset=utf-8',
                            success: function (data) {
                                // if success
                                // alert("Success : " + data);
                                $("#divPharmacy").html(data);
                                $("#DivPharmacy1").html(data);
                                $('#DivPharmacy1 #txtPharmacy').attr('id', 'txtPharmacy1');
                                $("#tbl-Pharmacies").fixedHeaderTable();
                            },
                            error: function (xhr, ajaxOptions, thrownError) {
                                //if error

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
                    $(this).dialog("close");
                    ShowLoader();
                }

            },
            Close: function () {
                $(this).dialog("close");
            }
        },
        close: function () {

        }
    });



    $("#add-pharmacy")
    .button()
    .click(function () {
        $("#dialog-pharmacy").dialog("open");
    });

});

function deletePharmacy(id) {

    ShowLoader();
    try {
        $('#txtPharmID').val(id);
        var requestData = {
            PharmacyCntr: $.trim($('#txtPharmID').val()),
            PharmacyName: $.trim($('#txtPharmacyName').val()),
            Address: $.trim($('#txtPharmAddress1').val()),
            City: $.trim($('#txtPharmCity').val()),
            State: $.trim($('#txtPharmState').val()),
            ZipCode: $.trim($('#txtPharmZipCode').val()),


        };

        $.ajax({
            type: 'POST',
            url: 'pharmacies-delete',
            data: JSON.stringify(requestData),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                $("#pharmacy-grid").html(data);
                $("#tbl-Pharmacies").fixedHeaderTable();
                pharmacyddl();
                HideLoader();

            },
            error: function (xhr, ajaxOptions, thrownError) {
                //if error

                alert('Error : ' + xhr.message);
                HideLoader();
            },
            complete: function (data) {
                

                HideLoader();

            },
            async: true,
            processData: false
        });
        function pharmacyddl() {
            $.ajax({
                type: 'POST',
                url: 'Medication-Data',
                // data: JSON.stringify(requestData),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    $("#divPharmacy").html(data);
                    $("#DivPharmacy1").html(data);
                    $('#DivPharmacy1 #txtPharmacy').attr('id', 'txtPharmacy1');
                    $("#tbl-Pharmacies").fixedHeaderTable();
                },
                error: function (xhr, ajaxOptions, thrownError) {
                   

                    alert('Error : ' + xhr.message);
                },
                complete: function (data) {
                   
                    HideLoader();

                },
                async: true,
                processData: false
            });
        }

    } catch (err) {

        if (err && err !== "") {
            alert(err.message);
            HideLoader();
        }
    }
}

function detailPharmacy(id) {


    var obj = jQuery.parseJSON($("#hdPharmJson" + id).val());
    $('#txtPharmID').val(id);
    $('#txtPharmacyName').val($.trim(obj.PharmacyName));
    $('#txtPharmAddress1').val($.trim(obj.Address1));
    $('#txtPharmAddress2').val($.trim(obj.Address2));
    $('#txtPharmCity').val($.trim(obj.City));
    $('#txtPharmState').val($.trim(obj.State));
    $('#txtPharmZipCode').val($.trim(obj.ZipCode));
    $('#txtPharmAddress2').val($.trim(obj.Address2));
    $('#chkPref').prop('checked', obj.Preferred);


    $('#txtPharmacyName').attr("disabled", true);
    $('#txtPharmAddress1').attr("disabled", true);
    $('#txtPharmCity').attr("disabled", true);
    $('#txtPharmState').attr("disabled", true);
    $('#txtPharmZipCode').attr("disabled", true);
    $('#txtPharmAddress2').attr("disabled", true);
    $('#chkPref').attr("disabled", true);
    $('#txtPhone').attr("disabled", true);
    $('#txtWebsite').attr("disabled", true);

    $("#dialog-pharmacy").dialog('option', 'title', 'A Pharmacy Details');
    $("#dialog-pharmacy").dialog("open");
}

function editPharmacy(id) {


    var obj = jQuery.parseJSON($("#hdPharmJson" + id).val());

    $('#txtPharmID').val(id);
    $('#txtPharmacyName').val($.trim(obj.PharmacyName));
    $('#txtPharmAddress1').val($.trim(obj.Address1));
    $('#txtPharmAddress2').val($.trim(obj.Address2));

    $('#txtPharmCity').val($.trim(obj.City));
    $('#txtPharmState').val($.trim(obj.State));
    $('#txtPharmZipCode').val($.trim(obj.ZipCode));
    $('#txtPharmAddress2').val($.trim(obj.Address2));
    $('#chkPref').prop('checked', obj.Preferred);

    $('#txtPharmacyName').attr("disabled", false);
    $('#txtPharmAddress1').attr("disabled", false);
    $('#txtPharmCity').attr("disabled", false);
    $('#txtPharmState').attr("disabled", false);
    $('#txtPharmZipCode').attr("disabled", false);
    $('#txtPharmAddress2').attr("disabled", false);
    $('#chkPref').attr("disabled", false);
    $('#txtPhone').attr("disabled", false);
    $('#txtWebsite').attr("disabled", false);

    $("#dialog-pharmacy").dialog('option', 'title', 'Edit Pharmacy');
    $("#dialog-pharmacy").dialog("open");

}

$(function () {
    $("input[type=submit], a, button")
    .button()
    .click(function (event) {
       
    });
});

$(document).ready(function () {
    $('#txtPharmacy').change(function () {
        $('.validateTips').empty();
        $("span").removeClass("ui-state-highlight ui-state-error");
        $("#txtPharmacy").removeClass(" ui-state-error");
    });
    $('#txtPharmacy1').change(function () {
        $('.validateTips').empty();
        $("span").removeClass("ui-state-highlight ui-state-error");
        $("#txtPharmacy1").removeClass(" ui-state-error");
    });
});


$(document).ready(function () {
    $('#createPresentMedication').removeClass('ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only');
    $('#createPastMedication').removeClass('ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only');
    $('#add-pharmacy').removeClass('ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only');
    $('#detail-current-medication').removeClass('ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only');
    $('#delete-current-medication').removeClass('ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only');
    $('#edit-current-medication').removeClass('ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only');
    $('#detail-past-medication').removeClass('ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only');
    $('#delete-past-medication').removeClass('ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only');
    $('#edit-past-medication').removeClass('ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only');
    $('#createuser4').removeClass('ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only');
    $('#createuser5').removeClass('ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only');
    $('#createuser6').removeClass('ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only');
    $('.full-width').horizontalNav(
        {
            responsive: false

        });
});

function toggle_combobox() {

    if ($('#cmbFacilityHome :selected').text() != 'Patient Entered') {

        $('#cmbVisitsHome').attr('disabled', false);
    }

    else
        //if ($('#cmbFacilityHome :selected').text() == 'Patient Entered')
    {

        $('#cmbVisitsHome').val('');
        $('#cmbVisitsHome').attr('disabled', true);
    }

}

function removing_class() {
    $('#createPresentMedication').removeClass('ui-state-hover ');
    $('#createPastMedication').removeClass('ui-state-hover ');
    $('#add-pharmacy').removeClass('ui-state-hover ');
    $('#detail-current-medication').removeClass('ui-state-hover ');
    $('#delete-current-medication').removeClass('ui-state-hover');
    $('#delete-past-medication').removeClass('ui-state-hover');
    $('#edit-current-medication').removeClass('ui-state-hover ');
    $('#detail-past-medication').removeClass('ui-state-hover');
    $('#edit-past-medication').removeClass('ui-state-hover');
    $('#createuser4').removeClass('ui-state-hover ');
    $('#createuser5').removeClass('ui-state-hover ');
    $('#createuser6').removeClass('ui-state-hover ');
}

function removing_class1() {
    $('#createPresentMedication').removeClass('ui-state-active');
    $('#createPastMedication').removeClass('ui-state-active');
    $('#add-pharmacy').removeClass('ui-state-active');
    $('#detail-current-medication').removeClass('ui-state-active');
    $('#delete-current-medication').removeClass('ui-state-active');
    $('#delete-past-medication').removeClass('ui-state-active');
    $('#edit-current-medication').removeClass('ui-state-active');
    $('#detail-past-medication').removeClass('ui-state-active');

    $('#edit-past-medication').removeClass('ui-state-active');
    $('#createuser4').removeClass('ui-state-active');
    $('#createuser5').removeClass('ui-state-active');
    $('#createuser6').removeClass('ui-state-active');

}
