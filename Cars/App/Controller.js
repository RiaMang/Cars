(function () {
    var app = angular.module('app');

    app.controller('ctrl', ['svc', 'fakesvc', '$modal', function (svc, fakesvc,$modal) {
        var scope = this;
        scope.options = {
            years: [],
            makes: [],
            models: [],
            trims: []
        }
        scope.cars = [];
        scope.carDetails = [];
        scope.filters = {
            year: '',
            make: '',
            model: '',
            trim: '',
            paging: true,
            page: '0',
            perpage: '10',
            filter: '',
            total: null
        }
        scope.id = 0;
        scope.selectedCars = [];

        scope.getYears = function () {
            svc.getYears().then(function (result) {
                scope.options.years = result;
            });
        }

        scope.clearFilters = function()
        {
            scope.options.makes = [];
            scope.options.models = [];
            scope.options.trims = [];
            scope.filters = {
                year: '',
                make: '',
                model: '',
                trim: '',
                paging: true,
                page: '0',
                perpage: '10',
                filter: '',
                total: null
            }
        }


        scope.getMakes = function () {
            svc.getMakes(scope.filters).then(function (result) {
                scope.options.makes = result;
                scope.options.models = [];
                scope.options.trims = [];
                scope.filters.make = '';
                scope.filters.model = '';
                scope.filters.trim = '';
                scope.getCars();
            });
        }

        scope.getModels = function () {
            svc.getModels(scope.filters).then(function (result) {
                scope.options.models = result;
                scope.options.trims = [];
                scope.filters.model = '';
                scope.filters.trim = '';
                scope.cars = [];
                scope.getCars();
            });
        }

        scope.getTrims = function () {
            svc.getTrims(scope.filters).then(function (result) {
                scope.options.trims = result;
                scope.filters.trim = '';
                scope.getCars();
            });
        }

        scope.getCars = function () {

            var f = angular.copy(scope.filters);
            f.page++;

            svc.getCars(f).then(function (result) {
                scope.cars = result;

            });

            svc.getCarsCount(scope.filters).then(function (response) {
                console.log(response)
                scope.filters.total = response;
            });
        }

        scope.getYears();

        scope.getDetails = function (id) {
            
            console.log(id);
            
            svc.getDetails(id).then(function(response) {
            scope.carDetails = response;
            });
            console.log("car details: "+scope.carDetails.Recalls);
            scope.open();
        }

        scope.open = function (id) {
            console.log("Id in open "+id)
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'carModal.html',
                controller: 'carModalCtrl as cm',
                size: 'lg',
                resolve: {
                    car: function() {
                        return svc.getDetails(id)
                    }
                 }
            });

            modalInstance.result.then(function () {
                
            }, function () {

            });
        };

        

        


        
    }]);

    angular.module('app').controller('carModalCtrl', function ($modalInstance, car) {

        var scope = this;

        scope.car = car;

        scope.ok = function () {
            $modalInstance.close();
        };

        scope.cancel = function () {
            $modalInstance.dismiss();
        };
    })

})();
