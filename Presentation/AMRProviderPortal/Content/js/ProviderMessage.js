function checkAll(obj) {



    var IsChecked = obj.checked;
    var checkBoxId = "";
    if (obj.id.split('_')[1] == "I") {
        checkBoxId = "chkI";
    }
    else if (obj.id.split('_')[1] == "A") {
        checkBoxId = "chkA";
    }
    else if (obj.id.split('_')[1] == "D") {
        checkBoxId = "chkD";
    }
    else if (obj.id.split('_')[1] == "S") {
        checkBoxId = "chkS";
    }
    else if (obj.id.split('_')[1] == "M") {
        checkBoxId = "chkM";
    }
    else if (obj.id.split('_')[1] == "R") {
        checkBoxId = "chkR";
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
$(document).ready(function () {
    $('#tabs').tabs();
    document.getElementById('tabs').style.display = 'block';
    $("#tbl-messageInbox").fixedHeaderTable();
    $('#InboxDeleteMessage').click(fnOpenInoboxDialog);
    $('#SentDeleteMessage').click(fnOpenDeletedDialog);
    $('#deleterefrill').click(fnopenrefillDialog);

    $('#ReferralDelete').click(fnopenreferralllDialog);
    var $tabs = $('#tabs').tabs();
    $("#tabs").tabs({
        activate: function (event, ui) {
            var selected = $tabs.tabs('option', 'active');
           
            switch (selected) {
                case 0:
                    {
                     //   alert("inbox");
                        RenderHtml('34I');
                        //$("#tbl-messageInbox").fixedHeaderTable();
                        break;
                    }

                    break;
                case 1:
                    {
                        //$("#compose").hide()
                      //  alert("Sent");
                        RenderHtml('Appointment Request');
                        //$("#tbl-MessageAppointment").fixedHeaderTable();


                        break;
                    }
                case 2:
                    //  $("#compose").hide()
                    {
                     //   alert("Delete");
                        
                        RenderHtml('Medication Refill');
                        //$("#tbl-MessageRefill").fixedHeaderTable();



                        break;
                    }
                case 3:
                    // $("#compose").hide()
                    {
                       
                       // alert("Appointment");
                        
                        RenderHtml('Referral Message');
                        //$("#tbl-MessageReferral").fixedHeaderTable();



                        break;
                    }
                case 4:
                    //$("#compose").hide()
                    {
                     //   alert("Refill");
                        
                        RenderHtml('34S');
                        //$("#tbl-MessageSent").fixedHeaderTable();

                        break;
                    }
                case 5:
                    //$("#compose").hide()
                    {
                       // alert("Referall");
                        RenderHtml('34D');
                        //$("#tbl-MessageDelete").fixedHeaderTable();

                        break;
                    }
               

            }
        }
    });

});
var selected = "";
var providerclass = "r0";
var Appointmetnclass = "r0";
var Referallclass = "r0";
var Refillclass = "r0";
function AppointmentReply(id) {
    clearFields();
    $("#Appcount").append("You have 256 characters remaining");
    Hidecmbstatus();
    $("#Detail").html("");

    var obj = jQuery.parseJSON($("#hdAppointmentDetailJson-" + id).val());
  //  alert(obj.MessageRequest);
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
  //  html = html + 'Preffered Time of Day :' + periodval;
   // html = html + 'Preferred Day :' + dayval;
    var html = '';
    html += '<table>';
    html += '<tr><td>Patient Preferences</tr>';
    html += '<tr><td>Preferred Date Range : '+ obj.AppointmentStart + ' to ' + obj.AppointmentEnd+'</td></tr>';
    html += '<tr><td>Preferred Day : '+dayval+'</td></tr>';
    html += '<tr><td>Preferred Time of Day : ' + periodval + '</td></tr>';
    html += '<tr><td>Preferred Time : '+obj.PreferredTime+'</td></tr>';

    html += '</table>';
    $('#hdReplyMessageId').val(obj.HeaderID);
 //   $("#Detail").html("");
    $("#Detail").html(html);
    $("#ReplyAttachMessage").dialog("open");
    $("#ReplyAttachMessage").dialog('option', 'title', 'Appointment Request Response');

}
function RefillReply(id) {

    $("#refillcount").html("You have <strong>" + 256 + "</strong> characters remaining");

    $("#pharname").val("");
    $("#pharphone").val("");
    $("#pharadd").val("");

    Hidecmbstatus1();
    clearFields();
    var obj = jQuery.parseJSON($("#hdRefillDetailJson-" + id).val());
    var html = '';
    //var AddressArray = obj.PharmacyAddress.split('|');
    //alert(AddressArray[1]);
    html += '<table>';
 //   html += '<tr><td>Patient Preferences</tr>';
    html += '<tr><td>Medication : ' + obj.MedicationName+ '</td></tr>';
    html += '<tr><td>Pharmacy Name : ' + obj.PharmacyName + '</td></tr>';
    html += '<tr><td>Pharmacy Phone Number : '+ obj.PharmacyPhone+'</td></tr>';
    html += '<tr><td>Pharmacy Address : ' + obj.PharmacyAddress + '</td></tr>';

    html += '</table>';
    $("#pharname").val(obj.PharmacyName);
    $("#pharphone").val(obj.PharmacyPhone);
    $("#pharadd").val(obj.PharmacyAddress);

    $("#txtMedicationName").val(obj.MedicationName);
    $('#hdReplyMessageId').val(obj.HeaderID);
    $("#RefilDetail").html(html);
    $("#ReplyRequestRefillMessage").dialog('option', 'title', 'Refill Request Response');
    $("#ReplyRequestRefillMessage").dialog("open");


}

function ReferralReply(id) {
    clearFields();
    //alert('test');
    $("#referalcount").html("You have <strong>" + 256 + "</strong> characters remaining");

    var obj = jQuery.parseJSON($("#hdReferralDetailJson-" + id).val());
    $('#RplyReason').text(obj.MessageRequest);
    $('#RplyComment').text(obj.VisitReason);
    $('#hdReplyMessageId').val(obj.HeaderID);
    $("#ReplyRequestReferralMessage").dialog("open");

}

function messageInboxReply(id) {
    $('#hdReplyMessageDetailId').val(id);
    $("#txtMessageWrite1").val("");
    SentMessage('Reply');
}

//function start for inbox detail ajax
var tempprovider = null;
var objnameprovider = null;
var objhassclass = null;
var ifclass = null;
var elseclass = null;
function messageInboxDetail(id, providerclass) {

  //  alert(providerclass);
    ShowLoader();
   // $("#MessageDetail" + id).removeClass("selected");
    var obj = jQuery.parseJSON($("#hdInboxDetailJson" + id).val());
    if (obj.ProviderIdTo != '0') {
        try {
          //  alert("hi");
              objhassclass = $("#MessageDetail" + id).hasClass("selected").toString();
              if (objhassclass == "false")
              {
                 
        $("#MessageDetail" + id).addClass("selected").siblings().removeClass("selected");
        $("#MessageDetail" + id).removeClass("r1");
        $("#MessageDetail" + id).removeClass("r0");
        if (tempprovider != null) {
            if (objnameprovider == 'r0') {
                $(tempprovider).addClass("r0");
            }
            else {
                $(tempprovider).addClass("r1");
            }
        }
       
            tempprovider = ("#MessageDetail" + id);

            objnameprovider = providerclass;
        }
    
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
                    $('#MedicationMessages').html("Refill Requests(<b style='font-size:14px;'>" + data.MedicationMessages + "</b>)");
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
                    html = html + 'To:  ' + obj.FacilityName;
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
           // alert(providerclass);
            objhassclass = $("#MessageDetail" + id).hasClass("selected").toString();
            if (objhassclass == "false")
            {
            $("#MessageDetail" + id).addClass("selected").siblings().removeClass("selected");
            $("#MessageDetail" + id).removeClass("r1");
            $("#MessageDetail" + id).removeClass("r0");
            if (tempprovider != null) {
                if (objnameprovider == 'r0') {
                    $(tempprovider).addClass("r0");
                }
                else {
                    $(tempprovider).addClass("r1");
                }
                  }
          
                tempprovider = ("#MessageDetail" + id);
                objnameprovider = providerclass;

            }

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
            html = html + 'From:  ' + obj.FacilityName;
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

} //Function end for inbox detail ajax

//Function end for Appointment detail ajax
var tempappointment = null;
var objnameApp = null;
var objhasAppclass = null;
function messageAppointmentDetail(id,Appointmetnclass) {

    //  alert(id);
    ShowLoader();
    var obj = jQuery.parseJSON($("#hdAppointmentDetailJson" + id).val());
    if (obj.ProviderIdTo != '0') {
    //  alert("hi");
        try {
            objhasAppclass = $("#MessageDetail" + id).hasClass("selected").toString();
            if (objhasAppclass == "false") {
                $("#MessageDetail" + id).addClass("selected").siblings().removeClass("selected");
                $("#MessageDetail" + id).removeClass("r1");
                $("#MessageDetail" + id).removeClass("r0");
                if (tempappointment != null) {
                    if (objnameApp == 'r0') {
                        $(tempappointment).addClass("r0");
                    }
                    else {
                        $(tempappointment).addClass("r1");
                    }
                }

                tempappointment = ("#MessageDetail" + id);

                objnameApp = Appointmetnclass;
            }

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
                     //   alert('found');
                    }
                    else {

                        $('#MessageHeader-' + obj.HeaderID + ' td').css('color', '#666699');
                        $('#MessageHeader-' + obj.HeaderID + ' td ').css('font-weight', 'normal');
                    }

                    $('#circle').html(data.TotalMessages);
                    $('#hdAppointmentMessages').html(data.AppointmentMessages);
                    $('#hdGeneralMessages').html(data.GeneralMessages);
                    $('#hdMedicationMessages').html(data.MedicationMessages);
                    $('#hdReferralMessages').html(data.ReferralMessages);
                    $('#hdTotalMessages').html(data.TotalMessages);

                    $('#GeneralMessages').html("Inbox(<b style='font-size:14px;'>" + data.GeneralMessages + "</b>)");
                    $('#AppointmentMessages').html("Appointment(<b style='font-size:14px;'>" + data.AppointmentMessages + "</b>)");
                    $('#MedicationMessages').html("Refill Requests(<b style='font-size:14px;'>" + data.MedicationMessages + "</b>)");
                    $('#ReferralMessages').html("Referral Requests(<b style='font-size:14px;'>" + data.ReferralMessages + "</b>)");

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
                    html = html + '<div style="margin-top: 1px; margin-left: 30px; font-size: 12px; color: #999999;">';
                    html = html + 'From:  ' + obj.From;
                    html = html + '</div>';
                    html = html + '<div style="margin-top: 1px; margin-left: 30px; font-size: 12px; color: #999999;">';
                    html = html + 'To:  ' + obj.FacilityName;
                    html = html + '</div>';
                    html = html + '</div>';
                    //<!--End of reply part-->
                    html = html + '<div id="reply" style="height: 570px;" class="ui-corner-all">';
                    html = html + '<div style="margin-left: 30px;">';
                    html = html + '        <br>';
                    //html = html + '        <br>';
                    //html = html + obj.MessageType;
                    //html = html + '                        <br>';
               
                    //html = html + 'Facility Name :' + obj.FacilityName;
                    //html = html + '                        <br>';
                    if (obj.ProviderIdFrom !== '0') {
                        html = html + 'Message: ' + obj.MessageResponse;
                        html = html + '                        <br>';
                        html = html + 'Preffered Time :' + obj.PreferredTime;
                        html = html + '                        <br>';
                        html = html + 'Appointment Start :' + obj.AppointmentStart;
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
                        html = html + 'Preferred Date Range : ' + obj.AppointmentStart + ' to ' + obj.AppointmentEnd;
                        html = html + '                        <br>';
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
                        html = html + 'Preferred Day : ' + dayval;
                        html = html + '                        <br>';
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
                        html = html + 'Preffered Time of Day : ' + periodval;
                        html = html + '                        <br>';
                        html = html + 'Preffered Time : ' + obj.PreferredTime;
                        html = html + '                        <br>';

                        html = html + 'Reason For Visit: ' + obj.VisitReason;
                        html = html + '                        <br>';

                        html = html + 'Comments: ' + obj.MessageRequest;
                   //     html = html + '                        <br>';
                        //var period = obj.PreferredPeriod;
                        //var periodval = "";
                        //if (period[0] == '1') {
                        //    periodval = 'Morning, ';
                        //}
                        //if (period[1] == '1') {
                        //    periodval += 'Afternoon, ';
                        //}
                        //if (period[2] == '1') {
                        //    periodval += 'Evening ';
                        //}

                        //var day = obj.PreferredWeekDay;
                        //var dayval = "";
                        //if (day[0] == '1') {
                        //    dayval = 'Monday, ';
                        //}
                        //if (day[1] == '1') {
                        //    dayval += 'Tuesday, ';
                        //}
                        //if (day[2] == '1') {
                        //    dayval += 'Wednesday, ';
                        //}
                        //if (day[3] == '1') {
                        //    dayval += 'Thursday, ';
                        //}
                        //if (day[4] == '1') {
                        //    dayval += 'Friday, ';
                        //}
                        //if (day[5] == '1') {
                        //    dayval += 'Saturday, ';
                        //}
                        //if (day[6] == '1') {
                        //    dayval += 'Sunday, ';
                        //}
                        html = html + '                        <br>';
                        //html = html + 'Preffered Period :' + periodval;
                        //html = html + '                        <br>';
                //      html = html + 'Urgency :' + obj.MessageUrgency;
                   //    html = html + '                        <br>';
                        //html = html + 'Week Day :' + dayval;
                        //html = html + '                        <br>';
                    //    html = html + 'Preffered Time :' + obj.PreferredTime;
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
       //   alert("hi if");

            if (objhasAppclass == "false") {
                $("#MessageDetail" + id).addClass("selected").siblings().removeClass("selected");
                $("#MessageDetail" + id).removeClass("r1");
                $("#MessageDetail" + id).removeClass("r0");
                if (tempappointment != null) {
                    if (objnameApp == 'r0') {
                        $(tempappointment).addClass("r0");
                    }
                    else {
                        $(tempappointment).addClass("r1");
                    }
                }

                tempappointment = ("#MessageDetail" + id);

                objnameApp = Appointmetnclass;
            }

            var html = '<div id="Inform_Head" class="ui-corner-all">';
            html = html + '<h4 style="padding-top: 10px; margin-left: 30px;">';
            if (obj.ProviderIdFrom !== '0') {
                html = html + '<b>Appointment Request Response</b></h4>';
            }
            else {
                html = html + '<b>Appointment Request</b></h4>';
            }
         //   html = html + '<b>' + obj.MessageType + '</b></h4>';
            html = html + '<div style=\"float: right; margin-top: -30px; margin-right:10px; font-size: 12px; color: #999999;\">';

            var today = new Date();
            var dd = today.getDate();
            var mm = today.getMonth() + 1; //January is 0!

            var yyyy = today.getFullYear();
            if (dd < 10) { dd = '0' + dd } if (mm < 10) { mm = '0' + mm } today = mm + '/' + dd + '/' + yyyy;

            html = html + today;
            html = html + '</div>';
            html = html + '<div style="margin-top: 1px; margin-left: 30px; font-size: 12px; color: #999999;">';
            html = html + 'From:  ' + obj.FacilityName;
            html = html + '</div>';
            html = html + '<div style="margin-top: 1px; margin-left: 30px; font-size: 12px; color: #999999;">';
            html = html + 'To:  ' + obj.To;
            html = html + '</div>';
            html = html + '</div>';
            //<!--End of reply part-->
            html = html + '<div id="reply" style="height: 570px;" class="ui-corner-all">';
            html = html + '<div style="margin-left: 30px;margin-top:-30px;">';
            html = html + '        <br>';
            //html = html + '        <br>';
            //html = html + obj.MessageType;
            //html = html + '                        <br>';
            //html = html + 'Facility Name :' + obj.FacilityName;
            //html = html + '                        <br>';
            if (obj.ProviderIdFrom !== '0') {
                html = html + '                        <br>';
                html = html + 'Date :' + obj.AppointmentStart;
                html = html + '                        <br>';
            
                html = html + 'Time :' + obj.PreferredTime;
                html = html + '                        <br>';
              
                html = html + 'Message Response : ' + obj.MessageResponse;
                html = html + '                        <br>';
            }

            else {

                var urgent1 = obj.MessageUrgency;
                if (urgent1 == "False") {
                    html = html + 'Urgent : No';
                    html = html + '                        <br>';
                }
                else {
                    html = html + 'Urgent : Yes';
                    html = html + '                        <br>';

                }
                //html = html + 'Urgent :' + (obj.MessageUrgency ? "Yes" : "No");
                //        html = html + '                        <br>';
                        html = html + 'Preferred Date Range :' + obj.AppointmentStart + ' to ' + obj.AppointmentEnd;
                        html = html + '                        <br>';
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
                        html = html + 'Preferred Day :' + dayval;
                        html = html + '                        <br>';
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
                        html = html + 'Preffered Time of Day :' + periodval;
                        html = html + '                        <br>';
                        html = html + 'Preffered Time :' + obj.PreferredTime;
                        html = html + '                        <br>';

                        html = html + 'Reason For Visit: ' + obj.VisitReason;
                        html = html + '                        <br>';

                        html = html + 'Comments:' + obj.MessageRequest;
                //html = html + 'Message: ' + obj.VisitReason;
                //var period = obj.PreferredPeriod;
                //var periodval = "";
                //if (period[0] == '1') {
                //    periodval = 'Morning, ';
                //}
                //if (period[1] == '1') {
                //    periodval += 'Afternoon, ';
                //}
                //if (period[2] == '1') {
                //    periodval += 'Evening ';
                //}

                //var day = obj.PreferredWeekDay;
                //var dayval = "";
                //if (day[0] == '1') {
                //    dayval = 'Monday, ';
                //}
                //if (day[1] == '1') {
                //    dayval += 'Tuesday, ';
                //}
                //if (day[2] == '1') {
                //    dayval += 'Wednesday, ';
                //}
                //if (day[3] == '1') {
                //    dayval += 'Thursday, ';
                //}
                //if (day[4] == '1') {
                //    dayval += 'Friday, ';
                //}
                //if (day[5] == '1') {
                //    dayval += 'Saturday, ';
                //}
                //if (day[6] == '1') {
                //    dayval += 'Sunday, ';
                //}
                //html = html + '                        <br>';
                //html = html + 'Preffered Period :' + periodval;
                //html = html + '                        <br>';
                //html = html + 'Urgency :' + obj.MessageUrgency;
                //html = html + '                        <br>';
                //html = html + 'Week Day :' + dayval;
                //html = html + '                        <br>';
                //html = html + 'Preffered Time :' + obj.PreferredTime;
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
var tempRefill = null;
var objnameRefill = null;
var objhasRefillclass = null;
function messageRequestRefillDetail(id, Refillclass) {

    //  alert(id);
    ShowLoader();
    var obj = jQuery.parseJSON($("#hdRequestRefillDetailJson" + id).val());
    if (obj.ProviderIdTo != '0') {
        try {
            // alert("if");
            objhasRefillclass = $("#MessageDetail" + id).hasClass("selected").toString();
            if (objhasRefillclass == "false") {
                $("#MessageDetail" + id).addClass("selected").siblings().removeClass("selected");
                $("#MessageDetail" + id).removeClass("r1");
                $("#MessageDetail" + id).removeClass("r0");
                if (tempRefill != null) {
                    if (objnameRefill == 'r0') {
                        $(tempRefill).addClass("r0");
                    }
                    else {
                        $(tempRefill).addClass("r1");
                    }
                }

                tempRefill = ("#MessageDetail" + id);

                objnameRefill = Refillclass;
            }
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
                        //alert('found');
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
                    $('#MedicationMessages').html("Refill Requests(<b style='font-size:14px;'>" + data.MedicationMessages + "</b>)");
                    $('#ReferralMessages').html("Referral Requests(<b style='font-size:14px;'>" + data.ReferralMessages + "</b>)");

                    var html = '<div id="Inform_Head" class="ui-corner-all">';
                    html = html + '<h4 style="padding-top: 10px; margin-left: 30px;">';
                    //  html = html + '<b>Refill Request</b></h4>';
                    if (obj.ProviderIdFrom !== '0') {
                        html = html + '<b>Refill Request Response</b></h4>';
                    }
                    else {
                        html = html + '<b>Refill Request</b></h4>';
                    }
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
                    html = html + '<div style="margin-top: 1px; margin-left: 50px; font-size: 12px; color: #999999;">';
                    html = html + 'To:  ' + obj.FacilityName;
                    html = html + '</div>';
                    html = html + '</div>';
                    //<!--End of reply part-->
                    //<!--End of reply part-->
                    html = html + '<div id=\"reply\" style=\"height: 625px;\" class=\"ui-corner-all\">';
                    html = html + '<div style=\"margin-left: 30px;margin-top:-25px;\">';
                    html = html + '        <br>';
                    html = html + '        <br>';
                    //html = html + 'Medication Refill Request';
                    //html = html + '                        <br>';
                    //html = html + 'Patient :' + obj.CreatedByName;
                    //html = html + '                       <br>';
                    html = html + 'Medication :' + obj.MedicationName;
                    html = html + '                        <br>';
                    html = html + 'No. Of Refills :' + obj.NoOfRefills;
                    html = html + '                        <br>';
                    //html = html + 'Provider :  ' + obj.To;
                    //html = html + '                        <br>';
                    html = html + 'Pharmacy Name :' + obj.PharmacyName;
                    html = html + '                        <br>';
                    html = html + 'Pharmacy Phone Number :' + obj.PharmacyPhone;
                    html = html + '                        <br>';
                    html = html + 'Pharmacy Address :' + obj.PharmacyAddress;
                    html = html + '                        <br>';
                    //html = html + 'No. Of Refills :' + obj.NoOfRefills;
                    //html = html + '                        <br>';
                 //   html = html + '                        <br>';

                    html = html + 'Comments : ' + obj.MessageRequest;
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
       //     alert("else");
            objhasRefillclass = $("#MessageDetail" + id).hasClass("selected").toString();
            if (objhasRefillclass == "false") {
                $("#MessageDetail" + id).addClass("selected").siblings().removeClass("selected");
                $("#MessageDetail" + id).removeClass("r1");
                $("#MessageDetail" + id).removeClass("r0");
                if (tempRefill != null) {
                    if (objnameRefill == 'r0') {
                        $(tempRefill).addClass("r0");
                    }
                    else {
                        $(tempRefill).addClass("r1");
                    }
                }

                tempRefill = ("#MessageDetail" + id);

                objnameRefill = Refillclass;
            }
            var html = '<div id="Inform_Head" class="ui-corner-all">';
            html = html + '<h4 style="padding-top: 10px; margin-left: 30px;">';
           // html = html + '<b>Refill Request Response</b></h4>';
            if (obj.ProviderIdFrom !== '0') {
                html = html + '<b>Refill Request Response</b></h4>';
            }
            else {
                html = html + '<b>Refill Request</b></h4>';
            }
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
            html = html + 'To:  ' + obj.To;
            html = html + '</div>';
            html = html + '</div>';
            //<!--End of reply part-->
            //<!--End of reply part-->
            html = html + '<div id=\"reply\" style=\"height: 625px;\" class=\"ui-corner-all\">';
            html = html + '<div style=\"margin-left: 30px;margin-top:-25px;\">';
            html = html + '        <br>';
            html = html + '        <br>';
            //html = html + 'Medication Refill Request';
            //html = html + '                        <br>';
            //html = html + 'Patient :' + obj.CreatedByName;
            //html = html + '                       <br>';
            html = html + 'Medication :' + obj.MedicationName;
            html = html + '                        <br>';
         //   html = html + '                        <br>';
            html = html + 'No. Of Refills :' + obj.NoOfRefills;
            html = html + '                        <br>';
            //html = html + 'Provider :  ' + obj.To;
            //html = html + '                        <br>';
            html = html + 'Pharmacy Name :' + obj.PharmacyName;
            html = html + '                        <br>';
            html = html + 'Pharmacy Phone Number :'+obj.PharmacyPhone;
            html = html + '                        <br>';
            html = html + 'Pharmacy Address :' + obj.PharmacyAddress;
            //html = html + '                        <br>';
            //html = html + 'No. Of Refills :' + obj.NoOfRefills;
            //html = html + '                        <br>';
            html = html + '                        <br>';

            html = html + 'Comments : ' + obj.MessageResponse;
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
var tempReferall = null;
var objnameReferall = null;
var objhasReferallclass = null;
function messageReferralRequestDetail(id, Referallclass) {

    //  alert(id);
    ShowLoader();
    var obj = jQuery.parseJSON($("#hdReferralRequestDetailJson" + id).val());
    if (obj.ProviderIdTo != '0') {
        try {
            objhasReferallclass = $("#MessageDetail" + id).hasClass("selected").toString();
            if (objhasReferallclass == "false") {
                $("#MessageDetail" + id).addClass("selected").siblings().removeClass("selected");
                $("#MessageDetail" + id).removeClass("r1");
                $("#MessageDetail" + id).removeClass("r0");
                if (tempReferall != null) {
                    if (objnameReferall == 'r0') {
                        $(tempReferall).addClass("r0");
                    }
                    else {
                        $(tempReferall).addClass("r1");
                    }
                }

                tempReferall = ("#MessageDetail" + id);

                objnameReferall = Referallclass;
            }
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
                    $('#MedicationMessages').html("Refill Requests(<b style='font-size:14px;'>" + data.MedicationMessages + "</b>)");
                    $('#ReferralMessages').html("Referral Requests(<b style='font-size:14px;'>" + data.ReferralMessages + "</b>)");

                    var html = '<div id="Inform_Head" class="ui-corner-all">';
                    html = html + '<h4 style="padding-top: 10px; margin-left: 10px;">';
                    if (obj.ProviderIdFrom !== '0') {
                        html = html + '<b>Referral Request Response</b></h4>';
                    }
                    else {
                        html = html + '<b>Referral Request</b></h4>';
                    }
                    html = html + '<div style=\"float: right; margin-top: -30px; margin-right:10px; font-size: 12px; color: #999999;\">';

                    var today = new Date();
                    var dd = today.getDate();
                    var mm = today.getMonth() + 1; //January is 0!

                    var yyyy = today.getFullYear();
                    if (dd < 10) { dd = '0' + dd } if (mm < 10) { mm = '0' + mm } today = mm + '/' + dd + '/' + yyyy;

                    html = html + today;
                    html = html + '</div>';
                    html = html + '<div style="margin-top: 1px; margin-left: 10px; font-size: 12px; color: #999999;">';
                    html = html + 'From:  ' + obj.From;
                    html = html + '</div>';
                    html = html + '<div style="margin-top: 1px; margin-left: 10px; font-size: 12px; color: #999999;">';
                    html = html + 'To:  ' + obj.FacilityName;
                    html = html + '</div>';
                    html = html + '</div>';
                    //<!--End of reply part-->
                    html = html + '<div id="reply" style="height: 570px;" class="ui-corner-all">';
                    html = html + '<div style="margin-left: 30px;">';
                    html = html + '        <br>';
                    html = html + '        <br>';
                    //html = html + obj.MessageType;
                    //html = html + '                        <br>';
                    //html = html + 'Facility Name :' + obj.FacilityName;
                    //html = html + '                        <br>';
                    if (obj.ProviderIdFrom !== '0') {
                        //html = html + 'Message: ' + obj.MessageResponse;
                        //html = html + '                        <br>';
                        //html = html + 'Preffered Time :' + obj.PreferredTime;
                        //html = html + '                        <br>';
                        //html = html + 'Appointment Start :' + obj.AppointmentStart;
                        //html = html + '                        <br>';
                        html = html + 'Message Respone : ' + obj.MessageResponse;
                    }

                    else {
                        html = html + 'Reason for Referral : ' + obj.MessageRequest;
                        html = html + '        <br>';
                        html = html + 'Comments :' + obj.VisitReason;
                        //html = html + 'Message: ' + obj.VisitReason;
                        //var period = obj.PreferredPeriod;
                        //var periodval = "";
                        //if (period[0] == '1') {
                        //    periodval = 'Morning, ';
                        //}
                        //if (period[1] == '1') {
                        //    periodval += 'Afternoon, ';
                        //}
                        //if (period[2] == '1') {
                        //    periodval += 'Evening ';
                        //}

                        //var day = obj.PreferredWeekDay;
                        //var dayval = "";
                        //if (day[0] == '1') {
                        //    dayval = 'Monday, ';
                        //}
                        //if (day[1] == '1') {
                        //    dayval += 'Tuesday, ';
                        //}
                        //if (day[2] == '1') {
                        //    dayval += 'Wednesday, ';
                        //}
                        //if (day[3] == '1') {
                        //    dayval += 'Thursday, ';
                        //}
                        //if (day[4] == '1') {
                        //    dayval += 'Friday, ';
                        //}
                        //if (day[5] == '1') {
                        //    dayval += 'Saturday, ';
                        //}
                        //if (day[6] == '1') {
                        //    dayval += 'Sunday, ';
                        //}
                        //html = html + '                        <br>';
                        //html = html + 'Preffered Period :' + periodval;
                        //html = html + '                        <br>';
                        //html = html + 'Urgency :' + obj.MessageUrgency;
                        //html = html + '                        <br>';
                        //html = html + 'Week Day :' + dayval;
                        //html = html + '                        <br>';
                        //html = html + 'Preffered Time :' + obj.PreferredTime;
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
            objhasReferallclass = $("#MessageDetail" + id).hasClass("selected").toString();
            if (objhasReferallclass == "false") {
                $("#MessageDetail" + id).addClass("selected").siblings().removeClass("selected");
                $("#MessageDetail" + id).removeClass("r1");
                $("#MessageDetail" + id).removeClass("r0");
                if (tempReferall != null) {
                    if (objnameReferall == 'r0') {
                        $(tempReferall).addClass("r0");
                    }
                    else {
                        $(tempReferall).addClass("r1");
                    }
                }

                tempReferall = ("#MessageDetail" + id);

                objnameReferall = Referallclass;
            }
            var html = '<div id="Inform_Head" class="ui-corner-all">';
            html = html + '<h4 style="padding-top: 10px; margin-left: 10px;">';
            if (obj.ProviderIdFrom !== '0') {
                html = html + '<b>Referral Request Response</b></h4>';
            }
            else {
                html = html + '<b>Referral Request</b></h4>';
            }
            html = html + '<div style=\"float: right; margin-top: -30px; margin-right:10px; font-size: 12px; color: #999999;\">';

            var today = new Date();
            var dd = today.getDate();
            var mm = today.getMonth() + 1; //January is 0!

            var yyyy = today.getFullYear();
            if (dd < 10) { dd = '0' + dd } if (mm < 10) { mm = '0' + mm } today = mm + '/' + dd + '/' + yyyy;

            html = html + today;
            html = html + '</div>';
            html = html + '<div style="margin-top: 1px; margin-left: 10px; font-size: 12px; color: #999999;">';
            html = html + 'From:  ' + obj.FacilityName;
            html = html + '</div>';
            html = html + '<div style="margin-top: 1px; margin-left: 10px; font-size: 12px; color: #999999;">';
            html = html + 'To:  ' + obj.To;
            html = html + '</div>';
            html = html + '</div>';
            //<!--End of reply part-->
            html = html + '<div id="reply" style="height: 570px;" class="ui-corner-all">';
            html = html + '<div style="margin-left: 30px;">';
            html = html + '        <br>';
            html = html + '        <br>';
            //html = html + obj.MessageType;
            //html = html + '                        <br>';
            //html = html + 'Facility Name :' + obj.FacilityName;
            //html = html + '                        <br>';
            if (obj.ProviderIdFrom !== '0') {
            //    html = html + 'Message: ' + obj.MessageResponse;
            //    html = html + '                        <br>';
            //    html = html + 'Preffered Time :' + obj.PreferredTime;
            //    html = html + '                        <br>';
            //    html = html + 'Appointment Start :' + obj.AppointmentStart;
                //    html = html + '                        <br>';
                html = html + 'Message Respone : ' + obj.MessageResponse;
            }

            else {
                html = html + 'Reason for Referral : ' + obj.MessageRequest;
                html = html + '        <br>';
                html = html + 'Comments :' + obj.VisitReason;
            //    html = html + 'Message: ' + obj.VisitReason;
            //    var period = obj.PreferredPeriod;
            //    var periodval = "";
            //    if (period[0] == '1') {
            //        periodval = 'Morning, ';
            //    }
            //    if (period[1] == '1') {
            //        periodval += 'Afternoon, ';
            //    }
            //    if (period[2] == '1') {
            //        periodval += 'Evening ';
            //    }

            //    var day = obj.PreferredWeekDay;
            //    var dayval = "";
            //    if (day[0] == '1') {
            //        dayval = 'Monday, ';
            //    }
            //    if (day[1] == '1') {
            //        dayval += 'Tuesday, ';
            //    }
            //    if (day[2] == '1') {
            //        dayval += 'Wednesday, ';
            //    }
            //    if (day[3] == '1') {
            //        dayval += 'Thursday, ';
            //    }
            //    if (day[4] == '1') {
            //        dayval += 'Friday, ';
            //    }
            //    if (day[5] == '1') {
            //        dayval += 'Saturday, ';
            //    }
            //    if (day[6] == '1') {
            //        dayval += 'Sunday, ';
            //    }
            //    html = html + '                        <br>';
            //    html = html + 'Preffered Period :' + periodval;
            //    html = html + '                        <br>';
            //    html = html + 'Urgency :' + obj.MessageUrgency;
            //    html = html + '                        <br>';
            //    html = html + 'Week Day :' + dayval;
            //    html = html + '                        <br>';
            //    html = html + 'Preffered Time :' + obj.PreferredTime;
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

var temp = null;
var objname = null;
var hasclass=null;
function inboxDetail(id, className, value) {
    var obj = id;
    tempprovider = null;
    
    ShowLoader();
    hasclass = $("#MessageHeader-"+ id).hasClass("selected").toString();
    // alert(hasclass);
  //  alert("value of s" + value+"and has class value is "+hasclass+" temp value is "+temp);
    //  alert("it contain calss" + hasclass);
    if (hasclass == "false"){

        $("#MessageHeader-" + obj).addClass("selected").siblings().removeClass("selected");


        $("#MessageHeader-" + obj).removeClass("r1");
        $("#MessageHeader-" + obj).removeClass("r0");

        if (temp != null) {
            if (objname == 'r0') {
                $(temp).addClass("r0");
            }
            else {
                $(temp).addClass("r1");
            }
        }
        
          //  alert("false afdsafdsfafsdf");
            temp = ("#MessageHeader-" + obj);

            objname = className;
        }
       
   
    
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
                // alert("Success : " + data);
                
                if (value == 'I') {
                    $("#MessageDetailGrid").html(data);
                    var msgdtlInbox = $("#hdFirstDetailInbox").val();
                    messageInboxDetail(msgdtlInbox,providerclass);
                   // $("#detail-inbox-request").hide();
                }
                else if (value == 'S') {
                    $("#MessageSentDetailGrid").html(data);
                    var msgdtlSent = $("#hdFirstDetailInbox").val();
                    messageInboxDetail(msgdtlSent, providerclass);
                   // $("#detail-sent-request").hide();
                }
                else if (value == 'D') {
                    $("#MessageDeleteDetailGrid").html(data);
                    //$("#detail-delete-request").hide();
                    var msgdtlDel = $("#hdFirstDetailInbox").val();
                    messageInboxDetail(msgdtlDel, providerclass);
                }

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

var tempsend = null;
var objnamesend = null;
var hasclasssend = null;
function SendDetail(id, classNamesend, value)
{

    var obj = id;
    tempprovider = null;
    ShowLoader();
    hasclasssend = $("#SendMessage-" + id).hasClass("selected").toString();
    // alert(hasclass);
   // alert("value of s" + value + "and has class value is " + hasclass + " temp value is " + temp);
    //  alert("it contain calss" + hasclass);
    if (hasclasssend == "false") {

        $("#SendMessage-" + obj).addClass("selected").siblings().removeClass("selected");


        $("#SendMessage-" + obj).removeClass("r1");
        $("#SendMessage-" + obj).removeClass("r0");

        if (tempsend != null) {
            if (objnamesend == 'r0') {
                $(tempsend).addClass("r0");
            }
            else {
                $(tempsend).addClass("r1");
            }
        }

        //  alert("false afdsafdsfafsdf");
        tempsend = ("#SendMessage-" + obj);

        objnamesend = classNamesend;
    }



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
                // alert("Success : " + data);

                if (value == 'I') {
                    $("#MessageDetailGrid").html(data);
                    var msgdtlInbox = $("#hdFirstDetailInbox").val();
                    messageInboxDetail(msgdtlInbox, providerclass);
                    // $("#detail-inbox-request").hide();
                }
                else if (value == 'S') {
                    $("#MessageSentDetailGrid").html(data);
                    var msgdtlSent = $("#hdFirstDetailInbox").val();
                    messageInboxDetail(msgdtlSent, providerclass);
                    // $("#detail-sent-request").hide();
                }
                else if (value == 'D') {
                    $("#MessageDeleteDetailGrid").html(data);
                    //$("#detail-delete-request").hide();
                    var msgdtlDel = $("#hdFirstDetailInbox").val();
                    messageInboxDetail(msgdtlDel, providerclass);
                }

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
var hasclassappointment = null;
var apptemp = null;
var appobjname = null;
function AppointmentDetail(id, className) {
    // alert(id);
    temp = null;
    objname = null;
    tempappointment = null;
    ShowLoader();
    hasclassappointment = $("#MessageHeader-" + id).hasClass("selected").toString();
    if (hasclassappointment == "false")
        {
    $("#MessageHeader-" + id).addClass("selected").siblings().removeClass("selected");
    //alert("#MessageHeader-" + id);
    $("#MessageHeader-" + id).removeClass("r1");
    $("#MessageHeader-" + id).removeClass("r0");

    if (apptemp != null) {
        if (appobjname == 'r0') {
            $(apptemp).addClass("r0");
        }
        else {
            $(apptemp).addClass("r1");
        }
    }
    apptemp = ("#MessageHeader-" + id);
    appobjname = className;
        }
   

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
                messageAppointmentDetail(msgdtlApp, Appointmetnclass);
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
var hasclassrefil = null;
var refiltemp = null;
var refilobjname = null;
function medicationRefillDetail(id, className) {
    tempRefill = null;
    ShowLoader();
    hasclassrefil = $("#MessageHeader-" + id).hasClass("selected").toString();
    if (hasclassrefil == "false") {
        $("#MessageHeader-" + id).addClass("selected").siblings().removeClass("selected");

        //alert("#MessageHeader-" + id);
        $("#MessageHeader-" + id).removeClass("r1");
        $("#MessageHeader-" + id).removeClass("r0");

        if (refiltemp != null) {
            if (refilobjname == 'r0') {
                $(refiltemp).addClass("r0");
            }
            else {
                $(refiltemp).addClass("r1");
            }
        }
        refiltemp = ("#MessageHeader-" + id);
        refilobjname = className;
    }
    
    //     alert('test');
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
                messageRequestRefillDetail(msgdtlMed, Refillclass);
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
var hasclassReferall = null;
function ReferralRequestDetail(id, className) {
    tempReferall = null;
    ShowLoader();
    hasclassReferall = $("#MessageHeader-" + id).hasClass("selected").toString();
    if (hasclassReferall == "false") {
        $("#MessageHeader-" + id).addClass("selected").siblings().removeClass("selected");
        $("#MessageHeader-" + id).removeClass("r1");
        $("#MessageHeader-" + id).removeClass("r0");

        if (temp != null) {
            if (objname == 'r0') {
                $(temp).addClass("r0");
            }
            else {
                $(temp).addClass("r1");
            }
        }
        temp = ("#MessageHeader-" + id);
        objname = className;
    }
  
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
                messageReferralRequestDetail(msgdtlRef, Referallclass);
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

                    $('#GeneralMessages').html("Inbox(<b style='font-size:14px;'>" + data.GeneralMessages + "</b>)");
                    $('#AppointmentMessages').html("Appointment(<b style='font-size:14px;'>" + data.AppointmentMessages + "</b>)");
                    $('#MedicationMessages').html("Refill Requests(<b style='font-size:14px;'>" + data.MedicationMessages + "</b>)");
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
                        $("#detail-inbox-request").text('');
                        $('#tabs-1 div:first').html(data.Msghtml)
                        $("#tbl-messageInbox").fixedHeaderTable();

                        //  $("#detail-inbox-request").hide();
                    }
                    else if (value == '34S') {
                        $("#detail-sent-request").text('');
                        $('#tabs-2 div:first').html(data.Msghtml)
                        $("#tbl-MessageSent").fixedHeaderTable();
                        // $("#detail-sent-request").hide();
                    }
                    else if (value == '34D') {
                        $("#detail-delete-request").text('');
                        $('#tabs-3 div:first').html(data.Msghtml)
                        $("#tbl-MessageDelete").fixedHeaderTable();
                        //  $("#detail-delete-request").hide();
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
var jqXHRData;
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

            //$('#MessageHeaderGrid').html(data.result.Msghtml);
            alert(data.result.Msghtml);
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

            data.formData = { FacilityId: $('#hdFacilityId').val(), PatientId: $('#hdPatientId').val(), CreatedByName: $('#hdCreatedByName').val(), MessageRequest: $('#hdMessageRequest').val(), MessageTypeId: $('#hdMessageTypeId').val(), MessageUrgencyId: $('#hdMessageUrgencyId').val(), Flag: $('#hdFlag').val() }; // e.g. {id: 123}

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

    // $("#txtPatient").on('change', function () {

    //     $("#hdPatientId").val('');
    // });
    $("#cmbAppointmentFacility").change(function () {
        $('#PatName').hide();
        $('#PatName').empty();
        // document.getElementById("txtPatient").value = "";
    });

    $("#txtPatient").autocomplete({

        select: function (event, ui) {
            $("#hdPatientId").val(ui.item.data);
            $("#PatName").text(ui.item.value);
            $("#PatName").show();
            //return false;
        }
    });

    $("#txtPatient").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "message-Patientlist-get",
                type: "POST",
                dataType: "json",
                data: { term: request.term, FacilityId: $.trim($('#cmbAppointmentFacility  option:selected').val()) },
                success: function (data) {
                    response($.map(data, function (item) {

                        return { data: item.PatientId, value: item.Name };
                    }))

                }
            })
        },
        messages: {
            noResults: "", results: ""
        }
    });

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
                    if ($('#hdPatientId').val() == '' || $('#PatName').text() == '') {
                        alert('Please Select Patient');
                        bValid = false;
                    }
                    if ($('#txtMessageWrite').val() == "" || $('#txtMessageWrite').val() == null) {
                        alert("Please enter a Message");
                        return false;
                    }


                    if (bValid) {


                        //ajax 
                        try {


                            if ($('#hdSentFlag').val() == 'Compose') {
                                // readBlob();
                                var textMessage = $.trim($('#txtMessageWrite').val());

                                var txt = textMessage.replace(/\n/g, "\\n");

                                var requestData = {

                                    AttachmentName: $.trim($('#files').val()),
                                    AttachmentTest: $('#ByteData').val(),
                                    FacilityId: $.trim($('#cmbAppointmentFacility  option:selected').val()),
                                    // ProviderId_To
                                    CreatedByName: $.trim($('#PatName').text()),
                                    PatientId: $.trim($('#hdPatientId').val()),
                                    MessageRequest: txt,
                                    MessageTypeId: $.trim($('#cmbMessageType  option:selected').val()),
                                    MessageUrgency: $.trim($('#cmbMessageUrgency  option:selected').val()),
                                    Flag: "message-compose"

                                };


                                $('#hdFacilityId').val($.trim($('#cmbAppointmentFacility  option:selected').val()));
                                $('#hdCreatedByName').val($.trim($('#PatName').text()));
                                // $('#PatientId').val($.trim($('#hdPatientId').val()));
                                $('#hdMessageRequest').val(txt);
                                $('#hdMessageTypeId').val($.trim($('#cmbMessageType  option:selected').val()));
                                $('#hdMessageUrgencyId').val($.trim($('#cmbMessageUrgency  option:selected').val()));
                                $('#hdFlag').val('message-compose');

                                if ($('#BrowseName').text() !== '') {
                                    if (jqXHRData) {

                                        var isStartUpload = true;
                                        var uploadFile = jqXHRData.files[0];

                                        if (!(/\.(jpg|jpeg|gif|png|doc|docx|xls|xlsx|pdf)$/i).test(uploadFile.name)) {
                                            alert('You must select an jpeg and pdf file only');
                                            isStartUpload = false;
                                            return false;
                                        } else
                                            if (uploadFile.size > 4000000) { // 4mb
                                                alert('Please upload a smaller image, max size is 4 MB');
                                                isStartUpload = false;
                                                return false;
                                            }
                                        if (isStartUpload) {

                                            jqXHRData.submit();
                                        }
                                    }
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
                                            alert("Message sent Successfully");
                                            //$("#MessageHeaderGrid").html(data);
                                            //$("#tbl-MessageInbox").fixedHeaderTable();


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

    $("#ReplyMessage").dialog
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
                   if ($('#txtMessageWrite1').val() == "" || $('#txtMessageWrite1').val() == null) {
                       alert("Please enter a Message");
                       return false;
                   }

                   if (bValid) {

                       //ajax 
                       try {
                           var textMessage = $.trim($('#txtMessageWrite1').val());

                           var txt = textMessage.replace(/\n/g, "\\n");
                           var requestData = {
                               MessageId: $.trim($('#hdReplyMessageId').val()),
                               FacilityId: $.trim($('#cmbAppointmentFacility1  option:selected').val()),
                               CreatedByName: $.trim($('#txtPatient1').text()),
                               PatientId: $.trim($('#cmbPatient  option:selected').val()),
                               MessageResponse: txt,
                               MessageTypeId: $.trim($('#cmbMessageType1  option:selected').val()),
                               MessageUrgency: $.trim($('#cmbMessageUrgency1  option:selected').val()),
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
                                   //$("#MessageDetailGrid").html(data.html);
                                   //$("#MessageHeaderGrid").html(data.MainGridHtml);
                                   alert(data.html);

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
               },
               Close: function () {
                   $(this).dialog("close");
               }
           },
           close: function () {
               $(this).dialog("close");
           }
       });

    $("#ReplyAttachMessage").dialog
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

               //  alert($("#hdReplyMessageId").val());
               if (bValid) {

                   //ajax 
                   try {

                       var requestData = {
                           MessageId: $.trim($('#hdReplyMessageId').val()),
                           AppointmentStart: $.trim($('#txtPrefDateFrom').val()),
                           PreferredTime: $.trim($('#cmbtime  option:selected').val()),
                           MessageResponse: $.trim($('#txtMessageWrite2').val()),
                           MessageStatusId: $.trim($('#cmbStatus  option:selected').val()),
                           MessageType: "Appointment Request"

                       };
                       $.ajax({
                           type: 'POST',
                           url: 'messages-attach-reply',
                           data: JSON.stringify(requestData),
                           dataType: 'json',
                           contentType: 'application/json; charset=utf-8',
                           success: function (data) {
                               // if success
                               alert(data.html);
                               //$("#AppintmentDetailGrid").html(data.html);
                              
                               //$("#AppointGrid").html(data.html1);
                               //$("#tbl-MessageAppointment").fixedHeaderTable();

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
           },
           Close: function () {
               $("#Appcount").text("");
               $(this).dialog("close");
           }
       },
       close: function () {
           $("#Appcount").text("");

           $(this).dialog("close");
       }
   });

    $("#ReplyRequestRefillMessage").dialog({
        autoOpen: false,
        show: {
            effect: "blind",
            duration: 1000
        },
        hide: {
            effect: "blind",
            duration: 1000
        },
        height: 690,
        width: 727,
        modal: true,
        buttons: {
            "Send": function () {

                var bValid = true;

                if ($('#txtMedicationName').val().length > 50) {
                    alert('Medication Name Length is not greater than 50.');
                    return false;
                }
                //  alert($("#hdReplyMessageId").val());
                if (bValid) {

                    //ajax 
                    try {

                        var requestData = {
                            MessageId: $.trim($('#hdReplyMessageId').val()),
                            MedicationName: $.trim($('#txtMedicationName').val()),
                            NoOfRefills: $.trim($('#txtCountRefills').val()),
                            MessageResponse: $.trim($('#txtComments').val()),
                            PharmacyAddress: $.trim($('#pharadd').val()),
                            PharmacyName: $.trim($('#pharname').val()),
                            PharmacyPhone: $.trim($('#pharphone').val()),
                            MessageStatusId: $.trim($('#cmbStatus1  option:selected').val())

                        };
                        $.ajax({
                            type: 'POST',
                            url: 'messages-request-refill-reply',
                            data: JSON.stringify(requestData),
                            dataType: 'json',
                            contentType: 'application/json; charset=utf-8',
                            success: function (data) {
                                // if success
                                alert(data.html);
                               // $("#RefillRequestDetailGrid").html(data.html);
                               // $("#MainRefillGrid").html(data.htmlMainGrid);


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
            },
            Close: function () {
                $("#refillcount").text("");
                $(this).dialog("close");
            }
        },
        close: function () {
            $("#refillcount").text("");

            $(this).dialog("close");
        }
    });

    $("#ReplyRequestReferralMessage").dialog
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
     height: 436,
     width: 727,
     modal: true,
     buttons: {
         "Send": function () {

             var bValid = true;

             //alert($("#hdReplyMessageId").val());
             if (bValid) {

                 //ajax 
                 try {

                     var requestData = {
                         MessageId: $.trim($('#hdReplyMessageId').val()),
                         //AppointmentStart: $.trim($('#txtPrefDateFromRef').val()),
                         //PreferredTime: $.trim($('#cmbtimeRef  option:selected').text()),
                         MessageResponse: $.trim($('#txtMessageRef').val())


                     };
                     $.ajax({
                         type: 'POST',
                         url: 'messages-request-referral-reply',
                         data: JSON.stringify(requestData),
                         dataType: 'json',
                         contentType: 'application/json; charset=utf-8',
                         success: function (data) {
                             // if success
                             alert(data.html);
                             //$("#ReferralRequestDetailGrid").html(data.html);
                             //$("#MainReferralGrid").html(data.htmlMainGrid);


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

    $("#counter").text("");
    $("#counter").append("You have <strong>" + 256 + "</strong> characters remaining");
    $("#counter1").text("");
    $("#counter1").append("You have <strong>" + 256 + "</strong> characters remaining");

    $('#hdSentFlag').val(type);
    if (type == 'Compose') {
        clearFields();
        $('#PatName').hide();
        $('#PatName').empty();
        // $("#cmbAppointmentFacilty").val('');

        // $("#txtPatient").val('');

        //// $("#cmbMessageType").val('');

        // $("#cmbMessageUrgency").val('');

        // alert('test');
    }
    if (type == 'Reply') {
        if ($('#hdReplyMessageDetailId').val() == '') {
            alert('Please select message before sending a reply.');
            return;
        }
        else {
            var mDetailId = $('#hdReplyMessageDetailId').val();


            try {

                var obj = jQuery.parseJSON($("#hdInboxDetailJson-" + mDetailId).val());
                $("#cmbAppointmentFacilty1").val(obj.FacilityId);
                $("#cmbAppointmentFacility1 ").attr('disabled', 'disabled');
                $("#txtPatient1").val(obj.PatientName);
                $("#txtPatient1 ").attr('disabled', 'disabled');
                $("#cmbMessageType1").val(obj.MessageTypeId);
                $("#cmbMessageType1 ").attr('disabled', 'disabled');
                $("#cmbMessageUrgency1").val(obj.MessageUrgency);
                $("#cmbMessageUrgency1").attr('disabled', 'disabled');
                $('#hdReplyMessageId').val(obj.MessageId);


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
    $('#btncancelupload').hide();
    //  document.getElementById('files').addEventListener('change', handleFileSelect, false);
    if (type == 'Reply') {
        $("#ReplyMessage").dialog("open");
    }
    else {
        $('#files').hide();
        //$('#my-simple-upload').replaceWith('<input id="my-simple-upload" type="file" value="" name="MyAttachFile"/>');
        $('#browse').val('Browse..');
        $('#BrowseName').text('');
        $("#ComposeMessage").dialog("open");
    }
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
    //$('#files').replaceWith('<input type="file" id="files" name="file"/>');
    //$('#ByteData').val('');
    //document.getElementById('files').addEventListener('change', handleFileSelect, false);
    // jqXHRData.abort();

    $('#BrowseName').text('');
    $('#btncancelupload').hide();
}
function ShowCancel() {
    $('#txtcancelupload').show();
}

$(".DeleteMessage").click(function (e) {
    e.preventDefault();
    var par = $(e.target).parent();
    alert(e.target.id);
    var mesgType = e.target.id;
    var sList = "";
    if (e.target.id == "SentDeleteMessage") {
        alert('Sent');
        $('input[name="chkS[]"]').each(function () {
            if (this.checked) {
                sList += $(this).attr('class') + ",";
            }

        });
    }
    else {
        alert('Inbox');
        $('input[name="chk[]"]').each(function () {
            if (this.checked) {
                sList += $(this).attr('class') + ",";
            }

        });
    }
    if (sList == '') {
        alert("first select message before deleting ");
    }
    else {
        //alert(sList);
        //ajax call start for deleting inbox grid
        ShowLoader();
        try {
            var requestData = {
                DeletedMessageIDs: sList,
                FacilityId: $.trim($('#cmbAppointmentFacility  option:selected').val()),
                ProviderId_To: $.trim($('#cmbAppointmentDoctor  option:selected').val()),
                MessageTypeId: $.trim($('#cmbMessageType  option:selected').val()),
                MessageUrgency: $.trim($('#cmbMessageUrgency  option:selected').val()),
                MessageType: mesgType
            };

            $.ajax({
                type: 'POST',
                url: 'inboxs-delete',
                data: JSON.stringify(requestData),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    // if success
                    // alert("Success : " + data);

                    if (mesgType == "SentDeleteMessage") {
                        $("#MessageSentHeaderGrid").html(data);
                        $("#tbl-MessageSent").fixedHeaderTable();
                    }
                    else {
                        $("#MessageHeaderGrid").html(data);
                        $("#tbl-messageInbox").fixedHeaderTable();
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

        } catch (err) {

            if (err && err !== "") {
                alert(err.message);
                HideLoader();
            }
        }


        //ajax call end for deleting inbox 


    }





}



);

//$("#btnCancel").click(function (e) {
//    //e.preventDefault();
//    var sList = "";
//    $('input[name="chk3[]"]').each(function () {
//        if (this.checked) {
//            sList += $(this).attr('class') + ",";
//        }

//    });
//    if (sList == '') {
//        alert("first select message before deleting ");
//    }
//    else {
//        //alert(sList);
//        //ajax call start for deleting inbox grid
//        ShowLoader();
//        try {

//            var requestData = {
//                DeletedMessageIDs: sList


//            };

//            $.ajax({
//                type: 'POST',
//                url: 'Appointment-Cancel',
//                data: JSON.stringify(requestData),
//                dataType: 'json',
//                contentType: 'application/json; charset=utf-8',
//                success: function (data) {
//                    // if success
//                    // alert("Success : " + data);
//                    $('#tabs-4 div:first').html(data);

//                    HideLoader();

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


//        //ajax call end for deleting inbox 


//    }





//}



//);

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


function Hidecmbstatus() {
   
    $("#cmbStatus option[value='1']").remove();
    $("#cmbStatus option[value='2']").remove();
    $("#cmbStatus option[value='4']").remove();
    $("#cmbStatus option[value='6']").remove();
    $("#cmbStatus option[value='7']").remove();
    $("#cmbStatus option[value='3']").attr("selected", "selected");
    

}


function Hidecmbstatus1() {

    $("#cmbStatus1 option[value='1']").remove();
    $("#cmbStatus1 option[value='2']").remove();
    $("#cmbStatus1 option[value='4']").remove();
    $("#cmbStatus1 option[value='6']").remove();
    $("#cmbStatus1 option[value='7']").remove();
    $("#cmbStatus1 option[value='3']").attr("selected", "selected");


}
var temp = null;
var objname = null;
var hasclass = null;
function ProviderinboxDetail(id, className, value) {
    var obj = id;
    hasclass = $("#MessageHeader-" + id).hasClass("selected").toString();
    if (hasclass == "false") {

        $("#MessageHeader-" + obj).addClass("selected").siblings().removeClass("selected");


        $("#MessageHeader-" + obj).removeClass("r1");
        $("#MessageHeader-" + obj).removeClass("r0");

        if (temp != null) {
            if (objname == 'r0') {
                $(temp).addClass("r0");
            }
            else {
                $(temp).addClass("r1");
            }
        }

        //  alert("false afdsafdsfafsdf");
        temp = ("#MessageHeader-" + obj);

        objname = className;
    }
    ProvidermessageInboxDetail(id, null);
}
function ProvidermessageInboxDetail(id, providerclass) {

    //  alert(providerclass);
    ShowLoader();
    // $("#MessageDetail" + id).removeClass("selected");
    var obj = jQuery.parseJSON($("#hdInboxDetailJson-" + id).val());
    if (obj.MessageRead == 'False') {
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

                        $('#MessageHeader-' + obj.ID + ' td div').css('color', '#666699');
                        $('#MessageHeader-' + obj.ID + ' td div').css('font-weight', 'normal');

                    $('#circle').html(data.TotalMessages);
                    $('#hdAppointmentMessages').html(data.AppointmentMessages);
                    $('#hdGeneralMessages').html(data.GeneralMessages);
                    $('#hdMedicationMessages').html(data.MedicationMessages);
                    $('#hdReferralMessages').html(data.ReferralMessages);
                    $('#hdTotalMessages').html(data.TotalMessages);



                    $('#GeneralMessages').html("Inbox(<b style='font-size:14px;'>" + data.GeneralMessages + "</b>)");
                    $('#AppointmentMessages').html("Appointment(<b style='font-size:14px;'>" + data.AppointmentMessages + "</b>)");
                    $('#MedicationMessages').html("Refill Requests(<b style='font-size:14px;'>" + data.MedicationMessages + "</b>)");
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
                    html = html + 'From:  ' + obj.PatientName;
                    html = html + '</div>';
                    html = html + '<div style="margin-top: 1px; margin-left: 90px; font-size: 12px; color: #999999;">';
                    html = html + 'To:  ' + obj.FacilityName;
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
            html = html + 'From:  ' + obj.PatientName;
            html = html + '</div>';
            html = html + '<div style="margin-top: 1px; margin-left: 90px; font-size: 12px; color: #999999;">';
            html = html + 'To:  ' + obj.FacilityName;
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
var tempsend = null;
var objnamesend = null;
var hasclasssend = null;
function ProviderSendDetail(id, classNamesend, value) {

    var obj = id;
    ShowLoader();
    hasclasssend = $("#SendMessage-" + id).hasClass("selected").toString();

    if (hasclasssend == "false") {

        $("#SendMessage-" + obj).addClass("selected").siblings().removeClass("selected");


        $("#SendMessage-" + obj).removeClass("r1");
        $("#SendMessage-" + obj).removeClass("r0");

        if (tempsend != null) {
            if (objnamesend == 'r0') {
                $(tempsend).addClass("r0");
            }
            else {
                $(tempsend).addClass("r1");
            }
        }

        //  alert("false afdsafdsfafsdf");
        tempsend = ("#SendMessage-" + obj);

        objnamesend = classNamesend;
    }

    ProviderSendmessageDetail(id, null);

}
function ProviderSendmessageDetail(id, providerclass) {

    //  alert(providerclass);
    ShowLoader();
    // $("#MessageDetail" + id).removeClass("selected");
    var obj = jQuery.parseJSON($("#hdSendDetailJson-" + id).val());
    //if (obj.MessageRead == 'False') {
    //    try {

    //        var requestData = {
    //            MessageDetailId: obj.ID,
    //            MessageRead: true
    //        };

    //        $.ajax({
    //            type: 'POST',
    //            url: 'Update-Message-Read-Flag',
    //            data: JSON.stringify(requestData),
    //            dataType: 'json',
    //            contentType: 'application/json; charset=utf-8',
    //            success: function (data) {

    //                $('#MessageHeader-' + obj.ID + ' td div').css('color', '#666699');
    //                $('#MessageHeader-' + obj.ID + ' td div').css('font-weight', 'normal');

    //                $('#circle').html(data.TotalMessages);
    //                $('#hdAppointmentMessages').html(data.AppointmentMessages);
    //                $('#hdGeneralMessages').html(data.GeneralMessages);
    //                $('#hdMedicationMessages').html(data.MedicationMessages);
    //                $('#hdReferralMessages').html(data.ReferralMessages);
    //                $('#hdTotalMessages').html(data.TotalMessages);



    //                $('#GeneralMessages').html("Inbox(<b style='font-size:14px;'>" + data.GeneralMessages + "</b>)");
    //                $('#AppointmentMessages').html("Appointment(<b style='font-size:14px;'>" + data.AppointmentMessages + "</b>)");
    //                $('#MedicationMessages').html("Refill Requests(<b style='font-size:14px;'>" + data.MedicationMessages + "</b>)");
    //                $('#ReferralMessages').html("Referral Requests(<b style='font-size:14px;'>" + data.ReferralMessages + "</b>)");

    //                var html = '<div id="Inform_Head" class="ui-corner-all">';
    //                html = html + '<h4 style="padding-top: 10px; margin-left: 10px;">';
    //                html = html + '<b>' + obj.MessageType + '</b></h4>';
    //                html = html + '<div style=\"float: right; margin-top: -30px; margin-right:10px; font-size: 12px; color: #999999;\">';

    //                var today = new Date();
    //                var dd = today.getDate();
    //                var mm = today.getMonth() + 1; //January is 0!

    //                var yyyy = today.getFullYear();
    //                if (dd < 10) { dd = '0' + dd } if (mm < 10) { mm = '0' + mm } today = mm + '/' + dd + '/' + yyyy;

    //                html = html + today;
    //                html = html + '</div>';
    //                html = html + '<div style="margin-top: 1px; margin-left: 50px; font-size: 12px; color: #999999;">';
    //                html = html + 'From:  ' + obj.FacilityName;
    //                html = html + '</div>';
    //                html = html + '<div style="margin-top: 1px; margin-left: 90px; font-size: 12px; color: #999999;">';
    //                html = html + 'To:  ' + obj.PatientName;
    //                html = html + '</div>';
    //                html = html + '</div>';
    //                //<!--End of reply part-->
    //                html = html + '<div id="reply" style="height: 550px;" class="ui-corner-all">';
    //                html = html + '<div style="margin-left: 30px;">';
    //                html = html + '        <br>';
    //                html = html + '        <br>';
    //                html = html + obj.MessageType;
    //                html = html + '                        <br>';
    //                html = html + 'Facility Name :' + obj.FacilityName;
    //                html = html + '                        <br>';
    //                if (obj.MessageRequest != '') {
    //                    html = html + 'Message: ' + obj.MessageRequest;
    //                }
    //                else {
    //                    html = html + 'Message: ' + obj.MessageResponse;
    //                }
    //                html = html + '                        <br>';
    //                html = html + '    </div>';
    //                html = html + '</div>';



    //                $("#detail-inbox-request").show();
    //                $("#detail-inbox-request").html(html);
    //                $("#detail-sent-request").show();
    //                $("#detail-sent-request").html(html);
    //                $("#detail-delete-request").show();
    //                $("#detail-delete-request").html(html);



    //                HideLoader();

    //            },
    //            error: function (xhr, ajaxOptions, thrownError) {
    //                //if error

    //                alert('Error : ' + xhr.message);
    //                HideLoader();
    //            },
    //            complete: function (data) {
    //                // if completed
    //                HideLoader();

    //            },
    //            async: true,
    //            processData: false
    //        });
    //    } catch (err) {

    //        if (err && err !== "") {
    //            alert(err.message);
    //            HideLoader();
    //        }
    //    }
    //}
    //else {
        try {
            var html = '<div id="Inform_Head" class="ui-corner-all">';
            html = html + '<h4 style="padding-top: 10px; margin-left: 10px;">';
            if (obj.MessageType == "Billing Message") {
                html = html + '<b>Billing Message Response</b></h4>';
            }
            else if (obj.MessageType == "General Inquiry") {
                html = html + '<b>General Inquiry Response</b></h4>';
              
            }
            else if (obj.MessageType == "Appointment Request")
            {
                html = html + '<b>Appointment Request Response</b></h4>';
            }
            else if (obj.MessageType == "Referral Message") {
                html = html + '<b>Referral Request Response</b></h4>';
            }
            else {

                html = html + '<b>Refill Request Response</b></h4>';
            }
          
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
            html = html + '<div style="margin-top: 1px; margin-left: 90px; font-size: 12px; color: #999999;">';
            html = html + 'To:  ' + obj.PatientName;
            html = html + '</div>';
            html = html + '</div>';
            //<!--End of reply part-->
            html = html + '<div id="reply" style="height: 550px;" class="ui-corner-all">';
            html = html + '<div style="margin-left: 30px;">';
            html = html + '        <br>';
            html = html + '        <br>';
            if (obj.MessageType == "Billing Message") {
                html = html + 'Billing Message Response';
            }
            else if (obj.MessageType == "General Inquiry") {
                html = html + 'General Inquiry Response';

            }
            else if (obj.MessageType == "Appointment Request") {
                html = html + 'Appointment Request Response';
            }
            else if (obj.MessageType == "Referral Message") {
                html = html + 'Referral Request Response';
            }
            else {

                html = html + 'Refill Request Response</h4>';
            }
         //   html = html + obj.MessageType;
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

//}
var hasclassappointment = null;
var apptemp = null;
var appobjname = null;
function ProviderAppointmentDetail(id, className) {
    // alert(id);
    //temp = null;
    //objname = null;
    //ShowLoader();
    hasclassappointment = $("#AppointmentMessage-" + id).hasClass("selected").toString();
    if (hasclassappointment == "false") {
        $("#AppointmentMessage-" + id).addClass("selected").siblings().removeClass("selected");
        //alert("#MessageHeader-" + id);
        $("#AppointmentMessage-" + id).removeClass("r1");
        $("#AppointmentMessage-" + id).removeClass("r0");

        if (apptemp != null) {
            if (appobjname == 'r0') {
                $(apptemp).addClass("r0");
            }
            else {
                $(apptemp).addClass("r1");
            }
        }
        apptemp = ("#AppointmentMessage-" + id);
        appobjname = className;
    }
    ProvidermessageAppointmentDetail(id, null);
}
function ProvidermessageAppointmentDetail(id, Appointmetnclass) {

    //  alert(id);
    ShowLoader();
    var obj = jQuery.parseJSON($("#hdAppointmentDetailJson-" + id).val());
    if (obj.MessageRead == 'False') {
        //  alert("hi");
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
                    //    //   alert('found');
                    //}
                    //else {

                    $('#AppointmentMessage-' + obj.ID + ' td').css('color', '#666699');
                    $('#AppointmentMessage-' + obj.ID + ' td ').css('font-weight', 'normal');
                    //}

                    $('#circle').html(data.TotalMessages);
                    $('#hdAppointmentMessages').html(data.AppointmentMessages);
                    $('#hdGeneralMessages').html(data.GeneralMessages);
                    $('#hdMedicationMessages').html(data.MedicationMessages);
                    $('#hdReferralMessages').html(data.ReferralMessages);
                    $('#hdTotalMessages').html(data.TotalMessages);

                    $('#GeneralMessages').html("Inbox(<b style='font-size:14px;'>" + data.GeneralMessages + "</b>)");
                    $('#AppointmentMessages').html("Appointment(<b style='font-size:14px;'>" + data.AppointmentMessages + "</b>)");
                    $('#MedicationMessages').html("Refill Requests(<b style='font-size:14px;'>" + data.MedicationMessages + "</b>)");
                    $('#ReferralMessages').html("Referral Requests(<b style='font-size:14px;'>" + data.ReferralMessages + "</b>)");

                    var html = '<div id="Inform_Head" class="ui-corner-all">';
                    html = html + '<h4 style="padding-top: 10px; margin-left: 30px;">';
                    //if (obj.ProviderIdFrom !== '0') {
                    //    html = html + '<b>Appointment Request Response</b></h4>';
                    //}
                    //else {
                    //    html = html + '<b>Appointment Request</b></h4>';
                    //}
                    html = html + '<b>' + obj.MessageType + '</b></h4>';
                    html = html + '<div style=\"float: right; margin-top: -30px; margin-right:10px; font-size: 12px; color: #999999;\">';

                    var today = new Date();
                    var dd = today.getDate();
                    var mm = today.getMonth() + 1; //January is 0!

                    var yyyy = today.getFullYear();
                    if (dd < 10) { dd = '0' + dd } if (mm < 10) { mm = '0' + mm } today = mm + '/' + dd + '/' + yyyy;

                    html = html + today;
                    html = html + '</div>';
                    html = html + '<div style="margin-top: 1px; margin-left: 30px; font-size: 12px; color: #999999;">';
                    html = html + 'From:  ' + obj.PatientName;
                    html = html + '</div>';
                    html = html + '<div style="margin-top: 1px; margin-left: 30px; font-size: 12px; color: #999999;">';
                    html = html + 'To:  ' + obj.FacilityName;
                    html = html + '</div>';
                    html = html + '</div>';
                    //<!--End of reply part-->
                    html = html + '<div id="reply" style="height: 570px;" class="ui-corner-all">';
                    html = html + '<div style="margin-left: 30px;">';
                    html = html + '        <br>';
                    //if (obj.ProviderIdFrom !== '0') {
                    //    html = html + 'Message: ' + obj.MessageResponse;
                    //    html = html + '                        <br>';
                    //    html = html + 'Preffered Time :' + obj.PreferredTime;
                    //    html = html + '                        <br>';
                    //    html = html + 'Appointment Start :' + obj.AppointmentStart;
                    //    html = html + '                        <br>';
                    //}

                    //else {
                        var urgent = obj.MessageUrgency;
                        if (urgent == "False") {
                            html = html + 'Urgent : No';
                            html = html + '                        <br>';
                        }
                        else {
                            html = html + 'Urgent : Yes';
                            html = html + '                        <br>';

                        }
                        html = html + 'Preferred Date Range : ' + obj.AppointmentStart + ' to ' + obj.AppointmentEnd;
                        html = html + '                        <br>';
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
                        html = html + 'Preferred Day : ' + dayval;
                        html = html + '                        <br>';
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
                        html = html + 'Preffered Time of Day : ' + periodval;
                        html = html + '                        <br>';
                        html = html + 'Preffered Time : ' + obj.PreferredTime;
                        html = html + '                        <br>';

                        html = html + 'Reason For Visit: ' + obj.VisitReason;
                        html = html + '                        <br>';

                        html = html + 'Comments: ' + obj.MessageRequest;
                        html = html + '                        <br>';
                    //}

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
            html = html + '<h4 style="padding-top: 10px; margin-left: 30px;">';
            //if (obj.ProviderIdFrom !== '0') {
            //    html = html + '<b>Appointment Request Response</b></h4>';
            //}
            //else {
            //    html = html + '<b>Appointment Request</b></h4>';
            //}
            html = html + '<b>' + obj.MessageType + '</b></h4>';
            html = html + '<div style=\"float: right; margin-top: -30px; margin-right:10px; font-size: 12px; color: #999999;\">';

            var today = new Date();
            var dd = today.getDate();
            var mm = today.getMonth() + 1; //January is 0!

            var yyyy = today.getFullYear();
            if (dd < 10) { dd = '0' + dd } if (mm < 10) { mm = '0' + mm } today = mm + '/' + dd + '/' + yyyy;

            html = html + today;
            html = html + '</div>';
            html = html + '<div style="margin-top: 1px; margin-left: 30px; font-size: 12px; color: #999999;">';
            html = html + 'From:  ' + obj.PatientName;
            html = html + '</div>';
            html = html + '<div style="margin-top: 1px; margin-left: 30px; font-size: 12px; color: #999999;">';
            html = html + 'To:  ' + obj.FacilityName;
            html = html + '</div>';
            html = html + '</div>';
            //<!--End of reply part-->
            html = html + '<div id="reply" style="height: 570px;" class="ui-corner-all">';
            html = html + '<div style="margin-left: 30px;">';
            html = html + '        <br>';
            //if (obj.ProviderIdFrom !== '0') {
            //    html = html + '                        <br>';
            //    html = html + 'Date :' + obj.AppointmentStart;
            //    html = html + '                        <br>';

            //    html = html + 'Time :' + obj.PreferredTime;
            //    html = html + '                        <br>';

            //    html = html + 'Message Response : ' + obj.MessageResponse;
            //    html = html + '                        <br>';
            //}

            //else {

                var urgent1 = obj.MessageUrgency;
                if (urgent1 == "False") {
                    html = html + 'Urgent : No';
                    html = html + '                        <br>';
                }
                else {
                    html = html + 'Urgent : Yes';
                    html = html + '                        <br>';

                }
                html = html + 'Preferred Date Range :' + obj.AppointmentStart + ' to ' + obj.AppointmentEnd;
                html = html + '                        <br>';
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
                html = html + 'Preferred Day :' + dayval;
                html = html + '                        <br>';
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
                html = html + 'Preffered Time of Day :' + periodval;
                html = html + '                        <br>';
                html = html + 'Preffered Time :' + obj.PreferredTime;
                html = html + '                        <br>';

                html = html + 'Reason For Visit: ' + obj.VisitReason;
                html = html + '                        <br>';

                html = html + 'Comments:' + obj.MessageRequest;
            //}

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
var hasclassrefil = null;
var refiltemp = null;
var refilobjname = null;
function ProvidermedicationRefillDetail(id, className) {
    //tempRefill = null;
    //ShowLoader();
    hasclassrefil = $("#RefillMessageHeader-" + id).hasClass("selected").toString();
    if (hasclassrefil == "false") {
        $("#RefillMessageHeader-" + id).addClass("selected").siblings().removeClass("selected");

        //alert("#MessageHeader-" + id);
        $("#RefillMessageHeader-" + id).removeClass("r1");
        $("#RefillMessageHeader-" + id).removeClass("r0");

        if (refiltemp != null) {
            if (refilobjname == 'r0') {
                $(refiltemp).addClass("r0");
            }
            else {
                $(refiltemp).addClass("r1");
            }
        }
        refiltemp = ("#RefillMessageHeader-" + id);
        refilobjname = className;
    }
    ProvidermessageRequestRefillDetail(id, null);
}
function ProvidermessageRequestRefillDetail(id, Refillclass) {

    //  alert(id);
    ShowLoader();
    var obj = jQuery.parseJSON($("#hdRefillDetailJson-" + id).val());
    if (obj.MessageRead == 'False') {
        try {
            // alert("if");
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
                    //    //alert('found');
                    //}
                    //else {

                    $('#RefillMessageHeader-' + obj.ID + ' td div').css('color', '#666699');
                    $('#RefillMessageHeader-' + obj.ID + ' td div').css('font-weight', 'normal');
                    //}

                    $('#circle').html(data.TotalMessages);
                    $('#hdAppointmentMessages').html(data.AppointmentMessages);
                    $('#hdGeneralMessages').html(data.GeneralMessages);
                    $('#hdMedicationMessages').html(data.MedicationMessages);
                    $('#hdReferralMessages').html(data.ReferralMessages);
                    $('#hdTotalMessages').html(data.TotalMessages);

                    $('#GeneralMessages').html("Inbox(<b style='font-size:14px;'>" + data.GeneralMessages + "</b>)");
                    $('#AppointmentMessages').html("Appointment(<b style='font-size:14px;'>" + data.AppointmentMessages + "</b>)");
                    $('#MedicationMessages').html("Refill Requests(<b style='font-size:14px;'>" + data.MedicationMessages + "</b>)");
                    $('#ReferralMessages').html("Referral Requests(<b style='font-size:14px;'>" + data.ReferralMessages + "</b>)");

                    var html = '<div id="Inform_Head" class="ui-corner-all">';
                    html = html + '<h4 style="padding-top: 10px; margin-left: 30px;">';
                    html = html + '<b>Refill Request</b></h4>';
                    //if (obj.ProviderIdFrom !== '0') {
                    //    html = html + '<b>Refill Request Response</b></h4>';
                    //}
                    //else {
                    //    html = html + '<b>Refill Request</b></h4>';
                    //}
                    html = html + '<div style=\"float: right; margin-top: -30px; margin-right:10px; font-size: 12px; color: #999999;\">';

                    var today = new Date();
                    var dd = today.getDate();
                    var mm = today.getMonth() + 1; //January is 0!

                    var yyyy = today.getFullYear();
                    if (dd < 10) { dd = '0' + dd } if (mm < 10) { mm = '0' + mm } today = mm + '/' + dd + '/' + yyyy;

                    html = html + today;
                    html = html + '</div>';
                    html = html + '<div style="margin-top: 1px; margin-left: 50px; font-size: 12px; color: #999999;">';
                    html = html + 'From:  ' + obj.PatientName;
                    html = html + '</div>';
                    html = html + '<div style="margin-top: 1px; margin-left: 50px; font-size: 12px; color: #999999;">';
                    html = html + 'To:  ' + obj.FacilityName;
                    html = html + '</div>';
                    html = html + '</div>';
                    //<!--End of reply part-->
                    //<!--End of reply part-->
                    html = html + '<div id=\"reply\" style=\"height: 625px;\" class=\"ui-corner-all\">';
                    html = html + '<div style=\"margin-left: 30px;margin-top:-25px;\">';
                    html = html + '        <br>';
                    html = html + '        <br>';
                    //html = html + 'Medication Refill Request';
                    //html = html + '                        <br>';
                    //html = html + 'Patient :' + obj.CreatedByName;
                    //html = html + '                       <br>';
                    html = html + 'Medication :' + obj.MedicationName;
                    html = html + '                        <br>';
                    html = html + 'No. Of Refills :' + obj.NoOfRefills;
                    html = html + '                        <br>';
                    //html = html + 'Provider :  ' + obj.To;
                    //html = html + '                        <br>';
                    html = html + 'Pharmacy Name :' + obj.PharmacyName;
                    html = html + '                        <br>';
                    html = html + 'Pharmacy Phone Number :' + obj.PharmacyPhone;
                    html = html + '                        <br>';
                    html = html + 'Pharmacy Address :' + obj.PharmacyAddress;
                    html = html + '                        <br>';
                    //html = html + 'No. Of Refills :' + obj.NoOfRefills;
                    //html = html + '                        <br>';
                    //   html = html + '                        <br>';

                    html = html + 'Comments : ' + obj.MessageRequest;
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
            html = html + '<h4 style="padding-top: 10px; margin-left: 30px;">';
            html = html + '<b>Refill Request</b></h4>';
            //if (obj.ProviderIdFrom !== '0') {
            //    html = html + '<b>Refill Request Response</b></h4>';
            //}
            //else {
            //    html = html + '<b>Refill Request</b></h4>';
            //}
            html = html + '<div style=\"float: right; margin-top: -30px; margin-right:10px; font-size: 12px; color: #999999;\">';

            var today = new Date();
            var dd = today.getDate();
            var mm = today.getMonth() + 1; //January is 0!

            var yyyy = today.getFullYear();
            if (dd < 10) { dd = '0' + dd } if (mm < 10) { mm = '0' + mm } today = mm + '/' + dd + '/' + yyyy;

            html = html + today;
            html = html + '</div>';
            html = html + '<div style="margin-top: 1px; margin-left: 50px; font-size: 12px; color: #999999;">';
            html = html + 'From:  ' + obj.PatientName;
            html = html + '</div>';
            html = html + '<div style="margin-top: 1px; margin-left: 50px; font-size: 12px; color: #999999;">';
            html = html + 'To:  ' + obj.FacilityName;
            html = html + '</div>';
            html = html + '</div>';
            //<!--End of reply part-->
            //<!--End of reply part-->
            html = html + '<div id=\"reply\" style=\"height: 625px;\" class=\"ui-corner-all\">';
            html = html + '<div style=\"margin-left: 30px;margin-top:-25px;\">';
            html = html + '        <br>';
            html = html + '        <br>';
            //html = html + 'Medication Refill Request';
            //html = html + '                        <br>';
            //html = html + 'Patient :' + obj.CreatedByName;
            //html = html + '                       <br>';
            html = html + 'Medication :' + obj.MedicationName;
            html = html + '                        <br>';
            //   html = html + '                        <br>';
            html = html + 'No. Of Refills :' + obj.NoOfRefills;
            html = html + '                        <br>';
            //html = html + 'Provider :  ' + obj.To;
            //html = html + '                        <br>';
            html = html + 'Pharmacy Name :' + obj.PharmacyName;
            html = html + '                        <br>';
            html = html + 'Pharmacy Phone Number :' + obj.PharmacyPhone;
            html = html + '                        <br>';
            html = html + 'Pharmacy Address :' + obj.PharmacyAddress;
            //html = html + '                        <br>';
            //html = html + 'No. Of Refills :' + obj.NoOfRefills;
            //html = html + '                        <br>';
            html = html + '                        <br>';

            html = html + 'Comments : ' + obj.MessageResponse;
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
var hasclassReferall = null;
function ProviderReferralRequestDetail(id, className) {
    tempReferall = null;
    ShowLoader();
    hasclassReferall = $("#ReferralMessageHeader-" + id).hasClass("selected").toString();
    if (hasclassReferall == "false") {
        $("#ReferralMessageHeader-" + id).addClass("selected").siblings().removeClass("selected");
        $("#ReferralMessageHeader-" + id).removeClass("r1");
        $("#ReferralMessageHeader-" + id).removeClass("r0");

        if (temp != null) {
            if (objname == 'r0') {
                $(temp).addClass("r0");
            }
            else {
                $(temp).addClass("r1");
            }
        }
        temp = ("#ReferralMessageHeader-" + id);
        objname = className;
    }
    ProvidermessageReferralRequestDetail(id, null);
}
function ProvidermessageReferralRequestDetail(id, Referallclass) {

    //  alert(id);
    ShowLoader();
    var obj = jQuery.parseJSON($("#hdReferralDetailJson-" + id).val());
    if (obj.MessageRead == "False") {
        if (obj.ProviderIdTo != '0') {
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
                        //    // alert('found');
                        //}
                        //else {

                        $('#ReferralMessageHeader-' + obj.ID + ' td div').css('color', '#666699');
                        $('#ReferralMessageHeader-' + obj.ID + ' td div').css('font-weight', 'normal');
                        // }
                        $('#circle').html(data.TotalMessages);
                        $('#hdAppointmentMessages').html(data.AppointmentMessages);
                        $('#hdGeneralMessages').html(data.GeneralMessages);
                        $('#hdMedicationMessages').html(data.MedicationMessages);
                        $('#hdReferralMessages').html(data.ReferralMessages);
                        $('#hdTotalMessages').html(data.TotalMessages);

                        $('#GeneralMessages').html("Inbox(<b style='font-size:14px;'>" + data.GeneralMessages + "</b>)");
                        $('#AppointmentMessages').html("Appointment(<b style='font-size:14px;'>" + data.AppointmentMessages + "</b>)");
                        $('#MedicationMessages').html("Refill Requests(<b style='font-size:14px;'>" + data.MedicationMessages + "</b>)");
                        $('#ReferralMessages').html("Referral Requests(<b style='font-size:14px;'>" + data.ReferralMessages + "</b>)");

                        var html = '<div id="Inform_Head" class="ui-corner-all">';
                        html = html + '<h4 style="padding-top: 10px; margin-left: 10px;">';

                        html = html + '<b>Referral Request</b></h4>';
                        html = html + '<div style=\"float: right; margin-top: -30px; margin-right:10px; font-size: 12px; color: #999999;\">';

                        var today = new Date();
                        var dd = today.getDate();
                        var mm = today.getMonth() + 1; //January is 0!

                        var yyyy = today.getFullYear();
                        if (dd < 10) { dd = '0' + dd } if (mm < 10) { mm = '0' + mm } today = mm + '/' + dd + '/' + yyyy;

                        html = html + today;
                        html = html + '</div>';
                        html = html + '<div style="margin-top: 1px; margin-left: 10px; font-size: 12px; color: #999999;">';
                        html = html + 'From:  ' + obj.PatientName;
                        html = html + '</div>';
                        html = html + '<div style="margin-top: 1px; margin-left: 10px; font-size: 12px; color: #999999;">';
                        html = html + 'To:  ' + obj.FacilityName;
                        html = html + '</div>';
                        html = html + '</div>';
                        //<!--End of reply part-->
                        html = html + '<div id="reply" style="height: 570px;" class="ui-corner-all">';
                        html = html + '<div style="margin-left: 30px;">';
                        html = html + '        <br>';
                        html = html + '        <br>';


                        html = html + 'Reason for Referral : ' + obj.MessageRequest;
                        html = html + '        <br>';
                        html = html + 'Comments :' + obj.VisitReason;

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
    }
    else {
        try {
            var html = '<div id="Inform_Head" class="ui-corner-all">';
            html = html + '<h4 style="padding-top: 10px; margin-left: 10px;">';

            html = html + '<b>Referral Request</b></h4>';
            html = html + '<div style=\"float: right; margin-top: -30px; margin-right:10px; font-size: 12px; color: #999999;\">';

            var today = new Date();
            var dd = today.getDate();
            var mm = today.getMonth() + 1; //January is 0!

            var yyyy = today.getFullYear();
            if (dd < 10) { dd = '0' + dd } if (mm < 10) { mm = '0' + mm } today = mm + '/' + dd + '/' + yyyy;

            html = html + today;
            html = html + '</div>';
            html = html + '<div style="margin-top: 1px; margin-left: 10px; font-size: 12px; color: #999999;">';
            html = html + 'From:  ' + obj.PatientName;
            html = html + '</div>';
            html = html + '<div style="margin-top: 1px; margin-left: 10px; font-size: 12px; color: #999999;">';
            html = html + 'To:  ' + obj.FacilityName;
            html = html + '</div>';
            html = html + '</div>';
            //<!--End of reply part-->
            html = html + '<div id="reply" style="height: 570px;" class="ui-corner-all">';
            html = html + '<div style="margin-left: 30px;">';
            html = html + '        <br>';
            html = html + '        <br>';

            html = html + 'Reason for Referral : ' + obj.MessageRequest;
            html = html + '        <br>';
            html = html + 'Comments :' + obj.VisitReason;

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


function checkUrl(url) {
    // var pattern = /^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%&]}).*$/;
    var pattern = /^.*(?=.{7,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&+=]).*$/;


    if (pattern.test(url)) {

        return true;
    } else {

        return false;
    }
}