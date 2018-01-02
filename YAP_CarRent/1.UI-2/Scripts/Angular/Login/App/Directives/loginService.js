(function () {
    angular.module("loginApp").factory("$restorService", function ($http) {


        console.log("restorService is up!");

        function sendRestoerPass(_email) {

          return  $http({
                method: "POST",
                url: "/Login/RestorePassword?email=" + _email
            }).then(function mySuccess(response) {
                return response.data;
            }, function myError(response) {
                return response.statusText;
            });


        }
        return {

            sendRestoerPass: sendRestoerPass
      

        };


    })
})();
     