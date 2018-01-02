carsManagerApp.controller('carsManagerCntroller', function ($scope, $carsManagerService) {
    $scope.title = "מכונית חדשה";

    // gets all the branches
  
    // clearing the search inputs
    $scope.cleanSearch = function () {
        $scope.currentBranch = "";      
        $scope.currentManufacturer = "";
        $scope.currentModel = "";
        $scope.currentSerial = "";
        $scope.currentKm = "";
        $scope.currentGear = "";
        $scope.currentStatus = "";
        $scope.currentImg = "";
    };


    $scope.changeManufacturer = function () {
        $carsManagerService.getAllManufacturerModels($scope.currentManufacturer).then(function (response) {
            console.table(response);
            $scope.modelList = response;

        });


    }
    $scope.createCar = function () {
        $carsManagerService.careateNewCar($scope.currentBranch)


    }
    //$scope.changeBranch = function () {
    //    $carsService.getAllBranches().then(function (response) {
    //        $scope.branchesList = response;
    //    })
    //}


});