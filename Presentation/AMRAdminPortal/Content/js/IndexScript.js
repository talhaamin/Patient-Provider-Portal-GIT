function viewLabResult(id) {
    ShowLoader();

    try {
        var obj = jQuery.parseJSON($('#hdlab-' + id).val());

    } catch (e) {
        alert(e.message);
    }





    $('#lblFacility').html(obj.FacilityId);
    $('#lblOrderDate').html(obj.OrderDate);
    $('#lblProvider').html(obj.ProviderId_Requested);
    $('#lblCollectionDate').html(obj.CollectionDate);
    $('#lblRequisition').html(obj.Requisiton);
    $('#lblReportDate').html(obj.ReportDate);
    $('#lblSpecimen').html(obj.Specimen);
    $('#lblReviewDate').html(obj.ReviewDate);
    $('#lblSpecimenSource').html(obj.SpecimenSource);
    $('#lblReviewer').html(obj.Reviewer);
    $('#lblTest').html(obj.LabName);

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

                    alert('Error : ' + xhr.message);
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
            duration: 1000
        },
        hide: {
            effect: "blind",
            duration: 1000
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

    //Dates for Problem List
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

function appointment_move() {
    window.location.href = 'appointments-index';
}

function lab_move() { //done
    window.location.href = 'clinical-summary?id=3';
    $("#tabs").tabs({
        active: 3
    });
}
function visit_move() {//done
    window.location.href = 'clinical-summary?id=2';
    $("#tabs").tabs({
        active: 2
    });
}
function medication_move() {
    window.location.href = 'medication-index';
}
function problem_move() {
    window.location.href = 'clinical-summary?id=7';
    $("#tabs").tabs({
        active: 7
    });
}
function vital_move() {
    window.location.href = 'clinical-summary?id=8';
    $("#tabs").tabs({
        active: 8
    });
}
function allergy_move() {
    window.location.href = 'clinical-summary?id=5';
    $("#tabs").tabs({
        active: 5
    });

}
function social_move() {//done
    window.location.href = 'clinical-summary?id=4';
    $("#tabs").tabs({
        active: 4
    });
}
function family_move() {//done
    window.location.href = 'clinical-summary?id=4';
    $("#tabs").tabs({
        active: 4
    });
}
function past_move() { //done
    window.location.href = 'clinical-summary?id=4';
    $("#tabs").tabs({
        active: 4
    });
}
function immunization_move() {
    window.location.href = 'clinical-summary?id=6';
    $("#tabs").tabs({
        active: 6
    });
}
    function documents_move() {
        window.location.href = 'clinical-summary?id=9';
        $("#tabs").tabs({
            active: 9
        });
    }
    function insurance_move() {
        window.location.href = 'clinical-summary?id=10';
        $("#tabs").tabs({
            active: 10
        });
    }
    function Proceduers_move() {
       // alert('Proc');
        window.location.href = 'clinical-summary?id=11';
        $("#tabs").tabs({
            active: 11
        });
    }
    function ClinicalInstructions_move() {
       // alert('Clin');
        window.location.href = 'clinical-summary?id=13';
        $("#tabs").tabs({
            active: 13
        });
    }
    function POC_move() {
      //  alert('POC');
        window.location.href = 'clinical-summary?id=12';
        $("#tabs").tabs({
            active: 12
        });
    
    }
    function PlanOfCare_Move()
    {
        window.location.href = 'clinical-summary?id=12';
      
        $("#tabs").tabs({
            active: 12
        });
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
    });

    $("#dialog-security").dialog({
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
        width: 700,
        modal: true,
        buttons: {
            "Save": function () {
                // alert('test');
                var bValid = true;
                //allFields.removeClass("ui-state-error");

                if (!Reqvalue($.trim($('#txtsecans').val()), 'Security Answer'))
                { return; }
                var flg = checkUrl($.trim($('#txtnpwd').val()));
              
                if (flg != true) {
                    $("#msg").css("display", "block");

                    bValid = false;
                }
                if (!Reqvalue($.trim($('#txtpwd').val()), 'Old Password'))
                { return; }

                if (!Reqvalue($.trim($('#txtnpwd').val()), 'New Password'))
                { return; }

                if ($('#txtnpwd').val() != $('#txtcpwd').val())
                {
                    alert('New Password and Confirm New Password didn\'t match');
                   
                    $("#msg").css("display", "block");
                    bValid = false;
                }
               

                if (bValid) {
                    // Save routine and call of Action
                    //    alert('test');
                    try {
                        var requestData = {
                            SecurityQuestionId: $.trim($('#cmbSecurityQuestion option:selected').val()),
                            SecurityAnswer: $.trim($('#txtsecans').val()),
                            Password: $.trim($('#txtpwd').val()),
                            NewPassword: $.trim($('#txtnpwd').val())
                        };

                        $.ajax({
                            type: 'POST',
                            url: 'home-security-save',
                            data: JSON.stringify(requestData),
                            dataType: 'json',
                            contentType: 'application/json; charset=utf-8',
                            success: function (data) {
                                // if success
                                // alert("Success : " + data);

                                alert("Password and security question have been changed successfully!");


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

        },
        close: function () {
            allFields.val("").removeClass("ui-state-error");
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




});

$(document).ready(function () {

    if ($('#cmbFacilityHome :selected').text() == 'Patient Entered') {
        $('#cmbVisitsHome').text("");

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
});

function chartSummary(id, flag) {

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
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    //if error

                    alert('Error : ' + xhr.message);
                    // HideLoader();
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
            }
        }
        //filling visit drop down end
    }



    //start Consolidate Call For Home Widgets Filter....
    visitOptions = document.getElementById('cmbVisitsHome').innerHTML;
    visitSelected = $('#cmbVisitsHome option:selected').val();
    //alert(visitOptions);
    //alert(visitSelected);
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
            url: 'home-widgets-filter',
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

                HideLoader();

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
function calculateBMI() {

    // var vt_height = parseInt($('#ht').val());
    // var vt_weight  = parseInt($('#wt').val());
    //// alert(vt_weight);

    //          alert('Please Input some Numeric Value');


}

function RefillDialog() {


    $("#dialog-refill").dialog("open");

}

// start of add lab pop up script

$(function () {

    tips = $(".validateTips");



    $("#addlab-form").dialog({
        autoOpen: false,
        show: {
            effect: "blind",
            duration: 1000
        },
        hide: {
            effect: "blind",
            duration: 1000
        },


        width: 550,
        modal: true,
        buttons: {
            "Save": function () {
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
    $("#createaddlab")
    .button()
    .click(function () {
        $('#hdFlag').val('labWidg');
        $("#addlab-form").dialog("open");
    });
});


//- end of add lab popup script





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
            duration: 1000
        },
        hide: {
            effect: "blind",
            duration: 1000
        },


        width: 600,
        modal: true,
        buttons: {
            "Save": function () {
                var bValid = true;

                allFields.removeClass("ui-state-error");
                bValid = bValid && checkLength(ConditionStatus, "Please Select ConditionStatus");
                //if((($('#cmbConditionStatusProb option:selected').val())==-1))
                //{
                //    alert("Please Select Condition Status option");
                //    return false;
                //}

                //else if ((($('#cmbConditionStatusProb option:selected').val()) == -1))
                //{
                //    alert("Please Select Condition Status option");
                //    return false;
                //}
                //else if ($('#txtProblemsDate_Year option:selected').val() == '--' && $('#txtProblemsDate_Day option:selected').val() != '--')
                //{
                //    alert('Please select Year or Month and Year or Month, Day and Year');
                //    return false;
                //}
                //else if ($('#txtProblemsDate_Year option:selected').val() == '--' && $('#txtProblemsDate_Month option:selected').val() != '--')
                //{
                //    alert('Please select Year or Month and Year or Month, Day and Year');
                //    return false;
                //}
                //else if ($('#txtProblemsLastDate_Year option:selected').val() == '--') {
                //    alert('Please select last change date Year');
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
            duration: 1000
        },
        hide: {
            effect: "blind",
            duration: 1000
        },


        width: 550,
        modal: true,
        buttons: {
            "Save": function () {
                var bValid = true;
                allFields.removeClass("ui-state-error");
                bValid = bValid && checkCharLength(AllergyType, "Allergy Type", 3, 20);
                bValid = bValid && checkCharLength(Allergy, "Allergen", 2, 50);
                bValid = bValid && checkCharLength(Reaction, "Reaction", 3, 50);
                bValid = bValid && checkLength(Status, "Please Select Status");

                if (bValid) {
                    //ajax 
                    try {

                       if ($('#txtAllergiesDate_Year option:selected').val() == '--' && $('#txtAllergiesDate_Day option:selected').val() != '--') {
                            alert('Please select Year or Month and Year or Month, Day and Year');
                            return false;
                        }
                        else if ($('#txtAllergiesDate_Year option:selected').val() == '--' && $('#txtAllergiesDate_Month option:selected').val() != '--') {
                            alert('Please select Year or Month and Year or Month, Day and Year');
                            return false;
                        }
                        else if ($('#txtAllergiesDate_Year option:selected').val() != '--' && $('#txtAllergiesDate_Month option:selected').val() == '--' && $('#txtAllergiesDate_Day option:selected').val() != '--') {
                            alert('Please select Year or Month and Year or Month, Day and Year');
                            return false;
                        }
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
            duration: 1000
        },
        hide: {
            effect: "blind",
            duration: 1000
        },


        width: 600,
        modal: true,
        buttons: {
            "Save": function () {
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
            duration: 1000
        },
        hide: {
            effect: "blind",
            duration: 1000
        },


        width: 550,
        modal: true,
        buttons: {
            "Save": function () {
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

//- end of add social popup script



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
            duration: 1000
        },
        hide: {
            effect: "blind",
            duration: 1000
        },


        width: 600,
        modal: true,
        buttons: {
            "Save": function () {
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
            duration: 1000
        },
        hide: {
            effect: "blind",
            duration: 1000
        },


        width: 550,
        height: 300,
        modal: true,
        buttons: {
            "Save": function () {
                var bValid = true;
                if (($('#txtoc_Month option:selected').val() == '--') && ($('#txtoc_Day option:selected').val() != '--')) {
                    alert('Please select Year Only or Month and Year Only Or Month, Day and Year');
                    return false;
                }
                if (($('#txtoc_Year option:selected').val() == '--') && ($('#txtoc_Month option:selected').val() != '--')) {
                    alert('Please select Year Only or Month and Year Only Or Month, Day and Year');
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
    $("#createaddpast")
    .button()
    .click(function () {
        $('#hdFlag').val('pastWidg');
        $("#addpast-form").dialog("open");
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
            duration: 1000
        },
        hide: {
            effect: "blind",
            duration: 1000
        },


        width: 600,
        modal: true,
        buttons: {
            "Save": function () {
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
            duration: 1000
        },
        hide: {
            effect: "blind",
            duration: 1000
        },


        width: 550,
        modal: true,

        buttons: {
            "Save": function () {
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
            duration: 1000
        },
        hide: {
            effect: "blind",
            duration: 1000
        },


        width: 600,
        modal: true,
        buttons: {
            "Save": function () {
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
            duration: 1000
        },
        hide: {
            effect: "blind",
            duration: 1000
        },

        height: 400,
        width: 550,
        modal: true,
        buttons: {
            "Save": function () {
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






// start of appointmnet button script

$(function () {

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
                            MessageRequest: txt

                        };


                        $.ajax({
                            type: 'POST',
                            url: 'index-Appointment-save',
                            data: JSON.stringify(requestData),
                            contentType: 'application/json; charset=utf-8',
                            dataType: 'json',
                            success: function (data) {
                                //alert(data);
                                $("#Appointment-portlet").empty();
                                $("#Appointment-portlet").html(data);
                                //alert(data);
                                $("#tbl-Appointments").fixedHeaderTable();
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
    $("#createappbtn")
    .button()
    .click(function () {
        $("#appbtn-form").dialog("open");
    });
    $("#createaddappointments")
    .button()
    .click(function () {
        $("#appbtn-form").dialog("open");
    });

});

//- end of appointment button script




// start of request referal button script

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
                                // $("#immunization-portlet").html(data);
                                //$("#Appointment-portlet").html(data);
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
    $("#createreqbtn")
    .button()
    .click(function () {
        $("#reqbtn-form").dialog("open");
    });
});

//- end of request referal button script



function pop_up_online() {
    var page = "http://www.healthtestingcenters.com/amr-blood-test-discount.aspx";

    var $dialog = $('<div></div>')
                   .html('<iframe style="border: 0px; " src="' + page + '" width="100%" height="100%"></iframe>')
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
                           duration: 1000
                       },
                       hide: {
                           effect: "blind",
                           duration: 1000
                       },
                       modal: true,
                       height: 600,
                       width: 900,
                       title: "Health Information Library"
                   });
    $dialog.dialog('open');
}
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
                    else if (($('#cmbMedication option:selected').val()) == -1 && ($('#cmbDoctor option:selected').val()) == -1 && ($('#cmbRFPharmacy option:selected').val().split('|')[0]) == -1) {
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
                            "PharmacyAddress": $.trim($('#PharmAddress').text())
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

    // start5  of script of add mediatin
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
                bValid = bValid && checkLength(Pharmacy, "Please Select Pharmacy");
                var DuringVisit = $("#DuringVist1").prop("checked");
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
                            MedicationName: $.trim($('#txtMedicationName').val()),
                            StartDate: $.trim($('#dtStartDate').val()),
                            Route: $.trim($('#txtRoute').val()),
                            Frequency: $.trim($('#txtFrequency').val()),
                            ExpireDate: $.trim($('#datepicker').val()),
                            Status: $.trim($('#cmbTakingMedicine option:selected').val()),
                            Pharmacy: (($('#cmbPharmacy option:selected').text()) == '--Select--') ? "" : $.trim($('#cmbPharmacy option:selected').text()),
                            Note: $.trim($('#txtInstructions').val()),
                            Days: $.trim($('#txtDays').val()),
                            Quantity: $.trim($('#txtQty').val()),
                            Refills: $.trim($('#txtRefills').val()),
                            duringvisit: DuringVisit,
                            Diagnosis: $.trim($('#txtDiagnosis').val()),

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
        $("#dialog-form").dialog("open");
    });

    // end of script of add addmen



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
            height: 630,
            width: 700,
            modal: true,
            buttons: {
                "Send": function () {

                    var bValid = true;
                    //var totbyte = Math.round($('#FileBytes').val() / 1000000);
                    //if (totbyte > 1) {
                    //    alert('Maximum Upload limit is 1mb');
                    //    bValid = false
                    //}
                    //if ($('#ByteData').val() == null || $('#ByteData').val() == '') {
                    //    bValid = false
                    //    alert('Please upload file and then proceed');
                    //}
                    if (bValid) {
                        //ajax 
                        try {
                            if (($('#cmbAppointmentFacility option:selected').val()) == -1 && ($('#cmbAppointmentDoctor option:selected').val()) == -1 && ($('#cmbMessageType option:selected').val()) == -1 && ($('#cmbMessageUrgency option:selected').val()) == -1) {
                                alert("Please Select 'Location , To , Subject , and MessagePriority' option");
                                return false;
                            }
                            else if (($('#cmbAppointmentDoctor option:selected').val()) == -1 && ($('#cmbMessageType option:selected').val()) == -1 && ($('#cmbMessageUrgency option:selected').val()) == -1) {
                                alert("Please Select ' To , Subject , MessagePriority' option");
                                return false;
                            }
                            else if (($('#cmbAppointmentFacility option:selected').val()) == -1 && ($('#cmbMessageType option:selected').val()) == -1 && ($('#cmbMessageUrgency option:selected').val()) == -1) {
                                alert("Please Select ' Location , Subject , MessagePriority' option");
                                return false;
                            }
                            else if (($('#cmbAppointmentFacility option:selected').val()) == -1 && ($('#cmbAppointmentDoctor option:selected').val()) == -1 && ($('#cmbMessageUrgency option:selected').val()) == -1) {
                                alert("Please Select 'Location , To , MessagePriority' option");
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
                                alert("Please Select 'Subject , MessagePriority ' option");
                                return false;
                            }
                            else if (($('#cmbAppointmentDoctor option:selected').val()) == -1 && ($('#cmbMessageType option:selected').val()) == -1) {
                                alert("Please Select 'To , Subject ' option");
                                return false;
                            }
                            else if (($('#cmbAppointmentDoctor option:selected').val()) == -1 && ($('#cmbMessageUrgency option:selected').val()) == -1) {
                                alert("Please Select 'To , MessagePriority ' option");
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
                                alert("Please Select 'MessagePriority' option");
                                return false;
                            }
                            var textMessage = $.trim($('#txtMessageWrite').val());

                            var txt = textMessage.replace(/\n/g, "\\n");
                            var requestData = {
                                AttachmentName: $.trim($('#files').val()),
                                AttachmentTest: $('#ByteData').val(),
                                ProviderId_To: $.trim($('#cmbAppointmentDoctor  option:selected').val()),
                                FacilityId: $.trim($('#cmbAppointmentFacility  option:selected').val()),
                                MessageRequest: txt,
                                MessageTypeId: $.trim($('#cmbMessageType  option:selected').val()),
                                MessageUrgencyId: $.trim($('#cmbMessageUrgency  option:selected').val()),
                                Flag: "home-compose"

                            };


                            //$.ajax({
                            //    type: 'POST',
                            //    url: 'messages-compose',
                            //    data: JSON.stringify(requestData),
                            //    dataType: 'json',
                            //    contentType: 'application/json; charset=utf-8',
                            //    success: function (data) {
                            //        // if success

                            //    },
                            //    error: function (xhr, ajaxOptions, thrownError) {
                            //        //if error

                            //        //alert('Error : ' + xhr.message);
                            //        HideLoader();
                            //    },
                            //    complete: function (data) {
                            //        // if completed
                            //        HideLoader();

                            //    },
                            //    async: true,
                            //    processData: false
                            //});

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

    $("#createmsg")
    .button()
    .click(function () {
        // clearAttach();
        // $('#btncancelupload').hide();
        // $("#ComposeMessage").dialog("open");

        $("#txtMessageWrite").val('');
        clearAttach();
        $('#browse').val('Browse..');
        $('#BrowseName').text('');
        $("#ComposeMessage").dialog("open");
        // $('#browse').hide();
        $('#files').hide();
    });
});


var jqXHRData;
$(document).ready(function () {

    initAttachFileUpload();




});

$(function () {

    $('#browse').click(function () { $('#my-simple-upload').click(); });


});

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

$(function () {

    $("#draggable").draggable({ axis: "y" });
    $("#draggable2").draggable({ axis: "x" });
   
});

$(function () {
    //var inc = 0;
    //$(".column").sortable({
    //    connectWith: ".column"
    //});

    $(".portlet").addClass("ui-widget ui-widget-content ui-helper-clearfix ui-corner-all")
    .find(".portlet-header")
    .addClass("ui-widget-header ui-corner-all")
    .prepend("<span class='ui-icon ui-icon-minusthick'></span>")
    .end()
    .find(".portlet-content");
    $(".portlet-header .ui-icon").click(function () {
        $(this).toggleClass("ui-icon-minusthick").toggleClass("ui-icon-plusthick");
        $(this).parents(".portlet:first").find(".portlet-content").toggle();

        if (inc % 2 == 0)
            $('#draggable3-1').css('height', '35px');
        else
            $('#draggable3-1').css('height', '140px');
        inc++;

    });
    $(".column").disableSelection();
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
    $("#txtCountRefills").mask("9?99");
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
        $('#createPresentMedication,#createaddstatements,#createaddproblems,#addvitalscreation,#createaddimmunizations,#createaddPOC,#createaddClinicalInstructions,#createaddProcedures,#createaddallergies,#addvitalscreation,#createaddpast,#createaddfamily,#createaddappointments,#createaddsocial').css('display', 'block');
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
function LoadFacityProvider() {
    ShowLoaderForAppointment();
    try {
        var requestData = {
            FacilityID: $.trim($('#cmbAppointmentFacility1 option:selected').val()),
            Value: "",
        };

        $.ajax({
            type: 'POST',
            url: 'Get-Provider-Facility-Data',
            data: JSON.stringify(requestData),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                // if success

                $("#DivFacityProvider").html(data);

            },
            error: function (xhr, ajaxOptions, thrownError) {
                //if error

                HideLoaderForAppointment();
                alert('Error : ' + xhr.message);

            },
            complete: function (data) {
                // if completed
                HideLoaderForAppointment();

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
}

function LoadFacityProviderRefil() {
    ShowLoaderForRefil();
    try {
        var requestData = {
            FacilityID: $.trim($('#cmbAppointmentFacilityRefill option:selected').val()),
            Value: "RequestRefil",
        };

        $.ajax({
            type: 'POST',
            url: 'Get-Provider-Facility-Data',
            data: JSON.stringify(requestData),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                // if success

                $("#refildiv").html(data);

            },
            error: function (xhr, ajaxOptions, thrownError) {
                //if error

                HideLoaderForRefil();
                alert('Error : ' + xhr.message);

            },
            complete: function (data) {
                // if completed
                HideLoaderForRefil();

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
}


function LoadRefilmediaction()
{
 try {
        var requestData = {
            FacilityID: $.trim($('#cmbAppointmentFacilityRefill option:selected').val()),
            Value: "RefilMedication",
        };

        $.ajax({
            type: 'POST',
            url: 'Get-Medication-from-provider',
            data: JSON.stringify(requestData),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                // if success

                $("#refilmed").html(data);

            },
            error: function (xhr, ajaxOptions, thrownError) {
                //if error

                HideLoaderForRefil();
                alert('Error : ' + xhr.message);

            },
            complete: function (data) {
                // if completed
                HideLoaderForRefil();

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


}

function LoadFacilityProviderSendMessage() {

    try {
        var requestData = {
            FacilityID: $.trim($('#cmbAppointmentFacility option:selected').val()),
            Value: "SendMessage",
        };
        ShowLoaderForComposeMessage();
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

function LoadFacilityProviderRequestRefferal() {
    ShowLoaderForRequestReferral();
    try {
        var requestData = {
            FacilityID: $.trim($('#cmbAppointmentFacility3 option:selected').val()),
            Value: "RequestRefferal",
        };

        $.ajax({
            type: 'POST',
            url: 'Get-Provider-Facility-Data',
            data: JSON.stringify(requestData),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                // if success

                $("#DivFacilityProviderRequestRefferal").html(data);

            },
            error: function (xhr, ajaxOptions, thrownError) {
                //if error

                HideLoaderForRequestReferral();
                alert('Error : ' + xhr.message);

            },
            complete: function (data) {
                // if completed
                HideLoaderForRequestReferral();

            },
            async: true,
            processData: false
        });
    }
    catch (err) {

        if (err && err !== "") {
            alert(err.message);
            HideLoaderForRequestReferral();
        }
    }
}
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
