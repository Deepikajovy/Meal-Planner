(function()
{
  var userAuthenticationService = function($http, $window) {
    var getUserName = function(){
      var authoriz = 'Bearer ' + $window.sessionStorage.getItem('tokenKey');
      return $http({
        method: 'GET',
        url: 'http://roameals.azurewebsites.net/api/Account/UserInfo',
        headers: {
          'Content-Type': 'application/json',
          'Authorization': authoriz
        }
      })
      .success(function(data){
        console.log("ttttttttttttttttttt", data)
        return data;
      })
      .error(function(data){
        return data;      
      });
    };
    
    return {
      GetUserName: getUserName
      };
  };
  
  var menuApp = angular.module('MealsApp');
  menuApp.factory('userAuthenticationService', ['$http','$window', userAuthenticationService]);
}());


//http://localhost:62555/
//http://roameals.azurewebsites.net/