

(function (i, s, o, g, r, a, m) {
    i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () {
        (i[r].q = i[r].q || []).push(arguments)
    }, i[r].l = 1 * new Date(); a = s.createElement(o),
    m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m)
})(window, document, 'script', '//www.google-analytics.com/analytics.js', 'ga');
ga('create', 'UA-26323561-2', 'auto');
ga('send', 'pageview');

$(document).ready(function () {
    //$("#counter").append("You have <strong>" + 256 + "</strong> characters remaining");
    var characters = 256;
   
        $("#counter").append("You have <strong>" + characters + "</strong> characters remaining");
        $("#appcount").append("You have <strong>" + characters + "</strong> characters remaining");
        $("#referallcount").append("You have <strong>" + characters + "</strong> characters remaining");

    $("#txtMessageWrite").keyup(function () {
       
        if ($(this).val().length > characters) {
            $(this).val($(this).val().substr(0, characters));
        }
        var remaining = characters - $(this).val().length;
        $("#counter").html("You have <strong>" + remaining + "</strong> characters remaining");
       
    });
    $("#txtComment").keyup(function () {

        if ($(this).val().length > characters) {
            $(this).val($(this).val().substr(0, characters));
        }
        var remaining = characters - $(this).val().length;
        $("#appcount").html("You have <strong>" + remaining + "</strong> characters remaining");

    });
    $("#txtComments").keyup(function () {

        if ($(this).val().length > characters) {
            $(this).val($(this).val().substr(0, characters));
        }
        var remaining = characters - $(this).val().length;
        $("#refillcount").html("You have <strong>" + remaining + "</strong> characters remaining");

    });
    $("#txtRComm").keyup(function () {

        if ($(this).val().length > characters) {
            $(this).val($(this).val().substr(0, characters));
        }
        var remaining = characters - $(this).val().length;
        $("#referallcount").html("You have <strong>" + remaining + "</strong> characters remaining");

    });
});
function DownloadContinuityCheck() {
        document.getElementById('chkProblemDownload').checked = true;
        document.getElementById('chkAllergiesDownload').checked = true;
        document.getElementById('chkImmumnizationDownload').checked = true;
        document.getElementById('chkMedicationsDownload').checked = true;
        document.getElementById('chkClinicalInstructionsDownload').checked = true;
        document.getElementById('chkFutureAppointmentDownload').checked = true;
        document.getElementById('chkRefferalsDownload').checked = true;
        document.getElementById('chkScheduledTestsDownload').checked = true;
        document.getElementById('chkDecisionAidsDownload').checked = true;
        document.getElementById('chkLabsDownload').checked = true;
        document.getElementById('chkVitalSignsDownload').checked = true;
        document.getElementById('chkProceduresDownload').checked = true;
        document.getElementById('chkSocialHistoryDownload').checked = true;
    }
    function DownloadContinuityUncheck() {
        document.getElementById('chkProblemDownload').checked = false;
        document.getElementById('chkAllergiesDownload').checked = false;
        document.getElementById('chkImmumnizationDownload').checked = false;
        document.getElementById('chkMedicationsDownload').checked = false;
        document.getElementById('chkClinicalInstructionsDownload').checked = false;
        document.getElementById('chkFutureAppointmentDownload').checked = false;
        document.getElementById('chkRefferalsDownload').checked = false;
        document.getElementById('chkScheduledTestsDownload').checked = false;
        document.getElementById('chkDecisionAidsDownload').checked = false;
        document.getElementById('chkLabsDownload').checked = false;
    }


    function SendContinuityCheck() {
        document.getElementById('chkProblemSend').checked = true;
        document.getElementById('chkAllergiesSend').checked = true;
        document.getElementById('chkImmumnizationSend').checked = true;
        document.getElementById('chkMedicationsSend').checked = true;
        document.getElementById('chkClinicalInstructionsSend').checked = true;
        document.getElementById('chkFutureAppointmentSend').checked = true;
        document.getElementById('chkRefferalsSend').checked = true;
        document.getElementById('chkScheduledTestsSend').checked = true;
        document.getElementById('chkDecisionAidsSend').checked = true;
        document.getElementById('chkLabsSend').checked = true;
    }

    function SendContinuityUncheck() {
        document.getElementById('chkProblemSend').checked = false;
        document.getElementById('chkAllergiesSend').checked = false;
        document.getElementById('chkImmumnizationSend').checked = false;
        document.getElementById('chkMedicationsSend').checked = false;
        document.getElementById('chkClinicalInstructionsSend').checked = false;
        document.getElementById('chkFutureAppointmentSend').checked = false;
        document.getElementById('chkRefferalsSend').checked = false;
        document.getElementById('chkScheduledTestsSend').checked = false;
        document.getElementById('chkDecisionAidsSend').checked = false;
        document.getElementById('chkLabsSend').checked = false;

    }

    function chartSummary_send(id, flag) {
       // alert();
        if (flag == 'visit')
        { sec_arg = $('#cmbContinuityFacilitySend option:selected').val(); }
        else if (flag == 'location') {
            sec_arg = id;
            id = $('#cmbContinuityVisitsSend option:selected').val();
        }

        if (flag == 'location') {

            //filling dropdown start 
            try {
                ShowLoader();
                //var IsPrimaryData =
                var requestData = {

                    FacilityId: sec_arg,
                    ExtensionToggleFunct: " toggle_combobox_send();",
                    ExtensionFilterFunct: "chartSummary_send(this.value,'visit');",
                    ExtensionIdName: "cmbVisitsHome"
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
                        id = $('#cmbContinuityVisitsSend option:selected').val();
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

    }

        




    function toggle_combobox_send() {
            
        if ($('#cmbContinuityFacilitySend :selected').text() != 'Patient Entered') {

            $('#cmbContinuityVisitsSend').attr('disabled', false);
            //$('#createPresentMedication,#createaddproblems,#addvitalscreation,#createaddimmunizations,#createaddallergies,#addvitalscreation,#createaddpast,#createaddfamily,#createaddappointments,#createaddsocial').css('display', 'none');

        }

        else {

            $('#cmbContinuityVisitsSend').text("");
            $('#cmbContinuityVisitsSend').attr('disabled', true);
            //$('#cmbContinuityVisitsSend').val(0);
            // $('#createPresentMedication,#createaddstatements,#createaddproblems,#addvitalscreation,#createaddimmunizations,#createaddallergies,#addvitalscreation,#createaddpast,#createaddfamily,#createaddappointments,#createaddsocial').css('display','block');
        }

    }




    function ShowLoaderReportViewer() {
        //  $('#page_loader1').css('display', 'block');
        //   $('#page_loader1').show();
        ShowLoader();
    }

    function HideLoaderReportViewer() {
        setTimeout(function () {
            //   $('#page_loader1').css('display', 'none');
            //   $('#page_loader1').hide();
            HideLoader();
        }, 5000);
    }

    // start of download continuity pop up script

    $(function () {

        tips = $(".validateTips");
       
       // $("#footer-div").html('<p style="margin-left: 40px; padding-top: 10px; font-family: Verdana, Geneva, sans-serif; font-size: 12px;">Copyright © 2005 -2014 Access My Records, Inc. - AMR Patient Portal Version 2.0 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="anc" onclick="javascript:location.href="https://www.amrportal.com/Content/Legal/PrivacyPolicy.html"">Privacy</span><strong>|</strong><span class="anc" onclick="javascript:location.href="https://www.amrportal.com/Content/Legal/Terms.html"">Terms</span></p>');
      //  console.log('removed!');
    //    alert('ass-removed');
   //     $("#footer-div a").removeClass("ui-button ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only")


        $("#form-addDownloadContinuity").dialog({
            autoOpen: false,
            show: {
                effect: "blind",
                duration: 1000
            },
            hide: {
                effect: "blind",
                duration: 1000
            },


            width: 400,
            height: 650,
            modal: true,
            buttons: {
                "View": function () {
                        
                        
                    var bValid = true;
                    var boolProblems;
                    var boolAllergies;
                    var boolImmunizations;
                    var boolMedications;
                    var boolLabs;
                    var boolClinicalInstructions;
                    var boolFutureAppointments;
                    var boolReferrals;
                    var boolScheduledTests;
                    var boolDecisionAids;
                    var boolSocialHistory;
                    var boolVitalSigns;
                    var boolProcedures;
                    if (bValid) {
                        //ajax 
                        try {
                            var checked = $('input[name="chkdownload"]:checked');
                            if (checked.length === 0)
                            {
                                alert("Please select any option");
                                bValid = false;
                                return false;
                            }
                            var facilityId = $.trim($('#cmbContinuityFacilityDownload option:selected').val());
                            var visitID = $.trim($('#cmbContinuityVisitsDownload option:selected').val());
                            if (facilityId == 0) {
                                visitID = 0;
                            }

                            if ($('#chkProblemDownload').prop('checked')) { boolProblems = true; } else { boolProblems = false; }
                            if ($('#chkAllergiesDownload').prop('checked')) { boolAllergies = true; } else { boolAllergies = false; }
                            if ($('#chkImmumnizationDownload').prop('checked')) { boolImmunizations = true; } else { boolImmunizations = false; }
                            if ($('#chkMedicationsDownload').prop('checked')) { boolMedications = true; } else { boolMedications = false; }
                            if ($('#chkLabsDownload').prop('checked')) { boolLabs = true; } else { boolLabs = false; }
                            if ($('#chkClinicalInstructionsDownload').prop('checked')) { boolClinicalInstructions = true; } else { boolClinicalInstructions = false; }
                            if ($('#chkFutureAppointmentDownload').prop('checked')) { boolFutureAppointments = true; } else { boolFutureAppointments = false; }
                            if ($('#chkRefferalsDownload').prop('checked')) { boolReferrals = true; } else { boolReferrals = false; }
                            if ($('#chkScheduledTestsDownload').prop('checked')) { boolScheduledTests = true; } else { boolScheduledTests = false; }
                            if ($('#chkDecisionAidsDownload').prop('checked')) { boolDecisionAids = true;  } else { boolDecisionAids = false; }
                            if ($('#chkSocialHistoryDownload').prop('checked')) { boolSocialHistory = true; } else { boolSocialHistory = false; }
                            if ($('#chkVitalSignsDownload').prop('checked')) { boolVitalSigns = true; } else { boolVitalSigns = false; }
                            if ($('#chkProceduresDownload').prop('checked')) { boolProcedures = true; } else { boolProcedures = false; }
                            //alert($.trim($('#cmbContinuityVisitsSend option:selected').val()));

                            var requestData = {
                                VisitID: visitID,
                                FacilityId: facilityId,
                                Problems: boolProblems,
                                Allergies: boolAllergies,
                                Immunizations: boolImmunizations,
                                Medications: boolMedications,
                                Labs: boolLabs,
                                ClinicalInstructions: boolClinicalInstructions,
                                FutureAppointments: boolFutureAppointments,
                                Referrals: boolReferrals,
                                ScheduledTests: boolScheduledTests,
                                DecisionAids: boolDecisionAids,
                                SocialHistory: boolSocialHistory,
                                VitalSigns: boolVitalSigns,
                                Procedures: boolProcedures
                            };

                            //$(this).dialog("close");
                            //ShowLoaderReportViewer();
                            var str = JSON.stringify(requestData);

                            //alert(encodeURIComponent(str));

                            var requestURL = 'index-patient-CCD/?healthRecordData=' + str;
                            window.open(requestURL);
                            // location.href = requestURL;

                            //$('#iframeCCDXML').attr('src', requestURL);
                            //$('#iframeCCDXML').load();

                        } catch (err) {

                            if (err && err !== "") {
                                $(this).dialog("close");
                                alert(err.message);
                                HideLoaderReportViewer();
                            }
                        }

                            


                        // end ajax


                    }
                },
                Cancel: function () {
                    $(this).dialog("close");
                }
            },
            cancel: function () {
                $(this).dialog("close");
            }
        });
        $("#addDownloadContinuity")
        .button()
        .click(function () {
            window.open('CCD-View');
            //$('#result').text('');
            //$('#hdFlag').val('vitalWidg');
            //$("#cmbContinuityFacilityDownload").val("Patient Entered");
            //$('#cmbContinuityVisitsDownload').text("");
            //$('#cmbContinuityVisitsDownload').attr('disabled', true);
            // $("#form-addDownloadContinuity").dialog("open");
        });
    });

    // end of download continuity pop up script

    function openDialog() {

        // document.execCommand("SaveAs", true, "D://abc.txt");
        document.open();

    }

    // start of send continuity pop up script

    $(function () {
        // ShowLoader();
        tips = $(".validateTips");



        $("#form-addSendContinuity").dialog({
            autoOpen: false,
            show: {
                effect: "blind",
                duration: 1000
            },
            hide: {
                effect: "blind",
                duration: 1000
            },


            width: 400,
            modal: true,
            buttons: {
                "Send": function () {
                    chkAccess();
                    var bValid = true;
                    var boolProblems;
                    var boolAllergies;
                    var boolImmunizations;
                    var boolMedications;
                    var boolLabs;
                    var boolClinicalInstructions;
                    var boolFutureAppointments;
                    var boolReferrals;
                    var boolScheduledTests;
                    var boolDecisionAids;
                    if (bValid) {
                        //ajax 
                        try {
                            var checked = $('input[name="chksend"]:checked');
                            if (checked.length === 0) {
                                alert("Please select atleast one checkbox");
                                bValid = false;
                                return false;
                            }
                            var facilityId = $.trim($('#cmbContinuityFacilitySend option:selected').val());
                            var visitID = $.trim($('#cmbContinuityVisitsSend option:selected').val());
                            if (facilityId == 0) {
                                visitID = 0;
                            }
                            if ($('#chkProblemSend').prop('checked')) { boolProblems = true; } else { boolProblems = false; }
                            if ($('#chkAllergiesSend').prop('checked')) { boolAllergies = true; } else { boolAllergies = false; }
                            if ($('#chkImmumnizationSend').prop('checked')) { boolImmunizations = true; } else { boolImmunizations = false; }
                            if ($('#chkMedicationsSend').prop('checked')) { boolMedications = true; } else { boolMedications = false; }
                            if ($('#chkLabsSend').prop('checked')) { boolLabs = true; } else { boolLabs = false; }
                            if ($('#chkClinicalInstructionsSend').prop('checked')) { boolClinicalInstructions = true; } else { boolClinicalInstructions = false; }
                            if ($('#chkFutureAppointmentSend').prop('checked')) { boolFutureAppointments = true; } else { boolFutureAppointments = false; }
                            if ($('#chkRefferalsSend').prop('checked')) { boolReferrals = true; } else { boolReferrals = false; }
                            if ($('#chkScheduledTestsSend').prop('checked')) { boolScheduledTests = true; } else { boolScheduledTests = false; }
                            if ($('#chkDecisionAidsSend').prop('checked')) { boolDecisionAids = true; } else { boolDecisionAids = false; }

                            //alert($.trim($('#cmbContinuityVisitsSend option:selected').val()));

                            var requestData = {
                                VisitID: visitID,
                                FacilityId: facilityId,
                                EmailID: $.trim($('#txtSendEmailCCD').val()),
                                Problems: boolProblems,
                                Allergies: boolAllergies,
                                Immunizations: boolImmunizations,
                                Medications: boolMedications,
                                Labs: boolLabs,
                                ClinicalInstructions: boolClinicalInstructions,
                                FutureAppointments: boolFutureAppointments,
                                Referrals: boolReferrals,
                                ScheduledTests: boolScheduledTests,
                                DecisionAids: boolDecisionAids
                            };

                            $.ajax({
                                type: 'POST',
                                url: 'index-patient-CCD-Send',
                                data: JSON.stringify(requestData),
                                dataType: 'json',
                                contentType: 'application/json; charset=utf-8',
                                success: function (data) {
                                    // if success
                                       
                                    // alert(data);



                                    //   $("#vital-portlet").html(data[0]);



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
                },
                Cancel: function () {
                    $(this).dialog("close");
                }
            },
            cancel: function () {
                $(this).dialog("close");
            }
        });
        $("#addSendContinuity")
        .button()
        .click(function () {
            $('#result').text('');
            $('#hdFlag').val('vitalWidg');
            $("#cmbContinuityFacilitySend").val("Patient Entered");
            $('#cmbContinuityVisitsSend').text("");
            $('#cmbContinuityVisitsSend').attr('disabled', true);
            $("#form-addSendContinuity").dialog("open");
        });
    });

    // end of send continuity pop up script






    // start of myWorkPlace pop up script
    function openMyWorkPlace(id)
    {
        chkAccess();
        $('.heading').remove();
        var element = '<h5 class="heading" style="float:left; margin-bottom:-8px; margin-top:8px; margin-right:8px; "><b><span id="myWorkPlaceCounter" class="myWorkPlaceCounterClass">1</span> of 10</b></h5>';
        $(".ui-dialog-buttonpane button:contains('Prev')").parent().parent().append(element);
        $('#myWorkPlaceFrame').attr("src", "/Content/site/" + id + ".html");
        next = id;
        $('.myWorkPlaceCounterClass').text(next);
        if(id==1){
            $(".ui-dialog-buttonpane button:contains('Prev')").attr("disabled", true).addClass("ui-state-disabled");
        } else { $(".ui-dialog-buttonpane button:contains('Prev')").attr("disabled", false).removeClass("ui-state-disabled"); }
        $(".ui-dialog-buttonpane button:contains('Start Over')").text("Next");
        $(".ui-dialog-buttonpane button:contains('Next')").css("height", "32px");
        $(".ui-dialog-buttonpane button:contains('Next')").css("width", "80px");
        if (id == 11) {
            $(".ui-dialog-buttonpane button:contains('Next')").text("Start Over");
            $(".ui-dialog-buttonpane button:contains('Start Over')").css("height", "32px");
            $(".ui-dialog-buttonpane button:contains('Start Over')").css("width", "100px");
        }


        $("#form-myWorkPlace").dialog("open");



    }

    var next;

    $(function () {

        tips = $(".validateTips");
          
            


        $("#form-myWorkPlace").dialog({
            autoOpen: false,
            show: {
                effect: "blind",
                duration: 100
            },
            hide: {
                effect: "blind",
                duration: 100
            },


            width: 900,
            modal: true,
            buttons: {
                "Prev": function () {
                    $(".ui-dialog-buttonpane button:contains('Start Over')").text("Next");
                    $(".ui-dialog-buttonpane button:contains('Next')").css("height", "32px");
                    $(".ui-dialog-buttonpane button:contains('Next')").css("width", "80px");

                        
                    if(next>1)
                        next--;
                    else
                    { alert("You reached to the start");  }

                    $('#myWorkPlaceFrame').attr("src", "/Content/site/" + next + ".html");
                    $('.myWorkPlaceCounterClass').text(next);
                    if (next == 1)
                    { $(".ui-dialog-buttonpane button:contains('Prev')").attr("disabled", true).addClass("ui-state-disabled"); }



                    if (next == 10) {
                        $(".ui-dialog-buttonpane button:contains('Next')").text("Start Over");
                        $(".ui-dialog-buttonpane button:contains('Start Over')").css("height", "32px");
                        $(".ui-dialog-buttonpane button:contains('Start Over')").css("width", "100px");
                    }
                        
                },
                Next: function () {
                    $(".ui-dialog-buttonpane button:contains('Start Over')").text("Next");
                    $(".ui-dialog-buttonpane button:contains('Next')").css("height", "32px");
                    $(".ui-dialog-buttonpane button:contains('Next')").css("width", "80px");

                    $(".ui-dialog-buttonpane button:contains('Prev')").attr("disabled", false).removeClass("ui-state-disabled");
                    if (next < 10)
                        next++;
                    else
                    { alert("You reached to the end"); next = 1; }
                     
                    $('#myWorkPlaceFrame').attr("src", "/Content/site/" + next + ".html");
                    $('.myWorkPlaceCounterClass').text(next);
                    if (next == 1)
                    { $(".ui-dialog-buttonpane button:contains('Prev')").attr("disabled", true).addClass("ui-state-disabled"); }



                    if (next == 10) {
                        $(".ui-dialog-buttonpane button:contains('Next')").text("Start Over");
                        $(".ui-dialog-buttonpane button:contains('Start Over')").css("height", "32px");
                        $(".ui-dialog-buttonpane button:contains('Start Over')").css("width", "100px");
                    }
                }
            },
                
        });
        $("#addMyWorkPlace")
        .button()
        .click(function () {
            $("#form-myWorkPlace").dialog("open");
        });
    });

//- end of myworkplace popup script

    function UpgradePremiumdialgue() {
        $("#form-Premiumdialge").dialog("open");

    }


    $(function () {

    




        $("#form-Premiumdialge").dialog({
            autoOpen: false,
            show: {
                effect: "blind",
                duration: 100
            },
            hide: {
                effect: "blind",
                duration: 100
            },

            height:580,
            width: 760,
            
            modal: true,
            buttons: {
                
                Close: function ()
                {
                    $(this).dialog("close");
                }
            },

        });
       
    });

    














        


       
    $(window).resize(function () {
        $("#timeout-dialog").dialog("option", "position", "center");
    });
    function Logout() {
          
        location.href = '/logout';
       
    }
    $(function () {

        $("#accordion").accordion({
            heightStyle: "content"
        });

        //  $("#accordion").accordion();

        var availableTags = [
        "ActionScript",
        "AppleScript",
        "Asp",
        "BASIC",
        "C",
        "C++",
        "Clojure",
        "COBOL",
        "ColdFusion",
        "Erlang",
        "Fortran",
        "Groovy",
        "Haskell",
        "Java",
        "JavaScript",
        "Lisp",
        "Perl",
        "PHP",
        "Python",
        "Ruby",
        "Scala",
        "Scheme"
        ];
        $("#autocomplete").autocomplete({
            source: availableTags
        });

            
            

        $("#button").button();
        $("#radioset").buttonset();



        $("#tabs").tabs();



        $("#dialog").dialog({
            autoOpen: false,
            width: 650,
            height: 640,
            /*      buttons: [
                  {
                      text: "Ok",
                      click: function () {
                          $(this).dialog("close");
                      }
                  },
                  {
                      text: "Cancel",
                      click: function () {
                          $(this).dialog("close");
                      }
                  }
                  ] */
        });

        // Link to open the dialog
        $("#dialog-link").click(function (event) {
            $("#dialog").dialog("open");
            event.preventDefault();
        });


        $("#datepicker").datepicker({
            inline: true,
           
            dateFormat: 'mm/dd/yy',
            changeMonth: true,
            changeYear: true,
            yearRange: "1900:+0"
        });

        $("#dtStartDate").datepicker({
            inline: true,
          
            dateFormat: 'mm/dd/yy',
            changeMonth: true,
            changeYear: true,
            yearRange: "1900:+0"
        });

        $("#dtStartDatepst").datepicker({
            inline: true,
         
            dateFormat: 'mm/dd/yy',
            changeMonth: true,
            changeYear: true,
            yearRange: "1900:+0"
        });

        $("#dtObservationDate").datepicker({
            inline: true,
         
            dateFormat: 'mm/dd/yy',
            changeMonth: true,
            changeYear: true,
            yearRange: "1900:+0"
                
        });

        $("#txtImmunizationEXDate").datepicker({
            inline: true,
          
            dateFormat: 'mm/dd/yy',
            changeMonth: true,
            changeYear: true,
            yearRange: "1900:+0"
        });

        $("#Planneddt").datepicker({
            inline: true,

            dateFormat: 'mm/dd/yy',
            changeMonth: true,
            changeYear: true,
            yearRange: "1900:+0"

        });

        $("#date").datepicker({
            inline: true,

            dateFormat: 'mm/dd/yy',
            changeMonth: true,
            changeYear: true,
            yearRange: "1900:+0"

        });

        $("#txtPlanneddt").datepicker({
            inline: true,

            dateFormat: 'mm/dd/yy',
            changeMonth: true,
            changeYear: true,
            yearRange: "1900:+0"

        });

        $('#cmbImmunizationTime').timepicker({
            timeFormat: "hh:mm tt",
            hourMin: 7,
            hourMax: 18,

        });

           

        //$("#txtDateReported").datepicker({
        //    inline: true
        //});

        //$("#dtDateOccurance").datepicker({
        //    inline: true
        //});
            
        $("#datepicker1").datepicker({
            inline: true,
           
            dateFormat: 'mm/dd/yy',
            changeMonth: true,
            changeYear: true,
            yearRange: "1900:+0"
        });

        $("#txtOrderDate").datepicker({
            inline: true,
           
            dateFormat: 'mm/dd/yy',
            changeMonth: true,
            changeYear: true,
            yearRange: "1900:+0"
        });

        $("#txtCollectionDate").datepicker({
            inline: true,
          
            dateFormat: 'mm/dd/yy',
            changeMonth: true,
            changeYear: true,
            yearRange: "1900:+0"
        });

        $("#txtReportDate").datepicker({
            inline: true,
            
            dateFormat: 'mm/dd/yy',
            changeMonth: true,
            changeYear: true,
            yearRange: "1900:+0"
        });

        $("#txtReviewDate").datepicker({
            inline: true,
          
            dateFormat: 'mm/dd/yy',
            changeMonth: true,
            changeYear: true,
            yearRange: "1900:+0"
        });

        $("#dtVisitDate").datepicker({
            inline: true,
           
            dateFormat: 'mm/dd/yy',
            changeMonth: true,
            changeYear: true,
            yearRange: "1900:+0"
        });

        $("#txtPrefDateFrom").datepicker({
            inline: true,
           
            dateFormat: 'mm/dd/yy',
            changeMonth: true,
            changeYear: true,
            yearRange: "1900:+0"
        });

        $("#txtPrefDateTo").datepicker({
            inline: true,
           
            dateFormat: 'mm/dd/yy',
            changeMonth: true,
            changeYear: true,
            yearRange: "1900:+0"
        });
            
        $("#txtRpredatefrom").datepicker({
            inline: true,
           
            dateFormat: 'mm/dd/yy',
            changeMonth: true,
            changeYear: true,
            yearRange: "1900:+0"
        });

        $("#txtRpredateto").datepicker({
            inline: true,
            dateFormat: 'mm/dd/yy',
            changeMonth: true,
            changeYear: true,
            yearRange: "1900:+0"
        });
        //$("#txtProblemsDate").datepicker({
        //    inline: true
        //});
            
        $("#txtImmunizationDate").datepicker({
                
            inline: true,
         
            dateFormat: 'mm/dd/yy',
            changeMonth: true,
            changeYear: true,
            yearRange: "1900:+0"
        });
            
        $("#show-date-imm").click(function () {
            $("#txtImmunizationDate").datepicker("show");
        });
        $("#show-date-refPrefEnd").click(function () {
            $("#txtRpredateto").datepicker("show");
        });
        $("#show-date-refPrefStart").click(function () {
            $("#txtRpredatefrom").datepicker("show");
        });
        $('#show-date-PreferedStart').click(function () {
            $('#txtPrefDateFrom').datepicker("show");
        });

        $('#show-date-PreferredEnd').click(function () {
            $('#txtPrefDateTo').datepicker("show");
        });
        $("#show-date-imm-exp").click(function () {
            $("#txtImmunizationEXDate").datepicker("show");
        });

        $("#show-date-vit").click(function () {
            $("#dtObservationDate").datepicker("show");
        });
        $("#show-date-vitData").click(function () {
            $("#dtObservationDateData").datepicker("show");
        });
        $("#show-date-imm-Data").click(function () {
            $("#txtImmunizationDateData").datepicker("show");
        });

        $("#show-date-POC").click(function () {
            $("#Planneddt").datepicker("show");
        });
        $("#show-date-proc").click(function () {
            $("#date").datepicker("show");
        });
        $("#show-date-Clinical").click(function () {
            $("#txtPlanneddt").datepicker("show");
        });

       

        $("#txtdob").datepicker({
            inline: true,
          
            dateFormat: 'mm/dd/yy',
            changeMonth: true,
            changeYear: true,
            yearRange: "1900:+0"
               
        });

        $("#slider").slider({
            range: true,
            values: [17, 67]
        });

        $("#progressbar").progressbar({
            value: 20
        });


        // Hover states on the static widgets
        $("#dialog-link, #icons li").hover(
        function () {
            $(this).addClass("ui-state-hover");
        },
        function () {
            $(this).removeClass("ui-state-hover");
        }
    );
    });

    function clearFields()
    {
           
        $(":input[type!='hidden']").val('');
        $(':input').prop('checked', false);

    }

      

      
    var jqXHRData;

    $(document).ready(function () {
        $("#cmbRFPharmacy").change(function () {

            var spl = $("#cmbRFPharmacy").val().split('|');
            $('#PharmAddress').text(spl[1]);
            //  alert(spl[2]);
            $("#Phone").val(spl[2]);
            // alert($("#Phone").val());
        });
      //  $("#txtCountRefills").mask("99999");
        $("input[id=btncancelupload]").click(function () {
            $('#BrowseName').text('');
            $("#btncancelupload").hide();
        });
        $("#txtPharmZipCode").mask("99999");
        $("#txtPhone").mask("999-999-9999");

        
        $('#btnAccountSetting').append("<img src=\"Content/img/account1.png\" width=\"20px\" style=\" float:left; margin-top: 0px; margin-left: -10px; margin-right: 10px;\"> ");
        $('#btnViewMyHealthRecord').append("<img src=\"Content/img/chart1.png\" width=\"20px\" style=\"float:left; margin-top: 0px; margin-left: -10px; margin-right: 10px;\">");
        $('#btnDownloadAndTransmit').append("<img src=\"Content/img/download1.png\" width=\"20px\" style=\"float:left; margin-top: 0px; margin-left: -10px; margin-right: 10px;\">");
        $('#UpgradePremium').append("<img src=\"Content/img/upgrade.png\" width=\"20px\" style=\"float:left; margin-top: 0px; margin-left: -10px; margin-right: 10px;\">");

        $('#btnDrugInformationCenter').append("<img src=\"Content/img/pill.png\" width=\"20px\" style=\" float:left; margin-top: 0px; margin-left: -10px; margin-right: 10px;\">");
        $('#btnHealthInformationLibrary').append("<img src=\"Content/img/document_library.png\" width=\"20px\" style=\"float:left; margin-top: 0px; margin-left: -10px; margin-right: 10px;\">");
        $('#btnICEProgram').append("<img src=\"Content/img/iphone.png\" width=\"20px\" style=\"float:left; margin-top: 0px; margin-left: -10px; margin-right: 10px;\">");
        $('#btnLocatePharmacy').append("<img src=\"Content/img/pharm.png\" width=\"20px\" style=\"float:left; margin-top: 0px; margin-left: -10px; margin-right: 10px;\">");
        $('#btnMedicalBraceletes').append(" <img src=\"Content/img/account1.png\" width=\"20px\" style=\" float:left; margin-top: 0px; margin-left: -10px; margin-right: 10px;\">");
        $('#btnMedicalIDCard').append("<img src=\"Content/img/medicalcard.png\" width=\"20px\" style=\"float:left; margin-top: 0px; margin-left: -10px; margin-right: 10px;\">");
        $('#btnOnlineBloodLab').append(" <img src=\"Content/img/blood.png\" width=\"20px\" style=\"float:left; margin-top: 0px; margin-left: -10px; margin-right: 10px;\">");
        $('#btnDiscoundCards').append(" <img src=\"Content/img/percentage.png\" width=\"20px\" style=\"float:left; margin-top: 0px; margin-left: -10px; margin-right: 10px;\">");
        $('#btnMedicalPortfolio').append(" <img src=\"Content/img/mportfolio.png\" width=\"20px\" style=\"float:left; margin-top: 0px; margin-left: -10px; margin-right: 10px;\">");
        $('#btnShareMyRecord').append(" <img src=\"Content/img/shareIcon.png\" width=\"20px\" style=\"float:left; margin-top: 0px; margin-left: -10px; margin-right: 10px;\">");

        hide_widg5(); // only for temporary purpose
        initSimpleFileUpload();
        initAutoFileUpload();
        initFileUploadWithCheckingSize();

        $("#hl-start-upload").on('click', function () {
            if (jqXHRData) {
                jqXHRData.submit();
            }
            return false;
        });

        $("#hl-start-upload-with-size").on('click', function () {
            if (jqXHRData) {
                var isStartUpload = true;
                var uploadFile = jqXHRData.files[0];

                if (!(/\.(gif|jpg|jpeg|tiff|png)$/i).test(uploadFile.name)) {
                    alert('You must select an image file only');
                    isStartUpload = false;
                } else if (uploadFile.size > 4000000) { // 4mb
                    alert('Please upload a smaller image, max size is 4 MB');
                    isStartUpload = false;
                }
                if (isStartUpload) {
                    jqXHRData.submit();
                }
            }
            return false;
        });
    });

    function initSimpleFileUpload() {
        'use strict';

        $('#fu-my-simple-upload').fileupload({
            url: 'master-images-save',
            dataType: 'json',
            //formData : {name:'thedate',value:'test'},
            add: function (e, data) {
                    
                jqXHRData = data
                // $.each(data.files, function (index, file) {
                //  alert('test');
                $('#newCanvas').html('<img src="' + URL.createObjectURL(data.files[0]) + '" style="cursor:pointer;  border: 1px solid rgb(166, 201, 226); -moz-border-radius: 5px;  -webkit-border-radius: 5px; border-radius: 5px; margin-bottom: 5px; position:relative;" width = "200" height = "200" align="right"/>');
                //});
            },
            done: function (event, data) {
                $('#PatientImg').html(data.result.Imghtml);
                //if (data.result.isUploaded) {

                //}
                // else {

                // }
                //alert(data.result.message);
                HideLoader();
            },
            fail: function (event, data) {
                if (data.files[0].error) {
                    alert(data.files[0].error);
                }
            }
        });
    }

    function initAutoFileUpload() {
        'use strict';

        $('#fu-my-auto-upload').fileupload({
            autoUpload: true,
            url: '/File/UploadFile',
            dataType: 'json',
            add: function (e, data) {
                var jqXHR = data.submit()
                    .success(function (data, textStatus, jqXHR) {
                        if (data.isUploaded) {

                        }
                        else {

                        }
                        alert(data.message);
                    })
                    .error(function (data, textStatus, errorThrown) {
                        if (typeof (data) != 'undefined' || typeof (textStatus) != 'undefined' || typeof (errorThrown) != 'undefined') {
                            alert(textStatus + errorThrown + data);
                        }
                    });
            },
            fail: function (event, data) {
                if (data.files[0].error) {
                    alert(data.files[0].error);
                }
            }
        });
    }

    function initFileUploadWithCheckingSize() {
        'use strict';

        $('#fu-my-simple-upload-with-size').fileupload({
            url: '/File/UploadFile',
            dataType: 'json',
            add: function (e, data) {
                jqXHRData = data;

            },
            done: function (event, data) {
                if (data.result.isUploaded) {

                }
                else {

                }
                alert(data.result.message);
            },
            fail: function (event, data) {
                if (data.files[0].error) {
                    alert(data.files[0].error);
                }
            }
        });
    }

      
       
    function ShareHide_widg1(BoolVal) {
        try {
            ShowLoader();
            var requestData = {
                Share: BoolVal
            };

           // alert(BoolVal);
            $.ajax({
                type: 'POST',
                url: 'Share-Hide-Appointments',
                data: JSON.stringify(requestData),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    // if success
                    // alert("Success : " + data);

                    $("#App-ShareHide").remove();
                   // $("#widg1 div:nth-child(1)").append(data);
                    //$("#widg1 div:first").before(data);
                    $("#App-Section").html(data);
                    //if (!BoolVal)
                    //{
                    //    $('#widg1').css('display', 'none');
                    //    $('.disable_button1').css('color', 'rgb(46, 110, 158)');
                    //}
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
    function ShareHide_Socialhistory(BoolVal)
       {
            try {
                ShowLoader();
                var requestData = {
                    Share: BoolVal
                };

               // alert(BoolVal);
                $.ajax({
                    type: 'POST',
                    url: 'Share-Hide-SocialHistory',
                    data: JSON.stringify(requestData),
                    dataType: 'json',
                    contentType: 'application/json; charset=utf-8',
                    success: function (data) {
                        // if success
                        // alert("Success : " + data);

                        $("#App-ShareHideSocialHistory").remove();
                        // $("#widg1 div:nth-child(1)").append(data);
                        //$("#widg1 div:first").before(data);
                        $("#App-SocialHistory").html(data);
                        //if (!BoolVal)
                        //{
                        //    $('#socialhistwidg').css('display', 'none');
                        //    $('.disable_button8').css('color', 'rgb(46, 110, 158)');
                        //}
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
   
    function ShareHide_Familyhist(BoolVal) {
        try {
            ShowLoader();
            var requestData = {
                Share: BoolVal
            };

         //   alert(BoolVal);
            $.ajax({
                type: 'POST',
                url: 'Share-Hide-FamilyHistory',
                data: JSON.stringify(requestData),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    // if success
                    // alert("Success : " + data);

                    $("#App-ShareHidefamilyhist").remove();
                    // $("#widg1 div:nth-child(1)").append(data);
                    //$("#widg1 div:first").before(data);
                    $("#App-familyhistwidg").html(data);
                    ////if (!BoolVal) {
                    ////    $('#familyhistwidg').css('display', 'none');
                    ////    $('.disable_button9').css('color', 'rgb(46, 110, 158)');
                    ////}
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
    
    function ShareHide_Visits(BoolVal) {
        try {
            ShowLoader();
            var requestData = {
                Share: BoolVal
            };

          //  alert(BoolVal);
            $.ajax({
                type: 'POST',
                url: 'Share-Hide-Visits',
                data: JSON.stringify(requestData),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    // if success
                    // alert("Success : " + data);

                    $("#App-ShareHideVisits").remove();
                    // $("#widg1 div:nth-child(1)").append(data);
                    //$("#widg1 div:first").before(data);
                    $("#App-Visits").html(data);
                    //if (!BoolVal) {
                    //    $('#visitswidg').css('display', 'none');
                    //    $('.disable_button6').css('color', 'rgb(46, 110, 158)');
                    //}
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

    function ShareHide_Medication(BoolVal) {
        try {
            ShowLoader();
            var requestData = {
                Share: BoolVal
            };

            //alert(BoolVal);
            $.ajax({
                type: 'POST',
                url: 'Share-Hide-Medication',
                data: JSON.stringify(requestData),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    // if success
                    // alert("Success : " + data);

                    $("#App-ShareHideMedications").remove();
                    // $("#widg1 div:nth-child(1)").append(data);
                    //$("#widg1 div:first").before(data);
                    $("#App-Medications").html(data);
                    //if (!BoolVal) {
                    //    $('#widg2').css('display', 'none');
                    //    $('.disable_button2').css('color', 'rgb(46, 110, 158)');
                    //}
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
    function ShareHide_PastMedicalHistory(BoolVal) {
        try {
            ShowLoader();
            var requestData = {
                Share: BoolVal
            };

           // alert(BoolVal);
            $.ajax({
                type: 'POST',
                url: 'Share-Hide-PastMedicalHistory',
                data: JSON.stringify(requestData),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    // if success
                    // alert("Success : " + data);

                    $("#App-ShareHidePastMedicalHistory").remove();
                    // $("#widg1 div:nth-child(1)").append(data);
                    //$("#widg1 div:first").before(data);
                    $("#App-PastMedicalHistory").html(data);
                    //if (!BoolVal) {
                    //    $('#Pmedicalhistwidg').css('display', 'none');
                    //    $('.disable_button10').css('color', 'rgb(46, 110, 158)');
                    //}
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
    function ShareHide_Problems(BoolVal) {
        try {
            ShowLoader();
            var requestData = {
                Share: BoolVal
            };

         //   alert(BoolVal);
            $.ajax({
                type: 'POST',
                url: 'Share-Hide-Problems',
                data: JSON.stringify(requestData),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    // if success
                    // alert("Success : " + data);

                    $("#App-ShareHideProblems").remove();
                    $("#App-Problems").html(data);
                    // $("#widg1 div:nth-child(1)").append(data);
                    //$("#widg1 div:first").before(data);
                    //$("#App-Problems").html(data);
                    //if (!BoolVal) {
                    //    $('#probswidg').css('display', 'none');
                    //    $('.disable_button12').css('color', 'rgb(46, 110, 158)');
                    //}
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
    function ShareHide_Insurance(BoolVal) {
        try {
            ShowLoader();
            var requestData = {
                Share: BoolVal
            };

          //  alert(BoolVal);
            $.ajax({
                type: 'POST',
                url: 'Share-Hide-Insurance',
                data: JSON.stringify(requestData),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    // if success
                    // alert("Success : " + data);

                    $("#App-ShareHideInsurance").remove();
                    // $("#widg1 div:nth-child(1)").append(data);
                    //$("#widg1 div:first").before(data);
                    $("#App-Insurance").html(data);
                    //if (!BoolVal) {
                    //    $('#Insurancewidg').css('display', 'none');
                    //    $('.disable_button15').css('color', 'rgb(46, 110, 158)');
                    //}
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
    function ShareHide_Immunization(BoolVal) {
        try {
            ShowLoader();
            var requestData = {
                Share: BoolVal
            };

          //  alert(BoolVal);
            $.ajax({
                type: 'POST',
                url: 'Share-Hide-Immunization',
                data: JSON.stringify(requestData),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    // if success
                    // alert("Success : " + data);

                    $("#App-ShareHideImmunization").remove();
                    // $("#widg1 div:nth-child(1)").append(data);
                    //$("#widg1 div:first").before(data);
                    $("#App-Immunization").html(data);
                    //if (!BoolVal) {
                    //    $('#immunizwidg').css('display', 'none');
                    //    $('.disable_button11').css('color', 'rgb(46, 110, 158)');
                    //}
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
    function ShareHide_Vitals(BoolVal) {
        try {
            ShowLoader();
            var requestData = {
                Share: BoolVal
            };

          //  alert(BoolVal);
            $.ajax({
                type: 'POST',
                url: 'Share-Hide-Vitals',
                data: JSON.stringify(requestData),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    // if success
                    // alert("Success : " + data);

                    $("#App-ShareHideVitals").remove();
                    // $("#widg1 div:nth-child(1)").append(data);
                    //$("#widg1 div:first").before(data);
                    $("#App-Vitals").html(data);
                    //if (!BoolVal) {
                    //    $('#vitalwidg').css('display', 'none');
                    //    $('.disable_button7').css('color', 'rgb(46, 110, 158)');
                    //}
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
    function ShareHide_Documents(BoolVal) {
        try {
            ShowLoader();
            var requestData = {
                Share: BoolVal
            };

         //   alert(BoolVal);
            $.ajax({
                type: 'POST',
                url: 'Share-Hide-Documents',
                data: JSON.stringify(requestData),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    // if success
                    // alert("Success : " + data);

                    $("#App-ShareHideDocuments").remove();
                    // $("#widg1 div:nth-child(1)").append(data);
                    //$("#widg1 div:first").before(data);
                    $("#App-Documents").html(data);
                    //if (!BoolVal) {
                    //    $('#Documentswidg').css('display', 'none');
                    //    $('.disable_button16').css('color', 'rgb(46, 110, 158)');
                    //}
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
    function ShareHide_Allergies(BoolVal) {
        try {
            ShowLoader();
            var requestData = {
                Share: BoolVal
            };

          //  alert(BoolVal);
            $.ajax({
                type: 'POST',
                url: 'Share-Hide-Allergies',
                data: JSON.stringify(requestData),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    // if success
                    // alert("Success : " + data);

                    $("#App-ShareHideAllergies").remove();
                    // $("#widg1 div:nth-child(1)").append(data);
                    //$("#widg1 div:first").before(data);
                    $("#App-Allergies").html(data);
                    //if (!BoolVal) {
                    //    $('#allergywidg').css('display', 'none');
                    //    $('.disable_button13').css('color', 'rgb(46, 110, 158)');
                    //}
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
    function ShareHide_POC(BoolVal) {
        try {
            ShowLoader();
            var requestData = {
                Share: BoolVal
            };

         //   alert(BoolVal);
            $.ajax({
                type: 'POST',
                url: 'Share-Hide-POC',
                data: JSON.stringify(requestData),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    // if success
                    // alert("Success : " + data);

                    $("#App-ShareHidePOC").remove();
                    // $("#widg1 div:nth-child(1)").append(data);
                    //$("#widg1 div:first").before(data);
                    $("#App-POC").html(data);
                    //if (!BoolVal) {
                    //    $('#pocwidg').css('display', 'none');
                    //    $('.disable_button14').css('color', 'rgb(46, 110, 158)');
                    //}
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
    function ShareHide_Procedures(BoolVal)
    {
        try {
            ShowLoader();
            var requestData = {
                Share: BoolVal
            };

          //  alert(BoolVal);
            $.ajax({
                type: 'POST',
                url: 'Share-Hide-Procedures',
                data: JSON.stringify(requestData),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    // if success
                    // alert("Success : " + data);

                    $("#App-ShareHideProcedures").remove();
                    // $("#widg1 div:nth-child(1)").append(data);
                    //$("#widg1 div:first").before(data);
                    $("#App-Procedures").html(data);
                    //if (!BoolVal) {
                    //    $('#Procedureswidg').css('display', 'none');
                    //    $('.disable_button17').css('color', 'rgb(46, 110, 158)');
                    //}
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
    
    function ShareHide_ClinicalInstructions(BoolVal) {
        try {
            ShowLoader();
            var requestData = {
                Share: BoolVal
            };

            //  alert(BoolVal);
            $.ajax({
                type: 'POST',
                url: 'Home/ClinicalInstructionsShareHide',
                data: JSON.stringify(requestData),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    // if success
                    // alert("Success : " + data);

                    $("#App-ShareHideClinicalInstructions").remove();
                    // $("#widg1 div:nth-child(1)").append(data);
                    //$("#widg1 div:first").before(data);
                    $("#App-ClinicalInstructions").html(data);
                    //if (!BoolVal) {
                    //    $('#Procedureswidg').css('display', 'none');
                    //    $('.disable_button17').css('color', 'rgb(46, 110, 158)');
                    //}
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
   
    function hide_widg1() {
        $('#widg1').css('display', 'none');
        $('.disable_button1').css('color', 'rgb(46, 110, 158)');
        DeleteWidget("widg1");
    }


    function hide_widg2() {
        $('#widg2').css('display', 'none');
        $('.disable_button2').css('color', 'rgb(46, 110, 158)');
        DeleteWidget("widg2");
        
    }

    function ShareHide_widg3(BoolVal) {
        try {
            ShowLoader();
            var requestData = {
                Share: BoolVal
            };

            //alert(BoolVal);
            $.ajax({
                type: 'POST',
                url: 'Home/LabsShareHide',
                data: JSON.stringify(requestData),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    // if success
                    // alert("Success : " + data);

                    $("#Lab-ShareHide").remove();
                    // $("#widg1 div:nth-child(1)").append(data);
                    //$("#widg1 div:first").before(data);
                    $("#Lab-Section").html(data);
                    //if (!BoolVal) {
                    //    $('#widg3').css('display', 'none');
                    //    $('.disable_button3').css('color', 'rgb(46, 110, 158)');
                    //}
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
    function hide_widg3() {
        $('#widg3').css('display', 'none');
        $('.disable_button3').css('color', 'rgb(46, 110, 158)');
        DeleteWidget("labWidget");
    }


    function hide_widg4() {
        $('#widg4').css('display', 'none');
        $('.disable_button4').css('color', 'rgb(46, 110, 158)');
        DeleteWidget("widg4");
    }

    function hide_widg5() {
        $('#widg5').css('display', 'none');
        $('.disable_button5').css('color', 'rgb(46, 110, 158)');
    }


    function visitswidg() {
        $('#visitswidg').css('display', 'none');
        $('.disable_button6').css('color', 'rgb(46, 110, 158)');
        DeleteWidget("visitswidg");
    }

    function vitalwidg() {
        $('#vitalwidg').css('display', 'none');
        $('.disable_button7').css('color', 'rgb(46, 110, 158)');
        DeleteWidget("vitalwidg");
    }
        
    function allergywidg() {
        $('#allergywidg').css('display', 'none');
        $('.disable_button13').css('color', 'rgb(46, 110, 158)');
        DeleteWidget("allergywidg");
    }
        

    function pocwidg() {
        $('#pocwidg').css('display', 'none');
        $('.disable_button14').css('color', 'rgb(46, 110, 158)');
        DeleteWidget("pocwidg");
    }

    function socialhistwidg() {
        $('#socialhistwidg').css('display', 'none');
        $('.disable_button8').css('color', 'rgb(46, 110, 158)');
        DeleteWidget("socialhistwidg");
    }
    function familyhistwidg() {
        $('#familyhistwidg').css('display', 'none');
        $('.disable_button9').css('color', 'rgb(46, 110, 158)');
        DeleteWidget("familyhistwidg");
    }
    function Pmedicalhistwidg() {
        $('#Pmedicalhistwidg').css('display', 'none');
        $('.disable_button10').css('color', 'rgb(46, 110, 158)');
        DeleteWidget("Pmedicalhistwidg");
    }
    function immunizwidg() {
        $('#immunizwidg').css('display', 'none');
        $('.disable_button11').css('color', 'rgb(46, 110, 158)');
        DeleteWidget("immunizwidg");
    }

    function  probswidg(){
        $('#probswidg').css('display', 'none');
        $('.disable_button12').css('color', 'rgb(46, 110, 158)');
        DeleteWidget("probswidg");
    }
    function insurancewidg()
    {
        $('#Insurancewidg').css('display', 'none');
        $('.disable_button15').css('color', 'rgb(46, 110, 158)');
        DeleteWidget("Insurancewidg");
    }
    function documentwidg() {
        $('#Documentswidg').css('display', 'none');
        $('.disable_button16').css('color', 'rgb(46, 110, 158)');
        DeleteWidget("Documentswidg");
    }
    function Procedurewidg() {
        $('#Procedureswidg').css('display', 'none');
        $('.disable_button17').css('color', 'rgb(46, 110, 158)');
        DeleteWidget("Procedurewidg");
    }
    function ClinInstructionWidgetHide() {
        $('#ClinicalInstructionsWidget').css('display', 'none');
        $('.disable_button18').css('color', 'rgb(46, 110, 158)');
        DeleteWidget("ClinicalInstructionsWidget");
    }
    function DeleteWidget(name) {
        ShowLoader();
        $.ajax({
            url: "index-WidgetId-Delete",
            type: "POST",
            dataType: "json",
            data: { WidgetName: name, action: 'hide' },
            type: "POST",
            success: function (result) {
                $("#rightPanel").html(result);
                $('.spinnermodal').hide();
                HideLoader();
            }
        });
    }
    function ShowWidget(name) {
        ShowLoader();
        $.ajax({
            url: "index-WidgetId-Delete",
            type: "POST",
            dataType: "json",
            data: { WidgetName: name, action: 'update', highestvalue: $("#HighestCountHidden").val() },
            type: "POST",
            success: function (result) {
                $("#rightPanel").html(result);
                $('.spinnermodal').hide();
                var newcurrentpageTemp = parseInt(parseInt($("#HighestCountHidden").val())) + 2;
                $("#HighestCountHidden").val(newcurrentpageTemp);
                HideLoader();
            }
        });
        // alert($("#HighestCountHidden").val());
    }
    // show buttons
    function show_widg1() {
        $('#appointmentWidgetButton').addClass('disable_button1');
        $('#widg1').css('display', 'block');
        $('.disable_button1').css('color', 'rgb(220, 220, 220)');
        $("#tbl-Appointments").fixedHeaderTable();
        ShowWidget("widg1");//Appointment
    }
    function show_widg2() {
        $('#medicationWidgetButton').addClass('disable_button2');
        $('#widg2').css('display', 'block');
        $('.disable_button2').css('color', 'rgb(220, 220, 220)');
        $("#tbl-Medications").fixedHeaderTable();
        ShowWidget("widg2");//Medication
    }
    function show_widg3() {
        $('#labWidgetButton').addClass('disable_button3');
        $('#widg3').css('display', 'block');
        $('.disable_button3').css('color', 'rgb(220, 220, 220)');
        $("#tbl-LabTests").fixedHeaderTable();
        ShowWidget("labWidget");//LabTest
    }
    function show_widg4() {
        $('#statementWidgetButton').addClass('disable_button4');
        $('#widg4').css('display', 'block');
        $('.disable_button4').css('color', 'rgb(220, 220, 220)');
        $("#tbl-Statement").fixedHeaderTable();
        ShowWidget("widg4");//Statement
    }
    function show_widg5() {
        $('#widg5').css('display', 'block');
        $('.disable_button5').css('color', 'rgb(220, 220, 220)');
    }
    function show_visitswidg() {
        $('#visitsWidgetButton').addClass('disable_button6');
        $('#visitswidg').css('display', 'block');
        $('.disable_button6').css('color', 'rgb(220, 220, 220)');
        $("#tbl-Visits").fixedHeaderTable();
        ShowWidget("visitswidg");
    }
    function show_vitalwidg() {
        $('#vitalWidgetButton').addClass('disable_button7');
        $('#vitalwidg').css('display', 'block');
        $('.disable_button7').css('color', 'rgb(220, 220, 220)');
        $("#tbl-VitalSigns").fixedHeaderTable();
        ShowWidget("vitalwidg");
    }
    function show_socialhistwidg() {
        $('#socialhistWidgetButton').addClass('disable_button8');
        $('#socialhistwidg').css('display', 'block');
        $('.disable_button8').css('color', 'rgb(220, 220, 220)');
        $("#tbl-SocialHistory").fixedHeaderTable();
        ShowWidget("socialhistwidg");
    }
    function show_familyhistwidg() {
        $('#familyhistWidgetButton').addClass('disable_button9');
        $('#familyhistwidg').css('display', 'block');
        $('.disable_button9').css('color', 'rgb(220, 220, 220)');
        $("#tbl-FamilyHistory").fixedHeaderTable();
        ShowWidget("familyhistwidg");
    }
    function show_Pmedicalhistwidg() {
        $('#PmedicalhistWidgetButton').addClass('disable_button10');
        $('#Pmedicalhistwidg').css('display', 'block');
        $('.disable_button10').css('color', 'rgb(220, 220, 220)');
        $("#tbl-PastMedications").fixedHeaderTable();
        ShowWidget("Pmedicalhistwidg");
    }
    function show_immunizwidg() {
        $('#immunizWidgetButton').addClass('disable_button11');
        $('#immunizwidg').css('display', 'block');
        $('.disable_button11').css('color', 'rgb(220, 220, 220)');
        $("#tbl-Immunizations").fixedHeaderTable();
        ShowWidget("immunizwidg");
    }
    function show_probswidg() {
        $('#probsWidgetButton').addClass('disable_button12');
        $('#probswidg').css('display', 'block');
        $('.disable_button12').css('color', 'rgb(220, 220, 220)');
        $("#tbl-Problems").fixedHeaderTable();
        ShowWidget("probswidg");
    }
    function show_allergywidg() {
        $('#allergyWidgetButton').addClass('disable_button13');
        $('#allergywidg').css('display', 'block');
        $('.disable_button13').css('color', 'rgb(220, 220, 220)');
        $("#tbl-Allergies").fixedHeaderTable();
        ShowWidget("allergywidg");
    }
    function show_pocwidg() {
        $('#pocWidgetButton').addClass('disable_button14');
        $('#pocwidg').css('display', 'block');
        $('.disable_button14').css('color', 'rgb(220, 220, 220)');
        $("#tbl-poc").fixedHeaderTable();
        ShowWidget("pocwidg");
    }
    function show_insurancewidg()
    {
        $('#insuranceWidgetButton').addClass('disable_button15');
        $('#Insurancewidg').css('display', 'block');
        $('.disable_button15').css('color', 'rgb(220, 220, 220)');
        $("#tbl-Insurance").fixedHeaderTable();
        ShowWidget("Insurancewidg");
    }
    function show_docewidg()
    {
        $('#documentsWidgetButton').addClass('disable_button16');
        $('#Documentswidg').css('display', 'block');
        $('.disable_button16').css('color', 'rgb(220, 220, 220)');
        $("#tbl-Documents").fixedHeaderTable();
        ShowWidget("Documentswidg");
    }
    function show_Procedurewidg()
    {
        $('#ProceduresWidgetButton').addClass('disable_button17');
        $('#Procedureswidg').css('display', 'block');
        $('.disable_button17').css('color', 'rgb(220, 220, 220)');
        $("#tbl-Procedure").fixedHeaderTable();
        ShowWidget("Procedurewidg");
    }
    function show_ClinicalInstructionsWidget() {
        $('#ClinicalInstructionsWidgetButton').addClass('disable_button18');
        $('#ClinicalInstructionsWidget').css('display', 'block');
        $('.disable_button18').css('color', 'rgb(220, 220, 220)');
        $("#tbl-ClinicalInstruction").fixedHeaderTable();
        ShowWidget("ClinicalInstructionsWidget");
    }
    function enabling_buttons() {

        $('.disable_button').css('color', 'rgb(46, 110, 158)');
    }
    function ShowLoader() {
        var body_height = parseInt($('body').height());
        $('#page_loader').css('height', body_height);
        document.getElementById('page_loader').style.display = 'block';
    }
    function HideLoader() {
        document.getElementById('page_loader').style.display = 'none';
    }
    function imageUpload()
    {
        // $('#newCanvas').html('<canvas  id = "mycanvas" style="cursor:pointer;  border: 1px solid rgb(166, 201, 226); -moz-border-radius: 5px;  -webkit-border-radius: 5px; border-radius: 5px; margin-bottom: 5px; position:relative;" width = "200" height = "200" align="right" > </canvas>');
         
        $("#profileImage-form").dialog("open");
    }


    // start of add propfile image pop up script

    $(function () {

        tips = $(".validateTips");



        $("#profileImage-form").dialog({
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
                "Upload": function () {
                    chkAccess();
                    //var c = document.getElementById("mycanvas");
                    //var ctx = c.getContext("2d");
                    //var image = ctx.getImageData(0, 0, 200, 200);
                    //var data = image.data;

                    var bValid = true;


                    if (bValid) {
                        //ajax 
                        try {
                              
                            if (jqXHRData) {
                                var isStartUpload = true;
                                var uploadFile = jqXHRData.files[0];

                                if (!(/\.(gif|jpg|jpeg|tiff|png)$/i).test(uploadFile.name)) {
                                    alert('You must select an image file only');
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
                            //alert(data);
                            //$.ajax({
                            //    type: 'POST',
                            //    url: 'master-images-save',
                            //    data: {Image: data},
                            //    dataType: 'json',
                            //    success: function (response) {
                            //        alert(response);




                            //    },
                            //    error: function (xhr, ajaxOptions, thrownError) {
                            //        //if error

                            //        alert('Error : ' + xhr.message);
                            //        HideLoader();
                            //    },
                            //    complete: function (data) {
                            //        // if completed
                            //        HideLoader();

                            //    },
                            //    async: true,
                            //    processData: false
                            //});
                        } catch (err) {

                            if (err && err !== "") {
                                alert(err.message);
                                HideLoader();
                            }
                        }
                        $(this).dialog("close");
                        ShowLoader();


                        //end ajax

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

    //- end of add profile image popup script


    function getDirectory()
    {
           
        strFile = document.FileForm.filename.value;
        intPos = strFile.lastIndexOf("\\");
        strDirectory = strFile.substring(0, intPos);
        alert(strDirectory);
        //            document.FileForm.Directory.value = strDirectory;




    }

    

    function MM_swapImgRestore() { //v3.0
        var i, x, a = document.MM_sr; for (i = 0; a && i < a.length && (x = a[i]) && x.oSrc; i++) x.src = x.oSrc;
    }
    function MM_preloadImages() { //v3.0
        var d = document; if (d.images) {
            if (!d.MM_p) d.MM_p = new Array();
            var i, j = d.MM_p.length, a = MM_preloadImages.arguments; for (i = 0; i < a.length; i++)
                if (a[i].indexOf("#") != 0) { d.MM_p[j] = new Image; d.MM_p[j++].src = a[i]; }
        }
    }

    function MM_findObj(n, d) { //v4.01
        var p, i, x; if (!d) d = document; if ((p = n.indexOf("?")) > 0 && parent.frames.length) {
            d = parent.frames[n.substring(p + 1)].document; n = n.substring(0, p);
        }
        if (!(x = d[n]) && d.all) x = d.all[n]; for (i = 0; !x && i < d.forms.length; i++) x = d.forms[i][n];
        for (i = 0; !x && d.layers && i < d.layers.length; i++) x = MM_findObj(n, d.layers[i].document);
        if (!x && d.getElementById) x = d.getElementById(n); return x;
    }

    function MM_swapImage() { //v3.0
        var i, j = 0, x, a = MM_swapImage.arguments; document.MM_sr = new Array; for (i = 0; i < (a.length - 2) ; i += 3)
            if ((x = MM_findObj(a[i])) != null) { document.MM_sr[j++] = x; if (!x.oSrc) x.oSrc = x.src; x.src = a[i + 2]; }
    }


    function DownloadHealthRecord(id, flag) {
            
            
        if (flag == 'visit')
        { sec_arg = $('#cmbContinuityFacilityDownload option:selected').val(); }
        else if (flag == 'location') {
            sec_arg = id;
            id = $('#cmbContinuityVisitsDownload option:selected').val();
        }

        if (flag == 'location') {
                
            //filling dropdown start 
            try {
                ShowLoader();
                //var IsPrimaryData =
                    
                var requestData = {

                    facilityId: sec_arg,
                    ExtensionToggleFunct: " toggle_combobox_ForDownloadAndSend();",
                    ExtensionFilterFunct: "DownloadHealthRecord(this.value,'visit');",
                    ExtensionIdName: "cmbContinuityVisitsDownload",
                    visitId: id

                };
                // 
                    
                $.ajax({
                    type: 'POST',
                    url: 'downloadhealthrecords-visit-dropdown',
                    data: JSON.stringify(requestData),
                    dataType: 'json',
                    contentType: 'application/json; charset=utf-8',
                    success: function (data) {
                        // if success
                            
                        $("#layoutComboBoxPortlet").html(data);
                        id = $('#cmbContinuityVisitsDownload option:selected').val();
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
                        //HideLoader();
                        // HideLoader();
                    },
                    complete: function (data) {
                        // if completed
                        HideLoader();

                    },
                    async: false,
                    processData: false
                });
                if ($('#cmbContinuityFacilityDownload :selected').text() == 'Patient Entered') {
                        
                    $('#cmbContinuityVisitsDownload').text("");
                    $('#cmbContinuityVisitsDownload').attr('disabled', true);
                }
            } catch (err) {

                if (err && err !== "") {
                    alert(err.message);
                }
            }
            //filling visit drop down end
        }

    }

    function SendHealthRecord(id, flag) {
           
            
        if (flag == 'visit')
        { sec_arg = $('#cmbContinuityFacilitySend option:selected').val(); }
        else if (flag == 'location') {
            sec_arg = id;
            id = $('#cmbContinuityVisitsSend option:selected').val();
        }

        if (flag == 'location') {
                
            //filling dropdown start 
            try {
                ShowLoader();
                //var IsPrimaryData =
                var requestData = {
                    facilityId: sec_arg,
                    ExtensionToggleFunct: " toggle_combobox_ForDownloadAndSend();",
                    ExtensionFilterFunct: "SendHealthRecord(this.value,'visit');",
                    ExtensionIdName: "cmbContinuityVisitsSend",
                    visitId: id

                };
                // 
                    
                $.ajax({
                    type: 'POST',
                    url: 'sendhealthrecords-visit-dropdown',
                    data: JSON.stringify(requestData),
                    dataType: 'json',
                    contentType: 'application/json; charset=utf-8',
                    success: function (data) {
                        // if success

                        $("#layoutComboBoxPortletSendHealth").html(data);
                        id = $('#cmbContinuityVisitsSend option:selected').val();
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
                        HideLoader();

                    },
                    async: false,
                    processData: false
                });
                if ($('#cmbContinuityFacilitySend :selected').text() == 'Patient Entered') {
                        
                    $('#cmbContinuityVisitsSend').text("");
                    $('#cmbContinuityVisitsSend').attr('disabled', true);
                }
            } catch (err) {

                if (err && err !== "") {
                    alert(err.message);
                }
            }
            //filling visit drop down end
        }

    }

    $(document).ready(function () {
          
        if ($('#cmbContinuityFacilityDownload :selected').text() == 'Patient Entered') {
            $('#cmbContinuityVisitsDownload').text("");
            $('#cmbContinuityVisitsDownload').attr('disabled', true);
            $('#cmbContinuityFacilityDownload').attr('disabled', false);
        }

        if ($('#cmbContinuityFacilitySend :selected').text() == 'Patient Entered') {
            $('#cmbContinuityVisitsSend').text("");
            $('#cmbContinuityVisitsSend').attr('disabled', true);
            $('#cmbContinuityFacilitySend').attr('disabled', false);
        }

    });
        

    function toggle_combobox_ForDownloadAndSend() {
       
        if ($('#cmbContinuityFacilityDownload :selected').text() != 'Patient Entered') {

            $('#cmbContinuityVisitsDownload').attr('disabled', false);
            $('#createPresentMedication,#createaddproblems,#addvitalscreation,#createaddimmunizations,#createaddallergies,#addvitalscreation,#createaddpast,#createaddfamily,#createaddappointments,#createaddsocial').css('display', 'none');

        }

        else {
            alert('else cond for download record');
            $('#cmbContinuityVisitsDownload').text("");
            $('#cmbContinuityVisitsDownload').attr('disabled', true);
            //$('#cmbVisitsHome').val(0);
            // $('#createPresentMedication,#createaddstatements,#createaddproblems,#addvitalscreation,#createaddimmunizations,#createaddallergies,#addvitalscreation,#createaddpast,#createaddfamily,#createaddappointments,#createaddsocial').css('display', 'block');
        }
   

        
        if ($('#cmbContinuityFacilitySend :selected').text() != 'Patient Entered') {

            $('#cmbContinuityVisitsSend').attr('disabled', false);
            $('#createPresentMedication,#createaddproblems,#addvitalscreation,#createaddimmunizations,#createaddallergies,#addvitalscreation,#createaddpast,#createaddfamily,#createaddappointments,#createaddsocial').css('display', 'none');

        }

        else if ($('#cmbContinuityFacilitySend :selected').text() == 'Patient Entered') {
                
            $('#cmbContinuityVisitsSend').text("");
            $('#cmbContinuityVisitsSend').attr('disabled', true);
            //$('#cmbVisitsHome').val(0);
            // $('#createPresentMedication,#createaddstatements,#createaddproblems,#addvitalscreation,#createaddimmunizations,#createaddallergies,#addvitalscreation,#createaddpast,#createaddfamily,#createaddappointments,#createaddsocial').css('display', 'block');
        }

    }
    $(function () {
        $("#txtCountRefills").keypress(function (event) {
            var key = event.which;
       //     alert(key);
            if (!((key >= 48 && key <= 57) || (key == 8) || (key == 0)))
                event.preventDefault();
        });
    });
  
    //Added By Khusroo
    $(document).ready(function () {
        $.ajax({
            dataType: "json",
            type: 'GET',
            url: 'Get-Messages-Counter',
            success: function (data) {
                $('#circle').html(data.TotalMessages);
                $('#hdAppointmentMessages').html(data.AppointmentMessages);
                $('#hdGeneralMessages').html(data.GeneralMessages);
                $('#hdMedicationMessages').html(data.MedicationMessages);
                $('#hdReferralMessages').html(data.ReferralMessages);
                $('#hdTotalMessages').html(data.TotalMessages);

                $('#GeneralMessages').html("Inbox(<b style='font-size:14px;'>" + data.GeneralMessages + "</b>)");
                $('#AppointmentMessages').html("Appointments(<b style='font-size:14px;'>" + data.AppointmentMessages + "</b>)");
                $('#MedicationMessages').html("Refill Requests(<b style='font-size:14px;'>" + data.MedicationMessages + "</b>)");
                $('#ReferralMessages').html("Referral Requests(<b style='font-size:14px;'>" + data.ReferralMessages + "</b>)");
                $('#SentMessages').html("Sent Items<b style='font-size:14px;'></b>");
                $('#DeleteMessages').html("Deleted<b style='font-size:14px;'></b>");
            }
        });
        // HideLoader();
    });

    function RefillDialog() {
        chkAccess();
        $("#refillcount").append("You have <strong>" + 256 + "</strong> characters remaining");

        $("#PharmAddress").text("");
        var found = $("#cmbRFPharmacy").val();
      
        if (found != null) {

            var spl = $("#cmbRFPharmacy option:first").val().split('|');
            $('#PharmAddress').text(spl[1]);
            $("#Phone").val(spl[2]);
            $("#dialog-refill").dialog("open");
        }
        else {
            $("#dialog-refill").dialog("open");

        }

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
                duration: 100
            },
            hide: {
                effect: "blind",
                duration: 100
            },
            height: 450,
            width: 700,
            modal: true,
            buttons: {
                "Send": function () {

                    var bValid = true;
                    var refil = $("#txtCountRefills").val();
                    
                    allFields.removeClass("ui-state-error");
                    //   bValid = bValid && checkLength(Location, "Please Select Location");
                    // bvalid = bvalid && checkLength(Doctor, "Please Select Doctor");


                    if (bValid) {

                        //  Save routine and call of Action
                        if (($('#cmbAppointmentFacilityRefill option:selected').val()) == -1 && ($('#cmbMedication option:selected').val()) == -1 && ($('#cmbDoctor option:selected').val()) == -1 && ($('#cmbRFPharmacy option:selected').val().split('|')[0]) == -1) {
                            alert("Please Select Location , Medication , Doctor , Pharmacy");
                            return false;
                        }
                        else if (($('#cmbAppointmentFacilityRefill option:selected').val()) == -1 && ($('#cmbMedication option:selected').val()) == -1 && ($('#cmbDoctor option:selected').val()) == -1) {
                            alert("Please Select Location , Medication , Doctor");
                            return false;
                        }
                        else if (($('#cmbAppointmentFacilityRefill option:selected').val()) == -1 && ($('#cmbMedication option:selected').val()) == -1 && ($('#cmbRFPharmacy option:selected').val().split('|')[0]) == -1) {
                            alert("Please Select Location , Medication , Pharmacy");
                            return false;
                        }
                        else if (($('#cmbMedication option:selected').val()) == -1 && ($('#cmbDoctor option:selected').val()) == -1 && ($('#cmbRFPharmacy option:selected').val().split('|')[0]) == -1) {
                            alert("Please Select Medication , Doctor , Pharmacy");
                            return false;
                        }
                        else if (($('#cmbAppointmentFacilityRefill option:selected').val()) == -1 && ($('#cmbDoctor option:selected').val()) == -1 && ($('#cmbRFPharmacy option:selected').val().split('|')[0]) == -1) {
                            alert("Please Select Location , Doctor , Pharmacy");
                            return false;
                        }
                        else if (($('#cmbAppointmentFacilityRefill option:selected').val()) == -1 && ($('#cmbMedication option:selected').val()) == -1) {
                            alert("Please Select Location , Medication");
                            return false;
                        }
                        else if (($('#cmbDoctor option:selected').val()) == -1 && ($('#cmbRFPharmacy option:selected').val().split('|')[0]) == -1) {
                            alert("Please Select Doctor , Pharmacy");
                            return false;
                        }
                        else if (($('#cmbMedication option:selected').val()) == -1 && ($('#cmbDoctor option:selected').val()) == -1) {
                            alert("Please Select Medication , Doctor");
                            return false;
                        }
                        else if (($('#cmbAppointmentFacilityRefill option:selected').val()) == -1 && ($('#cmbDoctor option:selected').val()) == -1) {
                            alert("Please Select Location , Doctor");
                            return false;
                        }
                        else if (($('#cmbMedication option:selected').val()) == -1 && ($('#cmbRFPharmacy option:selected').val().split('|')[0]) == -1) {
                            alert("Please Select Medication , Pharmacy");
                            return false;
                        }
                        else if (($('#cmbAppointmentFacilityRefill option:selected').val()) == -1 && ($('#cmbRFPharmacy option:selected').val().split('|')[0]) == -1) {
                            alert("Please Select Location , Pharmacy");
                            return false;
                        }

                        else if (($('#cmbAppointmentFacilityRefill option:selected').val()) == -1) {
                            alert("Please Select Location");
                            return false;
                        }
                        else if (($('#cmbMedication option:selected').val()) == -1) {
                            alert("Please Select Medication");
                            return false;
                        }
                        else if (($('#cmbDoctor option:selected').val()) == -1) {
                            alert("Please Select Doctor");
                            return false;
                        }
                        else if (($("#cmbRFPharmacy option:selected").val().split('|')[0]) == -1) {
                            alert("Please Select Pharmacy");
                            return false;
                        }
                        else if (refil.length > 3)
                        {
                            alert("Number of refills should not exceed 3 digits");
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
                                "PharmacyPhone": $.trim($('#Phone').val()),
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
                                    alert("Refill Request Sent Successfully");



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
                    $(".validateTips ").empty();
                    $("span").removeClass("ui-state-highlight");
                    $("#refillcount").text("");
                    $(this).dialog("close");
                }
            },
            close: function () {
                allFields.val("").removeClass("ui-state-error");
                $(".validateTips ").empty();
                $("#refillcount").text("");
                $("span").removeClass("ui-state-highlight");
            }
        });

        // start5  of script of add mediatin
        $("#dialog-form").dialog({
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
                    //bValid = bValid && checkLength(Pharmacy, "Please Select Pharmacy");
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
                                //Pharmacy: (($('#cmbPharmacy option:selected').text()) == '--Select--') ? "" : $.trim($('#cmbPharmacy option:selected').text()),
                                Note: $.trim($('#txtInstructions').val()),
                                // Days: $.trim($('#txtDays').val()),
                                Quantity: $.trim($('#txtQty').val()),
                                Refills: $.trim($('#txtRefills').val()),
                                duringvisit: DuringVisit,
                                Diagnosis: $.trim($('#txtDiagnosis').val()),
                                Dose: $.trim($('#txtDose').val()),
                                DoseUnit: $.trim($('#txtDoseUnit').val())
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
            $("#dialog-form").dialog("option", "title", "Add Medication");
            $("#dialog-form").dialog("open");
        });

        // end of script of add addmen



        $("#ComposeMessage").dialog
            ({
                create: function (event, ui) {
                    $(event.target).parent().css('position', 'fixed');
                },
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
                width: 750,
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
                                    alert("Please Select Location , To , Subject , and MessagePriority");
                                    return false;
                                }
                                else if (($('#cmbAppointmentDoctor option:selected').val()) == -1 && ($('#cmbMessageType option:selected').val()) == -1 && ($('#cmbMessageUrgency option:selected').val()) == -1) {
                                    alert("Please Select To , Subject , MessagePriority");
                                    return false;
                                }
                                else if (($('#cmbAppointmentFacility option:selected').val()) == -1 && ($('#cmbMessageType option:selected').val()) == -1 && ($('#cmbMessageUrgency option:selected').val()) == -1) {
                                    alert("Please Select Location , Subject , MessagePriority");
                                    return false;
                                }
                                else if (($('#cmbAppointmentFacility option:selected').val()) == -1 && ($('#cmbAppointmentDoctor option:selected').val()) == -1 && ($('#cmbMessageUrgency option:selected').val()) == -1) {
                                    alert("Please Select Location , To , MessagePriority");
                                    return false;
                                }

                                else if (($('#cmbAppointmentFacility option:selected').val()) == -1 && ($('#cmbAppointmentDoctor option:selected').val()) == -1 && ($('#cmbMessageType option:selected').val()) == -1) {
                                    alert("Please Select Location , To , Subject");
                                    return false;
                                }
                                else if (($('#cmbAppointmentFacility option:selected').val()) == -1 && ($('#cmbAppointmentDoctor option:selected').val()) == -1) {
                                    alert("Please Select Location , To ");
                                    return false;
                                }
                                else if (($('#cmbMessageType option:selected').val()) == -1 && ($('#cmbMessageUrgency option:selected').val()) == -1) {
                                    alert("Please Select Subject , MessagePriority ");
                                    return false;
                                }
                                else if (($('#cmbAppointmentDoctor option:selected').val()) == -1 && ($('#cmbMessageType option:selected').val()) == -1) {
                                    alert("Please Select To , Subject ");
                                    return false;
                                }
                                else if (($('#cmbAppointmentDoctor option:selected').val()) == -1 && ($('#cmbMessageUrgency option:selected').val()) == -1) {
                                    alert("Please Select To , MessagePriority ");
                                    return false;
                                }
                                else if (($('#cmbAppointmentFacility option:selected').val()) == -1 && ($('#cmbMessageType option:selected').val()) == -1) {
                                    alert("Please Select Location , Subject ");
                                    return false;
                                }
                                else if (($('#cmbAppointmentFacility option:selected').val()) == -1) {
                                    alert("Please Select Location");
                                    return false;
                                }
                                else if (($('#cmbAppointmentDoctor option:selected').val()) == -1) {
                                    alert("Please Select To ");
                                    return false;
                                }
                                else if (($('#cmbMessageType option:selected').val()) == -1) {
                                    alert("Please Select Subject ");
                                    return false;
                                }
                                else if (($('#cmbMessageUrgency option:selected').val()) == -1) {
                                    alert("Please Select MessagePriority ");
                                    return false;
                                }
                                var textMessage = $.trim($('#txtMessageWrite').val());
                                if (textMessage == null || textMessage =="")
                                {
                                    alert("Please enter a Message");
                                    return false;
                                }

                                var txt = textMessage.replace(/\n/g, "\\n");
                                var requestData = {
                                    AttachmentName: $.trim($('#files').val()),
                                    AttachmentTest: $('#ByteData').val(),
                                    ProviderId_To: $.trim($('#cmbAppointmentDoctor  option:selected').val()),
                                    FacilityId: $.trim($('#cmbAppointmentFacility  option:selected').val()),
                                    MessageRequest: txt,
                                    MessageTypeId: $.trim($('#cmbMessageType  option:selected').val()),
                                    MessageUrgency: $.trim($('#cmbMessageUrgency  option:selected').val()),
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
                                ShowLoader();
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
                                          //  $("#MessageHeaderGrid").html(data);

                                            alert("Message Sent Successfully");

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
            chkAccess();
            $("#txtMessageWrite").val('');
            clearAttach();
            $('#browse').val('Browse..');
            $('#btncancelupload').val('Cancel');
            $('#hdSentFlag').val("Compose");
            $('#BrowseName').text('');
            //  $('#counter').append("");
            $("#counter").append("You have <strong>" + 256 + "</strong> characters remaining");

            $("#ComposeMessage").dialog("open");
            // $('#browse').hide();
            $('#files').hide();
        });
    });
   
function clearAttach() {
        //  $('#files').replaceWith('<input type="file" id="files" name="file"/>');
        // $('#ByteData').val('');
        // document.getElementById('files').addEventListener('change', handleFileSelect, false);
        $('#BrowseName').text('');
        $('#btncancelupload').hide();
    }
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
                duration: 100
            },
            hide: {
                effect: "blind",
                duration: 100
            },

            width: 850,
            modal: true,
            buttons: {
                "Send": function () {
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

                    //var abc = $('#cmbMessageUrgency1 option:selected').val();
                    //alert("value of urgent" + abc);

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
                                alert("Please Select Location , Provider , urgency , Preferred Time ");
                                return false;
                            }
                            else if (($('#cmbAppointmentDoctor1 option:selected').val()) == -1 && ($('#cmbMessageUrgency1 option:selected').val()) == -1 && ($('#cmbPreftime option:selected').val()) == -1) {
                                alert("Please Select Provider , urgency , Preferred Time ");
                                return false;
                            }
                            else if (($('#cmbAppointmentFacility1 option:selected').val()) == -1 && ($('#cmbMessageUrgency1 option:selected').val()) == -1 && ($('#cmbPreftime option:selected').val()) == -1) {
                                alert("Please Select Location , urgency , Preferred Time ");
                                return false;
                            }
                            else if (($('#cmbAppointmentFacility1 option:selected').val()) == -1 && ($('#cmbAppointmentDoctor1 option:selected').val()) == -1 && ($('#cmbPreftime option:selected').val()) == -1) {
                                alert("Please Select Location , Provider , Preferred Time ");
                                return false;
                            }
                            else if (($('#cmbAppointmentFacility1 option:selected').val()) == -1 && ($('#cmbAppointmentDoctor1 option:selected').val()) == -1 && ($('#cmbMessageUrgency1 option:selected').val()) == -1) {
                                alert("Please Select Location , Provider , Urgency ");
                                return false;
                            }
                            else if (($('#cmbAppointmentFacility1 option:selected').val()) == -1 && ($('#cmbAppointmentDoctor1 option:selected').val()) == -1) {
                                alert("Please Select Location , Provider  ");
                                return false;
                            }
                            else if (($('#cmbMessageUrgency1 option:selected').val()) == -1 && ($('#cmbPreftime option:selected').val()) == -1) {
                                alert("Please Select Urgency , Preferred Time  ");
                                return false;
                            }
                            else if (($('#cmbAppointmentDoctor1 option:selected').val()) == -1 && ($('#cmbMessageUrgency1 option:selected').val()) == -1) {
                                alert("Please Select Provider , Urgency  ");
                                return false;
                            }
                            else if (($('#cmbAppointmentFacility1 option:selected').val()) == -1 && ($('#cmbMessageUrgency1 option:selected').val()) == -1) {
                                alert("Please Select Location , Urgency  ");
                                return false;
                            }
                            else if (($('#cmbAppointmentDoctor1 option:selected').val()) == -1 && ($('#cmbPreftime option:selected').val()) == -1) {
                                alert("Please Select Provider , Preferred Time  ");
                                return false;
                            }
                            else if (($('#cmbAppointmentFacility1 option:selected').val()) == -1) {
                                alert("Please Select Location  ");
                                return false;
                            }
                            else if (($('#cmbAppointmentDoctor1 option:selected').val()) == -1) {
                                alert("Please Select Provider  ");
                                return false;
                            }
                            else if (($('#cmbMessageUrgency1 option:selected').val()) == -1) {
                                alert("Please Select Urgency ");
                                return false;
                            }
                            else if (($('#cmbPreftime option:selected').val()) == -1) {
                                alert("Please Select Preferred Time ");
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
                                MessageUrgency: $('#cmbMessageUrgency1 option:selected').val(),
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
                                    
                                    $("#tbl-App").html(data);
                                    //alert(data);
                                    $("#tbl-Appointments").fixedHeaderTable();
                                    alert("Appointment Request Sent Successfully");
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

            }
        });
        $("#createappbtn")
        .button()
        .click(function () {
            chkAccess();
            $("#appcount").append("You have <strong>" + 256 + "</strong> characters remaining");

            $("#appbtn-form").dialog("open");
        });
        //$("#createaddappointments")
        //.button()
        //.click(function () {
        //    $("#appbtn-form").dialog("open");
        //});

    });

    $(function () {

        tips = $(".validateTips");



        $("#reqbtn-form").dialog({
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
                "Send": function () {
                    var bValid = true;



                    //function chkStr(value) {
                    //    var str;
                    //    if (value == true) {
                    //        str = '1';
                    //        return str;
                    //    }
                    //    else if (value == false) {
                    //        str = '0';
                    //        return str;
                    //    }
                    //};
                    //var PreferredPeriod1 = chkStr($('#chkRmor').prop('checked'));
                    //PreferredPeriod1 += chkStr($('#chkRaft').prop('checked'));
                    //PreferredPeriod1 += chkStr($('#chkReve').prop('checked'));

                    //var PreferredWeekDay1 = chkStr($('#chkRmon').prop('checked'));
                    //PreferredWeekDay1 += chkStr($('#chkRtue').prop('checked'));
                    //PreferredWeekDay1 += chkStr($('#chkRwed').prop('checked'));
                    //PreferredWeekDay1 += chkStr($('#chkRthu').prop('checked'));
                    //PreferredWeekDay1 += chkStr($('#chkRfri').prop('checked'));
                    //PreferredWeekDay1 += chkStr($('#chkRsat').prop('checked'));
                    //PreferredWeekDay1 += chkStr($('#chkRsun').prop('checked'));

                    if (bValid) {
                        //ajax 
                        try {
                            //if (($('#cmbAppointmentFacility3 option:selected').val()) == -1 && ($('#cmbRProvider option:selected').val()) == -1 && ($('#cmbRMessageUrgency option:selected').val()) == -1 && ($('#cmbRprefTime option:selected').val()) == -1) {
                            //    alert("Please Select Location , Send Request To , Urgency , Preferred Time  ");
                            //    return false;
                            //}
                            //else if (($('#cmbAppointmentFacility3 option:selected').val()) == -1 && ($('#cmbRProvider option:selected').val()) == -1 && ($('#cmbRMessageUrgency option:selected').val()) == -1) {
                            //    alert("Please Select Location , Send Request To , Urgency  ");
                            //    return false;
                            //}
                            //else if (($('#cmbRProvider option:selected').val()) == -1 && ($('#cmbRMessageUrgency option:selected').val()) == -1 && ($('#cmbRprefTime option:selected').val()) == -1) {
                            //    alert("Please Select Send Request To , Urgency , Preferred Time  ");
                            //    return false;
                            //}
                            //else if (($('#cmbAppointmentFacility3 option:selected').val()) == -1 && ($('#cmbRMessageUrgency option:selected').val()) == -1 && ($('#cmbRprefTime option:selected').val()) == -1) {
                            //    alert("Please Select Location , Urgency , Preferred Time  ");
                            //    return false;
                            //}
                            //else if (($('#cmbAppointmentFacility3 option:selected').val()) == -1 && ($('#cmbRProvider option:selected').val()) == -1 && ($('#cmbRprefTime option:selected').val()) == -1) {
                            //    alert("Please Select Location , Send Request To , Preferred Time  ");
                            //    return false;
                            //}
                            //else if (($('#cmbAppointmentFacility3 option:selected').val()) == -1 && ($('#cmbRProvider option:selected').val()) == -1) {
                            //    alert("Please Select Location , Send Request To   ");
                            //    return false;
                            //}
                            //else if (($('#cmbRMessageUrgency option:selected').val()) == -1 && ($('#cmbRprefTime option:selected').val()) == -1) {
                            //    alert("Please Select Urgency , Preferred Time  ");
                            //    return false;
                            //}
                            //else if (($('#cmbRProvider option:selected').val()) == -1 && ($('#cmbRMessageUrgency option:selected').val()) == -1) {
                            //    alert("Please Select Send Request To , Urgency   ");
                            //    return false;
                            //}
                            //else if (($('#cmbAppointmentFacility3 option:selected').val()) == -1 && ($('#cmbRprefTime option:selected').val()) == -1) {
                            //    alert("Please Select Location , Preferred Time  ");
                            //    return false;
                            //}
                            //else if (($('#cmbRProvider option:selected').val()) == -1 && ($('#cmbRprefTime option:selected').val()) == -1) {
                            //    alert("Please Select Send Request To , Preferred Time ");
                            //    return false;
                            //}
                            //else if (($('#cmbAppointmentFacility3 option:selected').val()) == -1 && ($('#cmbRMessageUrgency option:selected').val()) == -1) {
                            //    alert("Please Select Location , Urgency  ");
                            //    return false;
                            //}
                            //else if (($('#cmbAppointmentFacility3 option:selected').val()) == -1) {
                            //    alert("Please Select Location   ");
                            //    return false;
                            //}
                            //else if (($('#cmbRProvider option:selected').val()) == -1) {
                            //    alert("Please Select Send Request To  ");
                            //    return false;
                            //}
                            //else if (($('#cmbRMessageUrgency option:selected').val()) == -1) {
                            //    alert("Please Select Urgency  ");
                            //    return false;
                            //}
                            //else if (($('#cmbRprefTime option:selected').val()) == -1) {
                            //    alert("Please Select PreferredTime ");
                            //    return false;
                            //}

                            //if (($('#txtRpredatefrom').val()) == "" && ($('#txtRpredateto').val()) == "") {
                            //    $('#txtRpredatefrom').val('01/01/1900');
                            //    $('#txtRpredateto').val('01/01/1900');
                            //}
                            //else if (($('#txtRpredatefrom').val()) == "") {
                            //    $('#txtRpredatefrom').val('01/01/1900');
                            //}

                            //else if (($('#txtRpredateto').val()) == "") {
                            //    $('#txtRpredateto').val('01/01/1900');
                            //}

                            var textComment = $.trim($('#txtRComm').val());

                            var txt = textComment.replace(/\n/g, "\\n");
                            var requestData = {
                                FacilityId: $.trim($('#cmbAppointmentFacility3 option:selected').val()),
                                ProviderId_To: $.trim($('#cmbRProvider option:selected').val()),
                                //// ProviderId_Appointment: $.trim($('#cmbAppointmentDoctor1 option:selected').val()),
                                //MessageUrgencyId: $.trim($('#cmbRMessageUrgency option:selected').val()),
                                //PreferredPeriod: PreferredPeriod1,
                                //PreferredWeekDay: PreferredWeekDay1,

                                //AppointmentStart: $.trim($('#txtRpredatefrom').val()),
                                //AppointmentEnd: $.trim($('#txtRpredateto').val()),
                                //PreferredTime: $.trim($('#cmbRprefTime option:selected').text()),
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
                                    alert("Referral Request Sent Successfully");

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
            chkAccess();
            $("#referallcount").append("You have <strong>" + 256 + "</strong> characters remaining");
            $("#reqbtn-form").dialog("open");
        });
    });

    function clearPharmacyDialog() {
        $('#txtPharmID').val('');
        $('#txtPharmacyName').val('');
        $('#txtPharmAddress1').val('');
        $('#txtPharmAddress2').val('');
        $('#txtPharmCity').val('');
        $('#txtPharmState').val('');
        $('#txtPharmZipCode').val('');
        $('#chkPref').prop('checked', false);
        $('#txtPhone').val('');
        $('#txtWebsite').val('');
    }
    function OpenPharmacyDialoge() {
        //$("#PharmAddress").html("");
        $("#Phone").val(""); clearPharmacyDialog();
        $("#dialog-pharmacy").dialog("open");
    }
    $(function () {

        $("#dialog-pharmacy").dialog
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
                    //   if (editpharmacyflag != 1) { bValid = false; }

                    //var address=$.trim($('#txtPharmAddress1').val());
                    //var phone=$("#txtPhone").val();
                    //var addphone=address +'|'+phone;
                    if ($("#txtPharmacyName").val() == "") {
                        alert("Please enter the Pharmacy Name");
                        bValid = false;
                    }
                    else if ($("#txtPharmAddress1").val() == "") {
                        alert("Please enter the Address");
                        bValid = false;
                    }
                    else if ($("#txtPharmCity").val() == "") {
                        alert("Please enter the City");
                        bValid = false;
                    }
                    else if ($("#txtPharmZipCode").val() == "") {
                        alert("Please enter the Zip code");
                        bValid = false;

                    }
                 //   alert($('#txtPhone').val());
                    if (bValid) {

                        try {
                            var requestData = {
                                PharmacyCntr: $.trim($('#txtPharmID').val()),
                                PharmacyName: $.trim($('#txtPharmacyName').val()),
                                Address1: $.trim($('#txtPharmAddress1').val()),
                                City: $.trim($('#txtPharmCity').val()),
                                State: $.trim($('#cmbStates option:selected').text()),
                                PostalCode: $.trim($('#txtPharmZipCode').val()),
                                Phone: $.trim($('#txtPhone').val()),
                                Address2: $.trim($('#txtPharmAddress2').val()),
                                Preferred: $('#chkPref').prop('checked'),
                                Flag: 1,

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
                                    $("#cmbPharmacy").html(data);
                                    var sppl = $("#cmbRFPharmacy option:first").val().split('|');
                                    $('#PharmAddress').text(sppl[1]);
                                    $("#Phone").val(sppl[2]);
                                    $("#cmbRFPharmacy").change(function () {
                                    
                                      
                                        var spl = $("#cmbRFPharmacy").val().split('|');
                                        
                                        $('#PharmAddress').text(spl[1]);
                                        $("#Phone").val(spl[2]);


                                    });
                                    // $("#tbl-Pharmacies").fixedHeaderTable();
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
                                },
                                complete: function (data) {
                                    // if completed
                                    HideLoader();

                                },
                                async: true,
                                processData: false
                            });

                            //$.ajax({
                            //    type: 'POST',
                            //    url: 'Medication-Data',
                            //    // data: JSON.stringify(requestData),
                            //    dataType: 'json',
                            //    contentType: 'application/json; charset=utf-8',
                            //    success: function (data) {
                            //        // if success
                            //        // alert("Success : " + data);
                            //        $("#divPharmacy").html(data);
                            //        $("#DivPharmacy1").html(data);
                            //        $('#DivPharmacy1 #txtPharmacy').attr('id', 'txtPharmacy1');
                            //        $("#tbl-Pharmacies").fixedHeaderTable();
                            //    },
                            //    error: function (xhr, ajaxOptions, thrownError) {
                            //        //if error

                            //        alert('Error : ' + xhr.message);
                            //    },
                            //    complete: function (data) {
                            //        // if completed
                            //        HideLoader();

                            //    },
                            //    async: true,
                            //    processData: false
                            //});
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
                    $("#dialog-pharmacy")
         .next(".ui-dialog-buttonpane")
         .find("button:contains('Save')")
         .button("option", "disabled", false);
                    $(this).dialog("close");
                }
            },
            close: function () {
                $("#dialog-pharmacy")
    .next(".ui-dialog-buttonpane")
    .find("button:contains('Save')")
    .button("option", "disabled", false);
            }
        });



      //  $("#add-pharmacy")
      //  .button()
      //  .click(function () {

      //      $("#dialog-pharmacy")
      //.next(".ui-dialog-buttonpane")
      //.find("button:contains('Save')")
      //.button("option", "disabled", false);
      //      $('.HideEditDelete').css('display', 'none');
      //      $("#dialog-pharmacy").dialog("open");
      //  });

    });

    function LoadFacityProvider() {
        //ShowLoaderForAppointment();
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

                },
                complete: function (data) {
                    // if completed
                    //HideLoaderForAppointment();

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
            //ShowLoaderForComposeMessage();
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

                },
                complete: function (data) {
                    // if completed
                    //HideLoaderForComposeMessage();

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
    $(function () {

        $('#browse').click(function () { $('#my-simple-upload').click(); });


    });

    var requestImageData;
    var jqXHRData;
    $(document).ready(function () {

        initAttachFileUpload();




    });
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

    function LoadFacilityProviderRequestRefferal() {
        //ShowLoaderForRequestReferral();
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

                },
                complete: function (data) {
                    // if completed
                    //HideLoaderForRequestReferral();

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

    function HidevaluecmbMessageType() {
        $("#cmbMessageType option[value='1']").remove();
        $("#cmbMessageType option[value='2']").remove();
        $("#cmbMessageType option[value='5']").remove();


    }
    function getCookie(name) {
        var dc = document.cookie;
        var prefix = name + "=";
        var begin = dc.indexOf("; " + prefix);
        if (begin == -1) {
            begin = dc.indexOf(prefix);
            if (begin != 0) return null;
        }
        else {
            begin += 2;
            var end = document.cookie.indexOf(";", begin);
            if (end == -1) {
                end = dc.length;
            }
        }
        return unescape(dc.substring(begin + prefix.length, end));
    }
    function chkLogin(url) {
        var locAcc =  $('#AccessId').val();
        var extAcc =  localStorage.getItem("AccessId");
        if (locAcc != extAcc) {
            alert('Not logged in. Please log in to continue');
            window.location = '/Login?access=true';
        }
        else {
            var myCookie = getCookie(".Patient");

            if (myCookie == null) {
                alert('Not logged in. Please log in to continue');
                window.location = '/Login';
            }
            else {
                window.location = url;
            }
        }
    }
    function chkAccess() {
        var locAcc = $('#AccessId').val();
        var extAcc = localStorage.getItem("AccessId");
        if (locAcc != extAcc) {
            alert('Not logged in. Please log in to continue');
            window.location = '/Login?access=true';
        }
    }


    function ViewGDocumentAttachment(generalDocumentId) {
        chkAccess();
        //alert(generalDocumentId)
        var requestURL = "";

        //alert(generalDocumentId);
        var docType = generalDocumentId.split('|')[1];
        //alert(docType);

        requestURL = "Home/PatientClinicalDocumentAttachment/?patientDocumentId=" + generalDocumentId.split('|')[0] + "&VisitId=" + generalDocumentId.split('|')[2] + "&FacilityId=" + generalDocumentId.split('|')[3] + "&Menu=" + generalDocumentId.split('|')[4];

        var isOfficeDocRequest = false;

        switch (docType.toLowerCase()) {
            case "doc":
                isOfficeDocRequest = true;
                break;

            case "docx":
                isOfficeDocRequest = true;
                break;

            case "xls":
                isOfficeDocRequest = true;
                break;

            case "xlsx":
                isOfficeDocRequest = true;
                break;
            case "tif":
                isOfficeDocRequest = true;
                break;
            default:
                isOfficeDocRequest = false;
                break;
        }

        if (isOfficeDocRequest) {


            try {


                ShowLoaderReportViewer();

                $('#iframeViewAttachment').attr('src', requestURL);
                $('#iframeViewAttachment').load();

            } catch (err) {

                if (err && err !== "") {
                    alert(err.message);
                    HideLoaderReportViewer();
                }
            }



        }
        else {

            $('#clinical-print').remove();
            var $dialog = $('<div id="clinical-print" style="width:1000px;"></div>')
                           .html('<div id="page_loader2" style="display: block;"><div id="apDiv1" align="center"><table width="200" border="0" cellspacing="0" cellpadding="0"><tr><td height="70px;">&nbsp;</td></tr><tr><td align="center"><img src="Content/img/image_559926.gif" width="128" height="64" /></td></tr><tr><td style="text-align: center;" height="70px;">Please wait ...</td></tr></table></div></div><iframe id="iframeViewDoctorAttachment" onload="processingComplete();" style="border: 0px; " src="' + requestURL + '" width="100%" height="100%"></iframe>')
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
                               width: 1050,
                               title: "View General Document Attachment"
                           });
            $dialog.dialog('open');

        }
    }

    function ControllMessageCompose()
    {
        $("#browse").css("display", "block");
        $("#browse").val("Browse..");
        $("#cmbAppointmentFacility").attr('disabled', false);
        $("#cmbAppointmentDoctor ").attr('disabled', false);
        $("#cmbMessageType ").attr('disabled', false);
        $("#cmbMessageUrgency").attr('disabled', false);
        $("#BrowseName").val("");
        $("#btncancelupload").hide();
        $("#txtMessageWrite").val("");
        $("#cmbAppointmentFacility").val("-1");
        $("#cmbAppointmentDoctor").val("-1");
        $("#cmbMessageType").val("-1");
        $("#cmbMessageUrgency").val("false");
        $("#counter").text("");


    }
    function ControllAppointment()
    {

        $("#cmbAppointmentFacility1").val("-1");
        $("#cmbAppointmentDoctor1").val("-1");
        $("#cmbMessageUrgency1").val("false");
        $(':input').prop('checked', false);
        $("#txtPrefDateFrom").val("");
        $("#txtPrefDateTo").val("");
        $("#cmbPreftime").val("");
        $("#txtReason").val("");
        $("#txtComment").val("");
        $("#appcount").text("");
    }

    function ControllRefill()
    {

        $("#cmbAppointmentFacilityRefill").val("-1");
        $("#cmbDoctor").val("-1");
        $("#cmbMedication").val("-1");
        $("#txtCountRefills").val("");
        $("#txtComments").val("");
        $("#cmbRFPharmacy").val($("#cmbRFPharmacy option:first").val());
       // $("#refillcount").text("");
    }
    function ControllReferall()
    {
        $("#cmbAppointmentFacility3").val("-1");
        $("#cmbRProvider").val("-1");
        $("#txtReqRes").val("");
        $("#txtRComm").val("");
        $("#referallcount").text("");
    }



    $(function () {
        $("#date").datepicker({
            inline: true,
            dateFormat: 'mm/dd/yy',
            changeMonth: true,
            changeYear: true,
            yearRange: "1900:+0"


        });
        $("#Datadate").datepicker({
            inline: true,
            dateFormat: 'mm/dd/yy',
            changeMonth: true,
            changeYear: true,
            yearRange: "1900:+0"

        });

        $("#dtObservationDateData").datepicker({
            inline: true,
            dateFormat: 'mm/dd/yy',
            changeMonth: true,
            changeYear: true,
            yearRange: "1900:+0"
        });
        $("#show-date-vitData").datepicker({
            inline: true,
            dateFormat: 'mm/dd/yy',
            changeMonth: true,
            changeYear: true,
            yearRange: "1900:+0"

        });
        $("#txttobquitdate").datepicker({
            inline: true,
            dateFormat: 'mm/dd/yy',
            changeMonth: true,
            changeYear: true,
            yearRange: "1900:+0"
        });
        $("#datepickerData").datepicker({
            inline: true,
            dateFormat: 'mm/dd/yy',
            changeMonth: true,
            changeYear: true,
            yearRange: "1900:+0"
        });
        $("#dtStartDateData").datepicker({
            inline: true,
            dateFormat: 'mm/dd/yy',
            changeMonth: true,
            changeYear: true,
            yearRange: "1900:+0"
        });
        $("#txtImmunizationDateData").datepicker({
            inline: true,
            dateFormat: 'mm/dd/yy',
            changeMonth: true,
            changeYear: true,
            yearRange: "1900:+0"

        });
        $("#show-date-imm-Data").datepicker({
            inline: true,
            dateFormat: 'mm/dd/yy',
            changeMonth: true,
            changeYear: true,
            yearRange: "1900:+0"

        });
        $("#txtImmunizationEXDateData").datepicker({
            inline: true,
            dateFormat: 'mm/dd/yy',
            changeMonth: true,
            changeYear: true,
            yearRange: "1900:+0"

        });
    });
    $(function () {
        $("#Planneddt").datepicker({
            inline: true,
            dateFormat: 'mm/dd/yy',
            changeMonth: true,
            changeYear: true,
            yearRange: "1900:+0"
        });
    });
    $(function () {
        $("#date").datepicker({
            inline: true,
            dateFormat: 'mm/dd/yy',
            changeMonth: true,
            changeYear: true,
            yearRange: "1900:+0"
        });
    });
    $(function () {
        $("#Planneddt").datepicker({
            inline: true,
            dateFormat: 'mm/dd/yy',
            changeMonth: true,
            changeYear: true,
            yearRange: "1900:+0"
        });
    });
    $(function () {
        $("#txtPlanneddt").datepicker({
            inline: true,
            dateFormat: 'mm/dd/yy',
            changeMonth: true,
            changeYear: true,
            yearRange: "1900:+0"
        });
    });
    $(function () {
        $("#PlanneddtData").datepicker({
            inline: true,
            dateFormat: 'mm/dd/yy',
            changeMonth: true,
            changeYear: true,
            yearRange: "1900:+0"
        });
        $("#txtPlanneddtData").datepicker({
            inline: true,
            dateFormat: 'mm/dd/yy',
            changeMonth: true,
            changeYear: true,
            yearRange: "1900:+0"
        });
    });
    $(function () {
        $("#txtalcoholusedate").datepicker({
            // showOn: "button",
            inline: true,
            dateFormat: 'mm/dd/yy',
            changeMonth: true,
            changeYear: true,
            yearRange: "1900:+0"
            //  buttonImage:'Content/img/calender.png',
            //  buttonImageOnly: true,
            //  inline:true
        });
    });
    $(function () {
        $("#txtPrefDateTo").datepicker({
            inline: true,
            dateFormat: 'mm/dd/yy',
            changeMonth: true,
            changeYear: true,
            yearRange: "1900:+0"
        });
    });
    $(function () {
        $("#tabs").tabs();
    });

    function LoadFacityProviderRefil() {
        // ShowLoaderForRefil();
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
                    HideLoaderForRefil();


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

