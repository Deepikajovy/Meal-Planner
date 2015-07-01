// var app = angular.module('MealsApp'); - this is in app.js, don't need here - LIBBY

app.controller("MainController",
  ["$scope", "$http", "meals", "userAuthenticationService", "$window","$rootScope", function($scope, $http, meals, userAuthenticationService, $window,$rootScope) {
   angular.element(document).ready(function () {
alert(window.location.href);
if($window.sessionStorage.getItem('tokenKey') != null){

          localStorage.loggedin = "none";
          localStorage.loggedout = "block";
        }
        else
        {
          localStorage.loggedin = "block";
          localStorage.loggedout = "none";
        }

        var sup = document.getElementById("signup");
        sup.style.display = localStorage.loggedin;
        var sin = document.getElementById("signin");
        sin.style.display = localStorage.loggedin;
        var sout = document.getElementById("signout");
        sout.style.display = localStorage.loggedout;



    var userResponse = userAuthenticationService.GetUserName();
      userResponse.success(function(data)
      {
        $scope.UserName = data.Name;

        // var sup = document.getElementById("signup");
        // sup.style.display = localStorage.loggedin;
        // var sin = document.getElementById("signin");
        // sin.style.display = localStorage.loggedin;
        // var sout = document.getElementById("signout");
        // sout.style.display = localStorage.loggedout;
      })
      .error(function(data){
        // have made this error message below an emptu string so we don't have "error" coming up on the home page when viewing with no user logged in

         //  $window.location.href = '#Login';
      });

    meals.success(function(data) {
      $scope.mealsInfo = data;



   	$scope.searchMeals = function() {
    		console.log("search meals", $scope.search);
    		console.log("meals info", $scope.mealsInfo);
    	}()

    });

      $scope.getShoppingList = function() {
        $scope.arrayOfIng = "test";
      var authoriz = 'Bearer ' + $window.sessionStorage.getItem('tokenKey');
      console.log('authoriz', authoriz);
      $http({
        method: 'GET',
        url: 'http://roameals.azurewebsites.net/api/MealPlans/ShoppingList',
        headers: {
          'Content-Type': 'application/json',
          'Authorization': authoriz
        }
      })
      .success(function(data){

        $scope.arrayOfIng = data;
        console.log("Array of ingredients", $scope.arrayOfIng);
    	})

      .error(function(data){
        console.log("error: ", data);
      })
  }()


  $scope.arrayOfIng=" ";
    var logindisplay=function(event, dataforme)
    {
      $scope.UserName=dataforme
    }
     var logoutdisplay=function(event, dataforme )
    {
      $scope.UserName=" ";
    }
    $rootScope.$on("logged-in", logindisplay);
    $rootScope.$on("logged-out", logoutdisplay);
  })


  $scope.toggleCSS = function () {
    window.print();
  }

}]);


