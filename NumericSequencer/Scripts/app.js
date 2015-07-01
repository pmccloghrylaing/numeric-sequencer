///<reference path="angular.js" />

var sequencerApp = angular.module('sequencerApp', []);

sequencerApp.controller('SequencerController', [
    '$scope', '$http',
    function ($scope, $http) {
        
        var self = this;

        self.integer = null;
        self.requestedInteger = null;
        self.result = null;

        self.submit = function () {
            self.result = null;
            var integer = self.integer;
            $http.get('/api/sequences/' + integer)
                .then(function (result) {
                    self.requestedInteger = integer;
                    self.result = result.data;
                });
        };
    }
]);
