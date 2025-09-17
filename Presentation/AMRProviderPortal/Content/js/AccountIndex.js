$(document).ready(function () {
    $('#tabs').tabs();
    document.getElementById('tabs').style.display = 'block';
});


// start of change password pop up script

$(function () {

    tips = $(".validateTips");



    $("#changePassword-form").dialog({
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
                // alert('test');
                var bValid = true;
                //allFields.removeClass("ui-state-error");

                if (!Reqvalue($.trim($('#txtpwd').val()), 'Old Password'))
                { return; }

                if (!Reqvalue($.trim($('#txtnpwd').val()), 'New Password'))
                { return; }

                if ($('#txtnpwd').val() != $('#txtcpwd').val())
                { alert('New Password and Confirm New Password didn\'t match'); return; }

                if (bValid) {
                    // Save routine and call of Action
                    //    alert('test');
                    try {
                        var requestData = {
                            Password: $.trim($('#txtpwd').val()),
                            NewPassword: $.trim($('#txtnpwd').val())
                        };

                        $.ajax({
                            type: 'POST',
                            url: 'account-password-change',
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
            Close: function () {
                $(this).dialog("close");
            }
        },
        close: function () {

        }
    });

});

//- end of change password pop up script

function change_pass() {
    $("#changePassword-form").dialog("open");
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
