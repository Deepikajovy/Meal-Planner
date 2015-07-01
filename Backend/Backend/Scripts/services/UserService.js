// 	app.factory('UserInfo', ['$http', function($http) {
//   return $http.get('http://roameals.azurewebsites.net/api/meals')
//   .success(function(data) {
//     console.log(data)
//     return data;
//   })
//   .error(function(err) {
//     return err;
//   });
// }]);



var UserInfo = function($http,$window)
{

  var authoriz = 'Bearer ' + $window.sessionStorage.getItem('tokenKey');
    $http({
      method: 'GET',
      url: 'http://roameals.azurewebsites.net/api/Account/UserInfo',
       headers: { 'Content-Type': 'application/json',
                 'Authorization': authoriz }
     }).success(function(data)
   {
  	alert(data.Email);
  	return data;

    console.log(data);
   }).error(function (data) {
       console.log("error getting user info");
   });

};


var module=angular.module("MealsApp");
module.factory("UserInfo",UserInfo);