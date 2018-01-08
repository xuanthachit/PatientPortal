﻿appManagement.controller("indexController", [
    "$scope", "$rootScope", "$location", "$route", "Params", "$timeout", "$q", "indexServices",
    function ($scope, $rootScope, $location, $route, Params, $timeout, $q, indexServices) {

        $rootScope.title = "Trang chủ";
        $scope.Post = new Object();
        $scope.firstPost = [];
        $scope.Posts = [];
        $scope.about = [];
        $scope.depImages = [];
        $scope.mail = new Object();

        $scope.loadData = function () {
            var promiseMenu = indexServices.getListMenu();
            var promiseAbout = indexServices.getAbout(Params.languageCode);
            var promiseSlider = indexServices.getSlider();
            var promiseDepartment = indexServices.getDepartment(0);
            var promiseFeature = indexServices.getFeature();
            var promistContact = indexServices.getContact();
            var promisePosts = indexServices.getListPost(Params.languageCode);

            $scope.combineResult = $q.all([
                promiseMenu,
                promiseAbout,
                promiseSlider,
                promiseDepartment,
                promiseFeature,
                promistContact,
                promisePosts
            ]).then(function (resp) {

                    //Menu
                    if (resp[0].data != null) {
                        //$scope.menus = resp[0].data;
                        $scope.menus = nested(resp[0].data);
                    }

                    // About
                    if (resp[1].data != null) {
                        $scope.about = resp[1].data[0];
                    }

                    // Slider
                    if (resp[2].data != null){
                        $scope.sliders = resp[2].data;
                        $timeout(function () {
                            $("#main-slider").find('.owl-carousel').owlCarousel({
                                slideSpeed: 500,
                                paginationSpeed: 500,
                                singleItem: true,
                                navigation: true,
                                navigationText: [
                                "<i class='fa fa-angle-left'></i>",
                                "<i class='fa fa-angle-right'></i>"
                                ],
                                afterInit: progressBar,
                                afterMove: moved,
                                startDragging: pauseOnDragging,
                                transitionStyle: "fadeUp"
                            });
                        }, 500);
                    }

                    // Department
                    if (resp[3].data != null){
                        $scope.departments = resp[3].data;
                        if (resp[3].data[0] != null) {
                            if(resp[3].data[0].length > 0){
                                $scope.getDepartmentDetail(resp[3].data[0].Id);
                            }
                        }
                    }

                    // Feature
                    if (resp[4].data != null){
                        $scope.features = resp[4].data;
                    }

                    // Contact
                    if (resp[5].data != null) {
                        $scope.contact = resp[5].data;
                    }

                    // Post
                    if (resp[6].data != null) {
                        $scope.firstPost = resp[6].data[0];
                        resp[6].data = resp[6].data.slice(1);
                        $scope.Posts = resp[6].data;
                    }
            }).finally(function () {

                $timeout(function () {
                    $("#main-slider").find('.owl-carousel').owlCarousel({
                        slideSpeed: 500,
                        paginationSpeed: 500,
                        singleItem: true,
                        navigation: true,
                        navigationText: [
                        "<i class='fa fa-angle-left'></i>",
                        "<i class='fa fa-angle-right'></i>"
                        ],
                        afterInit: progressBar,
                        afterMove: moved,
                        startDragging: pauseOnDragging,
                        transitionStyle: "fadeUp"
                    });
                }, 200);
            });
        }

        $scope.sendmail = function (mail) {
            indexServices.postSendMail(mail);
        }

        function progressBar(elem) {
            $elem = elem;
            //build progress bar elements
            buildProgressBar();
            //start counting
            start();
        }

        function moved() {
            //clear interval
            clearTimeout(tick);
            //start again
            start();
        }

        //pause while dragging 
        function pauseOnDragging() {
            isPause = true;
        }

        //create div#progressBar and div#bar then append to $(".owl-carousel")
        function buildProgressBar() {
            $progressBar = $("<div>", {
                id: "progressBar"
            });
            $bar = $("<div>", {
                id: "bar"
            });
            $progressBar.append($bar).appendTo($elem);
        }

        function start() {
            //reset timer
            percentTime = 0;
            isPause = false;
            //run interval every 0.01 second
            tick = setInterval(interval, 10);
        };

        //$scope.getDepartmentDetail = function (id) {
        //    indexServices.getDepartment(id).then(function (d) {
        //        $scope.depImages = d.data; // Success
        //    }, function () {
        //        alert('Error Occurred !!!'); // Failed
        //    });
        //};

        function interval() {
            if (isPause === false) {
                percentTime += 1 / 7;
                $bar.css({
                    width: percentTime + "%"
                });
                //if percentTime is equal or greater than 100
                if (percentTime >= 100) {
                    //slide to next item 
                    $elem.trigger('owl.next')
                }
            }
        }

        $scope.scrollMenu = function () {
            var menu = $route.current.params.menu;
            if (menu == 'services') {
                $('html, body').animate({ scrollTop: $('#services').offset().top + 450 }, 1000);
            }
            if (menu == 'portfolio') {
                $('html, body').animate({ scrollTop: $('#portfolio').offset().top + 450 }, 1000);
            }
            if (menu == 'about') {
                $('html, body').animate({ scrollTop: $('#about').offset().top + 450 }, 1000);
            }
            if (menu == 'blog') {
                $('html, body').animate({ scrollTop: $('#blog').offset().top + 450 }, 1000);
            }
            if (menu == 'get-in-touch') {
                $('html, body').animate({ scrollTop: $('#get-in-touch').offset().top + 450 }, 1000);
            }
        };

        //loadData();


        //$scope.$watch('$viewContentLoaded', function () {
        //    // do something
        //    $(".dcdcdc li").first().addClass("active")
        //});
        //angular.element(document).ready(function () {
        //    var a = $(".dcdcdc li").first();
        //    a.addClass("active");
        //});
    }
]);