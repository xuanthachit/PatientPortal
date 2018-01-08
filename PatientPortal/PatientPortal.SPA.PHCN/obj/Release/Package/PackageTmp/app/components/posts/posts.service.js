﻿angular.module('spaPHCN').factory('PostsServices', ['$resource', 'Params',
    function ($resource, Params) {
        return $resource(Params.rootUrl + 'SPAPost', {}, {
            queryPost: {
                method: 'GET',
                url: Params.rootUrl + "SPAPost/GetListPost?languageCode=:languageCode&priority=:priority&numTop=:numTop",
                isArray: true,
                params: { languageCode: '@languageCode', priority: '@priority', numTop: '@numTop' }
            }
        });
    }]);