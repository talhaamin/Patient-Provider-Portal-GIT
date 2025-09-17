$(document).ready(function () {
    //$("#tbl-App").fixedHeaderTable();
    $("#tbl-MessageAppointment").fixedHeaderTable();
   
});

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
            //"Save": function () {
            //    var bValid = true;



            //    function chkStr(value) {
            //        var str;
            //        if (value == true) {
            //            str = '1';
            //            return str;
            //        }
            //        else if (value == false) {
            //            str = '0';
            //            return str;
            //        }
            //    };
            //    var PreferredPeriod1 = chkStr($('#chkmor').prop('checked'));
            //    PreferredPeriod1 += chkStr($('#chkAft').prop('checked'));
            //    PreferredPeriod1 += chkStr($('#chkeve').prop('checked'));

            //    var PreferredWeekDay1 = chkStr($('#chkmon').prop('checked'));
            //    PreferredWeekDay1 += chkStr($('#chktue').prop('checked'));
            //    PreferredWeekDay1 += chkStr($('#chkwed').prop('checked'));
            //    PreferredWeekDay1 += chkStr($('#chkthu').prop('checked'));
            //    PreferredWeekDay1 += chkStr($('#chkfri').prop('checked'));
            //    //alert(PreferredWeekDay1);
            //    //bValid = false;

            //    if (bValid) {
            //        //ajax 
            //        try {


            //            var requestData = {
            //                FacilityId: $.trim($('#cmbAppointmentFacility1').val()),
            //                ProviderId_To: $.trim($('#cmbAppointmentDoctor1').val()),
            //                ProviderId_Appointment: $.trim($('#cmbAppointmentDoctor1').val()),
            //                MessageUrgencyId: $.trim($('#cmbMessageUrgency1').val()),
            //                PreferredPeriod: PreferredPeriod1,
            //                PreferredWeekDay: PreferredWeekDay1,

            //                AppointmentStart: $.trim($('#txtPrefDateFrom').val()),
            //                AppointmentEnd: $.trim($('#txtPrefDateTo').val()),
            //                PreferredTime: $.trim($('#cmbPreftime option:selected').text()),
            //                VisitReason: $.trim($('#txtReason').val()),
            //                MessageRequest: $.trim($('#txtComment').val())

            //            };


            //            $.ajax({
            //                type: 'POST',
            //                url: 'index-Appointment-save',
            //                data: JSON.stringify(requestData),
            //                dataType: 'json',
            //                contentType: 'application/json; charset=utf-8',
            //                success: function (data) {
            //                    // if success
            //                    // alert("Success : " + data);
            //                    // $("#immunization-portlet").html(data);
            //                    $("#Appointment-portlet").html(data);
            //                    //alert(data);

            //                },
            //                error: function (xhr, ajaxOptions, thrownError) {
            //                    //if error

            //                    alert('Error : ' + xhr.message);
            //                    HideLoader();
            //                },
            //                complete: function (data) {
            //                    // if completed
            //                    HideLoader();

            //                },
            //                async: true,
            //                processData: false
            //            });
            //        } catch (err) {

            //            if (err && err !== "") {
            //                alert(err.message);
            //                HideLoader();
            //            }
            //        }
            //        $(this).dialog("close");
            //        ShowLoader();


            //        //                        end ajax


            //    }
            //},
            Close: function () {
        
                $(this).dialog("close");
            }
        },
        close: function () {

        }
    });
   

});

function clearthecontroll()
{
    $('#cmbAppointmentFacility1').val("-1");
    $('#cmbAppointmentDoctor1').val("6")
    $('#chkmor').attr("checked", false);
    $('#chkAft').attr("checked", false);
    $('#chkeve').attr("checked", false);
    $('#cmbMessageUrgency1').val("-1");
    $('#chkmon').attr("checked", false);
    $('#chktue').attr("checked", false);
    $('#chkwed').attr("checked", false);
    $('#chkthu').attr("checked", false);
    $('#chkfri').attr("checked", false);
    $('#chksat').attr("checked", false);
    $('#chksun').attr("checked", false);
    $('#txtPrefDateFrom').val(" ");
    $('#txtPrefDateTo').val(" ");
    $('#cmbPreftime').val("-1");
    $('#txtReason').val(" ");
    $('#txtComment').val(" ");
   
}

