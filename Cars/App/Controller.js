(function () {
    var app = angular.module('app');

    app.controller('ctrl', ['svc', 'fakesvc', function (svc, fakesvc) {
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
            total: 60506
        }
        
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

        scope.getDetails = function (id) {
            
            console.log(id);
            
            svc.getDetails(id).then(function(response) {
            scope.carDetails = response;
            });
            //$scope.open();
    }
        //$scope.open = function () {
        //    $scope.showModal = true;
        //};

        //$scope.ok = function () {
        //    $scope.showModal = false;
        //};

        //$scope.cancel = function () {
        //    $scope.showModal = false;
        //};

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
    }]);
})();