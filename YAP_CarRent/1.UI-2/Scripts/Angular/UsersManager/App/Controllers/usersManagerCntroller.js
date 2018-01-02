usersManagerApp.controller('usersManagerController', function ($scope, $usersManagerService) {
    $scope.title = "ניהול משתמשים";
    $usersManagerService.initPageParams(20).then(function(pageParams){
        $scope.userList = pageParams.Users;
        $scope.rolesList = pageParams.Roles;
        $scope.displayCount = pageParams.DisplayCount;
        $scope.currentPage = pageParams.CurrentPage;
        $scope.pages = pageParams.Pages;
    });
    $scope.getNumber = function (num) {
        return new Array(num);
    }
    $scope.toPage = function (page) {
        alert('page is ' + page);


    };
    // clearing the search inputs



    $scope.changeManufacturer = function () {
        $carsManagerService.getAllManufacturerModels($scope.currentManufacturer).then(function (response) {
            console.table(response);
            $scope.modelList = response;

        });


    }
    $scope.datePars = function (date) {
        var d = parseInt(date.split('(')[1].split(')')[0]);
        var d2 = new Date(d);
        var d3 = d2.getDate() + '/' + (d2.getMonth() + 1) + '/' + (d2.getFullYear()%1000);
        return (d3);


    }
    //$scope.changeBranch = function () {
    //    $carsService.getAllBranches().then(function (response) {
    //        $scope.branchesList = response;
    //    })
    //}


});