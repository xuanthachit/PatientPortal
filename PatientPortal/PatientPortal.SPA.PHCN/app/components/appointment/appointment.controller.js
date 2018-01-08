﻿angular.module('spaPHCN')
    .controller('AppointmentController', ['$scope', 'Params', 'AppointmentService', '$route', '$location', 'ModalService', 'DoctorListServices',
        function AppointmentController($scope, Params, AppointmentService, $route, $location, ModalService, DoctorListServices) {

            $scope.subTitle = "Đặt lịch khám";
            $scope.appointment = {
                Date: moment(new Date()).format("DD/MM/YYYY"),
                Time: '',
                PhysicianId: '',
                PatientId: '',
                Symptom: '',
                Status: 1,
                PatientName: '',
                PatientAddress: '',
                PatientEmail: '',
                PatientPhone: '',
                PatientGender: null,
                PatientDoB: null
            };
            $scope.optionsChooseDoctor = [
                { id: "0", name: "Chọn bác sĩ của bạn" },
                { id: "1", name: "Tư vấn một bác sĩ cho tôi" },
                { id: "2", name: "Tìm bác sĩ mà bạn biết" }
            ];

            $scope.gioitinhs = [
                { id: "1", name: "Nam" },
                { id: "2", name: "Nữ" }
            ];

            AppointmentService.queryDoctors({ search: "" }, function (data) {
                $scope.doctors = data;
            });

            $scope.giohens = getGioKham();

            $scope.changeDoctor = function (value) {
                if (value != null) {
                    DoctorListServices.getProfile({ id: value }, function (data) {
                        $scope.doctor = data;
                        $("#quickviewProfile").show(500);

                        if ($scope.appointment.Date != null) {

                            var from = $scope.appointment.Date.split("/");
                            var date = new Date(from[2], from[1] - 1, from[0], 0, 0, 0);
                            $scope.timeStamp = date.getTime();
                            AppointmentService.getScheduleExamine({ userId: value, startTime: $scope.timeStamp }, function (data) {
                                if (data != null) {
                                    $scope.giohens = data;
                                    $scope.appointment.Time = data[0];
                                }
                            });
                        }
                    });
                } else {
                    $("#quickviewProfile").hide(500);
                }
            }

            $scope.changeOrderDate = function (date) {
                var doctorId = $scope.appointment.PhysicianId;
                if (doctorId == "") {
                    $scope.giohens = getGioKham();
                }
                else {
                    $scope.appointment.Date = date;
                    var from = date.split("/");
                    date = new Date(from[2], from[1] - 1, from[0], 0, 0, 0);
                    $scope.timeStamp = date.getTime();
                    AppointmentService.getScheduleExamine({ userId: doctorId, startTime: date.getTime() }, function (data) {
                        if (data != null) {
                            $scope.giohens = data;
                        }
                    });

                    $("#dateMessage").show(500);
                }
            }

            $scope.chooseADoctor = function (value, idModal) {
                $scope.appointment.PhysicianId = value;
                AppointmentService.getDoctor({ id: value }, function (data) {
                    $scope.doctor = data;
                    $scope.option = { id: "0", name: "Chọn bác sĩ của bạn" };
                    $("#choosedoctor").show(500);
                    $("#quickviewProfile").show(500);
                    $("#searchDoctor").hide(500);
                    
                    $scope.closeModal(idModal);
                });
            }

            $scope.closeModal = function (idModal) {
                ModalService.Close(idModal);
            }

            $scope.doSearch = function (idModal) {
                AppointmentService.queryDoctors({ search: $scope.searchText }, function (data) {
                    if (data.length > 0) {
                        $scope.doctors = data;
                        //$scope.doctor = data[0];
                        //$scope.appointment.PhysicianId = data[0].Id;
                        ModalService.Open(idModal);
                    }
                    else {
                        $scope.appointment.PhysicianId = "";
                    }
                });
            }

            $scope.viewDetail = function (id, idModal) {
                var startTime = Date.parse(new Date());
                AppointmentService.queryScheduleExamine({ userId: id, startTime: startTime }, function (data) {
                    $scope.schedules = data;

                    ModalService.Open(idModal);

                });
            }

            $scope.changeType = function (option) {
                //$scope.giohens = null;
                if (option == 0) {
                    $("#choosedoctor").show(500);
                    $("#doctorSelected").show(500);
                    $("#searchDoctor").hide(500);
                    $("#quickviewProfile").hide(500);
                }
                if (option == 1) {
                    $("#searchDoctor").hide(500);
                    $("#doctorSelected").hide(500);
                    $("#choosedoctor").hide(500);
                    $scope.giohens = getGioKham();
                }
                if (option == 2) {
                    $("#searchDoctor").show(500);
                    $("#doctorSelected").hide(500);
                    $("#quickviewProfile").hide(500);
                    $("#choosedoctor").hide(500);
                }
            }

            $scope.makeAppointment = function (appointment) {
                
                var hms = appointment.Time;   // your input string
                var a = hms.split(':'); // split it at the colons

                // minutes are worth 60 seconds. Hours are worth 60 minutes.
                appointment.Time = (+a[0]) * 60 + (+a[1]);
                appointment.Status = 1;
                appointment.PatientGender = $scope.gioitinh.id;
                appointment.Date = formatToDateTime(appointment.Date);
                if (appointment.PatientDoB != null) {
                    appointment.PatientDoB = formatToDateTime(appointment.PatientDoB);
                }
                AppointmentService.insertAppointment({ action: 'I' }, appointment, function (success) {
                    $location.url('/')
                }, function (error) { });
            }

            function formatToDateTime(date) {
                var from = date.split("/");
                return new Date(Date.UTC(from[2], from[1] - 1, from[0]));
            }

            function getGioKham() {
                var now = moment().hours(8).minutes(0).seconds(0).milliseconds(0);
                var max = moment().hours(19).minutes(0).seconds(0).milliseconds(0);
                var gio = [];
                var duration = moment.duration({ 'minutes': 15 });
                now = now.add(duration);
                while (now < max) {
                    gio.push(now.format('HH:mm'));
                    now = now.add(duration);
                }
                return gio;
            };
        }
    ]);