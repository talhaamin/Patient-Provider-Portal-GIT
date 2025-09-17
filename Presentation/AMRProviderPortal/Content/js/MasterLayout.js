

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
    $("#counter1").append("You have <strong>" + characters + "</strong> characters remaining");
   // $("#Appcount").html("You have <strong>" + characters + "</strong> characters remaining");

    $("#txtMessageWrite").keyup(function () {

        if ($(this).val().length > characters) {
            $(this).val($(this).val().substr(0, characters));
        }
        var remaining = characters - $(this).val().length;
        $("#counter").html("You have <strong>" + remaining + "</strong> characters remaining");

    });
    $("#txtMessageWrite1").keyup(function () {

        if ($(this).val().length > characters) {
            $(this).val($(this).val().substr(0, characters));
        }
        var remaining = characters - $(this).val().length;
        $("#counter1").html("You have <strong>" + remaining + "</strong> characters remaining");

    });
    $("#txtMessageWrite2").keyup(function () {

        if ($(this).val().length > characters) {
            $(this).val($(this).val().substr(0, characters));
        }
        var remaining = characters - $(this).val().length;
        $("#Appcount").html("You have <strong>" + remaining + "</strong> characters remaining");

    });
    $("#txtComments").keyup(function () {

        if ($(this).val().length > characters) {
            $(this).val($(this).val().substr(0, characters));
        }
        var remaining = characters - $(this).val().length;
        $("#refillcount").html("You have <strong>" + remaining + "</strong> characters remaining");

    });
    $("#txtMessageRef").keyup(function () {

        if ($(this).val().length > characters) {
            $(this).val($(this).val().substr(0, characters));
        }
        var remaining = characters - $(this).val().length;
        $("#referalcount").html("You have <strong>" + remaining + "</strong> characters remaining");

    });
    
});

$(document).ready(function () {

    var c = document.getElementById("mycanvas");
    var ctx = c.getContext("2d");
    var imageLoader = document.getElementById('fu-my-simple-upload');
    imageLoader.addEventListener('change', handleImage, false);



    function handleImage(e) {
        // alert('test');
        var reader = new FileReader();
        reader.onload = function (event) {
            var img = new Image();
            img.onload = function () {
                //canvas.width = img.width;
                //canvas.height = img.height;
                ctx.drawImage(img, 0, 0, 200, 200);
            }
            img.src = event.target.result;
        }
        reader.readAsDataURL(e.target.files[0]);
    }

    initSimpleFileUpload();

});

function initSimpleFileUpload() {
    'use strict';

    $('#fu-my-simple-upload').fileupload({
        url: 'master-images-save',
        dataType: 'json',
        add: function (e, data) {

            jqXHRData = data
        },
        done: function (event, data) {
            $('#PatientImg').html(data.result.Imghtml);

            HideLoader();
        },
        fail: function (event, data) {
            if (data.files[0].error) {
                alert(data.files[0].error);
            }
        }
    });
}

function ShowLoader() {
    var body_height = parseInt($('body').height());
    $('#page_loader').css('height', body_height);
    document.getElementById('page_loader').style.display = 'block';
}
function HideLoader() {
    document.getElementById('page_loader').style.display = 'none';
}
function imageUpload() {

    $("#profileImage-form").dialog("open");
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
function Logout() {

    location.href = '/logout';
    

}

$(document).ready(function () {
    $.ajax({
        dataType: "json",
        type: 'GET',
        url: 'Get-Messages-Counter',
        success: function (data) {
            $('#circle').html(data.TotalMessages);
            $('#GeneralMessages').html("Inbox(<b style='font-size:14px;'>" + data.GeneralMessages + "</b>)");
            $('#AppointmentMessages').html("Appointment(<b style='font-size:14px;'>" + data.AppointmentMessages + "</b>)");
            $('#MedicationMessages').html("Refill Requests(<b style='font-size:14px;'>" + data.MedicationMessages + "</b>)");
            $('#ReferralMessages').html("Referral Requests(<b style='font-size:14px;'>" + data.ReferralMessages + "</b>)");
            $('#SentMessages').html("Sent Items<b style='font-size:14px;'></b>");
            $('#DeleteMessages').html("Deleted<b style='font-size:14px;'></b>");
        }
    });
});

$(window).resize(function () {
    $("#timeout-dialog").dialog("option", "position", "center");
});
$(function () {

    $("#accordion").accordion({
        heightStyle: "content"
    });

    $("#accordion").accordion();

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

    $("#txtPrefDateFrom").datepicker({
        inline: true
    });
    $("#txtPrefDateFromRef").datepicker({
        inline: true
    });

});

function clearFields() {

    $(":input[type!='hidden']").val('');
    $(':input').prop('checked', false);

}