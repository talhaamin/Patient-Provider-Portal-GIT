function checkAll(obj) {



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
    else if (obj.id.split('_')[1] == "S") {
        checkBoxId = "chkS";
    }

    var inputs = document.getElementsByTagName("input");

    for (var i = 0; i < inputs.length; i++) {

        if (inputs[i].type == "checkbox" && inputs[i].id == checkBoxId) {

            //  alert("Raza");

            if (IsChecked) {
                inputs[i].checked = true;
            }
            else {
                inputs[i].checked = false;
            }
        }
    }
}
var selected = "";
$(document).ready(function () {
    $('#tabs').tabs();
    document.getElementById('tabs').style.display = 'block';
    document.getElementById('composeButton').style.display = 'block';
    
    $("#tbl-messageInbox").fixedHeaderTable();
    var $tabs = $('#tabs').tabs();
    $("#tabs").tabs({
        activate: function (event, ui) {
             selected = $tabs.tabs('option', 'active');

            switch (selected) {
                case 0:
                  //  $("#MessageDetailGrid").hide();
                 //   $("#tbl-MessageGridDetail").hide();
                 //   $("#detail-inbox-request").hide();
                    RenderHtml('34I');
                   // $("#composeButton").show()
                    

                    break;
                case 1:
                   // $("#MessageDetailGrid").hide();
                   // $("#tbl-MessageGridDetail").hide();
                  //  $("#detail-sent-request").hide();
                    RenderHtml('34S');
                    //$("#composeButton").hide();
                    break;
                case 2:

                //    $("#MessageDetailGrid").hide();
                 //   $("#tbl-MessageGridDetail").hide();
                 //   $("#detail-delete-request").hide();
                    RenderHtml('34D');
                   // $("#composeButton").hide();
                    break;
                case 3:
                 //   $("#MessageDetailGrid").hide();
                //    $("#tbl-AppointmentDetailGrid").hide();
                //    $("#detail-Appointment-Request").hide();
                   
                    RenderHtml('Appointment Request');
              //      $("#composeButton").hide();

                    break;
                case 4:
             //       $("#RefillRequestDetailGrid").hide();
             //       $("#detail-refill-request").hide();
                    RenderHtml('Medication Refill');
                //    $("#composeButton").hide();

                    break;
                case 5:

                   // $("#ReferralRequestDetailGrid").hide();
                   // $("#detail-Referral-Request").hide();
                    RenderHtml('Referral Message');
                    //$("#composeButton").hide();
                    break;
            }
        }
    });
});
$(function () {

    $('#browse').click(function () { $('#my-simple-upload').click(); });


});

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

function messageInboxReply(id) {
    $('#hdReplyMessageDetailId').val(id);
    SentMessage('Reply');
}

//function start for inbox detail ajax
function messageInboxDetail(id) {


    //ShowLoader();
    var obj = jQuery.parseJSON($("#hdInboxDetailJson" + id).val());
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
                    $('#MessageDetail' + obj.ID + ' .unread').css('color', '#666699');
                    $('#MessageDetail' + obj.ID + ' .unread').css('font-weight', '');
                    $('#MessageDetail' + obj.ID + ' td div').removeClass('unread');
                    if ($('#MsgDetail').find(".unread").length > 0) {

                    }
                    else {
                        $('#MessageHeader-' + obj.HeaderID + ' td div').css('color', '#666699');
                        $('#MessageHeader-' + obj.HeaderID + ' td div').css('font-weight', '');
                    }
                    $('#circle').html(data.TotalMessages);
                    $('#hdAppointmentMessages').html(data.AppointmentMessages);
                    $('#hdGeneralMessages').html(data.GeneralMessages);
                    $('#hdMedicationMessages').html(data.MedicationMessages);
                    $('#hdReferralMessages').html(data.ReferralMessages);
                    $('#hdTotalMessages').html(data.TotalMessages);



                    $('#GeneralMessages').html("Inbox(<b style='font-size:14px;'>" + data.GeneralMessages + "</b>)");
                    $('#AppointmentMessages').html("Appointment(<b style='font-size:14px;'>" + data.AppointmentMessages + "</b>)");
                    $('#MedicationMessages').html("Medication Refill(<b style='font-size:14px;'>" + data.MedicationMessages + "</b>)");
                    $('#ReferralMessages').html("Referral Requests(<b style='font-size:14px;'>" + data.ReferralMessages + "</b>)");

                    var html = '<div id="Inform_Head" class="ui-corner-all">';
                    html = html + '<h4 style="padding-top: 10px; margin-left: 10px;">';
                    html = html + '<b>' + obj.MessageType + '</b></h4>';
                    html = html + '<div style=\"float: right; margin-top: -30px; margin-right:10px; font-size: 12px; color: #999999;\">';

                    var today = new Date();
                    var dd = today.getDate();
                    var mm = today.getMonth() + 1; //January is 0!

                    var yyyy = today.getFullYear();
                    if (dd < 10) { dd = '0' + dd } if (mm < 10) { mm = '0' + mm } today = mm + '/' + dd + '/' + yyyy;

                    html = html + today;
                    html = html + '</div>';
                    html = html + '<div style="margin-top: 1px; margin-left: 50px; font-size: 12px; color: #999999;">';
                    html = html + 'From:  ' + obj.From;
                    html = html + '</div>';
                    html = html + '<div style="margin-top: 1px; margin-left: 90px; font-size: 12px; color: #999999;">';
                    html = html + 'To:  ' + obj.To;
                    html = html + '</div>';
                    html = html + '</div>';
                    //<!--End of reply part-->
                    html = html + '<div id="reply" style="height: 550px;" class="ui-corner-all">';
                    html = html + '<div style="margin-left: 30px;">';
                    html = html + '        <br>';
                    html = html + '        <br>';
                    html = html + obj.MessageType;
                    html = html + '                        <br>';
                    html = html + 'Facility Name :' + obj.FacilityName;
                    html = html + '                        <br>';
                    if (obj.MessageRequest != '') {
                        html = html + 'Message: ' + obj.MessageRequest;
                    }
                    else {
                        html = html + 'Message: ' + obj.MessageResponse;
                    }
                    html = html + '                        <br>';
                    html = html + '    </div>';
                    html = html + '</div>';



                    $("#detail-inbox-request").show();
                    $("#detail-inbox-request").html(html);
                    $("#detail-sent-request").show();
                    $("#detail-sent-request").html(html);
                    $("#detail-delete-request").show();
                    $("#detail-delete-request").html(html);



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
    else {
        try {



            // alert(obj.FacilityName);
            //  alert($("#hdInboxDetailJson" + id).val());
            var html = '<div id="Inform_Head" class="ui-corner-all">';
            html = html + '<h4 style="padding-top: 10px; margin-left: 10px;">';
            html = html + '<b>' + obj.MessageType + '</b></h4>';
            html = html + '<div style=\"float: right; margin-top: -30px; margin-right:10px; font-size: 12px; color: #999999;\">';

            var today = new Date();
            var dd = today.getDate();
            var mm = today.getMonth() + 1; //January is 0!

            var yyyy = today.getFullYear();
            if (dd < 10) { dd = '0' + dd } if (mm < 10) { mm = '0' + mm } today = mm + '/' + dd + '/' + yyyy;

            html = html + today;
            html = html + '</div>';
            html = html + '<div style="margin-top: 1px; margin-left: 50px; font-size: 12px; color: #999999;">';
            html = html + 'From:  ' + obj.From;
            html = html + '</div>';
            html = html + '<div style="margin-top: 1px; margin-left: 90px; font-size: 12px; color: #999999;">';
            html = html + 'To:  ' + obj.To;
            html = html + '</div>';
            html = html + '</div>';
            //<!--End of reply part-->
            html = html + '<div id="reply" style="height: 550px;" class="ui-corner-all">';
            html = html + '<div style="margin-left: 30px;">';
            html = html + '        <br>';
            html = html + '        <br>';
            html = html + obj.MessageType;
            html = html + '                        <br>';
            html = html + 'Facility Name :' + obj.FacilityName;
            html = html + '                        <br>';
            if (obj.MessageRequest != '') {
                html = html + 'Message: ' + obj.MessageRequest;
            }
            else {
                html = html + 'Message: ' + obj.MessageResponse;
            }
            html = html + '                        <br>';
            html = html + '    </div>';
            html = html + '</div>';



            $("#detail-inbox-request").show();
            $("#detail-inbox-request").html(html);
            $("#detail-sent-request").show();
            $("#detail-sent-request").html(html);
            $("#detail-delete-request").show();
            $("#detail-delete-request").html(html);



            HideLoader();

        }
        catch (err) {

            if (err && err !== "") {
                alert(err.message);
                HideLoader();
            }
        }

    }

}

