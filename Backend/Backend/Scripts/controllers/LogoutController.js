
app.controller('LogoutController', function ($scope, $http, $window,$rootScope) {

     $scope.logout=function()
     {
        var authoriz = 'Bearer ' + $window.sessionStorage.getItem('tokenKey');
     
        $http({
            method: 'POST',
            url: 'http://roameals.azurewebsites.net/api/Account/Logout',
            
            headers: {'Authorization': authoriz}
        }).success(function () {
             $rootScope.$emit("logged-out", "")
            $window.sessionStorage.removeItem('tokenKey', "");

            console.log(" Logged out sucessfully.......");

            //var sup = document.getElementById("signup");
            //sup.style.display = 'block';
            localStorage.loggedin = "block";


                
            // var sin = document.getElementById("signin");
            // sin.style.display = 'block';
            //localStorage.loggedin = sin.style.display;

            // var sout = document.getElementById("signout");
            // sout.style.display = 'none';
            localStorage.loggedout = "none";
            // console.log("Token", data.access_token);
        var sup = document.getElementById("signup");
        sup.style.display = localStorage.loggedin;
        var sin = document.getElementById("signin");
        sin.style.display = localStorage.loggedin;
        var sout = document.getElementById("signout");
        sout.style.display = localStorage.loggedout;

          
         $window.location.href = '#Login';
         //'Content-Type': 'application/x-www-form-urlencoded'
        }).error(function () {

            console.log("Error Logging Out..." );
           
             //$scope.errormessage="Incorrect UserName or Password";
        });
    }
});


