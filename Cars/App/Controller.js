(function () {
    var app = angular.module('app');

    app.controller('ctrl', ['svc', 'fakesvc', function (svc, fakesvc) {
        var scope = this;
        scope.selected = {
            year: '',
            make: '',
            model: '',
            trim: ''
        }
        scope.options = {
            years: [],
            makes: [],
            models: [],
            trims: []
        }
        scope.cars = [];
        scope.paging = {
            page: '1',
            perpage: '10'
        }
        scope.tab = {
        total: 60506,
        search: '',
        }
        scope.allCars = false;

        scope.getYears = function () {
            svc.getYears().then(function (result) {
                scope.options.years = result;
                //scope.options.makes = [];
                //scope.options.models = [];
                //scope.options.trims = [];
                //scope.selected.make = '';
                //scope.selected.model = '';
                //scope.selected.trim = '';
            });
        }

        scope.getMakes = function () {
            svc.getMakes(scope.selected).then(function (result) {
                scope.options.makes = result;
                scope.options.models = [];
                scope.options.trims = [];
                scope.selected.make = '';
                scope.selected.model = '';
                scope.selected.trim = '';
                scope.cars = [];
            });
        }

        scope.getModels = function () {
            svc.getModels(scope.selected).then(function (result) {
                scope.options.models = result;
                scope.options.trims = [];
                scope.selected.model = '';
                scope.selected.trim = '';
                scope.cars = [];
            });
        }

        scope.getTrims = function () {
            svc.getTrims(scope.selected).then(function (result) {
                scope.options.trims = result;
                scope.seleted.trim = '';
                scope.cars = [];
            });
        }
        
        scope.getCars = function () {
            svc.getCars(scope.selected).then(function(result){
                scope.cars = result;
        });
        }

        scope.checkAllCars = function () {
            if(scope.allCars === true)
            {
                svc.getAllCars(scope.paging).then(function (result) {
                    scope.cars = result;
                });
            } else {
                svc.getCars(scope.selected).then(function (response) {
                    scope.cars = response;
                });
            }
        }

        scope.changeTable = function (currentPage, pageItems, filterBy, filterByFields, orderBy, orderByReverse) {
            if (filterBy != null || filterBy != '')
            {
                console.log(filterBy);
                scope.paging.page = currentPage;
                scope.paging.perpage = pageItems;
                svc.getCarCount(filterBy).then(function (result) {
                    scope.tab.total = result;
                });
                //scope.total = 60506;

                svc.getAllFilCars(filterBy, scope.paging).then(function (result) {
                    scope.cars = result;
                });
            } else {
            scope.paging.page = currentPage;
            scope.paging.perpage = pageItems;
            scope.tab.total = 60506;
            svc.getAllCars(scope.paging).then(function (result) {
                scope.cars = result;
            });
            
            }
           }
        
        scope.getYears();
    }]);
})();