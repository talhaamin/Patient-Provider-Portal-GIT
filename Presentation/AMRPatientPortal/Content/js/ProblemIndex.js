$(document).ready(function () {



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

});
$(function () {
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
        //alert(value);
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
        //alert(value);
        $('#txtPlastDate').val(value);
    });
});


function clearProblemcontrol() {
    $('#txtProblemsDate_Month').val("--");
    $('#txtProblemsDate_Day').val("--");
    $('#txtProblemsDate_Year').val("--");
    $('#txtProblemsLastDate_Month').val("--");
    $('#txtProblemsLastDate_Day').val("--");
    $('#txtProblemsLastDate_Year').val("--");
    $('#txtSearch').val("");
    $('#cmbConditionStatusProb').val("-1");
    $("#txtProblemsDescription").val("");
}



function clinicalSummary(id, flag) {
    if ($("#cmbFacilityHome").val() == 0) {
        $(".HideEditDelete").css('display', 'block');
    }
    else {
        $(".HideEditDelete").css('display', 'none');
    }
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
                ExtensionFilterFunct: "clinicalSummary(this.value,'visit');",
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
                            url: 'clinical-summary-problems-filter',
                            data: JSON.stringify(requestData),
                            dataType: 'json',
                            contentType: 'application/json; charset=utf-8',
                            success: function (data) {
                                // if success
                                $("#problem-portlet-tab").html(data);
                                HideLoader();
                                toggle_combobox();

                                $('#tab-problems').fixedHeaderTable();

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
                url: 'clinical-summary-problems-filter',
                data: JSON.stringify(requestData),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    // if success
                    $("#problem-portlet-tab").html(data);
                    HideLoader();
                    toggle_combobox();
                    $('#tab-problems').fixedHeaderTable();
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


//edit delete function start for problems
function OpenTabProblemRecord(id) {
    try {


        //$('.HideEditDelete').css('display', 'block');

        var obj = jQuery.parseJSON($("#hdProblemTab-" + id).val());
        $('#txtproblemID').val(id);
        $('#txtProblemsDate').val($.trim(obj.EffectiveDate));
        $('#txtPlastDate').val($.trim(obj.LastChangeDate));
        $('#txtProblemsDescription').val($.trim(obj.Note));
        $('#txtSearch').val($.trim(obj.Condition));
        $('#txtSearchText').val($.trim(obj.Condition));
        $('#txtSearchID').val($.trim(obj.CodeValue));
        $('#cmbConditionStatusProb').val($.trim(obj.ConditionStatusId));
        var year = obj.EffectiveDate[0];
        year += obj.EffectiveDate[1];
        year += obj.EffectiveDate[2];
        year += obj.EffectiveDate[3];
        var mon = obj.EffectiveDate[4];
        mon += obj.EffectiveDate[5];
        var date = obj.EffectiveDate[6];
        date += obj.EffectiveDate[7];

        $('#txtProblemsDate_Month').val(mon);
        $('#txtProblemsDate_Day').val(date);
        $('#txtProblemsDate_Year').val(year);


        var lyear = obj.LastChangeDate[0];
        lyear += obj.LastChangeDate[1];
        lyear += obj.LastChangeDate[2];
        lyear += obj.LastChangeDate[3];
        var lmon = obj.LastChangeDate[4];
        lmon += obj.LastChangeDate[5];
        var ldate = obj.LastChangeDate[6];
        ldate += obj.LastChangeDate[7];
        $('#txtProblemsLastDate_Month').val(lmon);
        $('#txtProblemsLastDate_Day').val(ldate);
        $('#txtProblemsLastDate_Year').val(lyear);

        $("#addproblems-form")
  .next(".ui-dialog-buttonpane")
  .find("button:contains('Save')")
  .button("option", "disabled", true);

        $('#txtProblemsDate_Month').attr("disabled", true);
        $('#txtProblemsDate_Day').attr("disabled", true);
        $('#txtProblemsDate_Year').attr("disabled", true);
        $('#txtProblemsLastDate_Month').attr("disabled", true);
        $('#txtProblemsLastDate_Day').attr("disabled", true);
        $('#txtProblemsLastDate_Year').attr("disabled", true);
        $('#txtSearch').attr("disabled", true);
        $('#txtProblemsDate').attr("disabled", true);
        $('#txtPlastDate').attr("disabled", true);
        $('#txtProblemsDescription').attr("disabled", true);
        $('#btnproblemSearch').attr("disabled", true);
        $('#cmbConditionStatusProb').attr("disabled", true);


    } catch (e) {
        alert(e.message);
    }
    $('#hdFlag').val('problemTab');
    $("#addproblems-form").dialog('option', 'title', 'Problem Details');
    $("#addproblems-form").dialog("open");

}

function EditProblemsTab() {
    $("#addproblems-form")
.next(".ui-dialog-buttonpane")
.find("button:contains('Save')")
.button("option", "disabled", false);
    $("#addproblems-form").dialog('option', 'title', 'Edit Problem');
    $('#txtProblemsDate_Month').attr("disabled", false);
    $('#txtProblemsDate_Day').attr("disabled", false);
    $('#txtProblemsDate_Year').attr("disabled", false);
    $('#txtProblemsLastDate_Month').attr("disabled", false);
    $('#txtProblemsLastDate_Day').attr("disabled", false);
    $('#txtProblemsLastDate_Year').attr("disabled", false);
    $('#txtSearch').attr("disabled", false);
    $('#txtProblemsDate').attr("disabled", false);
    $('#txtPlastDate').attr("disabled", false);
    $('#txtProblemsDescription').attr("disabled", false);
    $('#btnproblemSearch').attr("disabled", false);
    $('#cmbConditionStatusProb').attr("disabled", false);

    editflagProblemsTabs = 1;

}

function DeleteProblemTab() {
    $("#addproblems-form").dialog("close");
    chkAccess();
    ShowLoader();
    try {
        // $('#txtproblemID').val(id);
        var requestData = {
            PatientProblemCntr: $.trim($('#txtproblemID').val())
        };

        $.ajax({
            type: 'POST',
            url: 'problems-delete',
            data: JSON.stringify(requestData),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                // if success

                //  $("#problem-portlet").html(data[0]);
                //  alert(data[1])
                $("#problem-portlet-tab").html(data[1]);
                $("#tab-Problems").fixedHeaderTable();
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
                //if(editflagProblemsTabs!=1){bValid=false;}
                //allFields.removeClass("ui-state-error");

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

                if (bValid) {
                    var codVal = $.trim($('#txtSearchID').val());
                    if ($.trim($('#txtSearchText').val()) == "")
                    { codVal = 0; }

                    //ajax 
                    try {
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
                            url: 'clinical-summary-problems-save',
                            data: JSON.stringify(requestData),
                            dataType: 'json',
                            contentType: 'application/json; charset=utf-8',
                            success: function (data) {
                                //   alert("hi");
                                // alert(data[1]);
                                $("#problem-portlet-tab").html(data[1]);
                                $("#tab-Problems").fixedHeaderTable();
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

    $("#createaddproblems").click(function () {
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
        $('.ui-dialog-title').html("Add Problems");
        clearProblemcontrol();
        $("#addproblems-form")
  .next(".ui-dialog-buttonpane")
  .find("button:contains('Save')")
  .button("option", "disabled", false);
        $('.HideEditDelete').css('display', 'none');
        $("#addproblems-form").dialog("open");
        $("#addproblems-form")
.next(".ui-dialog-buttonpane")
.find("button:contains('Save')")
.button("option", "disabled", false);

    });
    // $("#createaddproblems_tab").click(function () {
    //    $('#txtproblemID').val('0');
    //    $('#txtSearchID').val('0');
    //    $('#txtSearchText').val('');
    //    $('#txtSearch').attr("disabled", false);
    //    $('#txtProblemsDate').attr("disabled", false);
    //    $('#txtPlastDate').attr("disabled", false);
    //    $('#cmbConditionStatusProb').attr("disabled", false);
    //    $('#txtProblemsDescription').attr("disabled", false);
    //    $('#btnproblemSearch').attr("disabled", false);
    //    $('#txtProblemsDate_Month').attr("disabled", false);
    //    $('#txtProblemsDate_Day').attr("disabled", false);
    //    $('#txtProblemsDate_Year').attr("disabled", false);
    //    $('#txtProblemsLastDate_Month').attr("disabled", false);
    //    $('#txtProblemsLastDate_Day').attr("disabled", false);
    //    $('#txtProblemsLastDate_Year').attr("disabled", false);
    //    $('.ui-dialog-title').html("Add Problems");
    //    $("#addproblems-form").dialog("open");

    //    $('#hdFlag').val('problemTab');
    //});
});
$(function () {

    $('#deleteProblemTab').click(fnOpenNormalDialogProblems);
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
                DeleteProblemTab();
            },
            "No": function () {
                $(this).dialog('close');
                //callback(false);
            }
        }
    });

}



function toggle_combobox() {
    if ($('#cmbFacilityHome :selected').text() != 'Patient Entered') {
        $('#createaddproblems').css('display', 'none');
        $('#cmbVisitsHome').attr('disabled', false);
        $('.showHideThisProblem').hide();
    }

    else {

        $('#cmbVisitsHome').text("");
        $('#cmbVisitsHome').attr('disabled', true);
        $('.showHideThisProblem').show();
        $('#createaddproblems').css('display', 'block');

        //$('#cmbVisitsHome').val(0);
        //  $('#createPresentMedication,#createaddstatements,#createaddproblems,#addvitalscreation,#createaddimmunizations,#createaddPOC,#createaddClinicalInstructions,#createaddProcedures,#createaddallergies,#addvitalscreation,#createaddpast,#createaddfamily,#createaddappointments,#createaddsocial').css('display', 'block');
    }

}



$(function () {
    $("input[type=submit], a, button")
    .button()
    .click(function (event) {

    });
    $('.full-width').horizontalNav(
{
    responsive: false

});
});