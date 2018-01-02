ordersApp.controller('ordersController', function ($scope, $ordersService, $carsService) {
    $scope.title = "רשימת הזמנות";
    $scope.messageIS = 'תהנה מהרגע';
    $scope.empty = "לא נמצאו הזמנות או שאינך מחובר למערכת";
    $ordersService.getCurrent().then(function (response) {
        console.table(response);
        $scope.currentUser = response;
        $scope.Email = $scope.currentUser.Email;
        console.table($scope.users);
        getOrders($scope.currentUser.TZ);

    });


    if ($scope.Email != "" && $scope.Email)
    {
        getOrders($scope.Email)
    }
    $ordersService.getAllUsers().then(function (response) {
        console.table(response);
        $scope.users = response;
        console.table($scope.users);


    });
    $scope.changeUser = function () {
        console.log($scope.Email + " <--");
        $scope.currentUser = $scope.users.find(u=>u.Email == $scope.Email);
        console.log($scope.currentUser.FirstName + " <--");
        getOrders($scope.currentUser.TZ);

    };


  
    function getOrders(tz) {
        if (tz) { 
        $ordersService.getAllOrders(tz).then(function (response) {
            console.table(response);
            $scope.oredersList = response;
            console.table($scope.oredersList);


        });

        }


    };

    function IsReturned(returned)
    {
        return(returned.split('/')[1])
            

    }














});
