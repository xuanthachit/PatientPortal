﻿appManagement.controller('detailController', ['$scope', 'PostService', '$filter', '$route', '$timeout',
    function ($scope, PostService, $filter, $route, $timeout) {
    $scope.isShow = false;
    //get Id parameter from Url
    var Id = $route.current.params.id;
    $('a[ng-href="/index/blog"]').parent('li').addClass('active');

    $scope.getAdvertise = function () {
        PostService.getAdvertise(function (data) {
            $scope.Advertises = data;
        });
    }

    //get data of view
    $scope.queryView = function () {
        PostService.query({ Id: Id, languageCode: 'vi' }, function (data) {
            $scope.getAdvertise();
            $timeout(function () {
                $scope.post = data.PostViewModel;
                $scope.Posts = data.lstPostListViewModel;
                $scope.isShow = true;
            }, 500);
        });
    };

        //get post detail
    $scope.viewDetail = function (Id) {
        PostService.get({ Id: Id }, function (data) {
            $('html, body').animate({ scrollTop: $('.postDetail').offset().top - 120 }, 1000);
            $scope.post = data;
        });
    }

    $scope.viewVideo = function (item) {
        $('#viewVideo').modal('toggle');
        if (item.Type > 0) {
            $scope.showVideo = true;
            $scope.thumbVideo = item.Resouce;
            $scope.TitleVideo = item.Name;
        }
        else {
            $scope.showVideo = false;
            $scope.thumbImage = item.Image;
            $scope.TitleAdvertise = item.Name;
        }
    };

}]);