//- end of appointment button script
function enablecontroll() {
    $('#cmbAppointmentFacility1').attr("disabled", false);
    $('#cmbAppointmentDoctor1').attr("disabled", false);
    $('#chkmor').attr("disabled", false);
    $('#chkAft').attr("disabled", false);
    $('#chkeve').attr("disabled", false);
    $('#cmbMessageUrgency1').attr("disabled", false);
    $('#chkmon').attr("disabled", false);
    $('#chktue').attr("disabled", false);
    $('#chkwed').attr("disabled", false);
    $('#chkthu').attr("disabled", false);
    $('#chkfri').attr("disabled", false);
    $('#chksat').attr("disabled", false);
    $('#chksun').attr("disabled", false);
    $('#txtPrefDateFrom').attr("disabled", false);
    $('#txtPrefDateTo').attr("disabled", false);
    $('#cmbPreftime').attr("disabled", false);
    $('#txtReason').attr("disabled", false);
    $('#txtComment').attr("disabled", false);
}

$(function () {
    $("input[type=submit], a, button")
    .button()
    .click(function (event) {
        // event.preventDefault();
    });
});


$(document).ready(function () {

    $('#MessageHeaderGrid').find('td').css('text-align', 'center');
    $('#MessageHeaderGrid').find('th').css('text-align', 'center');
    // Call horizontalNav on the navigations wrapping element
    $('.full-width').horizontalNav(
         {
             responsive: false

         });
});

