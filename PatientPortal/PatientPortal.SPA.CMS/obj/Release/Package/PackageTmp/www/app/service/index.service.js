﻿appManagement.service("indexServices", ["$http", 'Params',
    function ($http, Params) {
        // Get List Post
        this.getListPost = function (languageCode) {
            var response = $http.get(Params.rootUrl + "SPAPost/GetListPost?languageCode=" + languageCode );
            return response;
        };

        // get About
        this.getAbout = function (languageCode) {
            var response = $http.get(Params.rootUrl + "SPAPost/GetPostAbout?languageCode=" + languageCode);
            return response;
        };

        // get Slider
        this.getSlider = function () {
            var response = $http.get(Params.rootUrl + "Slider/Get");
            return response;
        };

        // get Department
        this.getDepartment = function () {
            var response = $http.get(Params.rootUrl + "Department/Get");
            return response;
        };

        // get Feature
        this.getFeature = function() {
            var response = $http.get(Params.rootUrl + "Feature/Get");
            return response;
        };

        // get Contact
        this.getContact = function() {
            var response = $http.get(Params.rootUrl + "Configuration/Get");
            return response;
        };

        //get Menu
        this.getListMenu = function () {
            var response = $http.get(Params.rootUrl + "Category/GetMenu");
            return response;
        };

        // post SendMail
        this.postSendMail = function (mail) {
            $http({
                url: Params.rootUrl + "Configuration/SendMail",
                method: "POST",
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                data: $.param(mail)
            }).then(function (data, status, headers, config) {
                $scope.status = status;
            }).error(function (data, status, headers, config) {
                $scope.status = status;
            });
        };
    }
]);