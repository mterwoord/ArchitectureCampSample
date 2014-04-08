angular.module('starter.services', [])
    .factory('sessionsService', function ($http) {
        var baseUrl = "http://192.168.43.87/conferences/api/";

        return {
            all: function () {
                return $http.get(baseUrl + "sessions/list");
            },
            get: function (sessionId) {
                return $http.get(baseUrl + "sessions/list/" + sessionId);
            },
            rate: function(rating, sessionId, speakerId) {
                $http.post(baseUrl + "ratings/list", { Rate: rating, SessionId: sessionId, SpeakerId: speakerId });
            }
        }
    });
