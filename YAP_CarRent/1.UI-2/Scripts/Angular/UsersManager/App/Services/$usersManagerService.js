(function () {
    angular.module("usersManagerApp").factory("$usersManagerService", function ($http) {


        console.log("$User Manager Service is up!");

      
        function initPageParams(perPage) {

            return $http({
                method: "GET",
                url: "/Users/InitPage?perPage=" + perPage
            }).then(function mySuccess(response) {
                return response.data;
            }, function myError(response) {
                return response.statusText;
            });


        }

    

        return {

            initPageParams: initPageParams,
         
        
      

        };


    })
})();
     