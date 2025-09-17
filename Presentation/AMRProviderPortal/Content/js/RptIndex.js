$(document).ready(function () {

    // first example
    $("#browser").treeview();

});
$(function () {
    $("input[type=submit], a, button")
    .button()
    .click(function (event) {
        // event.preventDefault();
    });
});


$(document).ready(function () {

    $("#txtDateFromRep").datepicker({
        dateFormat: "dd/mm/yy",
        onSelect: function (dateText, instance) {
            date = $.datepicker.parseDate(instance.settings.dateFormat, dateText, instance.settings);
            date.setDate(date.getDate() + 90);
            $("#txtDateToRep").datepicker("setDate", date);
        }
    });
    $("#txtDateToRep").datepicker({
        dateFormat: "dd/mm/yy"
    });


    $("#dialog-Report").dialog
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

     height: 300,
     width: 650,
     modal: true,
     buttons: {
         "Run": function () {

             var bValid = true;
             var $MU = $("#dialog-Report");


             if (bValid) {


                 //ajax 
                 try {


                     var requestData = {


                         Year: $('#cmbYear').val(),

                         FacilityId: $.trim($('#cmbAppointmentFacility  option:selected').val()),
                         QYear: $('#cmbQYear option:selected').val(),
                         Quarter: $.trim($('#cmbQuarter option:selected').text()),
                         FromDate: $.trim($('#txtDateFromRep').val()),
                         ToDate: $.trim($('#txtDateToRep').val()),
                         MU: $MU.data('MU'),
                         ReportPath: $MU.data('ReportPath')

                     };


                     $.ajax({
                         type: 'POST',
                         url: 'report-parameters',
                         data: JSON.stringify(requestData),
                         dataType: 'json',
                         contentType: 'application/json; charset=utf-8',
                         success: function (data) {

                             HideLoader();
                             var page = 'ProviderPortalQA/Reports/ReportForm.aspx';
                             //data;

                             $('#page_loader_Report').remove();
                             var $dialog = $('<div style="width:1000px;"></div>')
                                            .html('<div id="page_loader_Report" style="display: block;"><div id="apDiv1" align="center"><table width="200" border="0" cellspacing="0" cellpadding="0"><tr><td height="70px;">&nbsp;</td></tr><tr><td align="center"><img src="Content/img/image_559926.gif" width="128" height="64" /></td></tr><tr><td style="text-align: center;" height="70px;">Please wait ...</td></tr></table></div></div><iframe style="border: 0px; " src="' + page + '" width="100%" height="100%"></iframe>')
                                            .dialog({
                                                autoOpen: false,
                                                show: {
                                                    effect: "blind",
                                                    duration: 1000
                                                },
                                                hide: {
                                                    effect: "blind",
                                                    duration: 1000
                                                },
                                                modal: true,
                                                height: 600,
                                                width: 950,
                                                title: "Report"
                                            });
                             $dialog.dialog('open');

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
     close: function () {
         $(this).dialog("close");
     }
 });
    // Call horizontalNav on the navigations wrapping element
    $('.full-width').horizontalNav({});
});

function pop_up_report(id) {

    //ShowLoader();
    try {

        var requestData = {

            Id: id,

        };
        alert(JSON.stringify(requestData));
        $.ajax({
            type: 'POST',
            url: 'problemlistreport-index',
            data: JSON.stringify(requestData),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                // if success
                // alert("Success : " + data);
                // $("#current-med-grid").html(data);

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
function test(MU, ReportPath) {
    clearFields();
    $('#cmbYear').attr('disabled', true);
    $('#cmbQYear').attr('disabled', true);
    $('#cmbQuarter').attr('disabled', true);
    $('#txtDateFromRep').attr('disabled', true);
    $('#txtDateToRep').attr('disabled', true);

    $('#chkYear').change(function () {
        $('#cmbYear').attr('disabled', false);
        $('#cmbQYear').attr('disabled', true);
        $('#cmbQuarter').attr('disabled', true);
        $('#txtDateFromRep').attr('disabled', true);
        $('#txtDateToRep').attr('disabled', true);
    });

    $('#chkQuarter').change(function () {
        $('#cmbQYear').attr('disabled', false);
        $('#cmbQuarter').attr('disabled', false);
        $('#cmbYear').attr('disabled', true);
        $('#txtDateFromRep').attr('disabled', true);
        $('#txtDateToRep').attr('disabled', true);
    });

    $('#chkDate').change(function () {
        $('#txtDateFromRep').attr('disabled', false);
        $('#txtDateToRep').attr('disabled', true);
        $('#cmbYear').attr('disabled', true);
        $('#cmbQYear').attr('disabled', true);
        $('#cmbQuarter').attr('disabled', true);
    });
    $("#dialog-Report").data('MU', MU);
    $("#dialog-Report").data('ReportPath', ReportPath);
    $("#dialog-Report").dialog("open");
}
