﻿angular.module('MyApp').controller('detailController', ['$scope', 'PostService', '$filter', '$route', '$timeout', '$rootScope', 'MetaService',
    function ($scope, PostService, $filter, $route, $timeout, $rootScope, MetaService) {
        $rootScope.metaservice = MetaService;
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
            var Id = $route.current.params.id;
            PostService.get({ Id: Id }, function (data) {
                if (data != null) {
                    $scope.post = data;
                    $timeout(function () {
                        //set meta 
                        $rootScope.metaservice.setMetaPage($scope.post.TitleTrans, $scope.post.TitleSEO, $scope.post.DescriptionSEO);
                        var robots = getMetaRobot(data);
                        $rootScope.metaservice.setMetaPost($scope.post.Canonical, $scope.post.BreadcrumbsTitle, robots);
                        $rootScope.metaservice.setMetaFace(window.location.href, "Bài viết", $scope.post.TitleTrans, $scope.post.DescriptionTrans, $scope.post.Image);

                        $scope.getAdvertise();

                        //list post
                        $scope.Posts = data.lstPostListViewModel;
                        $scope.isShow = true;
                    }, 500);
                }
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

        function getMetaRobot(data) {
            var robotArr = [];
            if (data.MetaRobotIndex == 1) {
                robotArr.push('index');
            }
            if (data.MetaRobotFollow) {
                robotArr.push('follow');
            }
            if (data.MetaRobotAdvanced) {
                robotArr.push('advance');
            }
            var robots = '';
            if (robotArr.length > 1) {
                for (var i = 0; i < robotArr.length - 1; ++i) {
                    robots = robots + robotArr[i] + ', ';
                }
                robots = robots + robotArr[robotArr.length - 1];
            }

            return robots;
        }
    }]);