function checkAll(checkId) {
    var inputs = document.getElementsByTagName("input");
    for (var i = 0; i < inputs.length; i++) {
        if (inputs[i].type == "checkbox" && inputs[i].id == checkId) {
            if (inputs[i].checked == true) {
                inputs[i].checked = false;
            } else if (inputs[i].checked == false) {
                inputs[i].checked = true;
            }
        }
    }
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
        var flag = obj.MessageUrgency;
        if (flag == "True") {
            $('#cmbMessageUrgency2').val("true");
        }
        else {
            $('#cmbMessageUrgency2').val("false");
        }
        $('#cmbAppointmentFacility2').val($.trim(obj.FacilityId));
        $('#cmbAppointmentDoctor2').val($.trim(obj.ProviderId_To));
        $('#chkmor1').prop('checked', chkstr(obj.PreferredPeriod[0]));
        $('#chkAft1').prop('checked', chkstr(obj.PreferredPeriod[1]));
        $('#chkeve1').prop('checked', chkstr(obj.PreferredPeriod[2]));
    
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
function AppointmentDelete(id) {

    ShowLoader();
    try {
        var requestData = {
            DeletedMessageIDs: id
        };

        $.ajax({
            type: 'POST',
            url: 'Appointment-Cancel',
            data: JSON.stringify(requestData),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                // if success
                // alert("Success : " + data);
                $("#MessageHeaderGrid1").html(data.html);
               // $("#tbl-App").fixedHeaderTable();
                $("#tbl-MessageAppointment").fixedHeaderTable();
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
var tempAppoint = null;
var objnameAppoint = null;
var Appointmentclassdetail = null;
function AppointmentDetail(id, classAppointName) {

    tempAppointDetail = null;
    Appointmentclassdetail = $("#MessageHeader-" + id).hasClass("selected").toString();
    // alert(Appointmentclassdetail);

    if (Appointmentclassdetail == "false") {
        $("#MessageHeader-" + id).addClass("selected").siblings().removeClass("selected");

        $("#MessageHeader-" + id).removeClass("r1");
        $("#MessageHeader-" + id).removeClass("r0");

        if (tempAppoint != null) {
            if (objnameAppoint == 'r0') {
                $(tempAppoint).addClass("r0");
            }
            else {
                $(tempAppoint).addClass("r1");
            }
        }
        tempAppoint = ("#MessageHeader-" + id);
        objnameAppoint = classAppointName;
    }
    ShowLoader();
    var obj = jQuery.parseJSON($("#appointment-" + id).val());
    if (obj.MessageRead == 'False')
    {
        if (obj.ProviderIdFrom != '0') {
            
            try {

                var requestData = {
                    MessageDetailId: obj.ID,
                    MessageRead: true
                };

                $.ajax({
                    type: 'POST',
                    url: 'Update-Message-Read-Flag',
                    data: JSON.stringify(requestData),
                    dataType: 'json',
                    contentType: 'application/json; charset=utf-8',
                    success: function (data) {
                        // if success
                        //$('#MessageDetail' + obj.ID + ' .unread').css('color', '#666699');
                        //$('#MessageDetail' + obj.ID + ' .unread').css('font-weight', 'normal');
                        //$('#MessageDetail' + obj.ID + ' td div').removeClass('unread');
                        //if ($('#MsgDetail').find(".unread").length > 0) {
                        //    //  alert('found');
                        //}
                        //else {

                        $('#MessageHeader-' + obj.ID + ' td').css('color', '#666699');
                        $('#MessageHeader-' + obj.ID + ' td').css('font-weight', 'normal');
                        //   }

                      //  $('#circle').html(data.TotalMessages);
                        //$('#hdAppointmentMessages').html(data.AppointmentMessages);
                        //$('#hdGeneralMessages').html(data.GeneralMessages);
                        //$('#hdMedicationMessages').html(data.MedicationMessages);
                       // $('#hdReferralMessages').html(data.ReferralMessages);
                      //  $('#hdTotalMessages').html(data.TotalMessages);

                      //  $('#GeneralMessages').html("Inbox(<b style='font-size:14px;'>" + data.GeneralMessages + "</b>)");
                       // $('#AppointmentMessages').html("Appointment(<b style='font-size:14px;'>" + data.AppointmentMessages + "</b>)");
                       // $('#MedicationMessages').html("Refill Requests(<b style='font-size:14px;'>" + data.MedicationMessages + "</b>)");
                       // $('#ReferralMessages').html("Referral Requests(<b style='font-size:14px;'>" + data.ReferralMessages + "</b>)");

                        var html = '<div id="Inform_Head" class="ui-corner-all">';
                        html = html + '<h4 style="padding-top: 10px; margin-left: 30px;">';
                        if (obj.ProviderIdFrom !== '0') {
                            html = html + '<b>Appointment Request Response</b></h4>';
                        }
                        else {
                            html = html + '<b>Appointment Request</b></h4>';
                        }
                        //  html = html + '<b>' + obj.MessageType + '</b></h4>';
                        html = html + '<div style=\"float: right; margin-top: -30px; margin-right:10px; font-size: 12px; color: #999999;\">';

                        var today = new Date();
                        var dd = today.getDate();
                        var mm = today.getMonth() + 1; //January is 0!

                        var yyyy = today.getFullYear();
                        if (dd < 10) { dd = '0' + dd } if (mm < 10) { mm = '0' + mm } today = mm + '/' + dd + '/' + yyyy;

                        html = html + today;
                        html = html + '</div>';
                        html = html + '<div style="margin-top: 1px; margin-left: 50px; font-size: 12px; color: #999999;">';
                        html = html + 'From:  ' + obj.FacilityName;
                        html = html + '</div>';
                        html = html + '<div style="margin-top: 1px; margin-left: 50px; font-size: 12px; color: #999999;">';
                        html = html + 'To:  ' + obj.PatientName;
                        html = html + '</div>';
                        html = html + '</div>';
                        //<!--End of reply part-->
                        html = html + '<div id="reply" style="height: 368px;" class="ui-corner-all">';
                        html = html + '<div style="margin-left: 30px;">';
                        html = html + '        <br>';
                        //html = html + '        <br>';
                        //html = html + obj.MessageType;
                        //html = html + '                        <br>';
                        //html = html + 'Facility Name :' + obj.FacilityName;
                        //html = html + '                        <br>';
                        if (obj.ProviderIdFrom !== '0') {
                            html = html + 'Date :' + obj.AppointmentStart;
                            html = html + '                        <br>';
                            html = html + 'Time :' + obj.AppointmentStartTime;
                            html = html + '                        <br>';
                            html = html + 'Message Response: ' + obj.MessageResponse;
                            html = html + '                        <br>';


                        }

                        else {
                            var urgent = obj.MessageUrgency;
                            if (urgent == "False") {
                                html = html + 'Urgent : No';
                                html = html + '                        <br>';
                            }
                            else {
                                html = html + 'Urgent : Yes';
                                html = html + '                        <br>';

                            }
                            //html = html + 'Urgent : ' + (obj.MessageUrgency ? "Yes" : "No");
                            //html = html + '                        <br>';
                            html = html + 'Preferred Date Range : ' + obj.AppointmentStart + ' to ' + obj.AppointmentEnd;
                            html = html + '                        <br>';
                            //   html = html + 'Message: ' + obj.VisitReason;
                            var period = obj.PreferredPeriod;
                            var periodval = "";
                            if (period[0] == '1') {
                                periodval += 'Morning, ';
                            }
                            if (period[1] == '1') {
                                periodval += 'Afternoon, ';
                            }
                            if (period[2] == '1') {
                                periodval += 'Evening ';
                            }

                            var day = obj.PreferredWeekDay;
                            var dayval = "";
                            if (day[0] == '1') {
                                dayval += 'Monday, ';
                            }
                            if (day[1] == '1') {
                                dayval += 'Tuesday, ';
                            }
                            if (day[2] == '1') {
                                dayval += 'Wednesday, ';
                            }
                            if (day[3] == '1') {
                                dayval += 'Thursday, ';
                            }
                            if (day[4] == '1') {
                                dayval += 'Friday, ';
                            }
                            if (day[5] == '1') {
                                dayval += 'Saturday, ';
                            }
                            if (day[6] == '1') {
                                dayval += 'Sunday ';
                            }
                            html = html + 'Preferred Day : ' + dayval;
                            html = html + '                        <br>';
                            html = html + 'Preffered Time of Day : ' + periodval;
                            html = html + '                        <br>';
                            html = html + 'Preffered Time : ' + obj.PreferredTime;
                            html = html + '                        <br>';

                            html = html + 'Reason For Visit: ' + obj.VisitReason;
                            html = html + '                        <br>';

                            html = html + 'Comments: ' + obj.MessageRequest;
                        }

                        html = html + '    </div>';
                        html = html + '</div>';




                        $("#detail-Appointment-Request").html(html);

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
    }
    else {
       
        var html = '<div id="Inform_Head" class="ui-corner-all">';
        html = html + '<h4 style="padding-top: 10px; margin-left: 30px;">';
        if (obj.ProviderIdFrom !== '0') {
            html = html + '<b>Appointment Request Response</b></h4>';
        }
        else {
            html = html + '<b>Appointment Request</b></h4>';
        }
        //  html = html + '<b>' + obj.MessageType + '</b></h4>';
        html = html + '<div style=\"float: right; margin-top: -30px; margin-right:10px; font-size: 12px; color: #999999;\">';

        var today = new Date();
        var dd = today.getDate();
        var mm = today.getMonth() + 1; //January is 0!

        var yyyy = today.getFullYear();
        if (dd < 10) { dd = '0' + dd } if (mm < 10) { mm = '0' + mm } today = mm + '/' + dd + '/' + yyyy;

        html = html + today;
        html = html + '</div>';
        html = html + '<div style="margin-top: 1px; margin-left: 50px; font-size: 12px; color: #999999;">';
        html = html + 'From:  ' + obj.FacilityName;
        html = html + '</div>';
        html = html + '<div style="margin-top: 1px; margin-left: 50px; font-size: 12px; color: #999999;">';
        html = html + 'To:  ' + obj.PatientName;
        html = html + '</div>';
        html = html + '</div>';
        //<!--End of reply part-->
        html = html + '<div id="reply" style="height: 368px;" class="ui-corner-all">';
        html = html + '<div style="margin-left: 30px;">';
        html = html + '        <br>';
        //html = html + '        <br>';
        //html = html + obj.MessageType;
        //html = html + '                        <br>';
        //html = html + 'Facility Name :' + obj.FacilityName;
        //html = html + '                        <br>';
        if (obj.ProviderIdFrom !== '0') {
            html = html + 'Date :' + obj.AppointmentStart;
            html = html + '                        <br>';
            html = html + 'Time :' + obj.AppointmentStartTime;
            html = html + '                        <br>';
            html = html + 'Message Response: ' + obj.MessageResponse;
            html = html + '                        <br>';


        }

        else {
            var urgent = obj.MessageUrgency;
            if (urgent == "False") {
                html = html + 'Urgent : No';
                html = html + '                        <br>';
            }
            else {
                html = html + 'Urgent : Yes';
                html = html + '                        <br>';

            }
            //html = html + 'Urgent : ' + (obj.MessageUrgency ? "Yes" : "No");
            //html = html + '                        <br>';
            html = html + 'Preferred Date Range : ' + obj.AppointmentStart + ' to ' + obj.AppointmentEnd;
            html = html + '                        <br>';
            //   html = html + 'Message: ' + obj.VisitReason;
            var period = obj.PreferredPeriod;
            var periodval = "";
            if (period[0] == '1') {
                periodval += 'Morning, ';
            }
            if (period[1] == '1') {
                periodval += 'Afternoon, ';
            }
            if (period[2] == '1') {
                periodval += 'Evening ';
            }

            var day = obj.PreferredWeekDay;
            var dayval = "";
            if (day[0] == '1') {
                dayval += 'Monday, ';
            }
            if (day[1] == '1') {
                dayval += 'Tuesday, ';
            }
            if (day[2] == '1') {
                dayval += 'Wednesday, ';
            }
            if (day[3] == '1') {
                dayval += 'Thursday, ';
            }
            if (day[4] == '1') {
                dayval += 'Friday, ';
            }
            if (day[5] == '1') {
                dayval += 'Saturday, ';
            }
            if (day[6] == '1') {
                dayval += 'Sunday ';
            }
            html = html + 'Preferred Day : ' + dayval;
            html = html + '                        <br>';
            html = html + 'Preffered Time of Day : ' + periodval;
            html = html + '                        <br>';
            html = html + 'Preffered Time : ' + obj.PreferredTime;
            html = html + '                        <br>';

            html = html + 'Reason For Visit: ' + obj.VisitReason;
            html = html + '                        <br>';

            html = html + 'Comments: ' + obj.MessageRequest;
        }

        html = html + '    </div>';
        html = html + '</div>';




        $("#detail-Appointment-Request").html(html);

        HideLoader();
    }
    //try {

    //    var requestData = {
    //        MessageId: id,
    //        MessageType: "Appointment Request"
    //    };

    //    $("#hdReplyMessageId").val(id);

    //    $.ajax({
    //        type: 'POST',
    //        url: 'messages-inbox-GridDetail',
    //        data: JSON.stringify(requestData),
    //        dataType: 'json',
    //        contentType: 'application/json; charset=utf-8',
    //        success: function (data) {
    //            // if success
    //            // alert("Success : " + data);

    //            $("#AppintmentDetailGrid").html(data);
    //            var msgdtlApp = $("#hdFirstDetailApp").val();
    //           // alert(msgdtlApp);
    //            messageAppointmentDetail(msgdtlApp, classAppointDetailName);

    //        },
    //        error: function (xhr, ajaxOptions, thrownError) {
    //            //if error

    //            alert('Error : ' + xhr.message);
    //            HideLoader();
    //        },
    //        complete: function (data) {
    //            // if completed
    //           // HideLoader();

    //        },
    //        async: true,
    //        processData: false
    //    });
    //} catch (err) {

    //    if (err && err !== "") {
    //        alert(err.message);
    //        HideLoader();
    //    }
    //}

}
