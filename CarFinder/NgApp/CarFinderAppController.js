var app = angular.module("CarFinderApp");
    app.controller("CarFinderAppController", ["$scope", "http", function ($scope, $http) {
        $scope.getYears = [];
        $scope.getMakes = [];
        $scope.getModels = [];
        $scope.getTrims = [];

        $scope.selectedYear = " ";
        $scope.selectedMake = " ";
        $scope.selectedModel = " ";
        $scope.selectedTrim = " ";
        $scope.carData = {};

        scope.getYears = function() {
            //var options = { params: {} }; 

            //make request
            $http.get("api/years").then(function (response) {
                //assign result to a $scope variable
                $scope.getYears = response.data;
            });

        };

        $scope.getMakes = function() {
            var options = { params: { year: $scope.selectedYear } }; //pass the selected year to the API
            $http.get("api/makes", options).then(function(response) {
                $scope.getMakes = response.data;
            });
        };

        $scope.getModels = function() {
            var options = {
                params: { year: $scope.selectedYear, make: $scope.selectedMake } 
            };
            $http.get("api/models", options).then(function(response) {
                $scope.models = response.data;
            });
        };

        $scope.getTrims = function () {
            var options = { params: { year: $scope.selectedYear, make: $scope.selectedMake, model: $scope.selectedModel } };
            $http.get("api/trims", options).then(function(response) {
                $scope.trims = response.data;
                $scope.getCar();
            });
        };

        $scope.getCars = function() {
            var options = {
                params: {
                    year: $scope.selectedYear,
                    make: $scope.selectedMake,
                    model: $scope.selectedModel,
                    trim: $scope.selectedTrim
                }
            };
            $http.get("api/cars", options).then(function(response) {
                $scope.carData = response.data;
            });
            $scope.getYears();
        }]);