function messageAppointmentDetail(id) {

    //  alert(id);
    //ShowLoader();
    var obj = jQuery.parseJSON($("#hdAppointmentDetailJson" + id).val());
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
                    $('#MessageDetail' + obj.ID + ' .unread').css('color', '#666699');
                    $('#MessageDetail' + obj.ID + ' .unread').css('font-weight', 'normal');
                    $('#MessageDetail' + obj.ID + ' td div').removeClass('unread');
                    if ($('#MsgDetail').find(".unread").length > 0) {
                        alert('found');
                    }
                    else {

                        $('#MessageHeader-' + obj.HeaderID + ' td').css('color', '#666699');
                        $('#MessageHeader-' + obj.HeaderID + ' td').css('font-weight', 'normal');
                    }

                    $('#circle').html(data.TotalMessages);
                    $('#hdAppointmentMessages').html(data.AppointmentMessages);
                    $('#hdGeneralMessages').html(data.GeneralMessages);
                    $('#hdMedicationMessages').html(data.MedicationMessages);
                    $('#hdReferralMessages').html(data.ReferralMessages);
                    $('#hdTotalMessages').html(data.TotalMessages);

                    $('#GeneralMessages').html("Inbox(<b style='font-size:14px;'>" + data.GeneralMessages + "</b>)");
                    $('#AppointmentMessages').html("Appointment(<b style='font-size:14px;'>" + data.AppointmentMessages + "</b>)");
                    $('#MedicationMessages').html("Medication Refill(<b style='font-size:14px;'>" + data.MedicationMessages + "</b>)");
                    $('#ReferralMessages').html("Referral Requests(<b style='font-size:14px;'>" + data.ReferralMessages + "</b>)");

                    var html = '<div id="Inform_Head" class="ui-corner-all">';
                    html = html + '<h4 style="padding-top: 10px; margin-left: 10px;">';
                    html = html + '<b>' + obj.MessageType + '</b></h4>';
                    html = html + '<div style=\"float: right; margin-top: -30px; margin-right:10px; font-size: 12px; color: #999999;\">';

                    var today = new Date();
                    var dd = today.getDate();
                    var mm = today.getMonth() + 1; //January is 0!

                    var yyyy = today.getFullYear();
                    if (dd < 10) { dd = '0' + dd } if (mm < 10) { mm = '0' + mm } today = mm + '/' + dd + '/' + yyyy;

                    html = html + today;
                    html = html + '</div>';
                    html = html + '<div style="margin-top: 1px; margin-left: 50px; font-size: 12px; color: #999999;">';
                    html = html + 'From:  ' + obj.From;
                    html = html + '</div>';
                    html = html + '<div style="margin-top: 1px; margin-left: 90px; font-size: 12px; color: #999999;">';
                    html = html + 'To:  ' + obj.To;
                    html = html + '</div>';
                    html = html + '</div>';
                    //<!--End of reply part-->
                    html = html + '<div id="reply" style="height: 570px;" class="ui-corner-all">';
                    html = html + '<div style="margin-left: 30px;">';
                    html = html + '        <br>';
                    html = html + '        <br>';
                    html = html + obj.MessageType;
                    html = html + '                        <br>';
                    html = html + 'Facility Name :' + obj.FacilityName;
                    html = html + '                        <br>';
                    if (obj.ProviderIdFrom !== '0') {
                        html = html + 'Message: ' + obj.MessageResponse;
                        html = html + '                        <br>';
                        html = html + 'Preffered Time :' + obj.PreferredTime;
                        html = html + '                        <br>';
                        html = html + 'Appointment Start :' + obj.AppointmentStart;
                        html = html + '                        <br>';
                    }

                    else {
                        html = html + 'Message: ' + obj.VisitReason;
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
                        html = html + '                        <br>';
                        html = html + 'Preffered Period :' + periodval;
                        html = html + '                        <br>';
                        html = html + 'Urgency :' + obj.MessageUrgency;
                        html = html + '                        <br>';
                        html = html + 'Week Day :' + dayval;
                        html = html + '                        <br>';
                        html = html + 'Preffered Time :' + obj.PreferredTime;
                    }

                    html = html + '    </div>';
                    html = html + '</div>';




                    $("#detail-Appointment-Request").html(html);

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
    else {
        try {



            var html = '<div id="Inform_Head" class="ui-corner-all">';
            html = html + '<h4 style="padding-top: 10px; margin-left: 10px;">';
            html = html + '<b>' + obj.MessageType + '</b></h4>';
            html = html + '<div style=\"float: right; margin-top: -30px; margin-right:10px; font-size: 12px; color: #999999;\">';

            var today = new Date();
            var dd = today.getDate();
            var mm = today.getMonth() + 1; //January is 0!

            var yyyy = today.getFullYear();
            if (dd < 10) { dd = '0' + dd } if (mm < 10) { mm = '0' + mm } today = mm + '/' + dd + '/' + yyyy;

            html = html + today;
            html = html + '</div>';
            html = html + '<div style="margin-top: 1px; margin-left: 50px; font-size: 12px; color: #999999;">';
            html = html + 'From:  ' + obj.From;
            html = html + '</div>';
            html = html + '<div style="margin-top: 1px; margin-left: 90px; font-size: 12px; color: #999999;">';
            html = html + 'To:  ' + obj.To;
            html = html + '</div>';
            html = html + '</div>';
            //<!--End of reply part-->
            html = html + '<div id="reply" style="height: 570px;" class="ui-corner-all">';
            html = html + '<div style="margin-left: 30px;">';
            html = html + '        <br>';
            html = html + '        <br>';
            html = html + obj.MessageType;
            html = html + '                        <br>';
            html = html + 'Facility Name :' + obj.FacilityName;
            html = html + '                        <br>';
            if (obj.ProviderIdFrom !== '0') {
                html = html + 'Message: ' + obj.MessageResponse;
                html = html + '                        <br>';
                html = html + 'Preffered Time :' + obj.PreferredTime;
                html = html + '                        <br>';
                html = html + 'Appointment Start :' + obj.AppointmentStart;
                html = html + '                        <br>';
            }

            else {
                html = html + 'Message: ' + obj.VisitReason;
                var period = obj.PreferredPeriod;
                var periodval = "";
                if (period[0] == '1') {
                    periodval = 'Morning, ';
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
                    dayval = 'Monday, ';
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
                    dayval += 'Sunday, ';
                }
                html = html + '                        <br>';
                html = html + 'Preffered Period :' + periodval;
                html = html + '                        <br>';
                html = html + 'Urgency :' + obj.MessageUrgency;
                html = html + '                        <br>';
                html = html + 'Week Day :' + dayval;
                html = html + '                        <br>';
                html = html + 'Preffered Time :' + obj.PreferredTime;
            }

            html = html + '    </div>';
            html = html + '</div>';




            $("#detail-Appointment-Request").html(html);

            HideLoader();

        }
        catch (err) {

            if (err && err !== "") {
                alert(err.message);
                HideLoader();
            }
        }

    }


}

