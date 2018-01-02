usersManagerApp.controller('pagingController', function ($scope, $pagingService) {
    var vm = this;

    vm.dummyItems = _.range(1, 151); // dummy array of items to be paged
    vm.pager = {};
    vm.setPage = setPage(5);

    initController();

    function initController() {
        // initialize to page 1
        vm.setPage(1);
    }

    function setPage(page) {
        if (page < 1 || page > vm.pager.totalPages) {
            return;
        }

        // get pager object from service
        vm.pager = $pagingService.GetPager(vm.dummyItems.length, page);

        // get current page of items
        vm.items = vm.dummyItems.slice(vm.pager.startIndex, vm.pager.endIndex + 1);
    }

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
        $carsService.searchCars(branchID, $scope.currentGroup, $scope.currentManufacturer, $scope.currentMin, $scope.currentMax).then(function (response) {
            $scope.searchText = "חיפושך הניב " + response.length+ " תוצאות ";
       
            $scope.carsList = response;
        })
    };
});