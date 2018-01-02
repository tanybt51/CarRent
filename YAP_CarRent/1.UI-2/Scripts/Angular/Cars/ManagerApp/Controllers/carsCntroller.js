carsApp.controller('carsController', function ($scope, $carsService) {
    $scope.title = "ניהול מכונית";
    $scope.disable = true;
    //$scope.currentGroup = "";
    //$scope.currentManufacturer = "";
    //$scope.currentMin = "";
    //$scope.currentMax = "";
    // gets all the branches
    var branchID;

    // clearing the search inputs
    $scope.cleanSearch = function (item) {

        if (!item) {
            $scope.currentBranch = "";
        }
        $scope.currentBranch = "";
        $scope.currentGroup = "";
        $scope.currentManufacturer = "";
        $scope.searchText = "";
        $scope.disable = true;
    };

    $scope.changBranch = function () {
        if ($scope.currentBranch.length > 0)
            $scope.disable = false;
        else {
            $scope.cleanSearch(true);
            $scope.disable = true;
        }
        var d = document.getElementById($scope.currentBranch);
        if (d != null) {
            var att = d.getAttribute("data-id")
            console.log(att);
            branchID = att;
        }

    }

    $scope.search = function () {
        console.log("מתבצע חיפוש");
        $carsService.searchCars(branchID, $scope.currentGroup, $scope.currentManufacturer).then(function (response) {
            $scope.searchText = "חיפושך הניב " + response.length+ " תוצאות ";
       
            $scope.carsList = response;
        })
    };
    $scope.order = function (car) {
        console.log(car);
    };
});