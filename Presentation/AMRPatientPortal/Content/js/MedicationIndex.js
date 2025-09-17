$(function () {
    $('#show-date-CurrStartDate').click(function () {
        $('#dtStartDate').datepicker("show");
    });
    $('#show-date-CurrEndDate').click(function () {
        $('#datepicker').datepicker("show");
    });
    $('#show-date-PastStartDate').click(function () {
        $('#dtStartDatepst').datepicker("show");
    });
    $('#show-date-PastEndDate').click(function () {
        $('#datepicker1').datepicker("show");
    });
});

function onloadcurrent() {
    ShowLoader();
    var requestData = {
        Current: false,
        FacilityId: $("#cmbFacilityHome option:selected").val(),
        VisitId: $("#cmbVisitsHome option:selected").val()
    };
    $.ajax({
        type: 'POST',
        url: 'medications-show',
        data: JSON.stringify(requestData),
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            // if success

            $("#current-med-grid").html(data.html);
            $("#tbl-CurrentMedication").fixedHeaderTable();
            if ($("#cmbFacilityHome").val() == 0) {
                $("#createPresentMedication").css("display", "block");
                $("#createPastMedication").css("display", "block");
                // $(".hidePastmedication").css("display", "block");
                $('.hidePastmedication').show();
            }
            else {
                $("#createPresentMedication").css("display", "none");
                $("#createPastMedication").css("display", "none");
                // $(".hidePastmedication").css("display", "none");
                $('.hidePastmedication').hide();
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
}

function onloadpass() {
    
    if ($("#cmbFacilityHome").val() == 0) {
        $("#createPresentMedication").css("display", "block");
        $("#createPastMedication").css("display", "block");
        //$(".hidePastmedication").css("display", "block");
        $('.hidePastmedication').show();
    }
    else {
        $("#createPresentMedication").css("display", "none");
        $("#createPastMedication").css("display", "none");
       // $(".hidePastmedication").css("display", "none");
        $('.hidePastmedication').hide();
    }
    ShowLoader();
    var requestData = {
        Current: true,
        FacilityId: $("#cmbFacilityHome option:selected").val(),
        VisitId: $("#cmbVisitsHome option:selected").val()
    };
    $.ajax({
        type: 'POST',
        url: 'medications-show',
        data: JSON.stringify(requestData),
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            // if success

            $("#past-med-grid").html(data.html);
            $("#tbl-PastMedication").fixedHeaderTable();
            if ($("#cmbFacilityHome").val() == 0) {
                $("#createPresentMedication").css("display", "block");
                $("#createPastMedication").css("display", "block");
                // $(".hidePastmedication").css("display", "block");
                $('.hidePastmedication').show();
            }
            else {
                $("#createPresentMedication").css("display", "none");
                $("#createPastMedication").css("display", "none");
              //  $(".hidePastmedication").css("display", "none");
                $('.hidePastmedication').hide();
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
}

function medicationFilter(id, flag) {

    if (flag == 'visit')
    { sec_arg = $('#cmbFacilityHome option:selected').val(); }
    else if (flag == 'location') {
        sec_arg = id;
        id = $('#cmbVisitsHome option:selected').val();
    }
    if ($("#cmbFacilityHome").val() == 0) {
        $("#createPresentMedication").css("display", "block");
        $("#createPastMedication").css("display", "block");
        // $(".hidePastmedication").css("display", "block");
        $('.hidePastmedication').show();
    }
    else {
        $("#createPresentMedication").css("display", "none");
        $("#createPastMedication").css("display", "none");
       // $(".hidePastmedication").css("display", "none");
        $('.hidePastmedication').hide();
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
          //      $("#tbl-PastMedication").fixedHeaderTable();
                if ($("#cmbFacilityHome").val() == 0) {
                    $("#createPresentMedication").css("display", "block");
                    $("#createPastMedication").css("display", "block");
                    //$(".hidePastmedication").css("display", "block");
                    $('.hidePastmedication').show();
                }
                else {
                    $("#createPresentMedication").css("display", "none");
                    $("#createPastMedication").css("display", "none");
                    // $(".hidePastmedication").css("display", "none");
                    $('.hidePastmedication').hide();
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


            },
            async: true,
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
              //  $("#tbl-CurrentMedication").fixedHeaderTable();
                $("#tbl-PastMedication").fixedHeaderTable();
                if ($("#cmbFacilityHome").val() == 0) {
                    $("#createPresentMedication").css("display", "block");
                    $("#createPastMedication").css("display", "block");
                    // $(".hidePastmedication").css("display", "block");
                    $('.hidePastmedication').show();
                }
                else {
                    $("#createPresentMedication").css("display", "none");
                    $("#createPastMedication").css("display", "none");
                    // $(".hidePastmedication").css("display", "none");
                    $('.hidePastmedication').hide();
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


$(function () {

    $('#deleteCurrentMedication').click(DeleteCurrentRecordConfrim);
});
$(function () {

    $('#deletePastMedication').click(deletePastRecordConfirm);
});
function medicationFilterNew(id, flag)
{
    if (flag == 'visit') {
        sec_arg = $('#cmbFacilityHome option:selected').val();
    }
    else if (flag == 'location') {
        sec_arg = id;
        id = $('#cmbVisitsHome option:selected').val();
    }
    var facilityOptions = document.getElementById('cmbFacilityHome').innerHTML;
    var visitOptions = document.getElementById('cmbVisitsHome').innerHTML;
    var facilitySelected = $('#cmbFacilityHome option:selected').val();
    var visitSelected = $('#cmbVisitsHome option:selected').val();
    // alert(facilityOptions);
    // alert(visitOptions);
    if (flag == 'location') {

        //filling dropdown start 
        try {
            ShowLoader();
            //var IsPrimaryData =
            var requestData = {

                FacilityId: sec_arg,
                ExtensionToggleFunct: " toggle_combobox();",
                ExtensionFilterFunct: "medicationFilterNew(this.value,'visit');",
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
                                toggle_combobox();
                                $("#current-med-grid").html(data); HideLoader();
                                $("#tbl-CurrentMedication").fixedHeaderTable();
                                //      $("#tbl-PastMedication").fixedHeaderTable();
                                if ($("#cmbFacilityHome").val() == 0) {
                                    $("#createPresentMedication").css("display", "block");
                                    $("#createPastMedication").css("display", "block");
                                    //$(".hidePastmedication").css("display", "block");
                                    $('.hidePastmedication').show();
                                }
                                else {
                                    $("#createPresentMedication").css("display", "none");
                                    $("#createPastMedication").css("display", "none");
                                    // $(".hidePastmedication").css("display", "none");
                                    $('.hidePastmedication').hide();
                                }

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
                                        url: 'medication-past-medications-filter',
                                        data: JSON.stringify(requestData),
                                        dataType: 'json',
                                        contentType: 'application/json; charset=utf-8',
                                        success: function (data) {
                                            // if success
                                            $("#past-med-grid").html(data); HideLoader();
                                            //  $("#tbl-CurrentMedication").fixedHeaderTable();
                                            $("#tbl-PastMedication").fixedHeaderTable();
                                            if ($("#cmbFacilityHome").val() == 0) {
                                                $("#createPresentMedication").css("display", "block");
                                                $("#createPastMedication").css("display", "block");
                                                // $(".hidePastmedication").css("display", "block");
                                                $('.hidePastmedication').show();
                                            }
                                            else {
                                                $("#createPresentMedication").css("display", "none");
                                                $("#createPastMedication").css("display", "none");
                                                // $(".hidePastmedication").css("display", "none");
                                                $('.hidePastmedication').hide();
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
        // alert(visitOptions);
        // alert(visitSelected);
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
                    //      $("#tbl-PastMedication").fixedHeaderTable();
                    if ($("#cmbFacilityHome").val() == 0) {
                        $("#createPresentMedication").css("display", "block");
                        $("#createPastMedication").css("display", "block");
                        //$(".hidePastmedication").css("display", "block");
                        $('.hidePastmedication').show();
                    }
                    else {
                        $("#createPresentMedication").css("display", "none");
                        $("#createPastMedication").css("display", "none");
                        // $(".hidePastmedication").css("display", "none");
                        $('.hidePastmedication').hide();
                    }

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
                            url: 'medication-past-medications-filter',
                            data: JSON.stringify(requestData),
                            dataType: 'json',
                            contentType: 'application/json; charset=utf-8',
                            success: function (data) {
                                // if success
                                toggle_combobox();
                                $("#past-med-grid").html(data); HideLoader();
                                //  $("#tbl-CurrentMedication").fixedHeaderTable();
                                $("#tbl-PastMedication").fixedHeaderTable();
                                if ($("#cmbFacilityHome").val() == 0) {
                                    $("#createPresentMedication").css("display", "block");
                                    $("#createPastMedication").css("display", "block");
                                    // $(".hidePastmedication").css("display", "block");
                                    $('.hidePastmedication').show();
                                }
                                else {
                                    $("#createPresentMedication").css("display", "none");
                                    $("#createPastMedication").css("display", "none");
                                    // $(".hidePastmedication").css("display", "none");
                                    $('.hidePastmedication').hide();
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
}

$(document).ready(function () {
    //if ($('#cmbFacilityHome :selected').text() == 'Patient Entered') {
    //    $('#cmbVisitsHome').text("");

    //}
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
    if ($("#cmbFacilityHome").val() == 0) {
        $("#createPresentMedication").css("display", "block");
        $("#createPastMedication").css("display", "block");
        //$(".hidePastmedication").css("display", "block");
        $('.hidePastmedication').show();
    }
    else {
        $("#createPresentMedication").css("display", "none");
        $("#createPastMedication").css("display", "none");
        //$(".hidePastmedication").css("display", "none");
        $('.hidePastmedication').hide();
    }
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
    if ($("#cmbFacilityHome").val() == 0) {
        $("#createPresentMedication").css("display", "block");
        $("#createPastMedication").css("display", "block");
       // $("#add-pharmacy").css("display", "block");
        //$(".hidePastmedication").css("display", "block");
        $('.hidePastmedication').show();
    }
    else {
        $("#createPresentMedication").css("display", "none");
        $("#createPastMedication").css("display", "none");
        // $(".hidePastmedication").css("display", "none");
        $('.hidePastmedication').hide();
      //  $("#add-pharmacy").css("display", "none");
    }
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
   

    $("#dialog-formCurrent").dialog({
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
                            Dose: $.trim($('#txtDose').val()),
                            DoseUnit: $.trim($('#txtDoseUnit').val())
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

            },
            Close: function () {
                allFields.val("").removeClass("ui-state-error");
                $("#dialog-formCurrent").dialog("option", "title", "Add Medication");
                $(".validateTips ").empty();
                $("span").removeClass("ui-state-highlight");
                $(this).dialog("close");
                $("#dialog-formCurrent")
   .next(".ui-dialog-buttonpane")
   .find("button:contains('Save')")
   .button("option", "disabled", false);
 
   
            }
        },
        close: function () {
            allFields.val("").removeClass("ui-state-error");
            $("#dialog-formCurrent").dialog("option", "title", "Add Medication");
            $("#dialog-formCurrent")
.next(".ui-dialog-buttonpane")
.find("button:contains('Save')")
.button("option", "disabled", false);
        }
    });
    $("#createPresentMedication")
    .button()
    .click(function () {

        $('.hidePastmedication').hide();
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
        $('#txtDose').attr("disabled", false);
        $('#txtDoseUnit').attr("disabled", false);
        document.getElementById('chkMedicationAdministered').checked = false;

        $('#chkMedicationAdministered').attr("disabled", false);
      //  document.getElementById('chkMedicationAdministered').checked = false;
        $("#dialog-formCurrent").dialog("option", "title", "Add Medication");
        $("#dialog-formCurrent").dialog("open");
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
                            duringvisit: duringvisit,
                            Dose: $.trim($('#txtDose1').val()),
                            DoseUnit: $.trim($('#txtDoseUnit1').val())
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
             //   $("#dialog-form1").dialog("option", "title", "Add a new Medication");
                $("#dialog-form1")
     .next(".ui-dialog-buttonpane")
     .find("button:contains('Save')")
     .button("option", "disabled", false);

                $(this).dialog("close");


            }
        },
        close: function () {
            allFields.val("").removeClass("ui-state-error");
        //    $("#dialog-form1").dialog("option", "title", "Add a new Medication");
            $("#dialog-form1")
.next(".ui-dialog-buttonpane")
.find("button:contains('Save')")
.button("option", "disabled", false);
        }
    });
    $("#createPastMedication")
    .button()
    .click(function () {
        $('.hidePastmedication').hide();

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
        $('#txtDose1').attr("disabled", false);
        $('#txtDoseUnit1').attr("disabled", false);
        $("#dialog-form1").dialog("option", "title", "Add Medication");
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
    $('#txtDose1').val('');
    $('#txtDoseUnit1').val('');
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
    $('#txtDose').val('');
    $('#txtDoseUnit').val('');
}
function clearPharmacyDialog() {
    $('#txtPharmID').val('');
    $('#txtPharmacyNamemed').val('');
    $('#txtPharmAddress1med').val('');
    $('#txtPharmAddress2med').val('');
    $('#txtPharmCitymed').val('');
   // $('#txtPharmState').val('');
    $('#txtPharmZipCodemed').val('');
    $('#chkPrefmed').prop('checked', false);
    $('#txtPhonemed').val('');
    $('#txtWebsitephar').val('');


    $('#txtPharmacyNamemed').attr("disabled", false);
    $('#txtPharmAddress1med').attr("disabled", false);
    $('#txtPharmCitymed').attr("disabled", false);
    $('#cmbStatesmed').attr("disabled", false);
    $('#txtPharmZipCodemed').attr("disabled", false);
    $('#txtPharmAddress2med').attr("disabled", false);
    $('#chkPrefmed').attr("disabled", false);
    $('#txtPhonemed').attr("disabled", false);
    $('#txtWebsitephar').attr("disabled", false);
}

function deletePastMedication() {
    $("#dialog-form1").dialog("close");
    chkAccess();
    ShowLoader();
    try {
    //    $('#txtMedID1').val(id);
      //  alert("id is " + $('#txtMedID1').val());
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
                 $("#past-med-grid").html(data.html);
                $("#tbl-PastMedication").fixedHeaderTable();
                 $("#txtMedID1").val("0")
             //    $("#dialog-form1").dialog("close");
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
             //   $("#dialog-form1").dialog("close");

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
    //$('#datepicker1').val($.trim(obj.ExpireDate));
    $('#cmbTakingMedicine1').val($.trim(obj.Status));
    $('#txtPharmacy1').val($.trim(obj.Pharmacy));
    $('#txtInstructions1').val($.trim(obj.Note));
    $('#txtDays1').val($.trim(obj.Days));
    if (obj.Quantity == 0) {
        $('#txtQty1').val('');
    }
    else {
        $('#txtQty1').val($.trim(obj.Quantity));
    }
    if (obj.Refills == 0) {
        $('#txtRefills1').val('');
    }
    else {
        $('#txtRefills1').val($.trim(obj.Refills));
    }
    
    $('#txtDiagnosis1').val($.trim(obj.Diagnosis));
    //$('#dtStartDatepst').val($.trim(obj.StartDate));
    if (obj.Frequency == 0) {
        $('#txtFrequencypst').val('');
    }
    else {
        $('#txtFrequencypst').val($.trim(obj.Frequency));
    }
    $('#txtRoutepst').val($.trim(obj.Route));
    if ($.trim(obj.Dose) == 0)
    { $('#txtDose1').val(''); } else { $('#txtDose1').val(obj.Dose); }
    $('#txtDoseUnit1').val($.trim(obj.DoseUnit));
    if ($.trim(obj.StartDate) == '01/01/1900' || $.trim(obj.StartDate) == '1/1/1900') {
        $('#dtStartDatepst').val('');
    }
    else {
        $('#dtStartDatepst').val(obj.StartDate);
    }
    if ($.trim(obj.ExpireDate) == '01/01/1900' || $.trim(obj.ExpireDate) == '1/1/1900') {
        $('#datepicker1').val('');
    }
    else {
        $('#datepicker1').val(obj.ExpireDate);
    }
    var check = obj.Duringvisit;
    if (check == 'True') {
        document.getElementById('DuringVist1').checked = true;
    }
    else {
        document.getElementById('DuringVist1').checked = false;
    }

    $('#show-date-PastStartDate').hide();
    $('#show-date-PastEndDate').hide();
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

    $('#txtDose1').attr("disabled", true);
    $('#txtDoseUnit1').attr("disabled", true);
    if ($("#cmbFacilityHome").val() == 0) {

        // $(".hidePastmedication").css("display", "block");
        $('.hidePastmedication').show();
    }
    else {
     
        // $(".hidePastmedication").css("display", "none");
        $('.hidePastmedication').hide();
    }

    $("#dialog-form1").dialog("option", "title", "Medication Details");

    $("#dialog-form1").dialog("open");
    $("#dialog-form1")
      .next(".ui-dialog-buttonpane")
      .find("button:contains('Save')")
      .button("option", "disabled", true);
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
    $('#txtDose1').val($.trim(obj.Dose));
    $('#txtDoseUnit1').val($.trim(obj.DoseUnit));

    $('#show-date-PastStartDate').show();
    $('#show-date-PastEndDate').show();
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
    $('#txtDose1').attr("disabled", false);
    $('#txtDoseUnit1').attr("disabled", false);

    $("#dialog-form1").dialog("option", "title", "Edit Medication");
    $("#dialog-form1").dialog("open");
}
function deleteCurrentMedication() {
    $("#dialog-formCurrent").dialog("close");
    chkAccess();
    ShowLoader();
    try {
       // $('#txtMedID').val(id);
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
                $("#txtMedID").val("0");
                $("#current-med-grid").html(data.html);
                $("#tbl-CurrentMedication").fixedHeaderTable();
             //   $("#dialog-form").dialog('close');
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
             //   $("#dialog-form").dialog('close');
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

function DeleteCurrentRecordConfrim() {
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
                deleteCurrentMedication();
            },
            "No": function () {
                $(this).dialog('close');
                //callback(false);
            }
        }
    });

}
function deletePastRecordConfirm()
{
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
                deletePastMedication();
            },
            "No": function () {
                $(this).dialog('close');
                //callback(false);
            }
        }
    });
}
function deletePharmacyRecord(id)
{
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
                deletePharmacy(id);
            },
            "No": function () {
                $(this).dialog('close');
                //callback(false);
            }
        }
    });

}

function detailCurrentMedication(id) {

    
    var obj = jQuery.parseJSON($("#hdCurJson" + id).val());
    var check = obj.duringvisit;
    $('#txtMedID').val(id);
    $('#txtMedicationName').val($.trim(obj.MedicationName));
    //$('#datepicker').val($.trim(obj.ExpireDate));
    $('#cmbTakingMedicine').val($.trim(obj.Status));
    $("#txtPharmacy option:selected").text(obj.Pharmacy);
    $('#txtInstructions').val($.trim(obj.Note));
    $('#txtDays').val($.trim(obj.Days));
    if (obj.Quantity == 0) {
        $('#txtQty').val('');
    }
    else {
        $('#txtQty').val($.trim(obj.Quantity));
    }
    if (obj.Refills == 0) {
        $('#txtRefills').val('');
    }
    else {
        $('#txtRefills').val($.trim(obj.Refills));
    }
    $('#txtDiagnosis').val($.trim(obj.Diagnosis));
    //$('#dtStartDate').val($.trim(obj.StartDate));
    if (obj.Frequency == 0) {
        $('#txtFrequency').val('');
    }
    else {
        $('#txtFrequency').val($.trim(obj.Frequency));
    }
    $('#txtRoute').val($.trim(obj.Route));
    if ($.trim(obj.Dose) == 0)
    { $('#txtDose').val(''); } else { $('#txtDose').val(obj.Dose); }
    $('#txtDoseUnit').val($.trim(obj.DoseUnit));

    if ($.trim(obj.StartDate) == '01/01/1900' || $.trim(obj.StartDate) == '1/1/1900') {
        $('#dtStartDate').val('');
    }
    else {
        $('#dtStartDate').val(obj.StartDate);
    }
    if ($.trim(obj.ExpireDate) == '01/01/1900' || $.trim(obj.ExpireDate) == '1/1/1900') {
        $('#datepicker').val('');
    }
    else {
        $('#datepicker').val(obj.ExpireDate);
    }
    if (check == 'True') {
        document.getElementById('chkMedicationAdministered').checked = true;
    }
    else {
        document.getElementById('chkMedicationAdministered').checked = false;
    }

    $("#show-date-CurrEndDate").hide();
    $("#show-date-CurrStartDate").hide();
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
    $('#txtDose').attr("disabled", true);
    $('#txtDoseUnit').attr("disabled", true);
    $('#chkMedicationAdministered').attr("disabled", true);
    if ($("#cmbFacilityHome").val() == 0) {
       
      
        // $(".hidePastmedication").css("display", "block");
        $('.hidePastmedication').show();
    }
    else {
      
        // $(".hidePastmedication").css("display", "none");
        $('.hidePastmedication').hide();
    }
    $("#dialog-formCurrent")
.next(".ui-dialog-buttonpane")
.find("button:contains('Save')")
.button("option", "disabled", true);
    $("#dialog-formCurrent").dialog("option", "title", "Medication Details");
    $("#dialog-formCurrent").dialog("open");
  
}
function EditCurrentMedication()
{
    $("#dialog-formCurrent").dialog("option", "title", "Edit Medication");
    
    $("#dialog-formCurrent")
.next(".ui-dialog-buttonpane")
.find("button:contains('Save')")
.button("option", "disabled", false);
        
  $("#show-date-CurrEndDate").show();
  $("#show-date-CurrStartDate").show();
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
    $('#txtDose').attr("disabled", false);
    $('#txtDoseUnit').attr("disabled", false);
    $('#chkMedicationAdministered').attr("disabled", false);


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
    $('#txtDose').val($.trim(obj.Dose));
    $('#txtDoseUnit').val($.trim(obj.DoseUnit));
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
    $('#txtDose').attr("disabled", false);
    $('#txtDoseUnit').attr("disabled", false);
    $('#chkMedicationAdministered').attr("disabled", false);
    $("#dialog-formCurrent").dialog("option", "title", "Edit Medication");
    $("#dialog-formCurrent").dialog("open");
}

$(function () {

    $("#dialog-form3").dialog
    ({
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








//edit delete function start for pharmacy 
function OpenPharmacyRecord(id) {
    $('#showHidePharm').show();
    //if ($("#cmbFacilityHome").val() == 0) {

    //    // $(".hidePastmedication").css("display", "block");
    //    $('.HideEditDelete').show();
    //}
    //else {

    //    // $(".hidePastmedication").css("display", "none");
    //    $('.HideEditDelete').hide();
    //}
    var obj = jQuery.parseJSON($("#hdPharmJson" + id).val());
    var flag = false;
    flag = obj.Preferred;
  //  alert(flag);
    $('#txtPharmID').val(id);
    $('#txtPharmacyNamemed').val($.trim(obj.PharmacyName));
    $('#txtPharmAddress1med').val($.trim(obj.Address1));
    $('#txtPharmAddress2med').val($.trim(obj.Address2));
    $('#txtPharmCitymed').val($.trim(obj.City));
    $('#txtPhonemed').val($.trim(obj.Phone))
  //  $('#txtPharmState').val($.trim(obj.State));
    $('#cmbStatesmed').val(obj.State);

    $('#txtPharmZipCodemed').val($.trim(obj.ZipCode));
    $('#txtPharmAddress2med').val($.trim(obj.Address2));
    if (flag == "False") {
        $('#chkPrefmed').prop('checked', false);
    }
    else {
        $('#chkPrefmed').prop('checked', true);

    }

    $('#txtPharmacyNamemed').attr("disabled", true);
    $('#txtPharmAddress1med').attr("disabled", true);
    $('#txtPharmCitymed').attr("disabled", true);
    $('#cmbStatesmed').attr("disabled", true);
    $('#txtPharmZipCodemed').attr("disabled", true);
    $('#txtPharmAddress2med').attr("disabled", true);
    $('#chkPrefmed').attr("disabled", true);
    $('#txtPhonemed').attr("disabled", true);
    $('#txtWebsitephar').attr("disabled", true);
    $("#dialog-pharmacy1").dialog("open");
    $("#dialog-pharmacy1")
     .next(".ui-dialog-buttonpane")
     .find("button:contains('Save')")
     .button("option", "disabled", true);

    $("#dialog-pharmacy1").dialog('option', 'title', 'Pharmacy Details');

}

function EditPharamcy() {


    $('#txtPharmacyNamemed').attr("disabled", false);
    $('#txtPharmAddress1med').attr("disabled", false);
    $('#txtPharmCitymed').attr("disabled", false);
    $('#cmbStatesmed').attr("disabled", false);
    $('#txtPharmZipCodemed').attr("disabled", false);
    $('#txtPharmAddress2med').attr("disabled", false);
    $('#chkPrefmed').attr("disabled", false);
    $('#txtPhonemed').attr("disabled", false);
    $('#txtWebsitephar').attr("disabled", false);
    $("#dialog-pharmacy1")
     .next(".ui-dialog-buttonpane")
     .find("button:contains('Save')")
     .button("option", "disabled", false);
    $("#dialog-pharmacy1").dialog('option', 'title', 'Edit Pharmacy');
    $("#dialog-pharmacy1").dialog("open");

    editpharmacyflag = 1;

}

$(function () {

    $("#dialog-pharmacy1").dialog
    ({
        autoOpen: false,
        show: {
            effect: "blind",
            duration: 100
        },
        hide: {
            effect: "blind",
            duration: 100
        },
        height: 600,
        width: 700,
        modal: true,
        buttons: {
            "Save": function () {
                chkAccess();
                var bValid = true;
                //if (editpharmacyflag != 1) { bValid = false; }
                if ($("#txtPharmacyNamemed").val() == "") {
                    alert("Please enter the Pharmacy Name");
                    bValid = false;
                }
                else if ($("#txtPharmAddress1med").val() == "") {
                    alert("Please enter the Address");
                    bValid = false;
                }
                else if ($("#txtPharmCitymed").val() == "") {
                    alert("Please enter the City");
                    bValid = false;
                }
                else if ($("#txtPharmZipCodemed").val() == "") {
                    alert("Please enter the Zip code");
                    bValid = false;

                }


                if (bValid) {

                    try {
                        var requestData = {
                            PharmacyCntr: $.trim($('#txtPharmID').val()),
                            PharmacyName: $.trim($('#txtPharmacyNamemed').val()),
                            Address1: $.trim($('#txtPharmAddress1med').val()),
                            City: $.trim($('#txtPharmCitymed').val()),
                            State: $.trim($('#cmbStatesmed option:selected').text()),
                            PostalCode: $.trim($('#txtPharmZipCodemed').val()),
                            Address2: $.trim($('#txtPharmAddress2med').val()),
                            Phone:$.trim($("#txtPhonemed").val()),
                            Preferred: $('#chkPrefmed').prop('checked'),

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
                    $(this).dialog("close");
                    ShowLoader();
                }

            },
            Close: function () {
                $("#dialog-pharmacy1")
     .next(".ui-dialog-buttonpane")
     .find("button:contains('Save')")
     .button("option", "disabled", false);
                $(this).dialog("close");
            }
        },
        close: function () {
            $("#dialog-pharmacy1")
.next(".ui-dialog-buttonpane")
.find("button:contains('Save')")
.button("option", "disabled", false);
        }
    });



    $("#add-pharmacy")
    .button()
    .click(function () {
        $('#showHidePharm').hide();
        $("#dialog-pharmacy1")
  .next(".ui-dialog-buttonpane")
  .find("button:contains('Save')")
  .button("option", "disabled", false);
        $('.HideEditDelete').css('display', 'none');
        $("#dialog-pharmacy1").dialog("open");
    });

});

function deletePharmacy() {
    $("#dialog-pharmacy1").dialog("close");
    ShowLoader();
    try {
        
        var requestData = {
            PharmacyCntr: $.trim($('#txtPharmID').val()),
            PharmacyName: $.trim($('#txtPharmacyNamemed').val()),
            Address: $.trim($('#txtPharmAddress1med').val()),
            City: $.trim($('#txtPharmCitymed').val()),
            State: $.trim($('#cmbStatesmed option:selected').text()),
            ZipCode: $.trim($('#txtPharmZipCodemed').val()),


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

$(function () {

    $('#deletePharmacy').click(fnOpenNormalDialogPharmacy);
});
function fnOpenNormalDialogPharmacy() {
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
                deletePharmacy();
            },
            "No": function () {
                $(this).dialog('close');
                //callback(false);
            }
        }
    });

}

//edit delete function end for pharmacy 


$(function () {
    $("input[type=submit], a, button")
    .button()
    .click(function (event) {
       
    });
});

$(document).ready(function () {
    $("#txtPhonemed").mask("999-999-9999");
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
    if ($("#cmbFacilityHome").val() == 0) {
      //  $("#add-pharmacy").show();
        $(".hidePharmacy").show();
       
    }
   else {
     //   $("#add-pharmacy").hide();
        $(".hidePharmacy").hide();
      
    }

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
     //   $("#add-pharmacy").hide();
        $(".hidePharmacy").hide();
    }

    else
        //if ($('#cmbFacilityHome :selected').text() == 'Patient Entered')
    {
        $('#cmbVisitsHome').text("");
        $('#cmbVisitsHome').attr('disabled', true);
      //  $("#add-pharmacy").show();
        $(".hidePharmacy").show();
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
function DeleteCurrentRecord(id) {
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
                deleteCurrentMedication(id);
            },
            "No": function () {
                $(this).dialog('close');
                //callback(false);
            }
        }
    });

}
function deletePastRecord(id) {
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
                deletePastMedication(id);
            },
            "No": function () {
                $(this).dialog('close');
                //callback(false);
            }
        }
    });
}
function deletePharmacyRecord(id) {
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
                deletePharmacy(id);
            },
            "No": function () {
                $(this).dialog('close');
                //callback(false);
            }
        }
    });

}
function EditPastMedication()
{
    $("#dialog-form1").dialog("option", "title", "Edit Medication");
    $("#dialog-form1").next(".ui-dialog-buttonpane").find("button:contains('Save')").button("option", "disabled", false);


    $('#show-date-PastStartDate').show();
    $('#show-date-PastEndDate').show();
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
    $('#txtDose1').attr("disabled", false);
    $('#txtDoseUnit1').attr("disabled", false);
}
