function checkUrl(url) {
   // var pattern = /^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%&]}).*$/;
    var pattern = /^.*(?=.{7,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&+=]).*$/;


    if (pattern.test(url)) {
      
        return true;
    } else {
     
        return false;
    }
}