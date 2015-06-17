(function () {
    angular.module('app').factory('svc', ['$http', '$q', function ($http, $q) {
        var service = {};

        service.getYears = function () {
           
            return $http.post('/api/car/GetYears').then(function (response) {
                return response.data;
            });
        }

        service.getMakes = function (selected) {
            return $http.post('/api/car/GetMakes', selected).then(function (response) {
                return response.data;
            });
            
            
        };

        service.getModels = function (selected) {
            return $http.post('/api/car/GetModels', selected).then(function (response) {
                return response.data;
            });
        };

        service.getTrims = function (selected) {
            return $http.post('/api/car/GetTrims', selected).then(function (response) {
                return response.data;
            });
        };

        service.getCars = function (selected) {
            
            return $http.post('/api/car/GetCars', selected).then(function (response) {
                return response.data;
            });
        }

        service.getDetails = function (id) {
            var Id = {
                id: id
            }
            return $http.post('/api/car/GetCarDetails', Id).then(function (response) {
                return response.data;
            });
        }

        service.getCarsCount = function (filter) {

            return $http.post('/api/car/GetCarsCount', filter).then(function (response) {
                return response.data;
            });
        }

        //service.getAllFilCars = function (filter, paging) {
        //    var fil = {
        //        filter: filter,
        //        paging: paging
        //    }
        //    return $http.post('/api/car/GetAllFilCars', fil).then(function (response) {
        //        return response.data;
        //    });
        //}

        return service;
    }]);
})();