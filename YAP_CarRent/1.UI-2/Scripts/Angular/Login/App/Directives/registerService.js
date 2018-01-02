(function () {
    angular.module("loginApp").factory("$registerService", function ($http) {


        console.log("registerService is up!");

        function sendRestoerPass(_email) {

          return  $http({
                method: "POST",
                url: "/Login/register?email=" + _email
            }).then(function mySuccess(response) {
                return response.data;
            }, function myError(response) {
                return response.statusText;
            });


        }
        function tzValidation(_tz) {

            return $http({
                method: "POST",
                url: "/Login/TZ?tz=" + _tz
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
     