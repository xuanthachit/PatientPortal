﻿'use strict';

angular.module('spaApp')
    .controller("DoctorsController", [
    "$scope", "$location", "$route", '$rootScope', "Params", "DoctorsServices", "LoadJsService", "PagerService",
    function ($scope, $location, $route, $rootScope, Params, DoctorsServices, LoadJsService, PagerService) {
        $scope.pager = {};
        var Id = $route.current.params.userId;

        $scope.setPage = function (page) {
            var para = { pageIndex: page, numberInPage: Params.numberPerPage, search: $scope.searchValue };

            DoctorsServices.getDoctorList(para, function (data) {
                $scope.doctors = data.UserListViewModels;

                $scope.totalItems = data.TotalItem;

                if (page < 1 || (page > $scope.pager.totalPages && $scope.pager.totalPages != null)) {
                    return;
                }
                // get pager object from service
                $scope.pager = PagerService.GetPager($scope.totalItems, page, Params.numberPerPage);
                $scope.pager.totalPages = $scope.pager.totalPages;

                LoadJsService.calJqueryAccordion();
                LoadJsService.callJqueryMain();

                if (page > 1) {
                    LoadJsService.calJquery();
                    LoadJsService.calJqueryMigrate();
                    LoadJsService.calJqueryBa();
                    LoadJsService.calJqueryUI();
                    LoadJsService.calJqueryEA();
                    LoadJsService.calJqueryCaRoul();
                    LoadJsService.calJquerySliderControl();
                    LoadJsService.calJquerySlider();
                    LoadJsService.calJqueryAccordion();
                    LoadJsService.calJqueryTimeago();
                    LoadJsService.calJqueryHint();
                    LoadJsService.calJqueryIO();
                    LoadJsService.calJqueryIOMas();
                    LoadJsService.calJqueryFanc();
                    LoadJsService.calJqueryQtip();
                    LoadJsService.calJqueryBlockUI();
                    LoadJsService.callJqueryMain();
                }
            });
        }

        $scope.setPage(1);
    }]);