
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
        alert();
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
                duration: 1000
            },
            hide: {
                effect: "blind",
                duration: 1000
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
            inline: true
        });

        $("#dtStartDate").datepicker({
            inline: true
        });

        $("#dtStartDatepst").datepicker({
            inline: true
        });

        $("#dtObservationDate").datepicker({
            inline: true,
            changeMonth: true,
            changeYear: true

                
        });

        $("#txtImmunizationEXDate").datepicker({
            inline: true,
            changeMonth: true,
            changeYear: true


        });

           
        $('#cmbImmunizationTime').timepicker({
            timeFormat: "hh:mm tt"

        });

           

        //$("#txtDateReported").datepicker({
        //    inline: true
        //});

        //$("#dtDateOccurance").datepicker({
        //    inline: true
        //});
            
        $("#datepicker1").datepicker({
            inline: true
        });

        $("#txtOrderDate").datepicker({
            inline: true
        });

        $("#txtCollectionDate").datepicker({
            inline: true
        });

        $("#txtReportDate").datepicker({
            inline: true
        });

        $("#txtReviewDate").datepicker({
            inline: true
        });

        $("#dtVisitDate").datepicker({
            inline: true
        });

        $("#txtPrefDateFrom").datepicker({
            inline: true
        });

        $("#txtPrefDateTo").datepicker({
            inline: true
        });
            
        $("#txtRpredatefrom").datepicker({
            inline: true
        });

        $("#txtRpredateto").datepicker({
            inline: true
        });
        //$("#txtProblemsDate").datepicker({
        //    inline: true
        //});
            
        $("#txtImmunizationDate").datepicker({
                
            inline: true,
            changeMonth: true,
            changeYear: true
        });
            
        $("#show-date-imm").click(function () {
            $("#txtImmunizationDate").datepicker("show");
        });

            
        $("#show-date-imm-exp").click(function () {
            $("#txtImmunizationEXDate").datepicker("show");
        });

        $("#show-date-vit").click(function () {
            $("#dtObservationDate").datepicker("show");
        });

        $("#txtdob").datepicker({
            inline: true,
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
                duration: 1000
            },
            hide: {
                effect: "blind",
                duration: 1000
            },


            width: 550,
            modal: true,
            buttons: {
                "Upload": function () {

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
                $('#AppointmentMessages').html("Appointment(<b style='font-size:14px;'>" + data.AppointmentMessages + "</b>)");
                $('#MedicationMessages').html("Medication Refill(<b style='font-size:14px;'>" + data.MedicationMessages + "</b>)");
                $('#ReferralMessages').html("Referral Requests(<b style='font-size:14px;'>" + data.ReferralMessages + "</b>)");
            }
        });
        // HideLoader();
    });
      