function messageRequestRefillDetail(id) {

   //   alert(id);
   // ShowLoader();
    var obj = jQuery.parseJSON($("#hdRequestRefillDetailJson" + id).val());
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
                    $('#MessageDetail' + obj.ID + ' .unread').css('color', '#666699');
                    $('#MessageDetail' + obj.ID + ' .unread').css('font-weight', '');
                    $('#MessageDetail' + obj.ID + ' td div').removeClass('unread');
                    if ($('#MsgDetail').find(".unread").length > 0) {
                        alert('found');
                    }
                    else {

                        $('#MessageHeader-' + obj.HeaderID + ' td div').css('color', '#666699');
                        $('#MessageHeader-' + obj.HeaderID + ' td div').css('font-weight', '');
                    }

                    $('#circle').html(data.TotalMessages);
                    $('#hdAppointmentMessages').html(data.AppointmentMessages);
                    $('#hdGeneralMessages').html(data.GeneralMessages);
                    $('#hdMedicationMessages').html(data.MedicationMessages);
                    $('#hdReferralMessages').html(data.ReferralMessages);
                    $('#hdTotalMessages').html(data.TotalMessages);

                    $('#GeneralMessages').html("Inbox(<b style='font-size:14px;'>" + data.GeneralMessages + "</b>)");
                    $('#AppointmentMessages').html("Appointment(<b style='font-size:14px;'>" + data.AppointmentMessages + "</b>)");
                    $('#MedicationMessages').html("Medication Refill(<b style='font-size:14px;'>" + data.MedicationMessages + "</b>)");
                    $('#ReferralMessages').html("Referral Requests(<b style='font-size:14px;'>" + data.ReferralMessages + "</b>)");

                    var html = '<div id="Inform_Head" class="ui-corner-all">';
                    html = html + '<h4 style="padding-top: 10px; margin-left: 10px;">';
                    html = html + '<b>' + obj.MessageType + '</b></h4>';
                    html = html + '<div style=\"float: right; margin-top: -30px; margin-right:10px; font-size: 12px; color: #999999;\">';

                    var today = new Date();
                    var dd = today.getDate();
                    var mm = today.getMonth() + 1; //January is 0!

                    var yyyy = today.getFullYear();
                    if (dd < 10) { dd = '0' + dd } if (mm < 10) { mm = '0' + mm } today = mm + '/' + dd + '/' + yyyy;

                    html = html + today;
                    html = html + '</div>';
                    html = html + '<div style="margin-top: 1px; margin-left: 50px; font-size: 12px; color: #999999;">';
                    html = html + 'From:  ' + obj.From;
                    html = html + '</div>';
                    html = html + '<div style="margin-top: 1px; margin-left: 90px; font-size: 12px; color: #999999;">';
                    html = html + 'To:  ' + obj.To;
                    html = html + '</div>';
                    html = html + '</div>';
                    //<!--End of reply part-->
                    //<!--End of reply part-->
                    html = html + '<div id=\"reply\" style=\"height: 625px;\" class=\"ui-corner-all\">';
                    html = html + '<div style=\"margin-left: 30px;\">';
                    html = html + '        <br>';
                    html = html + '        <br>';
                    html = html + 'Medication Refill Request';
                    html = html + '                        <br>';
                    html = html + 'Patient :' + obj.CreatedByName;
                    html = html + '                       <br>';
                    html = html + 'Medication Name :' + obj.MedicationName;
                    html = html + '                        <br>';
                    html = html + 'Provider :  ' + obj.To;
                    html = html + '                        <br>';
                    html = html + 'Pharmacy Name :' + obj.PharmacyName;
                    html = html + '                        <br>';
                    html = html + 'Pharmacy Address :' + obj.PharmacyAddress;
                    html = html + '                        <br>';
                    html = html + 'No. Of Refills :' + obj.NoOfRefills;
                    html = html + '                        <br>';
                    html = html + 'Message Request: ' + obj.MessageRequest;
                    html = html + '                        <br>';
                    html = html + '                        <br>';

                    //  html = html + ' Response                       <br>';
                    //  html = html + 'Message Response: " + obj.MessageResponse + " ';
                    html = html + '   </div>';
                    html = html + '</div>';


                    $("#detail-refill-request").html(html);

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
    else {
        try {

            var html = '<div id="Inform_Head" class="ui-corner-all">';
            html = html + '<h4 style="padding-top: 10px; margin-left: 10px;">';
            html = html + '<b>' + obj.MessageType + '</b></h4>';
            html = html + '<div style=\"float: right; margin-top: -30px; margin-right:10px; font-size: 12px; color: #999999;\">';

            var today = new Date();
            var dd = today.getDate();
            var mm = today.getMonth() + 1; //January is 0!

            var yyyy = today.getFullYear();
            if (dd < 10) { dd = '0' + dd } if (mm < 10) { mm = '0' + mm } today = mm + '/' + dd + '/' + yyyy;

            html = html + today;
            html = html + '</div>';
            html = html + '<div style="margin-top: 1px; margin-left: 50px; font-size: 12px; color: #999999;">';
            html = html + 'From:  ' + obj.From;
            html = html + '</div>';
            html = html + '<div style="margin-top: 1px; margin-left: 90px; font-size: 12px; color: #999999;">';
            html = html + 'To:  ' + obj.To;
            html = html + '</div>';
            html = html + '</div>';
            //<!--End of reply part-->
            //<!--End of reply part-->
            html = html + '<div id=\"reply\" style=\"height: 625px;\" class=\"ui-corner-all\">';
            html = html + '<div style=\"margin-left: 30px;\">';
            html = html + '        <br>';
            html = html + '        <br>';
            html = html + 'Medication Refill Request';
            html = html + '                        <br>';
            html = html + 'Patient :' + obj.CreatedByName;
            html = html + '                       <br>';
            html = html + 'Medication Name :' + obj.MedicationName;
            html = html + '                        <br>';
            html = html + 'Provider :  ' + obj.To;
            html = html + '                        <br>';
            html = html + 'Pharmacy Name :' + obj.PharmacyName;
            html = html + '                        <br>';
            html = html + 'Pharmacy Address :' + obj.PharmacyAddress;
            html = html + '                        <br>';
            html = html + 'No. Of Refills :' + obj.NoOfRefills;
            html = html + '                        <br>';
            html = html + 'Message Request: ' + obj.MessageRequest;
            html = html + '                        <br>';
            html = html + '                        <br>';

            //  html = html + ' Response                       <br>';
            //  html = html + 'Message Response: " + obj.MessageResponse + " ';
            html = html + '   </div>';
            html = html + '</div>';


            $("#detail-refill-request").html(html);

            HideLoader();

        }
        catch (err) {

            if (err && err !== "") {
                alert(err.message);
                HideLoader();
            }
        }
    }


}

