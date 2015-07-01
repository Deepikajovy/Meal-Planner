app.factory('meals', ['$http', function($http) {
  return $http.get('http://roameals.azurewebsites.net/api/meals')
  .success(function(data) {
    // console.log(data)
    return data;
  })
  .error(function(err) {
    return err;
  });
}]);


