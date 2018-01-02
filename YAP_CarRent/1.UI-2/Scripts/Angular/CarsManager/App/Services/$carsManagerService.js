(function () {
    angular.module("carsManagerApp").factory("$carsManagerService", function ($http) {


        console.log("$Cars Manager Service is up!");

      
        function getAllManufacturerModels(manufacturer) {

            return $http({
                method: "GET",
                url: "/Cars/GetAllManufacturerModels?manufacturer=" + manufacturer
            }).then(function mySuccess(response) {
                return response.data;
            }, function myError(response) {
                return response.statusText;
            });


        }

        function careateNewCar(item) {
            alert(item);
            //return $http({
            //    method: "GET",
            //    url: "/Cars/GetAllManufacturerModels?manufacturer=" + manufacturer
            //}).then(function mySuccess(response) {
            //    return response.data;
            //}, function myError(response) {
            //    return response.statusText;
            //});


        }

        return {

            getAllManufacturerModels: getAllManufacturerModels,
            careateNewCar: careateNewCar
        
      

        };


    })
})();
     