function messageReferralRequestDetail(id) {

     // alert(id);
    //ShowLoader();
    var obj = jQuery.parseJSON($("#hdReferralRequestDetailJson" + id).val());
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
                    $('#MessageDetail' + obj.ID + ' .unread').css('color', '#666699');
                    $('#MessageDetail' + obj.ID + ' .unread').css('font-weight', 'normal');
                    $('#MessageDetail' + obj.ID + ' td div').removeClass('unread');
                    if ($('#MsgDetail').find(".unread").length > 0) {
                        // alert('found');
                    }
                    else {

                        $('#MessageHeader-' + obj.HeaderID + ' td div').css('color', '#666699');
                        $('#MessageHeader-' + obj.HeaderID + ' td div').css('font-weight', 'normal');
                    }
                    $('#circle').html(data.TotalMessages);
                    $('#hdAppointmentMessages').html(data.AppointmentMessages);
                    $('#hdGeneralMessages').html(data.GeneralMessages);
                    $('#hdMedicationMessages').html(data.MedicationMessages);
                    $('#hdReferralMessages').html(data.ReferralMessages);
                    $('#hdTotalMessages').html(data.TotalMessages);

                    $('#GeneralMessages').html("Inbox(<b style='font-size:14px;'>" + data.GeneralMessages + "</b>)");
                    $('#AppointmentMessages').html("Appointment(<b style='font-size:14px;'>" + data.AppointmentMessages + "</b>)");
                    $('#MedicationMessages').html("Medication Refill(<b style='font-size:14px;'>" + data.MedicationMessages + "</b>)");
                    $('#ReferralMessages').html("Referral Requests(<b style='font-size:14px;'>" + data.ReferralMessages + "</b>)");

                    var html = '<div id="Inform_Head" class="ui-corner-all">';
                    html = html + '<h4 style="padding-top: 10px; margin-left: 10px;">';
                    html = html + '<b>' + obj.MessageType + '</b></h4>';
                    html = html + '<div style=\"float: right; margin-top: -30px; margin-right:10px; font-size: 12px; color: #999999;\">';

                    var today = new Date();
                    var dd = today.getDate();
                    var mm = today.getMonth() + 1; //January is 0!

                    var yyyy = today.getFullYear();
                    if (dd < 10) { dd = '0' + dd } if (mm < 10) { mm = '0' + mm } today = mm + '/' + dd + '/' + yyyy;

                    html = html + today;
                    html = html + '</div>';
                    html = html + '<div style="margin-top: 1px; margin-left: 50px; font-size: 12px; color: #999999;">';
                    html = html + 'From:  ' + obj.From;
                    html = html + '</div>';
                    html = html + '<div style="margin-top: 1px; margin-left: 90px; font-size: 12px; color: #999999;">';
                    html = html + 'To:  ' + obj.To;
                    html = html + '</div>';
                    html = html + '</div>';
                    //<!--End of reply part-->
                    html = html + '<div id="reply" style="height: 570px;" class="ui-corner-all">';
                    html = html + '<div style="margin-left: 30px;">';
                    html = html + '        <br>';
                    html = html + '        <br>';
                    html = html + obj.MessageType;
                    html = html + '                        <br>';
                    html = html + 'Facility Name :' + obj.FacilityName;
                    html = html + '                        <br>';
                    if (obj.ProviderIdFrom !== '0') {
                        html = html + 'Message: ' + obj.MessageResponse;
                        html = html + '                        <br>';
                        html = html + 'Preffered Time :' + obj.PreferredTime;
                        html = html + '                        <br>';
                        html = html + 'Appointment Start :' + obj.AppointmentStart;
                        html = html + '                        <br>';
                    }

                    else {
                        html = html + 'Message: ' + obj.VisitReason;
                        var period = obj.PreferredPeriod;
                        var periodval = "";
                        if (period[0] == '1') {
                            periodval = 'Morning, ';
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
                            dayval = 'Monday, ';
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
                        html = html + '                        <br>';
                        html = html + 'Preffered Period :' + periodval;
                        html = html + '                        <br>';
                        html = html + 'Urgency :' + obj.MessageUrgency;
                        html = html + '                        <br>';
                        html = html + 'Week Day :' + dayval;
                        html = html + '                        <br>';
                        html = html + 'Preffered Time :' + obj.PreferredTime;
                    }

                    html = html + '    </div>';
                    html = html + '</div>';



                    $("#detail-Referral-Request").html(html);

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
    else {
        try {

            var html = '<div id="Inform_Head" class="ui-corner-all">';
            html = html + '<h4 style="padding-top: 10px; margin-left: 10px;">';
            html = html + '<b>' + obj.MessageType + '</b></h4>';
            html = html + '<div style=\"float: right; margin-top: -30px; margin-right:10px; font-size: 12px; color: #999999;\">';

            var today = new Date();
            var dd = today.getDate();
            var mm = today.getMonth() + 1; //January is 0!

            var yyyy = today.getFullYear();
            if (dd < 10) { dd = '0' + dd } if (mm < 10) { mm = '0' + mm } today = mm + '/' + dd + '/' + yyyy;

            html = html + today;
            html = html + '</div>';
            html = html + '<div style="margin-top: 1px; margin-left: 50px; font-size: 12px; color: #999999;">';
            html = html + 'From:  ' + obj.From;
            html = html + '</div>';
            html = html + '<div style="margin-top: 1px; margin-left: 90px; font-size: 12px; color: #999999;">';
            html = html + 'To:  ' + obj.To;
            html = html + '</div>';
            html = html + '</div>';
            //<!--End of reply part-->
            html = html + '<div id="reply" style="height: 570px;" class="ui-corner-all">';
            html = html + '<div style="margin-left: 30px;">';
            html = html + '        <br>';
            html = html + '        <br>';
            html = html + obj.MessageType;
            html = html + '                        <br>';
            html = html + 'Facility Name :' + obj.FacilityName;
            html = html + '                        <br>';
            if (obj.ProviderIdFrom !== '0') {
                html = html + 'Message: ' + obj.MessageResponse;
                html = html + '                        <br>';
                html = html + 'Preffered Time :' + obj.PreferredTime;
                html = html + '                        <br>';
                html = html + 'Appointment Start :' + obj.AppointmentStart;
                html = html + '                        <br>';
            }

            else {
                html = html + 'Message: ' + obj.VisitReason;
                var period = obj.PreferredPeriod;
                var periodval = "";
                if (period[0] == '1') {
                    periodval = 'Morning, ';
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
                    dayval = 'Monday, ';
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
                    dayval += 'Sunday, ';
                }
                html = html + '                        <br>';
                html = html + 'Preffered Period :' + periodval;
                html = html + '                        <br>';
                html = html + 'Urgency :' + obj.MessageUrgency;
                html = html + '                        <br>';
                html = html + 'Week Day :' + dayval;
                html = html + '                        <br>';
                html = html + 'Preffered Time :' + obj.PreferredTime;
            }

            html = html + '    </div>';
            html = html + '</div>';



            $("#detail-Referral-Request").html(html);

            HideLoader();

        }
        catch (err) {

            if (err && err !== "") {
                alert(err.message);
                HideLoader();
            }
        }
    }


}

function inboxDetail(id, value) {

    ShowLoader();

    try {

        var requestData = {
            MessageId: id

        };

        $("#hdReplyMessageId").val(id);

        $.ajax({
            type: 'POST',
            url: 'messages-inbox-GridDetail',
            data: JSON.stringify(requestData),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                // if success
                
                if (value == 'I') {
                    
                    $("#MessageDetailGrid").html(data);
                    var msgdtlInbox = $("#hdFirstDetailInbox").val();
                    //alert(msgdtl);
                    
                    messageInboxDetail(msgdtlInbox);
                    //$("#detail-inbox-request").hide();
                }
                else if (value == 'S') {
                    $("#MessageSentDetailGrid").html(data);
                    var msgdtlSent = $("#hdFirstDetailInbox").val();
                    messageInboxDetail(msgdtlSent);
                   // $("#detail-sent-request").hide();
                }
                else if (value == 'D') {
                    $("#MessageDeleteDetailGrid").html(data);
                    var msgdtlDel = $("#hdFirstDetailInbox").val();
                    messageInboxDetail(msgdtlDel);
                    //$("#detail-delete-request").hide();
                }

            },
            error: function (xhr, ajaxOptions, thrownError) {
                //if error

                alert('Error : ' + xhr.message);
                HideLoader();
            },
            complete: function (data) {
                // if completed
               // HideLoader();

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

function AppointmentDetail(id) {
    // alert(id);
    ShowLoader();

    try {

        var requestData = {
            MessageId: id,
            MessageType: "Appointment Request"
        };

        $("#hdReplyMessageId").val(id);

        $.ajax({
            type: 'POST',
            url: 'messages-inbox-GridDetail',
            data: JSON.stringify(requestData),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                // if success
                // alert("Success : " + data);
                
                $("#AppintmentDetailGrid").html(data);
                var msgdtlApp = $("#hdFirstDetailApp").val();
               // alert(msgdtlApp);
                messageAppointmentDetail(msgdtlApp);

            },
            error: function (xhr, ajaxOptions, thrownError) {
                //if error

                alert('Error : ' + xhr.message);
                HideLoader();
            },
            complete: function (data) {
                // if completed
               // HideLoader();

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

function sentDetail(id) {

    ShowLoader();

    try {

        var requestData = {
            MessageId: id

        };


        $.ajax({
            type: 'POST',
            url: 'messages-sent-Detail',
            data: JSON.stringify(requestData),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                // if success
                // alert("Success : " + data);

                $("#detail-sent-request").html(data);

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

function medicationRefillDetail(id) {
    ShowLoader();

    try {

        var requestData = {
            MessageId: id,
            MessageType: "Medication Refill"
        };

        $("#hdReplyMessageId").val(id);

        $.ajax({
            type: 'POST',
            url: 'messages-inbox-GridDetail',
            data: JSON.stringify(requestData),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                // if success
                // alert("Success : " + data);
                
                $("#RefillRequestDetailGrid").html(data);
                var msgdtlMed = $("#hdFirstDetailMed").val();
               // alert(msgdtlMed);
                messageRequestRefillDetail(msgdtlMed);
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
}

function ReferralRequestDetail(id) {
    ShowLoader();
    // alert('test');
    try {

        var requestData = {
            MessageId: id,
            MessageType: "Referral Request"
        };

        $("#hdReplyMessageId").val(id);

        $.ajax({
            type: 'POST',
            url: 'messages-inbox-GridDetail',
            data: JSON.stringify(requestData),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                // if success
                // alert("Success : " + data);

                $("#ReferralRequestDetailGrid").html(data);
                var msgdtlRef = $("#hdFirstDetailReferral").val();
                messageReferralRequestDetail(msgdtlRef);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                //if error

                alert('Error : ' + xhr.message);
                HideLoader();
            },
            complete: function (data) {
                // if completed
               // HideLoader();

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

function AppointmentRequestDetail(id) {
    ShowLoader();

    try {
        var bValid = true;

        // bValid = false;
        var requestData = {
            MessageId: id
        };

        if (bValid) {
            $.ajax({
                type: 'POST',
                url: 'message-Appointment-Request-detail',
                data: JSON.stringify(requestData),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    // if success
                    // alert("Success : " + data);
                    HideLoader();
                    $("#detail-Appointment-Request").html(data);

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
    } catch (err) {

        if (err && err !== "") {
            alert(err.message);
            HideLoader();
        }
    }

}

function RenderHtml(value) {

    ShowLoader();

    try {
        var bValid = true;

        // bValid = false;
        var requestData = {
            MessageType: value
        };

        if (bValid) {
            $.ajax({
                type: 'POST',
                url: 'message-tabs-data',
                data: JSON.stringify(requestData),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                success: function (data) {


                    $('#circle').html(data.TotalMessages);
                    $('#hdAppointmentMessages').html(data.AppointmentMessages);
                    $('#hdGeneralMessages').html(data.GeneralMessages);
                    $('#hdMedicationMessages').html(data.MedicationMessages);
                    $('#hdReferralMessages').html(data.ReferralMessages);
                    $('#hdTotalMessages').html(data.TotalMessages);

                    $('#GeneralMessages').html("Inbox(<b style='font-size:14px;'>" + data.GeneralMessages + "</b>)");
                    $('#AppointmentMessages').html("Appointment(<b style='font-size:14px;'>" + data.AppointmentMessages + "</b>)");
                    $('#MedicationMessages').html("Medication Refill(<b style='font-size:14px;'>" + data.MedicationMessages + "</b>)");
                    $('#ReferralMessages').html("Referral Requests(<b style='font-size:14px;'>" + data.ReferralMessages + "</b>)");
                    if (value == 'Referral Message') {
                        $('#tabs-6 div:first').html(data.Msghtml)
                        $("#tbl-MessageReferral").fixedHeaderTable();

                    }
                    else if (value == 'Appointment Request') {

                        $('#tabs-4 div:first').html(data.Msghtml)
                        $("#tbl-MessageAppointment").fixedHeaderTable();
                    }
                    else if (value == 'Medication Refill') {
                        $('#tabs-5 div:first').html(data.Msghtml)
                        $("#tbl-MessageRefill").fixedHeaderTable();
                    }
                    else if (value == '34I') {
                        $('#tabs-1 div:first').html(data.Msghtml)
                        $("#tbl-messageInbox").fixedHeaderTable();
                        $("#detail-inbox-request").hide();
                    }
                    else if (value == '34S') {
                        $('#tabs-2 div:first').html(data.Msghtml)
                        $("#tbl-MessageSent").fixedHeaderTable();
                        $("#detail-sent-request").hide();
                    }
                    else if (value == '34D') {
                        $('#tabs-3 div:first').html(data.Msghtml)
                        $("#tbl-MessageDelete").fixedHeaderTable();
                        $("#detail-delete-request").hide();
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

var requestImageData;
//$.trim($('#txtMessageWrite').val())
function initAttachFileUpload() {
    'use strict';
    // my - simple - upload
    $('#my-simple-upload').fileupload({
        url: 'messages-compose',
        dataType: 'json',
        // formData: { FacilityId: $('#hdFacilityId').val() },
        add: function (e, data) {

            jqXHRData = data
            $.each(data.files, function (index, file) {
                //alert(file.name);
                $('#BrowseName').text(file.name);
                $('#btncancelupload').show();
            });
        },
        done: function (event, data) {

            $('#MessageHeaderGrid').html(data.result.Msghtml);
            //if (data.result.isUploaded) {

            //}
            // else {

            // }
            //alert(data.result.message);
            HideLoader();
        },
        submit: function (e, data) {
            var $this = $(this);
            // $.getJSON('/example/url', function (result) {
            // alert($('#hdFacilityId').val());
            data.formData = { FacilityId: $('#hdFacilityId').val(), ProviderId_To: $('#hdProviderId_To').val(), MessageRequest: $('#hdMessageRequest').val(), MessageTypeId: $('#hdMessageTypeId').val(), MessageUrgencyId: $('#hdMessageUrgencyId').val(), Flag: $('#hdFlag').val() }; // e.g. {id: 123}
            $this.fileupload('send', data);
            //});
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
$(document).ready(function () {

    initAttachFileUpload();

    $("#ComposeMessage").dialog
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
            height: 636,
            width: 727,
            modal: true,
            buttons: {
                "Send": function () {

                    var bValid = true;
                    //var totbyte = Math.round($('#FileBytes').val() / 1000000);
                    //if (totbyte > 1)
                    //{
                    //    alert('Maximum Upload limit is 1mb');
                    //    bValid = false
                    //}
                    //if ($('#ByteData').val() == null || $('#ByteData').val() == '')
                    //{
                    //    bValid = false
                    //    alert('Please upload file and then proceed');
                    //}

                    if (bValid) {

                        //  alert($('#txtAttachment').val())

                        //ajax 
                        try {

                            if (($('#cmbAppointmentFacility option:selected').val()) == -1 && ($('#cmbAppointmentDoctor option:selected').val()) == -1 && ($('#cmbMessageType option:selected').val()) == -1 && ($('#cmbMessageUrgency option:selected').val()) == -1) {
                                alert("Please Select 'Location , To , Subject , and Message Priority' option");
                                return false;
                            }
                            else if (($('#cmbAppointmentDoctor option:selected').val()) == -1 && ($('#cmbMessageType option:selected').val()) == -1 && ($('#cmbMessageUrgency option:selected').val()) == -1) {
                                alert("Please Select ' To , Subject , Message Priority' option");
                                return false;
                            }
                            else if (($('#cmbAppointmentFacility option:selected').val()) == -1 && ($('#cmbMessageType option:selected').val()) == -1 && ($('#cmbMessageUrgency option:selected').val()) == -1) {
                                alert("Please Select ' Location , Subject , Message Priority' option");
                                return false;
                            }
                            else if (($('#cmbAppointmentFacility option:selected').val()) == -1 && ($('#cmbAppointmentDoctor option:selected').val()) == -1 && ($('#cmbMessageUrgency option:selected').val()) == -1) {
                                alert("Please Select 'Location , To , Message Priority' option");
                                return false;
                            }

                            else if (($('#cmbAppointmentFacility option:selected').val()) == -1 && ($('#cmbAppointmentDoctor option:selected').val()) == -1 && ($('#cmbMessageType option:selected').val()) == -1) {
                                alert("Please Select 'Location , To , Subject' option");
                                return false;
                            }
                            else if (($('#cmbAppointmentFacility option:selected').val()) == -1 && ($('#cmbAppointmentDoctor option:selected').val()) == -1) {
                                alert("Please Select 'Location , To ' option");
                                return false;
                            }
                            else if (($('#cmbMessageType option:selected').val()) == -1 && ($('#cmbMessageUrgency option:selected').val()) == -1) {
                                alert("Please Select 'Subject , Message Priority ' option");
                                return false;
                            }
                            else if (($('#cmbAppointmentDoctor option:selected').val()) == -1 && ($('#cmbMessageType option:selected').val()) == -1) {
                                alert("Please Select 'To , Subject ' option");
                                return false;
                            }
                            else if (($('#cmbAppointmentDoctor option:selected').val()) == -1 && ($('#cmbMessageUrgency option:selected').val()) == -1) {
                                alert("Please Select 'To , Message Priority ' option");
                                return false;
                            }
                            else if (($('#cmbAppointmentFacility option:selected').val()) == -1 && ($('#cmbMessageType option:selected').val()) == -1) {
                                alert("Please Select 'Location , Subject ' option");
                                return false;
                            }
                            else if (($('#cmbAppointmentFacility option:selected').val()) == -1) {
                                alert("Please Select 'Location' option");
                                return false;
                            }
                            else if (($('#cmbAppointmentDoctor option:selected').val()) == -1) {
                                alert("Please Select 'To' option");
                                return false;
                            }
                            else if (($('#cmbMessageType option:selected').val()) == -1) {
                                alert("Please Select 'Subject' option");
                                return false;
                            }
                            else if (($('#cmbMessageUrgency option:selected').val()) == -1) {
                                alert("Please Select 'Message Priority' option");
                                return false;
                            }
                            if ($('#hdSentFlag').val() == 'Reply') {

                                var textMessage = $.trim($('#txtMessageWrite').val());

                                var txt = textMessage.replace(/\n/g, "\\n");
                                var requestData = {
                                    MessageId: $.trim($('#hdReplyMessageId').val()),
                                    FacilityId: $.trim($('#cmbAppointmentFacility  option:selected').val()),
                                    ProviderId_To: $.trim($('#cmbAppointmentDoctor  option:selected').val()),
                                    MessageRequest: txt,
                                    MessageTypeId: $.trim($('#cmbMessageType  option:selected').val()),
                                    MessageUrgencyId: $.trim($('#cmbMessageUrgency  option:selected').val()),
                                    Flag: "message-reply"

                                };


                                $.ajax({
                                    type: 'POST',
                                    url: 'messages-reply',
                                    data: JSON.stringify(requestData),
                                    dataType: 'json',
                                    contentType: 'application/json; charset=utf-8',
                                    success: function (data) {
                                        // if success
                                        // alert("Success : " + data);
                                        $("#MessageDetailGrid").html(data);


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
                            }
                            else if ($('#hdSentFlag').val() == 'Compose') {


                                var textMessage = $.trim($('#txtMessageWrite').val());

                                var txt = textMessage.replace(/\n/g, "\\n");

                                var requestData = {

                                    AttachmentName: $.trim($('#files').val()),
                                    AttachmentTest: $('#ByteData').val(),
                                    FacilityId: $.trim($('#cmbAppointmentFacility  option:selected').val()),
                                    ProviderId_To: $.trim($('#cmbAppointmentDoctor  option:selected').val()),
                                    MessageRequest: txt,
                                    MessageTypeId: $.trim($('#cmbMessageType  option:selected').val()),
                                    MessageUrgencyId: $.trim($('#cmbMessageUrgency  option:selected').val()),
                                    Flag: "message-compose"

                                };

                                $('#hdFacilityId').val($.trim($('#cmbAppointmentFacility  option:selected').val()));
                                $('#hdProviderId_To').val($.trim($('#cmbAppointmentDoctor  option:selected').val()));
                                $('#hdMessageRequest').val(txt);
                                $('#hdMessageTypeId').val($.trim($('#cmbMessageType  option:selected').val()));
                                $('#hdMessageUrgencyId').val($.trim($('#cmbMessageUrgency  option:selected').val()));
                                $('#hdFlag').val('message-compose');
                                //alert(JSON.stringify(requestImageData));
                                //requestData = $.trim($('#cmbMessageType  option:selected').val());
                                //alert(requestData);
                                if ($('#BrowseName').text() !== '') {
                                    if (jqXHRData) {

                                        var isStartUpload = true;
                                        var uploadFile = jqXHRData.files[0];

                                        if (!(/\.(jpg|jpeg|gif|png|doc|docx|xls|xlsx|pdf)$/i).test(uploadFile.name)) {
                                            alert('You must select an jpg and pdf file only');
                                            isStartUpload = false;
                                            return false;
                                        } else if (uploadFile.size > 4000000) { // 4mb
                                            alert('Please upload a smaller image, max size is 4 MB');
                                            isStartUpload = false;
                                            return false;
                                        }
                                        if (isStartUpload) {
                                            jqXHRData.submit();
                                        }
                                    }
                                        //if (jqXHRData) {
                                        //    jqXHRData.submit();
                                        //}
                                    else {
                                        return false;
                                    }
                                }
                                else {
                                    $.ajax({
                                        type: 'POST',
                                        url: 'messages-compose',
                                        data: JSON.stringify(requestData),
                                        dataType: 'json',
                                        contentType: 'application/json; charset=utf-8',
                                        success: function (data) {
                                            // if success
                                            // alert("Success : " + data);
                                            $("#MessageHeaderGrid").html(data);
                                            alert("Message Sent Successfully");
                                            $("#tbl-messageInbox").fixedHeaderTable();
                                           

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
                                }
                            }
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


});

function SentMessage(type) {
    $('#hdSentFlag').val(type);
    if (type == 'Compose') {
        clearFields();
        $("#cmbAppointmentFacility").attr('disabled', false);
        $("#cmbAppointmentDoctor ").attr('disabled', false);
        $("#cmbMessageType ").attr('disabled', false);
        $("#cmbMessageUrgency").attr('disabled', false);


    }
    if (type == 'Reply') {
        if ($('#hdReplyMessageDetailId').val() == '') {
            alert('Please select message before sending a reply.');
            return;
        }
        else {
            var mDetailId = $('#hdReplyMessageDetailId').val();


            try {

                var obj = jQuery.parseJSON($("#hdInboxDetailJson" + mDetailId).val());
                $("#cmbAppointmentFacility").val(obj.FacilityId);
                $("#cmbAppointmentFacility ").attr('disabled', 'disabled');
                $("#cmbAppointmentDoctor").val(obj.ProviderIdFrom);
                $("#cmbAppointmentDoctor ").attr('disabled', 'disabled');
                $("#cmbMessageType").val(obj.MessageTypeId);
                $("#cmbMessageType ").attr('disabled', 'disabled');
                $("#cmbMessageUrgency").val(obj.MessageUrgencyId);
                $("#cmbMessageUrgency").attr('disabled', 'disabled');
                //  $('#ComposeMessage').attr("title", "Reply Message");


            }
            catch (err) {

                if (err && err !== "") {
                    alert(err.message);
                    HideLoader();
                }
            }
        }
    }
    else {
        $('#hdReplyMessageDetailId').val('');
    }
    $("#txtMessageWrite").val('');
    clearAttach();
    //$('#btncancelupload').hide();
    //  document.getElementById('files').addEventListener('change', handleFileSelect, false);
    $('#browse').val('Browse..');
    $('#BrowseName').text('');
    if (selected == 0) {
        $("#ComposeMessage").dialog("open");
    }
    else if (selected == "3")
    {
        $("#appbtn-form").dialog("open");
    }

    else if (selected == "4")
    {
        $("#dialog-refill").dialog("open");
    }

    else if (selected == "5")
    {
        $("#reqbtn-form").dialog("open");
    }
    $('#files').hide();
}
// for appointment
$(function ()
{
    var Location = $("#cmbAppointmentFacility1"),
         Provider = $("#cmbAppointmentDoctor1"),
       Urgency = $("#cmbMessageUrgency1"),
       PrefferedTime = $("#cmbPreftime"),

      allFields = $([]).add(Location).add(Provider).add(Urgency).add(PrefferedTime),

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
    $("#appbtn-form").dialog({
        autoOpen: false,
        show: {
            effect: "blind",
            duration: 1000
        },
        hide: {
            effect: "blind",
            duration: 1000
        },

        width: 850,
        modal: true,
        buttons: {
            "Save": function () {
                var bValid = true;
                allFields.removeClass("ui-state-error");
                //bValid = bValid && checkLength(Location, "Please Select Location");
                //bvalid = bvalid && checkLength(Provider, "Please Select Provider");
                //bValid = bValid && checkLength(Urgency, "Please Select Urgency");
                //bvalid = bvalid && checkLength(PrefferedTime, "Please Select Preffered Time");



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



                var PreferredPeriod1 = chkStr($('#chkmor').prop('checked'));
                PreferredPeriod1 += chkStr($('#chkAft').prop('checked'));
                PreferredPeriod1 += chkStr($('#chkeve').prop('checked'));

                var PreferredWeekDay1 = chkStr($('#chkmon').prop('checked'));
                PreferredWeekDay1 += chkStr($('#chktue').prop('checked'));
                PreferredWeekDay1 += chkStr($('#chkwed').prop('checked'));
                PreferredWeekDay1 += chkStr($('#chkthu').prop('checked'));
                PreferredWeekDay1 += chkStr($('#chkfri').prop('checked'));
                PreferredWeekDay1 += chkStr($('#chksat').prop('checked'));
                PreferredWeekDay1 += chkStr($('#chksun').prop('checked'));
                //alert(PreferredWeekDay1);
                //bValid = true;

                if (bValid) {
                    //ajax 
                    try {
                        if (($('#cmbAppointmentFacility1 option:selected').val()) == -1 && ($('#cmbAppointmentDoctor1 option:selected').val()) == -1 && ($('#cmbMessageUrgency1 option:selected').val()) == -1 && ($('#cmbPreftime option:selected').val()) == -1) {
                            alert("Please Select 'Location , Provider , urgency , PreferredTime' option");
                            return false;
                        }
                        else if (($('#cmbAppointmentDoctor1 option:selected').val()) == -1 && ($('#cmbMessageUrgency1 option:selected').val()) == -1 && ($('#cmbPreftime option:selected').val()) == -1) {
                            alert("Please Select 'Provider , urgency , PreferredTime' option");
                            return false;
                        }
                        else if (($('#cmbAppointmentFacility1 option:selected').val()) == -1 && ($('#cmbMessageUrgency1 option:selected').val()) == -1 && ($('#cmbPreftime option:selected').val()) == -1) {
                            alert("Please Select 'Location , urgency , PreferredTime' option");
                            return false;
                        }
                        else if (($('#cmbAppointmentFacility1 option:selected').val()) == -1 && ($('#cmbAppointmentDoctor1 option:selected').val()) == -1 && ($('#cmbPreftime option:selected').val()) == -1) {
                            alert("Please Select 'Location , Provider , PreferredTime' option");
                            return false;
                        }
                        else if (($('#cmbAppointmentFacility1 option:selected').val()) == -1 && ($('#cmbAppointmentDoctor1 option:selected').val()) == -1 && ($('#cmbMessageUrgency1 option:selected').val()) == -1) {
                            alert("Please Select 'Location , Provider , Urgency' option");
                            return false;
                        }
                        else if (($('#cmbAppointmentFacility1 option:selected').val()) == -1 && ($('#cmbAppointmentDoctor1 option:selected').val()) == -1) {
                            alert("Please Select 'Location , Provider ' option");
                            return false;
                        }
                        else if (($('#cmbMessageUrgency1 option:selected').val()) == -1 && ($('#cmbPreftime option:selected').val()) == -1) {
                            alert("Please Select 'Urgency , PrferredTime ' option");
                            return false;
                        }
                        else if (($('#cmbAppointmentDoctor1 option:selected').val()) == -1 && ($('#cmbMessageUrgency1 option:selected').val()) == -1) {
                            alert("Please Select 'Provider , Urgency ' option");
                            return false;
                        }
                        else if (($('#cmbAppointmentFacility1 option:selected').val()) == -1 && ($('#cmbMessageUrgency1 option:selected').val()) == -1) {
                            alert("Please Select 'Location , Urgency ' option");
                            return false;
                        }
                        else if (($('#cmbAppointmentDoctor1 option:selected').val()) == -1 && ($('#cmbPreftime option:selected').val()) == -1) {
                            alert("Please Select 'Provider , PrferredTime ' option");
                            return false;
                        }
                        else if (($('#cmbAppointmentFacility1 option:selected').val()) == -1) {
                            alert("Please Select 'Location ' option");
                            return false;
                        }
                        else if (($('#cmbAppointmentDoctor1 option:selected').val()) == -1) {
                            alert("Please Select 'Provider ' option");
                            return false;
                        }
                        else if (($('#cmbMessageUrgency1 option:selected').val()) == -1) {
                            alert("Please Select ' Urgency ' option");
                            return false;
                        }
                        else if (($('#cmbPreftime option:selected').val()) == -1) {
                            alert("Please Select ' PrferredTime ' option");
                            return false;
                        }
                        var textComment = $.trim($('#txtComment').val());
                        if (($('#txtPrefDateFrom').val()) == "" && ($('#txtPrefDateTo').val()) == "") {
                            $('#txtPrefDateFrom').val('01/01/1900');
                            $('#txtPrefDateTo').val('01/01/1900');
                        }
                        else if (($('#txtPrefDateFrom').val()) == "") {
                            $('#txtPrefDateFrom').val('01/01/1900');
                        }

                        else if (($('#txtPrefDateTo').val()) == "") {
                            $('#txtPrefDateTo').val('01/01/1900');
                        }
                        var txt = textComment.replace(/\n/g, "\\n");
                        var requestData = {
                            FacilityId: $.trim($('#cmbAppointmentFacility1 option:selected').val()),//
                            ProviderId_To: $.trim($('#cmbAppointmentDoctor1 option:selected').val()),//
                            ProviderId_Appointment: $.trim($('#cmbAppointmentDoctor1').val()),
                            MessageUrgencyId: $.trim($('#cmbMessageUrgency1 option:selected').val()),
                            PreferredPeriod: PreferredPeriod1,
                            PreferredWeekDay: PreferredWeekDay1,

                            AppointmentStart: $.trim($('#txtPrefDateFrom').val()),
                            AppointmentEnd: $.trim($('#txtPrefDateTo').val()),
                            PreferredTime: $.trim($('#cmbPreftime option:selected').text()),
                            VisitReason: $.trim($('#txtReason').val()),
                            MessageRequest: txt,
                            status: 'Appointment'

                        };


                        $.ajax({
                            type: 'POST',
                            url: 'index-Appointment-save',
                            data: JSON.stringify(requestData),
                            contentType: 'application/json; charset=utf-8',
                            dataType: 'json',
                            success: function (data) {
                                //alert(data);
                              //  $("#Appointment-portlet").empty();
                                $("#tbl-MessageAppointment").html(data);
                                //alert(data);
                                $("#tbl-MessageAppointment").fixedHeaderTable();
                                alert("Appointment Request Sent Succesfully");
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

        }
    });

  

});


//for refil
$(function () {
    var Pharmacy = $("#cmbPharmacy"),


       allFields = $([]).add(Pharmacy),

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



    tips = $(".validateTips");
    //   var Location=$("#cmbAppointmentFacilityRefill"),
    var Doctor = $("#cmbDoctor"),

      allFields = $([]).add(Doctor),
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
        alert(o);
        if (o.val() == -1) {
            o.addClass("ui-state-error");
            alert(n);
            updateTips(n);

            return false;
        } else {
            return true;
        }
    }


    $("#dialog-refill").dialog({
        autoOpen: false,
        show: {
            effect: "blind",
            duration: 1000
        },
        hide: {
            effect: "blind",
            duration: 1000
        },
        height: 450,
        width: 700,
        modal: true,
        buttons: {
            "Send": function () {

                var bValid = true;
                allFields.removeClass("ui-state-error");
                //   bValid = bValid && checkLength(Location, "Please Select Location");
                // bvalid = bvalid && checkLength(Doctor, "Please Select Doctor");


                if (bValid) {

                    //  Save routine and call of Action
                    if (($('#cmbAppointmentFacilityRefill option:selected').val()) == -1 && ($('#cmbMedication option:selected').val()) == -1 && ($('#cmbDoctor option:selected').val()) == -1 && ($('#cmbRFPharmacy option:selected').val().split('|')[0]) == -1) {
                        alert("Please Select 'Location , Medication , Doctor , Pharmacy'option");
                        return false;
                    }
                    else if (($('#cmbAppointmentFacilityRefill option:selected').val()) == -1 && ($('#cmbMedication option:selected').val()) == -1 && ($('#cmbDoctor option:selected').val()) == -1) {
                        alert("Please Select 'Location , Medication , Doctor' option");
                        return false;
                    }
                    else if (($('#cmbAppointmentFacilityRefill option:selected').val()) == -1 && ($('#cmbMedication option:selected').val()) == -1 && ($('#cmbRFPharmacy option:selected').val().split('|')[0]) == -1) {
                        alert("Please Select 'Location , Medication , Pharmacy' option");
                        return false;
                    }
                     if (($('#cmbMedication option:selected').val()) == -1 && ($('#cmbDoctor option:selected').val()) == -1 && ($('#cmbRFPharmacy option:selected').val().split('|')[0]) == -1) {
                        alert("Please Select 'Medication , Doctor , Pharmacy' option");
                        return false;
                    }
                    else if (($('#cmbAppointmentFacilityRefill option:selected').val()) == -1 && ($('#cmbDoctor option:selected').val()) == -1 && ($('#cmbRFPharmacy option:selected').val().split('|')[0]) == -1) {
                        alert("Please Select 'Location , Doctor , Pharmacy' option");
                        return false;
                    }
                    else if (($('#cmbAppointmentFacilityRefill option:selected').val()) == -1 && ($('#cmbMedication option:selected').val()) == -1) {
                        alert("Please Select 'Location , Medication' option");
                        return false;
                    }
                    else if (($('#cmbDoctor option:selected').val()) == -1 && ($('#cmbRFPharmacy option:selected').val().split('|')[0]) == -1) {
                        alert("Please Select ' Doctor , Pharmacy' option");
                        return false;
                    }
                    else if (($('#cmbMedication option:selected').val()) == -1 && ($('#cmbDoctor option:selected').val()) == -1) {
                        alert("Please Select 'Medication , Doctor' option");
                        return false;
                    }
                    else if (($('#cmbAppointmentFacilityRefill option:selected').val()) == -1 && ($('#cmbDoctor option:selected').val()) == -1) {
                        alert("Please Select 'Location , Doctor' option");
                        return false;
                    }
                    else if (($('#cmbMedication option:selected').val()) == -1 && ($('#cmbRFPharmacy option:selected').val().split('|')[0]) == -1) {
                        alert("Please Select 'Medication , Pharmacy' option");
                        return false;
                    }
                    else if (($('#cmbAppointmentFacilityRefill option:selected').val()) == -1 && ($('#cmbRFPharmacy option:selected').val().split('|')[0]) == -1) {
                        alert("Please Select 'Location , Pharmacy' option");
                        return false;
                    }

                    else if (($('#cmbAppointmentFacilityRefill option:selected').val()) == -1) {
                        alert("Please Select 'Location' option");
                        return false;
                    }
                    else if (($('#cmbMedication option:selected').val()) == -1) {
                        alert("Please Select 'Medication ' option");
                        return false;
                    }
                    else if (($('#cmbDoctor option:selected').val()) == -1) {
                        alert("Please Select ' Doctor' option");
                        return false;
                    }
                    else if (($("#cmbRFPharmacy option:selected").val().split('|')[0]) == -1) {
                        alert("Please Select 'Pharmacy' option");
                        return false;
                    }

                    try {

                        var textComment = $.trim($('#txtComments').val());

                        var txt = textComment.replace(/\n/g, "\\n");
                        var requestData = {
                            "FacilityId": $.trim($('#cmbAppointmentFacilityRefill option:selected').val()),
                            "MedicationName": $.trim($('#cmbMedication option:selected').text()),
                            "ProviderId_To": $.trim($('#cmbDoctor option:selected').val()),
                            "MessageRequest": txt,
                            "NoOfRefills": $.trim($('#txtCountRefills').val()),
                            "PharmacyName": $.trim($('#cmbRFPharmacy option:selected').text()),
                            "PharmacyAddress": $.trim($('#PharmAddress').text()),
                            "status": 'Medicaton Refill'
                        };

                        $.ajax({
                            type: 'POST',
                            url: 'medications-refill-save',
                            data: JSON.stringify(requestData),
                            dataType: 'json',
                            contentType: 'application/json; charset=utf-8',
                            success: function (data) {
                                // if success
                                // alert("Success : " + data);
                                $("#tbl-MessageRefill").html(data);
                                $("#tbl-MessageRefill").fixedHeaderTable();
                                alert("Refill Request Sent Succesfully");



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
            $(".validateTips ").empty();
            $("span").removeClass("ui-state-highlight");
        }
    });
});


//referall


$(function () {

    tips = $(".validateTips");



    $("#reqbtn-form").dialog({
        autoOpen: false,
        show: {
            effect: "blind",
            duration: 1000
        },
        hide: {
            effect: "blind",
            duration: 1000
        },

        width: 850,
        modal: true,
        buttons: {
            "Save": function () {
                var bValid = true;



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
                var PreferredPeriod1 = chkStr($('#chkRmor').prop('checked'));
                PreferredPeriod1 += chkStr($('#chkRaft').prop('checked'));
                PreferredPeriod1 += chkStr($('#chkReve').prop('checked'));

                var PreferredWeekDay1 = chkStr($('#chkRmon').prop('checked'));
                PreferredWeekDay1 += chkStr($('#chkRtue').prop('checked'));
                PreferredWeekDay1 += chkStr($('#chkRwed').prop('checked'));
                PreferredWeekDay1 += chkStr($('#chkRthu').prop('checked'));
                PreferredWeekDay1 += chkStr($('#chkRfri').prop('checked'));
                PreferredWeekDay1 += chkStr($('#chkRsat').prop('checked'));
                PreferredWeekDay1 += chkStr($('#chkRsun').prop('checked'));

                if (bValid) {
                    //ajax 
                    try {
                        if (($('#cmbAppointmentFacility3 option:selected').val()) == -1 && ($('#cmbRProvider option:selected').val()) == -1 && ($('#cmbRMessageUrgency option:selected').val()) == -1 && ($('#cmbRprefTime option:selected').val()) == -1) {
                            alert("Please Select 'Location , Send Request To , Urgency , PreferredTime ' option");
                            return false;
                        }
                        else if (($('#cmbAppointmentFacility3 option:selected').val()) == -1 && ($('#cmbRProvider option:selected').val()) == -1 && ($('#cmbRMessageUrgency option:selected').val()) == -1) {
                            alert("Please Select 'Location , Send Request To , Urgency ' option");
                            return false;
                        }
                        else if (($('#cmbRProvider option:selected').val()) == -1 && ($('#cmbRMessageUrgency option:selected').val()) == -1 && ($('#cmbRprefTime option:selected').val()) == -1) {
                            alert("Please Select ' Send Request To , Urgency , PreferredTime ' option");
                            return false;
                        }
                        else if (($('#cmbAppointmentFacility3 option:selected').val()) == -1 && ($('#cmbRMessageUrgency option:selected').val()) == -1 && ($('#cmbRprefTime option:selected').val()) == -1) {
                            alert("Please Select ' Location , Urgency , PreferredTime ' option");
                            return false;
                        }
                        else if (($('#cmbAppointmentFacility3 option:selected').val()) == -1 && ($('#cmbRProvider option:selected').val()) == -1 && ($('#cmbRprefTime option:selected').val()) == -1) {
                            alert("Please Select 'Location , Send Request To , PreferredTime ' option");
                            return false;
                        }
                        else if (($('#cmbAppointmentFacility3 option:selected').val()) == -1 && ($('#cmbRProvider option:selected').val()) == -1) {
                            alert("Please Select 'Location , Send Request To  ' option");
                            return false;
                        }
                        else if (($('#cmbRMessageUrgency option:selected').val()) == -1 && ($('#cmbRprefTime option:selected').val()) == -1) {
                            alert("Please Select ' Urgency , PreferredTime ' option");
                            return false;
                        }
                        else if (($('#cmbRProvider option:selected').val()) == -1 && ($('#cmbRMessageUrgency option:selected').val()) == -1) {
                            alert("Please Select ' Send Request To , Urgency  ' option");
                            return false;
                        }
                        else if (($('#cmbAppointmentFacility3 option:selected').val()) == -1 && ($('#cmbRprefTime option:selected').val()) == -1) {
                            alert("Please Select ' Location , PreferredTime ' option");
                            return false;
                        }
                        else if (($('#cmbRProvider option:selected').val()) == -1 && ($('#cmbRprefTime option:selected').val()) == -1) {
                            alert("Please Select 'Send Request To , PreferredTime ' option");
                            return false;
                        }
                        else if (($('#cmbAppointmentFacility3 option:selected').val()) == -1 && ($('#cmbRMessageUrgency option:selected').val()) == -1) {
                            alert("Please Select ' Location , Urgency ' option");
                            return false;
                        }
                        else if (($('#cmbAppointmentFacility3 option:selected').val()) == -1) {
                            alert("Please Select 'Location  ' option");
                            return false;
                        }
                        else if (($('#cmbRProvider option:selected').val()) == -1) {
                            alert("Please Select 'Send Request To ' option");
                            return false;
                        }
                        else if (($('#cmbRMessageUrgency option:selected').val()) == -1) {
                            alert("Please Select ' Urgency ' option");
                            return false;
                        }
                        else if (($('#cmbRprefTime option:selected').val()) == -1) {
                            alert("Please Select ' PreferredTime ' option");
                            return false;
                        }

                        if (($('#txtRpredatefrom').val()) == "" && ($('#txtRpredateto').val()) == "") {
                            $('#txtRpredatefrom').val('01/01/1900');
                            $('#txtRpredateto').val('01/01/1900');
                        }
                        else if (($('#txtRpredatefrom').val()) == "") {
                            $('#txtRpredatefrom').val('01/01/1900');
                        }

                        else if (($('#txtRpredateto').val()) == "") {
                            $('#txtRpredateto').val('01/01/1900');
                        }

                        var textComment = $.trim($('#txtRComm').val());

                        var txt = textComment.replace(/\n/g, "\\n");
                        var requestData = {
                            FacilityId: $.trim($('#cmbAppointmentFacility3 option:selected').val()),
                            ProviderId_To: $.trim($('#cmbRProvider option:selected').val()),
                            // ProviderId_Appointment: $.trim($('#cmbAppointmentDoctor1 option:selected').val()),
                            MessageUrgencyId: $.trim($('#cmbRMessageUrgency option:selected').val()),
                            PreferredPeriod: PreferredPeriod1,
                            PreferredWeekDay: PreferredWeekDay1,

                            AppointmentStart: $.trim($('#txtRpredatefrom').val()),
                            AppointmentEnd: $.trim($('#txtRpredateto').val()),
                            PreferredTime: $.trim($('#cmbRprefTime option:selected').text()),
                            VisitReason: txt,
                            MessageRequest: $.trim($('#txtReqRes').val())

                        };


                        $.ajax({
                            type: 'POST',
                            url: 'index-RequestReferral-save',
                            data: JSON.stringify(requestData),
                            dataType: 'json',
                            contentType: 'application/json; charset=utf-8',
                            success: function (data) {
                                // if success
                                // alert("Success : " + data);
                                $("#tbl-MessageReferral").html(data);
                                $("#tbl-MessageReferral").fixedHeaderTable();
                                alert("Referral Request Sent Succesfully");

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

        }
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

    // Call horizontalNav on the navigations wrapping element
    $('.full-width').horizontalNav({});
});

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



function clearAttach() {
    //  $('#files').replaceWith('<input type="file" id="files" name="file"/>');
    // $('#ByteData').val('');
    // document.getElementById('files').addEventListener('change', handleFileSelect, false);
    $('#BrowseName').text('');
    $('#btncancelupload').hide();
}
function ShowCancel() {
    $('#txtcancelupload').show();
}

function LoadFacilityProviderSendMessage() {
    ShowLoaderForComposeMessage();
    try {
        var requestData = {
            FacilityID: $.trim($('#cmbAppointmentFacility option:selected').val()),
            Value: "SendMessage",
        };

        $.ajax({
            type: 'POST',
            url: 'Get-Provider-Facility-Data',
            data: JSON.stringify(requestData),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                // if success

                $("#DivLoadFacilityProviderSendMessage").html(data);

            },
            error: function (xhr, ajaxOptions, thrownError) {
                //if error

                HideLoaderForComposeMessage();
                alert('Error : ' + xhr.message);

            },
            complete: function (data) {
                // if completed
                HideLoaderForComposeMessage();

            },
            async: true,
            processData: false
        });
    }
    catch (err) {

        if (err && err !== "") {
            alert(err.message);
            HideLoaderForComposeMessage();
        }
    }
}
function ShowLoaderForComposeMessage() {

    document.getElementById('loader').style.display = 'block';

}
function HideLoaderForComposeMessage() {


    document.getElementById('loader').style.display = 'none';
}