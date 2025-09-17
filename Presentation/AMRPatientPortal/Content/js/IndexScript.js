/// procedure dialoge detail
function fnOpenNormalDialog() {
    $("#dialog-confirm").html("Are you sure you want to delete this record?");

    // Define the Dialog and its properties.
    $("#dialog-confirm").dialog({
        resizable: false,
        modal: true,
        title: "Confirm",
        height: 140,
        width: 400,
        buttons: {
            "Yes": function () {
                $(this).dialog('close');
                DeleteProcedureRec();
            },
            "No": function () {
                $(this).dialog('close');
                //callback(false);
            }
        }
    });

}
$(function () {

    $('#deleteproc').click(fnOpenNormalDialog);
});
function OpenProcRecord(id) {

    try {

        var obj = jQuery.parseJSON($("#hdProcedure" + id).val());

        $('#txtPatProcedureCntr').val(obj.PatProcedureCntr);
        if (obj.ServiceDate == "01/01/1900") {
            $('#Datadate').val('');
        }
        else {
            $('#Datadate').val(obj.ServiceDate);
        }
        $('#txtProcedureDataDescription').val(obj.Description);
        $('#txtProcedureDataNote').val(obj.Note);
        $("#show-date-procdetail").hide();
        $('#Datadate').attr("disabled", true);
        $('#txtProcedureDataDescription').attr("disabled", true);
        $('#txtProcedureDataNote').attr("disabled", true);
        $("#Procedure-Records")
.next(".ui-dialog-buttonpane")
.find("button:contains('Save')")
.button("option", "disabled", true);
    }
    catch (e) {

        alert(e.message);
    }
    $("#Procedure-Records").dialog("open");
}
function EditProcedure(id) {

    $("#Procedure-Records").dialog('option', 'title', 'Edit Procedure');
    // var obj = jQuery.parseJSON($("#" + id).val());
    $("#show-date-procdetail").show();
    $('#Datadate').attr("disabled", false);
    $('#txtProcedureDataDescription').attr("disabled", false);
    $('#txtProcedureDataNote').attr("disabled", false);
    $("#Procedure-Records")
.next(".ui-dialog-buttonpane")
.find("button:contains('Save')")
.button("option", "disabled", false);
    editflag = 1;

}
function DeleteProcedureRec() {
    chkAccess();
    $("#Procedure-Records").dialog("close");
    ShowLoader();
    try {



        var requestData = {
            PatProcedureCntr: $.trim($('#txtPatProcedureCntr').val())

        };

        $.ajax({
            type: 'POST',
            url: 'Procedure-delete',
            data: JSON.stringify(requestData),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                // if success
                // alert("Success : " + data);
                $("#tbl-Procedure").fixedHeaderTable();
                $("#tab-Procedure").fixedHeaderTable();
                $("#Procedure-portlet").html(data.html);
                $("#Procdure-portlet-tab").html(data.html1);
                //  $("#tab-Immunization").fixedHeaderTable();
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
$(function () {

    $("#Procedure-Records").dialog({
        autoOpen: false,
        show: {
            effect: "blind",
            duration: 100
        },
        hide: {
            effect: "blind",
            duration: 100
        },


        width: 550,
        modal: true,

        buttons: {

            "Save": function () {
                chkAccess();
                var bValid = true;
                if (editflag != 1) {
                    bValid = false;

                }
                var date = $("#Datadate").val();

                var mySplitResult = date.split("/");

                var Servicedate = mySplitResult[2] + mySplitResult[0] + mySplitResult[1];



                if (bValid) {
                    //ajax 
                    try {
                        //if (($('#cmbConditionStatus option:selected').val()) == -1) {
                        //    alert("Please Select status");
                        //    return false;
                        //}
                        //else if ($('#txtAllergiesDate_Year option:selected').val() == '--' && $('#txtAllergiesDate_Day option:selected').val() != '--') {
                        //    alert('Please select Year or Month and Year or Month, Day and Year');
                        //    return false;
                        //}
                        var requestData = {
                            PatProcedureCntr: $.trim($('#txtPatProcedureCntr').val()),
                            ServiceDate: Servicedate,
                            Description: $.trim($('#txtProcedureDataDescription').val()),
                            Note: $.trim($('#txtProcedureDataNote').val()),
                            flag: $.trim($('#procedureDataflag').val())

                        };


                        $.ajax({
                            type: 'POST',
                            url: 'clinical-summary-Procedures-save',
                            data: JSON.stringify(requestData),
                            dataType: 'json',
                            contentType: 'application/json; charset=utf-8',
                            success: function (data) {

                                //   if ($('#hdFlag').val() == 'allergyWidg')
                                $("#Procedure-portlet").html(data.html);
                                $("#tbl-Procedure").fixedHeaderTable();
                                //$("#tbl-Allergies").fixedHeaderTable();
                                $("#Procdure-portlet-tab").html(data.html1);

                                $("#tab-Procedure").fixedHeaderTable();
                            },
                            error: function (xhr, ajaxOptions, thrownError) {
                                //if error

                                //alert('Error : ' + xhr.message);
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
                    $(this).dialog("close");
                    ShowLoader();


                    //   end ajax


                    //  }
                }

            },
            Close: function () {
                //allFields.val("").removeClass("ui-state-error");
                //$(".validateTips ").empty();
                //$("span").removeClass("ui-state-highlight");
                $("#procedureflag").val(" ");
                $("#txtprocedureid").val("0");
                $('#date').attr("disabled", false);
                $('#txtProcedureDescription').attr("disabled", false);
                $('#txtProcedureNote').attr("disabled", false);
                editflag = 0;
                $("#Procedure-Records").dialog('option', 'title', 'Procedure Details');
                $(this).dialog("close");
            }
        },
        close: function () {
            //allFields.val("").removeClass("ui-state-error");
            //$(".validateTips ").empty();
            //$("span").removeClass("ui-state-highlight");
            $('#date').attr("disabled", false);
            $('#txtProcedureDescription').attr("disabled", false);
            $('#txtProcedureNote').attr("disabled", false);
            $("#procedureflag").val(" ");
            $("#txtprocedureid").val("0");
            editflag = 0;
            $("#Procedure-Records").dialog('option', 'title', 'Procedure Details');
            $(this).dialog("close");
        }
    });
});
/// end of procedure dialoge


// staring of Plan of clare dialgoue detail
function OpenPocRecord(id) {

    try {
        var obj = jQuery.parseJSON($("#hdPoc" + id).val());

        $('#PlanCntrData').val(obj.PlanCntr);
        $('#txttypeData').val(obj.InstructionTypeId);
        $('#InstructionsData').val(obj.Instruction);
        var date = obj.AppointmentDateTime.split(' ');
        if (obj.AppointmentDateTime == "1/1/1900 12:00:00 AM" || obj.AppointmentDateTime == "1/1/1900 00:00:00") {
            $('#PlanneddtData').val(" ");
        }
        else {
            $('#PlanneddtData').val(date[0]);

        }

        // $('#PlanneddtData').val(obj.AppointmentDateTime);
        $('#GoalsData').val(obj.Goal);
        $('#CommentsData').val(obj.Note);

        $("#show-date-POCDetails").hide();
        $('#txttypeData').attr("disabled", true);
        $('#InstructionsData').attr("disabled", true);
        $('#PlanneddtData').attr("disabled", true);
        $('#GoalsData').attr("disabled", true);
        $('#CommentsData').attr("disabled", true);
        $("#POC-Records")
.next(".ui-dialog-buttonpane")
.find("button:contains('Save')")
.button("option", "disabled", true);
    }
    catch (e) {

        alert(e.message);
    }
    $("#POC-Records").dialog("open");
}

function EditPoc() {

    $("#POC-Records").dialog('option', 'title', 'Edit Poc');

    $("#show-date-POCDetails").show();
    $('#txttypeData').attr("disabled", false);
    $('#InstructionsData').attr("disabled", false);
    $('#PlanneddtData').attr("disabled", false);
    $('#GoalsData').attr("disabled", false);
    $('#CommentsData').attr("disabled", false);
    $("#POC-Records")
.next(".ui-dialog-buttonpane")
.find("button:contains('Save')")
.button("option", "disabled", false);
    editPocFlag = 1;

}

function DeletePoc() {
    $("#POC-Records").dialog("close");
    chkAccess();
    ShowLoader();
    try {


        var requestData = {
            PlanCntr: $.trim($('#PlanCntrData').val())


        };

        $.ajax({
            type: 'POST',
            url: 'POC-delete',
            data: JSON.stringify(requestData),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                // if success
                // alert("Success : " + data);

                $("#poc-portlet").html(data.html);
                $("#tbl-poc").fixedHeaderTable();
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
$(function () {
    $("#POC-Records").dialog({
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
        modal: true,
        buttons: {
            "Save": function () {
                chkAccess();
                var bValid = true;
                if (editPocFlag != 1) {

                    bValid = false;
                }
                // allFields.removeClass("ui-state-error");
                // bValid = bValid && checkLength(Status, "Please Select Status");
                var POCTab = $("#POCflag").val();
                //if (POCTab == "POCTab") {

                //}
                //else {
                //    {
                if (bValid) {
                    //ajax 
                    try {
                        //if (($('#cmbConditionStatus option:selected').val()) == -1) {
                        //    alert("Please Select status");
                        //    return false;
                        //}
                        //else if ($('#txtAllergiesDate_Year option:selected').val() == '--' && $('#txtAllergiesDate_Day option:selected').val() != '--') {
                        //    alert('Please select Year or Month and Year or Month, Day and Year');
                        //    return false;
                        //}
                        var requestData = {
                            PlanCntr: $.trim($("#PlanCntrData").val()),
                            InstructionTypeId: $.trim($("#txttypeData option:selected").val()),
                            Instruction: $.trim($('#InstructionsData').val()),
                            AppointmentDateTime: $.trim($('#PlanneddtData').val()),
                            Goal: $.trim($('#GoalsData').val()),
                            Note: $.trim($('#CommentsData').val()),


                        };


                        $.ajax({
                            type: 'POST',
                            url: 'clinical-summary-POC-save',
                            data: JSON.stringify(requestData),
                            dataType: 'json',
                            contentType: 'application/json; charset=utf-8',
                            success: function (data) {
                                //   if ($('#hdFlag').val() == 'allergyWidg')
                                $("#poc-portlet").html(data.html);
                                $("#tbl-poc").fixedHeaderTable();
                                //$("#tbl-Allergies").fixedHeaderTable();
                                // $("#PlanOfCare-portlet-tab").html(data.html1);

                                //  $("#tab-PlanOfCare").fixedHeaderTable();
                            },
                            error: function (xhr, ajaxOptions, thrownError) {
                                //if error

                                //alert('Error : ' + xhr.message);
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
                    $(this).dialog("close");
                    ShowLoader();


                    //   end ajax


                    //    }
                    //}
                }

            },
            Close: function () {
                $("#POCflag").val(" ");
                $("#PlanCntrData").val("0");
                $('#txttypeData').attr("disabled", false);
                $('#CommentsData').attr("disabled", false);
                $('#PlanneddtData').attr("disabled", false);
                $('#InstructionsData').attr("disabled", false);
                $('#GoalsData').attr("disabled", false);
                $(this).dialog("close");
            }
        },
        close: function () {
            $("#POCflag").val(" ");
            $("#PlanCntrData").val("0");
            $('#txttypeData').attr("disabled", false);
            $('#CommentsData').attr("disabled", false);
            $('#PlanneddtData').attr("disabled", false);
            $('#InstructionsData').attr("disabled", false);
            $('#GoalsData').attr("disabled", false);
            $(this).dialog("close");
        }
    });

});

function fnOpenNormalDialogPOC() {
    $("#dialog-confirm").html("Are you sure you want to delete this record?");

    // Define the Dialog and its properties.
    $("#dialog-confirm").dialog({
        resizable: false,
        modal: true,
        title: "Confirm",
        height: 140,
        width: 400,
        buttons: {
            "Yes": function () {
                $(this).dialog('close');
                DeletePoc();
            },
            "No": function () {
                $(this).dialog('close');
                //callback(false);
            }
        }
    });

}
$(function () {

    $('#deletepoc').click(fnOpenNormalDialogPOC);
});

///end of Plan of care dialoge


///-------Medical dialogue detail
function OpenMedicationRecord(id) {
    try {
        $("#show-date-sdMedDetail").hide();
        $("#show-date-edMedDetail").hide();
        var obj = jQuery.parseJSON($("#hdMedication-" + id).val());
        if ($.trim(obj.duringvisit) == 'True') { document.getElementById('DuringVist1Data').checked = true; } else { document.getElementById('DuringVist1Data').checked = false; }
        $('#PatientMedicationCntrData').val(obj.PatientMedicationCntr);
        $('#txtMedicationNameData').val(obj.MedicationName);
        if ($.trim(obj.StartDate) == '01/01/1900' || $.trim(obj.StartDate) == '1/1/1900') {
            $('#dtStartDateData').val('');
        }
        else {
            $('#dtStartDateData').val(obj.StartDate);
        }
        if ($.trim(obj.ExpireDate) == '01/01/1900' || $.trim(obj.ExpireDate) == '1/1/1900') {
            $('#datepickerData').val('');
        }
        else {
            $('#datepickerData').val(obj.ExpireDate);
        }
        $('#cmbTakingMedicineData').val(obj.Status);
        $('#cmbPharmacyData').val(obj.Pharmacy);
        $('#txtInstructionsData').val(obj.Note);
        $('#txtDaysData').val(obj.Days);
        if (obj.Quantity == 0) {
            $('#txtQtyData').val('');
        }
        else {
            $('#txtQtyData').val(obj.Quantity);
        }
        if (obj.Refills == 0) {
            $('#txtRefillsData').val('');
        }
        else {
            $('#txtRefillsData').val(obj.Refills);
        }
        $('#txtDiagnosisData').val(obj.Diagnosis);
        if (obj.Frequency == 0) {
            $('#txtFrequencyData').val('');
        }
        else {
            $('#txtFrequencyData').val(obj.Frequency);
        }
        $('#txtRouteData').val(obj.Route);
        if ($.trim(obj.Dose) == 0)
        { $('#txtDoseData').val(''); } else { $('#txtDoseData').val(obj.Dose); }

        $('#txtDoseUnitData').val(obj.DoseUnit);
        // $('#DuringVist1Data').val(obj.duringvisit);

        $("#show-date-sdMedDetail").hide();
        $("#show-date-edMedDetail").hide();
        $('#txtMedicationNameData').attr("disabled", true);
        $('#dtStartDateData').attr("disabled", true);
        $('#datepickerData').attr("disabled", true);
        $('#cmbTakingMedicineData').attr("disabled", true);
        $('#cmbPharmacyData').attr("disabled", true);
        $('#txtInstructionsData').attr("disabled", true);
        $('#txtDaysData').attr("disabled", true);
        $('#txtQtyData').attr("disabled", true);
        $('#txtRefillsData').attr("disabled", true);
        $('#txtDiagnosisData').attr("disabled", true);
        $('#txtFrequencyData').attr("disabled", true);
        $('#txtRouteData').attr("disabled", true);
        $('#txtDoseData').attr("disabled", true);
        $('#txtDoseUnitData').attr("disabled", true);
        $('#DuringVist1Data').attr("disabled", true);
        $("#Medication-Records")
.next(".ui-dialog-buttonpane")
.find("button:contains('Save')")
.button("option", "disabled", true);
    }
    catch (e) {

        alert(e.message);
    }
    $("#Medication-Records").dialog('option', 'title', 'Medication Details');
    $("#Medication-Records").dialog("open");
}
function EditMedication() {
    $("#Medication-Records").dialog('option', 'title', 'Edit Medication');
    $("#show-date-sdMedDetail").show();
    $("#show-date-edMedDetail").show();
    $('#txtMedicationNameData').attr("disabled", false);
    $('#dtStartDateData').attr("disabled", false);
    $('#datepickerData').attr("disabled", false);
    $('#cmbTakingMedicineData').attr("disabled", false);
    $('#cmbPharmacyData').attr("disabled", false);
    $('#txtInstructionsData').attr("disabled", false);
    $('#txtDaysData').attr("disabled", false);
    $('#txtQtyData').attr("disabled", false);
    $('#txtRefillsData').attr("disabled", false);
    $('#txtDiagnosisData').attr("disabled", false);
    $('#txtFrequencyData').attr("disabled", false);
    $('#txtRouteData').attr("disabled", false);
    $('#txtDoseData').attr("disabled", false);
    $('#txtDoseUnitData').attr("disabled", false);
    $('#DuringVist1Data').attr("disabled", false);
    $("#show-date-sdMedDetail").show();
    $("#show-date-edMedDetail").show();
    $("#Medication-Records")
.next(".ui-dialog-buttonpane")
.find("button:contains('Save')")
.button("option", "disabled", false);
    editflagMedication = 1;
}
$(function () {
    $("#Medication-Records").dialog({
        autoOpen: false,
        show: {
            effect: "blind",
            duration: 100
        },
        hide: {
            effect: "blind",
            duration: 100
        },

        width: 700,
        modal: true,
        buttons: {
            "Save": function () {
                chkAccess();
                var bValid = true;
                if (editflagMedication != 1) { bValid = false; }
                //  allFields.removeClass("ui-state-error");
                //  bValid = bValid && checkLength(Pharmacy, "Please Select Pharmacy");
                var DuringVisit = $("#DuringVist1Data").prop("checked");
                if (bValid) {
                    // Save routine and call of Action
                    //alert($('#cmbPharmacy option:selected').text());
                    try {

                        //if(($('#cmbPharmacy option:selected').text())=='--Select--')
                        //{
                        //    alert("Please Select pharamacy");
                        //    return false;
                        //}


                        var requestData = {
                            PatientMedicationCntr: $.trim($('#PatientMedicationCntrData').val()),
                            MedicationName: $.trim($('#txtMedicationNameData').val()),
                            StartDate: $.trim($('#dtStartDateData').val()),
                            Route: $.trim($('#txtRouteData').val()),
                            Frequency: $.trim($('#txtFrequencyData').val()),
                            ExpireDate: $.trim($('#datepickerData').val()),
                            Status: $.trim($('#cmbTakingMedicineData option:selected').val()),
                            Pharmacy: (($('#cmbPharmacyData option:selected').text()) == '--Select--') ? "" : $.trim($('#cmbPharmacy option:selected').text()),
                            Note: $.trim($('#txtInstructionsData').val()),
                            Days: $.trim($('#txtDaysData').val()),
                            Quantity: $.trim($('#txtQtyData').val()),
                            Refills: $.trim($('#txtRefillsData').val()),
                            duringvisit: DuringVisit,
                            Diagnosis: $.trim($('#txtDiagnosisData').val()),
                            Dose: $.trim($('#txtDoseData').val()),
                            DoseUnit: $.trim($('#txtDoseUnitData').val())
                        };


                        $.ajax({
                            type: 'POST',
                            url: 'medications-save',
                            data: JSON.stringify(requestData),
                            dataType: 'json',
                            contentType: 'application/json; charset=utf-8',
                            success: function (data) {
                                // if success
                                // alert("Success : " + data);
                                $("#medications-portlet").empty();
                                $("#medications-portlet").html(data);
                                $("#tbl-Medications").fixedHeaderTable();


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
                            // alert(err.message);
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
        //     $("#Medication-Records").dialog("open");
    });
});

function DeleteMedication() {
    $("#Medication-Records").dialog("close");
    chkAccess();
    ShowLoader();
    try {
        // $('#txtMedID1').val(id);
        var requestData = {
            PatientMedicationCntr: $.trim($('#PatientMedicationCntrData').val()),
            Status: 1,
        };

        $.ajax({
            type: 'POST',
            url: 'medications-delete-current',
            data: JSON.stringify(requestData),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                $("#medications-portlet").empty();
                $("#medications-portlet").html(data.html1);
                $("#tbl-Medications").fixedHeaderTable();
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

function fnOpenNormalDialogMedication() {
    $("#dialog-confirm").html("Are you sure you want to delete this record?");

    // Define the Dialog and its properties.
    $("#dialog-confirm").dialog({
        resizable: false,
        modal: true,
        title: "Confirm",
        height: 140,
        width: 400,
        buttons: {
            "Yes": function () {
                $(this).dialog('close');
                DeleteMedication();
            },
            "No": function () {
                $(this).dialog('close');
                //callback(false);
            }
        }
    });

}
$(function () {

    $('#deletemed').click(fnOpenNormalDialogMedication);
});
//---end of medication dialogue


//---starting of clinical Dialogue

function OpenClinicalRecord(id) {


    //try{
    var obj = jQuery.parseJSON($("#hdClinical" + id).val());
    var AppDate = obj.AppointmentDateTime;
    var date = obj.AppointmentDateTime.split(' ');
    if (AppDate == "1/1/1900 00:00:00" || AppDate == "1/1/1900 12:00:00 AM") {
        $('#txtPlanneddtData').val("");
    }
    else {
        $('#txtPlanneddtData').val(date[0]);
    }
    $('#typeData').val($.trim(obj.InstructionTypeId));
    $('#clinicalInstructionData').val(obj.PlanCntr);
    $('#txtInstructionData').val(obj.Instruction);
    //$('#txtPlanneddtData').val(obj.Instruction);
    $('#txtGoalsData').val(obj.Goal);
    $('#txtCommentclinincalinstructionData').val(obj.Note);

    $("#show-date-ClinicalDetail").hide();
    $('#txtInstructionData').attr("disabled", true);
    $('#txtPlanneddtData').attr("disabled", true);
    $('#txtGoalsData').attr("disabled", true);
    $('#txtCommentclinincalinstructionData').attr("disabled", true);
    $("#Clinical-Records")
.next(".ui-dialog-buttonpane")
.find("button:contains('Save')")
.button("option", "disabled", true);
    //}
    //catch (e) {

    //    alert(e.message);
    //}
    $("#Clinical-Records").dialog("open");
}
function EditClinical() {

    $("#Clinical-Records").dialog('option', 'title', 'Edit Clinical Instruction ');
    $("#show-date-ClinicalDetail").show();
    $('#txtInstructionData').attr("disabled", false);
    $('#txtPlanneddtData').attr("disabled", false);
    $('#txtGoalsData').attr("disabled", false);
    $('#txtCommentclinincalinstructionData').attr("disabled", false);
    $("#Clinical-Records")
.next(".ui-dialog-buttonpane")
.find("button:contains('Save')")
.button("option", "disabled", false);
    editClinicalFlag = 1;

}
$(function () {
    $("#Clinical-Records").dialog({
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
        modal: true,
        buttons: {
            "Save": function () {
                chkAccess();
                var bValid = true;
                if (editClinicalFlag != 1) { bValid = false; }
                // allFields.removeClass("ui-state-error");
                // bValid = bValid && checkLength(Status, "Please Select Status");
                var clinicalInstructionflag = $("#clinicalInstructionflag").val();
                if (clinicalInstructionflag == "clinicalInstructionflag") {

                }
                else {

                    if (bValid) {
                        //ajax 
                        try {
                            //if (($('#cmbConditionStatus option:selected').val()) == -1) {
                            //    alert("Please Select status");
                            //    return false;
                            //}
                            //else if ($('#txtAllergiesDate_Year option:selected').val() == '--' && $('#txtAllergiesDate_Day option:selected').val() != '--') {
                            //    alert('Please select Year or Month and Year or Month, Day and Year');
                            //    return false;
                            //}
                            var requestData = {
                                PlanCntr: $.trim($("#clinicalInstructionData").val()),
                                InstructionTypeId: $.trim($("#typeData option:selected").val()),
                                Instruction: $.trim($('#txtInstructionData').val()),
                                AppointmentDateTime: $.trim($('#txtPlanneddtData').val()),
                                Goal: $.trim($('#txtGoalsData').val()),
                                Note: $.trim($('#txtCommentclinincalinstructionData').val()),


                            };


                            $.ajax({
                                type: 'POST',
                                url: 'clinical-summary-ClinicalInstruction-save',
                                data: JSON.stringify(requestData),
                                dataType: 'json',
                                contentType: 'application/json; charset=utf-8',
                                success: function (data) {
                                    //   if ($('#hdFlag').val() == 'allergyWidg')
                                    $("#ClinicalInstructions-portlet").html(data.html);
                                    $("#tbl-ClinicalInstruction").fixedHeaderTable();
                                    //$("#tbl-Allergies").fixedHeaderTable();
                                    //    $("#ClinicalInstructions-portlet-tab").html(data.html1);

                                    //  $("#tab-PlanOfCare").fixedHeaderTable();
                                    clearclinicalinsturctionfield();
                                },
                                error: function (xhr, ajaxOptions, thrownError) {
                                    //if error

                                    //alert('Error : ' + xhr.message);
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
                        $(this).dialog("close");
                        ShowLoader();


                        //   end ajax


                    }
                }

            },
            Close: function () {
                //  allFields.val("").removeClass("ui-state-error");
                $("#clinicalInstructionflag").val(" ");
                $('#type').attr("disabled", false);
                $("#Clinical-Records").dialog('option', 'title', 'Clinical Instruction Details ');

                $("#clinicalInstruction").val("0");
                $('#txtCommentclinincalinstruction').attr("disabled", false);
                $('#txtPlanneddt').attr("disabled", false);
                $('#txtInstruction').attr("disabled", false);
                $('#txtGoals').attr("disabled", false);
                $("#addClinicalInstructions-form").dialog("close");
                $("#Clinical-Records").dialog("close");
            }
        },
        close: function () {
            $("#clinicalInstructionflag").val(" ");
            $('#type').attr("disabled", false);
            $("#Clinical-Records").dialog('option', 'title', 'Clinical Instruction Details ');

            $("#clinicalInstruction").val("0");
            $('#txtCommentclinincalinstruction').attr("disabled", false);
            $('#txtPlanneddt').attr("disabled", false);
            $('#txtInstruction').attr("disabled", false);
            $('#txtGoals').attr("disabled", false);
            $("#addClinicalInstructions-form").dialog("close");
            $("#Clinical-Records").dialog("close");
        }
    });
});

function DeleteClinical() {
    $("#Clinical-Records").dialog("close");
    chkAccess();
    ShowLoader();
    try {



        var requestData = {
            PlanCntr: $.trim($("#clinicalInstructionData").val()),

        };

        $.ajax({
            type: 'POST',
            url: 'ClinicalInstruction-delete',
            data: JSON.stringify(requestData),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                // if success
                // alert("Success : " + data);

                //$("#ClinicalInstructions-portlet-tab").html(data);


                //$("#tab-ClinicalInstruction").fixedHeaderTable();
                $("#ClinicalInstructions-portlet").html(data.html1);
                $("#tbl-ClinicalInstruction").fixedHeaderTable();
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

function fnOpenNormalDialogClinical() {
    $("#dialog-confirm").html("Are you sure you want to delete this record?");

    // Define the Dialog and its properties.
    $("#dialog-confirm").dialog({
        resizable: false,
        modal: true,
        title: "Confirm",
        height: 140,
        width: 400,
        buttons: {
            "Yes": function () {
                $(this).dialog('close');
                DeleteClinical();
            },
            "No": function () {
                $(this).dialog('close');
                //callback(false);
            }
        }
    });

}
$(function () {

    $('#deleteclinical').click(fnOpenNormalDialogClinical);
});

//---- End of clinical dialogue


//----starting of Social History dialoge
function OpenSocialHistRecord(id) {
    try {
        var obj = jQuery.parseJSON($("#hdSocialHist" + id).val());

        $('#txtsocialIDData').val(id);
        $('#cmbDescriptionData option:selected').text($.trim(obj.Description));
        $('#txtSocialValueData').val($.trim(obj.Qualifier));
        $('#txtSocialBeginData').val($.trim(obj.BeginDate));
        $('#txtSocialEndData').val($.trim(obj.EndDate));
        $('#txtSocialNoteData').val($.trim(obj.Note));

        var byear = obj.BeginDate[0];
        byear += obj.BeginDate[1];
        byear += obj.BeginDate[2];
        byear += obj.BeginDate[3];
        var bmon = obj.BeginDate[4];
        bmon += obj.BeginDate[5];
        var bdate = obj.BeginDate[6];
        bdate += obj.BeginDate[7];
        $('#txtSocialBegin_MonthData').val(bmon);
        $('#txtSocialBegin_DayData').val(bdate);
        $('#txtSocialBegin_YearData').val(byear);


        var eyear = obj.EndDate[0];
        eyear += obj.EndDate[1];
        eyear += obj.EndDate[2];
        eyear += obj.EndDate[3];
        var emon = obj.EndDate[4];
        emon += obj.EndDate[5];
        var edate = obj.EndDate[6];
        edate += obj.EndDate[7];
        $('#txtSocialEnd_MonthData').val(emon);
        $('#txtSocialEnd_DayData').val(edate);
        $('#txtSocialEnd_YearData').val(eyear);

        $('#txtSocialEnd_MonthData').attr("disabled", true);
        $('#txtSocialEnd_DayData').attr("disabled", true);
        $('#txtSocialEnd_YearData').attr("disabled", true);
        $('#txtSocialBegin_MonthData').attr("disabled", true);
        $('#txtSocialBegin_DayData').attr("disabled", true);
        $('#txtSocialBegin_YearData').attr("disabled", true);
        $('#cmbDescriptionData').attr("disabled", true);
        $('#txtSocialValueData').attr("disabled", true);
        $('#txtSocialBeginData').attr("disabled", true);
        $('#txtSocialEndData').attr("disabled", true);
        $('#txtSocialNoteData').attr("disabled", true);
        $("#Social-Records")
.next(".ui-dialog-buttonpane")
.find("button:contains('Save')")
.button("option", "disabled", true);

    } catch (e) {
        alert(e.message);
    }
    $("#Social-Records").dialog("open");
}
$(function () {

    $("#Social-Records").dialog({
        autoOpen: false,
        show: {
            effect: "blind",
            duration: 100
        },
        hide: {
            effect: "blind",
            duration: 100
        },


        width: 550,
        modal: true,
        buttons: {
            "Save": function () {
                chkAccess();
                var bValid = true;
                if (editflagSocial != 1) { bValid = false; }
                //allFields.removeClass("ui-state-error");
                //bValid = bValid && checkLength(Description, "Please Select Description");

                if (bValid) {
                    //alert($.trim($('#txtSocialValue').val().length))
                    //ajax 
                    try {


                        var requestData = {
                            PatSocialHistCntr: $.trim($('#txtsocialIDData').val()),
                            Description: $.trim($('#cmbDescriptionData option:selected').text()),
                            CodeValue: $.trim($('#cmbDescriptionData option:selected').val()),
                            Qualifier: $.trim($('#txtSocialValueData').val()),
                            BeginDate: $.trim($('#txtSocialBeginData').val()),
                            EndDate: $.trim($('#txtSocialEndData').val()),
                            Note: $.trim($('#txtSocialNoteData').val()),
                            Flag: $.trim($('#hdFlagData').val())

                        };



                        $.ajax({
                            type: 'POST',
                            url: 'index-socials-save',
                            data: JSON.stringify(requestData),
                            dataType: 'json',
                            contentType: 'application/json; charset=utf-8',
                            success: function (data) {
                                // if success
                                // alert("Success : " + data);
                                $("#social-portlet").empty();
                                $("#social-portlet").html(data[0]);
                                $("#tbl-SocialHistory").fixedHeaderTable();


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
                    allFields.val("").removeClass("ui-state-error");
                    $(".validateTips ").empty();
                    $("span").removeClass("ui-state-highlight");
                    $(this).dialog("close");
                    ShowLoader();


                    //                        end ajax


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

        }
    });

    $("#createaddsocial")
    .button()
    .click(function () {
        $('#hdFlag').val('socialWidg');
        $("#addsocial-form").dialog("open");
    });
});
function EditSocial() {

    $("#Social-Records").dialog('option', 'title', 'Edit Social History');
    $("#Social-Records")
  .next(".ui-dialog-buttonpane")
  .find("button:contains('Save')")
  .button("option", "disabled", false);
    $('#txtSocialEnd_MonthData').attr("disabled", false);
    $('#txtSocialEnd_DayData').attr("disabled", false);
    $('#txtSocialEnd_YearData').attr("disabled", false);
    $('#txtSocialBegin_MonthData').attr("disabled", false);
    $('#txtSocialBegin_DayData').attr("disabled", false);
    $('#txtSocialBegin_YearData').attr("disabled", false);
    $('#cmbDescriptionData').attr("disabled", false);
    $('#txtSocialValueData').attr("disabled", false);
    $('#txtSocialBeginData').attr("disabled", false);
    $('#txtSocialEndData').attr("disabled", false);
    $('#txtSocialNoteData').attr("disabled", false);
    editflagSocial = 1;
}
$(function () {

    $('#deletesocial').click(fnOpenNormalDialogSocial);
});
function fnOpenNormalDialogSocial() {
    $("#dialog-confirm").html("Are you sure you want to delete this record?");

    // Define the Dialog and its properties.
    $("#dialog-confirm").dialog({
        resizable: false,
        modal: true,
        title: "Confirm",
        height: 140,
        width: 400,
        buttons: {
            "Yes": function () {
                $(this).dialog('close');
                DeleteSocial();
            },
            "No": function () {
                $(this).dialog('close');
                //callback(false);
            }
        }
    });

}
function DeleteSocial() {
    $("#Social-Records").dialog("close");
    ShowLoader();
    try {


        // $('#txtsocialID').val(id);
        var requestData = {
            PatSocialHistCntr: $.trim($('#txtsocialIDData').val()),
        };

        $.ajax({
            type: 'POST',
            url: 'socialhistories-delete',
            data: JSON.stringify(requestData),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                // if success

                $("#social-portlet").empty();
                $("#social-portlet").html(data.html1);
                $("#tbl-SocialHistory").fixedHeaderTable();
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
////---end of social history

//-----start of Vital Record


function OpenVitalRecord(id) {
    try {

        var obj = jQuery.parseJSON($("#HdVitals" + id).val());

        $('#txtvitalIDData').val(id);
        if (obj.Temperature == "0.00") {
            obj.Temperature = "";
        }
        if (obj.Pulse == "0") {
            obj.Pulse = "";
        }
        if (obj.Respiration == "0") {
            obj.Respiration = "";
        }
        $('#txtTemperatureData').val($.trim(obj.Temperature));
        if (obj.VitalDate == "1/1/1900") {
            $('#dtObservationDateData').val('');
        }
        else {
            $('#dtObservationDateData').val($.trim(obj.VitalDate));
        }
        if (obj.BloodPressurePosn == 0) {
            $('#txtBloodPressureData').val('');
        }
        else {
            $('#txtBloodPressureData').val($.trim(obj.BloodPressurePosn));
        }
        if (obj.BloodPressurePosn1 == 0)
        {
            $('#txtBloodPressure1Data').val('');
        }
        else
        {
            $('#txtBloodPressure1Data').val($.trim(obj.BloodPressurePosn1));
        }
        $("#show-date-vitData").hide();
        if (obj.WeightLb == 0)

        {
            $('#txtWeightData').val('');
        }
        else {
            $('#txtWeightData').val($.trim(obj.WeightLb));
        }
        if (obj.HeightFt == 0)
        { $('#txtHeightData').val(''); }
        else {
            $('#txtHeightData').val($.trim(obj.HeightFt));
        }
        if (obj.HeightIn == 0)
        {
            $('#txtHeightinchData').val('');
        }
        else {
            $('#txtHeightinchData').val($.trim(obj.HeightIn));
        }
        $('#txtSystolicData').val($.trim(obj.Systolic));
        $('#txtDiastolicData').val($.trim(obj.Diastolic));
        $('#txtPulseData').val($.trim(obj.Pulse));
        $('#txtRespirationData').val($.trim(obj.Respiration));

        var height = parseInt($.trim(obj.HeightFt)) * 12;
        var height1 = $.trim(obj.HeightIn);
        var heightInch = parseInt(height) + parseInt(height1);
        var weight = parseFloat($.trim(obj.WeightLb));
        var result = weight / (heightInch * heightInch) * 703;

        if (isNaN(result))
            $('#resultData').text("");
        else
            $('#resultData').text(result.toFixed(1));

        $("#Vitals-Records")
              .next(".ui-dialog-buttonpane")
              .find("button:contains('Save')")
              .button("option", "disabled", true);
        $('#txtTemperatureData').attr("disabled", true);
        $('#dtObservationDateData').attr("disabled", true);
        $('#txtBloodPressureData').attr("disabled", true);
        $('#txtBloodPressure1Data').attr("disabled", true);
        $('#txtWeightLbData').attr("disabled", true);
        $('#txtHeightData').attr("disabled", true);
        $('#txtHeightinchData').attr("disabled", true);
        $('#txtSystolicData').attr("disabled", true);
        $('#txtDiastolicData').attr("disabled", true);
        $('#txtPulseData').attr("disabled", true);
        $('#txtRespirationData').attr("disabled", true);
        $('#txtWeightData').attr("disabled", true);


    } catch (e) {
        alert(e.message);
    }
    //$('#hdFlag').val('vitalTab');

    $("#Vitals-Records").dialog("open");
}
$(function () {
    $("#Vitals-Records").dialog({
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
        modal: true,
        buttons: {
            "Save": function () {
                var bValid = true;
                if (editflagVitals != 1) { bValid = false; }

                if ($.isNumeric($('#txtTemperatureData').val()) || $('#txtTemperatureData').val() == "") {

                }
                else {
                    alert("Please enter a valid number")
                    $('#txtTemperatureData').focus();
                    return false;
                }
                var dtVal = $('#dtObservationDateData').val();
                if (ValidateDate(dtVal)) {

                    if (bValid) {
                        //ajax 
                        try {
                            var ft_inch = $.trim($('#txtHeightData').val()) * 12;
                            var inch = $.trim($('#txtHeightinchData').val());
                            var height_inch = parseInt(ft_inch) + parseInt(inch);
                            var requestData = {
                                PatientVitalCntr: $.trim($('#txtvitalIDData').val()),
                                VitalDate: $.trim($('#dtObservationDateData').val()),
                                BloodPressurePosn: $.trim($('#txtBloodPressure').val()) + "/" + $.trim($('#txtBloodPressure1Data').val()),
                                WeightLb: $.trim($('#txtWeightData').val()),
                                Temperature: $.trim($('#txtTemperatureData').val()),
                                HeightIn: height_inch,
                                Systolic: $.trim($('#txtBloodPressureData').val()),
                                Diastolic: $.trim($('#txtBloodPressure1Data').val()),
                                Pulse: $.trim($('#txtPulseData').val()),
                                Respiration: $.trim($('#txtRespirationData').val()),
                                Flag: $.trim($('#hdFlagData').val())
                            };


                            $.ajax({
                                type: 'POST',
                                url: 'index-vitals-save',
                                data: JSON.stringify(requestData),
                                dataType: 'json',
                                contentType: 'application/json; charset=utf-8',
                                success: function (data) {
                                    // if success
                                    // alert("Success : " + data);
                                    $("#vital-portlet").empty();
                                    $("#vital-portlet").html(data[0]);
                                    $("#tbl-VitalSigns").fixedHeaderTable();


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
                        $(this).dialog("close");
                        ShowLoader();


                        //                        end ajax


                    }
                }
                else {
                    alert("Date should be entered as mm/dd/yyyy")
                    $('#dtObservationDate').focus();
                }
            },
            Close: function () {
                $(this).dialog("close");
            }
        },
        close: function () {
            $(this).dialog("close");
        }
    });
    $("#addvitalscreation")
    .button()
    .click(function () {
        $('#result').text('');
        $('#hdFlag').val('vitalWidg');
        $("#form-addvitals").dialog("open");
    });
});

function EditVitals() {
    $("#Vitals-Records").dialog('option', 'title', 'Edit Vitals');
    $("#show-date-vitData").show();
    $('#txtTemperatureData').attr("disabled", false);
    $('#txtHeightinchData').attr("disabled", false);
    $('#txtBloodPressure1Data').attr("disabled", false);
    $('#dtObservationDateData').attr("disabled", false);
    $('#txtBloodPressureData').attr("disabled", false);
    $('#txtWeightLbData').attr("disabled", false);
    $('#txtHeightData').attr("disabled", false);
    $('#txtSystolicData').attr("disabled", false);
    $('#txtDiastolicData').attr("disabled", false);
    $('#txtPulseData').attr("disabled", false);
    $('#txtRespirationData').attr("disabled", false);
    $('#txtWeightData').attr("disabled", false);

    $("#Vitals-Records")
          .next(".ui-dialog-buttonpane")
          .find("button:contains('Save')")
          .button("option", "disabled", false);
    editflagVitals = 1;
}

function vitalDelete() {
    chkAccess();
    $("#Vitals-Records").dialog("close");
    ShowLoader();
    try {
        //  $('#txtvitalID').val(id);
        var requestData = {
            PatientVitalCntr: $.trim($('#txtvitalIDData').val()),
        };

        $.ajax({

            type: 'POST',
            url: 'vitals-delete',
            data: JSON.stringify(requestData),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                // if success
                // alert("Success : " + data);
                //   $("#vital-portlet-tab").html(data);

                $("#vital-portlet").empty();
                $("#vital-portlet").html(data[0]);
                $("#tbl-VitalSigns").fixedHeaderTable();
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
$(function () {

    $('#deletevitals').click(fnOpenNormalDialogVitals);
});

function fnOpenNormalDialogVitals() {
    $("#dialog-confirm").html("Are you sure you want to delete this record?");

    // Define the Dialog and its properties.
    $("#dialog-confirm").dialog({
        resizable: false,
        modal: true,
        title: "Confirm",
        height: 140,
        width: 400,
        buttons: {
            "Yes": function () {
                $(this).dialog('close');
                vitalDelete();
            },
            "No": function () {
                $(this).dialog('close');
                //callback(false);
            }
        }
    });

}
///---end of vital dialogue

//----Starting of Problem dialogue

function OpenProblemRecord(id) {
    try {

        var obj = jQuery.parseJSON($("#hdProblem-" + id).val());
        $('#txtproblemIDData').val(id);
        $('#txtProblemsDateData').val($.trim(obj.EffectiveDate));
        $('#txtPlastDateData').val($.trim(obj.LastChangeDate));
        $('#txtProblemsDescriptionData').val($.trim(obj.Note));
        $('#txtSearchData').val($.trim(obj.Condition));
        $('#txtSearchTextData').val($.trim(obj.Condition));
        $('#txtSearchIDData').val($.trim(obj.CodeValue));
        $('#cmbConditionStatusProbDataData').val($.trim(obj.ConditionStatusId));

        var year = obj.EffectiveDate[0];
        year += obj.EffectiveDate[1];
        year += obj.EffectiveDate[2];
        year += obj.EffectiveDate[3];
        var mon = obj.EffectiveDate[4];
        mon += obj.EffectiveDate[5];
        var date = obj.EffectiveDate[6];
        date += obj.EffectiveDate[7];

        $('#txtProblemsDate_MonthData').val(mon);
        $('#txtProblemsDate_DayData').val(date);
        $('#txtProblemsDate_YearData').val(year);


        var lyear = obj.LastChangeDate[0];
        lyear += obj.LastChangeDate[1];
        lyear += obj.LastChangeDate[2];
        lyear += obj.LastChangeDate[3];
        var lmon = obj.LastChangeDate[4];
        lmon += obj.LastChangeDate[5];
        var ldate = obj.LastChangeDate[6];
        ldate += obj.LastChangeDate[7];
        $('#txtProblemsLastDate_MonthData').val(lmon);
        $('#txtProblemsLastDate_DayData').val(ldate);
        $('#txtProblemsLastDate_YearData').val(lyear);


        $("#Problem-Records")
.next(".ui-dialog-buttonpane")
.find("button:contains('Save')")
.button("option", "disabled", true);

        $('#txtProblemsDate_MonthData').attr("disabled", true);
        $('#txtProblemsDate_DayData').attr("disabled", true);
        $('#txtProblemsDate_YearData').attr("disabled", true);
        $('#txtProblemsLastDate_MonthData').attr("disabled", true);
        $('#txtProblemsLastDate_DayData').attr("disabled", true);
        $('#txtProblemsLastDate_YearData').attr("disabled", true);
        $('#txtSearchData').attr("disabled", true);
        $('#txtProblemsDateData').attr("disabled", true);
        $('#txtPlastDateData').attr("disabled", true);
        $('#txtProblemsDescriptionData').attr("disabled", true);
        $('#btnproblemSearchData').attr("disabled", true);
        $('#cmbConditionStatusProbDataData').attr("disabled", true);


    } catch (e) {
        alert(e.message);
    }
    //  $('#hdFlag').val('problemTab');
    $("#Problem-Records").dialog('option', 'title', 'Problem Details');
    $("#Problem-Records").dialog("open");

}
$(function () {
    $("#Problem-Records").dialog({
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
        modal: true,
        buttons: {
            "Save": function () {
                chkAccess();
                var bValid = true;

                allFields.removeClass("ui-state-error");
                if ($('#txtProblemsDate_YearData option:selected').val() == '--' && $('#txtProblemsDate_DayData option:selected').val() == '--' && $('#txtProblemsDate_MonthData option:selected').val() == '--') {
                    alert('Please select Year or Month and Year or Month, Day and Year');
                    return false;
                }
                else if ($('#txtProblemsDate_YearData option:selected').val() == '--' && $('#txtProblemsDate_DayData option:selected').val() != '--') {
                    alert('Please select Year or Month and Year or Month, Day and Year');
                    return false;
                }
                else if ($('#txtProblemsDate_YearData option:selected').val() != '--' && $('#txtProblemsDate_DayData option:selected').val() != '--' && $('#txtProblemsDate_MonthData option:selected').val() == '--') {
                    alert('Please select Year or Month and Year or Month, Day and Year');
                    return false;
                }
                else if ($('#txtProblemsDate_YearData option:selected').val() == '--' && $('#txtProblemsDate_MonthData option:selected').val() != '--') {
                    alert('Please select Year or Month and Year or Month, Day and Year');
                    return false;
                }
                if ($('#txtProblemsLastDate_YearData option:selected').val() == '--' && $('#txtProblemsLastDate_DayData option:selected').val() == '--' && $('#txtProblemsLastDate_MonthData option:selected').val() == '--') {
                    alert('Please select Last Year or Month and Year or Month, Day and Year');
                    return false;
                }
                else if ($('#txtProblemsLastDate_YearData option:selected').val() == '--' && $('#txtProblemsLastDate_DayData option:selected').val() != '--') {
                    alert('Please select Last Year or Month and Year or Month, Day and Year');
                    return false;
                }
                else if ($('#txtProblemsLastDate_YearData option:selected').val() != '--' && $('#txtProblemsLastDate_DayData option:selected').val() != '--' && $('#txtProblemsLastDate_MonthData option:selected').val() == '--') {
                    alert('Please select Last Year or Month and Year or Month, Day and Year');
                    return false;
                }
                else if ($('#txtProblemsLastDate_YearData option:selected').val() == '--' && $('#txtProblemsLastDate_MonthData option:selected').val() != '--') {
                    alert('Please select Last Year or Month and Year or Month, Day and Year');
                    return false;
                }



                //    bValid = bValid && checkLength(ConditionStatus, "Please Select ConditionStatus");

                if ((($('#cmbConditionStatusProbDataData option:selected').val()) == -1)) {
                    alert("Please Select Condition Status option");
                    return false;
                }
                //  bValid = bValid && checkLength(ConditionStatus, "Please Select ConditionStatus");
                //if ($.trim($('#txtSearch').val()) == '') {
                //    alert("Please type or select a condition");
                //    return;
                //}
                //else 
                //if ((($('#cmbConditionStatusProbData option:selected').val()) == -1)) {
                //    alert("Please Select Condition Status option");
                //    return false;
                //}
                //if ($('#txtProblemsDate_YearData option:selected').val() == '--' || $('#txtProblemsDate_DayData option:selected').val() == '--' || $('#txtProblemsDate_MonthData option:selected').val() == '--')
                //{
                //    alert('Please select Year or Month and Year or Month, Day and Year');
                //    return false;
                //}                    
                //else if ($('#txtProblemsDate_Year option:selected').val() == '--' && $('#txtProblemsDate_Day option:selected').val() != '--') {
                //    alert('Please select Year or Month and Year or Month, Day and Year');
                //    return false;
                //}
                //else if ($('#txtProblemsDate_Year option:selected').val() == '--' && $('#txtProblemsDate_Month option:selected').val() != '--') {
                //    alert('Please select Year or Month and Year or Month, Day and Year');
                //    return false;
                //}
                //else if ($('#txtProblemsDate_Year option:selected').val() == '--')
                //{
                //    alert('Please select Year');
                //    return false;
                //}


                //if ($('#txtProblemsLastDate_YearData option:selected').val() == '--' || $('#txtProblemsLastDate_DayData option:selected').val() == '--' || $('#txtProblemsLastDate_MonthData option:selected').val() == '--')
                //{
                //    alert('Please select last Year or Month and Year or Month, Day and Year');
                //    return false;
                //}
                //else if (($('#txtProblemsLastDate_Month option:selected').val() == '--') && ($('#txtProblemsLastDate_Day option:selected').val() != '--')) {
                //    alert('Please select Date Last change Month');
                //    return false;
                //}

                //else if ($(';#txtProblemsLastDate_Year option:selected').val() == '--') {
                //    alert('Please select last change date Year');
                //    return false;
                //}

                if (bValid) {
                    var codVal = $.trim($('#txtSearchID').val());
                    if ($.trim($('#txtSearchText').val()) == "")
                    { codVal = 0; }

                    //ajax 
                    try {
                        var requestData = {
                            PatientProblemCntr: $.trim($('#txtproblemIDData').val()),
                            EffectiveDate: $.trim($('#txtProblemsDateData').val()),
                            LastChangeDate: $.trim($('#txtPlastDateData').val()),
                            Description: $.trim($('#txtSearchData').val()),
                            Note: $.trim($('#txtProblemsDescriptionData').val()),
                            CodeValue: codVal,
                            Condition: $.trim($('#txtSearchTextData').val()),
                            ConditionStatusId: $.trim($('#cmbConditionStatusProbDataData').val()),
                            Flag: $.trim($('#hdFlag').val())
                        };


                        $.ajax({
                            type: 'POST',
                            url: 'clinical-summary-problems-save',
                            data: JSON.stringify(requestData),
                            dataType: 'json',
                            contentType: 'application/json; charset=utf-8',
                            success: function (data) {
                                // if success
                                //   if ($('#hdFlag').val() == 'problemWidg')
                                //   alert(data[0]);
                                $("#problem-portlet").html(data[0]);
                                $("#tbl-Problems").fixedHeaderTable();
                                // else if ($('#hdFlag').val() == 'problemTab')
                                $("#problem-portlet-tab").html(data[1]);
                                $("#tab-Problems").fixedHeaderTable();
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
                    allFields.val("").removeClass("ui-state-error");
                    $(".validateTips ").empty();
                    $("span").removeClass("ui-state-highlight");
                    $(this).dialog("close");
                    ShowLoader();


                    //                        end ajax


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
            $(this).dialog("close");
        }
    });
    $("#createaddproblems")
    .button()
    .click(function () {
        $('#txtproblemID').val('0');
        $('#txtSearchID').val('0');
        $('#txtSearchText').val('');
        $('#txtSearch').attr("disabled", false);
        $('#txtProblemsDate').attr("disabled", false);
        $('#txtPlastDate').attr("disabled", false);
        $('#txtProblemsDescription').attr("disabled", false);
        $('#cmbConditionStatusProb').attr("disabled", false);

        $('#btnproblemSearch').attr("disabled", false);
        $('#txtProblemsDate_Month').attr("disabled", false);
        $('#txtProblemsDate_Day').attr("disabled", false);
        $('#txtProblemsDate_Year').attr("disabled", false);
        $('#txtProblemsLastDate_Month').attr("disabled", false);
        $('#txtProblemsLastDate_Day').attr("disabled", false);
        $('#txtProblemsLastDate_Year').attr("disabled", false);
        $('#hdFlag').val('problemWidg');

        $("#addproblems-form").dialog('option', 'title', 'Add Problems');
        $("#addproblems-form").dialog("open");

    });
    $("#createaddproblems_tab")
   .button()
   .click(function () {
       $('#txtproblemID').val('0');
       $('#txtSearchID').val('0');
       $('#txtSearchText').val('');
       $('#txtSearch').attr("disabled", false);
       $('#txtProblemsDate').attr("disabled", false);
       $('#txtPlastDate').attr("disabled", false);
       $('#cmbConditionStatusProb').attr("disabled", false);
       $('#txtProblemsDescription').attr("disabled", false);
       $('#btnproblemSearch').attr("disabled", false);
       $('#txtProblemsDate_Month').attr("disabled", false);
       $('#txtProblemsDate_Day').attr("disabled", false);
       $('#txtProblemsDate_Year').attr("disabled", false);
       $('#txtProblemsLastDate_Month').attr("disabled", false);
       $('#txtProblemsLastDate_Day').attr("disabled", false);
       $('#txtProblemsLastDate_Year').attr("disabled", false);
       $("#addproblems-form").dialog('option', 'title', 'Add Problems');
       $("#addproblems-form").dialog("open");

       $('#hdFlag').val('problemTab');
   });
});
function EditProblems() {
    $("#Problem-Records")
.next(".ui-dialog-buttonpane")
.find("button:contains('Save')")
.button("option", "disabled", false);
    $("#Problem-Records").dialog('option', 'title', 'Edit Problem');
    $('#txtProblemsDate_MonthData').attr("disabled", false);
    $('#txtProblemsDate_DayData').attr("disabled", false);
    $('#txtProblemsDate_YearData').attr("disabled", false);
    $('#txtProblemsLastDate_MonthData').attr("disabled", false);
    $('#txtProblemsLastDate_DayData').attr("disabled", false);
    $('#txtProblemsLastDate_YearData').attr("disabled", false);
    $('#txtSearchData').attr("disabled", false);
    $('#txtProblemsDateData').attr("disabled", false);
    $('#txtPlastDateData').attr("disabled", false);
    $('#txtProblemsDescriptionData').attr("disabled", false);
    $('#btnproblemSearchData').attr("disabled", false);
    $('#cmbConditionStatusProbDataData').attr("disabled", false);

    editflagProblems = 1;

}
function DeleteProblems() {
    $("#Problem-Records").dialog("close");

    ShowLoader();
    try {
        // $('#txtproblemID').val(id);
        var requestData = {
            PatientProblemCntr: $.trim($('#txtproblemIDData').val()),
        };

        $.ajax({
            type: 'POST',
            url: 'problems-delete',
            data: JSON.stringify(requestData),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                // if success
                chkAccess();
                $("#problem-portlet").html(data[0]);

                $("#problem-portlet-tab").html(data[1]);
                $("#tbl-Problems").fixedHeaderTable();
                $("#tab-Problems").fixedHeaderTable();
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
$(function () {

    $('#deleteProblem').click(fnOpenNormalDialogProblems);
});
function fnOpenNormalDialogProblems() {
    $("#dialog-confirm").html("Are you sure you want to delete this record?");

    // Define the Dialog and its properties.
    $("#dialog-confirm").dialog({
        resizable: false,
        modal: true,
        title: "Confirm",
        height: 140,
        width: 400,
        buttons: {
            "Yes": function () {
                $(this).dialog('close');
                DeleteProblems();
            },
            "No": function () {
                $(this).dialog('close');
                //callback(false);
            }
        }
    });

}

function OpenVisitsRecords(id) {
    $("#Visits-Record")
.next(".ui-dialog-buttonpane")
.find("button:contains('Save')")
.button("option", "disabled", true);
    try {
        var obj = jQuery.parseJSON($("#hdVisit" + id).val());
        $('#txtvisitID').val(id);
        $('#txtProvider').val($.trim(obj.ProviderName));

        $('#txtLocation').val($.trim(obj.FacilityName));
        $('#txtFacilityAddress').val($.trim(obj.FacilityAddress + ', ' + obj.FacilityCityStatePostal));
        $('#txtReasonForVisits').val($.trim(obj.VisitReason));
        if (obj.VisitDate == "1/1/1900 ") {
            $('#dtDate').val('');
        }
        else {
            $('#dtDate').val($.trim(obj.VisitDate));
        }
        $('#txtProvider').attr("disabled", true);

        $('#txtLocation').attr("disabled", true);
        $('#txtFacilityAddress').attr("disabled", true);
        $('#txtReasonForVisits').attr("disabled", true);
        $('#dtDate').attr("disabled", true);

    } catch (e) {
        alert(e.message);
    }
    //$('#hdFlag').val('visitsTab');


    $("#Visits-Record").dialog("open");
}
$(function () {

    tips = $(".validateTips");



    $("#Visits-Record").dialog({
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
        modal: true,
        buttons: {
            "Save": function () {
                chkAccess();
                var bValid = true;


                if (bValid) {

                    $(this).dialog("close");
                }
            },
            Close: function () {
                $(this).dialog("close");
            }
        },
        close: function () {

        }
    });
    $("#createaddvisits")
    .button()
    .click(function () {
        $("#Visits-Record").dialog("open");
    });
    $("#createaddvisits_tab")
   .button()
   .click(function () {
       $("#Visits-Record").dialog("open");
   });
});


$(function () {

    $("#Provider-Records").dialog({
        autoOpen: false,
        show: {
            effect: "blind",
            duration: 100
        },
        hide: {
            effect: "blind",
            duration: 100
        },


        width: 750,
        modal: true,
        buttons: {
            "Close": function () {

                var bValid = true;


                if (bValid) {

                    $(this).dialog("close");
                }
            },
            Close: function () {
                $(this).dialog("close");
            }
        },
        close: function () {
            $(this).dialog("close");
        }
    });

    $("#Provider-Records1").dialog({
        autoOpen: false,
        show: {
            effect: "blind",
            duration: 100
        },
        hide: {
            effect: "blind",
            duration: 100
        },


        width: 750,
        modal: true,
        button: {
            "Close": function () {
                $("#Provider-Records").dialog("close");
            }
        },
        close: function () {
            $("#Provider-Records").dialog("close");
        }
    });

});


function OpenPastMedicalHistoryRecords(id) {
    try {
        var obj = jQuery.parseJSON($("#hdPastMed-" + id).val());
        $('#txtpastIDData').val(id);
        $('#dtDateOccuranceData').val($.trim(obj.DateOfOccurance));
        $('#txtPastDiagnosisData').val($.trim(obj.Description));
        $('#txtPastNotesData').val($.trim(obj.Note));
        var oyear = obj.DateOfOccurance[0];
        oyear += obj.DateOfOccurance[1];
        oyear += obj.DateOfOccurance[2];
        oyear += obj.DateOfOccurance[3];
        var omon = obj.DateOfOccurance[4];
        omon += obj.DateOfOccurance[5];
        var odate = obj.DateOfOccurance[6];
        odate += obj.DateOfOccurance[7];
        $('#txtoc_MonthData').val(omon);
        $('#txtoc_DayData').val(odate);
        $('#txtoc_YearData').val(oyear);
        $("#PastMed-Records")
.next(".ui-dialog-buttonpane")
.find("button:contains('Save')")
.button("option", "disabled", true);
        $('#txtoc_MonthData').attr("disabled", true);
        $('#txtoc_DayData').attr("disabled", true);
        $('#txtoc_YearData').attr("disabled", true);
        $('#dtDateOccuranceData').attr("disabled", true);
        $('#txtPastDiagnosisData').attr("disabled", true);
        $('#txtPastNotesData').attr("disabled", true);


    } catch (e) {
        alert(e.message);
    }
    $("#PastMed-Records").dialog('option', 'title', 'Medical History Details');
    $("#PastMed-Records").dialog("open");
}

$(function () {

    tips = $(".validateTips");



    $("#PastMed-Records").dialog({
        autoOpen: false,
        show: {
            effect: "blind",
            duration: 100
        },
        hide: {
            effect: "blind",
            duration: 100
        },


        width: 550,
        height: 300,
        modal: true,
        buttons: {
            "Save": function () {
                var bValid = true;
                if (editflagPstMed != 1) { bValid = false; }
                if ($('#txtoc_YearData option:selected').val() == '--' && $('#txtoc_DayData option:selected').val() == '--' && $('#txtoc_MonthData option:selected').val() == '--') {
                    alert('Please select Year or Month and Year or Month, Day and Year');
                    return false;
                }
                else if ($('#txtoc_YearData option:selected').val() == '--' && $('#txtoc_DayData option:selected').val() != '--') {
                    alert('Please select Year or Month and Year or Month, Day and Year');
                    return false;
                }
                else if ($('#txtoc_YearData option:selected').val() != '--' && $('#txtoc_DayData option:selected').val() != '--' && $('#txtoc_MonthData option:selected').val() == '--') {
                    alert('Please select Year or Month and Year or Month, Day and Year');
                    return false;
                }
                else if ($('#txtoc_YearData option:selected').val() == '--' && $('#txtoc_MonthData option:selected').val() != '--') {
                    alert('Please select Year or Month and Year or Month, Day and Year');
                    return false;
                }
                //if (($('#txtoc_YearData option:selected').val() == '--') && ($('#txtoc_MonthData option:selected').val() != '--')) {
                //    alert('Please select Year Only or Month and Year Only Or Month, Day and Year');
                //    return false;
                //}

                if (bValid) {
                    //ajax 
                    try {

                        var requestData = {
                            PatMedicalHistCntr: $.trim($('#txtpastIDData').val()),
                            DateOfOccurance: $.trim($('#dtDateOccuranceData').val()),
                            Description: $.trim($('#txtPastDiagnosisData').val()),
                            Note: $.trim($('#txtPastNotesData').val()),
                            Flag: $.trim($('#hdFlagData').val())


                        };


                        $.ajax({
                            type: 'POST',
                            url: 'clinical-summary-pasts-save',
                            data: JSON.stringify(requestData),
                            dataType: 'json',
                            contentType: 'application/json; charset=utf-8',
                            success: function (data) {
                                // if success


                                //   if ($('#hdFlag').val() == 'pastWidg')
                                $("#pasthistory-portlet").html(data[0]);
                                $("#tbl-PastMedications").fixedHeaderTable();
                                // else if ($('#hdFlag').val() == 'pastTab')
                                $("#pasthistory-portlet-tab").html(data[1]);
                                $("#tab-MedicalHistory").fixedHeaderTable();
                                $('#PastHistOcc').find('.fht-cell').css("width", "84px");
                                $('#PastHistDiagnose').find('.fht-cell').css("width", "138px");
                                $('#PastHistNote').find('.fht-cell').css("width", "204px");

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
                    $(this).dialog("close");
                    ShowLoader();


                }
            },
            Close: function () {
                $(this).dialog("close");
            }
        },
        close: function () {
            $(this).dialog("close");
        }
    });
    //$("#createaddpast")
    //.button()
    //.click(function () {

    //    $('#dtDateOccuranceData').attr("disabled", false);
    //    $('#txtPastDiagnosisData').attr("disabled", false);
    //    $('#txtPastNotesData').attr("disabled", false);
    //    $('#txtpastIDDataData').val('0');
    //    $('#hdFlag').val('pastWidg');

    //    $("#PastMed-Records").dialog('option', 'title', ' Medical History Details');
    //    $("#PastMed-Records").dialog("open");
    //});
    // $("#createaddpast-tab")
    //.button()
    //.click(function () {

    //    $('#dtDateOccuranceData').attr("disabled", false);
    //    $('#txtPastDiagnosisData').attr("disabled", false);
    //    $('#txtPastNotesData').attr("disabled", false);
    //    $('#txtpastIDData').val('0');
    //    $('#hdFlagData').val('pastTab');
    //    $("#PastMed-Records").dialog('option', 'title', ' Medical History Details');
    //    $("#PastMed-Records").dialog("open");
    //});

});
function EditPstMed() {
    $("#PastMed-Records").dialog('option', 'title', 'Edit Medical History');
    $('#txtoc_MonthData').attr("disabled", false);
    $('#txtoc_DayData').attr("disabled", false);
    $('#txtoc_YearData').attr("disabled", false);

    $("#PastMed-Records")
.next(".ui-dialog-buttonpane")
.find("button:contains('Save')")
.button("option", "disabled", false);
    $('#dtDateOccurancehData').attr("disabled", false);
    $('#txtPastDiagnosisData').attr("disabled", false);
    $('#txtPastNotesData').attr("disabled", false);
    editflagPstMed = 1;
}
function pastHistoryDelete() {
    $("#PastMed-Records").dialog("close");
    ShowLoader();
    try {
        // $('#txtpastID').val(id);
        var requestData = {
            PatMedicalHistCntr: $.trim($('#txtpastIDData').val())
        };

        $.ajax({
            type: 'POST',
            url: 'pastmedicalhistories-delete',
            data: JSON.stringify(requestData),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                // if success
                // alert("Success : " + data);
                //  $("#pasthistory-portlet").html(data[0]);
                //  $("#tbl-PastMedications").fixedHeaderTable();
                $("#pasthistory-portlet").html(data.html1);
                $("#tbl-PastMedications").fixedHeaderTable();
                // else if ($('#hdFlag').val() == 'pastTab')
                $("#pasthistory-portlet-tab").html(data.html);
                $("#tab-MedicalHistory").fixedHeaderTable();
                $('#PastHistOcc').find('.fht-cell').css("width", "84px");
                $('#PastHistDiagnose').find('.fht-cell').css("width", "138px");
                $('#PastHistNote').find('.fht-cell').css("width", "204px");
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
$(function () {

    $('#deletePstMed').click(fnOpenNormaPstMedHist);
});
function fnOpenNormaPstMedHist() {
    $("#dialog-confirm").html("Are you sure you want to delete this record?");

    // Define the Dialog and its properties.
    $("#dialog-confirm").dialog({
        resizable: false,
        modal: true,
        title: "Confirm",
        height: 140,
        width: 400,
        buttons: {
            "Yes": function () {
                $(this).dialog('close');
                pastHistoryDelete();
            },
            "No": function () {
                $(this).dialog('close');
                //callback(false);
            }
        }
    });

}


function OpenFamilyHistoryRecords(id) {
    //    $('.ui-dialog-title').html("Family History Details");
    try {

        $("#Family-Records")
.next(".ui-dialog-buttonpane")
.find("button:contains('Save')")
.button("option", "disabled", true);
        var obj = jQuery.parseJSON($("#FamilyHist-" + id).val());

        var strArr = $.trim($('#cmbRelationshipData  option:selected').val()).split("|");
        var concatVal = $.trim(obj.RelationshipId) + '|' + $.trim(obj.FamilyMember);

        if (((obj.AgeAtOnset) == 0) && ((obj.DiseasedAge) == 0)) {

            $.trim($('#txtAgeOnsetSpinnerData').val(''));
            $.trim($('#txtAgeDiseaseSpinnerData').val(''));
        }
        else if (((obj.AgeAtOnset) == 0) && ((obj.DiseasedAge) != 0)) {
            $.trim($('#txtAgeOnsetSpinnerData').val(''));
            $('#txtAgeDiseaseSpinnerData').val($.trim(obj.DiseasedAge));
        }
        else if (((obj.DiseasedAge) == 0) && ((obj.AgeAtOnset) != 0)) {
            $('#txtAgeOnsetSpinnerData').val($.trim(obj.AgeAtOnset));
            $.trim($('#txtAgeDiseaseSpinnerData').val(''));
        }
        else {
            $('#txtAgeOnsetSpinnerData').val($.trim(obj.AgeAtOnset));
            $('#txtAgeDiseaseSpinnerData').val($.trim(obj.DiseasedAge));
        }

        $('#cmbAliveData').val(obj.Diseased);
        $("#cmbConditionStatusFamData").val(obj.ConditionStatusId);
        $('#txtfamilyIDData').val(id);
        $('#cmbRelationshipData').val(concatVal);
        $('#txtDescriptionData').val($.trim(obj.Description));
        $('#txtFamilyNoteData').val($.trim(obj.Note));
        //$('#hdFDescriptionText').val($.trim(obj.Description));
        $('#hdFDescriptionTextData').val('');
        $('#txtDescriptionData').val($.trim(obj.Description));
        $('#hdFDescriptionIdData').val($.trim(obj.CodeValue));
        $('#cmbRelationshipData').attr("disabled", true);
        $('#txtDescriptionData').attr("disabled", true);
        $('#txtFamilyNoteData').attr("disabled", true);
        $('#cmbAliveData ').attr("disabled", true);
        $('#txtAgeOnsetSpinnerData').spinner({ disabled: true });
        $('#txtAgeDiseaseSpinnerData').spinner({ disabled: true });
        $('#cmbConditionStatusFamData').attr("disabled", true);

    } catch (e) {
        alert(e.message);
    }
    // $('#hdFlag').val('familyTab');
    $("#Family-Records").dialog('option', 'title', 'Family History Details');
    $("#Family-Records").dialog("open");

}
function EditFamily() {
    $("#Family-Records")
.next(".ui-dialog-buttonpane")
.find("button:contains('Save')")
.button("option", "disabled", false);
    $("#Family-Records").dialog('option', 'title', 'Edit Family History');
    $('#cmbRelationshipData ').attr("disabled", false);
    $('#txtDescriptionData').attr("disabled", false);
    $('#txtFamilyNoteData').attr("disabled", false);
    $('#cmbAliveData').attr("disabled", false);
    $('#txtAgeOnsetSpinnerData').spinner({ disabled: false });
    $('#txtAgeDiseaseSpinnerData').spinner({ disabled: false });
    $('#cmbConditionStatusFamData').attr("disabled", false);
    editflagFamily = 1;
}
function DeleteFamily() {
    $("#Family-Records").dialog("close");
    ShowLoader();
    try {
        //$('#txtfamilyID').val(id);
        var requestData = {
            PatFamilyHistCntr: $.trim($('#txtfamilyIDData').val())

        };

        $.ajax({
            type: 'POST',
            url: 'familyhistories-delete',
            data: JSON.stringify(requestData),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                // if success

                $("#family-portlet").html(data.html1);
                $("#tbl-FamilyHistory").fixedHeaderTable();
                // else if ($('#hdFlag').val() == 'familyTab')
                $("#family-portlet-tab").html(data.html);
                $("#tab-FamilyHistory").fixedHeaderTable();

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
$(function () {

    tips = $(".validateTips");



    $("#Family-Records").dialog({

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
        modal: true,
        buttons: {
            "Save": function () {
                chkAccess();
                var bValid = true;


                if (bValid) {
                    //ajax 


                    try {
                        var strArr = $.trim($('#cmbRelationshipData  option:selected').val()).split("|");
                        if ((strArr[0] == -1) && (($('#cmbConditionStatusFamData  option:selected').val()) == -1)) {
                            alert("Please Select ' Relationship and ConditionStatus ' option");
                            return false;
                        }
                        else if ((($('#cmbConditionStatusFamData  option:selected').val()) == -1)) {
                            alert("Please Select ' ConditionStatus ' option");
                            return false;
                        }
                        else if ((strArr[0] == -1)) {
                            alert("Please Select ' Relationship ' option");
                            return false;
                        }

                        var codeVal;

                        codeVal = $.trim($('#hdFDescriptionIdData ').val());
                        if ($.trim($('#hdFDescriptionTextData ').val()) == "")
                        { codeVal = 0; }
                        var requestData = {
                            PatFamilyHistCntr: $.trim($('#txtfamilyIDData').val()),
                            //Description: $.trim($('#hdFDescriptionText').val()),
                            Description: $.trim($('#txtDescriptionData ').val()),
                            //   CodeValue: $.trim($('#hdFDescriptionId').val()),
                            CodeValue: codeVal,
                            Note: $.trim($('#txtFamilyNoteData ').val()),
                            RelationshipId: strArr[0],
                            ConditionStatusId: $.trim($('#cmbConditionStatusFamData ').val()),
                            AgeAtOnset: $.trim($('#txtAgeOnsetSpinnerData ').val()),
                            DiseasedAge: $.trim($('#txtAgeDiseaseSpinnerData ').val()),
                            Diseased: $('#cmbAliveData  option:selected').val(),
                            Flag: $.trim($('#hdFlagData ').val())
                        };


                        $.ajax({
                            type: 'POST',
                            url: 'clinical-summary-families-save',
                            data: JSON.stringify(requestData),
                            dataType: 'json',
                            contentType: 'application/json; charset=utf-8',
                            success: function (data) {
                                // if success
                                // alert("Success : " + data);

                                //  if ($('#hdFlag').val() == 'familyWidg')
                                $("#family-portlet").html(data[0]);
                                $("#tbl-FamilyHistory").fixedHeaderTable();
                                // else if ($('#hdFlag').val() == 'familyTab')
                                $("#family-portlet-tab").html(data[1]);
                                $("#tab-FamilyHistory").fixedHeaderTable();

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
                    $(this).dialog("close");
                    ShowLoader();


                    //                        end ajax


                }
            },
            Close: function () {
                $(this).dialog("close");
            }
        },
        close: function () {
            $(this).dialog("close");
        }
    });
    //$("#createaddfamily")
    //.button()
    //.click(function () {
    //    $('#hdFlag').val('familyWidg');
    //    $('#txtfamilyIDData ').val('0');
    //    $('#hdFDescriptionTextData ').val('');
    //    $('#hdFDescriptionIdData ').val('0');
    //    $('#cmbRelationshipData ').attr("disabled", false);
    //    $('#txtDescriptionData ').attr("disabled", false);
    //    $('#txtFamilyNoteData ').attr("disabled", false);
    //    $('#cmbAliveData ').attr("disabled", false);
    //    $('#cmbConditionStatusFamData ' ).attr("disabled", false);
    //    $("#txtAgeOnsetSpinnerData ").spinner({ disabled: false });
    //    $("#txtAgeDiseaseSpinnerData ").spinner({ disabled: false });
    //    $('.ui-dialog-title').html("Add Family History ");
    //    $("#Family-Records").dialog("open");
    //});
    //  $("#createaddfamily-tab")
    //.button()
    //.click(function () {
    //    $('#hdFlag').val('familyTab');
    //    $('#txtfamilyIDData ').val('0');
    //    $('#hdFDescriptionTextData ').val('');
    //    $('#hdFDescriptionIdData  ').val('0');
    //    $('#cmbRelationshipData ').attr("disabled", false);
    //    $('#txtDescriptionData ').attr("disabled", false);
    //    $('#txtFamilyNoteData ').attr("disabled", false);
    //    $('#cmbAliveData ').attr("disabled", false);
    //    $('#cmbConditionStatusFamData ').attr("disabled", false);
    //    $("#txtAgeOnsetSpinnerData ").spinner({ disabled: false });
    //    $("#txtAgeDiseaseSpinnerData ").spinner({ disabled: false });
    //    $('.ui-dialog-title').html("Add Family History ");
    //    $("#Family-Records").dialog("open");

    //});

});
$(function () {

    $('#deleteFamily').click(fnOpenNormalFamily);
});
function fnOpenNormalFamily() {
    $("#dialog-confirm").html("Are you sure you want to delete this record?");

    // Define the Dialog and its properties.
    $("#dialog-confirm").dialog({
        resizable: false,
        modal: true,
        title: "Confirm",
        height: 140,
        width: 400,
        buttons: {
            "Yes": function () {
                $(this).dialog('close');
                DeleteFamily();
            },
            "No": function () {
                $(this).dialog('close');
                //callback(false);
            }
        }
    });

}



function OpenImmunizationRecords(id) {
    $("#Immunizaton-Records")
.next(".ui-dialog-buttonpane")
.find("button:contains('Save')")
.button("option", "disabled", true);
    var obj = jQuery.parseJSON($("#Immunization-" + id).val());
    var expDate = $.trim(obj.ExpirationDate);
    var immDate = $.trim(obj.ImmunizationDate);
    if (expDate == "1/1/0001" || expDate == "1/1/1900") { expDate = ""; }
    //if (immDate == "01/01/0001" || immDate == "01/01/1900") { immDate = ""; }
    $('#hdImmunizationTextData').val('');
    $('#txtimmunizationIDData').val(id);
    $('#hdImmunizationData').val(id);
    //  $('#hdImmunizationTextData').val(obj.Vaccine);
    $('#txtImmunizationDateData').val(immDate);
    $('#txtImmunizationData').val($.trim(obj.Vaccine));
    $('#cmbImmunizationTimeData').val($.trim(obj.Time));
    $('#txtAmountData').val($.trim(obj.Amount));
    $('#txtImmunizationNoteData').val($.trim(obj.Note));
    $('#txtImmunizationRouteData').val($.trim(obj.Route));
    $('#txtImmunizationSiteData').val($.trim(obj.Site));
    $('#txtImmunizationSeqData').val($.trim(obj.Sequence));
    $('#txtImmunizationEXDateData').val(expDate);
    $('#txtImmunizationLotNoData').val($.trim(obj.LotNumber));
    $('#txtImmunizationMuData').val($.trim(obj.Manufacturer));


    $("#show-date-imm-Data").hide();
    $("#show-date-imm-exp-Data").hide();
    $('#show-date-imm-Data').attr("disabled", true);
    $('#txtImmunizationDateData').attr("disabled", true);
    $('#txtImmunizationData').attr("disabled", true);
    $('#cmbImmunizationTimeData').attr("disabled", true);
    $('#txtAmountData').attr("disabled", true);
    $('#txtImmunizationNoteData').attr("disabled", true);
    $('#txtImmunizationRouteData').attr("disabled", true);
    $('#txtImmunizationSiteData').attr("disabled", true);
    $('#txtImmunizationSeqData').attr("disabled", true);
    $('#txtImmunizationEXDateData').attr("disabled", true);
    $('#txtImmunizationLotNoData').attr("disabled", true);
    $('#txtImmunizationMuData').attr("disabled", true);


    $("#Immunizaton-Records").dialog("open");


}
function EditImmunization() {
    $("#Immunizaton-Records")
.next(".ui-dialog-buttonpane")
.find("button:contains('Save')")
.button("option", "disabled", false);
    $("#Immunizaton-Records").dialog('option', 'title', 'Edit Immunization');
    $("#show-date-imm-Data").show();
    $("#show-date-imm-exp-Data").show();
    $('#txtImmunizationDateData').attr("disabled", false);
    $('#txtImmunizationData').attr("disabled", false);
    $('#cmbImmunizationTimeData').attr("disabled", false);
    $('#txtAmountData').attr("disabled", false);
    $('#txtImmunizationNoteData').attr("disabled", false);
    $('#txtImmunizationRouteData').attr("disabled", false);
    $('#txtImmunizationSiteData').attr("disabled", false);
    $('#txtImmunizationSeqData').attr("disabled", false);
    $('#txtImmunizationEXDateData').attr("disabled", false);
    $('#txtImmunizationLotNoData').attr("disabled", false);
    $('#txtImmunizationMuData').attr("disabled", false);

    editflagImmunization = 1;



}


$(function () {

    tips = $(".validateTips");

    $("#Immunizaton-Records").dialog({
        autoOpen: false,
        show: {
            effect: "blind",
            duration: 100
        },
        hide: {
            effect: "blind",
            duration: 100
        },


        width: 850,
        modal: true,
        buttons: {
            "Save": function () {
                chkAccess();
                var bValid = true;
                if (editflagImmunization != 1) { bValid = false; }
                var codVal;

                codVal = $.trim($('#hdImmunizationData').val());
                if ($.trim($('#hdImmunizationTextData').val()) == "")
                { codVal = 0; }
                var tmVal = $('#cmbImmunizationTimeData').val()

                var dtVal = $('#txtImmunizationDateData').val();
                var dtEval = $('#txtImmunizationEXDateData').val();




                if (ValidateDateFormat(dtVal)) {

                    if (ValidateTime(tmVal)) {

                        if (true) { //ValidateDateFormat(dtEVal)

                            if (bValid) {
                                //ajax 
                                try {




                                    var requestData = {
                                        PatientImmunizationCntr: $.trim($('#hdImmunizationData').val()),
                                        ImmunizationDate: $.trim($('#txtImmunizationDateData').val()),
                                        Time: $.trim($('#cmbImmunizationTimeData').val()),
                                        CodeValue: codVal,
                                        Vaccine: $.trim($('#txtImmunizationData').val()),
                                        //Vaccine: $.trim($('#hdImmunizationTextData').val()),
                                        Amount: $.trim($('#txtAmountData').val()),
                                        Note: $.trim($('#txtImmunizationNoteData').val()),
                                        Route: $.trim($('#txtImmunizationRouteData').val()),
                                        Site: $.trim($('#txtImmunizationSiteData').val()),
                                        Sequence: $.trim($('#txtImmunizationSeqData').val()),
                                        ExpirationDate: $.trim($('#txtImmunizationEXDateData').val()),
                                        LotNumber: $.trim($('#txtImmunizationLotNoData').val()),
                                        Manufacturer: $.trim($('#txtImmunizationMuData').val()),

                                        Flag: 'immunizationWidg'

                                    };


                                    $.ajax({
                                        type: 'POST',
                                        url: 'index-immunizations-save',
                                        data: JSON.stringify(requestData),
                                        dataType: 'json',
                                        contentType: 'application/json; charset=utf-8',
                                        success: function (data) {
                                            // if success
                                            // alert("Success : " + data);
                                            $("#immunization-portlet").empty();
                                            $("#immunization-portlet").html(data[0]);
                                            $("#tbl-Immunizations").fixedHeaderTable();


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
                                $(this).dialog("close");
                                ShowLoader();


                                //                        end ajax


                            }
                        }
                        else {
                            alert("Date should be entered as mm/dd/yyyy")
                            $('#txtImmunizationEXDate').focus();
                        }
                    }
                    else {

                        alert("Time should be entered as hh:mm tt ex.(05:30 pm)")
                        $('#cmbImmunizationTime').focus();
                    }
                }
                else {
                    alert("Date should be entered as mm/dd/yyyy")
                    $('#txtImmunizationDate').focus();
                }
            },
            Close: function () {
                $(this).dialog("close");
            }
        },
        close: function () {
            $(this).dialog("close");
        }
    });
    $("#createaddimmunizations")
    .button()
    .click(function () {
        $('#txtimmunizationIDData').val('0');
        $('#hdImmunizationTextData').val('');
        $('#hdImmunizationData').val('0');
        $('#hdFlagData').val('immunizationWidg');
        $("#addimmunizations-form").dialog("open");
    });
});
function immunizationDelete() {
    $("#Immunizaton-Records").dialog("close");
    ShowLoader();
    try {
        //  $('#txtimmunizationID').val(id);


        var requestData = {
            PatientImmunizationCntr: $.trim($('#hdImmunizationData').val())

        };

        $.ajax({
            type: 'POST',
            url: 'immunizations-delete',
            data: JSON.stringify(requestData),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                // if success
                // alert("Success : " + data);
                $("#immunization-portlet").empty();
                $("#immunization-portlet").html(data.html1);
                $("#tbl-Immunizations").fixedHeaderTable();
                //$("#immunization-portlet-tab").html(data);
                //$("#tab-Immunization").fixedHeaderTable();
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
$(function () {

    $('#deleteImmunization').click(fnOpenNormalImmunization);
});
function fnOpenNormalImmunization() {
    $("#dialog-confirm").html("Are you sure you want to delete this record?");

    // Define the Dialog and its properties.
    $("#dialog-confirm").dialog({
        resizable: false,
        modal: true,
        title: "Confirm",
        height: 140,
        width: 400,
        buttons: {
            "Yes": function () {
                $(this).dialog('close');
                immunizationDelete();
            },
            "No": function () {
                $(this).dialog('close');
                //callback(false);
            }
        }
    });

}


function Openproviderdialge() {
    try {
        var obj = jQuery.parseJSON($("#hdprovide").val());

        var address = obj.Address + " " + obj.state;
        $('#providername').val($.trim(obj.Name));
        //   $('#cmbFacilityProvider').val($.trim(obj.Facilityid));
        $("#cmbFacilityProvider option:selected").text(obj.Facilityid);
        // $("cmbFacilityProvider option:contains("+obj.Facilityid+")").attr('selected', true);
        $('#txtofficephone').val($.trim(obj.OfficePhone));
        $('#txtprovideradd').val(address);
        $('#providername').attr('disabled', 'disabled');
        $('#cmbFacilityProvider').attr('disabled', 'disabled');
        $('#txtofficephone').attr('disabled', 'disabled');
        $('#txtprovideradd').attr('disabled', 'disabled');
    } catch (e) {
        alert(e.message);
    }
    $("#Provider-Records").dialog('option', 'title', 'Provider Info Details');
    $("#Provider-Records").dialog("open");
}
function EditAllergy() {
    $("#Allergies-Records")
.next(".ui-dialog-buttonpane")
.find("button:contains('Save')")
.button("option", "disabled", false);
    $("#Allergies-Records").dialog('option', 'title', 'Edit Allergy');
    $('#dtEffectiveDateData').attr("disabled", false);
    $('#txtAllergenTypeData').attr("disabled", false);
    $('#txtAllergiesDate_MonthData').attr("disabled", false);
    $('#txtAllergiesDate_DayData').attr("disabled", false);
    $('#txtAllergiesDate_YearData').attr("disabled", false);
    $('#txtAllergenData').attr("disabled", false);
    $('#txtReactionData').attr("disabled", false);
    $('#txtNoteData').attr("disabled", false);
    $('#cmbConditionStatusData').attr("disabled", false);
    ediflagAllergy = 1;
}
$(function () {

    $('#deleteAllergies').click(fnOpenNormalAllergy);
});
function fnOpenNormalAllergy() {
    $("#dialog-confirm").html("Are you sure you want to delete this record?");

    // Define the Dialog and its properties.
    $("#dialog-confirm").dialog({
        resizable: false,
        modal: true,
        title: "Confirm",
        height: 140,
        width: 400,
        buttons: {
            "Yes": function () {
                $(this).dialog('close');
                allergyDelete();
            },
            "No": function () {
                $(this).dialog('close');
                //callback(false);
            }
        }
    });

}
function allergyDelete() {
    $("#Allergies-Records").dialog('close');
    chkAccess();
    ShowLoader();
    try {
        //$('#txtallergyID').val(id);
        var requestData = {
            PatientAllergyCntr: $.trim($('#txtallergyIDData').val())
        };

        $.ajax({
            type: 'POST',
            url: 'allergies-delete',
            data: JSON.stringify(requestData),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                // if success
                // alert("Success : " + data);
                $("#allergies-portlet").html(data.html1);
                //  else if ($('#hdFlag').val() == 'allergyTab')
                $("#tbl-Allergies").fixedHeaderTable();
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

function OpenAllergiesRecords(id) {
    try {
        $("#Allergies-Records")
.next(".ui-dialog-buttonpane")
.find("button:contains('Save')")
.button("option", "disabled", true);
        var obj = jQuery.parseJSON($("#Allergies-" + id).val());
        $('#txtallergyIDData').val(id);
        //   $('#dtEffectiveDateData').val($.trim(obj.EffectiveDate));
        $('#txtAllergenTypeData').val($.trim(obj.AllergenType));
        $('#txtAllergenData').val($.trim(obj.Allergen));
        $('#txtReactionData').val($.trim(obj.Reaction));
        $('#txtNoteData').val($.trim(obj.Note));
        $('#cmbConditionStatusData').val($.trim(obj.ConditionStatusId));

        var year = obj.EffectiveDate[0];
        year += obj.EffectiveDate[1];
        year += obj.EffectiveDate[2];
        year += obj.EffectiveDate[3];
        var mon = obj.EffectiveDate[4];
        mon += obj.EffectiveDate[5];
        var date = obj.EffectiveDate[6];
        date += obj.EffectiveDate[7];
        //alert("year is:"+year+"month is :"+mon+"day is:"+date);
        $('#txtAllergiesDate_MonthData').val(mon);
        $('#txtAllergiesDate_DayData').val(date);
        $('#txtAllergiesDate_YearData').val(year);

        $('#dtEffectiveDateData').attr("disabled", true);
        $('#txtAllergenTypeData').attr("disabled", true);
        $('#txtAllergiesDate_MonthData').attr("disabled", true);
        $('#txtAllergiesDate_DayData').attr("disabled", true);
        $('#txtAllergiesDate_YearData').attr("disabled", true);
        $('#txtAllergenData').attr("disabled", true);
        $('#txtReactionData').attr("disabled", true);
        $('#txtNoteData').attr("disabled", true);
        $('#cmbConditionStatusData').attr("disabled", true);
    } catch (e) {
        alert(e.message);
    }
    $("#Allergies-Records").dialog('option', 'title', 'Allergy Details');
    $("#Allergies-Records").dialog("open")
}



function viewLabResult(id) {
    ShowLoader();

    try {
        var obj = jQuery.parseJSON($('#hdlab-' + id).val());

    } catch (e) {
        alert(e.message);
    }
    
    $('#lblFacility').prop('disabled', true);
    $('#lblOrderDate').prop('disabled', true);
    $('#lblProvider').prop('disabled', true);
    $('#lblCollectionDate').prop('disabled', true);
    $('#lblRequisition').prop('disabled', true);
    $('#lblReportDate').prop('disabled', true);
    $('#lblSpecimen').prop('disabled', true);
    $('#lblReviewDate').prop('disabled', true);
    $('#lblSpecimenSource').prop('disabled', true);
    $('#lblReviewer').prop('disabled', true);
    $('#lblTest').prop('disabled', true);

    //$('#lblFacility').prop('disabled', true);
    //$('#lblOrderDate').prop('disabled', true);
    //$('#lblProvider').prop('disabled', true);
    //$('#lblCollectionDate').prop('disabled', true);
    //$('#lblRequisition').prop('disabled', true);
    //$('#lblReportDate').prop('disabled', true);
    //$('#lblSpecimen').prop('disabled', true);
    //$('#lblReviewDate').prop('disabled', true);
    //$('#lblSpecimenSource').prop('disabled', true);
    //$('#lblReviewer').prop('disabled', true);
    //$('#lblTest').prop('disabled', true);


    if (obj.CollectionDate == "1/1/1900" || obj.CollectionDate == "1/1/1900 00:00:00" || obj.CollectionDate == "1/1/1900 12:00:00 AM") {
        $('#lblCollectionDate').val("");

    }
    else {

        $('#lblCollectionDate').val(obj.CollectionDate);

    }

    if (obj.OrderDate == "1/1/1900" || obj.OrderDate == "1/1/1900 00:00:00" || obj.OrderDate == "1/1/1900 12:00:00 AM") {
        $('#lblOrderDate').val("");

    }
    else {

        $('#lblOrderDate').val(obj.OrderDate);
    }

    if (obj.ReportDate == "1/1/1900" || obj.ReportDate == "1/1/1900 00:00:00" || obj.ReportDate == "1/1/1900 12:00:00 AM") {
        $('#lblReportDate').val("");

    }
    else {

        $('#lblReportDate').val(obj.ReportDate);
    }
    if (obj.ReviewDate == "1/1/1900" || obj.ReviewDate == "1/1/1900 00:00:00" || obj.ReviewDate == "1/1/1900 12:00:00 AM") {
        $('#lblReviewDate').val("");

    }
    else {

        $('#lblReviewDate').val(obj.ReviewDate);
    }

    $('#lblFacility').val(obj.FacilityName);
    
    $('#lblProvider').val(obj.ProviderName);
    $('#lblRequisition').val(obj.Requisiton);
   
    $('#lblSpecimen').val(obj.Specimen);
  
    $('#lblSpecimenSource').val(obj.SpecimenSource);
    $('#lblReviewer').val(obj.Reviewer);
    $('#lblTest').val(obj.LabName);

    var bValid = true;
    if (bValid) {
        //ajax 
        try {
            var requestData = {
                //FacilityId: obj.FacilityId,
                //VisitId: obj.VisitId,
                FacilityId: $('#cmbFacilityHome').val(),
                //obj.FacilityId,

                VisitId: $('#cmbVisitsHome').val(),
                LabResultCntr: id
            };


            $.ajax({
                type: 'POST',
                url: 'summary-lab-result',
                data: JSON.stringify(requestData),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    // if success
                    // alert("Success : " + data);
                    $("#labResultBody").html(data);
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
                async: false,
                processData: false
            });
        } catch (err) {

            if (err && err !== "") {
                alert(err.message);
                HideLoader();
            }
        }
    }

    $("#lab_result_open").dialog("open");
    $("#lab_result_open").dialog('option', 'title', 'Lab Test Details');
}

function removing_lab_result_class() {
    $('#lab_result_open').removeClass('ui-state-hover ');
    $('#lab_result_open').removeClass('ui-state-active');
}

$(document).ready(function () {
    $("#lab_result_open").dialog({
        autoOpen: false,
        show: {
            effect: "blind",
            duration: 100
        },
        hide: {
            effect: "blind",
            duration: 100
        },
        height: 630,
        width: 800,
        modal: true,
        buttons: {

            Close: function () {
                $(this).dialog("close");
            }
        },
        close: function () {

        }
    });
});

$(function () {
    $('#cmbImmunizationTimeData').timepicker({
        timeFormat: "hh:mm tt"
        });
    $('#txtHeight').keyup(function () {
        updateTotal();
    });

    $('#txtHeightinch').keyup(function () {
        updateTotal();
    });

    $('#txtWeight').keyup(function () {
        updateTotal();
    });

    var updateTotal = function () {

        var heightSpec = $('#txtHeight').val();
        var heightVal = heightSpec.split("_");
        var height = heightVal[0] * 12;
        var height1Spec = $("#txtHeightinch").val();
        var height1Val = height1Spec.split("_");
        var height1 = height1Val[0];
        var heightInch = parseInt(height) + parseInt(height1);
        var weightSpec = $('#txtWeight').val();

        var weightVal = weightSpec.split("_");
        var weight = weightVal[0];

        var result = Math.round(weight / (heightInch * heightInch) * 703);
        if (isNaN(result))
        { $('#result').text(''); }
        else {
            $('#result').text(result.toFixed(1));
        }
    };

    var output_total = $('#result');

    // var total = firstInput + firstInput1;

    output_total.text();

});

$(function () {
    $("#show-date-imm-exp-Data").click(function () {
        $("#txtImmunizationEXDateData").datepicker("show");
    });

    $("#show-date-proc").click(function () {
        $("#date").datepicker("show");
    });
    $("#show-date-sdMed").click(function () {
        $("#dtStartDate").datepicker("show");
    });
    $("#show-date-edMed").click(function () {
        $("#datepicker").datepicker("show");
    });
    $("#show-date-sdMedDetail").click(function () {
        $("#dtStartDateData").datepicker("show");
    });
    $("#show-date-edMedDetail").click(function () {
        $("#datepickerData").datepicker("show");
    });
        $('#show-date-Clinical').click(function () {
        $('#txtPlanneddt').datepicker("show");
    });
    $('#show-date-ClinicalDetail').click(function () {
        $('#txtPlanneddtData').datepicker("show");
    });

    $('#show-date-procdetail').click(function () {
        $('#Datadate').datepicker("show");
    });
    $('#show-date-POC').click(function () {
        $('#Planneddt').datepicker("show");
    });
    $('#show-date-POCDetails').click(function () {
        $('#PlanneddtData').datepicker("show");
    });
    $('#txtHeightData').keyup(function () {
        updateTotal();
    });

    $('#txtHeightinchData').keyup(function () {
        updateTotal();
    });

    $('#txtWeightData').keyup(function () {
        updateTotal();
    });

    var updateTotal = function () {
        
        var heightSpec = $('#txtHeightData').val();
        var heightVal = heightSpec.split("_");
        var height = heightVal[0] * 12;
        var height1Spec = $("#txtHeightinchData").val();
        var height1Val = height1Spec.split("_");
        var height1 = height1Val[0];
        var heightInch = parseInt(height) + parseInt(height1);
        var weightSpec = $('#txtWeightData').val();

        var weightVal = weightSpec.split("_");
        var weight = weightVal[0];

        var result = Math.round(weight / (heightInch * heightInch) * 703);
        if (isNaN(result))
        { $('#resultData').text(''); }
        else {
            $('#resultData').text(result.toFixed(1));
        }
    };

    var output_total = $('#resultData');

    // var total = firstInput + firstInput1;

    output_total.text();

});

$(function () {
    $('#txtoc_Month').change(function () {

        var date = $('#txtoc_Day').val();
        var month = $('#txtoc_Month option:selected').val();
        var year = $('#txtoc_Year option:selected').val();
        if (date == "--" && month == "--" && year == "--") {
            date = "";
            month = "";
            year = "";
        }
        else if (date == "--" && month == "--") { date = ""; month = ""; }
        else if (date == "--" && year == "--") { date = ""; year = ""; }
        else if (month == "--" && year == "--") { month = ""; year = ""; }
        else if (date == "--") { date = ""; }
        else if (month == "--") { month = ""; }
        else if (year == "--") { year = ""; }
        var value = year + month + date;
        $('#dtDateOccurance').val(value);
    });

    $('#txtoc_Day').change(function () {

        var date = $('#txtoc_Day').val();
        var month = $('#txtoc_Month option:selected').val();
        var year = $('#txtoc_Year option:selected').val();
        if (date == "--" && month == "--" && year == "--") {
            date = "";
            month = "";
            year = "";
        }
        else if (date == "--" && month == "--") { date = ""; month = ""; }
        else if (date == "--" && year == "--") { date = ""; year = ""; }
        else if (month == "--" && year == "--") { month = ""; year = ""; }
        else if (date == "--") { date = ""; }
        else if (month == "--") { month = ""; }
        else if (year == "--") { year = ""; }
        var value = year + month + date;
        $('#dtDateOccurance').val(value);

    });
    $('#txtoc_Year').change(function () {

        var date = $('#txtoc_Day').val();
        var month = $('#txtoc_Month option:selected').val();
        var year = $('#txtoc_Year option:selected').val();
        if (date == "--" && month == "--" && year == "--") {
            date = "";
            month = "";
            year = "";
        }
        else if (date == "--" && month == "--") { date = ""; month = ""; }
        else if (date == "--" && year == "--") { date = ""; year = ""; }
        else if (month == "--" && year == "--") { month = ""; year = ""; }
        else if (date == "--") { date = ""; }
        else if (month == "--") { month = ""; }
        else if (year == "--") { year = ""; }
        var value = year + month + date;
        $('#dtDateOccurance').val(value);
    });
    $('#txtSocialBegin_Month').change(function () {
        //var value = $('#txtSocialBegin_Month option:selected').val()
        // + $('#txtSocialBegin_Day option:selected').val() + $('#txtSocialBegin_Year').val();
        //$('#txtSocialBegin').val(value);
        var date = $('#txtSocialBegin_Day').val();
        var month = $('#txtSocialBegin_Month option:selected').val();
        var year = $('#txtSocialBegin_Year option:selected').val();
        if (date == "--" && month == "--" && year == "--") {
            date = "";
            month = "";
            year = "";
        }
        else if (date == "--" && month == "--") { date = ""; month = ""; }
        else if (date == "--" && year == "--") { date = ""; year = ""; }
        else if (month == "--" && year == "--") { month = ""; year = ""; }
        else if (date == "--") { date = ""; }
        else if (month == "--") { month = ""; }
        else if (year == "--") { year = ""; }
        var value = year + month + date;
        $('#txtSocialBegin').val(value);

    });

    $('#txtSocialBegin_Day').change(function () {
        //var value = $('#txtSocialBegin_Month option:selected').val()
        // + $('#txtSocialBegin_Day option:selected').val() + $('#txtSocialBegin_Year').val();
        //$('#txtSocialBegin').val(value);
        var date = $('#txtSocialBegin_Day').val();
        var month = $('#txtSocialBegin_Month option:selected').val();
        var year = $('#txtSocialBegin_Year option:selected').val();
        if (date == "--" && month == "--" && year == "--") {
            date = "";
            month = "";
            year = "";
        }
        else if (date == "--" && month == "--") { date = ""; month = ""; }
        else if (date == "--" && year == "--") { date = ""; year = ""; }
        else if (month == "--" && year == "--") { month = ""; year = ""; }
        else if (date == "--") { date = ""; }
        else if (month == "--") { month = ""; }
        else if (year == "--") { year = ""; }
        var value = year + month + date;
        $('#txtSocialBegin').val(value);

    });
    $('#txtSocialBegin_Year').change(function () {
        //var value = $('#txtSocialBegin_Month option:selected').val()
        // + $('#txtSocialBegin_Day option:selected').val() + $('#txtSocialBegin_Year').val();
        //$('#txtSocialBegin').val(value);
        var date = $('#txtSocialBegin_Day').val();
        var month = $('#txtSocialBegin_Month option:selected').val();
        var year = $('#txtSocialBegin_Year option:selected').val();
        if (date == "--" && month == "--" && year == "--") {
            date = "";
            month = "";
            year = "";
        }
        else if (date == "--" && month == "--") { date = ""; month = ""; }
        else if (date == "--" && year == "--") { date = ""; year = ""; }
        else if (month == "--" && year == "--") { month = ""; year = ""; }
        else if (date == "--") { date = ""; }
        else if (month == "--") { month = ""; }
        else if (year == "--") { year = ""; }
        var value = year + month + date;
        $('#txtSocialBegin').val(value);
    });

    $('#txtSocialEnd_Month').change(function () {
        //var value = $('#txtSocialEnd_Month option:selected').val()
        // + $('#txtSocialEnd_Day option:selected').val() + $('#txtSocialEnd_Year').val();
        //$('#txtSocialEnd').val(value);
        var date = $('#txtSocialEnd_Day').val();
        var month = $('#txtSocialEnd_Month option:selected').val();
        var year = $('#txtSocialEnd_Year option:selected').val();
        if (date == "--" && month == "--" && year == "--") {
            date = "";
            month = "";
            year = "";
        }
        else if (date == "--" && month == "--") { date = ""; month = ""; }
        else if (date == "--" && year == "--") { date = ""; year = ""; }
        else if (month == "--" && year == "--") { month = ""; year = ""; }
        else if (date == "--") { date = ""; }
        else if (month == "--") { month = ""; }
        else if (year == "--") { year = ""; }
        var value = year + month + date;
        $('#txtSocialEnd').val(value);

    });

    $('#txtSocialEnd_Day').change(function () {
        //var value = $('#txtSocialEnd_Month option:selected').val()
        // + $('#txtSocialEnd_Day option:selected').val() + $('#txtSocialEnd_Year').val();
        //$('#txtSocialEnd').val(value);
        var date = $('#txtSocialEnd_Day').val();
        var month = $('#txtSocialEnd_Month option:selected').val();
        var year = $('#txtSocialEnd_Year option:selected').val();
        if (date == "--" && month == "--" && year == "--") {
            date = "";
            month = "";
            year = "";
        }
        else if (date == "--" && month == "--") { date = ""; month = ""; }
        else if (date == "--" && year == "--") { date = ""; year = ""; }
        else if (month == "--" && year == "--") { month = ""; year = ""; }
        else if (date == "--") { date = ""; }
        else if (month == "--") { month = ""; }
        else if (year == "--") { year = ""; }
        var value = year + month + date;
        $('#txtSocialEnd').val(value);

    });
    $('#txtSocialEnd_Year').change(function () {
        //var value = $('#txtSocialEnd_Month option:selected').val()
        // + $('#txtSocialEnd_Day option:selected').val() + $('#txtSocialEnd_Year').val();
        //$('#txtSocialEnd').val(value);
        var date = $('#txtSocialEnd_Day').val();
        var month = $('#txtSocialEnd_Month option:selected').val();
        var year = $('#txtSocialEnd_Year option:selected').val();
        if (date == "--" && month == "--" && year == "--") {
            date = "";
            month = "";
            year = "";
        }
        else if (date == "--" && month == "--") { date = ""; month = ""; }
        else if (date == "--" && year == "--") { date = ""; year = ""; }
        else if (month == "--" && year == "--") { month = ""; year = ""; }
        else if (date == "--") { date = ""; }
        else if (month == "--") { month = ""; }
        else if (year == "--") { year = ""; }
        var value = year + month + date;
        $('#txtSocialEnd').val(value);
    });

    //Dates for Problem List add
    $('#txtProblemsDate_Month').change(function () {
        var date = $('#txtProblemsDate_Day').val();
        var month = $('#txtProblemsDate_Month option:selected').val();
        var year = $('#txtProblemsDate_Year option:selected').val();
        if (date == "--" && month == "--" && year == "--") {
            date = "";
            month = "";
            year = "";
        }
        else if (date == "--" && month == "--") { date = ""; month = ""; }
        else if (date == "--" && year == "--") { date = ""; year = ""; }
        else if (month == "--" && year == "--") { month = ""; year = ""; }
        else if (date == "--") { date = ""; }
        else if (month == "--") { month = ""; }
        else if (year == "--") { year = ""; }
        var value = year + month + date;
        $('#txtProblemsDate').val(value);

    });

    $('#txtProblemsDate_Day').change(function () {
        //var value = $('#txtProblemsDate_Year').val()
        //  + $('#txtProblemsDate_Month option:selected').val()  + $('#txtProblemsDate_Day option:selected').val() ;    
        //$('#txtProblemsDate').val(value);
        var date = $('#txtProblemsDate_Day').val();
        var month = $('#txtProblemsDate_Month option:selected').val();
        var year = $('#txtProblemsDate_Year option:selected').val();
        if (date == "--" && month == "--" && year == "--") {
            date = "";
            month = "";
            year = "";
        }
        else if (date == "--" && month == "--") { date = ""; month = ""; }
        else if (date == "--" && year == "--") { date = ""; year = ""; }
        else if (month == "--" && year == "--") { month = ""; year = ""; }
        else if (date == "--") { date = ""; }
        else if (month == "--") { month = ""; }
        else if (year == "--") { year = ""; }
        var value = year + month + date;
        $('#txtProblemsDate').val(value);
    });
    $('#txtProblemsDate_Year').change(function () {
        //var value = $('#txtProblemsDate_Year').val()
        // + $('#txtProblemsDate_Month option:selected').val()  + $('#txtProblemsDate_Day option:selected').val() ;
        //$('#txtProblemsDate').val(value);
        var date = $('#txtProblemsDate_Day').val();
        var month = $('#txtProblemsDate_Month option:selected').val();
        var year = $('#txtProblemsDate_Year option:selected').val();
        if (date == "--" && month == "--" && year == "--") {
            date = "";
            month = "";
            year = "";
        }
        else if (date == "--" && month == "--") { date = ""; month = ""; }
        else if (date == "--" && year == "--") { date = ""; year = ""; }
        else if (month == "--" && year == "--") { month = ""; year = ""; }
        else if (date == "--") { date = ""; }
        else if (month == "--") { month = ""; }
        else if (year == "--") { year = ""; }
        var value = year + month + date;
        $('#txtProblemsDate').val(value);
    });

    $('#txtProblemsLastDate_Month').change(function () {
        //      var value = $('#txtProblemsLastDate_Month option:selected').val()
        //+ $('#txtProblemsLastDate_Day option:selected').val() +  $('#txtProblemsLastDate_Year').val();
        //      $('#txtPlastDate').val(value);
        var date = $('#txtProblemsLastDate_Day option:selected').val();
        var month = $('#txtProblemsLastDate_Month option:selected').val();
        var year = $('#txtProblemsLastDate_Year').val();
        if (date == "--" && month == "--" && year == "--") {
            date = "";
            month = "";
            year = "";
        }
        else if (date == "--" && month == "--") { date = ""; month = ""; }
        else if (date == "--" && year == "--") { date = ""; year = ""; }
        else if (month == "--" && year == "--") { month = ""; year = ""; }
        else if (date == "--") { date = ""; }
        else if (month == "--") { month = ""; }
        else if (year == "--") { year = ""; }
        var value = year + month + date;
        $('#txtPlastDate').val(value);

    });

    $('#txtProblemsLastDate_Day').change(function () {
        //      var value = $('#txtProblemsLastDate_Month option:selected').val()
        //+ $('#txtProblemsLastDate_Day option:selected').val() +  $('#txtProblemsLastDate_Year').val();
        //      $('#txtPlastDate').val(value);
        var date = $('#txtProblemsLastDate_Day option:selected').val();
        var month = $('#txtProblemsLastDate_Month option:selected').val();
        var year = $('#txtProblemsLastDate_Year').val();
        if (date == "--" && month == "--" && year == "--") {
            date = "";
            month = "";
            year = "";
        }
        else if (date == "--" && month == "--") { date = ""; month = ""; }
        else if (date == "--" && year == "--") { date = ""; year = ""; }
        else if (month == "--" && year == "--") { month = ""; year = ""; }
        else if (date == "--") { date = ""; }
        else if (month == "--") { month = ""; }
        else if (year == "--") { year = ""; }
        var value = year + month + date;
        $('#txtPlastDate').val(value);
    });
    $('#txtProblemsLastDate_Year').change(function () {
        //      var value = $('#txtProblemsLastDate_Month option:selected').val()
        //+ $('#txtProblemsLastDate_Day option:selected').val() +  $('#txtProblemsLastDate_Year').val();
        //      $('#txtPlastDate').val(value);
        var date = $('#txtProblemsLastDate_Day option:selected').val();
        var month = $('#txtProblemsLastDate_Month option:selected').val();
        var year = $('#txtProblemsLastDate_Year').val();
        if (date == "--" && month == "--" && year == "--") {
            date = "";
            month = "";
            year = "";
        }
        else if (date == "--" && month == "--") { date = ""; month = ""; }
        else if (date == "--" && year == "--") { date = ""; year = ""; }
        else if (month == "--" && year == "--") { month = ""; year = ""; }
        else if (date == "--") { date = ""; }
        else if (month == "--") { month = ""; }
        else if (year == "--") { year = ""; }
        var value = year + month + date;
        $('#txtPlastDate').val(value);
    });

    //Dates for problem list edit 
    $('#txtProblemsDate_MonthData').change(function () {
        var date = $('#txtProblemsDate_DayData').val();
        var month = $('#txtProblemsDate_MonthData option:selected').val();
        var year = $('#txtProblemsDate_YearData option:selected').val();
        if (date == "--" && month == "--" && year == "--") {
            date = "";
            month = "";
            year = "";
        }
        else if (date == "--" && month == "--") { date = ""; month = ""; }
        else if (date == "--" && year == "--") { date = ""; year = ""; }
        else if (month == "--" && year == "--") { month = ""; year = ""; }
        else if (date == "--") { date = ""; }
        else if (month == "--") { month = ""; }
        else if (year == "--") { year = ""; }
        var value = year + month + date;
        $('#txtProblemsDateData').val(value);

    });

    $('#txtProblemsDate_DayData').change(function () {
        //var value = $('#txtProblemsDate_Year').val()
        //  + $('#txtProblemsDate_MonthData option:selected').val()  + $('#txtProblemsDate_Day option:selected').val() ;    
        //$('#txtProblemsDate').val(value);
        var date = $('#txtProblemsDate_DayData').val();
        var month = $('#txtProblemsDate_MonthData option:selected').val();
        var year = $('#txtProblemsDate_YearData option:selected').val();
        if (date == "--" && month == "--" && year == "--") {
            date = "";
            month = "";
            year = "";
        }
        else if (date == "--" && month == "--") { date = ""; month = ""; }
        else if (date == "--" && year == "--") { date = ""; year = ""; }
        else if (month == "--" && year == "--") { month = ""; year = ""; }
        else if (date == "--") { date = ""; }
        else if (month == "--") { month = ""; }
        else if (year == "--") { year = ""; }
        var value = year + month + date;
        $('#txtProblemsDateData').val(value);
    });
    $('#txtProblemsDate_YearData').change(function () {
        //var value = $('#txtProblemsDate_Year').val()
        // + $('#txtProblemsDate_MonthData option:selected').val()  + $('#txtProblemsDate_Day option:selected').val() ;
        //$('#txtProblemsDate').val(value);
        var date = $('#txtProblemsDate_DayData').val();
        var month = $('#txtProblemsDate_MonthData option:selected').val();
        var year = $('#txtProblemsDate_YearData option:selected').val();
        if (date == "--" && month == "--" && year == "--") {
            date = "";
            month = "";
            year = "";
        }
        else if (date == "--" && month == "--") { date = ""; month = ""; }
        else if (date == "--" && year == "--") { date = ""; year = ""; }
        else if (month == "--" && year == "--") { month = ""; year = ""; }
        else if (date == "--") { date = ""; }
        else if (month == "--") { month = ""; }
        else if (year == "--") { year = ""; }
        var value = year + month + date;
        $('#txtProblemsDateData').val(value);
    });

    $('#txtProblemsLastDate_MonthData').change(function () {
        //      var value = $('#txtProblemsLastDate_Month option:selected').val()
        //+ $('#txtProblemsLastDate_Day option:selected').val() +  $('#txtProblemsLastDate_Year').val();
        //      $('#txtPlastDate').val(value);
        var date = $('#txtProblemsLastDate_DayData option:selected').val();
        var month = $('#txtProblemsLastDate_MonthData option:selected').val();
        var year = $('#txtProblemsLastDate_YearData').val();
        if (date == "--" && month == "--" && year == "--") {
            date = "";
            month = "";
            year = "";
        }
        else if (date == "--" && month == "--") { date = ""; month = ""; }
        else if (date == "--" && year == "--") { date = ""; year = ""; }
        else if (month == "--" && year == "--") { month = ""; year = ""; }
        else if (date == "--") { date = ""; }
        else if (month == "--") { month = ""; }
        else if (year == "--") { year = ""; }
        var value = year + month + date;
        $('#txtPlastDateData').val(value);

    });

    $('#txtProblemsLastDate_DayData').change(function () {
        //      var value = $('#txtProblemsLastDate_Month option:selected').val()
        //+ $('#txtProblemsLastDate_Day option:selected').val() +  $('#txtProblemsLastDate_Year').val();
        //      $('#txtPlastDate').val(value);
        var date = $('#txtProblemsLastDate_DayData option:selected').val();
        var month = $('#txtProblemsLastDate_MonthData option:selected').val();
        var year = $('#txtProblemsLastDate_YearData').val();
        if (date == "--" && month == "--" && year == "--") {
            date = "";
            month = "";
            year = "";
        }
        else if (date == "--" && month == "--") { date = ""; month = ""; }
        else if (date == "--" && year == "--") { date = ""; year = ""; }
        else if (month == "--" && year == "--") { month = ""; year = ""; }
        else if (date == "--") { date = ""; }
        else if (month == "--") { month = ""; }
        else if (year == "--") { year = ""; }
        var value = year + month + date;
        $('#txtPlastDateData').val(value);
    });
    $('#txtProblemsLastDate_YearData').change(function () {
        //      var value = $('#txtProblemsLastDate_Month option:selected').val()
        //+ $('#txtProblemsLastDate_Day option:selected').val() +  $('#txtProblemsLastDate_Year').val();
        //      $('#txtPlastDate').val(value);
        var date = $('#txtProblemsLastDate_DayData option:selected').val();
        var month = $('#txtProblemsLastDate_MonthData option:selected').val();
        var year = $('#txtProblemsLastDate_YearData').val();
        if (date == "--" && month == "--" && year == "--") {
            date = "";
            month = "";
            year = "";
        }
        else if (date == "--" && month == "--") { date = ""; month = ""; }
        else if (date == "--" && year == "--") { date = ""; year = ""; }
        else if (month == "--" && year == "--") { month = ""; year = ""; }
        else if (date == "--") { date = ""; }
        else if (month == "--") { month = ""; }
        else if (year == "--") { year = ""; }
        var value = year + month + date;
        $('#txtPlastDateData').val(value);
    });


    $('#txtAllergiesDate_Month').change(function () {
        var date = $('#txtAllergiesDate_Day').val();
        var month = $('#txtAllergiesDate_Month option:selected').val();
        var year = $('#txtAllergiesDate_Year option:selected').val();
        if (date == "--" && month == "--" && year == "--") {
            date = "";
            month = "";
            year = "";
        }
        else if (date == "--" && month == "--") { date = ""; month = ""; }
        else if (date == "--" && year == "--") { date = ""; year = ""; }
        else if (month == "--" && year == "--") { month = ""; year = ""; }
        else if (date == "--") { date = ""; }
        else if (month == "--") { month = ""; }
        else if (year == "--") { year = ""; }
        var value = year + month + date;
        $('#txtAllergiesDate').val(value);

    });

    $('#txtAllergiesDate_Day').change(function () {

        var date = $('#txtAllergiesDate_Day').val();
        var month = $('#txtAllergiesDate_Month option:selected').val();
        var year = $('#txtAllergiesDate_Year option:selected').val();
        if (date == "--" && month == "--" && year == "--") {
            date = "";
            month = "";
            year = "";
        }
        else if (date == "--" && month == "--") { date = ""; month = ""; }
        else if (date == "--" && year == "--") { date = ""; year = ""; }
        else if (month == "--" && year == "--") { month = ""; year = ""; }
        else if (date == "--") { date = ""; }
        else if (month == "--") { month = ""; }
        else if (year == "--") { year = ""; }
        var value = year + month + date;
        $('#txtAllergiesDate').val(value);
    });
    $('#txtAllergiesDate_Year').change(function () {

        var date = $('#txtAllergiesDate_Day').val();
        var month = $('#txtAllergiesDate_Month option:selected').val();
        var year = $('#txtAllergiesDate_Year option:selected').val();
        if (date == "--" && month == "--" && year == "--") {
            date = "";
            month = "";
            year = "";
        }
        else if (date == "--" && month == "--") { date = ""; month = ""; }
        else if (date == "--" && year == "--") { date = ""; year = ""; }
        else if (month == "--" && year == "--") { month = ""; year = ""; }
        else if (date == "--") { date = ""; }
        else if (month == "--") { month = ""; }
        else if (year == "--") { year = ""; }
        var value = year + month + date;
        $('#txtAllergiesDate').val(value);
    });

    $('#txtAllergiesDate_MonthData').change(function () {
        var date = $('#txtAllergiesDate_DayData').val();
        var month = $('#txtAllergiesDate_MonthData option:selected').val();
        var year = $('#txtAllergiesDate_YearData option:selected').val();
        if (date == "--" && month == "--" && year == "--") {
            date = "";
            month = "";
            year = "";
        }
        else if (date == "--" && month == "--") { date = ""; month = ""; }
        else if (date == "--" && year == "--") { date = ""; year = ""; }
        else if (month == "--" && year == "--") { month = ""; year = ""; }
        else if (date == "--") { date = ""; }
        else if (month == "--") { month = ""; }
        else if (year == "--") { year = ""; }
        var value = year + month + date;
        $('#txtAllergiesDateData').val(value);

    });

    $('#txtAllergiesDate_DayData').change(function () {

        var date = $('#txtAllergiesDate_DayData').val();
        var month = $('#txtAllergiesDate_MonthData option:selected').val();
        var year = $('#txtAllergiesDate_YearData option:selected').val();
        if (date == "--" && month == "--" && year == "--") {
            date = "";
            month = "";
            year = "";
        }
        else if (date == "--" && month == "--") { date = ""; month = ""; }
        else if (date == "--" && year == "--") { date = ""; year = ""; }
        else if (month == "--" && year == "--") { month = ""; year = ""; }
        else if (date == "--") { date = ""; }
        else if (month == "--") { month = ""; }
        else if (year == "--") { year = ""; }
        var value = year + month + date;
        $('#txtAllergiesDateData').val(value);
    });
    $('#txtAllergiesDate_YearData').change(function () {

        var date = $('#txtAllergiesDate_DayData').val();
        var month = $('#txtAllergiesDate_MonthData option:selected').val();
        var year = $('#txtAllergiesDate_YearData option:selected').val();
        if (date == "--" && month == "--" && year == "--") {
            date = "";
            month = "";
            year = "";
        }
        else if (date == "--" && month == "--") { date = ""; month = ""; }
        else if (date == "--" && year == "--") { date = ""; year = ""; }
        else if (month == "--" && year == "--") { month = ""; year = ""; }
        else if (date == "--") { date = ""; }
        else if (month == "--") { month = ""; }
        else if (year == "--") { year = ""; }
        var value = year + month + date;
        $('#txtAllergiesDateData').val(value);
    });

    $('#txtoc_MonthData').change(function () {
        var date = $('#txtoc_DayData').val();
        var month = $('#txtoc_MonthData option:selected').val();
        var year = $('#txtoc_YearData option:selected').val();
        if (date == "--" && month == "--" && year == "--") {
            date = "";
            month = "";
            year = "";
        }
        else if (date == "--" && month == "--") { date = ""; month = ""; }
        else if (date == "--" && year == "--") { date = ""; year = ""; }
        else if (month == "--" && year == "--") { month = ""; year = ""; }
        else if (date == "--") { date = ""; }
        else if (month == "--") { month = ""; }
        else if (year == "--") { year = ""; }
        var value = year + month + date;
        $('#dtDateOccuranceData').val(value);

    });

    $('#txtoc_DayData').change(function () {

        var date = $('#txtoc_DayData').val();
        var month = $('#txtoc_MonthData option:selected').val();
        var year = $('#txtoc_YearData option:selected').val();
        if (date == "--" && month == "--" && year == "--") {
            date = "";
            month = "";
            year = "";
        }
        else if (date == "--" && month == "--") { date = ""; month = ""; }
        else if (date == "--" && year == "--") { date = ""; year = ""; }
        else if (month == "--" && year == "--") { month = ""; year = ""; }
        else if (date == "--") { date = ""; }
        else if (month == "--") { month = ""; }
        else if (year == "--") { year = ""; }
        var value = year + month + date;
        $('#dtDateOccuranceData').val(value);
    });
    $('#txtoc_YearData').change(function () {

        var date = $('#txtoc_DayData').val();
        var month = $('#txtoc_MonthData option:selected').val();
        var year = $('#txtoc_YearData option:selected').val();
        if (date == "--" && month == "--" && year == "--") {
            date = "";
            month = "";
            year = "";
        }
        else if (date == "--" && month == "--") { date = ""; month = ""; }
        else if (date == "--" && year == "--") { date = ""; year = ""; }
        else if (month == "--" && year == "--") { month = ""; year = ""; }
        else if (date == "--") { date = ""; }
        else if (month == "--") { month = ""; }
        else if (year == "--") { year = ""; }
        var value = year + month + date;
        $('#dtDateOccuranceData').val(value);
    });

    $('#txtHeight').change(function () {
        if ($("#txtHeightinch").val().length > 0 && $("#txtWeight").val().length > 0) {

            updateTotal();

        }
    });

    $('#txtHeightinch').change(function () {
        if ($("#txtHeight").val().length > 0 && $("#txtWeight").val().length > 0) {

            updateTotal();

        }
    });

    $('#txtWeight').change(function () {
        if ($("#txtHeight").val().length > 0 && $("#txtHeightinch").val().length > 0) {

            updateTotal();

        }
    });

    //var updateTotal = function () {
    //    var height = $('#txtHeight').val()*12;
    //    var height1 = $("#txtHeightinch").val();
    //    var heightInch = parseInt(height) + parseInt(height1);
    //    var weight = $('#txtWeight').val();
    //    var result = weight /(heightInch*heightInch)*703;

    //    $('#result').text( result.toFixed(1) );
    //};


    var output_total = $('#result');

    // var total = firstInput + firstInput1;

    output_total.text();

});

$(function () {
    $("#txtAgeOnsetSpinner").spinner();
    $("#txtAgeDiseaseSpinner").spinner();

    $("#disable").click(function () {
        $("#txtAgeDiseaseSpinner").spinner({ disabled: true });

    });

    $("#enable").click(function () {
        $("#txtAgeDiseaseSpinner").spinner({ disabled: false });
    });

});


var AppointmentFlag = true;
function appointment_move(flag) {
    if ($('#cmbFacilityHome :selected').text() == 'Patient Entered') {
         $('#createaddappointments').toggle();
    }
    $('#App-ShareHide').toggle();

    $('#widg1 .portlet-header .ui-icon').toggleClass("ui-icon-minusthick").toggleClass("ui-icon-plusthick");
    $('#widg1 .portlet-header .ui-icon').parents(".portlet:first").find(".portlet-content").toggle();
    if (AppointmentFlag) {
        AppointmentWidget();
    }
    AppointmentFlag = !AppointmentFlag;

        //  window.location.href = 'appointments-index';
        //if (flag) {
        //    //   alert(flag);
        //    $("#createaddappointments").css("display", "none");
        //    $("#App-ShareHide").css("display", "none");
        //    $("#Appointment-portlet").css("display", "none");
        //    $("#Appointment").attr("onclick", "appointment_move(false)");


        //} else {
        //    $("#createaddappointments").css("display", "block");
        //    $("#App-ShareHide").css("display", "block");
        //    $("#Appointment-portlet").css("display", "block");
        //    $("#Appointment").attr("onclick", "appointment_move(true)");

        //}

}

function AppointmentWidget()
{
    var facilityId = $('#cmbFacilityHome option:selected').val();
    var visitId = $('#cmbVisitsHome option:selected').val();

    //Start Allergy Ajax
    try {
        ShowLoader();
        //var IsPrimaryData =
        var requestData = {

            FacilityId: facilityId,
            VisitId: visitId

        };


        $.ajax({
            type: 'POST',
            url: 'Home/AppointmentWidgetData',
            data: JSON.stringify(requestData),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                // if success

                $("#Appointment-portlet").html(data);

                $("#tbl-Appointments").fixedHeaderTable();
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

}
var LabFlag = true;
    function lab_move(flag) { //done
        chkAccess();
        if ($('#cmbFacilityHome :selected').text() == 'Patient Entered') {
          //  $('#createaddproblems').toggle();
        }
        $('#Lab-ShareHide').toggle();

        $('#widg3 .portlet-header .ui-icon').toggleClass("ui-icon-minusthick").toggleClass("ui-icon-plusthick");
        $('#widg3 .portlet-header .ui-icon').parents(".portlet:first").find(".portlet-content").toggle();
        if (LabFlag) {
            $('#widg3 .portlet-header').css('margin-bottom', '0em');
            LabWidget();
        }
        else {
            $('#widg3 .portlet-header').css('margin-bottom', '0.3em');
        }
        LabFlag = !LabFlag;
        //window.location.href = 'clinical-summary?id=3';medication_move()
        //$("#tabs").tabs({
        //    active: 3
        //});
        //if (flag) {
        //    //   alert(flag);
        //    //  $("#createaddappointments").css("display", "none");
        //    $("#Lab-ShareHide").css("display", "none");
        //    $("#lab-portlet").css("display", "none");
        //    $("#Labtest").attr("onclick", "lab_move(false)");


        //} else {
        //    //   $("#createaddappointments").css("display", "block");
        //    $("#Lab-ShareHide").css("display", "block");
        //    $("#lab-portlet").css("display", "block");
        //    $("#Labtest").attr("onclick", "lab_move(true)");

        //}
    }

    function LabWidget()
    {
        var facilityId = $('#cmbFacilityHome option:selected').val();
        var visitId = $('#cmbVisitsHome option:selected').val();

        //Start Allergy Ajax
        try {
            ShowLoader();
            //var IsPrimaryData =
            var requestData = {

                FacilityId: facilityId,
                VisitId: visitId

            };


            $.ajax({
                type: 'POST',
                url: 'Home/LabWidgetData',
                data: JSON.stringify(requestData),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    // if success

                    $("#lab-portlet").html(data);

                    $("#tbl-LabTests").fixedHeaderTable();
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


    }
    var Visitflag = true;
    function visit_move(flag) {//done
        chkAccess();
        if ($('#cmbFacilityHome :selected').text() == 'Patient Entered') {
            //  $('#createaddproblems').toggle();
        }
        $('#App-ShareHideVisits').toggle();

        $('#visitswidg .portlet-header .ui-icon').toggleClass("ui-icon-minusthick").toggleClass("ui-icon-plusthick");
        $('#visitswidg .portlet-header .ui-icon').parents(".portlet:first").find(".portlet-content").toggle();
        if (Visitflag) {
            $('#visitswidg .portlet-header').css('margin-bottom', '0em');
            VisitWidget();
        }
        else {
            $('#visitswidg .portlet-header').css('margin-bottom', '0.3em');
        }
        Visitflag = !Visitflag;
        //window.location.href = 'clinical-summary?id=2';
        //$("#tabs").tabs({
        //    active: 2
        //});
        //if (flag) {
        //    //   alert(flag);
        //    //  $("#createaddproblems").css("display", "none");
        //    $("#App-ShareHideVisits").css("display", "none");
        //    $("#Visits-portlet").css("display", "none");
        //    $("#VisitMove").attr("onclick", "visit_move(false)");


        //} else {
        //    //   $("#createaddproblems").css("display", "block");
        //    $("#App-ShareHideVisits").css("display", "block");
        //    $("#Visits-portlet").css("display", "block");
        //    $("#VisitMove").attr("onclick", "visit_move(true)");

        //}
    }
    function VisitWidget()
    {

        var facilityId = $('#cmbFacilityHome option:selected').val();
        var visitId = $('#cmbVisitsHome option:selected').val();

        try {
            ShowLoader();
            //var IsPrimaryData =
            var requestData = {

                FacilityId: facilityId,
                VisitId: visitId

            };
             $.ajax({
                type: 'POST',
                url: 'Home/VisitsWidgetData',
                //clinical-summary-PlanOfCare-filter',
                data: JSON.stringify(requestData),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    // if success

                    $("#Visits-portlet").html(data);
                    $("#tbl-Visits").fixedHeaderTable();
                    // toggle_combobox_PlanOfCare();

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
    }
    var medicationflag = true;
    function medication_move(flag) {
        chkAccess();
        if ($('#cmbFacilityHome :selected').text() == 'Patient Entered') {
            $('#createPresentMedication').toggle();
        }
        $('#App-ShareHideMedications').toggle();

        $('#widg2 .portlet-header .ui-icon').toggleClass("ui-icon-minusthick").toggleClass("ui-icon-plusthick");
        $('#widg2 .portlet-header .ui-icon').parents(".portlet:first").find(".portlet-content").toggle();
        if (medicationflag) {
            $('#widg2 .portlet-header').css('margin-bottom', '0em');
            MedicationWidget();
        }
        else {
            $('#widg2 .portlet-header').css('margin-bottom', '0.3em');
        }
        medicationflag = !medicationflag;
        //  window.location.href = 'medication-index';
        //if (flag) {
        //    //   alert(flag);
        //    $("#createPresentMedication").css("display", "none");
        //    $("#App-ShareHideMedications").css("display", "none");
        //    $("#medications-portlet").css("display", "none");
        //    $("#Medicatons").attr("onclick", "medication_move(false)");


        //} else {
        //    $("#createPresentMedication").css("display", "block");
        //    $("#App-ShareHideMedications").css("display", "block");
        //    $("#medications-portlet").css("display", "block");
        //    $("#Medicatons").attr("onclick", "medication_move(true)");

        //}
    }
    function MedicationWidget()
    {
        var facilityId = $('#cmbFacilityHome option:selected').val();
        var visitId = $('#cmbVisitsHome option:selected').val();

        //Start Allergy Ajax
        try {
            ShowLoader();
            //var IsPrimaryData =
            var requestData = {

                FacilityId: facilityId,
                VisitId: visitId

            };


            $.ajax({
                type: 'POST',
                url: 'Home/MedicationWidgetData',
                data: JSON.stringify(requestData),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    // if success

                    $("#medications-portlet").html(data);

                    $("#tbl-Medications").fixedHeaderTable();
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
    }
    var ProblemFlag = true;
    function problem_move(flag) {
        chkAccess();
        if ($('#cmbFacilityHome :selected').text() == 'Patient Entered') {
            $('#createaddproblems').toggle();
        }
        $('#App-ShareHideProblems').toggle();

        $('#probswidg .portlet-header .ui-icon').toggleClass("ui-icon-minusthick").toggleClass("ui-icon-plusthick");
        $('#probswidg .portlet-header .ui-icon').parents(".portlet:first").find(".portlet-content").toggle();
        if (ProblemFlag) {
            $('#probswidg .portlet-header').css('margin-bottom', '0em');
            ProblemWidget();
        }
        else {
            $('#probswidg .portlet-header').css('margin-bottom', '0.3em');
        }

        ProblemFlag = !ProblemFlag;
        //window.location.href = 'clinical-summary?id=7';
        //$("#tabs").tabs({
        //    active: 7
        //});
        //if (flag) {
        //    //   alert(flag);
        //    $("#createaddproblems").css("display", "none");
        //    $("#App-ShareHideProblems").css("display", "none");
        //    $("#problem-portlet").css("display", "none");
        //    $("#problem").attr("onclick", "problem_move(false)");


        //} else {
        //    $("#createaddproblems").css("display", "block");
        //    $("#App-ShareHideProblems").css("display", "block");
        //    $("#problem-portlet").css("display", "block");
        //    $("#problem").attr("onclick", "problem_move(true)");

        //}
    }

    function ProblemWidget()
    {
        var facilityId = $('#cmbFacilityHome option:selected').val();
        var visitId = $('#cmbVisitsHome option:selected').val();

        //Start Allergy Ajax
        try {
            ShowLoader();
            //var IsPrimaryData =
            var requestData = {

                FacilityId: facilityId,
                VisitId: visitId

            };


            $.ajax({
                type: 'POST',
                url: 'Home/ProblemWidgetData',
                data: JSON.stringify(requestData),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    // if success

                    $("#problem-portlet").html(data);

                    $("#tbl-Problems").fixedHeaderTable();
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
    }
    var VitalFlag = true;
    function vital_move(flag) {
        chkAccess();
        if ($('#cmbFacilityHome :selected').text() == 'Patient Entered') {
            $('#addvitalscreation').toggle();
        }
        $('#App-ShareHideVitals').toggle();

        $('#vitalwidg .portlet-header .ui-icon').toggleClass("ui-icon-minusthick").toggleClass("ui-icon-plusthick");
        $('#vitalwidg .portlet-header .ui-icon').parents(".portlet:first").find(".portlet-content").toggle();
        if (VitalFlag) {
            $('#vitalwidg .portlet-header').css('margin-bottom', '0em');
            VitalWidget();
        }
        else {
            $('#vitalwidg .portlet-header').css('margin-bottom', '0.3em');
        }
        VitalFlag = !VitalFlag;
        //window.location.href = 'clinical-summary?id=8';
        //$("#tabs").tabs({
        //    active: 8
        //});

        //if (flag) {
        //    //   alert(flag);
        //    $("#addvitalscreation").css("display", "none");
        //    $("#App-ShareHideVitals").css("display", "none");
        //    $("#vital-portlet").css("display", "none");
        //    $("#vitalsmove").attr("onclick", "vital_move(false)");


        //} else {
        //    $("#addvitalscreation").css("display", "block");
        //    $("#App-ShareHideVitals").css("display", "block");
        //    $("#vital-portlet").css("display", "block");
        //    $("#vitalsmove").attr("onclick", "vital_move(true)");

        //}
    }

    function VitalWidget()
    {
        var facilityId = $('#cmbFacilityHome option:selected').val();
        var visitId = $('#cmbVisitsHome option:selected').val();

        //Start Allergy Ajax
        try {
            ShowLoader();
            //var IsPrimaryData =
            var requestData = {

                FacilityId: facilityId,
                VisitId: visitId

            };


            $.ajax({
                type: 'POST',
                url: 'Home/VitalWidgetData',
                data: JSON.stringify(requestData),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    // if success

                    $("#vital-portlet").html(data);

                    $("#tbl-VitalSigns").fixedHeaderTable();
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

    }
    var allergyflag = true;
    function allergy_move(flag) {
        chkAccess();
        if ($('#cmbFacilityHome :selected').text() == 'Patient Entered') {
            $('#createaddallergies').toggle();   
        }
        $('#App-ShareHideAllergies').toggle();
        
        $('#allergywidg .portlet-header .ui-icon').toggleClass("ui-icon-minusthick").toggleClass("ui-icon-plusthick");
        $('#allergywidg .portlet-header .ui-icon').parents(".portlet:first").find(".portlet-content").toggle();
        if (allergyflag) {
            $('#allergywidg .portlet-header').css('margin-bottom', '0em');
            AllergyWidget();
        }
        else {
            $('#allergywidg .portlet-header').css('margin-bottom', '0.3em');
        }
        allergyflag = !allergyflag;
        //window.location.href = 'clinical-summary?id=5';
        //$("#tabs").tabs({
        //    active: 5
        //});
        //if (flag) {
        //    //   alert(flag);
        //    $("#createaddallergies").css("display", "none");
        //    $("#App-ShareHideAllergies").css("display", "none");
        //    $("#allergies-portlet").css("display", "none");
        //    $("#allergy").attr("onclick", "allergy_move(false)");
    

        //} else {
        //    $("#createaddallergies").css("display", "block");
        //    $("#App-ShareHideAllergies").css("display", "block");
        //    $("#allergies-portlet").css("display", "block");
        //    $("#allergy").attr("onclick", "allergy_move(true)");
 
        //}

    }

    
    function AllergyWidget()
    {
        var facilityId = $('#cmbFacilityHome option:selected').val();
        var visitId = $('#cmbVisitsHome option:selected').val();

        //Start Allergy Ajax
        try {
            ShowLoader();
            //var IsPrimaryData =
            var requestData = {

                FacilityId: facilityId,
                VisitId: visitId

            };


            $.ajax({
                type: 'POST',
                url: 'AllergyWidget',
                data: JSON.stringify(requestData),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    // if success

                    $("#allergies-portlet").html(data);

                    $("#tbl-Allergies").fixedHeaderTable();
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
    }

    var socialHistLatest = true;
    function socialLatest_move(flag)
    {
        chkAccess();
        if ($('#cmbFacilityHome :selected').text() == 'Patient Entered') {
            $('#EditcalInstructions').toggle();
        }
        $('#App-ShareHideSocialHistory').toggle();

        $('#socialhistLatestwidg .portlet-header .ui-icon').toggleClass("ui-icon-minusthick").toggleClass("ui-icon-plusthick");
        $('#socialhistLatestwidg .portlet-header .ui-icon').parents(".portlet:first").find(".portlet-content").toggle();
        if ($('#cmbFacilityHome option:selected').val() == "0") {
            //  alert("patient Entered");
            if (socialHistLatest) {
                //   $('#EditcalInstructions').toggle();
                $('#socialhistLatestwidg .portlet-header').css('margin-bottom', '0em');
                MySocialHistoryWidget();
                
            }
            else {
                $('#socialhistLatestwidg .portlet-header').css('margin-bottom', '0.3em');
            }
        }
        else {
            if (socialHistLatest) {
                // alert("else");
                $('#socialhistLatestwidg .portlet-header').css('margin-bottom', '0em');
                SocailWidget();
            }
            else {
                $('#socialhistLatestwidg .portlet-header').css('margin-bottom', '0.3em');
            }

        }
        socialHistLatest = !socialHistLatest;
    }
    var socialhistoryflag = true;
   
    function social_move(flag) {//done
        if ($('#cmbFacilityHome :selected').text() == 'Patient Entered') {
            $('#EditcalInstructions').toggle();
        }
        $('#App-ShareHideSocialHistory').toggle();

        $('#socialhistwidg .portlet-header .ui-icon').toggleClass("ui-icon-minusthick").toggleClass("ui-icon-plusthick");
        $('#socialhistwidg .portlet-header .ui-icon').parents(".portlet:first").find(".portlet-content").toggle();
        if ($('#cmbFacilityHome option:selected').val() == "0") {
          //  alert("patient Entered");
            if (mysocialflag) {
             //   $('#EditcalInstructions').toggle();
                MySocialHistoryWidget();

            }
        }
        else {
            if (socialhistoryflag) {
               // alert("else");
                SocailWidget();
            }

        }
        mysocialflag = !mysocialflag;
        socialhistoryflag = !socialhistoryflag;

        //window.location.href = 'clinical-summary?id=4';
        //$("#tabs").tabs({
        //    active: 4
        //});
        //if (flag) {
        //    //   alert(flag);
        //    $("#createaddsocial").css("display", "none");
        //    $("#App-ShareHideSocialHistory").css("display", "none");
        //    $("#social-portlet").css("display", "none");
        //    $("#SocailMove").attr("onclick", "social_move(false)");


        //} else {
        //    $("#createaddsocial").css("display", "block");
        //    $("#App-ShareHideSocialHistory").css("display", "block");
        //    $("#social-portlet").css("display", "block");
        //    $("#SocailMove").attr("onclick", "social_move(true)");

        //}
    }

    function SocailWidget()
    {
        var facilityId = $('#cmbFacilityHome option:selected').val();
        var visitId = $('#cmbVisitsHome option:selected').val();

        //Start Allergy Ajax
        try {
            ShowLoader();
            //var IsPrimaryData =
            var requestData = {

                FacilityId: facilityId,
                VisitId: visitId

            };


            $.ajax({
                type: 'POST',
                url: 'Home/SocailWidgetData',
                data: JSON.stringify(requestData),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    // if success
                   // alert("socail History");
                    //$("#social-portlet").html(" ");
                    //$("#social-portlet").html(data);

                 //   $("#tbl-SocialHistory").fixedHeaderTable();

                    //if (facilityId == "0") {
                    //    $("#SocialHistorySelf-portlet").html(data);
                    //    $("#social-portlet").html(data);
                    //    $("#tbl-socailHistoryself").fixedHeaderTable();
                    //}
                    //else {
                    // //   alert("else");
                    //    $("#social-portlet").html(data);
                    //    $("#SocialHistorySelf-portlet").html(data);
                    //    $("#tbl-SocialHistory").fixedHeaderTable();

                    //}
                    //$('#SocialHistoryLatest-portlet').css('display', 'block');
                    $("#SocialHistoryLatest-portlet").html(data);
                    
                    $("#tbl-SocialHistory").fixedHeaderTable();
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
    }

    var mysocialflag = true;
    function MySocialHistory_move(flag) {

        if ($('#cmbFacilityHome :selected').text() == 'Patient Entered') {
            $('#EditcalInstructions').toggle();
        }
        $('#App-ShareHideSocialHistory').toggle();

        $('#SocailHistorySelf .portlet-header .ui-icon').toggleClass("ui-icon-minusthick").toggleClass("ui-icon-plusthick");
        $('#SocailHistorySelf .portlet-header .ui-icon').parents(".portlet:first").find(".portlet-content").toggle();
        if ($('#cmbFacilityHome option:selected').val() == "0") {
            if (mysocialflag) {
                //  alert("patient Entered");
                MySocialHistoryWidget();
            }
        }
        else {
            if (socialhistoryflag) {
                SocailWidget();
            }
        }

        socialhistoryflag = !socialhistoryflag;
        mysocialflag = !mysocialflag;
        // if (flag) {
        //    //   alert(flag);
        //    $("#EditcalInstructions").css("display", "none");
        //    $("#App-ShareHideSocialHistory").css("display", "none");
        //    $("#SocialHistorySelf-portlet").css("display", "none");
        //    $("#MySocialHistory").attr("onclick", "MySocialHistory_move(false)");


        //} else {
        //    $("#EditcalInstructions").css("display", "block");
        //    $("#App-ShareHideSocialHistory").css("display", "block");
        //    $("#SocialHistorySelf-portlet").css("display", "block");
        //    $("#MySocialHistory").attr("onclick", "MySocialHistory_move(true)");

        //}
    }
    function MySocialHistoryWidget() {

        var facilityId = $('#cmbFacilityHome option:selected').val();
        var visitId = $('#cmbVisitsHome option:selected').val();

        try {
            ShowLoader();
            //var IsPrimaryData =
            var requestData = {

                FacilityId: facilityId,
                VisitId: visitId

            };
            // alert(requestData);
            // alert("PlanOfCare");
            $.ajax({
                type: 'POST',
                url: 'Home/MySocialHistoryWidgetData',
                //clinical-summary-PlanOfCare-filter',
                data: JSON.stringify(requestData),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    // if success
                    // alert("Social - Self");
                    //if (facilityId == "0") {
                    //    $("#SocialHistorySelf-portlet").html(data);
                    //    $("#social-portlet").html(data);
                    //    $("#tbl-socailHistoryself").fixedHeaderTable();
                    //}
                    //else {

                    //    $("#social-portlet").html(data);
                    //    $("#SocialHistorySelf-portlet").html(data);
                    //    $("#tbl-SocialHistory").fixedHeaderTable();

                    //}
                    // $('#SocialHistoryLatest-portlet').css('display', 'block');
                     $("#SocialHistoryLatest-portlet").html(data);
                    // toggle_combobox_PlanOfCare();

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
    }
    var FamilyFlag = true;
    function family_move(flag) {//done
        chkAccess();
        if ($('#cmbFacilityHome :selected').text() == 'Patient Entered') {
            $('#createaddfamily').toggle();
        }
        $('#App-ShareHidefamilyhist').toggle();
        
        $('#familyhistwidg .portlet-header .ui-icon').toggleClass("ui-icon-minusthick").toggleClass("ui-icon-plusthick");
        $('#familyhistwidg .portlet-header .ui-icon').parents(".portlet:first").find(".portlet-content").toggle();
        if (FamilyFlag) {
            $('#familyhistwidg .portlet-header').css('margin-bottom', '0em');
            FamilyWidget();
        }
        else {
            $('#familyhistwidg .portlet-header').css('margin-bottom', '0.3em');
        }
        FamilyFlag = !FamilyFlag;
        //window.location.href = 'clinical-summary?id=4';
        //$("#tabs").tabs({
        //    active: 4
        //});
        //if (flag) {
        //    //   alert(flag);
        //    $("#createaddfamily").css("display", "none");
        //    $("#App-ShareHidefamilyhist").css("display", "none");
        //    $("#family-portlet").css("display", "none");
        //    $("#Familyhis").attr("onclick", "family_move(false)");
    

        //} else {
        //    $("#createaddfamily").css("display", "block");
        //    $("#App-ShareHidefamilyhist").css("display", "block");
        //    $("#family-portlet").css("display", "block");
        //    $("#Familyhis").attr("onclick", "family_move(true)");
 
        //}

    }
    function FamilyWidget()
    {

        var facilityId = $('#cmbFacilityHome option:selected').val();
        var visitId = $('#cmbVisitsHome option:selected').val();

        //Start Allergy Ajax
        try {
            ShowLoader();
            //var IsPrimaryData =
            var requestData = {

                FacilityId: facilityId,
                VisitId: visitId

            };


            $.ajax({
                type: 'POST',
                url: 'Home/FamilyWidgetData',
                data: JSON.stringify(requestData),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    // if success

                    $("#family-portlet").html(data);

                    $("#tbl-FamilyHistory").fixedHeaderTable();
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
    }
    var PastMedicationFlag = true;
    function past_move(flag) { //done
        chkAccess();
        if ($('#cmbFacilityHome :selected').text() == 'Patient Entered') {
            $('#createaddpast').toggle();
        }
       // $('#App-PastMedicalHistory').toggle();
        $('#App-ShareHidePastMedicalHistory').toggle();
        $('#Pmedicalhistwidg .portlet-header .ui-icon').toggleClass("ui-icon-minusthick").toggleClass("ui-icon-plusthick");
        $('#Pmedicalhistwidg .portlet-header .ui-icon').parents(".portlet:first").find(".portlet-content").toggle();
        if (PastMedicationFlag) {
            $('#Pmedicalhistwidg .portlet-header').css('margin-bottom', '0em');
            PastWidget();
        }
        else {
            $('#Pmedicalhistwidg .portlet-header').css('margin-bottom', '0.3em');
        }

        PastMedicationFlag = !PastMedicationFlag;
        //window.location.href = 'clinical-summary?id=4';
        //$("#tabs").tabs({
        //    active: 4
        //});
        //if (flag) {
        //    //   alert(flag);
        //    $("#createaddpast").css("display", "none");
        //    $("#App-ShareHidePastMedicalHistory").css("display", "none");
        //    $("#pasthistory-portlet").css("display", "none");
        //    $("#pastmedication").attr("onclick", "past_move(false)");


        //} else {
        //    $("#createaddpast").css("display", "block");
        //    $("#App-ShareHidePastMedicalHistory").css("display", "block");
        //    $("#pasthistory-portlet").css("display", "block");
        //    $("#pastmedication").attr("onclick", "past_move(true)");

        //}
    }

    function PastWidget()
    {
        var facilityId = $('#cmbFacilityHome option:selected').val();
        var visitId = $('#cmbVisitsHome option:selected').val();

        try {
            ShowLoader();
            //var IsPrimaryData =
            var requestData = {

                FacilityId: facilityId,
                VisitId: visitId

            };
            // alert(requestData);
            // alert("PlanOfCare");
            $.ajax({
                type: 'POST',
                url: 'Home/PastMedicationWidgetData',
                //clinical-summary-PlanOfCare-filter',
                data: JSON.stringify(requestData),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    // if success

                    $("#pasthistory-portlet").html(data);
                    $("#tbl-PastMedications").fixedHeaderTable();
                    // toggle_combobox_PlanOfCare();

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

    }
    var Immunizationflag = true;
    function immunization_move(flag) {
        chkAccess();
        if ($('#cmbFacilityHome :selected').text() == 'Patient Entered') {
            $('#createaddimmunizations').toggle();
        }
        $('#App-ShareHideImmunization').toggle();

        $('#immunizwidg .portlet-header .ui-icon').toggleClass("ui-icon-minusthick").toggleClass("ui-icon-plusthick");
        $('#immunizwidg .portlet-header .ui-icon').parents(".portlet:first").find(".portlet-content").toggle();
        if (Immunizationflag) {
            $('#immunizwidg .portlet-header').css('margin-bottom', '0em');
            ImmunizationWidget();
        }
        else {
            $('#immunizwidg .portlet-header').css('margin-bottom', '0.3em');
        }

        Immunizationflag = !Immunizationflag;
        //window.location.href = 'clinical-summary?id=6';
        //$("#tabs").tabs({
        //    active: 6
        //});

        //if (flag) {
        //    //   alert(flag);
        //    $("#createaddimmunizations").css("display", "none");
        //    $("#App-ShareHideImmunization").css("display", "none");
        //    $("#immunization-portlet").css("display", "none");
        //    $("#immunization").attr("onclick", "immunization_move(false)");


        //} else {
        //    $("#createaddimmunizations").css("display", "block");
        //    $("#App-ShareHideImmunization").css("display", "block");
        //    $("#immunization-portlet").css("display", "block");
        //    $("#immunization").attr("onclick", "immunization_move(true)");

        //}

    }

    function ImmunizationWidget()
    {

        var facilityId = $('#cmbFacilityHome option:selected').val();
        var visitId = $('#cmbVisitsHome option:selected').val();

        try {
            ShowLoader();
            //var IsPrimaryData =
            var requestData = {

                FacilityId: facilityId,
                VisitId: visitId

            };
            // alert(requestData);
            // alert("PlanOfCare");
            $.ajax({
                type: 'POST',
                url: 'Home/ImmunizationWidgetData',
                //clinical-summary-PlanOfCare-filter',
                data: JSON.stringify(requestData),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    // if success
                    //if (xhr.status == 302) {
                    //    window.location = xhr.getResponseHeader('Location')
                    //}
                    //if (data.html == '/Login')
                    //{
                    //    window.location.href =  data;
                    //}
                    $("#immunization-portlet").html(data);
                    $("#tbl-Immunizations").fixedHeaderTable();
                    // toggle_combobox_PlanOfCare();

                },
                error: function (xhr, ajaxOptions, thrownError) {
                    //if error
                    //if (xhr.status == 302) {
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

    }
    var DocumentFlag = true;
    function documents_move(flag) {
        chkAccess();
        if ($('#cmbFacilityHome :selected').text() == 'Patient Entered') {
           // $('#createaddimmunizations').toggle();
        }
        $('#App-ShareHideDocuments').toggle();

        $('#Documentswidg .portlet-header .ui-icon').toggleClass("ui-icon-minusthick").toggleClass("ui-icon-plusthick");
        $('#Documentswidg .portlet-header .ui-icon').parents(".portlet:first").find(".portlet-content").toggle();
        if (DocumentFlag) {
            $('#Documentswidg .portlet-header').css('margin-bottom', '0em');
            DocumentWidget();
        }
        else {
            $('#Documentswidg .portlet-header').css('margin-bottom', '0.3em');
        }

        DocumentFlag = !DocumentFlag;
        //window.location.href = 'clinical-summary?id=9';
        //$("#tabs").tabs({
        //    active: 9
        //});

      //  if (flag) {
        //    //   alert(flag);
        //    //  $("#createaddfamily").css("display", "none");
        //    $("#App-ShareHideDocuments").css("display", "none");
        //    $("#Documents-portlet").css("display", "none");
        //    $("#documents").attr("onclick", "documents_move(false)");


        //} else {
        //    // $("#createaddfamily").css("display", "block");
        //    $("#App-ShareHideDocuments").css("display", "block");
        //    $("#Documents-portlet").css("display", "block");
        //    $("#documents").attr("onclick", "documents_move(true)");

        //}
    }

    function DocumentWidget()
    {
        var facilityId = $('#cmbFacilityHome option:selected').val();
        var visitId = $('#cmbVisitsHome option:selected').val();

        try {
            ShowLoader();
            //var IsPrimaryData =
            var requestData = {

                FacilityId: facilityId,
                VisitId: visitId

            };
            // alert(requestData);
            // alert("PlanOfCare");
            $.ajax({
                type: 'POST',
                url: 'Home/DocumentWidgetData',
                //clinical-summary-PlanOfCare-filter',
                data: JSON.stringify(requestData),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    // if success

                    $("#Documents-portlet").html(data);
                    $("#tbl-Documents").fixedHeaderTable();
                    // toggle_combobox_PlanOfCare();

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
    }
    var Insurnaceflag = true;
    function insurance_move(flag) {
        chkAccess();
        if ($('#cmbFacilityHome :selected').text() == 'Patient Entered') {
            // $('#createaddimmunizations').toggle();
        }
        $('#App-ShareHideInsurance').toggle();

        $('#Insurancewidg .portlet-header .ui-icon').toggleClass("ui-icon-minusthick").toggleClass("ui-icon-plusthick");
        $('#Insurancewidg .portlet-header .ui-icon').parents(".portlet:first").find(".portlet-content").toggle();
        if (Insurnaceflag) {
            $('#Insurancewidg .portlet-header').css('margin-bottom', '0em');
            IssuranceWidget();
        }
        else {
            $('#Insurancewidg .portlet-header').css('margin-bottom', '0.3em');
        }
        Insurnaceflag = !Insurnaceflag;
        //window.location.href = 'clinical-summary?id=10';
        //$("#tabs").tabs({
        //    active: 10
        //});
        //if (flag) {
        //    //   alert(flag);
        //    //    $("#createaddfamily").css("display", "none");
        //    $("#App-ShareHideInsurance").css("display", "none");
        //    $("#Insurance-portlet").css("display", "none");
        //    $("#Insurance").attr("onclick", "insurance_move(false)");


        //} else {
        //    //    $("#createaddfamily").css("display", "block");
        //    $("#App-ShareHideInsurance").css("display", "block");
        //    $("#Insurance-portlet").css("display", "block");
        //    $("#Insurance").attr("onclick", "insurance_move(true)");

        //}
    }
    function IssuranceWidget()
    {
        var facilityId = $('#cmbFacilityHome option:selected').val();
        var visitId = $('#cmbVisitsHome option:selected').val();

        try {
            ShowLoader();
            //var IsPrimaryData =
            var requestData = {

                FacilityId: facilityId,
                VisitId: visitId

            };
            // alert(requestData);
            // alert("PlanOfCare");
            $.ajax({
                type: 'POST',
                url: 'Home/InsuranceWidgetData',
                //clinical-summary-PlanOfCare-filter',
                data: JSON.stringify(requestData),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    // if success

                    $("#Insurance-portlet").html(data);
                    $("#tbl-Insurance").fixedHeaderTable();
                    // toggle_combobox_PlanOfCare();

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
    
    }
    var Procedure = true;
    function Proceduers_move(flag) {
        chkAccess();
        if ($('#cmbFacilityHome :selected').text() == 'Patient Entered') {
             $('#createaddProcedures').toggle();
        }
        $('#App-ShareHideProcedures').toggle();

        $('#Procedureswidg .portlet-header .ui-icon').toggleClass("ui-icon-minusthick").toggleClass("ui-icon-plusthick");
        $('#Procedureswidg .portlet-header .ui-icon').parents(".portlet:first").find(".portlet-content").toggle();
        if (Procedure) {
            $('#Procedureswidg .portlet-header').css('margin-bottom', '0em');
            ProcedureWidget();
        }
        else {
            $('#Procedureswidg .portlet-header').css('margin-bottom', '0.3em');
        }
        Procedure = !Procedure;
        // alert('Proc');
        //window.location.href = 'clinical-summary?id=11';
        //$("#tabs").tabs({
        //    active: 11
        //});

        //if (flag) {
        //    //   alert(flag);
        //    $("#createaddProcedures").css("display", "none");
        //    $("#App-ShareHideProcedures").css("display", "none");
        //    $("#Procedure-portlet").css("display", "none");
        //    $("#Procedures").attr("onclick", "Proceduers_move(false)");


        //} else {
        //    $("#createaddProcedures").css("display", "block");
        //    $("#App-ShareHideProcedures").css("display", "block");
        //    $("#Procedure-portlet").css("display", "block");
        //    $("#Procedures").attr("onclick", "Proceduers_move(true)");

        //}

    }
    function ProcedureWidget()
    {

        var facilityId = $('#cmbFacilityHome option:selected').val();
        var visitId = $('#cmbVisitsHome option:selected').val();

        try {
            ShowLoader();
            //var IsPrimaryData =
            var requestData = {

                FacilityId: facilityId,
                VisitId: visitId

            };
            // alert(requestData);
            // alert("PlanOfCare");
            $.ajax({
                type: 'POST',
                url: 'Home/ProcedureWidgetData',
                //clinical-summary-PlanOfCare-filter',
                data: JSON.stringify(requestData),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    // if success

                    $("#Procedure-portlet").html(data);
                    $("#tbl-Procedure").fixedHeaderTable();
                    // toggle_combobox_PlanOfCare();

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
    }
    var ClinicalInstructionFlag = true;
    function ClinicalInstructions_move(flag) {
        chkAccess();
        if ($('#cmbFacilityHome :selected').text() == 'Patient Entered') {
            $('#createaddClinicalInstructions').toggle();
        }
        $('#App-ShareHideClinicalInstructions').toggle();

        $('#ClinicalInstructionsWidget .portlet-header .ui-icon').toggleClass("ui-icon-minusthick").toggleClass("ui-icon-plusthick");
        $('#ClinicalInstructionsWidget .portlet-header .ui-icon').parents(".portlet:first").find(".portlet-content").toggle();
        if (ClinicalInstructionFlag) {
            $('#ClinicalInstructionsWidget .portlet-header').css('margin-bottom', '0em');
            ClinicalInstructionWidget();
        }
        else {
            $('#ClinicalInstructionsWidget .portlet-header').css('margin-bottom', '0.3em');
        }
        ClinicalInstructionFlag = !ClinicalInstructionFlag;
        // alert('Clin');
        //window.location.href = 'clinical-summary?id=13';
        //$("#tabs").tabs({
        //    active: 13
        //});

        //if (flag) {
        //    //   alert(flag);
        //    $("#createaddClinicalInstructions").css("display", "none");
        //    $("#App-ShareHideClinicalInstructions").css("display", "none");
        //    $("#ClinicalInstructions-portlet").css("display", "none");
        //    $("#clinicalinstruction").attr("onclick", "ClinicalInstructions_move(false)");


        //} else {
        //    $("#createaddClinicalInstructions").css("display", "block");
        //    $("#App-ShareHideClinicalInstructions").css("display", "block");
        //    $("#ClinicalInstructions-portlet").css("display", "block");
        //    $("#clinicalinstruction").attr("onclick", "ClinicalInstructions_move(true)");

        //}
    }

    function ClinicalInstructionWidget()
    {
        var facilityId = $('#cmbFacilityHome option:selected').val();
        var visitId = $('#cmbVisitsHome option:selected').val();

        try {
            ShowLoader();
            //var IsPrimaryData =
            var requestData = {

                FacilityId: facilityId,
                VisitId: visitId

            };
            // alert(requestData);
            // alert("PlanOfCare");
            $.ajax({
                type: 'POST',
                url: 'Home/ClinicalInstructionWidgetData',
                //clinical-summary-PlanOfCare-filter',
                data: JSON.stringify(requestData),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    // if success

                    $("#ClinicalInstructions-portlet").html(data);
                    $("#tbl-ClinicalInstruction").fixedHeaderTable();
                    // toggle_combobox_PlanOfCare();

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

    }


    
    function POC_move() {
        //  alert('POC');
        //window.location.href = 'clinical-summary?id=12';
        //$("#tabs").tabs({
        //    active: 12
        //});
    
    }
    var PlanOfCareFlag = true;
    function PlanOfCare_Move(flag)
    {
        chkAccess();
        if ($('#cmbFacilityHome :selected').text() == 'Patient Entered') {
            $('#createaddPOC').toggle();
        }
        $('#App-ShareHidePOC').toggle();

        $('#pocwidg .portlet-header .ui-icon').toggleClass("ui-icon-minusthick").toggleClass("ui-icon-plusthick");
        $('#pocwidg .portlet-header .ui-icon').parents(".portlet:first").find(".portlet-content").toggle();
        if (PlanOfCareFlag) {
            $('#pocwidg .portlet-header').css('margin-bottom', '0em');
            PlanOfCareWidget();
        }
        else {
            $('#pocwidg .portlet-header').css('margin-bottom', '0.3em');
        }

        PlanOfCareFlag = !PlanOfCareFlag;
        //window.location.href = 'clinical-summary?id=12';
      
        //$("#tabs").tabs({
        //    active: 12
        ////});
        //if (flag) {
        //    //   alert(flag);
        //    $("#createaddPOC").css("display", "none");
        //    $("#App-ShareHidePOC").css("display", "none");
        //    $("#poc-portlet").css("display", "none");
        //    $("#PlanOfCare").attr("onclick", "PlanOfCare_Move(false)");


        //} else {
        //    $("#createaddPOC").css("display", "block");
        //    $("#App-ShareHidePOC").css("display", "block");
        //    $("#poc-portlet").css("display", "block");
        //    $("#PlanOfCare").attr("onclick", "PlanOfCare_Move(true)");

        //}

    }

    function PlanOfCareWidget() {
        var facilityId = $('#cmbFacilityHome option:selected').val();
        var visitId = $('#cmbVisitsHome option:selected').val();

        try {
            ShowLoader();
            //var IsPrimaryData =
            var requestData = {

                FacilityId: facilityId,
                VisitId: visitId

            };
            // alert(requestData);
            // alert("PlanOfCare");
            $.ajax({
                type: 'POST',
                url: 'Home/PlanOfCareWidgetData',
                //clinical-summary-PlanOfCare-filter',
                data: JSON.stringify(requestData),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    // if success

                    $("#poc-portlet").html(data);
                    $("#tbl-poc").fixedHeaderTable();
                    // toggle_combobox_PlanOfCare();

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
    }
    var ProviderFlag = true;
    function providers_move(flag) {
        chkAccess();
        $('#ProviderWidget .portlet-header .ui-icon').toggleClass("ui-icon-minusthick").toggleClass("ui-icon-plusthick");
        $('#ProviderWidget .portlet-header .ui-icon').parents(".portlet:first").find(".portlet-content").toggle();
        if (ProviderFlag) {
            $('#ProviderWidget .portlet-header').css('margin-bottom', '0em');
            ProviderWidget();
        }
        else {
            $('#ProviderWidget .portlet-header').css('margin-bottom', '0.3em');
        }
        ProviderFlag = !ProviderFlag;
    }
    function ProviderWidget()
    {
        var facilityId = $('#cmbFacilityHome option:selected').val();
        var visitId = $('#cmbVisitsHome option:selected').val();

        try {
            ShowLoader();
            //var IsPrimaryData =
            var requestData = {

                FacilityId: facilityId,
                VisitId: visitId

            };
            // alert(requestData);
            // alert("PlanOfCare");
            $.ajax({
                type: 'POST',
                url: 'Home/ProviderWidgetData',
                //clinical-summary-PlanOfCare-filter',
                data: JSON.stringify(requestData),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    // if success

                    $("#ProviderWidget-portlet").html(data);
                    //$("#tab-providers").fixedHeaderTable();
                    // toggle_combobox_PlanOfCare();

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

    }
    function checkUrl(url) {
        // var pattern = /^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%&]}).*$/;
        var pattern = /^.*(?=.{7,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&+=]).*$/;


        if (pattern.test(url)) {

            return true;
        } else {

            return false;
        }
    }

    $(document).ready(function () {
        
        $("#cmbRFPharmacy").change(function () {

            var spl = $("#cmbRFPharmacy").val().split('|');
            $('#PharmAddress').text(spl[1]);
          //  alert(spl[2]);
            $("#Phone").val(spl[2]);
           // alert($("#Phone").val());
        });

        $("#dialog-security").dialog({
            autoOpen: false,
            open: function (event, ui) { $(".ui-dialog-titlebar-close").hide(); },
            closeOnEscape: false,
            show: {
                effect: "blind",
                duration: 100
            },
            hide: {
                effect: "blind",
                duration: 100
            },
            height: 550,
            width: 820,
            modal: true,
            buttons: {
                "Save": function () {
                    // alert('test');
                    chkAccess();
                    var bValid = true;
                    //allFields.removeClass("ui-state-error");
                    if ($("#chkterms").prop('checked') == false) {
                        alert('Please Agree on terms & conditions');
                        return;
                    }

        //            if (!Reqvalue($.trim($('#txtsecans').val()), 'Security Answer1'))

                    if (!Reqvalue($.trim($('#txtsecans').val()), 'Security Answer 1'))
                 { return; }

                    if (($('#cmbSecurityQuestion option:selected').val()) == ($('#cmbSecurityQuestion2 option:selected').val()))
                    {
                        alert("Please select different question other than first");
                        bValid = false;
                        return;
                    }
                    if (!Reqvalue($.trim($('#securityans').val()), 'Security Answer2'))
                    { return; }

                    if (($('#cmbSecurityQuestion option:selected').val()) == ($('#cmbSecurityQuestion2 option:selected').val())) {
                        alert("Security question 2 should be different than security question 1");
                        bValid = false;
                        return;
                    }
                    if (!Reqvalue($.trim($('#securityans').val()), 'Security Answer 2'))
                    { return; }


                    //var flg = checkUrl($.trim($('#txtnpwd').val()));

                    //if (flg != true) {
                    //   // $("#msg").css("display", "block");

                    //    bValid = false;
                    //}
                    if (!Reqvalue($.trim($('#txtpwd').val()), 'Old Password'))
                    { return; }

                    if (!Reqvalue($.trim($('#txtnpwd').val()), 'New Password'))
                    { return; }

                    if ($('#txtnpwd').val() != $('#txtcpwd').val()) {
                        alert('New password and confirm password are not the same');

                      //  $("#msg").css("display", "block");
                        return false;
                    }
                 
                    if (!PasswordValid($('#txtnpwd').val()))
                    {
                        return;
                    }

                    if (bValid) {
                        // Save routine and call of Action
                        //    alert('test');
                        try {
                            var requestData = {
                                SecurityQuestionId: $.trim($('#cmbSecurityQuestion option:selected').val()),
                                SecurityQuestionId2: $.trim($('#cmbSecurityQuestion2 option:selected').val()),
                                SecurityAnswer: $.trim($('#txtsecans').val()),
                                SecurityAnswer2: $.trim($('#securityans').val()),
                                Password: $.trim($('#txtpwd').val()),
                                NewPassword: $.trim($('#txtnpwd').val())
                            };
                            //debugger;
                            $.ajax({
                                type: 'POST',
                                url: 'home-security-save',
                                data: JSON.stringify(requestData),
                                dataType: 'json',
                                contentType: 'application/json; charset=utf-8',
                                success: function (data) {
                                   
                                    if (data == "Invalid password.") {
                                        alert("Invalid old password");
                                        $("#txtpwd").addClass("ui-state-error txt");
                                        HideLoader();
                                    }
                                    else {
                                        // if success
                                        // alert("Success : " + data);
                                        $("#dialog-security").dialog("close");
                                        $("#txtpwd").removeClass("ui-state-error");

                                        alert("Password and security settings have been changed successfully!");

                                    }
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
                      //  $(this).dialog("close");
                        ShowLoader();
                    }

                },

            },
            close: function () {
                allFields.val("").removeClass("ui-state-error");
                $("#dialog-security").dialog("close");

            }
        });
        $("#txtDescription").autocomplete({

            select: function (event, ui) {

                $("#hdFDescriptionId").val(ui.item.data);
                $("#hdFDescriptionText").val(ui.item.value);
                //$("#PatName").show();
                //return false;
            }
        });

        $("#txtDescription").autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: "index-problemslist-get",
                    type: "POST",
                    dataType: "json",
                    data: { term: request.term },
                    success: function (data) {
                        response($.map(data, function (item) {

                            return { value: item.Value, data: item.ProblemID };
                        }))

                    }
                })
            },
            messages: {
                noResults: "", results: ""
            }
        });
        $("#txtSearch").autocomplete({

            select: function (event, ui) {

                $("#txtSearchID").val(ui.item.data);
                $("#txtSearchText").val(ui.item.value);
                //alert(ui.item.data);
                // alert(ui.item.value);
                //$("#PatName").show();
                //return false;
            }
        });
        $("#txtSearch").autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: "index-problemslist-get",
                    type: "POST",
                    dataType: "json",
                    data: { term: request.term },
                    success: function (data) {
                        response($.map(data, function (item) {

                            return { data: item.ProblemID, value: item.Value };
                        }))

                    }
                })
            },
            messages: {
                noResults: "", results: ""
            }
        });

        $("#txtSearchData").autocomplete({

            select: function (event, ui) {

                $("#txtSearchIDData").val(ui.item.data);
                $("#txtSearchTextData").val(ui.item.value);
                //alert(ui.item.data);
                // alert(ui.item.value);
                //$("#PatName").show();
                //return false;
            }
        });
        $("#txtSearchData").autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: "index-problemslist-get",
                    type: "POST",
                    dataType: "json",
                    data: { term: request.term },
                    success: function (data) {
                        response($.map(data, function (item) {

                            return { data: item.ProblemID, value: item.Value };
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

        if ($('#cmbFacilityHome :selected').text() == 'Patient Entered') {
           // $('#cmbVisitsHome').text("");

        }
        $('#PastHistOcc').find('.fht-cell').css("width", "84px");
        $('#PastHistDiagnose').find('.fht-cell').css("width", "138px");
        $('#PastHistNote').find('.fht-cell').css("width", "204px");


        $("#txtImmunization").autocomplete({

            select: function (event, ui) {

                $("#hdImmunization").val(ui.item.data);
                $("#hdImmunizationText").val(ui.item.value);

                //$("#PatName").show();
                //return false;
            }
        });
        $("#txtImmunization").autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: "index-immunizationlists-get",
                    type: "POST",
                    dataType: "json",
                    data: { term: request.term },
                    success: function (data) {
                        response($.map(data, function (item) {

                            return { data: item.CVX_Code, value: item.Description };
                        }))

                    },

                })
            },
            messages: {
                noResults: "", results: ""
            }
        });

        $("#txtImmunizationData").autocomplete({

            select: function (event, ui) {

                $("#hdImmunizationData").val(ui.item.data);
                $("#hdImmunizationTextData").val(ui.item.value);

                //$("#PatName").show();
                //return false;
            }
        });
        $("#txtImmunizationData").autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: "index-immunizationlists-get",
                    type: "POST",
                    dataType: "json",
                    data: { term: request.term },
                    success: function (data) {
                        response($.map(data, function (item) {

                            return { data: item.CVX_Code, value: item.Description };
                        }))

                    },

                })
            },
            messages: {
                noResults: "", results: ""
            }
        });


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


        $("#txtMedicationNameData").autocomplete({
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

    function chartSummary(id, flag) {
        chkAccess();
        if ($("#cmbFacilityHome").val() == 0) {
            $(".HideEditDelete").css('display', 'block');
        }
        else {
            $(".HideEditDelete").css('display', 'none');
        }
        if (flag == 'visit')
        {
            sec_arg = $('#cmbFacilityHome option:selected').val();
            //if (sec_arg == 0) {
            //    $(".HideEditDelete").css('display', 'block');
               
            //}
            //else if (sec_arg == 4) {
            //    $(".HideEditDelete").css('display', 'none');
            //}

        }
        else if (flag == 'location') {
            sec_arg = id;
            id = $('#cmbVisitsHome option:selected').val();
        }
        var facilityOptions = document.getElementById('cmbFacilityHome').innerHTML;
        var visitOptions = document.getElementById('cmbVisitsHome').innerHTML;
        var facilitySelected = $('#cmbFacilityHome option:selected').val();
        var visitSelected = $('#cmbVisitsHome option:selected').val();
        if (flag == 'location') {

            //filling dropdown start 
            try {
                ShowLoader();
                //var IsPrimaryData =
                var requestData = {

                    FacilityId: sec_arg,
                    ExtensionToggleFunct: " toggle_combobox();",
                    ExtensionFilterFunct: "chartSummary(this.value,'visit');",
                    ExtensionIdName: "cmbVisitsHome",
                    facilityOptions: facilityOptions,
                    visitOptions: visitOptions,
                    facilitySelected: facilitySelected,
                    visitSelected: visitSelected
                    // VisitId: id

                };


                $.ajax({
                    type: 'POST',
                    url: 'clinical-summary-visit-dropdown',
                    data: JSON.stringify(requestData),
                    dataType: 'json',
                    contentType: 'application/json; charset=utf-8',
                    success: function (data) {
                        // if success

                        $("#homeComboBoxPortlet").html(data);
                        id = $('#cmbVisitsHome option:selected').val();
                        // alert(id);

                        visitOptions = document.getElementById('cmbVisitsHome').innerHTML;
                        visitSelected = $('#cmbVisitsHome option:selected').val();
                        // alert(visitOptions);
                        // alert(visitSelected);

                        

                        try {
                            ShowLoader();
                            //var IsPrimaryData =
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
                                url: 'Home/VisitFill',
                                data: JSON.stringify(requestData),
                                dataType: 'json',
                                contentType: 'application/json; charset=utf-8',
                                success: function (data) {
                                    // if success

                                    $.each(data, function (index, item) {
                                        //alert( "Item Index = " + index + "Id = " + item.Id + "Item WidgetHtml" + item.WidgetHtml);
                                        $('#' + item.Id).empty();
                                        $('#' + item.Id).html(item.WidgetHtml);
                                    });

                                    //$("#lab-portlet").html(data); 

                                    $(".portlet-header span").removeClass("ui-icon-minusthick").addClass("ui-icon-plusthick");
                                    $('#Visits-portlet').css('display', 'none');
                                    $('#medications-portlet').css('display', 'none');
                                    $('#family-portlet').css('display', 'none');
                                    $('#allergies-portlet').css('display', 'none');
                                    $('#social-portlet').css('display', 'none');
                                    $('#immunization-portlet').css('display', 'none');
                                    $('#lab-portlet').css('display', 'none');
                                    $('#pasthistory-portlet').css('display', 'none');

                                    $('#Procedure-portlet').css('display', 'none');
                                    $('#vital-portlet').css('display', 'none');
                                    $('#poc-portlet').css('display', 'none');
                                    $('#Appointment-portlet').css('display', 'none');
                                    //  $('#ClinicalInstructionsWidget').css('display', 'none');
                                    $('#Insurance-portlet').css('display', 'none');
                                    $('#Documents-portlet').css('display', 'none');
                                    $('#problem-portlet').css('display', 'none');
                                    $('#ClinicalInstructions-portlet').css('display', 'none');
                                    $('.portlet-content').css('display', 'none');

                                    $("#App-ShareHidefamilyhist").css("display", "none");
                                    $("#App-ShareHideDocuments").css("display", "none");
                                    $("#App-ShareHideInsurance").css("display", "none");
                                    $("#App-ShareHideAllergies").css("display", "none");
                                    $("#App-ShareHideMedications").css("display", "none");
                                    $("#App-ShareHideProblems").css("display", "none");
                                    $("#App-ShareHideProcedures").css("display", "none");
                                    $("#App-ShareHideVisits").css("display", "none");
                                    $("#App-ShareHideImmunization").css("display", "none");
                                    $("#App-ShareHide").css("display", "none");
                                    $("#App-ShareHideVitals").css("display", "none");
                                    $("#Lab-ShareHide").css("display", "none");
                                    $("#App-ShareHideClinicalInstructions").css("display", "none");
                                    $("#App-ShareHidePOC").css("display", "none");
                                    $("#App-ShareHidePastMedicalHistory").css("display", "none");
                                    $("#statementdshare").css("display", "none");
                                    $("#App-ShareHideSocialHistory").css("display", "none");

                                    $("#createaddallergies").css("display", "none");
                                    $("#createaddappointments").css("display", "none");
                                    $("#createPresentMedication").css("display", "none");
                                    $("#createaddproblems").css("display", "none");
                                    $("#addvitalscreation").css("display", "none");
                                    $("#EditcalInstructions").css("display", "none");
                                    $("#createaddstatements").css("display", "none");
                                    $("#createaddfamily").css("display", "none");
                                    $("#createaddpast").css("display", "none");
                                    $("#createaddimmunizations").css("display", "none");
                                    $("#createaddProcedures").css("display", "none");
                                    $("#createaddClinicalInstructions").css("display", "none");
                                    $("#createaddPOC").css("display", "none");

                                    //$("#SocialHist-Div")
                                    //.find(".portlet-header")
                                    //.addClass("ui-widget-header ui-corner-all")
                                    //.prepend("<span class='ui-icon ui-icon-plusthick portlet-toggle'></span>");
                                    $('#App-ShareHideSocialHistory').hide();
                                    //$("#SocialHist-Div .portlet-header .ui-icon").click(function () {
                                    //    $(this).toggleClass("ui-icon-plusthick").toggleClass("ui-icon-minusthick");
                                    //    $(this).parents(".portlet:first").find(".portlet-content").toggle();
                                    //    $('#EditcalInstructions').toggle();
                                    //    $('#App-ShareHideSocialHistory').toggle();

                                    //});
                                    //if (facilitySelected != 0) {
                                    //    $("#socialhistwidg .portlet-header").click(function () {
                                    //        $("#tbl-SocialHistory").fixedHeaderTable();
                                    //    });
                                    //}
                                    $("#tbl-SocialHistory").fixedHeaderTable();
                                    $("#tbl-LabTests").fixedHeaderTable();
                                    $("#tbl-Statement").fixedHeaderTable();
                                    $("#tbl-Visits").fixedHeaderTable();
                                    $("#tbl-FamilyHistory").fixedHeaderTable();
                                    $("#tbl-Medications").fixedHeaderTable();
                                    $("#tbl-Allergies").fixedHeaderTable();
                                    $("#tbl-Documents").fixedHeaderTable();
                                    $("#tbl-Immunizations").fixedHeaderTable();
                                    $("#tbl-Insurance").fixedHeaderTable();
                                    $("#tbl-Medications").fixedHeaderTable();
                                    $("#tbl-poc").fixedHeaderTable();
                                    $("#tbl-Problems").fixedHeaderTable();
                                    $("#tbl-VitalSigns").fixedHeaderTable();
                                    $("#tbl-PastMedications").fixedHeaderTable();
                                    $("#tbl-Procedure").fixedHeaderTable();
                                    //$("#EditcalInstructions").bind("click", function () {
                                    //    //alert("test");

                                    //    SocailHistorySelfEidt();
                                    //    $("#SocialHistory").dialog("open");
                                    //});
                                    allergyflag = true;
                                    Insurnaceflag = true;
                                    PlanOfCareFlag = true;
                                    Visitflag = true;
                                    mysocialflag = true;
                                    socialhistoryflag = true;
                                    mysocialflag = true;
                                    ProblemFlag = true;
                                    medicationflag = true;
                                    Immunizationflag = true;
                                    FamilyFlag = true;
                                    ProviderFlag = true;
                                    AppointmentFlag = true;
                                    VitalFlag = true;
                                    PastMedicationFlag = true;
                                    DocumentFlag = true;
                                    ClinicalInstructionFlag = true;
                                    LabFlag = true;
                                    Procedure = true;
                                    socialhistoryflag = true;
                                    socialHistLatest = true;
                                    HideLoader();
                                    toggle_combobox();
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


                                },
                                async: true,
                                processData: false
                            });
                        } catch (err) {

                            if (err && err !== "") {
                                alert(err.message);
                            }
                        }

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
                        // HideLoader();
                    },
                    complete: function (data) {
                        // if completed
                      //  HideLoader();

                    },
                    async: true,
                    processData: false
                });
            } catch (err) {

                if (err && err !== "") {
                    alert(err.message);
                }
            }
            //filling visit drop down end
        }


        if (flag == 'visit') {
            //start Consolidate Call For Home Widgets Filter....
            visitOptions = document.getElementById('cmbVisitsHome').innerHTML;
            visitSelected = $('#cmbVisitsHome option:selected').val();
            ShowLoader();
                                 

            // alert(visitOptions);
            // alert(visitSelected);
    try {
                ShowLoader();
                //var IsPrimaryData =
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
                    url: 'Home/VisitFill',
                    data: JSON.stringify(requestData),
                    dataType: 'json',
                    contentType: 'application/json; charset=utf-8',
                    success: function (data) {
                        // if success

                        $.each(data, function (index, item) {
                            //alert( "Item Index = " + index + "Id = " + item.Id + "Item WidgetHtml" + item.WidgetHtml);
                            $('#' + item.Id).empty();
                            $('#' + item.Id).html(item.WidgetHtml);
                        });

                        //$("#lab-portlet").html(data); 

                        $(".portlet-header span").removeClass("ui-icon-minusthick").addClass("ui-icon-plusthick");
                        $('#Visits-portlet').css('display', 'none');
                        $('#medications-portlet').css('display', 'none');
                        $('#family-portlet').css('display', 'none');
                        $('#allergies-portlet').css('display', 'none');
                        $('#social-portlet').css('display', 'none');
                        $('#immunization-portlet').css('display', 'none');
                        $('#lab-portlet').css('display', 'none');
                        $('#pasthistory-portlet').css('display', 'none');

                        $('#Procedure-portlet').css('display', 'none');
                        $('#vital-portlet').css('display', 'none');
                        $('#poc-portlet').css('display', 'none');
                        $('#Appointment-portlet').css('display', 'none');
                        //  $('#ClinicalInstructionsWidget').css('display', 'none');
                        $('#Insurance-portlet').css('display', 'none');
                        $('#Documents-portlet').css('display', 'none');
                        $('#problem-portlet').css('display', 'none');
                        $('#ClinicalInstructions-portlet').css('display', 'none');
                        $('.portlet-content').css('display', 'none');

                        $("#App-ShareHidefamilyhist").css("display", "none");
                        $("#App-ShareHideDocuments").css("display", "none");
                        $("#App-ShareHideInsurance").css("display", "none");
                        $("#App-ShareHideAllergies").css("display", "none");
                        $("#App-ShareHideMedications").css("display", "none");
                        $("#App-ShareHideProblems").css("display", "none");
                        $("#App-ShareHideProcedures").css("display", "none");
                        $("#App-ShareHideVisits").css("display", "none");
                        $("#App-ShareHideImmunization").css("display", "none");
                        $("#App-ShareHide").css("display", "none");
                        $("#App-ShareHideVitals").css("display", "none");
                        $("#Lab-ShareHide").css("display", "none");
                        $("#App-ShareHideClinicalInstructions").css("display", "none");
                        $("#App-ShareHidePOC").css("display", "none");
                        $("#App-ShareHidePastMedicalHistory").css("display", "none");
                        $("#statementdshare").css("display", "none");
                        $("#App-ShareHideSocialHistory").css("display", "none");

                        $("#createaddallergies").css("display", "none");
                        $("#createaddappointments").css("display", "none");
                        $("#createPresentMedication").css("display", "none");
                        $("#createaddproblems").css("display", "none");
                        $("#addvitalscreation").css("display", "none");
                        $("#EditcalInstructions").css("display", "none");
                        $("#createaddstatements").css("display", "none");
                        $("#createaddfamily").css("display", "none");
                        $("#createaddpast").css("display", "none");
                        $("#createaddimmunizations").css("display", "none");
                        $("#createaddProcedures").css("display", "none");
                        $("#createaddClinicalInstructions").css("display", "none");
                        $("#createaddPOC").css("display", "none");

                        //                        $("#SocialHist-Div")
                        //.find(".portlet-header")
                        //.addClass("ui-widget-header ui-corner-all")
                        //.prepend("<span class='ui-icon ui-icon-plusthick portlet-toggle'></span>");
                        $('#App-ShareHideSocialHistory').hide();
                        //$("#SocialHist-Div .portlet-header .ui-icon").click(function () {
                        //    $(this).toggleClass("ui-icon-plusthick").toggleClass("ui-icon-minusthick");
                        //    $(this).parents(".portlet:first").find(".portlet-content").toggle();
                        //    $('#EditcalInstructions').toggle();
                        //    $('#App-ShareHideSocialHistory').toggle();

                        //});
                        //if (facilitySelected != 0) {
                        //    $("#socialhistwidg .portlet-header").click(function () {
                        //        $("#tbl-SocialHistory").fixedHeaderTable();
                        //    });
                        //}

                        $("#tbl-SocialHistory").fixedHeaderTable();
                        $("#tbl-LabTests").fixedHeaderTable();
                        $("#tbl-Statement").fixedHeaderTable();
                        $("#tbl-Visits").fixedHeaderTable();
                        $("#tbl-FamilyHistory").fixedHeaderTable();
                        $("#tbl-Medications").fixedHeaderTable();
                        $("#tbl-Allergies").fixedHeaderTable();
                        $("#tbl-Documents").fixedHeaderTable();
                        $("#tbl-Immunizations").fixedHeaderTable();
                        $("#tbl-Insurance").fixedHeaderTable();
                        $("#tbl-Medications").fixedHeaderTable();
                        $("#tbl-poc").fixedHeaderTable();
                        $("#tbl-Problems").fixedHeaderTable();
                        $("#tbl-VitalSigns").fixedHeaderTable();
                        $("#tbl-PastMedications").fixedHeaderTable();
                        $("#tbl-Procedure").fixedHeaderTable();
                        toggle_combobox();
                        //$("#EditcalInstructions").bind("click", function () {
                        //    //alert("test");


                        //    SocailHistorySelfEidt();
                        //    $("#SocialHistory").dialog("open");
                        //});
                        allergyflag = true;
                        Insurnaceflag = true;
                        PlanOfCareFlag = true;
                        Visitflag = true;
                        mysocialflag = true;
                        ProblemFlag = true;
                        medicationflag = true;
                        Immunizationflag = true;
                        FamilyFlag = true;
                        ProviderFlag = true;
                        AppointmentFlag = true;
                        VitalFlag = true;
                        PastMedicationFlag = true;
                        DocumentFlag = true;
                        ClinicalInstructionFlag = true;
                        LabFlag = true;
                        Procedure = true;
                        socialhistoryflag = true;
                        socialhistoryflag = true;
                        mysocialflag = true;
                        socialHistLatest = true;
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


                    },
                    async: true,
                    processData: false
                });
            } catch (err) {

                if (err && err !== "") {
                    alert(err.message);
                }
            }

        }
        //ending Consolidate Call For Home Widgets Filter....

    }


    function searchSNOMED() {
        if ($.trim($('#txtSearch').val()).length <= 0) {
            alert('Please input some value to search.');
            return;
        }

        ShowLoader();
        try {
            var requestData = {
                ProblemID: "0",
                Value: $.trim($('#txtSearch').val())


            };

            $.ajax({
                type: 'POST',
                url: 'index-problemslist-get',
                data: JSON.stringify(requestData),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    // if success
                    // alert("Success : " + data);
                    $("#snomed-container").html(data);

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
    function calculateBMI() {

        // var vt_height = parseInt($('#ht').val());
        // var vt_weight  = parseInt($('#wt').val());
        //// alert(vt_weight);

        //          alert('Please Input some Numeric Value');


    }



    // start of add lab pop up script

    $(function () {

        tips = $(".validateTips");



        $("#addlab-form").dialog({
            autoOpen: false,
            show: {
                effect: "blind",
                duration: 100
            },
            hide: {
                effect: "blind",
                duration: 100
            },


            width: 550,
            modal: true,
            buttons: {
                "Save": function () {
                    chkAccess();
                    var bValid = true;


                    if (bValid) {
                        //ajax 
                        try {
                            var requestData = {
                                TestName: $.trim($('#txtTestName').val()),
                                LabName: $.trim($('#txtLabName').val()),
                                ProviderId_Requested: $.trim($('#cmbProvider').val()),
                                OrderDate: $.trim($('#txtOrderDate').val()),
                                CollectionDate: $.trim($('#txtCollectionDate').val()),
                                ReportDate: $.trim($('#txtReportDate').val()),
                                ReviewDate: $.trim($('#txtReviewDate').val()),
                                Flag: $.trim($('#hdFlag').val())
                            };


                            $.ajax({
                                type: 'POST',
                                url: 'index-labs-save',
                                data: JSON.stringify(requestData),
                                dataType: 'json',
                                contentType: 'application/json; charset=utf-8',
                                success: function (data) {
                                    // if success
                                    // alert("Success : " + data);
                                    $("#lab-portlet").html(data);

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
                        $(this).dialog("close");
                        ShowLoader();

                        //   end ajax

                    }
                },
                Close: function () {
                    $(this).dialog("close");
                }
            },
            close: function () {
                $(this).dialog("close");
            }
        });
        $("#createaddlab")
        .button()
        .click(function () {
            $('#hdFlag').val('labWidg');
            $("#addlab-form").dialog("open");
        });
    });


    //- end of add lab popup script

// start of app details pop up script
    $(function () {

        tips = $(".validateTips");



        $("#appbtn-form1").dialog({
            autoOpen: false,
            show: {
                effect: "blind",
                duration: 100
            },
            hide: {
                effect: "blind",
                duration: 100
            },

            width: 850,
            modal: true,
            buttons: {
                Close: function () {

                    $(this).dialog("close");
                }
            },
            close: function () {

            }
        });


    });
// end of app details pop up script

    // start of add problems pop up script

    $(function () {

        tips = $(".validateTips");
        var ConditionStatus = $("#cmbConditionStatusProb"),

           allFields = $([]).add(ConditionStatus),
           tips = $(".validateTips");
        function updateTips(t) {
            tips
            .text(t)
            .addClass("ui-state-highlight ui-corner-all");
            setTimeout(function () {
                //  tips.removeClass("ui-state-highlight", 1500);
            }, 500);
        }
        function checkLength(o, n) {
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


        $("#addproblems-form").dialog({
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
            modal: true,
            buttons: {
                "Save": function () {
                    chkAccess();
                    var bValid = true;

                    allFields.removeClass("ui-state-error");
                  //  alert("hi");
                    if ($('#txtProblemsDate_Year option:selected').val() == '--' && $('#txtProblemsDate_Day option:selected').val() == '--' && $('#txtProblemsDate_Month option:selected').val() == '--') {
                        alert('Please select Year or Month and Year or Month, Day and Year');
                        return false;
                    }
                    else if ($('#txtProblemsDate_Year option:selected').val() == '--' && $('#txtProblemsDate_Day option:selected').val() != '--') {
                        alert('Please select Year or Month and Year or Month, Day and Year');
                        return false;
                    }
                    else if ($('#txtProblemsDate_Year option:selected').val() != '--' && $('#txtProblemsDate_Day option:selected').val() != '--' && $('#txtProblemsDate_Month option:selected').val() == '--') {
                        alert('Please select Year or Month and Year or Month, Day and Year');
                        return false;
                    }
                    else if ($('#txtProblemsDate_Year option:selected').val() == '--' && $('#txtProblemsDate_Month option:selected').val() != '--') {
                        alert('Please select Year or Month and Year or Month, Day and Year');
                        return false;
                    }
                    if ($('#txtProblemsLastDate_Year option:selected').val() == '--' && $('#txtProblemsLastDate_Day option:selected').val() == '--' && $('#txtProblemsLastDate_Month option:selected').val() == '--') {
                        alert('Please select Last Year or Month and Year or Month, Day and Year');
                        return false;
                    }
                    else if ($('#txtProblemsLastDate_Year option:selected').val() == '--' && $('#txtProblemsLastDate_Day option:selected').val() != '--') {
                        alert('Please select Last Year or Month and Year or Month, Day and Year');
                        return false;
                    }
                    else if ($('#txtProblemsLastDate_Year option:selected').val() != '--' && $('#txtProblemsLastDate_Day option:selected').val() != '--' && $('#txtProblemsLastDate_Month option:selected').val() == '--') {
                        alert('Please select Last Year or Month and Year or Month, Day and Year');
                        return false;
                    }
                    else if ($('#txtProblemsLastDate_Year option:selected').val() == '--' && $('#txtProblemsLastDate_Month option:selected').val() != '--') {
                        alert('Please select Last Year or Month and Year or Month, Day and Year');
                        return false;
                    }
                    bValid = bValid && checkLength(ConditionStatus, "Please Select ConditionStatus");

                    if((($('#cmbConditionStatusProb option:selected').val())==-1))
                    {
                        alert("Please Select Condition Status option");
                        return false;
                    }

                    //else if ((($('#cmbConditionStatusProb option:selected').val()) == -1))
                    //{
                    //    alert("Please Select Condition Status option");
                    //    return false;
                    //}
                    //else
                    //if ($('#txtProblemsDate_Year option:selected').val() == '--' || $('#txtProblemsDate_Day option:selected').val() == '--' || $('#txtProblemsDate_Month option:selected').val() == '--')
                    //{
                    //    alert('Please select Year or Month and Year or Month, Day and Year');
                    //    return false;
                    //}
                    //else if ($('#txtProblemsLastDate_Day option:selected').val() == '--' && $('#txtProblemsDate_Month option:selected').val() != '--')
                    //{
                    //    alert('Please select Year or Month and Year or Month, Day and Year');
                    //    return false;
                    //}
                   // else
                    //if ($('#txtProblemsLastDate_Year option:selected').val() == '--' || $('#txtProblemsLastDate_Month option:selected').val() == '--' || $('#txtProblemsLastDate_Day option:selected').val() == '--') {
                    //    alert('Please select last change month date Year');
                    //    return false;
                    //}
                    if (bValid) {
                        //ajax 
                        try {
                            var codVal = $.trim($('#txtSearchID').val());
                            if ($.trim($('#txtSearchText').val()) == "")
                            { codVal = 0; }
                            var requestData = {
                                PatientProblemCntr: $.trim($('#txtproblemID').val()),
                                EffectiveDate: $.trim($('#txtProblemsDate').val()),
                                LastChangeDate: $.trim($('#txtPlastDate').val()),
                                Description: $.trim($('#txtSearch').val()),
                                Note: $.trim($('#txtProblemsDescription').val()),
                                CodeValue: codVal,
                                Condition: $.trim($('#txtSearchText').val()),
                                ConditionStatusId: $.trim($('#cmbConditionStatusProb').val()),
                                Flag: $.trim($('#hdFlag').val())
                            };


                            $.ajax({
                                type: 'POST',
                                url: 'index-problems-save',
                                data: JSON.stringify(requestData),
                                dataType: 'json',
                                contentType: 'application/json; charset=utf-8',
                                success: function (data) {
                                    // if success
                                    // alert("Success : " + data);
                                    $("#problem-portlet").empty();
                                    $("#problem-portlet").html(data[0]);
                                    $("#tbl-Problems").fixedHeaderTable();


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


                        //                        end ajax


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
                $(".validateTips ").empty();
                $("span").removeClass("ui-state-highlight");
                $(this).dialog("close");
            }
        });
        $("#createaddproblems")
        .button()
        .click(function () {
            $('#txtSearchID').val('0');
            $('#txtSearchText').val('');
            $('#hdFlag').val('problemWidg');
            $("#addproblems-form").dialog("open");
        });
    });

    //- end of add problems popup script


    // start of add Allergies pop up script

    $(function () {

   
        tips = $(".validateTips");
        var AllergyType = $("#txtAllergenType");
        var Allergy = $("#txtAllergen");
        var Reaction = $("#txtReaction");
        var Status = $("#cmbConditionStatus");

        allFields = $([]).add(Status).add(AllergyType).add(Allergy).add(Reaction),
        tips = $(".validateTips");

        function updateTips(t) {
            tips
            .text(t)
            .addClass("ui-state-highlight ui-corner-all");
            setTimeout(function () {
                //  tips.removeClass("ui-state-highlight", 1500);
            }, 500);
        }
        function checkLength(o, n) {

            if (o.val() == -1) {
                o.addClass("ui-state-error");
                updateTips(n);
                return false;
            } else {
                return true;
            }
        }

        function checkCharLength(o, n, min, max) {
            if (o.val().length > max || o.val().length < min) {
                o.addClass("ui-state-error");
                updateTips("Length of " + n + " must be between " +
                min + " and " + max + ".");
                return false;
            } else {
                return true;
            }
        }


        $("#addallergies-form").dialog({
            autoOpen: false,
            show: {
                effect: "blind",
                duration: 100
            },
            hide: {
                effect: "blind",
                duration: 100
            },


            width: 550,
            modal: true,
            buttons: {
                "Save": function () {
                    chkAccess();
                    var bValid = true;
                    allFields.removeClass("ui-state-error");
                    
                    
                    if ($('#txtAllergiesDate_Year option:selected').val() == '--' && $('#txtAllergiesDate_Day option:selected').val() == '--' && $('#txtAllergiesDate_Month option:selected').val() == '--') {
                        alert('Please select  Year or Month and Year or Month, Day and Year');
                        return false;
                    }
                    else if ($('#txtAllergiesDate_Year option:selected').val() == '--' && $('#txtAllergiesDate_Day option:selected').val() != '--') {
                        alert('Please select  Year or Month and Year or Month, Day and Year');
                        return false;
                    }
                    else if ($('#txtAllergiesDate_Year option:selected').val() != '--' && $('#txtAllergiesDate_Day option:selected').val() != '--' && $('#txtAllergiesDate_Month option:selected').val() == '--') {
                        alert('Please select  Year or Month and Year or Month, Day and Year');
                        return false;
                    }
                    else if ($('#txtAllergiesDate_Year option:selected').val() == '--' && $('#txtAllergiesDate_Month option:selected').val() != '--') {
                        alert('Please select  Year or Month and Year or Month, Day and Year');
                        return false;
                    }
                    bValid = bValid && checkCharLength(AllergyType, "Allergy Type", 3, 20);
                    bValid = bValid && checkCharLength(Allergy, "Allergen", 2, 50);
                    bValid = bValid && checkCharLength(Reaction, "Reaction", 3, 50);
                    bValid = bValid && checkLength(Status, "Please Select Status");

                    if (bValid) {
                        //ajax 
                        try {

                           //if ($('#txtAllergiesDate_Year option:selected').val() == '--' || $('#txtAllergiesDate_Month option:selected').val() == '--' || $('#txtAllergiesDate_Day option:selected').val() == '--') {
                           //     alert('Please select Year or Month and Year or Month, Day and Year');
                           //     return false;
                           // }
                            var requestData = {
                                PatientAllergyCntr: $.trim($('#txtallergyID ').val()),
                                EffectiveDate: $.trim($('#txtAllergiesDate').val()),
                                AllergenType: $.trim($('#txtAllergenType').val()),
                                Allergen: $.trim($('#txtAllergen').val()),
                                Reaction: $.trim($('#txtReaction').val()),
                                Note: $.trim($('#txtNote').val()),
                                ConditionStatusId: $.trim($('#cmbConditionStatus').val()),
                                Flag: $.trim($('#hdFlag').val())
                            };


                            $.ajax({
                                type: 'POST',
                                url: 'clinical-summary-allergies-save',
                                data: JSON.stringify(requestData),
                                dataType: 'json',
                                contentType: 'application/json; charset=utf-8',
                                success: function (data) {
                                    $("#allergies-portlet").empty();
                                    $("#allergies-portlet").html(data[0]);
                                    $("#tbl-Allergies").fixedHeaderTable();

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
                        $(this).dialog("close");
                        ShowLoader();


                        //   end ajax


                    }
                },
                Close: function () {
                    $(this).dialog("close");
                }
            },
            close: function () {
                $(this).dialog("close");
            }
        });
        $("#createaddallergies")
        .button()
        .click(function () {
            $('#txtallergyID ').val('0');
            $('#dtEffectiveDate').attr("disabled", false);
            $('#txtAllergenType').attr("disabled", false);
            $('#txtAllergen').attr("disabled", false);
            $('#txtReaction').attr("disabled", false);
            $('#txtNote').attr("disabled", false);
            $('#cmbConditionStatus').attr("disabled", false);
            $('#hdFlag').val('allergyWidg');
            $("#addallergies-form").dialog("open");
        });


    });



    //- end of add Allergies popup script


    // start of add vitals pop up script

    $(function () {

        tips = $(".validateTips");



        $("#form-addvitals").dialog({
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
            modal: true,
            buttons: {
                "Save": function () {
                    chkAccess();
                    var bValid = true;
                    //if (Reqvalue($.trim($('#txtBloodPressure').val()), 'Blood Pressure'))
                    //{ return; }

                    //if (Reqvalue($.trim($('#txtBloodPressure1').val()), 'Blood Pressure'))
                    //{ return; }

                    //if (Reqvalue($.trim($('#txtWeight').val()), 'Weight'))
                    //{ return; }

                    //if (Reqvalue($.trim($('#txtHeight').val()), 'Height'))
                    //{ return; }

                    //if (Reqvalue($.trim($('#txtHeightinch').val()), 'Height'))
                    //{ return; }

                    //if (Reqvalue($.trim($('#txtTemperature').val()), 'Temperature'))
                    //{ return; }

                    //if (Reqvalue($.trim($('#txtPulse').val()), 'Pulse'))
                    //{ return; }

                    //if (Reqvalue($.trim($('#txtRespiration').val()), 'Respiration'))
                    //{ return; }

                    if ($.isNumeric($('#txtTemperature').val()) || $('#txtTemperature').val() == "") {

                    }
                    else {
                        alert("Please enter a valid number")
                        $('#txtTemperature').focus();
                        return false;
                    }
                    var dtVal = $('#dtObservationDate').val();
                    if (ValidateDate(dtVal)) {

                        if (bValid) {
                            //ajax 
                            try {
                                var ft_inch = $.trim($('#txtHeight').val()) * 12;
                                var inch = $.trim($('#txtHeightinch').val());
                                var height_inch = parseInt(ft_inch) + parseInt(inch);
                                var requestData = {
                                    VitalDate: $.trim($('#dtObservationDate').val()),
                                    BloodPressurePosn: $.trim($('#txtBloodPressure').val()) + "/" + $.trim($('#txtBloodPressure1').val()),
                                    WeightLb: $.trim($('#txtWeight').val()),
                                    Temperature: $.trim($('#txtTemperature').val()),
                                    HeightIn: height_inch,
                                    Systolic: $.trim($('#txtBloodPressure').val()),
                                    Diastolic: $.trim($('#txtBloodPressure1').val()),
                                    Pulse: $.trim($('#txtPulse').val()),
                                    Respiration: $.trim($('#txtRespiration').val()),
                                    Flag: $.trim($('#hdFlag').val())
                                };


                                $.ajax({
                                    type: 'POST',
                                    url: 'index-vitals-save',
                                    data: JSON.stringify(requestData),
                                    dataType: 'json',
                                    contentType: 'application/json; charset=utf-8',
                                    success: function (data) {
                                        // if success
                                        // alert("Success : " + data);
                                        $("#vital-portlet").empty();
                                        $("#vital-portlet").html(data[0]);
                                        $("#tbl-VitalSigns").fixedHeaderTable();


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
                            $(this).dialog("close");
                            ShowLoader();


                            //                        end ajax


                        }
                    }
                    else {
                        alert("Date should be entered as mm/dd/yyyy")
                        $('#dtObservationDate').focus();
                    }
                },
                Close: function () {
                    $(this).dialog("close");
                }
            },
            close: function () {
                $(this).dialog("close");
            }
        });
        $("#addvitalscreation")
        .button()
        .click(function () {
            $('#result').text('');
            $('#hdFlag').val('vitalWidg');
            $("#form-addvitals").dialog('option', 'title', 'Add Vitals');
            $("#form-addvitals").dialog("open");
        });
    });

    //- end of add vitals popup script


    // start of add Social pop up script

    $(function () {

        tips = $(".validateTips");
        var Description = $("#cmbDescription"),

           allFields = $([]).add(Description),
           tips = $(".validateTips");
        function updateTips(t) {
            tips
            .text(t)
            .addClass("ui-state-highlight ui-corner-all");
            setTimeout(function () {
                //  tips.removeClass("ui-state-highlight", 1500);
            }, 500);
        }
        function checkLength(o, n) {

            if (o.val() == -1) {
                o.addClass("ui-state-error");
                updateTips(n);
                return false;
            } else {
                return true;
            }
        }



        $("#addsocial-form").dialog({
            autoOpen: false,
            show: {
                effect: "blind",
                duration: 100
            },
            hide: {
                effect: "blind",
                duration: 100
            },


            width: 550,
            modal: true,
            buttons: {
                "Save": function () {
                    chkAccess();
                    var bValid = true;

                    allFields.removeClass("ui-state-error");
                    bValid = bValid && checkLength(Description, "Please Select Description");

                    if (bValid) {
                        //alert($.trim($('#txtSocialValue').val().length))
                        //ajax 
                        try {
                            //if(($('#cmbDescription option:selected').val())==-1)
                            //{
                            //    alert("Please Select description");
                            //    return false;
                            //}    else if (($('#txtSocialBegin_Month option:selected').val() == '--') && ($('#txtSocialBegin_Day option:selected').val() != '--')) {
                            //    alert('Please select Year Only or Month and Year Only Or Month, Day and Year');
                            //    return false;
                            //}    else if (($('#txtSocialBegin_Year option:selected').val() == '--') && ($('#txtSocialBegin_Day option:selected').val() != '--')) {
                            //    alert('Please select Year Only or Month and Year Only Or Month, Day and Year');
                            //    return false;
                            //}  
                            //else if (($('#txtSocialBegin_Year option:selected').val() == '--') && ($('#txtSocialBegin_Month option:selected').val() != '--')) {
                            //    alert('Please select Year Only or Month and Year Only Or Month, Day and Year');
                            //    return false;
                            //}
                            //else if (($('#txtSocialEnd_Month option:selected').val() == '--') && ($('#txtSocialEnd_Day option:selected').val() != '--')) {
                            //    alert('Please select Year Only or Month and Year Only Or Month, Day and Year');
                            //    return false;
                            //}
                            //else if (($('#txtSocialEnd_Year option:selected').val() == '--') && ($('#txtSocialEnd_Day option:selected').val() != '--')) {
                            //    alert('Please select Year Only or Month and Year Only Or Month, Day and Year');
                            //    return false;
                            //} 
                            //else if (($('#txtSocialEnd_Year option:selected').val() == '--') && ($('#txtSocialEnd_Month option:selected').val() != '--')) {
                            //    alert('Please select Year Only or Month and Year Only Or Month, Day and Year');
                            //    return false;
                            //} 
                            //else if ($('#txtSocialValue').val().length > 20)  {
                            //    alert('Cannot exceed');
                            //    return
                            //}




                            var requestData = {
                                Description: $.trim($('#cmbDescription option:selected').text()),
                                CodeValue: $.trim($('#cmbDescription option:selected').val()),
                                Qualifier: $.trim($('#txtSocialValue').val()),
                                BeginDate: $.trim($('#txtSocialBegin').val()),
                                EndDate: $.trim($('#txtSocialEnd').val()),
                                Note: $.trim($('#txtSocialNote').val()),
                                Flag: $.trim($('#hdFlag').val())

                            };



                            $.ajax({
                                type: 'POST',
                                url: 'index-socials-save',
                                data: JSON.stringify(requestData),
                                dataType: 'json',
                                contentType: 'application/json; charset=utf-8',
                                success: function (data) {
                                    // if success
                                    // alert("Success : " + data);
                                    $("#social-portlet").empty();
                                    $("#social-portlet").html(data[0]);
                                    $("#tbl-SocialHistory").fixedHeaderTable();


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

                        catch (err) {

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


                        //                        end ajax


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

            }
        });

        $("#createaddsocial")
        .button()
        .click(function () {
     
            $('#hdFlag').val('socialWidg');

            $("#addfamily-form").dialog("open");
             
         
        });
    });

    //- end of add social popup script

    function checkbox()
    {
        var chk = $('#chkchildren').prop("checked");
        if (chk = false)
        {
            $("#txtson").attr("disabled", "disabled");
            $("#txtdaughter").attr("disabled", "disabled");
        }
        else{
            $("#txtson").attr("disabled", false);
            $("#txtdaughter").attr("disabled", false);
        }

    }

    function SocailHistorySelfEidt() {
        var obj = jQuery.parseJSON($("#patiendsocialself").val());

        var Retired = obj.Retired;
        var Caffeine = obj.CaffeineUser;

        var ExerciseMember = obj.ExerciseMember;
   

        var AlcoholUser = obj.AlcoholUser;


        var AlcoholFamilyHist = obj.AlcoholFamilyHist;


        var SmokingSecondHand = obj.SmokingSecondHand;
 

        if (Retired == "True") {
            $("#txtretried").val("true");
        }
        if (Retired == "False") {
            $("#txtretried").val("false");
        }
        if (Caffeine == "True") {
            $("#txtCaffeine").val("true");
        }
        if (Caffeine == "False") {
            $("#txtCaffeine").val("false");
        }
        if (ExerciseMember == "True") {
            $("#txtculbmember").val("true");
        }
        if (ExerciseMember == "False") {
            $("#txtculbmember").val("false");
        }
        if (AlcoholUser == "True") {
            $("#txtdrinkalcohol").val("true");
        }
        if (AlcoholUser == "False") {
            $("#txtdrinkalcohol").val("false");
        }
        if (AlcoholFamilyHist == "True") {
            $("#txtalocohofamilyhist").val("true");
        }
        if (AlcoholFamilyHist =="False") {
            $("#txtalocohofamilyhist").val("false");
        }

        if (SmokingSecondHand == "True") {
            $("#txtsecondhandsmoke").val("true");
        }
        if (SmokingSecondHand == "False") {
            $("#txtsecondhandsmoke").val("false");
        }
        if (obj.AlcoholLastUse == "1/1/0001")
        {
            obj.AlcoholLastUse = " ";
        }
        if (obj.SmokingQuitDate == "1/1/0001")
        {
            obj.SmokingQuitDate = " ";
        }
  
        $("#txtbirthplace").val(obj.Birthplace);
        $("#txtoccupation").val(obj.Occupation);
        $("#cmbedulevel").val(obj.EducationLevelId);
        if (obj.ChildrenSon == 0) {
            $("#txtson").val('');
        }
        else {
            $("#txtson").val(obj.ChildrenSon);
        }
        if (obj.ChildrenDaughter == 0) {
            $("#txtdaughter").val('');
        }
        else {
            $("#txtdaughter").val(obj.ChildrenDaughter);
        }
        //  $("#txtCaffeine").val(obj.CaffeineUser);
        $("#txtType").val(obj.CaffieneType);
        $("#txtamtperday").val(obj.CaffeineAmount);
        if (obj.ExerciseAmount == 0) {
            $("#txthoursweek").val('');
        }
        else {
            $("#txthoursweek").val(obj.ExerciseAmount);
        }
        // $("#txtculbmember").val(obj.ExerciseMember);
        $("#cmbfrequency").val(obj.ExerciseFrequencyId);
        $("#cmbactivitylevel").val(obj.ActivityLevelId);
        $("#txtdescription1").val(obj.Activity1);
        $("#txtdescription2").val(obj.Activity2);
        $("#txtdescription3").val(obj.Activity3);
        //  $("#txtdrinkalcohol").val(obj.AlcoholUser);
        $("#cmbalocoholfreq").val(obj.AlcoholFrequencyId);
        $("#txtalcoholusedate").val(obj.AlcoholLastUse);
        $("#txtalcoholtype").val(obj.AlcoholType);
        if (obj.AlcoholStartAge == 0) {
            $("#txtalcoholstartage").val('');
        }
        else {
            $("#txtalcoholstartage").val(obj.AlcoholStartAge);
        }
        //   $("#txtalocohofamilyhist").val(obj.AlcoholFamilyHist);

        $("#cmbtobaccosmokestatus").val(obj.SmokingStatusId);
        if (obj.SmokingDailyAmount == 0)
        { $("#txttobamountperday").val('');}
        else {
            $("#txttobamountperday").val(obj.SmokingDailyAmount);
        }

        $("#txttobtype").val(obj.SmokingType);
        if (obj.SmokingYears == 0)
        {
            $("#txttobyearusing").val('');
        }
        else
        {
            $("#txttobyearusing").val(obj.SmokingYears);
        }
        if (obj.SmokingQuitAttempts == 0)
        {
            $("#txttobquitattempt").val('');
        }
        else
     {       $("#txttobquitattempt").val(obj.SmokingQuitAttempts);
    }
        $("#txttobquitdate").val(obj.SmokingQuitDate);
    }
    //function EditcalInstructions() {
    //    $("#EditcalInstructions").bind("click", function () {
    //        //alert("test");
    //        SocailHistorySelfEidt();
    //        $("#SocialHistory").dialog("open");
    //    });
    //    //SocailHistorySelfEidt();
    //    //$("#SocialHistory").dialog("open");
    //}
    $(function () {

        $("#EditcalInstructions").click(function () {
            $('#tabs').tabs("option", "active", 0);
            SocailHistorySelfEidt();
            $("#SocialHistory").dialog("open");

        });
    });
   
    $(function () {

        $("#SocialHistory").dialog({
            autoOpen: false,
            show: {
                effect: "blind",
                duration: 100
            },
            hide: {
                effect: "blind",
                duration: 100
            },


            width: 1000,
            heigth:400,
            modal: true,
            buttons: {

                "Save": function () {
                    chkAccess();
                    var bValid = true;
                    //allFields.removeClass("ui-state-error");
                    //bValid = bValid && checkLength(RelationShip, "Please Select RelationShip option");
                    //bValid = bValid && checkLength1(ConditionStatus, "Please Select ConditonStatus option");

               
                    if (bValid) {
                        //ajax 
                        try {

                       
                            var requestData = {
                            
                                Birthplace: $.trim($('#txtbirthplace').val()),
                                Occupation: $.trim($('#txtoccupation').val()),
                                Retired: $.trim($('#txtretried option:selected').val()),
                                EducationLevelId: $.trim($('#cmbedulevel option:selected').val()),

                                chkchildren: $.trim($('#chkchildren').prop("checked")),
                                ChildrenSon: $.trim($('#txtson').val()),
                                ChildrenDaughter: $.trim($('#txtdaughter').val()),
                                CaffeineUser: $.trim($('#txtCaffeine option:selected').val()),

                                CaffieneType: $.trim($('#txtType').val()),
                                CaffeineAmount: $.trim($('#txtamtperday').val()),
                                ExerciseAmount: $.trim($('#txthoursweek').val()),
                                ExerciseMember: $.trim($('#txtculbmember option:selected').val()),
                                ExerciseFrequencyId: $.trim($('#cmbfrequency option:selected').val()),

                                txthoursweek: $.trim($('#txthoursweek').val()),
                                ActivityLevelId: $.trim($('#cmbactivitylevel option:selected').val()),
                                Activity1: $.trim($('#txtdescription1').val()),
                                Activity2: $.trim($('#txtdescription2').val()),
                                Activity3: $.trim($('#txtdescription3').val()),

                                AlcoholUser: $.trim($('#txtdrinkalcohol option:selected').val()),
                                AlcoholFrequencyId: $.trim($('#cmbalocoholfreq option:selected').val()),
                                AlcoholLastUse: $.trim($('#txtalcoholusedate').val()),
                                AlcoholType: $.trim($('#txtalcoholtype').val()),
       
                                AlcoholStartAge: $.trim($('#txtalcoholstartage').val()),
                                AlcoholFamilyHist: $.trim($('#txtalocohofamilyhist option:selected').val()),

                                SmokingStatusId: $.trim($('#cmbtobaccosmokestatus option:selected').val()),
                                SmokingDailyAmount: $.trim($('#txttobamountperday').val()),
                                SmokingType: $.trim($('#txttobtype').val()),
                                SmokingYears: $.trim($('#txttobyearusing').val()),
                                SmokingQuitAttempts: $.trim($('#txttobquitattempt').val()),
                                SmokingQuitDate: $.trim($('#txttobquitdate').val()),
                                SmokingSecondHand: $.trim($('#txtsecondhandsmoke option:selected').val()),
                                //  CodeValue: $.trim($('#hdFDescriptionId').val()),
                          

                            };


                            $.ajax({
                                type: 'POST',
                                url: 'social-history-self-save',
                                data: JSON.stringify(requestData),
                                dataType: 'json',
                                contentType: 'application/json; charset=utf-8',
                                success: function (data) {
                                    // if success
                                    // alert("Success : " + data);
                                    $("#SocialHistoryLatest-portlet").html(data.html);
                                    $('#tabs').tabs("option", "active", 0);
                                    //    $("#tbl-FamilyHistory").fixedHeaderTable();


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


                        //                        end ajax



                    }
                },
                Close: function () {
                    allFields.val("").removeClass("ui-state-error");
                    $(".validateTips ").empty();
                    $("span").removeClass("ui-state-highlight");
                    $('#tabs').tabs("option", "active", 0);
                    $(this).dialog("close");
                }
            },
            close: function () {
                $('#tabs').tabs("option", "active", 0);
                $(this).dialog("close");
            }
        });
    });


    // start of add family pop up script

    $(function () {

        tips = $(".validateTips");
        var RelationShip = $("#cmbRelationship"),
            ConditionStatus = $("#cmbConditionStatusFam"),


           allFields = $([]).add(RelationShip).add(ConditionStatus),
           tips = $(".validateTips");
        function updateTips(t) {
            tips
            .text(t)
            .addClass("ui-state-highlight ui-corner-all");
            setTimeout(function () {
                //  tips.removeClass("ui-state-highlight", 1500);
            }, 500);
        }
        function checkLength(o, n) {

            if (o.val().split('|') == -1) {
                o.addClass("ui-state-error");
                updateTips(n);
                return false;
            } else {
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



        $("#addfamily-form").dialog({
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
            modal: true,
            buttons: {
                "Save": function () {
                    chkAccess();
                    var bValid = true;
                    allFields.removeClass("ui-state-error");
                    bValid = bValid && checkLength(RelationShip, "Please Select RelationShip option");
                    bValid = bValid && checkLength1(ConditionStatus, "Please Select ConditonStatus option");


                    if (bValid) {
                        //ajax 
                        try {

                            var strArr = $.trim($('#cmbRelationship  option:selected').val()).split('|');
                            //if((strArr[0] ==-1) && (($('#cmbConditionStatusFam option:selected').val())==-1))
                            //{
                            //    alert("Please Select Relationship and Condition option");
                            //    return false;
                            //}
                            //else if((($('#cmbConditionStatusFam option:selected').val())==-1))
                            //{
                            //    alert("Please Select ConditionStatus option");
                            //    return false;
                            //}
                            //else if((strArr[0]==-1))
                            //{
                            //    alert("Please Select Relationship option");
                            //    return false;
                            //}
                            var codeVal;

                            codeVal = $.trim($('#hdFDescriptionId').val());
                            if ($.trim($('#hdFDescriptionText').val()) == "")
                            { codeVal = 0; }
                            var requestData = {
                                //  Description: $.trim($('#hdFDescriptionText').val()),
                                Description: $.trim($('#txtDescription').val()),
                                //  CodeValue: $.trim($('#hdFDescriptionId').val()),
                                CodeValue: codeVal,
                                Note: $.trim($('#txtFamilyNote').val()),
                                RelationshipId: strArr[0],
                                ConditionStatusId: $.trim($('#cmbConditionStatusFam').val()),
                                AgeAtOnset: $.trim($('#txtAgeOnsetSpinner').val()),
                                DiseasedAge: $.trim($('#txtAgeDiseaseSpinner').val()),
                                Flag: $.trim($('#hdFlag').val())

                            };


                            $.ajax({
                                type: 'POST',
                                url: 'index-families-save',
                                data: JSON.stringify(requestData),
                                dataType: 'json',
                                contentType: 'application/json; charset=utf-8',
                                success: function (data) {
                                    // if success
                                    // alert("Success : " + data);
                                    $("#family-portlet").html(data[0]);
                                    $("#tbl-FamilyHistory").fixedHeaderTable();


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


                        //                        end ajax



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

                $(this).dialog("close");
            }
        });
        $("#createaddfamily")
        .button()
        .click(function () {

            $('#hdFDescriptionText').val('');
            $('#hdFDescriptionId').val('0');

            $('#hdFlag').val('familyWidg');
            $("#addfamily-form").dialog("open");
        });
    });

    //- end of add family popup script




    // start of add past pop up script
    $(function () {

        tips = $(".validateTips");



        $("#addpast-form").dialog({
            autoOpen: false,
            show: {
                effect: "blind",
                duration: 100
            },
            hide: {
                effect: "blind",
                duration: 100
            },


            width: 550,
            height: 300,
            modal: true,
            buttons: {
                "Save": function () {
                    chkAccess();
                    var bValid = true;
                    if ($('#txtoc_Year option:selected').val() == '--' && $('#txtoc_Day option:selected').val() == '--' && $('#txtoc_Month option:selected').val() == '--') {
                        alert('Please select Year or Month and Year or Month, Day and Year');
                        return false;
                    }
                    else if ($('#txtoc_Year option:selected').val() == '--' && $('#txtoc_Day option:selected').val() != '--') {
                        alert('Please select Year or Month and Year or Month, Day and Year');
                        return false;
                    }
                    else if ($('#txtoc_Year option:selected').val() != '--' && $('#txtoc_Day option:selected').val() != '--' && $('#txtoc_Month option:selected').val() == '--') {
                        alert('Please select Year or Month and Year or Month, Day and Year');
                        return false;
                    }
                    else if ($('#txtoc_Year option:selected').val() == '--' && $('#txtoc_Month option:selected').val() != '--') {
                        alert('Please select Year or Month and Year or Month, Day and Year');
                        return false;
                    }
                 

                    if (bValid) {
                        //ajax 
                        try {
                            var requestData = {
                                DateOfOccurance: $.trim($('#dtDateOccurance').val()),
                                Description: $.trim($('#txtPastDiagnosis').val()),
                                Note: $.trim($('#txtPastNotes').val()),
                                Flag: $.trim($('#hdFlag').val())

                            };


                            $.ajax({
                                type: 'POST',
                                url: 'index-pasts-save',
                                data: JSON.stringify(requestData),
                                dataType: 'json',
                                contentType: 'application/json; charset=utf-8',
                                success: function (data) {
                                    // if success
                                    // alert("Success : " + data);
                                    $("#pasthistory-portlet").empty();
                                    $("#pasthistory-portlet").html(data[0]);
                                    $("#tbl-PastMedications").fixedHeaderTable();
                                    $('#PastHistOcc').find('.fht-cell').css("width", "84px");
                                    $('#PastHistDiagnose').find('.fht-cell').css("width", "138px");
                                    $('#PastHistNote').find('.fht-cell').css("width", "204px");

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
                        $(this).dialog("close");
                        ShowLoader();


                    }
                },
                Close: function () {
                    $(this).dialog("close");
                }
            },
            close: function () {
                $(this).dialog("close");
            }
        });
        $("#createaddpast")
        .button()
        .click(function () {
            $('#hdFlag').val('pastWidg');
            $("#addpast-form").dialog("open");
            $("#addpast-form").dialog('option', 'title', ' Add Medical History');
        });
    });

    //- end of add past popup script



    // start of add immunizations pop up script

    $(function () {

        tips = $(".validateTips");



        $("#addimmunizations-form").dialog({
            autoOpen: false,
            show: {
                effect: "blind",
                duration: 100
            },
            hide: {
                effect: "blind",
                duration: 100
            },


            width: 850,
            modal: true,
            buttons: {
                "Save": function () {
                    chkAccess();
                    var bValid = true;
                    var codVal;

                    codVal = $.trim($('#hdImmunization').val());
                    if ($.trim($('#hdImmunizationText').val()) == "")
                    { codVal = 0; }
                    var tmVal = $('#cmbImmunizationTime').val()

                    var dtVal = $('#txtImmunizationDate').val();
                    var dtEval = $('#txtImmunizationEXDate').val();




                    if (ValidateDateFormat(dtVal)) {

                        if (ValidateTime(tmVal)) {

                            if (true) { //ValidateDateFormat(dtEVal)

                                if (bValid) {
                                    //ajax 
                                    try {




                                        var requestData = {
                                            ImmunizationDate: $.trim($('#txtImmunizationDate').val()),
                                            Time: $.trim($('#cmbImmunizationTime').val()),
                                            CodeValue: codVal,
                                            Vaccine: $.trim($('#txtImmunization').val()),
                                            //Vaccine: $.trim($('#hdImmunizationText').val()),
                                            Amount: $.trim($('#txtAmount').val()),
                                            Note: $.trim($('#txtImmunizationNote').val()),
                                            Route: $.trim($('#txtImmunizationRoute').val()),
                                            Site: $.trim($('#txtImmunizationSite').val()),
                                            Sequence: $.trim($('#txtImmunizationSeq').val()),
                                            ExpirationDate: $.trim($('#txtImmunizationEXDate').val()),
                                            LotNumber: $.trim($('#txtImmunizationLotNo').val()),
                                            Manufacturer: $.trim($('#txtImmunizationMu').val()),

                                            Flag: 'immunizationWidg'

                                        };


                                        $.ajax({
                                            type: 'POST',
                                            url: 'index-immunizations-save',
                                            data: JSON.stringify(requestData),
                                            dataType: 'json',
                                            contentType: 'application/json; charset=utf-8',
                                            success: function (data) {
                                                // if success
                                                // alert("Success : " + data);
                                                $("#immunization-portlet").empty();
                                                $("#immunization-portlet").html(data[0]);
                                                $("#tbl-Immunizations").fixedHeaderTable();


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
                                    $(this).dialog("close");
                                    ShowLoader();


                                    //                        end ajax


                                }
                            }
                            else {
                                alert("Date should be entered as mm/dd/yyyy")
                                $('#txtImmunizationEXDate').focus();
                            }
                        }
                        else {

                            alert("Time should be entered as hh:mm tt ex.(05:30 pm)")
                            $('#cmbImmunizationTime').focus();
                        }
                    }
                    else {
                        alert("Date should be entered as mm/dd/yyyy")
                        $('#txtImmunizationDate').focus();
                    }
                },
                Close: function () {
                    $(this).dialog("close");
                }
            },
            close: function () {
                $(this).dialog("close");
            }
        });
        $("#createaddimmunizations")
        .button()
        .click(function () {
            $('#txtimmunizationID').val('0');
            $('#hdImmunizationText').val('');
            $('#hdImmunization').val('0');
            $('#hdFlag').val('immunizationWidg');
            $("#addimmunizations-form").dialog("open");
        });
    });

    //- end of add immunizations popup script


    $(function () {

        $("#createaddPOC").click(function () {
     
            $("#addPOC-form").dialog("open");
        });
        $("#createaddPOC-tab").click(function () { $("#addPOC-form").dialog("open"); });
        $("#addPOC-form").dialog({
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
            modal: true,
            buttons: {
                "Save": function () {
                    chkAccess();
                    var bValid = true;

                    // allFields.removeClass("ui-state-error");
                    // bValid = bValid && checkLength(Status, "Please Select Status");
                    var POCTab = $("#POCflag").val();
                    if (POCTab == "POCTab") {

                    }
                    else {
                        {
                            if (bValid) {
                                //ajax 
                                try {
                                    //if (($('#cmbConditionStatus option:selected').val()) == -1) {
                                    //    alert("Please Select status");
                                    //    return false;
                                    //}
                                    //else if ($('#txtAllergiesDate_Year option:selected').val() == '--' && $('#txtAllergiesDate_Day option:selected').val() != '--') {
                                    //    alert('Please select Year or Month and Year or Month, Day and Year');
                                    //    return false;
                                    //}
                                    var requestData = {
                                        PlanCntr: $.trim($("#PlanCntr").val()),
                                        InstructionTypeId: $.trim($("#txttype option:selected").val()),
                                        Instruction: $.trim($('#Instructions').val()),
                                        AppointmentDateTime: $.trim($('#Planneddt').val()),
                                        Goal: $.trim($('#Goals').val()),
                                        Note: $.trim($('#Comments').val()),


                                    };


                                    $.ajax({
                                        type: 'POST',
                                        url: 'clinical-summary-POC-save',
                                        data: JSON.stringify(requestData),
                                        dataType: 'json',
                                        contentType: 'application/json; charset=utf-8',
                                        success: function (data) {
                                            //   if ($('#hdFlag').val() == 'allergyWidg')
                                            $("#poc-portlet").html(data.html);
                                            $("#tbl-poc").fixedHeaderTable();
                                            //$("#tbl-Allergies").fixedHeaderTable();
                                            // $("#PlanOfCare-portlet-tab").html(data.html1);

                                            //  $("#tab-PlanOfCare").fixedHeaderTable();
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
                                $(this).dialog("close");
                                ShowLoader();


                                //   end ajax


                            }
                        }
                    }

                },
                Close: function () {
                    $("#POCflag").val(" ");
                    $("#PlanCntr").val("0");
                    $('#txttype').attr("disabled", false);
                    $('#Comments').attr("disabled", false);
                    $('#Planneddt').attr("disabled", false);
                    $('#Instructions').attr("disabled", false);
                    $('#Goals').attr("disabled", false);
                    $(this).dialog("close");
                }
            },
            close: function () {
                $("#POCflag").val(" ");
                $("#PlanCntr").val("0");
                $('#txttype').attr("disabled", false);
                $('#Comments').attr("disabled", false);
                $('#Planneddt').attr("disabled", false);
                $('#Instructions').attr("disabled", false);
                $('#Goals').attr("disabled", false);
                $(this).dialog("close");
            }
        });
    });

    $(function () {

        $("#createaddProcedures").click(function () {
            $("#addProcedures-form").dialog("open");
        });
        $("#createaddProcedures-tab").click(function () { $("#addProcedures-form").dialog("open"); });
        $("#addProcedures-form").dialog({
            autoOpen: false,
            show: {
                effect: "blind",
                duration: 100
            },
            hide: {
                effect: "blind",
                duration: 100
            },


            width: 550,
            modal: true,

            buttons: {
                "Save": function () {
                    chkAccess();
                    var bValid = true;

                    var date = $("#date").val();

                    var mySplitResult = date.split("/");

                    var Servicedate = mySplitResult[2] + mySplitResult[0] + mySplitResult[1];

                    // var d1 = new Date(Number(parts[2]), Number(parts[0]), Number(parts[1]) - 1);


                    //alert(date);
                    // allFields.removeClass("ui-state-error");
                    // bValid = bValid && checkLength(Status, "Please Select Status");
                    var ProcedureTab = $("#procedureflag").val();

                    if (ProcedureTab == "ProcedureTab") {

                    }
                    else {

                        if (bValid) {
                            //ajax 
                            try {
                                //if (($('#cmbConditionStatus option:selected').val()) == -1) {
                                //    alert("Please Select status");
                                //    return false;
                                //}
                                //else if ($('#txtAllergiesDate_Year option:selected').val() == '--' && $('#txtAllergiesDate_Day option:selected').val() != '--') {
                                //    alert('Please select Year or Month and Year or Month, Day and Year');
                                //    return false;
                                //}
                                var requestData = {
                                    PatProcedureCntr: $.trim($('#txtprocedureid').val()),
                                    ServiceDate: Servicedate,
                                    Description: $.trim($('#txtProcedureDescription').val()),
                                    Note: $.trim($('#txtProcedureNote').val()),
                                    flag: $.trim($('#procedureflag').val())

                                };


                                $.ajax({
                                    type: 'POST',
                                    url: 'clinical-summary-Procedures-save',
                                    data: JSON.stringify(requestData),
                                    dataType: 'json',
                                    contentType: 'application/json; charset=utf-8',
                                    success: function (data) {

                                        //   if ($('#hdFlag').val() == 'allergyWidg')
                                        $("#Procedure-portlet").html(data.html);
                                        $("#tbl-Procedure").fixedHeaderTable();
                                        //$("#tbl-Allergies").fixedHeaderTable();
                                        $("#Procdure-portlet-tab").html(data.html1);

                                        $("#tab-Procedure").fixedHeaderTable();
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
                            $(this).dialog("close");
                            ShowLoader();


                            //   end ajax


                        }
                    }

                },
                Close: function () {
                    //allFields.val("").removeClass("ui-state-error");
                    //$(".validateTips ").empty();
                    //$("span").removeClass("ui-state-highlight");
                    $("#procedureflag").val(" ");
                    $("#txtprocedureid").val("0");
                    $('#date').attr("disabled", false);
                    $('#txtProcedureDescription').attr("disabled", false);
                    $('#txtProcedureNote').attr("disabled", false);
                    $(this).dialog("close");
                }
            },
            close: function () {
                //allFields.val("").removeClass("ui-state-error");
                //$(".validateTips ").empty();
                //$("span").removeClass("ui-state-highlight");
                $('#date').attr("disabled", false);
                $('#txtProcedureDescription').attr("disabled", false);
                $('#txtProcedureNote').attr("disabled", false);
                $("#procedureflag").val(" ");
                $("#txtprocedureid").val("0");
                $(this).dialog("close");
            }
        });
    });
    $(function () {

        $("#createaddClinicalInstructions").click(function () {
            $("#addClinicalInstructions-form").dialog("open");
        });
        $("#createaddClinicalInstructionstab").click(function () { $("#addClinicalInstructions-form").dialog("open"); });
        $("#addClinicalInstructions-form").dialog({
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
            modal: true,
            buttons: {
                "Save": function () {
                    chkAccess();
                    var bValid = true;

                    // allFields.removeClass("ui-state-error");
                    // bValid = bValid && checkLength(Status, "Please Select Status");
                    var clinicalInstructionflag = $("#clinicalInstructionflag").val();
                    if (clinicalInstructionflag == "clinicalInstructionflag") {

                    }
                    else {

                        if (bValid) {
                            //ajax 
                            try {
                                //if (($('#cmbConditionStatus option:selected').val()) == -1) {
                                //    alert("Please Select status");
                                //    return false;
                                //}
                                //else if ($('#txtAllergiesDate_Year option:selected').val() == '--' && $('#txtAllergiesDate_Day option:selected').val() != '--') {
                                //    alert('Please select Year or Month and Year or Month, Day and Year');
                                //    return false;
                                //}
                                var requestData = {
                                    PlanCntr: $.trim($("#clinicalInstruction").val()),
                                    InstructionTypeId: $.trim($("#type option:selected").val()),
                                    Instruction: $.trim($('#txtInstruction').val()),
                                    AppointmentDateTime: $.trim($('#txtPlanneddt').val()),
                                    Goal: $.trim($('#txtGoals').val()),
                                    Note: $.trim($('#txtCommentclinincalinstruction').val()),


                                };


                                $.ajax({
                                    type: 'POST',
                                    url: 'clinical-summary-ClinicalInstruction-save',
                                    data: JSON.stringify(requestData),
                                    dataType: 'json',
                                    contentType: 'application/json; charset=utf-8',
                                    success: function (data) {
                                        //   if ($('#hdFlag').val() == 'allergyWidg')
                                        $("#ClinicalInstructions-portlet").html(data.html);
                                        $("#tbl-ClinicalInstruction").fixedHeaderTable();
                                        //$("#tbl-Allergies").fixedHeaderTable();
                                        //    $("#ClinicalInstructions-portlet-tab").html(data.html1);

                                        //  $("#tab-PlanOfCare").fixedHeaderTable();
                                        clearclinicalinsturctionfield();
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
                            $(this).dialog("close");
                            ShowLoader();


                            //   end ajax


                        }
                    }

                },
                Close: function () {
                    //  allFields.val("").removeClass("ui-state-error");
                    $("#clinicalInstructionflag").val(" ");
                    $('#type').attr("disabled", false);
                    $("#clinicalInstruction").val("0");
                    $('#txtCommentclinincalinstruction').attr("disabled", false);
                    $('#txtPlanneddt').attr("disabled", false);
                    $('#txtInstruction').attr("disabled", false);
                    $('#txtGoals').attr("disabled", false);
                    $("#addClinicalInstructions-form").dialog("close");
                }
            },
            close: function () {
                $("#clinicalInstructionflag").val(" ");
                $('#type').attr("disabled", false);
                $("#clinicalInstruction").val("0");
                $('#txtCommentclinincalinstruction').attr("disabled", false);
                $('#txtPlanneddt').attr("disabled", false);
                $('#txtInstruction').attr("disabled", false);
                $('#txtGoals').attr("disabled", false);
                $("#addClinicalInstructions-form").dialog("close");
            }
        });
    });

    function clearclinicalinsturctionfield()
    {
    
        $('#txtInstruction').val();
        $('#txtPlanneddt').val();
        $('#txtGoals').val();
        $('#txtCommentclinincalinstruction').val();}
    // start of add Stateemnts pop up script

    $(function () {

        tips = $(".validateTips");



        $("#addstatements-form").dialog({
            autoOpen: false,
            show: {
                effect: "blind",
                duration: 100
            },
            hide: {
                effect: "blind",
                duration: 100
            },

            height: 400,
            width: 550,
            modal: true,
            buttons: {
                "Save": function () {
                    chkAccess();
                    var bValid = true;


                    if (bValid) {

                        $(this).dialog("close");
                    }
                },
                Close: function () {
                    $(this).dialog("close");
                }
            },
            close: function () {

            }
        });
        $("#createaddstatements")
        .button()
        .click(function () {
            $("#addstatements-form").dialog("open");
        });
    });

    //- end of add Statements popup script

    function Statements_Move(flag)
    {
        if (flag) {
            //   alert(flag);
            $("#createaddstatements").css("display", "none");
            $("#statementdshare").css("display", "none");
            $("#statement_portlet").css("display", "none");
            $("#statement").attr("onclick", "Statements_Move(false)");


        } else {
            $("#createaddstatements").css("display", "block");
            $("#App-ShareHideInsurance").css("display", "block");
            $("#statement_portlet").css("display", "block");
            $("#statement").attr("onclick", "Statements_Move(true)");

        }


    }




    // start of appointmnet button script

  

    //- end of appointment button script




    // start of request referal button script

   

    //- end of request referal button script



    function pop_up_online() {
        var page = "http://www.healthtestingcenters.com/amr-blood-test-discount.aspx";

        var $dialog = $('<div></div>')
                       .html('<iframe style="border: 0px; " src="' + page + '" width="100%" height="100%"></iframe>')
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
                           width: 900,
                           title: "Online Blood Lab"
                       });
        $dialog.dialog('open');
    }


    function pop_up_library() {
        var page = "http://www.healthtestingcenters.com/amr-blood-test-discount.aspx";

        var $dialog = $('<div></div>')
                       .html('<iframe style="border: 0px; " src="' + page + '" width="100%" height="100%"></iframe>')
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
                           width: 900,
                           title: "Health Information Library"
                       });
        $dialog.dialog('open');
    }
   


    //var jqXHRData;
    //$(document).ready(function () {

    //    initAttachFileUpload();




    //});

    //$(function () {

    //    $('#browse').click(function () { $('#my-simple-upload').click(); });


    //});

    //var requestImageData;
    ////$.trim($('#txtMessageWrite').val())
    //function initAttachFileUpload() {
    //    'use strict';
    //    // my - simple - upload
    //    $('#my-simple-upload').fileupload({
    //        url: 'messages-compose',
    //        dataType: 'json',
    //        // formData: { FacilityId: $('#hdFacilityId').val() },
    //        add: function (e, data) {

    //            jqXHRData = data
    //            $.each(data.files, function (index, file) {
    //                //alert(file.name);
    //                $('#BrowseName').text(file.name);
    //                $('#btncancelupload').show();
    //            });
    //        },
    //        done: function (event, data) {

    //            $('#MessageHeaderGrid').html(data.result.Msghtml);
    //            //if (data.result.isUploaded) {

    //            //}
    //            // else {

    //            // }
    //            //alert(data.result.message);
    //            HideLoader();
    //        },
    //        submit: function (e, data) {
    //            var $this = $(this);
    //            // $.getJSON('/example/url', function (result) {
    //            // alert($('#hdFacilityId').val());
    //            data.formData = { FacilityId: $('#hdFacilityId').val(), ProviderId_To: $('#hdProviderId_To').val(), MessageRequest: $('#hdMessageRequest').val(), MessageTypeId: $('#hdMessageTypeId').val(), MessageUrgencyId: $('#hdMessageUrgencyId').val(), Flag: $('#hdFlag').val() }; // e.g. {id: 123}
    //            $this.fileupload('send', data);
    //            //});
    //            return false;
    //        },
    //        fail: function (event, data) {
    //            if (data.files[0].error) {
    //                alert(data.files[0].error);
    //            }
    //        }
    //    });
    //}

    $(function () {

        $("#draggable").draggable({ axis: "y" });
        $("#draggable2").draggable({ axis: "x" });
   
    });

    $(function () {
        //var inc = 0;
        //$(".column").sortable({
        //    connectWith: ".column"
        //});
        $(".portlet")
			.addClass("ui-widget ui-widget-content ui-helper-clearfix ui-corner-all")
			.find(".portlet-header")
				.addClass("ui-widget-header ui-corner-all")
				//.prepend("<span class='ui-icon ui-icon-minusthick portlet-toggle'></span>");
                .prepend("<span class='ui-icon ui-icon-plusthick portlet-toggle'></span>");

        //$(".portlet").addClass("ui-widget ui-widget-content ui-helper-clearfix ui-corner-all")
        //.find(".portlet-header")
        //.addClass("ui-widget-header ui-corner-all")
        //.prepend("<span class='ui-icon ui-icon-minusthick'></span>")
        //.end()
        //.find(".portlet-content");

        //$(".portlet-header .ui-icon").click(function () {
        //    // $(this).toggleClass("ui-icon-minusthick").toggleClass("ui-icon-plusthick");
        //    $(this).toggleClass("ui-icon-plusthick").toggleClass("ui-icon-minusthick");
        //    $(this).parents(".portlet:first").find(".portlet-content").toggle();

        //    //if (inc % 2 == 0)
        //    //    $('#draggable3-1').css('height', '35px');
        //    //else
        //    //    $('#draggable3-1').css('height', '140px');
        //    //inc++;

        //});
        $(".column").disableSelection();

        $("#SocialHist-Div #socialhistLatestwidg .portlet-header .ui-icon").click(function () {
            $(this).toggleClass("ui-icon-plusthick").toggleClass("ui-icon-minusthick");
            $(this).parents(".portlet:first").find(".portlet-content").toggle();
            if ($('#cmbFacilityHome :selected').text() == 'Patient Entered') {
                $('#EditcalInstructions').toggle();
            }
            $('#App-ShareHideSocialHistory').toggle();
            if ($('#cmbFacilityHome option:selected').val() == "0") {
                if (socialHistLatest) {
                    //   $('#EditcalInstructions').toggle();
                    $('#socialhistLatestwidg .portlet-header').css('margin-bottom', '0em');
                    MySocialHistoryWidget();

                }
                else {
                    $('#socialhistLatestwidg .portlet-header').css('margin-bottom', '0.3em');
                }
                socialHistLatest = !socialHistLatest;
            }
            else {
                if (socialHistLatest) {
                    // alert("else");
                    $('#socialhistLatestwidg .portlet-header').css('margin-bottom', '0em');
                    SocailWidget();
                }
                else {
                    $('#socialhistLatestwidg .portlet-header').css('margin-bottom', '0.3em');
                }
                socialHistLatest = !socialHistLatest;
            }
        });

        //$("#SocialHist-Div #socialhistwidg .portlet-header .ui-icon").click(function () {
        //    $(this).toggleClass("ui-icon-plusthick").toggleClass("ui-icon-minusthick");
        //    $(this).parents(".portlet:first").find(".portlet-content").toggle();
        //    $('#EditcalInstructions').toggle();
        //    $('#App-ShareHideSocialHistory').toggle();
        //    if ($('#cmbFacilityHome option:selected').val() == "0") {
        //        if (mysocialflag) {
        //            MySocialHistoryWidget();
        //        }
        //        mysocialflag = !mysocialflag;

               


        //    }
        //    else {
        //        if (socialhistoryflag) {
        //            SocailWidget();
        //        }
        //        socialhistoryflag = !socialhistoryflag;

           
        //    }
        //});

        //$("#SocialHist-Div #SocailHistorySelf .portlet-header .ui-icon").click(function () {
        //    $(this).toggleClass("ui-icon-plusthick").toggleClass("ui-icon-minusthick");
        //    $(this).parents(".portlet:first").find(".portlet-content").toggle();
        //    $('#EditcalInstructions').toggle();
        //    $('#App-ShareHideSocialHistory').toggle();
        //    if ($('#cmbFacilityHome option:selected').val() == "0") {
        //        if (mysocialflag) {
        //            MySocialHistoryWidget();
        //        }
        //        mysocialflag = !mysocialflag;



        //    }
        //    else {
        //        if (socialhistoryflag) {
        //            SocailWidget();
        //        }
        //        socialhistoryflag = !socialhistoryflag;

        //    }

        //});
        $("#allergywidg .portlet-header .ui-icon").click(function () {
            $(this).toggleClass("ui-icon-plusthick").toggleClass("ui-icon-minusthick");
            $(this).parents(".portlet:first").find(".portlet-content").toggle();
            if ($('#cmbFacilityHome :selected').text() == 'Patient Entered') {
                $('#createaddallergies').toggle();
            }
            $('#App-ShareHideAllergies').toggle();
            if (allergyflag) {
                $('#allergywidg .portlet-header').css('margin-bottom', '0em');
                AllergyWidget();
            }
            else {
                $('#allergywidg .portlet-header').css('margin-bottom', '0.3em');
            }
            allergyflag = !allergyflag;
        });
        $("#widg1 .portlet-header .ui-icon").click(function () {
            $(this).toggleClass("ui-icon-plusthick").toggleClass("ui-icon-minusthick");
            $(this).parents(".portlet:first").find(".portlet-content").toggle();
            if ($('#cmbFacilityHome :selected').text() == 'Patient Entered') {
                $('#createaddappointments').toggle();
            }
            $('#App-ShareHide').toggle();
            if (AppointmentFlag) {
                AppointmentWidget();
            }
            AppointmentFlag = !AppointmentFlag;
        });
        $("#widg3 .portlet-header .ui-icon").click(function () {
            $(this).toggleClass("ui-icon-plusthick").toggleClass("ui-icon-minusthick");
            $(this).parents(".portlet:first").find(".portlet-content").toggle();
            $('#Lab-ShareHide').toggle();
            if (LabFlag) {
                $('#widg3 .portlet-header').css('margin-bottom', '0em');
                LabWidget();
            }
            else {
                $('#widg3 .portlet-header').css('margin-bottom', '0.3em');
            }
            LabFlag = !LabFlag;
        });
        $("#visitswidg .portlet-header .ui-icon").click(function () {
            $(this).toggleClass("ui-icon-plusthick").toggleClass("ui-icon-minusthick");
            $(this).parents(".portlet:first").find(".portlet-content").toggle();
            $('#App-ShareHideVisits').toggle();
            if (Visitflag) {
                $('#visitswidg .portlet-header').css('margin-bottom', '0em');
                VisitWidget();
            }
            else {
                $('#visitswidg .portlet-header').css('margin-bottom', '0.3em');
            }
             Visitflag = !Visitflag;
        });
        $("#widg2 .portlet-header .ui-icon").click(function () {
            $(this).toggleClass("ui-icon-plusthick").toggleClass("ui-icon-minusthick");
            $(this).parents(".portlet:first").find(".portlet-content").toggle();
            if ($('#cmbFacilityHome :selected').text() == 'Patient Entered') {
                $('#createPresentMedication').toggle();
            }
            $('#App-ShareHideMedications').toggle();
            if (medicationflag) {
                $('#widg2 .portlet-header').css('margin-bottom', '0em');
                MedicationWidget();
            }
            else {
                $('#widg2 .portlet-header').css('margin-bottom', '0.3em');
            }
            medicationflag = !medicationflag;
        });
        $("#probswidg .portlet-header .ui-icon").click(function () {
            $(this).toggleClass("ui-icon-plusthick").toggleClass("ui-icon-minusthick");
            $(this).parents(".portlet:first").find(".portlet-content").toggle();
            if ($('#cmbFacilityHome :selected').text() == 'Patient Entered') {
                $('#createaddproblems').toggle();
            }
            $('#App-ShareHideProblems').toggle();
            if (ProblemFlag) {
                $('#probswidg .portlet-header').css('margin-bottom', '0em');
                ProblemWidget();
            }
            else {
                $('#probswidg .portlet-header').css('margin-bottom', '0.3em');
            }
            ProblemFlag = !ProblemFlag;
        });
        $("#vitalwidg .portlet-header .ui-icon").click(function () {
            $(this).toggleClass("ui-icon-plusthick").toggleClass("ui-icon-minusthick");
            $(this).parents(".portlet:first").find(".portlet-content").toggle();
            if ($('#cmbFacilityHome :selected').text() == 'Patient Entered') {
                $('#addvitalscreation').toggle();
            }
            $('#App-ShareHideVitals').toggle();
            if (VitalFlag) {
                $('#vitalwidg .portlet-header').css('margin-bottom', '0em');
                VitalWidget();
            }
            else {
                $('#vitalwidg .portlet-header').css('margin-bottom', '0.3em');
            }
            VitalFlag =! VitalFlag
        });
        $("#familyhistwidg .portlet-header .ui-icon").click(function () {
            $(this).toggleClass("ui-icon-plusthick").toggleClass("ui-icon-minusthick");
            $(this).parents(".portlet:first").find(".portlet-content").toggle();
            if ($('#cmbFacilityHome :selected').text() == 'Patient Entered') {
                $('#createaddfamily').toggle();
            }
            $('#App-ShareHidefamilyhist').toggle();
            if (FamilyFlag) {
                $('#familyhistwidg .portlet-header').css('margin-bottom', '0em');
                FamilyWidget();
            }
            else {
                $('#familyhistwidg .portlet-header').css('margin-bottom', '0.3em');
            }
            FamilyFlag = !FamilyFlag;
        });
        $("#Pmedicalhistwidg .portlet-header .ui-icon").click(function () {
            $(this).toggleClass("ui-icon-plusthick").toggleClass("ui-icon-minusthick");
            $(this).parents(".portlet:first").find(".portlet-content").toggle();
            if ($('#cmbFacilityHome :selected').text() == 'Patient Entered') {
                $('#createaddpast').toggle();
                    
            }
            $('#App-ShareHidePastMedicalHistory').toggle();
            if (PastMedicationFlag) {
                $('#Pmedicalhistwidg .portlet-header').css('margin-bottom', '0em');
                PastWidget();
            }
            else {
                $('#Pmedicalhistwidg .portlet-header').css('margin-bottom', '0.3em');
            }
            PastMedicationFlag = !PastMedicationFlag;
            //$('#App-PastMedicalHistory').toggle();
        });
        $("#immunizwidg .portlet-header .ui-icon").click(function () {
            $(this).toggleClass("ui-icon-plusthick").toggleClass("ui-icon-minusthick");
            $(this).parents(".portlet:first").find(".portlet-content").toggle();
            if ($('#cmbFacilityHome :selected').text() == 'Patient Entered') {
                $('#createaddimmunizations').toggle();
            } $('#App-ShareHideImmunization').toggle();
            if (Immunizationflag) {
                $('#immunizwidg .portlet-header').css('margin-bottom', '0em');
                ImmunizationWidget();
            }
            else {
                $('#immunizwidg .portlet-header').css('margin-bottom', '0.3em');
            }
            Immunizationflag = !Immunizationflag;
        });
        $("#Documentswidg .portlet-header .ui-icon").click(function () {
            $(this).toggleClass("ui-icon-plusthick").toggleClass("ui-icon-minusthick");
            $(this).parents(".portlet:first").find(".portlet-content").toggle();
            $('#App-ShareHideDocuments').toggle();
            if (DocumentFlag) {
                $('#Documentswidg .portlet-header').css('margin-bottom', '0em');
                DocumentWidget();
            }
            else {
                $('#Documentswidg .portlet-header').css('margin-bottom', '0.3em');
            }
            DocumentFlag = !DocumentFlag;
        });
        $("#Insurancewidg .portlet-header .ui-icon").click(function () {
            $(this).toggleClass("ui-icon-plusthick").toggleClass("ui-icon-minusthick");
            $(this).parents(".portlet:first").find(".portlet-content").toggle();
            $('#App-ShareHideInsurance').toggle();
            if (Insurnaceflag) {
                $('#Insurancewidg .portlet-header').css('margin-bottom', '0em');
                IssuranceWidget();
            }
            else {
                $('#Insurancewidg .portlet-header').css('margin-bottom', '0.3em');
            }
            Insurnaceflag = !Insurnaceflag;
        });
        $("#Procedureswidg .portlet-header .ui-icon").click(function () {
            $(this).toggleClass("ui-icon-plusthick").toggleClass("ui-icon-minusthick");
            $(this).parents(".portlet:first").find(".portlet-content").toggle();
            if ($('#cmbFacilityHome :selected').text() == 'Patient Entered') {
                $('#createaddProcedures').toggle();
            } $('#App-ShareHideProcedures').toggle();
            if (Procedure) {
                $('#Procedureswidg .portlet-header').css('margin-bottom', '0em');
                ProcedureWidget();
            }
            else {
                $('#Procedureswidg .portlet-header').css('margin-bottom', '0.3em');
            }
            Procedure = !Procedure;
        });
        $("#pocwidg .portlet-header .ui-icon").click(function () {
            $(this).toggleClass("ui-icon-plusthick").toggleClass("ui-icon-minusthick");
            $(this).parents(".portlet:first").find(".portlet-content").toggle();
            if ($('#cmbFacilityHome :selected').text() == 'Patient Entered') {
                $('#createaddPOC').toggle();
            } $('#App-ShareHidePOC').toggle();
            if (PlanOfCareFlag) {
                $('#pocwidg .portlet-header').css('margin-bottom', '0em');
                PlanOfCareWidget();
            }
            else {
                $('#pocwidg .portlet-header').css('margin-bottom', '0.3em');
            }
            PlanOfCareFlag = !PlanOfCareFlag;
        });
        $("#ClinicalInstructionsWidget .portlet-header .ui-icon").click(function () {
            $(this).toggleClass("ui-icon-plusthick").toggleClass("ui-icon-minusthick");
            $(this).parents(".portlet:first").find(".portlet-content").toggle();
            
            if ($('#cmbFacilityHome :selected').text() == 'Patient Entered') {
                $('#createaddClinicalInstructions').toggle();
            }
            $('#App-ShareHideClinicalInstructions').toggle();
            if (ClinicalInstructionFlag) {
                $('#ClinicalInstructionsWidget .portlet-header').css('margin-bottom', '0em');
                ClinicalInstructionWidget();
            }
            else {
                $('#ClinicalInstructionsWidget .portlet-header').css('margin-bottom', '0.3em');
            }
            ClinicalInstructionFlag = !ClinicalInstructionFlag;
        });
        $("#ProviderWidget .portlet-header .ui-icon").click(function () {
            $(this).toggleClass("ui-icon-plusthick").toggleClass("ui-icon-minusthick");
            $(this).parents(".portlet:first").find(".portlet-content").toggle();
            if (ProviderFlag) {
                $('#ProviderWidget .portlet-header').css('margin-bottom', '0em');
                ProviderWidget();
            }
            else {
                $('#ProviderWidget .portlet-header').css('margin-bottom', '0.3em');
            }
            ProviderFlag = !ProviderFlag;
        });
    });

    $(function () {
        $("input[type=submit], a, button")
        .button()
        .click(function (event) {
            //event.preventDefault();
        });
    });

    $(document).ready(function () {
        $(window).scrollTop(0);
    });

    $(document).load().scrollTop(0);

    $(document).ready(function () {
        $('#cmbDescription').change(function () {
            $('.validateTips').empty();
            $("span").removeClass("ui-state-highlight ui-state-error");
            $("#cmbDescription").removeClass(" ui-state-error");
        });

        $('#cmbPharmacy').change(function () {
            $('.validateTips').empty();
            $("span").removeClass("ui-state-highlight ui-state-error");
            $("#cmbPharmacy").removeClass(" ui-state-error");
        });

        $('#cmbConditionStatusProb').change(function () {
            $('.validateTips').empty();
            $("span").removeClass("ui-state-highlight ui-state-error");
            $("#cmbConditionStatusProb").removeClass(" ui-state-error");
        });

        $('#cmbRelationship').change(function () {
            $('.validateTips').empty();
            $("span").removeClass("ui-state-highlight ui-state-error");
            $("#cmbRelationship").removeClass(" ui-state-error");
        });

        $('#cmbConditionStatusFam').change(function () {
            $('.validateTips').empty();
            $("span").removeClass("ui-state-highlight ui-state-error");
            $("#cmbConditionStatusFam").removeClass(" ui-state-error");
        });
    });

    $(document).ready(function () {
        $('#createPresentMedication').removeClass('ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only');
        $('#createaddlab').removeClass('ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only');
        $('#createaddvisits').removeClass('ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only');
        $('#createaddmedications').removeClass('ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only');
        $('#createaddproblems').removeClass('ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only');
        $('#addvitalscreation').removeClass('ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only');
        $('#createaddsocial').removeClass('ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only');
        $('#createaddfamily').removeClass('ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only');
        $('#createaddpast').removeClass('ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only');
        $('#createaddimmunizations').removeClass('ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only');
        $('#createaddstatements').removeClass('ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only');
        $('#createaddappointments').removeClass('ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only');
        $('#createaddallergies').removeClass('ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only');

        //$('#cmbVisitsHome').attr('disabled', true);

        // applying masking
        //masking for refill medication 
       
        //masking end for refill medication 

        //start of masking of vital modal popup
        $("#txtBloodPressure").mask("9?9999");
        $("#txtBloodPressure1").mask("9?9999");
        $("#txtWeightLb").mask("9?9999");
        $("#txtWeight").mask("9?9999");

        $("#txtHeight").mask("9?9999");
        $("#txtHeightinch").mask("9?9999");
        //$("#txtTemperature").mask("999.99");
        $("#txtPulse").mask("9?9999");
        $("#txtRespiration").mask("9?9999");
        //end of masking of vital modal popup
        //start of masking of vital edit modal popup
        $("#txtBloodPressureData").mask("9?9999");
        $("#txtBloodPressure1Data").mask("9?9999");
        //$("#txtWeightLb").mask("9?9999");
        $("#txtWeightData").mask("9?9999");

        $("#txtHeightData").mask("9?9999");
        $("#txtHeightinchData").mask("9?9999");
        //$("#txtTemperature").mask("999.99");
        $("#txtPulseData").mask("9?9999");
        $("#txtRespirationData").mask("9?9999");
        //end of masking of vital edit modal popup
        //start of masking of medication modal popup
        $("#txtDays").mask("9?99");
        $("#txtQty").mask("9?99");
        $("#txtRefills").mask("9?99");
        //end of masking of medication modal popup

        //start of masking of medication modal popup
        $("#dtEffectiveDate").mask("9999/99");
        //end of masking of medication modal popup

        //start of masking of problem modal popup
        // $("#txtProblemsDate").mask("9999/99");
        $("#txtPlastDate").mask("9999/99");

        //end of masking of problem modal popup
        // applying masking end
        // Call horizontalNav on the navigations wrapping element
        $('.full-width').horizontalNav(
            {
                responsive: false

            });
    });

    function toggle_combobox() {
        if ($('#cmbFacilityHome :selected').text() != 'Patient Entered') {


            $('#cmbVisitsHome').attr('disabled', false);
            $('#createPresentMedication,#createaddproblems,#addvitalscreation,#createaddPOC,#createaddClinicalInstructions,#createaddProcedures,#createaddimmunizations,#createaddallergies,#addvitalscreation,#createaddpast,#createaddfamily,#createaddappointments,#createaddsocial').css('display', 'none');

        }

        else {

            $('#cmbVisitsHome').text("");
            $('#cmbVisitsHome').attr('disabled', true);
            //$('#cmbVisitsHome').val(0);
          //  $('#createPresentMedication,#createaddstatements,#createaddproblems,#addvitalscreation,#createaddimmunizations,#createaddPOC,#createaddClinicalInstructions,#createaddProcedures,#createaddallergies,#addvitalscreation,#createaddpast,#createaddfamily,#createaddappointments,#createaddsocial').css('display', 'block');
        }

    }


    function removing_class() {
        $('#createPresentMedication').removeClass('ui-state-hover ');
        $('#createaddlab').removeClass('ui-state-hover ');
        $('#createaddvisits').removeClass('ui-state-hover ');
        $('#createaddmedications').removeClass('ui-state-hover ');
        $('#createaddproblems').removeClass('ui-state-hover ');
        $('#addvitalscreation').removeClass('ui-state-hover ');
        $('#createaddsocial').removeClass('ui-state-hover ');
        $('#createaddfamily').removeClass('ui-state-hover ');
        $('#createaddpast').removeClass('ui-state-hover ');
        $('#createaddimmunizations').removeClass('ui-state-hover ');
        $('#createaddstatements').removeClass('ui-state-hover ');
        $('#createaddappointments').removeClass('ui-state-hover ');


    }
    function removing_class1() {
        $('#createPresentMedication').removeClass('ui-state-active');

    }

    function ValidateDate(dtValue) {

        var dtRegex = new RegExp(/\b\d{1,2}[\/-]\d{1,2}[\/-]\d{4}\b/);
        return dtRegex.test(dtValue);

    }

    function ValidateDateFormat(dtValue) {
        if (dtValue == "") {
            return true;
        }
        else {
            var dtRegex = new RegExp(/\b\d{1,2}[\/-]\d{1,2}[\/-]\d{4}\b/);
            return dtRegex.test(dtValue);
        }

    }
    function ValidateTime(dtValue) {
        if (dtValue == "") {
            return true;
        }
        else {
            var dtRegex = new RegExp(/^(1[0-2]|0[1-9]):[0-5][0-9]\040(AM|am|PM|pm)$/);
            return dtRegex.test(dtValue);
        }

    }

    var reader;
    // var progress = document.querySelector('.percent');



    function abortRead() {



        reader = new FileReader();
        reader.abort();
    }

    function errorHandler(evt) {
        switch (evt.target.error.code) {
            case evt.target.error.NOT_FOUND_ERR:
                alert('File Not Found!');
                break;
            case evt.target.error.NOT_READABLE_ERR:
                alert('File is not readable');
                break;
            case evt.target.error.ABORT_ERR:
                break; // noop
            default:
                alert('An error occurred reading this file.');
        };
    }

    function updateProgress(evt) {
        var progress = document.querySelector('.percent');
        // evt is an ProgressEvent.
        if (evt.lengthComputable) {
            var percentLoaded = Math.round((evt.loaded / evt.total) * 100);
            // Increase the progress bar length.
            if (percentLoaded < 100) {
                progress.style.width = percentLoaded + '%';
                progress.textContent = percentLoaded + '%';

            }
            if (percentLoaded >= 100) {
                // $('#btncancelupload').hide();
            }

        }
    }

    function handleFileSelect(evt) {
        // Reset progress indicator on new file selection.
        $('#btncancelupload').show();
        var progress = document.querySelector('.percent');

        progress.style.width = '0%';
        progress.textContent = '0%';

        // var reader;
        var files = document.getElementById('files').files;
        var file = files[0];

        reader = new FileReader();
        reader.onerror = errorHandler;
        reader.onprogress = updateProgress;
        reader.onabort = function (e) {
            alert('File read cancelled');
        };
        reader.onloadstart = function (e) {
            document.getElementById('progress_bar').className = 'loading';
        };
        reader.onload = function (e) {
            // Ensure that the progress bar displays 100% at the end.
            progress.style.width = '100%';
            progress.textContent = '100%';
            setTimeout("document.getElementById('progress_bar').className='';", 2000);
        }

        reader.onloadend = function (evt) {


            if (evt.target.readyState == FileReader.DONE) {
                $('#ByteData').val(evt.target.result);
                $('#FileBytes').val(file.size);

            }
        }

        reader.readAsBinaryString(evt.target.files[0]);

    }



    
    function ShowCancel() {
        $('#txtcancelupload').show();
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
    
   
    //function LoadRefilmediaction()
    //{
    //    try {
    //        var requestData = {
    //            FacilityID: $.trim($('#cmbAppointmentFacilityRefill option:selected').val()),
    //            Value: "RefilMedication",
    //        };

    //        $.ajax({
    //            type: 'POST',
    //            url: 'Get-Medication-from-provider',
    //            data: JSON.stringify(requestData),
    //            dataType: 'json',
    //            contentType: 'application/json; charset=utf-8',
    //            success: function (data) {
    //                // if success

    //                $("#refilmed").html(data);

    //            },
    //            error: function (xhr, ajaxOptions, thrownError) {
    //                //if error
    //                var myCookie = getCookie(".Patient");

    //                if (myCookie == null) {
    //                    // do cookie doesn't exist stuff;
    //                    alert('Not logged in. Please log in to continue');
    //                    window.location = '/Login'
    //                    //xhr.getResponseHeader('Location');
    //                }
    //                else {

    //                    alert('Error : ' + xhr.message);
    //                }
    //                HideLoaderForRefil();
                    

    //            },
    //            complete: function (data) {
    //                // if completed
    //                HideLoaderForRefil();

    //            },
    //            async: true,
    //            processData: false
    //        });
    //    }
    //    catch (err) {

    //        if (err && err !== "") {
    //            alert(err.message);
    //            HideLoader();
    //        }
    //    }


    //}

   

    //function LoadFacilityProviderRequestRefferal() {
    //    //ShowLoaderForRequestReferral();
    //    try {
    //        var requestData = {
    //            FacilityID: $.trim($('#cmbAppointmentFacility3 option:selected').val()),
    //            Value: "RequestRefferal",
    //        };

    //        $.ajax({
    //            type: 'POST',
    //            url: 'Get-Provider-Facility-Data',
    //            data: JSON.stringify(requestData),
    //            dataType: 'json',
    //            contentType: 'application/json; charset=utf-8',
    //            success: function (data) {
    //                // if success

    //                $("#DivFacilityProviderRequestRefferal").html(data);

    //            },
    //            error: function (xhr, ajaxOptions, thrownError) {
    //                //if error

    //               // HideLoaderForRequestReferral();
    //                alert('Error : ' + xhr.message);

    //            },
    //            complete: function (data) {
    //                // if completed
    //                //HideLoaderForRequestReferral();

    //            },
    //            async: true,
    //            processData: false
    //        });
    //    }
    //    catch (err) {

    //        if (err && err !== "") {
    //            alert(err.message);
    //            HideLoaderForRequestReferral();
    //        }
    //    }
    //}
    function ShowLoaderForAppointment() {

        document.getElementById('loader2').style.display = 'block';

    }
    function ShowLoaderForRefil() {

        document.getElementById('loader3').style.display = 'block';

    }
    function HideLoaderForAppointment() {


        document.getElementById('loader2').style.display = 'none';
    }
    function HideLoaderForRefil() {


        document.getElementById('loader3').style.display = 'none';
    }

    function ShowLoaderForComposeMessage() {

        document.getElementById('loader1').style.display = 'block';

    }
    function HideLoaderForComposeMessage() {


        document.getElementById('loader1').style.display = 'none';
    }
    function ShowLoaderForRequestReferral() {

        document.getElementById('loader4').style.display = 'block';

    }
    function HideLoaderForRequestReferral() {


        document.getElementById('loader4').style.display = 'none';
    }

    function HidevaluecmbMessageType()
    {
        $("#cmbMessageType option[value='1']").remove();
        $("#cmbMessageType option[value='2']").remove();
        $("#cmbMessageType option[value='5']").remove();
    

    }
    
    function AppointmentDetail(id) {
        try {
            var obj = jQuery.parseJSON($("#hdAppointmentDetails" + id).val());


            function chkstr(value) {
                if (value == '0') {
                    return false;
                }
                if (value == '1')
                    return true;
            }
            $('#cmbAppointmentFacility2').val($.trim(obj.FacilityId));
            $('#cmbAppointmentDoctor2').val($.trim(obj.ProviderId_To));
            $('#chkmor1').prop('checked', chkstr(obj.PreferredPeriod[0]));
            $('#chkAft1').prop('checked', chkstr(obj.PreferredPeriod[1]));
            $('#chkeve1').prop('checked', chkstr(obj.PreferredPeriod[2]));
            $('#cmbMessageUrgency2').val($.trim(obj.MessageUrgencyId));
            $('#chkmon1').prop('checked', chkstr(obj.PreferredWeekDay[0]));
            $('#chktue1').prop('checked', chkstr(obj.PreferredWeekDay[1]));
            $('#chkwed1').prop('checked', chkstr(obj.PreferredWeekDay[2]));
            $('#chkthu1').prop('checked', chkstr(obj.PreferredWeekDay[3]));
            $('#chkfri1').prop('checked', chkstr(obj.PreferredWeekDay[4]));
            $('#chksat1').prop('checked', chkstr(obj.PreferredWeekDay[5]));
            $('#chksun1').prop('checked', chkstr(obj.PreferredWeekDay[6]));
            $('#txtPrefDateFrom1').val($.trim(obj.AppointmentStart));
            $('#txtPrefDateTo1').val($.trim(obj.AppointmentEnd));
            $('#cmbPreftime1 option:selected').text($.trim(obj.PreferredTime));
            $('#txtReason1').val($.trim(obj.VisitReason));
            $('#txtComment1').val($.trim(obj.MessageRequest));


            $('#cmbAppointmentFacility2').attr("disabled", true);
            $('#cmbAppointmentDoctor2').attr("disabled", true);
            $('#chkmor1').attr("disabled", true);
            $('#chkAft1').attr("disabled", true);
            $('#chkeve1').attr("disabled", true);
            $('#cmbMessageUrgency2').attr("disabled", true);
            $('#chkmon1').attr("disabled", true);
            $('#chktue1').attr("disabled", true);
            $('#chkwed1').attr("disabled", true);
            $('#chkthu1').attr("disabled", true);
            $('#chkfri1').attr("disabled", true);
            $('#chksat1').attr("disabled", true);
            $('#chksun1').attr("disabled", true);
            $('#txtPrefDateFrom1').attr("disabled", true);
            $('#txtPrefDateTo1').attr("disabled", true);
            $('#cmbPreftime1').attr("disabled", true);
            $('#txtReason1').attr("disabled", true);
            $('#txtComment1').attr("disabled", true);

        } catch (e) {
            alert(e.message);
        }
        $("#appbtn-form1").dialog("open");
        //alert(id);
    }


		    window.onload = function () {
		        
		        $('#txtRoute').editableSelect({ effects: 'slide' 
		        
		        
		        });
		        $('#txtRouteData').editableSelect({
		            effects: 'slide'
		        
		        
		        });
		        $('#txtRoute').css( "background-color", "#DDE4FF" );
		        $('#txtRouteData').css("background-color", "#DDE4FF");
		    
		        
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
 //////////////index script 
		//     $(function () {
        //    $("#date").datepicker({
        //        inline: true,
        //        dateFormat: 'mm/dd/yy',
        //        changeMonth: true,
        //        changeYear: true,
        //        yearRange: "1900:+0"
                

        //    });
        //    $("#Datadate").datepicker({
        //        inline: true,
        //        dateFormat: 'mm/dd/yy',
        //        changeMonth: true,
        //        changeYear: true,
        //        yearRange: "1900:+0"
                
        //    });

        //    $("#dtObservationDateData").datepicker({
        //        inline: true,
        //        dateFormat: 'mm/dd/yy',
        //        changeMonth: true,
        //        changeYear: true,
        //        yearRange: "1900:+0"
        //    });
        //    $("#show-date-vitData").datepicker({
        //        inline: true,
        //        dateFormat: 'mm/dd/yy',
        //        changeMonth: true,
        //        changeYear: true,
        //        yearRange: "1900:+0"
                
        //    });
        //    $("#txttobquitdate").datepicker({
        //        inline: true,
        //        dateFormat: 'mm/dd/yy',
        //        changeMonth: true,
        //        changeYear: true,
        //        yearRange: "1900:+0"
        //    });
        //    $("#datepickerData").datepicker({
        //        inline: true,
        //        dateFormat: 'mm/dd/yy',
        //        changeMonth: true,
        //        changeYear: true,
        //        yearRange: "1900:+0"
        //    });
        //    $("#dtStartDateData").datepicker({
        //        inline: true,
        //        dateFormat: 'mm/dd/yy',
        //        changeMonth: true,
        //        changeYear: true,
        //        yearRange: "1900:+0"
        //    });
        //    $("#txtImmunizationDateData").datepicker({
        //        inline: true,
        //        dateFormat: 'mm/dd/yy',
        //        changeMonth: true,
        //        changeYear: true,
        //        yearRange: "1900:+0"
                
        //    });
        //    $("#show-date-imm-Data").datepicker({
        //        inline: true,
        //        dateFormat: 'mm/dd/yy',
        //        changeMonth: true,
        //        changeYear: true,
        //        yearRange: "1900:+0"
                
        //    });
        //    $("#txtImmunizationEXDateData").datepicker({
        //        inline: true,
        //        dateFormat: 'mm/dd/yy',
        //        changeMonth: true,
        //        changeYear: true,
        //        yearRange: "1900:+0"
                
        //    });
        //});
        //$(function () {
        //    $("#Planneddt").datepicker({
        //        inline: true,
        //        dateFormat: 'mm/dd/yy',
        //        changeMonth: true,
        //        changeYear: true,
        //        yearRange: "1900:+0"
        //    });
        //});
        //$(function () {
        //    $("#date").datepicker({
        //        inline: true,
        //        dateFormat: 'mm/dd/yy',
        //        changeMonth: true,
        //        changeYear: true,
        //        yearRange: "1900:+0"
        //    });
        //});
        //$(function () {
        //    $("#Planneddt").datepicker({
        //        inline: true,
        //        dateFormat: 'mm/dd/yy',
        //        changeMonth: true,
        //        changeYear: true,
        //        yearRange: "1900:+0"
        //    });
        //});
        //$(function () {
        //    $("#txtPlanneddt").datepicker({
        //        inline: true,
        //        dateFormat: 'mm/dd/yy',
        //        changeMonth: true,
        //        changeYear: true,
        //        yearRange: "1900:+0"
        //    });
        //});
        //$(function () {
        //    $("#PlanneddtData").datepicker({
        //        inline: true,
        //        dateFormat: 'mm/dd/yy',
        //        changeMonth: true,
        //        changeYear: true,
        //        yearRange: "1900:+0"
        //    });
        //    $("#txtPlanneddtData").datepicker({
        //        inline: true,
        //        dateFormat: 'mm/dd/yy',
        //        changeMonth: true,
        //        changeYear: true,
        //        yearRange: "1900:+0"
        //    });
        //});
        //$(function () {
        //    $("#txtalcoholusedate").datepicker({
        //       // showOn: "button",
        //        inline: true,
        //        dateFormat: 'mm/dd/yy',
        //        changeMonth: true,
        //        changeYear: true,
        //        yearRange: "1900:+0"
        //      //  buttonImage:'Content/img/calender.png',
        //      //  buttonImageOnly: true,
        //      //  inline:true
        //    });
        //});
        //$(function () {
        //    $("#txtPrefDateTo").datepicker({
        //        inline: true,
        //        dateFormat: 'mm/dd/yy',
        //        changeMonth: true,
        //        changeYear: true,
        //        yearRange: "1900:+0"
        //    });
        //});
        //$(function () {
        //    $("#tabs").tabs();
        //});
        $(document).ready(function () {
            $("#txtPharmZipCode").mask("99999");
            $('#tabs').tabs();
            document.getElementById('tabs').style.display = 'block';
            var $tabs = $('#tabs').tabs();
            var selected = $tabs.tabs('option', 'active');

            if ($("#cmbFacilityHome").val() == 0) {
                $(".HideEditDelete").css('display', 'block');
            }
            else {
                $(".HideEditDelete").css('display', 'none');
            }

        });

        //$(function () {
        //    $("#Allergies-Records").dialog({
        //        autoOpen: false,
        //        show: {
        //            effect: "blind",
        //            duration: 1000
        //        },
        //        hide: {
        //            effect: "blind",
        //            duration: 1000
        //        },


        //        width: 550,
        //        modal: true,
        //        buttons: {
        //            "Save": function () {
        //                chkAccess();
        //                var bValid = true;
        //                if (ediflagAllergy != 1) { bValid = false; }
        //                allFields.removeClass("ui-state-error");
        //                if ($('#txtAllergiesDate_YearData option:selected').val() == '--' && $('#txtAllergiesDate_DayData option:selected').val() == '--' && $('#txtAllergiesDate_MonthData option:selected').val() == '--') {
        //                    alert('Please select  Year or Month and Year or Month, Day and Year');
        //                    return false;
        //                }
        //                else if ($('#txtAllergiesDate_YearData option:selected').val() == '--' && $('#txtAllergiesDate_DayData option:selected').val() != '--') {
        //                    alert('Please select  Year or Month and Year or Month, Day and Year');
        //                    return false;
        //                }
        //                else if ($('#txtAllergiesDate_YearData option:selected').val() != '--' && $('#txtAllergiesDate_DayData option:selected').val() != '--' && $('#txtAllergiesDate_MonthData option:selected').val() == '--') {
        //                    alert('Please select Year or Month and Year or Month, Day and Year');
        //                    return false;
        //                }
        //                else if ($('#txtAllergiesDate_YearData option:selected').val() == '--' && $('#txtAllergiesDate_MonthData option:selected').val() != '--') {
        //                    alert('Please select Year or Month and Year or Month, Day and Year');
        //                    return false;
        //                }
        //                //bValid = bValid && checkCharLength(AllergyType, "Allergy Type", 3, 20);
        //                //bValid = bValid && checkCharLength(Allergy, "Allergen", 2, 50);
        //                //bValid = bValid && checkCharLength(Reaction, "Reaction", 3, 50);
        //                //bValid = bValid && checkLength(Status, "Please Select Status");


        //                if (bValid) {
        //                    //ajax 
        //                    try {


        //                        var requestData = {
        //                            PatientAllergyCntr: $.trim($('#txtallergyIDData').val()),
        //                            EffectiveDate: $.trim($('#txtAllergiesDateData').val()),
        //                            AllergenType: $.trim($('#txtAllergenTypeData').val()),
        //                            Allergen: $.trim($('#txtAllergenData').val()),
        //                            Reaction: $.trim($('#txtReactionData').val()),
        //                            Note: $.trim($('#txtNoteData').val()),
        //                            ConditionStatusId: $.trim($('#cmbConditionStatusData option:selected').val()),
        //                            Flag: $.trim($('#hdFlagData').val())
        //                        };


        //                        $.ajax({
        //                            type: 'POST',
        //                            url: 'clinical-summary-allergies-save',
        //                            data: JSON.stringify(requestData),
        //                            dataType: 'json',
        //                            contentType: 'application/json; charset=utf-8',
        //                            success: function (data) {
        //                                //   if ($('#hdFlag').val() == 'allergyWidg')
        //                                $("#allergies-portlet").html(data[0]);
        //                                //  else if ($('#hdFlag').val() == 'allergyTab')
        //                                $("#tbl-Allergies").fixedHeaderTable();
        //                                $("#allergies-portlet-tab").html(data[1]);

        //                                $("#tab-Allergies").fixedHeaderTable();
        //                            },
        //                            error: function (xhr, ajaxOptions, thrownError) {
        //                                //if error

        //                                //alert('Error : ' + xhr.message);
        //                                HideLoader();
        //                            },
        //                            complete: function (data) {
        //                                // if completed
        //                                HideLoader();

        //                            },
        //                            async: true,
        //                            processData: false
        //                        });
        //                    } catch (err) {

        //                        if (err && err !== "") {
        //                            alert(err.message);
        //                            HideLoader();
        //                        }
        //                    }
        //                    $(this).dialog("close");
        //                    ShowLoader();


        //                    //   end ajax


        //                }
        //            },
        //            Close: function () {
        //                allFields.val("").removeClass("ui-state-error");
        //                $(".validateTips ").empty();
        //                $("span").removeClass("ui-state-highlight");
        //                $(this).dialog("close");
        //            }
        //        },
        //        close: function () {
        //            allFields.val("").removeClass("ui-state-error");
        //            $(".validateTips ").empty();
        //            $("span").removeClass("ui-state-highlight");
        //            $(this).dialog("close");
        //        }
        //    });

        //    $("#createaddallergies_tab")
        // .button()
        // .click(function () {
        //     $('#txtallergyIDData ').val('0');
        //     $('#dtEffectiveDateData').attr("disabled", false);
        //     $('#txtAllergenTypeData').attr("disabled", false);
        //     $('#txtAllergenData').attr("disabled", false);
        //     $('#txtReactionData').attr("disabled", false);
        //     $('#txtNoteData').attr("disabled", false);
        //     $('#txtAllergiesDate_MonthData').attr("disabled", false);
        //     $('#txtAllergiesDate_DayData').attr("disabled", false);
        //     $('#txtAllergiesDate_YearData').attr("disabled", false);
        //     $('#cmbConditionStatusData').attr("disabled", false);
        //     $('#hdFlagData').val('allergyTab');
        //     $("#Allergies-Records").dialog("open");
        // });
//});


