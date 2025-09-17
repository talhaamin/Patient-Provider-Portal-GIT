function checkEmail(value) {

 


  //  var filter = /^[a-zA-Z0-9_.-]+@[a-zA-Z0-9]+[a-zA-Z0-9.-]+[a-zA-Z0-9]+.[a-z]{0,4}$/;

    var filter=/^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
   
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