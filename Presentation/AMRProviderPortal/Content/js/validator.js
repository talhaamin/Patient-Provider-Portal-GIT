function checkEmail(value) {

 


    var filter = /^[a-zA-Z0-9_.-]+@[a-zA-Z0-9]+[a-zA-Z0-9.-]+[a-zA-Z0-9]+.[a-z]{0,4}$/;

    if (!filter.test(value)) {
        alert('Please provide a valid email address');
     
        return false;
    }
    return true;


}

function checkPhone(value, fldname) {

   

    var filter = /^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$/;

    if (!filter.test(value)) {
        alert('This is not a valid phone number');
        object.focus;
        return false;
    }
    else {

        return true;
    }


}



function checkDate(value, fldname) {

  

    var filter = /^(0[1-9]|1[0-2])\/(0[1-9]|1\d|2\d|3[01])\/(19|20)\d{2}$/;

    if (!filter.test(value)) {
        alert('This is not a valid date of format(mm/dd/yyyy)');
        object.focus;
        return false;
    }
    else {

        return true;
    }


}


function checkString(value, fldname) {

  

    var filter = /^([a-z])+([0-9a-z]+)$/i;

    if (!filter.test(value)) {
        alert('This is not a String');
        object.focus;
        return false;
    }
    else {

        return true;
    }


}


function checkNumber(value,fldname) {
    alert(value);
  alert(fldname)

    var filter = /^[0-9]*(\.[0-9]+)?$/;

    if (!filter.test(value)) {
        alert('Please enter numeric value in ' + fldname);
        
        return false;
    }
    else {
       
        return true;
    }

}

function Reqvalue(value, fldname) {

    if (value == '') {
        alert('Please Enter ' + fldname);
        return false;
    }
    return true;
}
function IsValidEmail(value) {
    var re = /\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*/;
    if (!re.test(value)) {
        alert('Please provide a valid email address'); return false;
    }

    return true;

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



function PasswordValid(Value) {

    var Pass = /^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#~+.?!@$%^&*-]).{8,16}$/;
    if (!Pass.test(Value)) {

        // alert('Please provide a valid email address'); 
        alert("Passwords must be between 8 and 16 characters." + "\n" + "Passwords must include any combination of the following 4 elements:" + "\n" + "• Lowercase Letter(s)-Examples:  ‘a’, ‘b’, ‘ab’, ‘abc’, etc)" + "\n" + "• Uppercase Letter(s)-Examples: ‘A’, ‘B’, or ‘AB, or ‘ABC’, etc." + "\n" + "• Number(s) - Examples: ‘1’, ‘2’ ‘3’ ‘4’ ‘5’ ‘6’ ‘7’ ‘8’ ‘9’ ‘0’, or ‘12’, or ‘123’, ‘1234’, etc." + "\n" + "• At least one of the following symbols: ‘@’, ‘!’, ‘#’, ‘$’, ‘%’, ‘^’, ‘&’, ‘~’, ‘+’,‘?’, ‘*’, and or ‘.’");

        return false;

    }
    return true;

}