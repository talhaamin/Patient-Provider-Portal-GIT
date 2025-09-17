$(document).ready(function () {
    $("#tbl-App").fixedHeaderTable();
});

$(function () {

    tips = $(".validateTips");



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
    //$("#createappbtn")
    //.button()
    //.click(function () {
    //    $("#appbtn-form").dialog("open");
    //});
    //$("#createaddappointments")
    //.button()
    //.click(function () {
    //    $("#appbtn-form").dialog("open");
    //});

});

//- end of appointment button script


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
        $('#cmbAppointmentFacility1').val($.trim(obj.FacilityId));
        $('#cmbAppointmentDoctor1').val($.trim(obj.ProviderId_To));
        $('#chkmor').prop('checked', chkstr(obj.PreferredPeriod[0]));
        $('#chkAft').prop('checked', chkstr(obj.PreferredPeriod[1]));
        $('#chkeve').prop('checked', chkstr(obj.PreferredPeriod[2]));
        $('#cmbMessageUrgency1').val($.trim(obj.MessageUrgencyId));
        $('#chkmon').prop('checked', chkstr(obj.PreferredWeekDay[0]));
        $('#chktue').prop('checked', chkstr(obj.PreferredWeekDay[1]));
        $('#chkwed').prop('checked', chkstr(obj.PreferredWeekDay[2]));
        $('#chkthu').prop('checked', chkstr(obj.PreferredWeekDay[3]));
        $('#chkfri').prop('checked', chkstr(obj.PreferredWeekDay[4]));
        $('#txtPrefDateFrom').val($.trim(obj.AppointmentStart));
        $('#txtPrefDateTo').val($.trim(obj.AppointmentEnd));
        $('#cmbPreftime option:selected').text($.trim(obj.PreferredTime));
        $('#txtReason').val($.trim(obj.VisitReason));
        $('#txtComment').val($.trim(obj.MessageRequest));


        $('#cmbAppointmentFacility1').attr("disabled", true);
        $('#cmbAppointmentDoctor1').attr("disabled", true);
        $('#chkmor').attr("disabled", true);
        $('#chkAft').attr("disabled", true);
        $('#chkeve').attr("disabled", true);
        $('#cmbMessageUrgency1').attr("disabled", true);
        $('#chkmon').attr("disabled", true);
        $('#chktue').attr("disabled", true);
        $('#chkwed').attr("disabled", true);
        $('#chkthu').attr("disabled", true);
        $('#chkfri').attr("disabled", true);
        $('#txtPrefDateFrom').attr("disabled", true);
        $('#txtPrefDateTo').attr("disabled", true);
        $('#cmbPreftime').attr("disabled", true);
        $('#txtReason').attr("disabled", true);
        $('#txtComment').attr("disabled", true);

    } catch (e) {
        alert(e.message);
    }
    $("#appbtn-form").dialog("open");
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
                $("#MessageHeaderGrid").html(data);
                $("#tbl-App").fixedHeaderTable();
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
