(function () {
    angular.module("carsApp").factory("$carsService",function ($http) {


        console.log("Cars Service is up!");

        function GetCarsByBranch(branch) {

            return $http({
                method: "GET",
                url: "/Cars/GetCarsByBranch?branch=" + branch
            }).then(function mySuccess(response) {
                return response.data;
            }, function myError(response) {
                return response.statusText;
            });


        }
        function searchCars(branch, currentGroup, currentManufacturer) {

            //int _branch, string _group, string _manufacturer
            return $http({
                method: "GET",
                url: "/Cars/GetManageCars?_branch=" + branch + "&_group=" + currentGroup + "&_manufacturer=" + currentManufacturer 
            }).then(function mySuccess(response) {
                return response.data;
            }, function myError(response) {
                return response.statusText;
            });


        }
        return {


            GetCarsByBranch: GetCarsByBranch,
            searchCars: searchCars,


        };


    })

})();

     
