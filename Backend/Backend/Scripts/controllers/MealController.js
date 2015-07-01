app.controller("MealController", ["$scope", "$http", "meals", '$routeParams', '$window', function($scope, $http, meals, $routeParams, $window) {

  meals.success(function(data) {
    // console.log("meals success", data);
    $scope.detail = data[$routeParams.id];
    // console.log("Scope detail data", $scope.detail);
  });

  if ($window.sessionStorage.length == 0) {
    $window.location.href = '#Login';
  }

  $scope.AddToMealPlan = function () {
    // console.log(index)
    var authoriz = 'Bearer ' + $window.sessionStorage.getItem('tokenKey');
    var meal = $scope.detail;
    var id = meal.Id;

    $http({
      url: "http://roameals.azurewebsites.net/api/MealPlans/AddTo",
      method: "POST",
      data: meal,
      headers: {
        'Content-Type': 'application/json',
        'Authorization': authoriz
      }
    })
      .success(function (data) {
      // console.log("yolo swag");
    })
      .error(function (data) {
      console.log("add meal error", data);
    })
  }


  $scope.getMealPlan = function () {
    var authoriz = 'Bearer ' + $window.sessionStorage.getItem('tokenKey');
    $http({
      method: 'GET',
      url: 'http://roameals.azurewebsites.net/api/MealPlans',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': authoriz
      }
    })
      .success(function (data) {
      $scope.showMealPlan = data;
      // console.log($scope.showMealPlan);
    })
      .error(function (data) {
      console.log("error: ", data);
    })
  }()

  $scope.showMealPlan = "";

  var IngredientList = [];

  var localName = $scope.ingredientName;
  var localQuantity = $scope.quantity;
  var localMeasurement = $scope.measurement;

  $scope.newIngred = function () {

    var ingredient = {
      Name: $scope.ingredientName,
      Quantity: $scope.quantity,
      Measurement: $scope.measurement

    }
    IngredientList.push(ingredient);

    $scope.ingredientName = "";
    $scope.quantity = "";
    $scope.measurement = "";
    console.log(IngredientList);
  }


  $scope.addAMeal = function () {
    var authoriz = 'Bearer ' + $window.sessionStorage.getItem('tokenKey');

    var ingredient = {
      Name: localName,
      Quantity: localQuantity,
      Measurement: localMeasurement
    }

    IngredientList.push(ingredient);
    var data =
      {
        Name: $scope.mealName,
        Description: $scope.description,
        ImageUrl: $scope.imageurl,
        Ingredients: IngredientList
      };

    // console.log(IngredientList);


    var mealData = JSON.stringify(data);
    $http({
      method: 'POST',
      url: 'http://roameals.azurewebsites.net/api/Meals',
      data: mealData,
      headers: {
        'Content-Type': 'application/json',
        'Authorization': authoriz
      }
    })
      .success(function (data) {
      // console.log("ADDING A MEAL");
      // console.log(data);
      IngredientList.length = 0;
      location.reload();
    })
      .error(function (data) {
      console.log("error: ", data);
    });
  };

  $scope.deleteMeal = function (index) {
     var authoriz = 'Bearer ' + $window.sessionStorage.getItem('tokenKey');
     $http({
      method: 'POST',
      url: 'http://roameals.azurewebsites.net/api/MealPlans/DeleteFromMealPlan?mealindex='+index,
      data: index,
      headers: {
        'Content-Type': 'application/json',
        'Authorization': authoriz
      }}).success(function(data)
      {
         // console.log("deleted successfully ");
          location.reload();
      }).error(function(data)
      {
         console.log("error deleting ");
      });
  };
}]);



