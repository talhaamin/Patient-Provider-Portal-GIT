 $(document).ready(function () {
          
            

            $("#tbl-Appointments").fixedHeaderTable();
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
            $("#tbl-PastMedications").fixedHeaderTable();
            $("#tbl-poc").fixedHeaderTable();
            $("#tbl-Problems").fixedHeaderTable();
            $("#tbl-VitalSigns").fixedHeaderTable();
            $("#tbl-Procedure").fixedHeaderTable();
            $("#tbl-poc").fixedHeaderTable();
            $("#tbl-ClinicalInstruction").fixedHeaderTable();
            
          
            
        });
        $(function () {
            $('#txtHeight').keyup(function () {
               
                    updateTotal();
                
            });

            $('#txtHeightinch').keyup(function () {
                
                    updateTotal();
                
            });

            $('#txtWeightLb').keyup(function () {
                updateTotal();
            });

            var updateTotal = function () {
                var heightSpec = $('#txtHeight').val();
                var heightVal = heightSpec.split("_");
                var height = heightVal[0] * 12;
                var height1Spec = $('#txtHeightinch').val();
                var height1Val = height1Spec.split("_");
                var height1 = height1Val[0];
                var heightInch = parseInt(height) + parseInt(height1);
                var weightSpec = $('#txtWeightLb').val();
                var weightVal = weightSpec.split("_");
                var weight = weightVal[0];
                var result = Math.round(weight / (heightInch * heightInch) * 703);
                //if (isNaN($('#txtHeight').val()) && isNaN($("#txtHeightinch").val()) &&  isNaN($('#txtWeightLb').val()))
                //{
                    
                    
                
                if (isNaN(result))
                { $('#result').text(''); }
                else {
                    $('#result').text(result.toFixed(1));
                }
                //}
                //else
                //{
                //    $('#result').text('');
                //}
                   
                
            };

            var output_total = $('#result');

            // var total = firstInput + firstInput1;

            output_total.text();

        });

        $(function () {
            $('#txtoc_Month').change(function () {
                //var value = $('#txtoc_Month option:selected').val()
                // + $('#txtoc_Day option:selected').val() + $('#txtoc_Year').val();
                //$('#dtDateOccurance').val(value);
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
                //var value = $('#txtoc_Month option:selected').val()
                // + $('#txtoc_Day option:selected').val() + $('#txtoc_Year').val();
                //$('#dtDateOccurance').val(value);
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
                //var value = $('#txtoc_Month option:selected').val()
                // + $('#txtoc_Day option:selected').val() + $('#txtoc_Year').val();
                //$('#dtDateOccurance').val(value);
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

            //$('#txtProblemsDate_Month').change(function () {
            //    var value = $('#txtProblemsDate_Year').val() +$('#txtProblemsDate_Month option:selected').val()
            //     + $('#txtProblemsDate_Day option:selected').val();
            //    $('#txtProblemsDate').val(value);

            //});

            //$('#txtProblemsDate_Day').change(function () {
            //    var value = $('#txtProblemsDate_Year').val() + $('#txtProblemsDate_Month option:selected').val()
            //      + $('#txtProblemsDate_Day option:selected').val();
            //    $('#txtProblemsDate').val(value);

            //});
            //$('#txtProblemsDate_Year').change(function () {
            //    var value = $('#txtProblemsDate_Year').val() + $('#txtProblemsDate_Month option:selected').val()
            //     + $('#txtProblemsDate_Day option:selected').val();
            //    $('#txtProblemsDate').val(value);
            //});

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
                if ($("#txtHeightinch").val().length > 0 && $("#txtWeightLb").val().length > 0) {

                    updateTotal();
                }
            });

            $('#txtHeightinch').change(function () {
                if ($("#txtHeight").val().length > 0 && $("#txtWeightLb").val().length > 0) {

                    updateTotal();
                }
            });

            $('#txtWeightLb').change(function () {
                if ($("#txtHeight").val().length > 0 && $("#txtHeightinch").val().length > 0) {

                    updateTotal();
                }
            });
            function calculateBMI() {

                var height = $('#cmbHeightFt').val() * 12;
                var height1 = $("#cmbHeightIn").val();
                var heightInch = parseInt(height) + parseInt(height1);
                var weight = $('#txtWeight').val();
                var BMI = weight / (heightInch * heightInch) * 703;
                $('#txtBMI').val(BMI.toFixed(1));
               // document.getElementById('txtBMI').value = 'text to be displayed';
               
            }
            $('#cmbHeightFt').change(function () {

                calculateBMI();

            });

            $('#cmbHeightIn').change(function () {


                calculateBMI();

            });

            $('#txtWeight').change(function () {
                if ($("#cmbHeightFt").val().length > 0 && $("#cmbHeightIn").val().length > 0) {

                    calculateBMI();
                }
            });



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


            var navFlag = $('#querystringhd').val();


            $("#tabs").tabs({
                active: navFlag
            });
        });

        $(document).ready(function () {
            $(window).scrollTop(0);
        });

        $(document).load().scrollTop(0);

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



        $(document).ready(function () {
            $('#PastHistOcc').find('.fht-cell').css("width","84px");
            $('#PastHistDiagnose').find('.fht-cell').css("width","138px");
            $('#PastHistNote').find('.fht-cell').css("width","204px");
            $('#txtBMI').attr('disabled', true);
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
        $(function () {
           
            $("#createaddProcedures").click(function () {
                
                $("#addProcedures-form").dialog('option', 'title', 'Add Procedure');
                $("#addProcedures-form").dialog("open");
            });
            $("#createaddProcedures-tab").click(function(){
                
                $("#proc-remove-class").removeClass("ui-state-highlight");
                $( "#proc-remove-class" ).empty();
                $("#date").removeClass("ui-state-error");
                $("#addProcedures-form").dialog('option', 'title', 'Add Procedure');
                $("#addProcedures-form").dialog("open");
            
            });


            tips = $(".validateTips");
            var DateProc = $("#date");
            

            allFields = $([]).add(DateProc),
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

                if (o.val() == "") {
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


                width: 600,
                modal: true,

                buttons: {
                    "Save": function () {
                        var bValid = true;
                      
                        allFields.removeClass("ui-state-error");
                        
                        bValid = bValid && checkLength(DateProc, "Please Select date");

                        var date=$("#date").val();
                   
                        var mySplitResult = date.split("/");
                        
                        var Servicedate=mySplitResult[2]+mySplitResult[0]+mySplitResult[1];
                       
                       // var d1 = new Date(Number(parts[2]), Number(parts[0]), Number(parts[1]) - 1);
                       
                      
                        //alert(date);
                        // allFields.removeClass("ui-state-error");
                        // bValid = bValid && checkLength(Status, "Please Select Status");
                        var ProcedureTab=$("#procedureflag").val();
                      
                        if(ProcedureTab=="ProcedureTab")
                        {
                           
                        }
                        else
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
                                        PatProcedureCntr:$.trim($('#txtprocedureid').val()),
                                        ServiceDate: Servicedate,
                                        Description:$.trim($('#txtProcedureDescription').val()),
                                        Note: $.trim($('#txtProcedureNote').val()),
                                        flag:$.trim($('#procedureflag').val())
                                   
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
           
            $("#createaddPOC").click(function () {
                
                $("#addPOC-form").dialog("open");
            });
            $("#createaddPOC-tab").click(function(){
                $("#poc-remove-class").removeClass("ui-state-highlight");
                $( "#poc-remove-class" ).empty();
                $("#Planneddt").removeClass("ui-state-error");
                $("#addPOC-form").dialog('option', 'title', 'Add Plan Of Care');
                $("#addPOC-form").dialog("open");
            
            });


            tips = $(".validateTips");
            var DatePoc = $("#Planneddt");
            

            allFields = $([]).add(DatePoc),
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

                if (o.val() == "") {
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


                width: 550,
                modal: true,
                buttons: {
                    "Save": function () {
                        var bValid = true;
                     
                        allFields.removeClass("ui-state-error");
                        
                        bValid = bValid && checkLength(DatePoc, "Please Select Date First");
                     

                        var POCTab=$("#POCflag").val();
                        if(POCTab =="POCTab")
                        {
                           
                        }
                        else{
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
                                            PlanCntr:$.trim($("#PlanCntr").val()),
                                            InstructionTypeId: $.trim($( "#txttype option:selected" ).val()),
                                            Instruction:$.trim($('#Instructions').val()),
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
                                                $("#PlanOfCare-portlet-tab").html(data.html1);
                                        
                                                $("#tab-PlanOfCare").fixedHeaderTable();
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
           
            $("#createaddClinicalInstructions").click(function () {
                $("#type option:selected").val(4);
                //$("#type").attr("disabled",true);
                $("#clinical-remove-class").removeClass("ui-state-highlight");
                $( "#clinical-remove-class" ).empty();
                $("#txtPlanneddt").removeClass("ui-state-error");
                $("#addClinicalInstructions-form").dialog('option', 'title', 'Add Clinical Instructions');
                $("#addClinicalInstructions-form").dialog("open");
            });
            $("#createaddClinicalInstructionstab").click(function(){
                $("#type option:selected").val(4);
                //$("#type").attr("disabled",true);
                $("#clinical-remove-class").removeClass("ui-state-highlight");
                $( "#clinical-remove-class" ).empty();
                $("#txtPlanneddt").removeClass("ui-state-error");
                $("#addClinicalInstructions-form").dialog('option', 'title', 'Add Clinical Instructions');
                $("#addClinicalInstructions-form").dialog("open");
            
            });


            tips = $(".validateTips");
            var DateClinical = $("#txtPlanneddt");
            

            allFields = $([]).add(DateClinical),
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

                if (o.val() == "") {
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
                     
                         allFields.removeClass("ui-state-error");
                         bValid = bValid && checkLength(DateClinical, "Please Select Date First");
                        var clinicalInstructionflag=$("#clinicalInstructionflag").val();
                        if(clinicalInstructionflag =="clinicalInstructionflag")
                        {
                        
                        }
                        else
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
                                        PlanCntr:$.trim($("#clinicalInstruction").val()),
                                        InstructionTypeId: $.trim($( "#type option:selected" ).val()),
                                        Instruction:$.trim($('#txtInstruction').val()),
                                        AppointmentDateTime: $.trim($('#txtPlanneddt').val()),
                                        Goal: $.trim($('#txtGoals').val()),
                                        Note: $.trim($('#txtComment').val()),
                                  
                                   
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
                                            $("#tab-ClinicalInstruction").fixedHeaderTable();
                                            //$("#tbl-Allergies").fixedHeaderTable();
                                            $("#ClinicalInstructions-portlet-tab").html(data.html1);
                                        
                                            $("#tab-ClinicalInstruction").fixedHeaderTable();
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
                        $('#txtComment').attr("disabled", false);
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
                    $('#txtComment').attr("disabled", false);
                    $('#txtPlanneddt').attr("disabled", false);
                    $('#txtInstruction').attr("disabled", false);
                    $('#txtGoals').attr("disabled", false);
                    $("#addClinicalInstructions-form").dialog("close");
                }
            });
        });
        function clearclinicalinsturctionfield()
        {
           // $("#type option:selected" ).val();
            $('#txtInstruction').val(" ");
            $('#txtPlanneddt').val(" ");
            $('#txtGoals').val(" ");
            $('#txtComment').val(" ");
        }
        function clearProcedure()
        {
        
            $('#txtprocedureid').val();
           
            $('#txtProcedureDescription').val();
            $('#txtProcedureNote').val();
        }
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
                            var FacilityHome = document.getElementById('cmbFacilityHome');
                            var facilityoptions = FacilityHome.innerHTML;
                            var FacilityGlobal = document.getElementById('cmbFacilityGlobal');
                            FacilityGlobal.innerHTML = facilityoptions;
                            $('#cmbFacilityGlobal').val($('#cmbFacilityHome').val());
                            var VisitHome = document.getElementById('cmbVisitsHome');
                            var visitoptions = VisitHome.innerHTML;
                            var VisitGlobal = document.getElementById('cmbVisitsGlobal');
                            VisitGlobal.innerHTML = visitoptions;
                            $('#cmbVisitsGlobal').val($('#cmbVisitsHome').val());

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
                        //$("#homeComboBoxPortlet").html(data);
                        //id = $('#cmbVisitsHome option:selected').val();
                        var FacilityHome = document.getElementById('cmbFacilityHome');
                        var facilityoptions = FacilityHome.innerHTML;
                        var FacilityGlobal = document.getElementById('cmbFacilityGlobal');
                        FacilityGlobal.innerHTML = facilityoptions;
                        $('#cmbFacilityGlobal').val($('#cmbFacilityHome').val());
                        var VisitHome = document.getElementById('cmbVisitsHome');
                        var visitoptions = VisitHome.innerHTML;
                        var VisitGlobal = document.getElementById('cmbVisitsGlobal');
                        VisitGlobal.innerHTML = visitoptions;
                        $('#cmbVisitsGlobal').val($('#cmbVisitsHome').val());
                        $("#tbl-Appointments").fixedHeaderTable();
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
                        $("#tbl-PastMedications").fixedHeaderTable();
                        $("#tbl-poc").fixedHeaderTable();
                        $("#tbl-Problems").fixedHeaderTable();
                        $("#tbl-VitalSigns").fixedHeaderTable();
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

        function labTests(id, flag) {
            var temp;
            if (flag == 'visit')
                sec_arg = $('#cmbFacilityLab option:selected').val();
            else if (flag == 'location') {
                sec_arg = id; temp = id;
                id = $('#cmbVisitsLab option:selected').val();
            }
            var facilityOptions = document.getElementById('cmbFacilityLab').innerHTML;
            var visitOptions = document.getElementById('cmbVisitsLab').innerHTML;
            var facilitySelected = $('#cmbFacilityLab option:selected').val();
            var visitSelected = $('#cmbVisitsLab option:selected').val();

            if (flag == 'location') {

                //filling dropdown start 
                try {
                    ShowLoader();
                    //var IsPrimaryData =
                    var requestData = {

                        FacilityId: sec_arg,
                        ExtensionToggleFunct: "toggle_combobox_lab();",
                        ExtensionFilterFunct: "labTests(this.value,'visit');",
                        ExtensionIdName: "cmbVisitsLab",
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

                            $("#labComboBoxPortlet").html(data);
                            id = $('#cmbVisitsHome option:selected').val();

                            var FacilityLab = document.getElementById('cmbFacilityLab');
                            var facilityoptions = FacilityLab.innerHTML;
                            var FacilityGlobal = document.getElementById('cmbFacilityGlobal');
                            FacilityGlobal.innerHTML = facilityoptions;
                            $('#cmbFacilityGlobal').val($('#cmbFacilityLab').val());

                            var VisitLab = document.getElementById('cmbVisitsLab');
                            var visitoptions = VisitLab.innerHTML;
                            var VisitGlobal = document.getElementById('cmbVisitsGlobal');
                            VisitGlobal.innerHTML = visitoptions;
                            $('#cmbVisitsGlobal').val($('#cmbVisitsLab').val());
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



            if (flag == 'visit')
                sec_arg = $('#cmbFacilityLab option:selected').val();
            else if (flag == 'location') {
                sec_arg = temp;
                id = $('#cmbVisitsLab option:selected').val();
            }
            visitOptions = document.getElementById('cmbVisitsLab').innerHTML;
            visitSelected = $('#cmbVisitsLab option:selected').val();
           // alert(visitOptions);
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
                    url: 'clinical-summary-labs-filter',
                    data: JSON.stringify(requestData),
                    dataType: 'json',
                    contentType: 'application/json; charset=utf-8',
                    success: function (data) {
                        // if success

                        $("#lab-portlet-tab").html(data);
                        $("#tab-Labtest").fixedHeaderTable();
                    },
                    error: function (xhr, ajaxOptions, thrownError) {
                        //if error

                        alert('Error : ' + xhr.message);
                        HideLoader();
                    },
                    complete: function (data) {
                        // if completed
                        HideLoader();
                        var FacilityLab = document.getElementById('cmbFacilityLab');
                        var facilityoptions = FacilityLab.innerHTML;
                        var FacilityGlobal = document.getElementById('cmbFacilityGlobal');
                        FacilityGlobal.innerHTML = facilityoptions;
                        $('#cmbFacilityGlobal').val($('#cmbFacilityLab').val());

                        var VisitLab = document.getElementById('cmbVisitsLab');
                        var visitoptions = VisitLab.innerHTML;
                        var VisitGlobal = document.getElementById('cmbVisitsGlobal');
                        VisitGlobal.innerHTML = visitoptions;
                        $('#cmbVisitsGlobal').val($('#cmbVisitsLab').val());
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
        

        function histories(id, flag) {
            var temp;
            if (flag == 'visit')
                sec_arg = $('#cmbFacilityHistory option:selected').val();
            else if (flag == 'location') {
                sec_arg = id; temp = id;
                id = $('#cmbVisitsHistory option:selected').val();
            }
            var facilityOptions = document.getElementById('cmbFacilityHistory').innerHTML;
            var visitOptions = document.getElementById('cmbVisitsHistory').innerHTML;
            var facilitySelected = $('#cmbFacilityHistory option:selected').val();
            var visitSelected = $('#cmbVisitsHistory option:selected').val();
            if (flag == 'location') {

                //filling dropdown start 
                try {
                    ShowLoader();
                    //var IsPrimaryData =
                    var requestData = {

                        FacilityId: sec_arg,
                        ExtensionToggleFunct: "toggle_combobox_history();",
                        ExtensionFilterFunct: "histories(this.value,'visit');",
                        ExtensionIdName: "cmbVisitsHistory",
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

                            $("#historyComboBoxPortlet").html(data);
                            id = $('#cmbVisitsHome option:selected').val();
                            var FacilityHistory = document.getElementById('cmbFacilityHistory');
                            var facilityoptions = FacilityHistory.innerHTML;
                            var FacilityGlobal = document.getElementById('cmbFacilityGlobal');
                            FacilityGlobal.innerHTML = facilityoptions;
                            $('#cmbFacilityGlobal').val($('#cmbFacilityHistory').val());

                            var VisitsHistory = document.getElementById('cmbVisitsHistory');
                            var visitoptions = VisitsHistory.innerHTML;
                            var VisitGlobal = document.getElementById('cmbVisitsGlobal');
                            VisitGlobal.innerHTML = visitoptions;
                            $('#cmbVisitsGlobal').val($('#cmbVisitsHistory').val());
                            

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
            if (flag == 'visit')
                sec_arg = $('#cmbFacilityHistory option:selected').val();
            else if (flag == 'location') {
                sec_arg = temp;
                id = $('#cmbVisitsHistory option:selected').val();
            }
            visitOptions = document.getElementById('cmbVisitsHistory').innerHTML;
            visitSelected = $('#cmbVisitsHistory option:selected').val();
           // alert(visitOptions);
           // alert(visitSelected);
            //starting social
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
                    url: 'clinical-summary-social-filter',
                    data: JSON.stringify(requestData),
                    dataType: 'json',
                    contentType: 'application/json; charset=utf-8',
                    success: function (data) {
                        // if success

                        $("#social-portlet-tab").html(data);

                        toggle_combobox_history();
                        var FacilityHistory = document.getElementById('cmbFacilityHistory');
                        var facilityoptions = FacilityHistory.innerHTML;
                        var FacilityGlobal = document.getElementById('cmbFacilityGlobal');
                        FacilityGlobal.innerHTML = facilityoptions;
                        $('#cmbFacilityGlobal').val($('#cmbFacilityHistory').val());

                        var VisitsHistory = document.getElementById('cmbVisitsHistory');
                        var visitoptions = VisitsHistory.innerHTML;
                        var VisitGlobal = document.getElementById('cmbVisitsGlobal');
                        VisitGlobal.innerHTML = visitoptions;
                        $('#cmbVisitsGlobal').val($('#cmbVisitsHistory').val());
                        $("#tab-SocialHistory").fixedHeaderTable();
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
            //ending social


            //starting family
            try {
                ShowLoader();

                var requestData = {

                    FacilityId: sec_arg,
                    VisitId: id

                };


                $.ajax({
                    type: 'POST',
                    url: 'clinical-summary-family-filter',
                    data: JSON.stringify(requestData),
                    dataType: 'json',
                    contentType: 'application/json; charset=utf-8',
                    success: function (data) {
                        // if success

                        $("#family-portlet-tab").html(data);
                        toggle_combobox_history();
                        var FacilityHistory = document.getElementById('cmbFacilityHistory');
                        var facilityoptions = FacilityHistory.innerHTML;
                        var FacilityGlobal = document.getElementById('cmbFacilityGlobal');
                        FacilityGlobal.innerHTML = facilityoptions;
                        $('#cmbFacilityGlobal').val($('#cmbFacilityHistory').val());

                        var VisitsHistory = document.getElementById('cmbVisitsHistory');
                        var visitoptions = VisitsHistory.innerHTML;
                        var VisitGlobal = document.getElementById('cmbVisitsGlobal');
                        VisitGlobal.innerHTML = visitoptions;
                        $('#cmbVisitsGlobal').val($('#cmbVisitsHistory').val());
                        $("#tab-FamilyHistory").fixedHeaderTable();
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
            //ending family



            //starting past
            try {
                ShowLoader();
                //var IsPrimaryData =
                var requestData = {

                    FacilityId: sec_arg,
                    VisitId: id

                };


                $.ajax({
                    type: 'POST',
                    url: 'clinical-summary-past-filter',
                    data: JSON.stringify(requestData),
                    dataType: 'json',
                    contentType: 'application/json; charset=utf-8',
                    success: function (data) {
                        // if success

                        $("#pasthistory-portlet-tab").html(data);
                        toggle_combobox_history();
                        var FacilityHistory = document.getElementById('cmbFacilityHistory');
                        var facilityoptions = FacilityHistory.innerHTML;
                        var FacilityGlobal = document.getElementById('cmbFacilityGlobal');
                        FacilityGlobal.innerHTML = facilityoptions;
                        $('#cmbFacilityGlobal').val($('#cmbFacilityHistory').val());

                        var VisitsHistory = document.getElementById('cmbVisitsHistory');
                        var visitoptions = VisitsHistory.innerHTML;
                        var VisitGlobal = document.getElementById('cmbVisitsGlobal');
                        VisitGlobal.innerHTML = visitoptions;
                        $('#cmbVisitsGlobal').val($('#cmbVisitsHistory').val());
                        $("#tab-MedicalHistory").fixedHeaderTable();
                        HideLoader();
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
                    async: false,
                    processData: false
                });
            } catch (err) {

                if (err && err !== "") {
                    alert(err.message);
                }
            }
            //ending past


        }

        function allergies(id, flag) {
            var temp;
            if (flag == 'visit')
                sec_arg = $('#cmbFacilityAllergy option:selected').val();
            else if (flag == 'location') {
                sec_arg = id; temp = id;
                id = $('#cmbVisitsAllergy option:selected').val();
            }
            var facilityOptions = document.getElementById('cmbFacilityAllergy').innerHTML;
            var visitOptions = document.getElementById('cmbVisitsAllergy').innerHTML;
            var facilitySelected = $('#cmbFacilityAllergy option:selected').val();
            var visitSelected = $('#cmbVisitsAllergy option:selected').val();
            if (flag == 'location') {

                //filling dropdown start 
                try {
                    ShowLoader();
                    //var IsPrimaryData =
                    var requestData = {

                        FacilityId: sec_arg,
                        ExtensionToggleFunct: "toggle_combobox_allergy();",
                        ExtensionFilterFunct: "allergies(this.value,'visit'); ",
                        ExtensionIdName: "cmbVisitsAllergy",
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

                            $("#allergyComboBoxPortlet").html(data);
                            id = $('#cmbVisitsHome option:selected').val();
                            var cmbFacilityAllergy = document.getElementById('cmbFacilityAllergy');
                            var facilityoptions = cmbFacilityAllergy.innerHTML;
                            var FacilityGlobal = document.getElementById('cmbFacilityGlobal');
                            FacilityGlobal.innerHTML = facilityoptions;
                            $('#cmbFacilityGlobal').val($('#cmbFacilityAllergy').val());

                            var VisitsAllergy = document.getElementById('cmbVisitsAllergy');
                            var visitoptions = VisitsAllergy.innerHTML;
                            var VisitGlobal = document.getElementById('cmbVisitsGlobal');
                            VisitGlobal.innerHTML = visitoptions;
                            $('#cmbVisitsGlobal').val($('#cmbVisitsAllergy').val());
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

            if (flag == 'visit')
                sec_arg = $('#cmbFacilityAllergy option:selected').val();
            else if (flag == 'location') {
                sec_arg = temp;
                id = $('#cmbVisitsAllergy option:selected').val();
            }

            //Start Allergy Ajax
            visitOptions = document.getElementById('cmbVisitsAllergy').innerHTML;
            visitSelected = $('#cmbVisitsAllergy option:selected').val();
           // alert(visitOptions);
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
                    url: 'clinical-summary-allergies-filter',
                    data: JSON.stringify(requestData),
                    dataType: 'json',
                    contentType: 'application/json; charset=utf-8',
                    success: function (data) {
                        // if success

                        $("#allergies-portlet-tab").html(data);
                        toggle_combobox_allergy();
                        var cmbFacilityAllergy = document.getElementById('cmbFacilityAllergy');
                        var facilityoptions = cmbFacilityAllergy.innerHTML;
                        var FacilityGlobal = document.getElementById('cmbFacilityGlobal');
                        FacilityGlobal.innerHTML = facilityoptions;
                        $('#cmbFacilityGlobal').val($('#cmbFacilityAllergy').val());

                        var VisitsAllergy = document.getElementById('cmbVisitsAllergy');
                        var visitoptions = VisitsAllergy.innerHTML;
                        var VisitGlobal = document.getElementById('cmbVisitsGlobal');
                        VisitGlobal.innerHTML = visitoptions;
                        $('#cmbVisitsGlobal').val($('#cmbVisitsAllergy').val());
                        $("#tab-Allergies").fixedHeaderTable();
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
                }
            }
            //Start Allergy Ajax

        }

        function visits(id, flag) {
            var temp;
            if (flag == 'visit')
                sec_arg = $('#cmbFacilityVisit option:selected').val();
            else if (flag == 'location') {
                sec_arg = id; temp = id;
                id = $('#cmbVisitsVisit option:selected').val();
            }
            var facilityOptions = document.getElementById('cmbFacilityVisit').innerHTML;
            var visitOptions = document.getElementById('cmbVisitsVisit').innerHTML;
            var facilitySelected = $('#cmbFacilityVisit option:selected').val();
            var visitSelected = $('#cmbVisitsVisit option:selected').val();
            if (flag == 'location') {

                //filling dropdown start 
                try {
                    ShowLoader();
                    //var IsPrimaryData =
                    var requestData = {


                        FacilityId: sec_arg,
                        ExtensionToggleFunct: "toggle_combobox_visit() ;",
                        ExtensionFilterFunct: "visits(this.value,'visit');",
                        ExtensionIdName: "cmbVisitsVisit",
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

                            $("#VisitsComboBoxPortlet").html(data);
                            id = $('#cmbVisitsHome option:selected').val();
                            toggle_combobox_visit();
                            var FacilityVisit = document.getElementById('cmbFacilityVisit');
                            var facilityoptions = FacilityVisit.innerHTML;
                            var FacilityGlobal = document.getElementById('cmbFacilityGlobal');
                            FacilityGlobal.innerHTML = facilityoptions;
                            $('#cmbFacilityGlobal').val($('#cmbFacilityVisit').val());

                            var VisitsVisit = document.getElementById('cmbVisitsVisit');
                            var visitoptions = VisitsVisit.innerHTML;
                            var VisitGlobal = document.getElementById('cmbVisitsGlobal');
                            VisitGlobal.innerHTML = visitoptions;  
                            $('#cmbVisitsGlobal').val($('#cmbVisitsVisit').val());
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

            if (flag == 'visit')
                sec_arg = $('#cmbFacilityVisit option:selected').val();
            else if (flag == 'location') {
                sec_arg = temp;
                id = $('#cmbVisitsVisit option:selected').val();
            }
            visitOptions = document.getElementById('cmbFacilityVisit').innerHTML;
            visitSelected = $('#cmbFacilityVisit option:selected').val();
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
                    url: 'clinical-summary-visits-filter',
                    data: JSON.stringify(requestData),
                    dataType: 'json',
                    contentType: 'application/json; charset=utf-8',
                    success: function (data) {
                        // if success

                        $("#visit-portlet-tab").html(data);
                        toggle_combobox_visit();
                        id = $('#cmbVisitsHome option:selected').val();
                        var FacilityVisit = document.getElementById('cmbFacilityVisit');
                        var facilityoptions = FacilityVisit.innerHTML;
                        var FacilityGlobal = document.getElementById('cmbFacilityGlobal');
                        FacilityGlobal.innerHTML = facilityoptions;
                        $('#cmbFacilityGlobal').val($('#cmbFacilityVisit').val());

                        var VisitsVisit = document.getElementById('cmbVisitsVisit');
                        var visitoptions = VisitsVisit.innerHTML;
                        var VisitGlobal = document.getElementById('cmbVisitsGlobal');
                        VisitGlobal.innerHTML = visitoptions; 
                        $('#cmbVisitsGlobal').val($('#cmbVisitsVisit').val());
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
                }
            }

        }

        function providers(id, flag) {
            var temp;
            if (flag == 'visit')
                sec_arg = $('#cmbFacilityProvider option:selected').val();
            else if (flag == 'location') {
                sec_arg = id; temp = id;
                id = $('#cmbVisitsProvider option:selected').val();
            }
            var facilityOptions = document.getElementById('cmbFacilityProvider').innerHTML;
            var visitOptions = document.getElementById('cmbVisitsProvider').innerHTML;
            var facilitySelected = $('#cmbFacilityProvider option:selected').val();
            var visitSelected = $('#cmbVisitsProvider option:selected').val();
            if (flag == 'location') {

                //filling dropdown start 
                try {
                    ShowLoader();
                    //var IsPrimaryData =
                    var requestData = {


                        FacilityId: sec_arg,
                        ExtensionToggleFunct: "toggle_combobox_providers();",
                        ExtensionFilterFunct: "providers(this.value,'visit');",
                        ExtensionIdName: "cmbVisitsProvider",
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

                            $("#ProviderComboBoxPortlet").html(data);
                            id = $('#cmbVisitsHome option:selected').val();
                            toggle_combobox_providers();
                            var FacilityVisit = document.getElementById('cmbFacilityProvider');
                            var facilityoptions = FacilityVisit.innerHTML;
                            var FacilityGlobal = document.getElementById('cmbFacilityGlobal');
                            FacilityGlobal.innerHTML = facilityoptions;
                            $('#cmbFacilityGlobal').val($('#cmbFacilityProvider').val());

                            var VisitsVisit = document.getElementById('cmbVisitsProvider');
                            var visitoptions = VisitsVisit.innerHTML;
                            var VisitGlobal = document.getElementById('cmbVisitsGlobal');
                            VisitGlobal.innerHTML = visitoptions;  
                            $('#cmbVisitsGlobal').val($('#cmbVisitsProvider').val());
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

            if (flag == 'visit')
                sec_arg = $('#cmbFacilityProvider option:selected').val();
            else if (flag == 'location') {
                sec_arg = temp;
                id = $('#cmbVisitsProvider option:selected').val();
            }
            visitOptions = document.getElementById('cmbFacilityProvider').innerHTML;
            visitSelected = $('#cmbFacilityProvider option:selected').val();
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
                    url: 'ClinicalSummary/FilterProvidersData',
                    data: JSON.stringify(requestData),
                    dataType: 'json',
                    contentType: 'application/json; charset=utf-8',
                    success: function (data) {
                        // if success

                        $("#provider-portlet-tab").html(data);
                        toggle_combobox_providers();
                        id = $('#cmbVisitsHome option:selected').val();
                        var FacilityVisit = document.getElementById('cmbFacilityProvider');
                        var facilityoptions = FacilityVisit.innerHTML;
                        var FacilityGlobal = document.getElementById('cmbFacilityGlobal');
                        FacilityGlobal.innerHTML = facilityoptions;
                        $('#cmbFacilityGlobal').val($('#cmbFacilityProvider').val());

                        var VisitsVisit = document.getElementById('cmbVisitsProvider');
                        var visitoptions = VisitsVisit.innerHTML;
                        var VisitGlobal = document.getElementById('cmbVisitsGlobal');
                        VisitGlobal.innerHTML = visitoptions; 
                        $('#cmbVisitsGlobal').val($('#cmbVisitsProvider').val());
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
                }
            }

        }

        function immunizations(id, flag) {
            var temp;
            if (flag == 'visit')
                sec_arg = $('#cmbFacilityImmunization option:selected').val();
            else if (flag == 'location') {
                sec_arg = id; temp = id;
                id = $('#cmbVisitsImmunization option:selected').val();
            }
            var facilityOptions = document.getElementById('cmbFacilityImmunization').innerHTML;
            var visitOptions = document.getElementById('cmbVisitsImmunization').innerHTML;
            var facilitySelected = $('#cmbFacilityImmunization option:selected').val();
            var visitSelected = $('#cmbVisitsImmunization option:selected').val();
            if (flag == 'location') {

                //filling dropdown start 
                try {
                    ShowLoader();
                    //var IsPrimaryData =
                    var requestData = {

                        FacilityId: sec_arg,
                        ExtensionToggleFunct: "toggle_combobox_immunization() ;",
                        ExtensionFilterFunct: "immunizations(this.value,'visit'); ",
                        ExtensionIdName: "cmbVisitsImmunization",
                        facilityOptions: facilityOptions,
                        visitOptions: visitOptions,
                        facilitySelected: facilitySelected,
                        visitSelected: visitSelected
                    };


                    $.ajax({
                        type: 'POST',
                        url: 'clinical-summary-visit-dropdown',
                        data: JSON.stringify(requestData),
                        dataType: 'json',
                        contentType: 'application/json; charset=utf-8',
                        success: function (data) {
                            // if success

                            $("#immunizationComboBoxPortlet").html(data);
                            id = $('#cmbVisitsHome option:selected').val();
                            var FacilityImmunization = document.getElementById('cmbFacilityImmunization');
                            var facilityoptions = FacilityImmunization.innerHTML;
                            var FacilityGlobal = document.getElementById('cmbFacilityGlobal');
                            FacilityGlobal.innerHTML = facilityoptions;
                            $('#cmbFacilityGlobal').val($('#cmbFacilityImmunization').val());

                            var VisitsImmunization = document.getElementById('cmbVisitsImmunization');
                            var visitoptions = VisitsImmunization.innerHTML;
                            var VisitGlobal = document.getElementById('cmbVisitsGlobal');
                            VisitGlobal.innerHTML = visitoptions;
                            $('#cmbVisitsGlobal').val($('#cmbVisitsImmunization').val());
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

            if (flag == 'visit')
                sec_arg = $('#cmbFacilityImmunization option:selected').val();
            else if (flag == 'location') {
                sec_arg = temp;
                id = $('#cmbVisitsImmunization option:selected').val();
            }
            visitOptions = document.getElementById('cmbVisitsImmunization').innerHTML;
            visitSelected = $('#cmbVisitsImmunization option:selected').val();
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
                    url: 'clinical-summary-immunizations-filter',
                    data: JSON.stringify(requestData),
                    dataType: 'json',
                    contentType: 'application/json; charset=utf-8',
                    success: function (data) {
                        // if success

                        $("#immunization-portlet-tab").html(data);
                        toggle_combobox_immunization();
                        var FacilityImmunization = document.getElementById('cmbFacilityImmunization');
                        var facilityoptions = FacilityImmunization.innerHTML;
                        var FacilityGlobal = document.getElementById('cmbFacilityGlobal');
                        FacilityGlobal.innerHTML = facilityoptions;
                        $('#cmbFacilityGlobal').val($('#cmbFacilityImmunization').val());

                        var VisitsImmunization = document.getElementById('cmbVisitsImmunization');
                        var visitoptions = VisitsImmunization.innerHTML;
                        var VisitGlobal = document.getElementById('cmbVisitsGlobal');
                        VisitGlobal.innerHTML = visitoptions;
                        $('#cmbVisitsGlobal').val($('#cmbVisitsImmunization').val());
                        $("#tab-Immunization").fixedHeaderTable();
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
                }
            }


        }

        function problems(id, flag) {
            var temp;
            if (flag == 'visit')
                sec_arg = $('#cmbFacilityProblem option:selected').val();
            else if (flag == 'location') {
                sec_arg = id; temp = id;
                id = $('#cmbVisitsProblem option:selected').val();
            }
            var facilityOptions = document.getElementById('cmbFacilityProblem').innerHTML;
            var visitOptions = document.getElementById('cmbVisitsProblem').innerHTML;
            var facilitySelected = $('#cmbFacilityProblem option:selected').val();
            var visitSelected = $('#cmbVisitsProblem option:selected').val();
            if (flag == 'location') {
                    
                //filling dropdown start 
                try {
                    ShowLoader();
                    //var IsPrimaryData =
                    var requestData = {

                        FacilityId: sec_arg,
                        ExtensionToggleFunct: "toggle_combobox_problem();",
                        ExtensionFilterFunct: "problems(this.value,'visit');",
                        ExtensionIdName: "cmbVisitsProblem",
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

                            $("#problemComboBoxPortlet").html(data);
                            id = $('#cmbVisitsHome option:selected').val();

                            toggle_combobox_problem();
                            var FacilityProblem = document.getElementById('cmbFacilityProblem');
                            var facilityoptions = FacilityProblem.innerHTML;
                            var FacilityGlobal = document.getElementById('cmbFacilityGlobal');
                            FacilityGlobal.innerHTML = facilityoptions;
                            $('#cmbFacilityGlobal').val($('#cmbFacilityProblem').val());

                            var VisitsProblem = document.getElementById('cmbVisitsProblem');
                            var visitoptions = VisitsProblem.innerHTML;
                            var VisitGlobal = document.getElementById('cmbVisitsGlobal');
                            VisitGlobal.innerHTML = visitoptions;
                            $('#cmbVisitsGlobal').val($('#cmbVisitsProblem').val());


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

            if (flag == 'visit')
                sec_arg = $('#cmbFacilityProblem option:selected').val();
            else if (flag == 'location') {
                sec_arg = temp;
                id = $('#cmbVisitsProblem option:selected').val();
            }
            visitOptions = document.getElementById('cmbVisitsProblem').innerHTML;
            visitSelected = $('#cmbVisitsProblem option:selected').val();
            //alert(visitOptions);
          //  alert(visitSelected);
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
                        toggle_combobox_problem();
                        var FacilityProblem = document.getElementById('cmbFacilityProblem');
                        var facilityoptions = FacilityProblem.innerHTML;
                        var FacilityGlobal = document.getElementById('cmbFacilityGlobal');
                        FacilityGlobal.innerHTML = facilityoptions;
                        $('#cmbFacilityGlobal').val($('#cmbFacilityProblem').val());

                        var VisitsProblem = document.getElementById('cmbVisitsProblem');
                        var visitoptions = VisitsProblem.innerHTML;
                        var VisitGlobal = document.getElementById('cmbVisitsGlobal');
                        VisitGlobal.innerHTML = visitoptions;
                        $('#cmbVisitsGlobal').val($('#cmbVisitsProblem').val());
                        $('#tab-problems').fixedHeaderTable();
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
                }
            }

        }

        function vitals(id, flag) {
            var temp;
            if (flag == 'visit')
                sec_arg = $('#cmbFacilityVital option:selected').val();
            else if (flag == 'location') {
                sec_arg = id; temp = id;
                id = $('#cmbVisitsVital option:selected').val();
            }
            var facilityOptions = document.getElementById('cmbFacilityVital').innerHTML;
            var visitOptions = document.getElementById('cmbVisitsVital').innerHTML;
            var facilitySelected = $('#cmbFacilityVital option:selected').val();
            var visitSelected = $('#cmbVisitsVital option:selected').val();

            if (flag == 'location') {

                //filling dropdown start 
                try {
                    ShowLoader();
                    //var IsPrimaryData =
                    var requestData = {

                        FacilityId: sec_arg,
                        ExtensionToggleFunct: "toggle_combobox_vital();",
                        ExtensionFilterFunct: "vitals(this.value,'visit');",
                        ExtensionIdName: "cmbVisitsVital",
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

                            $("#vitalComboBoxPortlet").html(data);
                            id = $('#cmbVisitsHome option:selected').val();
                            var FacilityVital = document.getElementById('cmbFacilityVital');
                            var facilityoptions = FacilityVital.innerHTML;
                            var FacilityGlobal = document.getElementById('cmbFacilityGlobal');
                            FacilityGlobal.innerHTML = facilityoptions;
                            $('#cmbFacilityGlobal').val($('#cmbFacilityVital').val());

                            var VisitsVital = document.getElementById('cmbVisitsVital');
                            var visitoptions = VisitsVital.innerHTML;
                            var VisitGlobal = document.getElementById('cmbVisitsGlobal');
                            VisitGlobal.innerHTML = visitoptions;
                            $('#cmbVisitsGlobal').val($('#cmbVisitsVital').val());
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

            if (flag == 'visit')
                sec_arg = $('#cmbFacilityVital option:selected').val();
            else if (flag == 'location') {
                sec_arg = temp;
                id = $('#cmbVisitsVital option:selected').val();
            }
            visitOptions = document.getElementById('cmbVisitsVital').innerHTML;
            visitSelected = $('#cmbVisitsVital option:selected').val();
            //alert(visitOptions);
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
                    url: 'clinical-summary-vitals-filter',
                    data: JSON.stringify(requestData),
                    dataType: 'json',
                    contentType: 'application/json; charset=utf-8',
                    success: function (data) {
                        // if success

                        $("#vital-portlet-tab").html(data);
                        toggle_combobox_vital();
                        var FacilityVital = document.getElementById('cmbFacilityVital');
                        var facilityoptions = FacilityVital.innerHTML;
                        var FacilityGlobal = document.getElementById('cmbFacilityGlobal');
                        FacilityGlobal.innerHTML = facilityoptions;
                        $('#cmbFacilityGlobal').val($('#cmbFacilityVital').val());

                        var VisitsVital = document.getElementById('cmbVisitsVital');
                        var visitoptions = VisitsVital.innerHTML;
                        var VisitGlobal = document.getElementById('cmbVisitsGlobal');
                        VisitGlobal.innerHTML = visitoptions;
                        $('#cmbVisitsGlobal').val($('#cmbVisitsVital').val());

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
                }
            }

        }

        function documents(id, flag) {
            var temp;
            if (flag == 'visit')
                sec_arg = $('#cmbFacilityDocument option:selected').val();
            else if (flag == 'location') {
                sec_arg = id; temp = id;
                id = $('#cmbVisitsDocument option:selected').val();
            }
            var facilityOptions = document.getElementById('cmbFacilityDocument').innerHTML;
            var visitOptions = document.getElementById('cmbVisitsDocument').innerHTML;
            var facilitySelected = $('#cmbFacilityDocument option:selected').val();
            var visitSelected = $('#cmbVisitsDocument option:selected').val();

            if (flag == 'location') {

                //filling dropdown start 
                try {
                    ShowLoader();
                    //var IsPrimaryData =
                    var requestData = {

                        FacilityId: sec_arg,
                        ExtensionToggleFunct: "toggle_combobox_document();",
                        ExtensionFilterFunct: "documents(this.value,'visit');",
                        ExtensionIdName: "cmbVisitsDocument",
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

                            $("#documentComboBoxPortlet").html(data);
                            id = $('#cmbVisitsHome option:selected').val();
                            var FacilityDocument = document.getElementById('cmbFacilityDocument');
                            var facilityoptions = FacilityDocument.innerHTML;
                            var FacilityGlobal = document.getElementById('cmbFacilityGlobal');
                            FacilityGlobal.innerHTML = facilityoptions;
                            $('#cmbFacilityGlobal').val($('#cmbFacilityDocument').val());

                            var VisitsDocument = document.getElementById('cmbVisitsDocument');
                            var visitoptions = VisitsDocument.innerHTML;
                            var VisitGlobal = document.getElementById('cmbVisitsGlobal');
                            VisitGlobal.innerHTML = visitoptions;
                            $('#cmbVisitsGlobal').val($('#cmbVisitsDocument').val());

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

            if (flag == 'visit')
                sec_arg = $('#cmbFacilityDocument option:selected').val();
            else if (flag == 'location') {
                sec_arg = temp;
                id = $('#cmbVisitsDocument option:selected').val();
            }
            visitOptions = document.getElementById('cmbVisitsDocument').innerHTML;
            visitSelected = $('#cmbVisitsDocument option:selected').val();
          //  alert(visitOptions);
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
                    url: 'clinical-summary-documents-filter',
                    data: JSON.stringify(requestData),
                    dataType: 'json',
                    contentType: 'application/json; charset=utf-8',
                    success: function (data) {
                        // if success

                        $("#document-portlet-tab").html(data);
                        var FacilityDocument = document.getElementById('cmbFacilityDocument');
                        var facilityoptions = FacilityDocument.innerHTML;
                        var FacilityGlobal = document.getElementById('cmbFacilityGlobal');
                        FacilityGlobal.innerHTML = facilityoptions;
                        $('#cmbFacilityGlobal').val($('#cmbFacilityDocument').val());

                        var VisitsDocument = document.getElementById('cmbVisitsDocument');
                        var visitoptions = VisitsDocument.innerHTML;
                        var VisitGlobal = document.getElementById('cmbVisitsGlobal');
                        VisitGlobal.innerHTML = visitoptions;
                        $('#cmbVisitsGlobal').val($('#cmbVisitsDocument').val());
                        $('#tab-Documents').fixedHeaderTable();

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
                }
            }

        }

        function Procedures(id, flag) {
            var temp;
            if (flag == 'visit')
                sec_arg = $('#cmbFacilityProcedure option:selected').val();
            else if (flag == 'location') {
                sec_arg = id; temp = id;
                id = $('#cmbVisitsProcedure option:selected').val();
            }
            var facilityOptions = document.getElementById('cmbFacilityProcedure').innerHTML;
            var visitOptions = document.getElementById('cmbVisitsProcedure').innerHTML;
            var facilitySelected = $('#cmbFacilityProcedure option:selected').val();
            var visitSelected = $('#cmbVisitsProcedure option:selected').val();

            if (flag == 'location') {
                //alert("hi");
                //filling dropdown start 
                try {
                    ShowLoader();
                    //var IsPrimaryData =
                    var requestData = {

                        FacilityId: sec_arg,
                        ExtensionToggleFunct: "toggle_combobox_Procedures();",
                        ExtensionFilterFunct: "Procedures(this.value,'visit');",
                        ExtensionIdName: "cmbVisitsProcedure",
                        facilityOptions: facilityOptions,
                        visitOptions: visitOptions,
                        facilitySelected: facilitySelected,
                        visitSelected: visitSelected
                        // VisitId: id

                    };


                    $.ajax({
                        type: 'POST',
                        url: 'clinical-summary-Procedure-dropdown',
                        data: JSON.stringify(requestData),
                        dataType: 'json',
                        contentType: 'application/json; charset=utf-8',
                        success: function (data) {
                            // if success

                            $("#ProcedureComboBoxPortlet").html(data);
                            id = $('#cmbVisitsHome option:selected').val();
                            var FacilityDocument = document.getElementById('cmbFacilityProcedure');
                            var facilityoptions = FacilityDocument.innerHTML;
                            var FacilityGlobal = document.getElementById('cmbFacilityGlobal');
                            FacilityGlobal.innerHTML = facilityoptions;
                            $('#cmbFacilityGlobal').val($('#cmbFacilityProcedure').val());

                            var VisitsDocument = document.getElementById('cmbVisitsProcedure');
                            var visitoptions = VisitsDocument.innerHTML;
                            var VisitGlobal = document.getElementById('cmbVisitsGlobal');
                            VisitGlobal.innerHTML = visitoptions;
                            $('#cmbVisitsGlobal').val($('#cmbVisitsProcedure').val());

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

            if (flag == 'visit')
                sec_arg = $('#cmbFacilityProcedure option:selected').val();
            else if (flag == 'location') {
                sec_arg = temp;
                id = $('#cmbVisitsProcedure option:selected').val();
            }
            visitOptions = document.getElementById('cmbVisitsProcedure').innerHTML;
            visitSelected = $('#cmbVisitsProcedure option:selected').val();
            //  alert(visitOptions);
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
                    url: 'clinical-summary-Procdure-filter',
                    data: JSON.stringify(requestData),
                    dataType: 'json',
                    contentType: 'application/json; charset=utf-8',
                    success: function (data) {
                        // if success

                        $("#Procdure-portlet-tab").html(data);
                        toggle_combobox_Procedures();
                        var FacilityDocument = document.getElementById('cmbFacilityProcedure');
                        var facilityoptions = FacilityDocument.innerHTML;
                        var FacilityGlobal = document.getElementById('cmbFacilityGlobal');
                        FacilityGlobal.innerHTML = facilityoptions;
                        $('#cmbFacilityGlobal').val($('#cmbFacilityProcedure').val());

                        var VisitsDocument = document.getElementById('cmbVisitsProcedure');
                        var visitoptions = VisitsDocument.innerHTML;
                        var VisitGlobal = document.getElementById('cmbVisitsGlobal');
                        VisitGlobal.innerHTML = visitoptions;
                        $('#cmbVisitsGlobal').val($('#cmbVisitsProcedure').val());
                        $('#tab-Procedure').fixedHeaderTable();

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
                }
            }

        }

        function PlanOfCare(id,flag)
        {
            var temp;
           // alert("Hi");
            if (flag == 'visit')
                sec_arg = $('#cmbFacilityPlanOfCare option:selected').val();
            else if (flag == 'location') {
                sec_arg = id; temp = id;
                id = $('#cmbVisitsPlanOfCare option:selected').val();
            }
            var facilityOptions = document.getElementById('cmbFacilityPlanOfCare').innerHTML;
            var visitOptions = document.getElementById('cmbVisitsPlanOfCare').innerHTML;
            var facilitySelected = $('#cmbFacilityPlanOfCare option:selected').val();
            var visitSelected = $('#cmbVisitsPlanOfCare option:selected').val();

            if (flag == 'location') {
               // alert("Hi");
                //filling dropdown start 
                try {
                    ShowLoader();
                    //var IsPrimaryData =
                    var requestData = {

                        FacilityId: sec_arg,
                        ExtensionToggleFunct: "toggle_combobox_PlanOfCare();",
                        ExtensionFilterFunct: "PlanOfCare(this.value,'visit');",
                        ExtensionIdName: "cmbVisitsPlanOfCare",
                        facilityOptions: facilityOptions,
                        visitOptions: visitOptions,
                        facilitySelected: facilitySelected,
                        visitSelected: visitSelected
                        // VisitId: id

                    };
                   // alert("Hi");

                    $.ajax({
                        type: 'POST',
                        url: 'clinical-summary-visit-dropdown',
                        data: JSON.stringify(requestData),
                        dataType: 'json',
                        contentType: 'application/json; charset=utf-8',
                        success: function (data) {
                            // if success
                           // toggle_combobox_PlanOfCare();
                            $("#PlanOfCareComboBoxPortlet").html(data);
                            id = $('#cmbVisitsHome option:selected').val();
                            var FacilityDocument = document.getElementById('cmbFacilityPlanOfCare');
                            var facilityoptions = FacilityDocument.innerHTML;
                            var FacilityGlobal = document.getElementById('cmbFacilityGlobal');
                            FacilityGlobal.innerHTML = facilityoptions;
                            $('#cmbFacilityGlobal').val($('#cmbFacilityPlanOfCare').val());

                            var VisitsDocument = document.getElementById('cmbVisitsPlanOfCare');
                            var visitoptions = VisitsDocument.innerHTML;
                            var VisitGlobal = document.getElementById('cmbVisitsGlobal');
                            VisitGlobal.innerHTML = visitoptions;
                            $('#cmbVisitsGlobal').val($('#cmbVisitsPlanOfCare').val());

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

            if (flag == 'visit')
                sec_arg = $('#cmbFacilityPlanOfCare option:selected').val();
            else if (flag == 'location') {
                sec_arg = temp;
                id = $('#cmbVisitsPlanOfCare option:selected').val();
            }
            visitOptions = document.getElementById('cmbVisitsPlanOfCare').innerHTML;
            visitSelected = $('#cmbVisitsPlanOfCare option:selected').val();
            //  alert(visitOptions);
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
                    url: 'clinical-summary-PlanOfCare-filter',
                    data: JSON.stringify(requestData),
                    dataType: 'json',
                    contentType: 'application/json; charset=utf-8',
                    success: function (data) {
                        // if success
                       
                        $("#PlanOfCare-portlet-tab").html(data);
                        toggle_combobox_PlanOfCare();
                        var FacilityDocument = document.getElementById('cmbFacilityPlanOfCare');
                        var facilityoptions = FacilityDocument.innerHTML;
                        var FacilityGlobal = document.getElementById('cmbFacilityGlobal');
                        FacilityGlobal.innerHTML = facilityoptions;
                        $('#cmbFacilityGlobal').val($('#cmbFacilityPlanOfCare').val());

                        var VisitsDocument = document.getElementById('cmbVisitsPlanOfCare');
                        var visitoptions = VisitsDocument.innerHTML;
                        var VisitGlobal = document.getElementById('cmbVisitsGlobal');
                        VisitGlobal.innerHTML = visitoptions;
                        $('#cmbVisitsGlobal').val($('#cmbVisitsPlanOfCare').val());
                        $('#tab-PlanOfCare').fixedHeaderTable();

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
                }
            }

        
        }

        function ClinicalInstructions(id,flag)
        {
            var temp;
            // alert("Hi");
            if (flag == 'visit')
                sec_arg = $('#cmbFacilityClinicalInstructions option:selected').val();
            else if (flag == 'location') {
                sec_arg = id; temp = id;
                id = $('#cmbVisitsClinicalInstructions option:selected').val();
            }
            var facilityOptions = document.getElementById('cmbFacilityClinicalInstructions').innerHTML;
            var visitOptions = document.getElementById('cmbVisitsClinicalInstructions').innerHTML;
            var facilitySelected = $('#cmbFacilityClinicalInstructions option:selected').val();
            var visitSelected = $('#cmbVisitsClinicalInstructions option:selected').val();

            if (flag == 'location') {
                // alert("Hi");
                //filling dropdown start 
                try {
                    ShowLoader();
                    //var IsPrimaryData =
                    var requestData = {

                        FacilityId: sec_arg,
                        ExtensionToggleFunct: "toggle_combobox_ClinicalInstructions();",
                        ExtensionFilterFunct: "ClinicalInstructions(this.value,'visit');",
                        ExtensionIdName: "cmbVisitsClinicalInstructions",
                        facilityOptions: facilityOptions,
                        visitOptions: visitOptions,
                        facilitySelected: facilitySelected,
                        visitSelected: visitSelected
                        // VisitId: id

                    };
                    // alert("Hi");

                    $.ajax({
                        type: 'POST',
                        url: 'clinical-summary-visit-dropdown',
                        data: JSON.stringify(requestData),
                        dataType: 'json',
                        contentType: 'application/json; charset=utf-8',
                        success: function (data) {
                            // if success

                            $("#ClinicalInstructionsComboBoxPortlet").html(data);
                            id = $('#cmbVisitsHome option:selected').val();
                            var FacilityDocument = document.getElementById('cmbFacilityClinicalInstructions');
                            var facilityoptions = FacilityDocument.innerHTML;
                            var FacilityGlobal = document.getElementById('cmbFacilityGlobal');
                            FacilityGlobal.innerHTML = facilityoptions;
                            $('#cmbFacilityGlobal').val($('#cmbFacilityClinicalInstructions').val());

                            var VisitsDocument = document.getElementById('cmbVisitsClinicalInstructions');
                            var visitoptions = VisitsDocument.innerHTML;
                            var VisitGlobal = document.getElementById('cmbVisitsClinicalInstructions');
                            VisitGlobal.innerHTML = visitoptions;
                            $('#cmbVisitsGlobal').val($('#cmbVisitsClinicalInstructions').val());

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

            if (flag == 'visit')
                sec_arg = $('#cmbFacilityClinicalInstructions option:selected').val();
            else if (flag == 'location') {
                sec_arg = temp;
                id = $('#cmbVisitsClinicalInstructions option:selected').val();
            }
            visitOptions = document.getElementById('cmbVisitsClinicalInstructions').innerHTML;
            visitSelected = $('#cmbVisitsClinicalInstructions option:selected').val();
            //  alert(visitOptions);
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
                    url: 'clinical-summary-PlanOfCare-filter',
                    data: JSON.stringify(requestData),
                    dataType: 'json',
                    contentType: 'application/json; charset=utf-8',
                    success: function (data) {
                        // if success
                        
                        
                        $("#ClinicalInstructions-portlet-tab").html(data);
                        toggle_combobox_ClinicalInstructions();
                        var FacilityDocument = document.getElementById('cmbFacilityClinicalInstructions');
                        var facilityoptions = FacilityDocument.innerHTML;
                        var FacilityGlobal = document.getElementById('cmbFacilityGlobal');
                        FacilityGlobal.innerHTML = facilityoptions;
                        $('#cmbFacilityGlobal').val($('#cmbFacilityClinicalInstructions').val());

                        var VisitsDocument = document.getElementById('cmbVisitsClinicalInstructions');
                        var visitoptions = VisitsDocument.innerHTML;
                        var VisitGlobal = document.getElementById('cmbVisitsGlobal');
                        VisitGlobal.innerHTML = visitoptions;
                        $('#cmbVisitsGlobal').val($('#cmbVisitsClinicalInstructions').val());
                        $('#tab-ClinicalInstruction').fixedHeaderTable();

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
                }
            }

        
        }
        function Insurance(id,flag)
        {
        
            var temp;
            if (flag == 'visit')
                sec_arg = $('#cmbFacilityInsurance option:selected').val();
            else if (flag == 'location') {
                sec_arg = id; temp = id;
                id = $('#cmbVisitsInsurance option:selected').val();
            }
            var facilityOptions = document.getElementById('cmbFacilityInsurance').innerHTML;
            var visitOptions = document.getElementById('cmbVisitsInsurance').innerHTML;
            var facilitySelected = $('#cmbFacilityInsurance option:selected').val();
            var visitSelected = $('#cmbVisitsInsurance option:selected').val();

            if (flag == 'location') {

                //filling dropdown start 
                try {
                    ShowLoader();
                    //var IsPrimaryData =
                    var requestData = {

                        FacilityId: sec_arg,
                        ExtensionToggleFunct: "toggle_combobox_document();",
                        ExtensionFilterFunct: "Insurance(this.value,'visit');",
                        ExtensionIdName: "cmbVisitsInsurance",
                        facilityOptions: facilityOptions,
                        visitOptions: visitOptions,
                        facilitySelected: facilitySelected,
                        visitSelected: visitSelected
                        // VisitId: id

                    };


                    $.ajax({
                        type: 'POST',
                        url: 'clinical-summary-Insurance-dropdown',
                        data: JSON.stringify(requestData),
                        dataType: 'json',
                        contentType: 'application/json; charset=utf-8',
                        success: function (data) {
                            // if success

                            $("#InsuranceComboBoxPortlet").html(data);
                            id = $('#cmbVisitsHome option:selected').val();
                            var FacilityDocument = document.getElementById('cmbFacilityInsurance');
                            var facilityoptions = FacilityDocument.innerHTML;
                            var FacilityGlobal = document.getElementById('cmbFacilityGlobal');
                            FacilityGlobal.innerHTML = facilityoptions;
                            $('#cmbFacilityGlobal').val($('#cmbFacilityInsurance').val());

                            var VisitsDocument = document.getElementById('cmbVisitsInsurance');
                            var visitoptions = VisitsDocument.innerHTML;
                            var VisitGlobal = document.getElementById('cmbVisitsGlobal');
                            VisitGlobal.innerHTML = visitoptions;
                            $('#cmbVisitsGlobal').val($('#cmbVisitsInsurance').val());

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

            if (flag == 'visit')
                sec_arg = $('#cmbFacilityInsurance option:selected').val();
            else if (flag == 'location') {
                sec_arg = temp;
                id = $('#cmbVisitsInsurance option:selected').val();
            }
            visitOptions = document.getElementById('cmbVisitsInsurance').innerHTML;
            visitSelected = $('#cmbVisitsInsurance option:selected').val();
            //  alert(visitOptions);
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
                    url: 'clinical-summary-PlanOfCare-filter',
                    data: JSON.stringify(requestData),
                    dataType: 'json',
                    contentType: 'application/json; charset=utf-8',
                    success: function (data) {
                        // if success

                        $("#Insurance-portlet-tab").html(data);
                        var FacilityDocument = document.getElementById('cmbFacilityInsurance');
                        var facilityoptions = FacilityDocument.innerHTML;
                        var FacilityGlobal = document.getElementById('cmbFacilityGlobal');
                        FacilityGlobal.innerHTML = facilityoptions;
                        $('#cmbFacilityGlobal').val($('#cmbFacilityInsurance').val());

                        var VisitsDocument = document.getElementById('cmbVisitsInsurance');
                        var visitoptions = VisitsDocument.innerHTML;
                        var VisitGlobal = document.getElementById('cmbVisitsGlobal');
                        VisitGlobal.innerHTML = visitoptions;
                        $('#cmbVisitsGlobal').val($('#cmbVisitsInsurance').val());
                        $('#tab-Insurance').fixedHeaderTable();

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
                }
            }

        }

        function open_medications() {

            window.location.href = '/medication-index';
        }


        function socialHistoryEdit(id) {
            try {
                //alert(($("#" + id).val()));

                var obj = jQuery.parseJSON($("#hdSocial" + id).val());

               
                $('#txtsocialID').val(id);
                $('#cmbDescription option:selected').text($.trim(obj.Description));
                $('#txtSocialValue').val($.trim(obj.Qualifier));
                $('#txtSocialBegin').val($.trim(obj.BeginDate));
                $('#txtSocialEnd').val($.trim(obj.EndDate));
                $('#txtSocialNote').val($.trim(obj.Note));


                var byear = obj.BeginDate[0];
                byear += obj.BeginDate[1];
                byear += obj.BeginDate[2];
                byear += obj.BeginDate[3];

                var bmon = obj.BeginDate[4];
                bmon += obj.BeginDate[5];

                var bdate = obj.BeginDate[6];
                bdate += obj.BeginDate[7];

                $('#txtSocialBegin_Month').val(bmon);
                $('#txtSocialBegin_Day').val(bdate);
                $('#txtSocialBegin_Year').val(byear);


                var eyear = obj.EndDate[0];
                eyear += obj.EndDate[1];
                eyear += obj.EndDate[2];
                eyear += obj.EndDate[3];

                var emon = obj.EndDate[4];
                emon += obj.EndDate[5];

                var edate = obj.EndDate[6];
                edate += obj.EndDate[7];

                $('#txtSocialEnd_Month').val(emon);
                $('#txtSocialEnd_Day').val(edate);
                $('#txtSocialEnd_Year').val(eyear);

                $('#txtSocialEnd_Month').attr("disabled", false);
                $('#txtSocialEnd_Day').attr("disabled", false);
                $('#txtSocialEnd_Year').attr("disabled", false);
                $('#txtSocialBegin_Month').attr("disabled", false);
                $('#txtSocialBegin_Day').attr("disabled", false);
                $('#txtSocialBegin_Year').attr("disabled", false);
                $('#cmbDescription').attr("disabled", false);
                $('#txtSocialValue').attr("disabled", false);
                $('#txtSocialBegin').attr("disabled", false);
                $('#txtSocialEnd').attr("disabled", false);
                $('#txtSocialNote').attr("disabled", false);

            } catch (e) {

                alert(e.message);
            }
            $('#hdFlag').val('socialTab');

            $("#addsocial-form").dialog('option', 'title', 'Edit Social');
            $("#addsocial-form").dialog("open");
        }
        function socialHistoryDetails(id) {

            try {
                var obj = jQuery.parseJSON($("#hdSocial" + id).val());
                $('#txtsocialID').val(id);
                $('#cmbDescription option:selected').text($.trim(obj.Description));
                $('#txtSocialValue').val($.trim(obj.Qualifier));
                $('#txtSocialBegin').val($.trim(obj.BeginDate));
                $('#txtSocialEnd').val($.trim(obj.EndDate));
                $('#txtSocialNote').val($.trim(obj.Note));

                var byear = obj.BeginDate[0];
                byear += obj.BeginDate[1];
                byear += obj.BeginDate[2];
                byear += obj.BeginDate[3];
                var bmon = obj.BeginDate[4];
                bmon += obj.BeginDate[5];
                var bdate = obj.BeginDate[6];
                bdate += obj.BeginDate[7];
                $('#txtSocialBegin_Month').val(bmon);
                $('#txtSocialBegin_Day').val(bdate);
                $('#txtSocialBegin_Year').val(byear);


                var eyear = obj.EndDate[0];
                eyear += obj.EndDate[1];
                eyear += obj.EndDate[2];
                eyear += obj.EndDate[3];
                var emon = obj.EndDate[4];
                emon += obj.EndDate[5];
                var edate = obj.EndDate[6];
                edate += obj.EndDate[7];
                $('#txtSocialEnd_Month').val(emon);
                $('#txtSocialEnd_Day').val(edate);
                $('#txtSocialEnd_Year').val(eyear);

                $('#txtSocialEnd_Month').attr("disabled", true);
                $('#txtSocialEnd_Day').attr("disabled", true);
                $('#txtSocialEnd_Year').attr("disabled", true);
                $('#txtSocialBegin_Month').attr("disabled", true);
                $('#txtSocialBegin_Day').attr("disabled", true);
                $('#txtSocialBegin_Year').attr("disabled", true);
                $('#cmbDescription').attr("disabled", true);
                $('#txtSocialValue').attr("disabled", true);
                $('#txtSocialBegin').attr("disabled", true);
                $('#txtSocialEnd').attr("disabled", true);
                $('#txtSocialNote').attr("disabled", true);


            } catch (e) {
                alert(e.message);
            }
            $('#hdFlag').val('socialTab');
            $("#addsocial-form").dialog('option', 'title', 'Social History Details');
            $("#addsocial-form").dialog("open");
        }
        function socialHistoryDelete(id) {

            ShowLoader();
            try {


                $('#txtsocialID').val(id);
                var requestData = {
                    PatSocialHistCntr: $.trim($('#txtsocialID').val())
                };

                $.ajax({
                    type: 'POST',
                    url: 'socialhistories-delete',
                    data: JSON.stringify(requestData),
                    dataType: 'json',
                    contentType: 'application/json; charset=utf-8',
                    success: function (data) {
                        // if success

                        $("#social-portlet-tab").html(data);
                        $("#tab-SocialHistory").fixedHeaderTable();
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


        function familyHistoryEdit(id) {

            $('.ui-dialog-title').html("Edit Family History ");

            try {

                
                var obj = jQuery.parseJSON($("#" + id).val());
                var strArr = $.trim($('#cmbRelationship  option:selected').val()).split("|");
                var concatVal = $.trim(obj.RelationshipId) + '|' + $.trim(obj.FamilyMember);

                if (((obj.AgeAtOnset) == 0) && ((obj.DiseasedAge) == 0)) {

                    $.trim($('#txtAgeOnsetSpinner').val(''));
                    $.trim($('#txtAgeDiseaseSpinner').val(''));
                }
                else if (((obj.AgeAtOnset) == 0) && ((obj.DiseasedAge) != 0)) {
                    $.trim($('#txtAgeOnsetSpinner').val(''));
                    $('#txtAgeDiseaseSpinner').val($.trim(obj.DiseasedAge));
                }
                else if (((obj.DiseasedAge) == 0) && ((obj.AgeAtOnset) != 0)) {
                    $('#txtAgeOnsetSpinner').val($.trim(obj.AgeAtOnset));
                    $.trim($('#txtAgeDiseaseSpinner').val(''));
                }
                else {
                    $('#txtAgeOnsetSpinner').val($.trim(obj.AgeAtOnset));
                    $('#txtAgeDiseaseSpinner').val($.trim(obj.DiseasedAge));
                }

                $('#cmbAlive').val(obj.Diseased);
                $('#hdFDescriptionText').val('');
                //   $('#hdFDescriptionText').val($.trim(obj.Description));
                $('#txtDescription').val($.trim(obj.Description));
                $('#hdFDescriptionId').val($.trim(obj.CodeValue));
                $('#txtfamilyID').val(id);
                $('#cmbRelationship').val(concatVal);
                //$('#txtDescription').val($.trim(obj.Description));
                $('#txtFamilyNote').val($.trim(obj.Note));
                $('#cmbConditionStatusFam').val($.trim(obj.ConditionStatusId));

                $('#cmbRelationship ').attr("disabled", false);
                $('#txtDescription').attr("disabled", false);
                $('#txtFamilyNote').attr("disabled", false);
                $('#cmbAlive').attr("disabled", false);
                $('#txtAgeOnsetSpinner').spinner({ disabled: false });
                $('#txtAgeDiseaseSpinner').spinner({ disabled: false });
                $('#cmbConditionStatusFam').attr("disabled", false);

            } catch (e) {
                alert(e.message);
            }
            $('#hdFlag').val('familyTab');
            $("#addfamily-form").dialog("open");
        }
        function familyHistoryDetails(id) {
            $('.ui-dialog-title').html("Family History Details");
            try {


                var obj = jQuery.parseJSON($("#" + id).val());

                var strArr = $.trim($('#cmbRelationship  option:selected').val()).split("|");
                var concatVal = $.trim(obj.RelationshipId) + '|' + $.trim(obj.FamilyMember);

                if (((obj.AgeAtOnset) == 0) && ((obj.DiseasedAge) == 0)) {

                    $.trim($('#txtAgeOnsetSpinner').val(''));
                    $.trim($('#txtAgeDiseaseSpinner').val(''));
                }
                else if (((obj.AgeAtOnset) == 0) && ((obj.DiseasedAge) != 0)) {
                    $.trim($('#txtAgeOnsetSpinner').val(''));
                    $('#txtAgeDiseaseSpinner').val($.trim(obj.DiseasedAge));
                }
                else if (((obj.DiseasedAge) == 0) && ((obj.AgeAtOnset) != 0)) {
                    $('#txtAgeOnsetSpinner').val($.trim(obj.AgeAtOnset));
                    $.trim($('#txtAgeDiseaseSpinner').val(''));
                }
                else {
                    $('#txtAgeOnsetSpinner').val($.trim(obj.AgeAtOnset));
                    $('#txtAgeDiseaseSpinner').val($.trim(obj.DiseasedAge));
                }

                $('#cmbAlive').val(obj.Diseased);

                $('#txtfamilyID').val(id);
                $('#cmbRelationship').val(concatVal);
                $('#txtDescription').val($.trim(obj.Description));
                $('#txtFamilyNote').val($.trim(obj.Note));
                //$('#hdFDescriptionText').val($.trim(obj.Description));
                $('#hdFDescriptionText').val('');
                $('#txtDescription').val($.trim(obj.Description));
                $('#hdFDescriptionId').val($.trim(obj.CodeValue));
                $('#cmbRelationship').attr("disabled", true);
                $('#txtDescription').attr("disabled", true);
                $('#txtFamilyNote').attr("disabled", true);
                $('#cmbAlive ').attr("disabled", true);
                $('#txtAgeOnsetSpinner').spinner({ disabled: true });
                $('#txtAgeDiseaseSpinner').spinner({ disabled: true });
                $('#cmbConditionStatusFam').attr("disabled", true);

            } catch (e) {
                alert(e.message);
            }
            $('#hdFlag').val('familyTab');
            $("#addfamily-form").dialog("open");
        }
        function familyHistoryDelete(id) {

            ShowLoader();
            try {
                $('#txtfamilyID').val(id);
                var requestData = {
                    PatFamilyHistCntr: $.trim($('#txtfamilyID').val())
                };

                $.ajax({
                    type: 'POST',
                    url: 'familyhistories-delete',
                    data: JSON.stringify(requestData),
                    dataType: 'json',
                    contentType: 'application/json; charset=utf-8',
                    success: function (data) {
                        // if success

                        $("#family-portlet-tab").html(data);
                        $("#tab-FamilyHistory").fixedHeaderTable();
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

        function pastHistoryEdit(id) {

            try {
                var obj = jQuery.parseJSON($("#" + id).val());
                $('#txtpastID').val(id);
                $('#dtDateOccurance').val($.trim(obj.DateOfOccurance));
                $('#txtPastDiagnosis').val($.trim(obj.Description));
                $('#txtPastNotes').val($.trim(obj.Note));


                var oyear = obj.DateOfOccurance[0];
                oyear += obj.DateOfOccurance[1];
                oyear += obj.DateOfOccurance[2];
                oyear += obj.DateOfOccurance[3];
                var omon = obj.DateOfOccurance[4];
                omon += obj.DateOfOccurance[5];
                var odate = obj.DateOfOccurance[6];
                odate += obj.DateOfOccurance[7];
                $('#txtoc_Month').val(omon);
                $('#txtoc_Day').val(odate);
                $('#txtoc_Year').val(oyear);



                $('#txtoc_Month').attr("disabled", false);
                $('#txtoc_Day').attr("disabled", false);
                $('#txtoc_Year').attr("disabled", false);


                $('#dtDateOccurance').attr("disabled", false);
                $('#txtPastDiagnosis').attr("disabled", false);
                $('#txtPastNotes').attr("disabled", false);


            } catch (e) {
                alert(e.message);
            }
            $('#hdFlag').val('pastTab');

            $("#addpast-form").dialog('option', 'title', 'Edit Past Medical Hisory');
            $("#addpast-form").dialog("open");
        }

        function pastHistoryDetails(id) {

            try {
                var obj = jQuery.parseJSON($("#" + id).val());
                $('#txtpastID').val(id);
                $('#dtDateOccurance').val($.trim(obj.DateOfOccurance));
                $('#txtPastDiagnosis').val($.trim(obj.Description));
                $('#txtPastNotes').val($.trim(obj.Note));
                var oyear = obj.DateOfOccurance[0];
                oyear += obj.DateOfOccurance[1];
                oyear += obj.DateOfOccurance[2];
                oyear += obj.DateOfOccurance[3];
                var omon = obj.DateOfOccurance[4];
                omon += obj.DateOfOccurance[5];
                var odate = obj.DateOfOccurance[6];
                odate += obj.DateOfOccurance[7];
                $('#txtoc_Month').val(omon);
                $('#txtoc_Day').val(odate);
                $('#txtoc_Year').val(oyear);

                $('#txtoc_Month').attr("disabled", true);
                $('#txtoc_Day').attr("disabled", true);
                $('#txtoc_Year').attr("disabled", true);
                $('#dtDateOccurance').attr("disabled", true);
                $('#txtPastDiagnosis').attr("disabled", true);
                $('#txtPastNotes').attr("disabled", true);


            } catch (e) {
                alert(e.message);
            }
            $('#hdFlag').val('pastTab');
            $("#addpast-form").dialog('option', 'title', 'Pas;t Medical Hisory Details');
            $("#addpast-form").dialog("open");
        }
        function pastHistoryDelete(id) {
            ShowLoader();
            try {
                $('#txtpastID').val(id);
                var requestData = {
                    PatMedicalHistCntr: $.trim($('#txtpastID').val())
                };

                $.ajax({
                    type: 'POST',
                    url: 'pastmedicalhistories-delete',
                    data: JSON.stringify(requestData),
                    dataType: 'json',
                    contentType: 'application/json; charset=utf-8',
                    success: function (data) {
                        // if success
                        // alert("Success : " + data);
                        $("#pasthistory-portlet-tab").html(data);
                        $("#tab-MedicalHistory").fixedHeaderTable();
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

        function allergyEdit(id) {

            try {

                var obj = jQuery.parseJSON($("#" + id).val());
                $('#txtallergyID').val(id);
                $('#dtEffectiveDate').val($.trim(obj.EffectiveDate));
                $('#txtAllergenType').val($.trim(obj.AllergenType));
                $('#txtAllergen').val($.trim(obj.Allergen));
                $('#txtReaction').val($.trim(obj.Reaction));
                $('#txtNote').val($.trim(obj.Note));
                $('#cmbConditionStatus').val($.trim(obj.ConditionStatusId));

                var year = obj.EffectiveDate[0];
                year += obj.EffectiveDate[1];
                year += obj.EffectiveDate[2];
                year += obj.EffectiveDate[3];
                var mon = obj.EffectiveDate[4];
                mon += obj.EffectiveDate[5];
                var date = obj.EffectiveDate[6];
                date += obj.EffectiveDate[7];

                $('#txtAllergiesDate_Month').val(mon);
                $('#txtAllergiesDate_Day').val(date);
                $('#txtAllergiesDate_Year').val(year);

                $('#dtEffectiveDate').attr("disabled", false);
                $('#txtAllergenType').attr("disabled", false);
                $('#txtAllergiesDate_Month').attr("disabled", false);
                $('#txtAllergiesDate_Day').attr("disabled", false);
                $('#txtAllergiesDate_Year').attr("disabled", false);
                $('#txtAllergen').attr("disabled", false);
                $('#txtReaction').attr("disabled", false);
                $('#txtNote').attr("disabled", false);
                $('#cmbConditionStatus').attr("disabled", false);


            } catch (e) {
                alert(e.message);
            }
            $('#hdFlag').val('allergyTab');

            $("#addallergies-form").dialog('option', 'title', 'Edit Allergy');
            $("#addallergies-form").dialog("open");
        }

        function EditProcedure(id) {

            try {

                var obj = jQuery.parseJSON($("#" + id).val());

                $('#txtprocedureid').val(id);
               
                $('#date').val($.trim(obj.DateModified));
                $('#txtProcedureDescription').val($.trim(obj.Description));
                $('#txtProcedureNote').val($.trim(obj.Note));
                $('#date').attr("disabled", false);
                $('#txtProcedureDescription').attr("disabled", false);
                $('#txtProcedureNote').attr("disabled", false);
           


            } catch (e) {
                alert(e.message);
            }
           
            $("#proc-remove-class").removeClass("ui-state-highlight");
            $( "#proc-remove-class" ).empty();
            $("#date").removeClass("ui-state-error");
            $("#addProcedures-form").dialog('option', 'title', 'Edit Procedure');
            $("#addProcedures-form").dialog("open");
        }
        function detailsProcedure(id)
        {
            try {

                var obj = jQuery.parseJSON($("#" + id).val());

                $('#txtprocedureid').val(id);
               
                $('#date').val($.trim(obj.DateModified));
                $('#txtProcedureDescription').val($.trim(obj.Description));
                $('#txtProcedureNote').val($.trim(obj.Note));
                $('#date').attr("disabled", true);
                $('#txtProcedureDescription').attr("disabled", true);
                $('#txtProcedureNote').attr("disabled", true);
             


            } catch (e) {
                alert(e.message);
            }
            $('#procedureflag').val('ProcedureTab');
            $("#proc-remove-class").removeClass("ui-state-highlight");
            $( "#proc-remove-class" ).empty();
            $("#date").removeClass("ui-state-error");
            $("#addProcedures-form").dialog('option', 'title', 'Procedures Details');
            $("#addProcedures-form").dialog("open");
        }
        function DeleteProcedure(id)
        {
         
            ShowLoader();
            try {
               

               
                var requestData = {
                    PatProcedureCntr: id
                   
                };

                $.ajax({
                    type: 'POST',
                    url: 'Procedure-delete',
                    data: JSON.stringify(requestData),
                    dataType: 'json',
                    contentType: 'application/json; charset=utf-8',
                    success: function (data) {
                        // if success
                        // alert("Success : " + data);

                        $("#Procdure-portlet-tab").html(data.html1);
                        $("#tab-Immunization").fixedHeaderTable();
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

        function DeletePOC(id)
        {
         
            ShowLoader();
            try {
               

                var requestData = {
                    PlanCntr: id,
                    
                   
                };

                $.ajax({
                    type: 'POST',
                    url: 'POC-delete',
                    data: JSON.stringify(requestData),
                    dataType: 'json',
                    contentType: 'application/json; charset=utf-8',
                    success: function (data) {
                        // if success
                        // alert("Success : " + data);

                        $("#PlanOfCare-portlet-tab").html(data.html1);
                        $("#tab-PlanOfCare").fixedHeaderTable();
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
       function DeleteClinicalInstruction(id)
        {
            ShowLoader();
            try {
               

              
                var requestData = {
                    PlanCntr: id,
                   
                };

                $.ajax({
                    type: 'POST',
                    url: 'ClinicalInstruction-delete',
                    data: JSON.stringify(requestData),
                    dataType: 'json',
                    contentType: 'application/json; charset=utf-8',
                    success: function (data) {
                        // if success
                        // alert("Success : " + data);
                      
                        $("#ClinicalInstructions-portlet-tab").html(data);

                        
                        $("#tab-ClinicalInstruction").fixedHeaderTable();
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

        function EditPOC(id) {

            try {
               
                var obj = jQuery.parseJSON($("#Plan-" + id).val());
                $('#PlanCntr').val(id);
               
                $('#txttype').val($.trim(obj.InstructionTypeId));
                $('#Comments').val($.trim(obj.Note));
                var AppDate = obj.AppointmentDateTime;
                if(AppDate == "1/1/1900 00:00:00" || AppDate == "1/1/1900 12:00:00 AM")
                {
                    $('#Planneddt').val("");
                }
                else
                {
                    $('#Planneddt').val($.trim(obj.AppointmentDateTime));
                }
               // $('#Planneddt').val($.trim(obj.AppointmentDateTime));
                $('#Instructions').val($.trim(obj.Instruction));
                $('#Goals').val($.trim(obj.Goal));
            

                $('#txttype').attr("disabled", false);
                $('#Comments').attr("disabled", false);
                $('#Planneddt').attr("disabled", false);
                $('#Instructions').attr("disabled", false);
                $('#Goals').attr("disabled", false);
                //alert($('#Goals').val());


            } catch (e) {
                alert(e.message);
            }
            $("#poc-remove-class").removeClass("ui-state-highlight");
            $( "#poc-remove-class" ).empty();
            $("#Planneddt").removeClass("ui-state-error");
            
            $("#addPOC-form").dialog('option', 'title', 'Edit Plan Of Care');
            $("#addPOC-form").dialog("open");
        }


        function DetailPOC(id)
        {
            try {
               
                var obj = jQuery.parseJSON($("#Plan-" + id).val());
                $('#PlanCntr').val(id);
               
                $('#txttype').val($.trim(obj.InstructionTypeId));
                $('#Comments').val($.trim(obj.Note));
                var AppDate = obj.AppointmentDateTime;
                if(AppDate == "1/1/1900 00:00:00" || AppDate == "1/1/1900 12:00:00 AM")
                {
                    $('#Planneddt').val("");
                }
                else
                {
                    $('#Planneddt').val($.trim(obj.AppointmentDateTime));
                }
               // $('#Planneddt').val($.trim(obj.AppointmentDateTime));
                $('#Instructions').val($.trim(obj.Instruction));
                $('#Goals').val($.trim(obj.Goal));
            

                $('#txttype').attr("disabled", true);
                $('#Comments').attr("disabled", true);
                $('#Planneddt').attr("disabled", true);
                $('#Instructions').attr("disabled", true);
                $('#Goals').attr("disabled", true);
              


            } catch (e) {
                alert(e.message);
            }
            $('#POCflag').val('POCTab');
            $("#poc-remove-class").removeClass("ui-state-highlight");
            $( "#poc-remove-class" ).empty();
            $("#Planneddt").removeClass("ui-state-error");
            $("#addPOC-form").dialog('option', 'title', 'Plan Of Care Details');
            $("#addPOC-form").dialog("open");
        
        }
        function EditClinicalInstruction(id) {

            try {
               
                var obj = jQuery.parseJSON($("#CI-" + id).val());
                $('#clinicalInstruction').val(id);
               
                $('#type').val($.trim(obj.InstructionTypeId));
                $('#txtComment').val($.trim(obj.Note));
                var AppDate = obj.AppointmentDateTime;
                if(AppDate == "1/1/1900 00:00:00" || AppDate == "1/1/1900 12:00:00 AM")
                {
                    $('#txtPlanneddt').val("");
                }
                else
                {
                    $('#txtPlanneddt').val($.trim(obj.AppointmentDateTime));
                }
                //$('#txtPlanneddt').val($.trim(obj.AppointmentDateTime));
                $('#txtInstruction').val($.trim(obj.Instruction));
                $('#txtGoals').val($.trim(obj.Goal));
            

                $('#type').attr("disabled", false);
                $('#txtComment').attr("disabled", false);
                $('#txtPlanneddt').attr("disabled", false);
                $('#txtInstruction').attr("disabled", false);
                $('#txtGoals').attr("disabled", false);
              //  alert($('#txtGoals').val());


            } catch (e) {
                alert(e.message);
            }
           
            $("#clinical-remove-class").removeClass("ui-state-highlight");
            $( "#clinical-remove-class" ).empty();
            $("#txtPlanneddt").removeClass("ui-state-error");

            $("#addClinicalInstructions-form").dialog('option', 'title', 'Edit Clinical Instructions');
            $("#addClinicalInstructions-form").dialog("open");
        }

        function DetailClinicalInstruction(id)
        {
            try {
               
                var obj = jQuery.parseJSON($("#CI-" + id).val());
                $('#clinicalInstruction').val(id);
              
                $('#type').val($.trim(obj.Goal));
                $('#txtComment').val($.trim(obj.Note));
                var AppDate = obj.AppointmentDateTime;
                if(AppDate == "1/1/1900 00:00:00" || AppDate == "1/1/1900 12:00:00 AM")
                {
                    $('#txtPlanneddt').val("");
                }
                else
                {
                    $('#txtPlanneddt').val($.trim(obj.AppointmentDateTime));
                }
                //$('#txtPlanneddt').val($.trim(obj.AppointmentDateTime));
                $('#txtInstruction').val($.trim(obj.Instruction));
                $('#txtGoals').val($.trim(obj.Goal));
            

                $('#type').attr("disabled", true);
                $('#txtComment').attr("disabled", true);
                $('#txtPlanneddt').attr("disabled", true);
                $('#txtInstruction').attr("disabled", true);
                $('#txtGoals').attr("disabled", true);
              


            } catch (e) {
                alert(e.message);
            }
            $('#clinicalInstructionflag').val('clinicalInstructionflag');
            $("#clinical-remove-class").removeClass("ui-state-highlight");
            $( "#clinical-remove-class" ).empty();
            $("#txtPlanneddt").removeClass("ui-state-error");
            $("#addClinicalInstructions-form").dialog('option', 'title', 'Clinical Instructions Details');
            $("#addClinicalInstructions-form").dialog("open");
            $('.dialog').dialogButtons().css('color','red');
        
        }
        function allergyDetails(id) {
            try {

                var obj = jQuery.parseJSON($("#" + id).val());
                $('#txtallergyID').val(id);
                $('#dtEffectiveDate').val($.trim(obj.EffectiveDate));
                $('#txtAllergenType').val($.trim(obj.AllergenType));
                $('#txtAllergen').val($.trim(obj.Allergen));
                $('#txtReaction').val($.trim(obj.Reaction));
                $('#txtNote').val($.trim(obj.Note));
                $('#cmbConditionStatus').val($.trim(obj.ConditionStatusId));

                var year = obj.EffectiveDate[0];
                year += obj.EffectiveDate[1];
                year += obj.EffectiveDate[2];
                year += obj.EffectiveDate[3];
                var mon = obj.EffectiveDate[4];
                mon += obj.EffectiveDate[5];
                var date = obj.EffectiveDate[6];
                date += obj.EffectiveDate[7];

                $('#txtAllergiesDate_Month').val(mon);
                $('#txtAllergiesDate_Day').val(date);
                $('#txtAllergiesDate_Year').val(year);

                $('#dtEffectiveDate').attr("disabled", true);
                $('#txtAllergenType').attr("disabled", true);
                $('#txtAllergiesDate_Month').attr("disabled", true);
                $('#txtAllergiesDate_Day').attr("disabled", true);
                $('#txtAllergiesDate_Year').attr("disabled", true);
                $('#txtAllergen').attr("disabled", true);
                $('#txtReaction').attr("disabled", true);
                $('#txtNote').attr("disabled", true);
                $('#cmbConditionStatus').attr("disabled", true);
            } catch (e) {
                alert(e.message);
            }
            $('#hdFlag').val('allergyTab');
            $("#addallergies-form").dialog('option', 'title', 'Allergy Details');
            $("#addallergies-form").dialog("open");
        }

        function allergyDelete(id) {

            ShowLoader();
            try {
                $('#txtallergyID').val(id);
                var requestData = {
                    PatientAllergyCntr: $.trim($('#txtallergyID').val())
                };

                $.ajax({
                    type: 'POST',
                    url: 'allergies-delete',
                    data: JSON.stringify(requestData),
                    dataType: 'json',
                    contentType: 'application/json; charset=utf-8',
                    success: function (data) {
                        // if success
                        // alert("Success : " + data);
                        $("#allergies-portlet-tab").html(data);
                        $("#tab-Allergies").fixedHeaderTable();
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
        function visitDetails(id) {

            try {
                var obj = jQuery.parseJSON($("#" + id).val());
                $('#txtvisitID').val(id);
                $('#txtProvider').val($.trim(obj.ProviderName));

                $('#txtLocation').val($.trim(obj.FacilityName));
                $('#txtReasonForVisits').val($.trim(obj.VisitReason));
                $('#dtDate').val($.trim(obj.VisitDate));

                $('#txtProvider').attr("disabled", true);

                $('#txtLocation').attr("disabled", true);
                $('#txtReasonForVisits').attr("disabled", true);
                $('#dtDate').attr("disabled", true);

            } catch (e) {
                alert(e.message);
            }
            $('#hdFlag').val('visitsTab');

            $("#addvisits-form").dialog('option', 'title', 'Visit Details');
            $("#addvisits-form").dialog("open");
        }
        function immunizationEdit(id) {

            try {
                var obj = jQuery.parseJSON($("#" + id).val());
                var expDate = $.trim(obj.ExpirationDate);
                var immDate = $.trim(obj.ImmunizationDate);
                if (expDate == "1/1/0001") { expDate = ""; }
                if (immDate == "01/01/0001") { immDate = ""; }

                $('#hdImmunizationText').val('');

                $('#txtimmunizationID').val(id);
                $('#hdImmunization').val(id);
                //   $('#hdImmunizationText').val(obj.Vaccine);
                $('#txtImmunizationDate').val(immDate);
                $('#txtImmunization').val($.trim(obj.Vaccine));
                $('#cmbImmunizationTime').val($.trim(obj.Time));
                $('#txtAmount').val($.trim(obj.Amount));
                $('#txtImmunizationNote').val($.trim(obj.Note));
                $('#txtImmunizationRoute').val($.trim(obj.Route));
                $('#txtImmunizationSite').val($.trim(obj.Site));
                $('#txtImmunizationSeq').val($.trim(obj.Sequence));
                $('#txtImmunizationEXDate').val(expDate);
                $('#txtImmunizationLotNo').val($.trim(obj.LotNumber));
                $('#txtImmunizationMu').val($.trim(obj.Manufacturer));

                $("#show-date-imm").show();
                $("#show-date-imm-exp").show();
                $('#txtImmunizationDate').attr("disabled", false);
                $('#txtImmunization').attr("disabled", false);
                $('#cmbImmunizationTime').attr("disabled", false);
                $('#txtAmount').attr("disabled", false);
                $('#txtImmunizationNote').attr("disabled", false);
                $('#txtImmunizationRoute').attr("disabled", false);
                $('#txtImmunizationSite').attr("disabled", false);
                $('#txtImmunizationSeq').attr("disabled", false);
                $('#txtImmunizationEXDate').attr("disabled", false);
                $('#txtImmunizationLotNo').attr("disabled", false);
                $('#txtImmunizationMu').attr("disabled", false);



            } catch (e) {
                alert(e.message);
            }
            $('#hdFlag').val('immunizationTab');
            $("#addimmunizations-form").dialog('option', 'title', 'Edit Immunization');
            $("#addimmunizations-form").dialog("open");
        }
        function immunizationDetails(id) {

            try {
                var obj = jQuery.parseJSON($("#" + id).val());
                var expDate = $.trim(obj.ExpirationDate);
                var immDate = $.trim(obj.ImmunizationDate);
                if (expDate == "1/1/0001") { expDate = ""; }
                if (immDate == "01/01/0001") { immDate = ""; }
                $('#hdImmunizationText').val('');
                $('#txtimmunizationID').val(id);
                $('#hdImmunization').val(id);
                //  $('#hdImmunizationText').val(obj.Vaccine);
                $('#txtImmunizationDate').val(immDate);
                $('#txtImmunization').val($.trim(obj.Vaccine));
                $('#cmbImmunizationTime').val($.trim(obj.Time));
                $('#txtAmount').val($.trim(obj.Amount));
                $('#txtImmunizationNote').val($.trim(obj.Note));
                $('#txtImmunizationRoute').val($.trim(obj.Route));
                $('#txtImmunizationSite').val($.trim(obj.Site));
                $('#txtImmunizationSeq').val($.trim(obj.Sequence));
                $('#txtImmunizationEXDate').val(expDate);
                $('#txtImmunizationLotNo').val($.trim(obj.LotNumber));
                $('#txtImmunizationMu').val($.trim(obj.Manufacturer));

                
                $("#show-date-imm").hide();
                $("#show-date-imm-exp").hide();
                $('#show-date-imm').attr("disabled", true);
                $('#txtImmunizationDate').attr("disabled", true);
                $('#txtImmunization').attr("disabled", true);
                $('#cmbImmunizationTime').attr("disabled", true);
                $('#txtAmount').attr("disabled", true);
                $('#txtImmunizationNote').attr("disabled", true);
                $('#txtImmunizationRoute').attr("disabled", true);
                $('#txtImmunizationSite').attr("disabled", true);
                $('#txtImmunizationSeq').attr("disabled", true);
                $('#txtImmunizationEXDate').attr("disabled", true);
                $('#txtImmunizationLotNo').attr("disabled", true);
                $('#txtImmunizationMu').attr("disabled", true);


            } catch (e) {
                alert(e.message);
            }
            $('#hdFlag').val('immunizationTab');
            $("#addimmunizations-form").dialog('option', 'title', 'Immunization Details');
            $("#addimmunizations-form").dialog("open");
        }
        function immunizationDelete(id) {

            ShowLoader();
            try {
                $('#txtimmunizationID').val(id);


                var requestData = {
                    PatientImmunizationCntr: $.trim($('#txtimmunizationID').val())

                };

                $.ajax({
                    type: 'POST',
                    url: 'immunizations-delete',
                    data: JSON.stringify(requestData),
                    dataType: 'json',
                    contentType: 'application/json; charset=utf-8',
                    success: function (data) {
                        // if success
                        // alert("Success : " + data);

                        $("#immunization-portlet-tab").html(data);
                        $("#tab-Immunization").fixedHeaderTable();
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

        function problemEdit(id) {


            try {

                var obj = jQuery.parseJSON($("#" + id).val());

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
            } catch (e) {
                alert(e.message);
            }
            $('#hdFlag').val('problemTab');
            $("#addproblems-form").dialog('option', 'title', 'Edit Problem');
            $("#addproblems-form").dialog("open");
        }

        function problemDetails(id) {

            try {
                var obj = jQuery.parseJSON($("#" + id).val());
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
            $("#addproblems-form").dialog('option', 'title', 'Details Problem');
            $("#addproblems-form").dialog("open");
        }
        function problemDelete(id) {


            ShowLoader();
            try {
                $('#txtproblemID').val(id);
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

                        $("#problem-portlet").html(data[0]);

                        $("#problem-portlet-tab").html(data[1]);
                        $("#tab-Problems").fixedHeaderTable();
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

        function vitalEdit(id) {

            try {
                var obj = jQuery.parseJSON($("#HiddenVitals" + id).val());
               



                $('#txtvitalID').val(id);
                $('#dtObservationDate').val($.trim(obj.VitalDate));
                $('#txtBloodPressure').val($.trim(obj.BloodPressurePosn));
                $('#txtBloodPressure1').val($.trim(obj.BloodPressurePosn1));
                $('#txtTemperature').val($.trim(obj.Temperature));
                $('#txtWeightLb').val($.trim(obj.WeightLb));
                $('#txtHeight').val($.trim(obj.HeightFt));
                $('#txtHeightinch').val($.trim(obj.HeightIn));
                $('#txtSystolic').val($.trim(obj.Systolic));
                $('#txtDiastolic').val($.trim(obj.Diastolic));
                $('#txtPulse').val($.trim(obj.Pulse));
                $('#txtRespiration').val($.trim(obj.Respiration));

                var height = parseInt($.trim(obj.HeightFt)) * 12;
                var height1 = $.trim(obj.HeightIn);
                var heightInch = parseInt(height) + parseInt(height1);
                var weight = parseFloat($.trim(obj.WeightLb));
                var result = weight / (heightInch * heightInch) * 703;

                if (isNaN(result))
                    $('#result').text("");
                else
                    $('#result').text(result.toFixed(1));
                $("#show-date-vit").show();
                $('#txtTemperature').attr("disabled", false);
                $('#txtHeightinch').attr("disabled", false);
                $('#txtBloodPressure1').attr("disabled", false);
                $('#dtObservationDate').attr("disabled", false);
                $('#txtBloodPressure').attr("disabled", false);
                $('#txtWeightLb').attr("disabled", false);
                $('#txtHeight').attr("disabled", false);
                $('#txtSystolic').attr("disabled", false);
                $('#txtDiastolic').attr("disabled", false);
                $('#txtPulse').attr("disabled", false);
                $('#txtRespiration').attr("disabled", false);


            } catch (e) {
                alert(e.message);
            }
            $('#hdFlag').val('vitalTab');
            $("#addvitals-form").dialog('option', 'title', 'Edit Vitals');
            $("#addvitals-form").dialog("open");
        }
        function vitalDetails(id) {


            try {
                var obj = jQuery.parseJSON($("#HiddenVitals" + id).val());
                
                $('#txtvitalID').val(id);
                $('#txtTemperature').val($.trim(obj.Temperature));
                $('#dtObservationDate').val($.trim(obj.VitalDate));
                $('#txtBloodPressure').val($.trim(obj.BloodPressurePosn));
                $('#txtBloodPressure1').val($.trim(obj.BloodPressurePosn1));
                $("#show-date-vit").hide();
                $('#txtWeightLb').val($.trim(obj.WeightLb));
                $('#txtHeight').val($.trim(obj.HeightFt));
                $('#txtHeightinch').val($.trim(obj.HeightIn));
                $('#txtSystolic').val($.trim(obj.Systolic));
                $('#txtDiastolic').val($.trim(obj.Diastolic));
                $('#txtPulse').val($.trim(obj.Pulse));
                $('#txtRespiration').val($.trim(obj.Respiration));

                var height = parseInt($.trim(obj.HeightFt)) * 12;
                var height1 = $.trim(obj.HeightIn);
                var heightInch = parseInt(height) + parseInt(height1);
                var weight = parseFloat($.trim(obj.WeightLb));
                var result = weight / (heightInch * heightInch) * 703;

                if (isNaN(result))
                    $('#result').text("");
                else
                    $('#result').text(result.toFixed(1));

                $('#txtTemperature').attr("disabled", true);
                $('#dtObservationDate').attr("disabled", true);
                $('#txtBloodPressure').attr("disabled", true);
                $('#txtBloodPressure1').attr("disabled", true);
                $('#txtWeightLb').attr("disabled", true);
                $('#txtHeight').attr("disabled", true);
                $('#txtHeightinch').attr("disabled", true);
                $('#txtSystolic').attr("disabled", true);
                $('#txtDiastolic').attr("disabled", true);
                $('#txtPulse').attr("disabled", true);
                $('#txtRespiration').attr("disabled", true);


            } catch (e) {
                alert(e.message);
            }
            $('#hdFlag').val('vitalTab');

            $("#addvitals-form").dialog('option', 'title', 'Details Vitals');
            $("#addvitals-form").dialog("open");
        }
        function vitalDelete(id) {
            ShowLoader();
            try {
                $('#txtvitalID').val(id);
                var requestData = {
                    PatientVitalCntr: $.trim($('#txtvitalID').val())
                };

                $.ajax({

                    type: 'POST',
                    url: 'vitals-delete',
                    data: JSON.stringify(requestData),
                    dataType: 'json',
                    contentType: 'application/json; charset=utf-8',
                    success: function (data) {
                        // if success
                        // alert("Success : " + data);
                        //   $("#vital-portlet-tab").html(data);

                        $("#vital-portlet").html(data[0]);
                        //  else if ($('#hdFlag').val() == 'vitalTab')
                        $("#vital-portlet-tab").html(data[1]);
                        $('#tab-vitals').fixedHeaderTable();
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













        //   $('#chkAdd').click(function () {
        function calling() {

            if ($('#chkAdd').is(":checked")) {

                $('#txtMAdd').val($('#txtAdd').val());
                $('#txtMAdd2').val($('#txtAdd2').val());
                $('#cmbMCountry').val($('#cmbCountry').val());
                $('#txtMCity').val($('#txtCity').val());
                $('#cmbMStates').val($('#cmbStates').val());
                $('#txtMZipcode').val($('#txtZipcode').val());
            }
            else {

                $('#txtMAdd').val("");
                $('#txtMAdd2').val("");
                $('#cmbMCountry').val('United States');
                $('#txtMCity').val('');
                $('#cmbMStates').val('AL');
                $('#txtMZipcode').val('');
            }
        }
        //      });











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
                        FacilityId: $('#cmbFacilityHome').val(),
                        //obj.FacilityId,
                        
                        VisitId:  $('#cmbVisitsHome').val(),
                        //obj.VisitId,
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






        //function pop_up_print_clinicalsummary(status) {

        //    var page1 = '';
          
        //    page1 = '@Url.Action("ClinicalSummaryPrint", "ClinicalSummary")';
        //    $('#clinical-print').remove();
        //    var $dialog = $('<div id="clinical-print" style="width:1000px;"></div>')
        //                   .html('<div id="page_loader1" style="display: block;"><div id="apDiv1" align="center"><table width="200" border="0" cellspacing="0" cellpadding="0"><tr><td height="70px;">&nbsp;</td></tr><tr><td align="center"><img src="Content/img/image_559926.gif" width="128" height="64" /></td></tr><tr><td style="text-align: center;" height="70px;">Please wait ...</td></tr></table></div></div><iframe style="border: 0px; " src="' + page1 + '" width="100%" height="100%"></iframe>')
        //                   .dialog({
        //                       autoOpen: false,
        //                       show: {
        //                           effect: "blind",
        //                           duration: 1000
        //                       },
        //                       hide: {
        //                           effect: "blind",
        //                           duration: 1000
        //                       },
        //                       modal: true,
        //                       height: 600,
        //                       width: 1050,
        //                       title: "Print Clinical Summary"
        //                   });
        //    $dialog.dialog('open');

        //}



        function MoveOn_ChartSumamry() {
            $("#tabs").tabs({
                active: 0
            });

        }






        function MoveOn_LabTests() {
            $("#tabs").tabs({
                active: 3
            });

        }


        function MoveOn_Visits() {
            $("#tabs").tabs({
                active: 2
            });

        }





        function MoveOn_Problems() {
            $("#tabs").tabs({
                active: 7
            });

        }


        function MoveOn_Vitals() {
            $("#tabs").tabs({
                active: 8
            });

        }



        function MoveOn_SocialHistory() {
            $("#tabs").tabs({
                active: 4
            });

        }



        function MoveOn_FamilyHistory() {
            $("#tabs").tabs({
                active: 4
            });

        }

        function MoveOn_PastMedicalHistory() {
            $("#tabs").tabs({
                active: 4
            });

        }


        function MoveOn_Immunizations() {
            $("#tabs").tabs({
                active: 6
            });



        }

        function MoveOn_allergies() {
            $("#tabs").tabs({
                active: 5
            });



        }
        function MoveOn_Procedure() {
            //alert("hi");
            $("#tabs").tabs({
                active: 11
            });

        }

        function MoveOn_ClinincalInstruction() {
            //alert("hi");
            $("#tabs").tabs({
                active: 13
            });

        }
        function MoveOn_POC() {
            //alert("hi");
            $("#tabs").tabs({
                active: 12
            });

        }
        function MoveOn_Insurance() {
            //alert("hi");
            $("#tabs").tabs({
                active: 10
            });

        }

        function MoveOn_Documents() {
            $("#tabs").tabs({
                active: 9
            });



        }

    

    <!--Start of modal popup of results-->
   


        $(function () {

            $("#dialog-form-LabTest").dialog
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
                width: 300,
                modal: true,
                buttons: {

                    Close: function () {
                        $(this).dialog("close");
                    }

                }

            });


            $("#LabTestResult1")
            .button()
            .click(function () {
                $("#dialog-form-LabTest").dialog("open");
            });

            $("#LabTestResult2")
            .button()
            .click(function () {
                $("#dialog-form-LabTest").dialog("open");
            });

        });

    

    <!-- end of modal pop up script of results -->
    <!-- applying script for modal pop up -->
   
        $(function () {
            var name = $("#field1"),
            email = $("#email"),
            password = $("#password"),
            allFields = $([]).add(name).add(email).add(password),
            tips = $(".validateTips");
            function updateTips(t) {
                tips
                .text(t)
                .addClass("ui-state-highlight");
                setTimeout(function () {
                    tips.removeClass("ui-state-highlight", 1500);
                }, 500);
            }
            function checkLength(o, n, min, max) {
                if (o.val().length > max || o.val().length < min) {
                    o.addClass("ui-state-error");
                    updateTips("Length of " + n + " must be between " +
                    min + " and " + max + ".");
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
                        bValid = bValid && checkLength(name, "username", 3, 16);
                        bValid = bValid && checkLength(email, "email", 6, 80);
                        bValid = bValid && checkLength(password, "password", 5, 16);
                        bValid = bValid && checkRegexp(name, /^[a-z]([0-9a-z_])+$/i, "Username may consist of a-z, 0-9, underscores, begin with a letter.");
                        // From jquery.validate.js (by joern), contributed by Scott Gonzalez: http://projects.scottsplayground.com/email_address_validation/

                        bValid = bValid && checkRegexp(password, /^([0-9a-zA-Z])+$/, "Password field only allow : a-z 0-9");
                        if (bValid) {
                            $("#users tbody").append("<tr>" +
                            "<td>" + name.val() + "</td>" +
                            "<td>" + email.val() + "</td>" +
                            "<td>" + password.val() + "</td>" +
                            "</tr>");
                            $(this).dialog("close");
                        }
                    },
                    Close: function () {
                        $(this).dialog("close");
                    }
                },
                close: function () {
                    allFields.val("").removeClass("ui-state-error");
                }
            });
            $("#createuser")
            .button()
            .click(function () {
                $("#dialog-form").dialog("open");
            });
        });


        $(function () {
            var name = $("#field1"),
            email = $("#email"),
            password = $("#password"),
            allFields = $([]).add(name).add(email).add(password),
            tips = $(".validateTips");
            function updateTips(t) {
                tips
                .text(t)
                .addClass("ui-state-highlight");
                setTimeout(function () {
                    tips.removeClass("ui-state-highlight", 1500);
                }, 500);
            }
            function checkLength(o, n, min, max) {
                if (o.val().length > max || o.val().length < min) {
                    o.addClass("ui-state-error");
                    updateTips("Length of " + n + " must be between " +
                    min + " and " + max + ".");
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
            $("#dialog-form1").dialog({
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
                        bValid = bValid && checkLength(name, "username", 3, 16);
                        bValid = bValid && checkLength(email, "email", 6, 80);
                        bValid = bValid && checkLength(password, "password", 5, 16);
                        bValid = bValid && checkRegexp(name, /^[a-z]([0-9a-z_])+$/i, "Username may consist of a-z, 0-9, underscores, begin with a letter.");
                        // From jquery.validate.js (by joern), contributed by Scott Gonzalez: http://projects.scottsplayground.com/email_address_validation/

                        bValid = bValid && checkRegexp(password, /^([0-9a-zA-Z])+$/, "Password field only allow : a-z 0-9");
                        if (bValid) {
                            $("#users tbody").append("<tr>" +
                            "<td>" + name.val() + "</td>" +
                            "<td>" + email.val() + "</td>" +
                            "<td>" + password.val() + "</td>" +
                            "</tr>");
                            $(this).dialog("close");
                        }
                    },
                    Close: function () {
                        $(this).dialog("close");
                    }
                },
                close: function () {
                    allFields.val("").removeClass("ui-state-error");
                }
            });
            $("#createuser1")
            .button()
            .click(function () {
                $("#dialog-form1").dialog("open");
            });
        });


        $(function () {
            var name = $("#field1"),
            email = $("#email"),
            password = $("#password"),
            allFields = $([]).add(name).add(email).add(password),
            tips = $(".validateTips");
            function updateTips(t) {
                tips
                .text(t)
                .addClass("ui-state-highlight");
                setTimeout(function () {
                    tips.removeClass("ui-state-highlight", 1500);
                }, 500);
            }
            function checkLength(o, n, min, max) {
                if (o.val().length > max || o.val().length < min) {
                    o.addClass("ui-state-error");
                    updateTips("Length of " + n + " must be between " +
                    min + " and " + max + ".");
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
            $("#dialog-form2").dialog({
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
                        bValid = bValid && checkLength(name, "username", 3, 16);
                        bValid = bValid && checkLength(email, "email", 6, 80);
                        bValid = bValid && checkLength(password, "password", 5, 16);
                        bValid = bValid && checkRegexp(name, /^[a-z]([0-9a-z_])+$/i, "Username may consist of a-z, 0-9, underscores, begin with a letter.");
                        // From jquery.validate.js (by joern), contributed by Scott Gonzalez: http://projects.scottsplayground.com/email_address_validation/

                        bValid = bValid && checkRegexp(password, /^([0-9a-zA-Z])+$/, "Password field only allow : a-z 0-9");
                        if (bValid) {
                            $("#users tbody").append("<tr>" +
                            "<td>" + name.val() + "</td>" +
                            "<td>" + email.val() + "</td>" +
                            "<td>" + password.val() + "</td>" +
                            "</tr>");
                            $(this).dialog("close");
                        }
                    },
                    Close: function () {
                        $(this).dialog("close");
                    }
                },
                close: function () {
                    allFields.val("").removeClass("ui-state-error");
                }
            });
            $("#createuser2")
            .button()
            .click(function () {
                $("#dialog-form2").dialog("open");
            });
        });

        // start of demographics pop up script

        $(function () {
            var name = $("#field1"),
            email = $("#email"),
            password = $("#password"),
            allFields = $([]).add(name).add(email).add(password),
            tips = $(".validateTips");
            function updateTips(t) {
                tips
                .text(t)
                .addClass("ui-state-highlight");
                setTimeout(function () {
                    tips.removeClass("ui-state-highlight", 1500);
                }, 500);
            }
            function checkLength(o, n, min, max) {
                if (o.val().length > max || o.val().length < min) {
                    o.addClass("ui-state-error");
                    updateTips("Length of " + n + " must be between " +
                    min + " and " + max + ".");
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
            $("#demographics-form").dialog({
                autoOpen: false,
                show: {
                    effect: "blind",
                    duration: 1000
                },
                hide: {
                    effect: "blind",
                    duration: 1000
                },

                width: 1000,
                modal: true,
                closeOnEscape: false,
                buttons: {
                    "Save": function () {

                        var bValid = true;                        

                        if ($('#txtFname').val().length < 1) {
                            alert('Please Enter Your First Name');
                            var bValid = false;
                            $('#txtFname').focus();
                        }

                        if ($('#txtLname').val().length < 1) {
                            alert('Please Enter Your Last Name');
                            var bValid = false;
                            $('#txtLname').focus();
                        }

                        if ($('#txtAdd').val().length < 1 || $('#txtAdd').val().length >= 50) {
                            alert('Address Must be Greater than 0 Characters and less than 50 Characters');
                            var bValid = false;
                            $('#txtAdd').focus();
                        }

                        if ($('#txtCity').val().length < 1 || $('#txtCity').val().length >= 30) {
                            alert('City Must be Greater than 0 Characters and less than 30 Characters');
                            var bValid = false;
                            $('#txtCity').focus();
                        }

                        //if ($('#cmbStates').val().length < 1 || $('#cmbStates').val().length >= 10) {
                        //    alert('State Must be Greater than 0 Characters and less than 10 Characters');
                        //    var bValid = false;
                        //    $('#cmbStates').focus();
                        //}
                        
                        if ($('#txtZipcode').val().length < 5) {
                            alert('Please Enter a Valid Zip Code');
                            var bValid = false;
                            $('#txtZipcode').focus();
                        }

                        if ($('#txthomphon').val().length < 1 || $('#txthomphon').val().length > 19) {
                            alert('Please Enter a Valid Home Phone');
                            var bValid = false;
                            $('#txthomphon').focus();
                        }

                        if ($('#txtemail').val().length < 1 || $('#txtemail').val().length >= 60) {
                            alert('Please Enter a Valid Email Address not more than 60 Characters');
                            var bValid = false;
                            $('#txthomphon').focus;
                        }

                        

                        allFields.removeClass("ui-state-error");

                        if (bValid) {

                            try {
                               
                                var requestData = {
                                    FirstName: $.trim($('#txtFname').val()),
                                    MiddleName: $.trim($('#txtMname').val()),
                                    LastName: $.trim($('#txtLname').val()),
                                    Title: $.trim($('#cmbName').val()),
                                    GenderId: $.trim($('#cmbGenderDialog').val()),
                                    PreferredLanguageId: $.trim($('#cmbPreferredLanguageDialog').val()),
                                    EthnicityId: $.trim($('#cmbEthnicityDialog').val()),
                                    Address1: $.trim($('#txtAdd').val()),
                                    Address2: $.trim($('#txtAdd2').val()),
                                    CountryCode: $.trim($('#cmbCountry').val()),
                                    City: $.trim($('#txtCity').val()),
                                    State: $.trim($('#cmbStates').val()),
                                    Zip: $.trim($('#txtZipcode').val()),
                                    HomePhone: $.trim($('#txthomphon').val()),
                                    MobilePhone: $.trim($('#txtcellpho').val()),
                                    WorkPhone: $.trim($('#txtworkpho').val()),
                                    EMail: $.trim($('#txtemail').val()),
                                    DOB: $.trim($('#txtdob').val()),
                                    RaceId: $.trim($('#cmbRaceDialog').val()),
                                    SmokingStatusId: $.trim($('#cmbSmokingStatusDialog option:selected').val()),
                                    MailAddress1: $.trim($('#txtMAdd').val()),
                                    MailAddress2: $.trim($('#txtMAdd2').val()),
                                    MailCity: $.trim($('#txtMCity').val()),
                                    MailCountryCode: $.trim($('#cmbMCountry').val()),
                                    MailState: $.trim($('#cmbMStates').val()),
                                    MailPostalCode: $.trim($('#txtMZipcode').val()),
                                    RaceId_NotAnswered: $.trim($('#rdrcprefno').prop('checked')),
                                    RaceId_Native: $.trim($('#rdrccnat').prop('checked')),
                                    RaceId_Asian: $.trim($('#rdrcasn').prop('checked')),
                                    RaceId_Black: $.trim($('#rdrcblk').prop('checked')),
                                    RaceId_Hawaiian: $.trim($('#rdrchw').prop('checked')),
                                    RaceId_White: $.trim($('#rdrcwh').prop('checked')),
                                };


                                $.ajax({
                                    type: 'POST',
                                    url: 'clinical-summary-patientSummary-save',
                                    data: JSON.stringify(requestData),
                                    dataType: 'json',
                                    contentType: 'application/json; charset=utf-8',
                                    success: function (data) {
                                        // if success

                                        $("#ChartSummary").html(data);

                                        var obj = jQuery.parseJSON($("#demographicsdata").val());
                                        var name = obj.FirstName + ' ' + obj.LastName;
                                        $('#nav_panel h1').html(name);



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
                    Close: function () {
                        $(this).dialog("close");
                    }
                },
                close: function () {
                    allFields.val("").removeClass("ui-state-error");
                }
            });
        });

        //- end of demographics popup script

        // start of personal pop up script

        $(function () {

            tips = $(".validateTips");



            $("#personal-form").dialog({
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


                        if (bValid) {
                            try {
                                var requestData = {
                                    HeightFt: $.trim($('#cmbHeightFt').val()),
                                    HeightIn: $.trim($('#cmbHeightIn').val()),
                                    HairColor: $.trim($('#cmbHair').val()),
                                    EyeColor: $.trim($('#cmbEye').val()),
                                    BloodTypeId: $.trim($('#cmbBloodType').val()),
                                    ReligionId: $.trim($('#cmbReligion').val()),
                                    Comments: $.trim($('#txtComments').val()),
                                    Weight: $.trim($('#txtWeight').val()),
                                    OrganDoner: $('#rdyes').prop('checked'),
                                    BoneMarrow: $('#chkBone').prop('checked'),
                                    ConnectiveTissue: $('#chkTissue').prop('checked'),
                                    Cornea: $('#chkCornea').prop('checked'),
                                    Heart: $('#chkHeart').prop('checked'),
                                    HeartValves: $('#chkHeartVal').prop('checked'),
                                    Intestines: $('#chkIntestine').prop('checked'),
                                    Kidneys: $('#chkKidney').prop('checked'),
                                    Liver: $('#chkLiver').prop('checked'),
                                    Lungs: $('#chkLungs').prop('checked'),
                                    Pancreas: $('#chkPan').prop('checked'),
                                    //PatientId: $.trim($('#txtMAdd').val()),
                                    Skin: $('#chkSkin').prop('checked')

                                };


                                $.ajax({
                                    type: 'POST',
                                    url: 'clinical-summary-patientAndOrganData-save',
                                    data: JSON.stringify(requestData),
                                    dataType: 'json',
                                    contentType: 'application/json; charset=utf-8',
                                    success: function (data) {
                                        // if success

                                        $("#ChartSummary").html(data);

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
                    Close: function () {
                        $(this).dialog("close");
                    }
                },
                close: function () {

                }
            });
        });

        //- end of personal popup script

        function editPersonal() {


            var obj = jQuery.parseJSON($("#objPersonal").val());
            $('#cmbHeightFt').val($.trim(obj.HeightFt));
            $('#cmbHeightIn').val($.trim(obj.HeightIn));
            $('#txtWeight').val($.trim(obj.Weight));
            $('#cmbHair').val($.trim(obj.HairColor));
            $('#cmbEye').val($.trim(obj.EyeColor));
            $('#cmbBloodType').val($.trim(obj.BloodTypeId));
            $('#cmbReligion').val($.trim(obj.ReligionId));
            $('#txtComments').val($.trim(obj.Comments));
            $('#txtBMI').val($.trim(obj.BMI));

            if (obj.OrganDoner.toUpperCase() == 'YES') {
                $("#rdyes").prop("checked", true)
            }
            else {
                $("#rdNo").prop("checked", true)
            }

            function castStrToBool(str) {
                if (str.toLowerCase() == 'false') {
                    return false;
                } else if (str.toLowerCase() == 'true') {
                    return true;
                } else {
                    return undefined;
                }
            }

            $("#chkPan").prop("checked", castStrToBool(obj.Pancreas));
            $("#chkBone").prop("checked", castStrToBool(obj.BoneMarrow));
            $("#chkHeartVal").prop("checked", castStrToBool(obj.HeartValves));
            $("#chkLungs").prop("checked", castStrToBool(obj.Lungs));
            $("#chkLiver").prop("checked", castStrToBool(obj.Liver));
            $("#chkCornea").prop("checked", castStrToBool(obj.Cornea));
            $("#chkSkin").prop("checked", castStrToBool(obj.Skin));
            $("#chkTissue").prop("checked", castStrToBool(obj.ConnectiveTissue));
            $("#chkHeart").prop("checked", castStrToBool(obj.Heart));
            $("#chkKidney").prop("checked", castStrToBool(obj.Kidneys));
            $("#chkIntestine").prop("checked", castStrToBool(obj.Intestines));

            if (obj.Pancreas.toUpperCase() == 'TRUE' && obj.BoneMarrow.toUpperCase() == 'TRUE' && obj.HeartValves.toUpperCase() == 'TRUE' && obj.Lungs.toUpperCase() == 'TRUE'
                && obj.Liver.toUpperCase() == 'TRUE' && obj.Cornea.toUpperCase() == 'TRUE' && obj.Skin.toUpperCase() == 'TRUE' && obj.ConnectiveTissue.toUpperCase() == 'TRUE'
                && obj.Heart.toUpperCase() == 'TRUE' && obj.Kidneys.toUpperCase() == 'TRUE' && obj.Intestines.toUpperCase() == 'TRUE') {
                $('#chkOrgYes').prop("checked", true)
            }
            else {
                $('#chkOrgNo').prop("checked", true)
            }

            var height = $('#cmbHeightFt').val() * 12;
            var height1 = $("#cmbHeightIn").val();
            var heightInch = parseInt(height) + parseInt(height1);
            var weight = $('#txtWeight').val();
            var BMI = weight / (heightInch * heightInch) * 703;
            $('#txtBMI').val(BMI.toFixed(1));
            $("#personal-form").dialog('option', 'title', 'Edit Personal');
            $('#personal-form').dialog("open");

        }
        // start of emergency pop up script

        $(function () {
            var name = $("#field1"),
            email = $("#email"),
            password = $("#password"),
            allFields = $([]).add(name).add(email).add(password),
            tips = $(".validateTips");
            function updateTips(t) {
                tips
                .text(t)
                .addClass("ui-state-highlight");
                setTimeout(function () {
                    tips.removeClass("ui-state-highlight", 1500);
                }, 500);
            }
            function checkLength(o, n, min, max) {
                if (o.val().length > max || o.val().length < min) {
                    o.addClass("ui-state-error");
                    updateTips("Length of " + n + " must be between " +
                    min + " and " + max + ".");
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
            $("#emergency-form").dialog({
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

                        if ($('#txtEname').val().length < 1 )
                        {
                            alert('Please Enter a Name');
                            var bValid = false;
                            $('#txtEname').focus();
                        }

                        if ($('#txtEAdd').val().length < 1 || $('#txtEAdd').val().length >= 50)
                        {
                            alert('Address Must be Greater than 0 Characters and less than 50 Characters');
                            var bValid = false;
                            $('#txtEAdd').focus();
                        }

                        if ($('#txtECity').val().length < 1 || $('#txtECity').val().length >=30)
                        {
                            alert('City Must be Greater than 0 Characters and less than 50 Characters');
                            var bValid = false;
                            $('#txtECity').focus();
                        }

                        if ($('#txtEZip').val().length < 5)
                        {
                            alert('Please Enter a Valid Zip Code');
                            var bValid = false;
                            $('#txtEZip').focus;
                        }

                        if ($('#cmbERel').val() == -1)
                        {
                            alert('Please Select a Relationship');
                            var bValid = false;
                            
                        }


                        
                        allFields.removeClass("ui-state-error");
                       

                        if (bValid) {

                            try {
                                //var IsPrimaryData =
                                var obj = jQuery.parseJSON($("#objEmergency").val());

                                var requestData = {
                                    PatientEmergencyId: obj.PatientEmergencyId,
                                    FirstName: $.trim($('#txtEname').val()),
                                    HomePhone: $.trim($('#txtEHomephone').val()),
                                    Address1: $.trim($('#txtEAdd').val()),
                                    City: $.trim($('#txtECity').val()),
                                    CountryCode: $.trim($('#cmbECountry').val()),
                                    //EmergencyCountryName: $.trim($('#cmbECountry').val()),
                                    MobilePhone: $.trim($('#txtEMobphone').val()),
                                    RelationshipId: $.trim($('#cmbERel').val()),
                                    //EmergencyRelationship: $.trim($('#cmbERel').val()),
                                    State: $.trim($('#cmbEStates').val()),
                                    //EmergencyWorkPhone: $.trim($('#cmbStates').val()),
                                    PostalCode: $.trim($('#txtEZip').val()),
                                    IsPrimary: $("#chkIsPrimary").is(":checked") ? "true" : "false",

                                };


                                $.ajax({
                                    type: 'POST',
                                    url: 'clinical-summary-patientEmergency-save',
                                    data: JSON.stringify(requestData),
                                    dataType: 'json',
                                    contentType: 'application/json; charset=utf-8',
                                    success: function (data) {
                                        // if success

                                        $("#ChartSummary").html(data);

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
                    Close: function () {
                        $(this).dialog("close");
                    }
                },
                close: function () {
                    allFields.val("").removeClass("ui-state-error");
                }
            });
        });

        //- end of emergency popup script






        // start of add visits pop up script

        $(function () {

            tips = $(".validateTips");



            $("#addvisits-form").dialog({
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
            $("#createaddvisits")
            .button()
            .click(function () {
                $("#addvisits-form").dialog("open");
            });
            $("#createaddvisits_tab")
           .button()
           .click(function () {
               $("#addvisits-form").dialog("open");
           });
        });

        //- end of add visits popup script

        // start of add labs pop up script

        $(function () {

            tips = $(".validateTips");



            $("#addlabs-form").dialog({
                autoOpen: false,
                show: {
                    effect: "blind",
                    duration: 1000
                },
                hide: {
                    effect: "blind",
                    duration: 1000
                },


                width: 500,
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
                                    url: 'clinical-summary-labs-save',
                                    data: JSON.stringify(requestData),
                                    dataType: 'json',
                                    contentType: 'application/json; charset=utf-8',
                                    success: function (data) {
                                        // if success
                                        // alert("Success : " + data);
                                        if ($('#hdFlag').val() == 'labWidg')
                                            $("#lab-portlet").html(data);
                                        else if ($('#hdFlag').val() == 'labTab')
                                            $("#lab-portlet-tab").html(data);


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
                $("#addlabs-form").dialog("open");
            });
            $("#createaddlab_tab")
           .button()
           .click(function () {
               $('#hdFlag').val('labTab');
               $("#addlabs-form").dialog("open");
           });

        });



        //- end of add labs popup script



        // start of add Medications pop up script

        $(function () {

            tips = $(".validateTips");
            var pharmacy = $("#txtPharmacy"),


               allFields = $([]).add(pharmacy),
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
            


            $("#addmedications-form").dialog({
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
                        bValid = bValid && checkLength(pharmacy, "Please Select pharmacy");
                        var DuringVisit=$("#DuringVist1").prop("checked");
                        alert(DuringVisit);
                        if (bValid) {
                            //ajax 
                            try {
                                //if (($('#txtPharmacy option:selected').val()) == -1) {
                                //    alert("Please Select ' Pharmacy ' option");
                                //    return false;
                                //}

                                var requestData = {
                                    Diagnosis: $.trim($('#txtDiagnosis').val()),
                                    StartDate: $.trim($('#dtStartDate').val()),
                                    Route: $.trim($('#txtRoute').val()),
                                    Frequency: $.trim($('#txtFrequency').val()),
                                    Refills: $.trim($('#txtRefills').val()),
                                    Quantity: $.trim($('#txtQty').val()),
                                    Days: $.trim($('#txtDays').val()),
                                    Note: $.trim($('#txtInstructions').val()),
                                    Pharmacy: $.trim($('#txtPharmacy option:selected').text()),
                                    Status: $.trim($('#cmbTakingMedicine').val()),
                                    ExpireDate: $.trim($('#datepicker').val()),
                                    duringvisit:DuringVisit,

                                    MedicationName: $.trim($('#txtMedicationName').val())
                                };


                                $.ajax({
                                    type: 'POST',
                                    url: 'clinical-summary-medications-save',
                                    data: JSON.stringify(requestData),
                                    dataType: 'json',
                                    contentType: 'application/json; charset=utf-8',
                                    success: function (data) {
                                        // if success
                                        // alert("Success : " + data);
                                        $("#medications-portlet").html(data);
                                        $("#tbl-Medications").fixedHeaderTable();


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
            $("#createaddmedications")
            .button()
            .click(function () {
                $("#addmedications-form").dialog("open");
            });

        });


        //- end of add medications popup script



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
                        //if ($.trim($('#txtSearch').val()) == '') {
                        //    alert("Please type or select a condition");
                        //    return;
                        //}
                        //else if ((($('#cmbConditionStatusProb option:selected').val()) == -1)) {
                        //    alert("Please Select Condition Status option");
                        //    return false;
                        //}
                        //else if ($('#txtProblemsDate_Year option:selected').val() == '--' && $('#txtProblemsDate_Day option:selected').val() != '--') {
                        //    alert('Please select Year or Month and Year or Month, Day and Year');
                        //    return false;
                        //}
                        //else if ($('#txtProblemsDate_Year option:selected').val() == '--' && $('#txtProblemsDate_Month option:selected').val() != '--') {
                        //    alert('Please select Year or Month and Year or Month, Day and Year');
                        //    return false;
                        //}
                        //else if (($('#txtProblemsLastDate_Month option:selected').val() == '--') && ($('#txtProblemsLastDate_Day option:selected').val() != '--')) {
                        //    alert('Please select Date Last change Month');
                        //    return false;
                        //}
                        // else if ($('#txtProblemsDate_Year option:selected').val() == '--')
                        //{
                        //     alert('Please select Year');
                        //return false;
                        //}
                        //else if ($(';#txtProblemsLastDate_Year option:selected').val() == '--') {
                        //    alert('Please select last change date Year');
                        //    return false;
                        //}

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
                                        // if success
                                        //   if ($('#hdFlag').val() == 'problemWidg')
                                        $("#problem-portlet").html(data[0]);
                                        $("#tbl-Problems").fixedHeaderTable();
                                        // else if ($('#hdFlag').val() == 'problemTab')
                                        $("#problem-portlet-tab").html(data[1]);
                                        $("#tab-Problems").fixedHeaderTable();
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
            $("#createaddproblems")
            .button()
            .click(function () {
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
                $("#addproblems-form").dialog("open");

            });
            $("#createaddproblems_tab")
           .button()
           .click(function () {
               $('#txtproblemID').val('0');
               $('#txtSearchID').val('0');
               $('#txtSearchText').val('');
               $('#txtSearch').attr("disabled", false);
               $('#txtProblemsDate').attr("disabled", false);
               $('#txtPlastDate').attr("disabled", false);
               $('#cmbConditionStatusProb').attr("disabled", false);
               $('#txtProblemsDescription').attr("disabled", false);
               $('#btnproblemSearch').attr("disabled", false);
               $('#txtProblemsDate_Month').attr("disabled", false);
               $('#txtProblemsDate_Day').attr("disabled", false);
               $('#txtProblemsDate_Year').attr("disabled", false);
               $('#txtProblemsLastDate_Month').attr("disabled", false);
               $('#txtProblemsLastDate_Day').attr("disabled", false);
               $('#txtProblemsLastDate_Year').attr("disabled", false);
               $('.ui-dialog-title').html("Add Problems");
               $("#addproblems-form").dialog("open");

               $('#hdFlag').val('problemTab');
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
                                    PatientAllergyCntr: $.trim($('#txtallergyID').val()),
                                    EffectiveDate: $.trim($('#txtAllergiesDate').val()),
                                    AllergenType: $.trim($('#txtAllergenType').val()),
                                    Allergen: $.trim($('#txtAllergen').val()),
                                    Reaction: $.trim($('#txtReaction').val()),
                                    Note: $.trim($('#txtNote').val()),
                                    ConditionStatusId: $.trim($('#cmbConditionStatus option:selected').val()),
                                    Flag: $.trim($('#hdFlag').val())
                                };


                                $.ajax({
                                    type: 'POST',
                                    url: 'clinical-summary-allergies-save',
                                    data: JSON.stringify(requestData),
                                    dataType: 'json',
                                    contentType: 'application/json; charset=utf-8',
                                    success: function (data) {
                                        //   if ($('#hdFlag').val() == 'allergyWidg')
                                        $("#allergies-portlet").html(data[0]);
                                        //  else if ($('#hdFlag').val() == 'allergyTab')
                                        $("#tbl-Allergies").fixedHeaderTable();
                                        $("#allergies-portlet-tab").html(data[1]);
                                        
                                        $("#tab-Allergies").fixedHeaderTable();
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
            $("#createaddallergies")
            .button()
            .click(function () {
                $('#txtallergyID ').val('0');
                $('#dtEffectiveDate').attr("disabled", false);
                $('#txtAllergenType').attr("disabled", false);
                $('#txtAllergen').attr("disabled", false);
                $('#txtReaction').attr("disabled", false);
                $('#txtNote').attr("disabled", false);
                $('#txtAllergiesDate_Month').attr("disabled", false);
                $('#txtAllergiesDate_Day').attr("disabled", false);
                $('#txtAllergiesDate_Year').attr("disabled", false);
                $('#cmbConditionStatus').attr("disabled", false);
                $('#hdFlag').val('allergyWidg');
                $("#addallergies-form").dialog("open");
            });
              $("#createaddallergies_tab")
           .button()
           .click(function () {
               $('#txtallergyID ').val('0');
               $('#dtEffectiveDate').attr("disabled", false);
               $('#txtAllergenType').attr("disabled", false);
               $('#txtAllergen').attr("disabled", false);
               $('#txtReaction').attr("disabled", false);
               $('#txtNote').attr("disabled", false);
               $('#txtAllergiesDate_Month').attr("disabled", false);
               $('#txtAllergiesDate_Day').attr("disabled", false);
               $('#txtAllergiesDate_Year').attr("disabled", false);
               $('#cmbConditionStatus').attr("disabled", false);
               $('#hdFlag').val('allergyTab');
               $("#addallergies-form").dialog("open");
           });
        });



        //- end of add Allergies popup script



        // start of add vitals pop up script

        $(function () {
            //var bValid = true;

            tips = $(".validateTips");
           

            $("#addvitals-form").dialog({
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

                        

                        
                        //allFields.removeClass("ui-state-error");
                        //bValid = bValid && checkLength(Status, "Please Select Status");

                        if (ValidateDate(dtVal)) {

                 

                            if (bValid) {
                                //ajax 
                                try {
                                    var ft_inch = $.trim($('#txtHeight').val()) * 12;
                                    var inch = $.trim($('#txtHeightinch').val());
                                    var height_inch = parseInt(ft_inch) + parseInt(inch);

                                    var requestData = {

                                        PatientVitalCntr: $.trim($('#txtvitalID').val()),
                                        VitalDate: $.trim($('#dtObservationDate').val()),
                                        BloodPressurePosn: $.trim($('#txtBloodPressure').val()) + "/" + $.trim($('#txtBloodPressure1').val()),
                                        WeightLb: $.trim($('#txtWeightLb').val()),
                                        HeightIn: height_inch,
                                        Systolic: $.trim($('#txtBloodPressure').val()),
                                        Diastolic: $.trim($('#txtBloodPressure1').val()),
                                        Temperature: $.trim($('#txtTemperature').val()),
                                        Pulse: $.trim($('#txtPulse').val()),
                                        Respiration: $.trim($('#txtRespiration').val()),
                                        Flag: $.trim($('#hdFlag').val())

                                    };

                                    


                                    $.ajax({
                                        type: 'POST',
                                        url: 'clinical-summary-vitals-save',
                                        data: JSON.stringify(requestData),
                                        dataType: 'json',
                                        contentType: 'application/json; charset=utf-8',
                                        success: function (data) {
                                            // if success
                                            // alert("Success : " + data);

                                            //  if ($('#hdFlag').val() == 'vitalWidg')
                                            $("#vital-portlet").html(data[0]);
                                            $("#tbl-VitalSigns").fixedHeaderTable();
                                            //  else if ($('#hdFlag').val() == 'vitalTab')
                                            $("#vital-portlet-tab").html(data[1]);
                                            $('#tab-vitals').fixedHeaderTable();
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


                                //  end ajax


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
            $("#createaddvitals")
            .button()
            .click(function () {
                $('#txtvitalID').val('0');
                $('#result').text('');
                $("#show-date-vit").show();
                $('#dtObservationDate').attr("disabled", false);
                $('#txtBloodPressure').attr("disabled", false);
                $('#txtWeightLb').attr("disabled", false);
                $('#txtHeight').attr("disabled", false);
                $('#txtSystolic').attr("disabled", false);
                $('#txtDiastolic').attr("disabled", false);
                $('#txtPulse').attr("disabled", false);
                $('#txtRespiration').attr("disabled", false);
                $('#txtBloodPressure1').attr("disabled", false);
                $('#txtHeightinch').attr("disabled", false);
                $('#txtTemperature').attr("disabled", false);
                $('#hdFlag').val('vitalWidg');
                $("#addvitals-form").dialog('option', 'title', 'Add Vitals');
                $("#addvitals-form").dialog("open");
            });
            $("#createaddvitals_tab")
            .button()
            .click(function () {
                $('#txtvitalID').val('0');
                $('#result').text('');
                $('#dtObservationDate').attr("disabled", false);
                $('#txtBloodPressure').attr("disabled", false);
                $('#txtWeightLb').attr("disabled", false);
                $('#txtHeight').attr("disabled", false);
                $('#txtSystolic').attr("disabled", false);
                $('#txtDiastolic').attr("disabled", false);
                $('#txtPulse').attr("disabled", false);
                $('#txtRespiration').attr("disabled", false);
                $('#txtBloodPressure1').attr("disabled", false);
                $('#txtHeightinch').attr("disabled", false);
                $('#txtTemperature').attr("disabled", false);
                $('#hdFlag').val('vitalTab');
                $("#addvitals-form").dialog('option', 'title', 'Add Vitals');
                $("#addvitals-form").dialog("open");
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

                            //ajax 
                            try {
                                //if (($('#cmbDescription option:selected').val()) == -1) {
                                //    alert("Please Select ' Description ' option");
                                //    return false;
                                //}


                                var requestData = {
                                    PatSocialHistCntr: $.trim($('#txtsocialID').val()),
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
                                    url: 'clinical-summary-socials-save',
                                    data: JSON.stringify(requestData),
                                    dataType: 'json',
                                    contentType: 'application/json; charset=utf-8',
                                    success: function (data) {
                                        // if success
                                        // alert("Success : " + data);

                                        //   if ($('#hdFlag').val() == 'socialWidg')
                                        $("#social-portlet").html(data[0]);
                                        $("#tbl-SocialHistory").fixedHeaderTable();
                                        //  else if ($('#hdFlag').val() == 'socialTab') {
                                        $('#social-portlet-tab').html(data[1]);
                                        $("#tab-SocialHistory").fixedHeaderTable();
                                        
                                        // }

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
            $("#createaddsocial_tab")
           .button()
           .click(function () {
               $('#txtsocialID').val('0');
               $('#hdFlag').val('socialTab');
               $('#cmbDescription').attr("disabled", false);
               $('#txtSocialValue').attr("disabled", false);
               $('#txtSocialBegin').attr("disabled", false);
               $('#txtSocialEnd').attr("disabled", false);
               $('#txtSocialNote').attr("disabled", false);
               $('#txtSocialEnd_Month').attr("disabled", false);
               $('#txtSocialEnd_Day').attr("disabled", false);
               $('#txtSocialEnd_Year').attr("disabled", false);
               $('#txtSocialBegin_Month').attr("disabled", false);
               $('#txtSocialBegin_Day').attr("disabled", false);
               $('#txtSocialBegin_Year').attr("disabled", false);
             
               $("#addsocial-form").dialog('option', 'title', 'Add Social History');
               $("#addsocial-form").dialog("open");
           });
            $("#createaddsocial")
          .button()
          .click(function () {
              $('#hdFlag').val('socialWidg');
              $('#txtsocialID').val('0');
              $("#addsocial-form").dialog('option', 'title', 'Add Social History');
              $("#addsocial-form").dialog("open");
          });

        });

        //- end of add social popup script



        // start of add family pop up script

        $(function () {

            tips = $(".validateTips");



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


                        if (bValid) {
                            //ajax 


                            try {
                                var strArr = $.trim($('#cmbRelationship  option:selected').val()).split("|");
                                if ((strArr[0] == -1) && (($('#cmbConditionStatusFam option:selected').val()) == -1)) {
                                    alert("Please Select ' Relationship and ConditionStatus ' option");
                                    return false;
                                }
                                else if ((($('#cmbConditionStatusFam option:selected').val()) == -1)) {
                                    alert("Please Select ' ConditionStatus ' option");
                                    return false;
                                }
                                else if ((strArr[0] == -1)) {
                                    alert("Please Select ' Relationship ' option");
                                    return false;
                                }

                                var codeVal;

                                codeVal = $.trim($('#hdFDescriptionId').val());
                                if ($.trim($('#hdFDescriptionText').val()) == "")
                                { codeVal = 0; }
                                var requestData = {
                                    PatFamilyHistCntr: $.trim($('#txtfamilyID').val()),
                                    //Description: $.trim($('#hdFDescriptionText').val()),
                                    Description: $.trim($('#txtDescription').val()),
                                    //   CodeValue: $.trim($('#hdFDescriptionId').val()),
                                    CodeValue: codeVal,
                                    Note: $.trim($('#txtFamilyNote').val()),
                                    RelationshipId: strArr[0],
                                    ConditionStatusId: $.trim($('#cmbConditionStatusFam').val()),
                                    AgeAtOnset: $.trim($('#txtAgeOnsetSpinner').val()),
                                    DiseasedAge: $.trim($('#txtAgeDiseaseSpinner').val()),
                                    Diseased: $('#cmbAlive option:selected').val(),
                                    Flag: $.trim($('#hdFlag').val())
                                };


                                $.ajax({
                                    type: 'POST',
                                    url: 'clinical-summary-families-save',
                                    data: JSON.stringify(requestData),
                                    dataType: 'json',
                                    contentType: 'application/json; charset=utf-8',
                                    success: function (data) {
                                        // if success
                                        // alert("Success : " + data);

                                        //  if ($('#hdFlag').val() == 'familyWidg')
                                        $("#family-portlet").html(data[0]);
                                        $("#tbl-FamilyHistory").fixedHeaderTable();
                                        // else if ($('#hdFlag').val() == 'familyTab')
                                        $("#family-portlet-tab").html(data[1]);
                                        $("#tab-FamilyHistory").fixedHeaderTable();

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
                    $(this).dialog("close");
                }
            });
            $("#createaddfamily")
            .button()
            .click(function () {
                $('#hdFlag').val('familyWidg');
                $('#txtfamilyID').val('0');
                $('#hdFDescriptionText').val('');
                $('#hdFDescriptionId').val('0');
                $('#cmbRelationship').attr("disabled", false);
                $('#txtDescription').attr("disabled", false);
                $('#txtFamilyNote').attr("disabled", false);
                $('#cmbAlive').attr("disabled", false);
                $('#cmbConditionStatusFam').attr("disabled", false);
                $("#txtAgeOnsetSpinner").spinner({ disabled: false });
                $("#txtAgeDiseaseSpinner").spinner({ disabled: false });
                $('.ui-dialog-title').html("Add Family History ");
                $("#addfamily-form").dialog("open");
            });
            $("#createaddfamily-tab")
          .button()
          .click(function () {
              $('#hdFlag').val('familyTab');
              $('#txtfamilyID').val('0');
              $('#hdFDescriptionText').val('');
              $('#hdFDescriptionId').val('0');
              $('#cmbRelationship').attr("disabled", false);
              $('#txtDescription').attr("disabled", false);
              $('#txtFamilyNote').attr("disabled", false);
              $('#cmbAlive').attr("disabled", false);
              $('#cmbConditionStatusFam').attr("disabled", false);
              $("#txtAgeOnsetSpinner").spinner({ disabled: false });
              $("#txtAgeDiseaseSpinner").spinner({ disabled: false });
              $('.ui-dialog-title').html("Add Family History ");
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
                                    PatMedicalHistCntr: $.trim($('#txtpastID').val()),
                                    DateOfOccurance: $.trim($('#dtDateOccurance').val()),
                                    Description: $.trim($('#txtPastDiagnosis').val()),
                                    Note: $.trim($('#txtPastNotes').val()),
                                    Flag: $.trim($('#hdFlag').val())


                                };


                                $.ajax({
                                    type: 'POST',
                                    url: 'clinical-summary-pasts-save',
                                    data: JSON.stringify(requestData),
                                    dataType: 'json',
                                    contentType: 'application/json; charset=utf-8',
                                    success: function (data) {
                                        // if success


                                        //   if ($('#hdFlag').val() == 'pastWidg')
                                        $("#pasthistory-portlet").html(data[0]);
                                        $("#tbl-PastMedications").fixedHeaderTable();
                                        // else if ($('#hdFlag').val() == 'pastTab')
                                        $("#pasthistory-portlet-tab").html(data[1]);
                                        $("#tab-MedicalHistory").fixedHeaderTable();
                                        $('#PastHistOcc').find('.fht-cell').css("width","84px");
                                        $('#PastHistDiagnose').find('.fht-cell').css("width","138px");
                                        $('#PastHistNote').find('.fht-cell').css("width","204px");

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

                $('#dtDateOccurance').attr("disabled", false);
                $('#txtPastDiagnosis').attr("disabled", false);
                $('#txtPastNotes').attr("disabled", false);
                $('#txtpastID').val('0');
                $('#hdFlag').val('pastWidg');

                $("#addpast-form").dialog('option', 'title', 'Add Past Medical History');
                $("#addpast-form").dialog("open");
            });
            $("#createaddpast-tab")
           .button()
           .click(function () {

               $('#dtDateOccurance').attr("disabled", false);
               $('#txtPastDiagnosis').attr("disabled", false);
               $('#txtPastNotes').attr("disabled", false);
               $('#txtpastID').val('0');
               $('#hdFlag').val('pastTab');
               $("#addpast-form").dialog('option', 'title', 'Add Past Medical History');
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
                        var dtExDateval = $('#txtImmunizationEXDate').val();
                        if (ValidateDateFormat(dtVal)) {
                            if (ValidateTime(tmVal)) {
                                if (ValidateDateFormat(dtExDateval)) {
                                    if (bValid) {
                                        //ajax 
                                        //  alert($.trim($('#cmbImmunizationTime').val()));
                                        try {
                                            var requestData = {
                                                PatientImmunizationCntr: $.trim($('#txtimmunizationID').val()),
                                                ImmunizationDate: $.trim($('#txtImmunizationDate').val()),
                                                Time: $.trim($('#cmbImmunizationTime').val()),
                                                CodeValue: codVal,
                                                // Vaccine: $.trim($('#hdImmunizationText').val()),
                                                Vaccine: $.trim($('#txtImmunization').val()),
                                                Amount: $.trim($('#txtAmount').val()),
                                                Note: $.trim($('#txtImmunizationNote').val()),
                                                Route: $.trim($('#txtImmunizationRoute').val()),
                                                Site: $.trim($('#txtImmunizationSite').val()),
                                                Sequence: $.trim($('#txtImmunizationSeq').val()),
                                                ExpirationDate: $.trim($('#txtImmunizationEXDate').val()),
                                                LotNumber: $.trim($('#txtImmunizationLotNo').val()),
                                                Manufacturer: $.trim($('#txtImmunizationMu').val()),
                                                Flag: $.trim($('#hdFlag').val())

                                            };


                                            $.ajax({
                                                type: 'POST',
                                                url: 'clinical-summary-immunizations-save',
                                                data: JSON.stringify(requestData),
                                                dataType: 'json',
                                                contentType: 'application/json; charset=utf-8',
                                                success: function (data) {
                                                    // if success
                                                    // alert("Success : " + data);
                                                    //  if ($('#hdFlag').val() == 'immunizationWidg')
                                                    $("#immunization-portlet").html(data[0]);
                                                    $("#tbl-Immunizations").fixedHeaderTable();
                                                    //  else if ($('#hdFlag').val() == 'immunizationTab')
                                                    $("#immunization-portlet-tab").html(data[1]);
                                                    $("#tab-Immunization").fixedHeaderTable();
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

                $("#show-date-imm").show();
                $("#show-date-imm-exp").show();
                $('#txtimmunizationID').val('0');
                $('#hdImmunizationText').val('');
                $('#hdImmunization').val('0');
                $('#hdFlag').val('immunizationWidg');
                $('#txtImmunizationDate').attr("disabled", false);
                $('#txtImmunizationVaccineAdminstered').attr("disabled", false);
                $('#txtImmunizationDate').attr("disabled", false);
                $('#txtImmunization').attr("disabled", false);
                $('#cmbImmunizationTime').attr("disabled", false);
                $('#txtAmount').attr("disabled", false);
                $('#txtImmunizationNote').attr("disabled", false);
                $('#txtImmunizationRoute').attr("disabled", false);
                $('#txtImmunizationSite').attr("disabled", false);
                $('#txtImmunizationSeq').attr("disabled", false);
                $('#txtImmunizationEXDate').attr("disabled", false);
                $('#txtImmunizationLotNo').attr("disabled", false);
                $('#txtImmunizationMu').attr("disabled", false);
                $("#addimmunizations-form").dialog('option', 'title', 'Add Immunization');
                $("#addimmunizations-form").dialog("open");

            });
            $("#createaddimmunizations_tab")
           .button()
           .click(function () {
               $('#txtimmunizationID').val('0');
               $('#hdImmunizationText').val('');
               $('#hdImmunization').val('0');
               $('#hdFlag').val('immunizationTab');
               $('#txtImmunizationDate').attr("disabled", false);
               $('#txtImmunizationVaccineAdminstered').attr("disabled", false);
               $('#txtImmunizationDate').attr("disabled", false);
               $('#txtImmunization').attr("disabled", false);
               $('#cmbImmunizationTime').attr("disabled", false);
               $('#txtAmount').attr("disabled", false);
               $('#txtImmunizationNote').attr("disabled", false);
               $('#txtImmunizationRoute').attr("disabled", false);
               $('#txtImmunizationSite').attr("disabled", false);
               $('#txtImmunizationSeq').attr("disabled", false);
               $('#txtImmunizationEXDate').attr("disabled", false);
               $('#txtImmunizationLotNo').attr("disabled", false);
               $('#txtImmunizationMu').attr("disabled", false);
               $("#addimmunizations-form").dialog('option', 'title', 'Add Immunization');
               $("#addimmunizations-form").dialog("open");
           });
        });
        //- end of add immunizations popup script




        $(document).ready(function () {

            $("#txtEHomephone").mask("(999) 999-9999");
            $("#txtEMobphone").mask("(999) 999-9999");
            $("#txtEZip").mask("99999");


            if ($('#cmbFacilityHome :selected').text() == 'Patient Entered') {
                $('#cmbVisitsHome').text("");
                $('#cmbVisitsHistory').text("");
                $('#cmbVisitsAllergy').text("");
                $('#cmbVisitsVisit').text("");
                $('#cmbVisitsImmunization').text("");
                $('#cmbVisitsLab').text("");
                $('#cmbVisitsProblem').text("");
                $('#cmbVisitsVital').text("");
            }


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
                        url: "clinical-summary-immunizationlists-get",
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
        });


        //$("#txtImmunization").autocomplete({

        //    select: function (event, ui) {
        //        $("#hdPatientId").val(ui.item.data);
        //        $("#PatName").text(ui.item.value);
        //        $("#PatName").show();
        //        //return false;
        //    }
        //});


















        $(function () {

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
            $("input[type=submit], a, button")
            .button()
            .click(function (event) {
                // event.preventDefault();
            });
        });

        $(document).ready(function () {

            $('#cmbDescription').change(function () {
                $('.validateTips').empty();
                $("span").removeClass("ui-state-highlight ui-state-error");
                $("#cmbDescription").removeClass(" ui-state-error");
            });

            $('#txtPharmacy').change(function () {
                $('.validateTips').empty();
                $("span").removeClass("ui-state-highlight ui-state-error");
                $("#txtPharmacy").removeClass(" ui-state-error");
            });

            $('#cmbConditionStatusProb').change(function () {
            $('.validateTips').empty();
            $("span").removeClass("ui-state-highlight ui-state-error");
            $("#cmbConditionStatusProb").removeClass(" ui-state-error");
            });

            $('#cmbConditionStatus').change(function () {
                $('.validateTips').empty();
                $("span").removeClass("ui-state-highlight ui-state-error");
                $("#cmbConditionStatus").removeClass(" ui-state-error");
            });

            
        });

        $(document).ready(function () {
            $('#LabTestResult1').removeClass('ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only');
            $('#LabTestResult2').removeClass('ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only');
            $('#createuser2').removeClass('ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only');
            $('#createuser').removeClass('ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only');
            $('#chartSummaryEdit').removeClass('ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only');
            $('#createuser1').removeClass('ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only');
            $('#lab_result_open').removeClass('ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only');
            $('#createdemographics').removeClass('ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only');
            $('#createpersonal').removeClass('ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only');
            $('#createemergency').removeClass('ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only');
            $('#createaddlab').removeClass('ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only');
            $('#createaddvisits').removeClass('ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only');
            $('#createaddmedications').removeClass('ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only');
            $('#createaddproblems').removeClass('ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only');
            $('#createaddvitals').removeClass('ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only');
            $('#createaddsocial').removeClass('ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only');
            $('#createaddfamily').removeClass('ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only');
            $('#createaddpast').removeClass('ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only');
            $('#createaddimmunizations').removeClass('ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only');
            $('#createaddvisits_tab').removeClass('ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only');
            $('#createaddimmunizations_tab').removeClass('ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only');
            $('#createaddproblems_tab').removeClass('ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only');
            $('#createaddallergies').removeClass('ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only');
            $('#createaddallergies_tab').removeClass('ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only');
            $('#createaddsocial_tab').removeClass('ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only');
            $('#createaddlab_tab').removeClass('ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only');
            $('#createaddpast-tab').removeClass('ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only');
            $('#createaddfamily-tab').removeClass('ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only');
            $('#createaddvitals_tab').removeClass('ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only');

            $('#lab-portlet').find('td').css('width', '33.3%');
            $('#lab-portlet').find('th').css('width', '33.3%');

            $('#allergies-portlet').find('td').css('width', '11.11%');
            $('#allergies-portlet').find('th').css('width', '11.11%');



            $('#lab-portlet-tab').find('td').css('width', '12.5%');
            $('#lab-portlet-tab').find('th').css('width', '12.5%');

            $('#medications-portlet').find('td').css('width', '25%');
            $('#medications-portlet').find('th').css('width', '25%');

            $('#problem-portlet').find('td').css('width', '50%');
            $('#problem-portlet').find('th').css('width', '50%');

            $('#problem-portlet-tab').find('td').css('width', '16.66%');
            $('#problem-portlet-tab').find('th').css('width', '16.66%');

            //$('#vital-portlet').find('td').css('width', '20%');
            //$('#vital-portlet').find('th').css('width', '20%');

            $('#vital-portlet-tab').find('td').css('width', '12.5%');
            $('#vital-portlet-tab').find('th').css('width', '12.5%');

            $('#social-portlet').find('td').css('width', '20%');
            $('#social-portlet').find('th').css('width', '20%');

            $('#family-portlet').find('td').css('width', '33.3%');
            $('#family-portlet').find('th').css('width', '33.3%');

            // $('#social-portlet-tab').find('td').css('width', '9.09%');
            //$('#social-portlet-tab').find('th').css('width', '9.09%');

            //$('#family-portlet-tab').find('td').css('width', '16.6%');
            //$('#family-portlet-tab').find('th').css('width', '16.6%');

            $('#pasthistory-portlet').find('td').css('width', '33.3%');
            $('#pasthistory-portlet').find('th').css('width', '33.3%');

            $('#pasthistory-portlet-tab').find('td').css('width', '16.6%');
            $('#pasthistory-portlet-tab').find('th').css('width', '16.6%');

            $('#immunization-portlet').find('td').css('width', '50%');
            $('#immunization-portlet').find('th').css('width', '50%');

            $('#immunization-portlet-tab').find('td').css('width', '20%');
            $('#immunization-portlet-tab').find('th').css('width', '20%');

            $('#lab-portlet').find('td').css('width', '20%');
            $('#lab-portlet').find('th').css('width', '20%');

            $('#appointment-portlet').find('td').css('width', '25%');
            $('#appointment-portlet').find('th').css('width', '25%');

            $('#visit-portlet').find('td').css('width', '25%');
            $('#visit-portlet').find('th').css('width', '25%');

            $('#visit-portlet-tab').find('td').css('width', '20%');
            $('#visit-portlet-tab').find('th').css('width', '20%');

            //$('#cmbVisitsHome').attr('disabled', true);
            $('#cmbVisitsHistory').attr('disabled', true);
            $('#cmbVisitsLab').attr('disabled', true);
            $('#cmbVisitsAllergy').attr('disabled', true);
            $('#cmbVisitsVisit').attr('disabled', true);
            $('#cmbVisitsProblem').attr('disabled', true);
            $('#cmbVisitsImmunization').attr('disabled', true);
            $('#cmbVisitsVital').attr('disabled', true);

            $("#txtBloodPressure").mask("9?9999");
            $("#txtBloodPressure1").mask("9?9999");
            $("#txtWeightLb").mask("9?9999");
            $("#txtHeight").mask("9?9999");
            $("#txtHeightinch").mask("9?9999");
            //$("#txtTemperature").mask("999.99");
            $("#txtPulse").mask("9?9999");
            $("#txtRespiration").mask("9?9999");

            $("#txtZipcode").mask("99999");
            $("#txthomphon").mask("(999) 999-9999");
            $("#txtcellpho").mask("(999) 999-9999");
            $("#txtworkpho").mask("(999) 999-9999");
            $("#txtfax").mask("(999) 999-9999");
            $("#txtemail").mask("");

            $("#txtEZip").mask("99999");
            $("#txtEHomephone").mask("(999) 999-9999");
            $("#txtEMobphone").mask("(999) 999-9999");

            //start of masking of problem modal popup
            $("#txtProblemsDate").mask("9999/99");
            $("#txtPlastDate").mask("9999/99");

            //end of masking of problem modal popup
            // $('.disable_button').css('color', 'grey');
            // Call horizontalNav on the navigations wrapping element
            $('.full-width').horizontalNav(
                {
                    responsive: false

                });


        });

        

        function toggle_combobox() {

            if ($('#cmbFacilityHome :selected').text() != 'Patient Entered') {
                $('#createaddsocial,#createaddfamily,#createaddpast,#createaddPOC,#createaddProcedures,#createaddClinicalInstructions,#createaddallergies,#createaddimmunizations,#createaddvitals,#createaddproblems,#createaddmedications').css('display', 'none');
               
                $('#cmbVisitsHome').attr('disabled', false);
            }
            else {
                $('#createaddsocial,#createaddfamily,#createaddPOC,#createaddpast,#createaddPOC,#createaddProcedures,#createaddClinicalInstructions,#createaddallergies,#createaddimmunizations,#createaddvitals,#createaddproblems,#createaddmedications').css('display', 'block');
          
                $('#cmbVisitsHome').text("");
                $('#cmbVisitsGlobal').text("");
                $('#cmbVisitsHome').attr('disabled', true);

                //       $('#cmbVisitsHome').val(0);
            }

        }



        function toggle_combobox_history() {

            if ($('#cmbFacilityHistory :selected').text() != 'Patient Entered') {
                $('#createaddsocial_tab,#createaddfamily-tab,#createaddpast-tab').css('display', 'none');
                $('.showHideThis').hide();
                $('#cmbVisitsHistory').attr('disabled', false);
            }

            else if ($('#cmbFacilityHistory :selected').text() == 'Patient Entered') {
                $('#createaddsocial_tab,#createaddfamily-tab,#createaddpast-tab').css('display', 'block');
                $('.showHideThis').show();
                
                $('#cmbVisitsHistory').text("");
                $('#cmbVisitsHistory').attr('disabled', true);
                //$('#cmbVisitsHistory').val(0);
            }

        }









        function toggle_combobox_allergy() {

            if ($('#cmbFacilityAllergy :selected').text() != 'Patient Entered') {
                $('#createaddallergies_tab').css('display', 'none');
                $('.showHideThisAllergy').hide();
                $('#cmbVisitsAllergy').attr('disabled', false);
            }

            else if ($('#cmbFacilityAllergy :selected').text() == 'Patient Entered') {
                $('#createaddallergies_tab').css('display', 'block');
                $('.showHideThisAllergy').show();
                $('#cmbVisitsAllergy').text("");
                $('#cmbVisitsAllergy').attr('disabled', true);
                // $('#cmbVisitsAllergy').val(0);
            }

        }




        function toggle_combobox_visit() {

            if ($('#cmbFacilityVisit :selected').text() != 'Patient Entered') {

                $('#cmbVisitsVisit').attr('disabled', false);
            }

            else if ($('#cmbFacilityVisit :selected').text() == 'Patient Entered') {

                $('#cmbVisitsVisit').text("");
                $('#cmbVisitsVisit').attr('disabled', true);
                // $('#cmbVisitsVisit').val(0);
            }

        }
        function toggle_combobox_providers() {

            if ($('#cmbFacilityProvider :selected').text() != 'Patient Entered') {

                $('#cmbVisitsProvider').attr('disabled', false);
            }

            else if ($('#cmbVisitsProvider :selected').text() == 'Patient Entered') {

                $('#cmbVisitsProvider').text("");
                $('#cmbVisitsProvider').attr('disabled', true);
                // $('#cmbVisitsVisit').val(0);
            }

        }
        
        function toggle_combobox_Procedures() {
            // alert('clinical')
            if ($('#cmbFacilityProcedure :selected').text() != 'Patient Entered') {
                // alert('if');
                $('.showHideThis').hide();
                $('#createaddProcedures-tab').css('display', 'none');
                $('#cmbVisitsProcedure').attr('disabled', false);
            }

            else if ($('#cmbFacilityProcedure :selected').text() == 'Patient Entered') {
                //  alert('Instruction')
                // alert('else');
                $('.showHideThis').show();
                $('#cmbVisitsProcedure').text("");
                $('#cmbVisitsProcedure').attr('disabled', true);
                $('#createaddProcedures-tab').css('display', 'block');
                // $('#cmbVisitsVisit').val(0);
            }

        }
        function toggle_combobox_PlanOfCare() {
           // alert('clinical')
            if ($('#cmbFacilityPlanOfCare :selected').text() != 'Patient Entered') {
               // alert('if');
                $('.showHideThis').hide();
                $('#cmbVisitsPlanOfCare').attr('disabled', false);
                $('#createaddPOC-tab').css('display', 'none');

            }

            else if ($('#cmbFacilityPlanOfCare :selected').text() == 'Patient Entered') {
                //  alert('Instruction')
               // alert('else');
                $('.showHideThis').show();
                $('#cmbVisitsPlanOfCare').text("");
                $('#cmbVisitsPlanOfCare').attr('disabled', true);
                $('#createaddPOC-tab').css('display', 'block');
                // $('#cmbVisitsVisit').val(0);
            }

        }
        function toggle_combobox_ClinicalInstructions() {
          //  alert('clinical')
            if ($('#cmbFacilityClinicalInstructions :selected').text() != 'Patient Entered') {
                $('.showHideThis').hide();
                $('#cmbVisitsClinicalInstructions').attr('disabled', false);
                $('#createaddClinicalInstructionstab').css('display', 'none');

            }

            else if ($('#cmbFacilityClinicalInstructions :selected').text() == 'Patient Entered') {
            //    alert('Instruction')
                $('.showHideThis').show();
                $('#cmbVisitsClinicalInstructions').text("");
                $('#cmbVisitsClinicalInstructions').attr('disabled', true);
                $('#createaddClinicalInstructionstab').css('display', 'block');
                // $('#cmbVisitsVisit').val(0);
            }

        }
        function toggle_combobox_immunization() {

            if ($('#cmbFacilityImmunization :selected').text() != 'Patient Entered') {
                $('.showHideThisImmunization').hide();
                $('#createaddimmunizations_tab').css('display', 'none');
                $('#cmbVisitsImmunization').attr('disabled', false);
            }

            else if ($('#cmbFacilityImmunization :selected').text() == 'Patient Entered') {
                $('#cmbVisitsImmunization').attr('disabled', true);
                $('.showHideThisImmunization').show();
                $('#cmbVisitsImmunization').text("");
                $('#createaddimmunizations_tab').css('display', 'block');
                // $('#cmbVisitsImmunization').val(0);
            }

        }


        function toggle_combobox_lab() {

            if ($('#cmbFacilityLab :selected').text() != 'Patient Entered') {

                $('#cmbVisitsLab').attr('disabled', false);
            }

            else if ($('#cmbFacilityLab :selected').text() == 'Patient Entered') {
                $('#cmbVisitsLab').text("");
                $('#cmbVisitsLab').attr('disabled', true);
                // $('#cmbVisitsLab').val(0);

            }

        }


        function toggle_combobox_problem() {

            if ($('#cmbFacilityProblem :selected').text() != 'Patient Entered') {
                $('#createaddproblems_tab').css('display', 'none');
                $('.showHideThisProblem').hide();
                $('#cmbVisitsProblem').attr('disabled', false);
            }

            else if ($('#cmbFacilityProblem :selected').text() == 'Patient Entered') {
                $('#cmbVisitsProblem').attr('disabled', true);
                $('#cmbVisitsProblem').text("");
                $('.showHideThisProblem').show();
                $('#createaddproblems_tab').css('display', 'block');
                // $('#cmbVisitsProblem').val(0);
            }

        }


        function toggle_combobox_vital() {

            if ($('#cmbFacilityVital :selected').text() != 'Patient Entered') {
                $('#createaddvitals_tab').css('display', 'none');
                $('.showHideThisVital').hide(); 
                $('#cmbVisitsVital').attr('disabled', false);
            }

            else if ($('#cmbFacilityVital :selected').text() == 'Patient Entered') {
                $('#cmbVisitsVital').text("");
                $('.showHideThisVital').show(); 
                $('#cmbVisitsVital').attr('disabled', true);
                $('#createaddvitals_tab').css('display', 'block');
                // $('#cmbVisitsVital').val(0);
            }

        }

        function toggle_combobox_document() {

            if ($('#cmbFacilityDocument :selected').text() != 'Patient Entered') {
                $('#createaddvitals_tab').css('display', 'none');
                $('#createaddProcedures-tab').css('display', 'none');
                $('#createaddPOC-tab').css('display', 'none');
                $('#createaddClinicalInstructionstab').css('display', 'none');
                
                

                $('#cmbVisitsDocument').attr('disabled', false);
            }

            else if ($('#cmbFacilityDocument :selected').text() == 'Patient Entered') {
                $('#cmbVisitsDocument').text("");
                $('#cmbVisitsDocument').attr('disabled', true);
                $('#createaddvitals_tab').css('display', 'block');
                $('#createaddProcedures-tab').css('display', 'block');
                $('#createaddPOC-tab').css('display', 'block');
                $('#createaddClinicalInstructionstab').css('display', 'block');
                // $('#cmbVisitsVital').val(0);
            }

        }

        function enabling_buttons() {
            $('.disable_button').prop('disabled', false);
            // $('.disable_button').css('color', 'rgb(46, 110, 158)');
        }


        function removing_class2() {
            $('#createuser2').removeClass('ui-state-hover ');
            $('#createuser2').removeClass('ui-state-hover ');
        }
        function removing_class3() {
            $('#createuser1').removeClass('ui-state-hover ');
            $('#createuser1').removeClass('ui-state-hover ');
        }
        function removing_class4() {
            $('#createuser').removeClass('ui-state-hover ');
            $('#createuser').removeClass('ui-state-hover ');
        }
        function removing_class5() {
            $('#chartSummaryEdit').removeClass('ui-state-hover ');
            $('#chartSummaryEdit').removeClass('ui-state-hover ');
        }
        function removing_class1() {
            $('#LabTestResult1').removeClass('ui-state-active');
            $('#LabTestResult2').removeClass('ui-state-active');
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
    
    


        $(function () {
            $("input[type=submit], a, button")
            .button()
            .click(function (event) {
                // event.preventDefault();
            });
        });

        $(document).ready(function () {
            $('#LabTestResult1').removeClass('ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only');
            $('#LabTestResult2').removeClass('ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only');


            // Call horizontalNav on the navigations wrapping element
            $('.full-width').horizontalNav(
                {
                    responsive: false

                });


        });







        function removing_class() {
            $('#LabTestResult1').removeClass('ui-state-hover ');
            $('#LabTestResult2').removeClass('ui-state-hover ');
            $('#createdemographics').removeClass('ui-state-hover ');
            $('#createpersonal').removeClass('ui-state-hover ');
            $('#createemergency').removeClass('ui-state-hover ');
            $('#createaddlab').removeClass('ui-state-hover ');
            $('#createaddvisits').removeClass('ui-state-hover ');
            $('#createaddmedications').removeClass('ui-state-hover ');
            $('#createaddproblems').removeClass('ui-state-hover ');
            $('#createaddvitals').removeClass('ui-state-hover ');
            $('#createaddsocial').removeClass('ui-state-hover ');
            $('#createaddfamily').removeClass('ui-state-hover ');
            $('#createaddpast').removeClass('ui-state-hover ');
            $('#createaddimmunizations').removeClass('ui-state-hover ');

            $('#createaddvisits_tab').removeClass('ui-state-hover ');
            $('#createaddimmunizations_tab').removeClass('ui-state-hover ');
            $('#createaddproblems_tab').removeClass('ui-state-hover ');
            $('#createaddallergies').removeClass('ui-state-hover ');
            $('#createaddallergies_tab').removeClass('ui-state-hover ');
            $('#createaddsocial_tab').removeClass('ui-state-hover ');
            $('#createaddlab_tab').removeClass('ui-state-hover ');

            $('#createaddpast-tab').removeClass('ui-state-hover ');
            $('#createaddfamily-tab').removeClass('ui-state-hover ');
            $('#createaddvitals_tab').removeClass('ui-state-hover ');
        }


        function removing_class1() {
            $('#LabTestResult1').removeClass('ui-state-active');
            $('#LabTestResult2').removeClass('ui-state-active');
        }

        function removing_lab_result_class() {
            $('#lab_result_open').removeClass('ui-state-hover ');
            $('#lab_result_open').removeClass('ui-state-active');
        }

        function removing_social_hist_class() {
            $('#social_hist_edit').removeClass('ui-state-hover ');
            $('#social_hist_edit').removeClass('ui-state-active');

            $('# social_hist_delete').removeClass('ui-state-hover ');
            $('# social_hist_delete').removeClass('ui-state-active');

            $('#social_hist_details').removeClass('ui-state-hover');
            $('#social_hist_details').removeClass('ui-state-active');
        }



        function editDemographics() {
            
            var obj = jQuery.parseJSON($("#demographicsdata").val());
            $('#txtFname').val($.trim(obj.FirstName));
            $('#txtMname').val($.trim(obj.MiddleName));
            $('#txtLname').val($.trim(obj.LastName));
            $('#cmbName').val($.trim(obj.Title));
            $('#cmbGenderDialog').val($.trim(obj.GenderId));
            $('#cmbPreferredLanguageDialog').val($.trim(obj.PreferredLanguageId));
            $('#cmbEthnicityDialog').val($.trim(obj.EthnicityId));
            $('#txtAdd').val($.trim(obj.PAddress));
            $('#cmbCountry').val($.trim(obj.CountryCode));
            $('#txtCity').val($.trim(obj.City));
            $('#cmbStates').val($.trim(obj.State));
            $('#txtZipcode').val($.trim(obj.Zip));
            $('#txthomphon').val($.trim(obj.HomePhone));
            $('#txtcellpho').val($.trim(obj.MobilePhone));
            $('#txtworkpho').val($.trim(obj.WorkPhone));
            $('#txtemail').val($.trim(obj.EMail));
            $('#txtdob').val($.trim(obj.DOB));
            $('#cmbRaceDialog').val($.trim(obj.RaceId));
            $('#cmbSmokingStatusDialog').val($.trim(obj.SmokingStatusId));
            $('#txtMAdd').val($.trim(obj.MailAddress1));
            $('#txtMAdd2').val($.trim(obj.MailAddress2));
            $('#txtMCity').val($.trim(obj.MailCity));
            $('#cmbMCountry').val($.trim(obj.MailCountryCode));
            $('#cmbMStates').val($.trim(obj.MailPostalCode));
            $('#txtMZipcode').val($.trim(obj.MailState));
            //var value;
            if (obj.RaceId_NotAnswered == 'True') {
                $('#rdrcprefno').prop('checked', true);
            }
            else {
                $('#rdrcprefno').prop('checked', false);
            }
            if (obj.RaceId_Native == 'True') {
                $('#rdrccnat').prop('checked', true);
            }
            else {
                $('#rdrccnat').prop('checked', false);
            }
            if (obj.RaceId_Asian == 'True') {
                $('#rdrcasn').prop('checked', true);
            }
            else {
                $('#rdrcasn').prop('checked', false);
            }
            if (obj.RaceId_Black == 'True') {
                $('#rdrcblk').prop('checked', true);
            }
            else {
                $('#rdrcblk').prop('checked', false);
            }
            if (obj.RaceId_Hawaiian == 'True') {
                $('#rdrchw').prop('checked', true);
            }
            else {
                $('#rdrchw').prop('checked', false);
            }
            if (obj.RaceId_White == 'True') {
                $('#rdrcwh').prop('checked', true);

            }
            else {
                $('#rdrcwh').prop('checked', false);
            }
            $("#demographics-form").dialog('option', 'title', 'Edit Demographics');
            $("#demographics-form").dialog("open");
        }



        function editEmergency() {

            //alert($("#objEmergency").val());
            var obj = jQuery.parseJSON($("#objEmergency").val());

            // var obj = jQuery.parseJSON($('#Emergencydata').val());
            $('#txtEname').val($.trim(obj.EmergencyName));
            $('#txtEAdd').val($.trim(obj.EmergencyAddress));
            $('#cmbECountry').val($.trim(obj.EmergencyCountryCode));
            $('#txtECity').val($.trim(obj.EmergencyCity));
            $('#cmbEStates').val($.trim(obj.EmergencyState));
            $('#txtEZip').val($.trim(obj.EmergencyZip));
            $('#txtEHomephone').val($.trim(obj.EmergencyHomePhone));
            $('#txtEMobphone').val($.trim(obj.EmergencyMobilePhone));
            $('#cmbERel').val($.trim(obj.EmergencyRelationshipId));
            $('#chkIsPrimary').prop('checked', $.trim(obj.IsPrimary));
            //$('#txtEHomephone').val($.trim(obj.EmergencyPhone));
            $("#emergency-form").dialog('option', 'title', 'Edit Emergency');
            $('#emergency-form').dialog("open");
        }
    

     
       