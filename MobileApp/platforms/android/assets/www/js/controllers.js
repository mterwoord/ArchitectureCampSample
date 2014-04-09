angular.module('starter.controllers', [])

    .controller('DashController', function ($scope) {
    })

    .controller('SessionsController', function ($scope, sessionsService) {
        sessionsService.all().then(function (result) {
            $scope.sessions = result.data;
        });
    })

    .controller('SessionDetailsController', function ($scope, $stateParams, sessionsService) {
        $scope.rateIt = function() {
            sessionsService.rate(4, $scope.session.SessionBaseId, $scope.session.Speaker1Id);
        };

        sessionsService.get($stateParams.sessionId).then(function (result) {
            $scope.session = result.data;
        });
    })

    .controller('AccountController', function ($scope) {
    });
