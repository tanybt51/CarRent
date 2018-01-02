var carService = function ($http) {


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
    function searchCars(branch, _from, _to, currentGroup, currentManufacturer, currentMin, currentMax) {
        var from = new Date(_from).toLocaleString();
        var to = new Date(_from).toLocaleString();

        //int _branch,string _group,string _manufacturer,int _min,int _max)
        return $http({
            method: "GET",
            url: "/Cars/GetCarsSearch?_branch=" + branch + "&_from=" + from + "&_to=" + to + "&_group=" + currentGroup + "&_manufacturer=" + currentManufacturer + "&_min=" + currentMin + "&_max=" + currentMax
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


}