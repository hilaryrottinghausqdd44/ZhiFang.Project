$(function () {
    //页面所有功能对象
    var shell_win = {
        /**考勤枚举*/
        /**系统*/
        system: {
            /**微信ID*/
            open_id: Shell.util.Cookie.getCookie("openId"),
            /**账号ID*/
            user_id: Shell.util.Cookie.getCookie("userId"),
            /**用户昵称*/
            UserName: null,
            /**用户手机号码*/
            MobileCode: null,
            /**头像地址*/
            HeadImgUrl: null,
            /**系统初始化*/
            init: function () {
                var me = this,
					url = "/WeiXinAppService.svc/ST_UDTO_SearchBWeiXinAccountByHQL",
					fields = "BWeiXinAccount_UserName,BWeiXinAccount_MobileCode,BWeiXinAccount_HeadImgUrl",
					id = me.open_id;

                if (!id) {
                    me.show_error("微信ID不存在 ！");
                    return;
                }

                url = Shell.util.Path.rootPath + url + "?page=-1&rows=-1&fields=" + fields +
					"&where=WeiXinAccount='" + id + "'";

                //获取账户信息
                Shell.util.Server.ajax({
                    async: false,
                    url: url
                }, function (data) {
                    if (data.success) {
                        if (!data.value || !data.value.list || data.value.list.length == 0) {
                            me.show_error("没有获取到用户数据！");
                            return;
                        }
                        var obj = data.value.list[0];
                        me.UserName = obj.UserName;
                        me.MobileCode = obj.MobileCode;
                        me.HeadImgUrl = obj.HeadImgUrl;
                        //显示页面
                        shell_win.show_page();
                    } else {
                        me.show_error(data.msg);
                    }
                });
            },
            /**ianshi错误信息*/
            show_error: function (value) {
                var msg = '<div style="text-align:center;margin:50px;">' + value + '</div>';
                $("#page_home").html(msg);
            }
        },
        /**手机号码信息*/
        cellphone: {
            /**手机号码是否已经被注册*/
            is_used: null,
            /**最后一次手机号码*/
            last_value: "",
            /**提示信息*/
            info: {
                /**没有输入手机号的提示信息*/
                is_empty: "请输入手机号码",
                /**手机号已被注册的提示信息*/
                is_used: "手机号已经被注册，请换一个手机号",
                /**输入的手机号格式不正确*/
                is_unvalid: "输入的手机号格式不正确",
                /**手机号码正在判断是否已经被注册*/
                is_used_valid: "手机号码正在判断是否已经被注册"
            },
            /**获取手机号框的值*/
            getValue: function () {
                var value = $("#cellphone_number").val();
                value = !value ? "" : $.trim(value);
                return value;
            }
        },
        /**验证码信息*/
        captcha: {
            server_value: null,
            /**提示信息*/
            info: {
                is_error_length: "验证码必须是6位",
                is_error_not_same: "验证码错误"
            },
            /**获取验证码框的值*/
            getValue: function () {
                var value = $("#captcha").val();
                value = !value ? "" : $.trim(value);
                return value;
            },
            /**显示验证码信息*/
            show_captcha_info: function (last_times) {
                var html = "";
                if (last_times == 0) {
                    $("#reload_captcha_btn").attr("disabled", false);
                    $("#reload_captcha_div").show();
                    $("#reload_captcha_btn").html("重新获取验证码");
                } else {
                    $("#reload_captcha_btn").attr("disabled", true);
                    $("#reload_captcha_div").show();
                    $("#reload_captcha_btn").html(last_times + "秒后重新获取验证码");
                }
            },
            /**重新获取验证码*/
            reload: function () {
                var cellphone_number = shell_win.cellphone.getValue(),
					captcha_value = shell_win.captcha.getValue();

                //校验需要提交的信息
                var isValid = shell_win.valided.next_params(cellphone_number, captcha_value);

                if (!isValid) return;

                ShellComponent.mask.to_server("验证验获取中...");
                //服务器校验手机号是否已被注册,没注册就发送验证码
                shell_win.server.cellphone_number_is_used({
                    cellphone_number: cellphone_number
                }, function (data) {
                    ShellComponent.mask.hide();
                    if (data.success) {
                        shell_win.captcha.server_value = data.value.vaildcode;
                        var max_times = data.value.TimeOut;
                        shell_win.time_meter.start(function () {
                            if (max_times-- == 0) {
                                shell_win.time_meter.end();
                            } else {
                                shell_win.captcha.show_captcha_info(max_times);
                            }
                        });
                    } else {
                        ATCommon.info.error(data.msg);
                    }
                });
            }
        },
        /**显示页面*/
        show_page: function () {
            //初始化头像和名称
            var photo = shell_win.system.HeadImgUrl ? shell_win.system.HeadImgUrl :
				Shell.util.Path.uiPath + "/img/icon/DefaultHeadImg.jpg";
            $("#account_img").attr("src", photo);
            $("#account_name").html(shell_win.system.UserName);
            $("#register_div").show();
            $("#page1").show();
        },
        /**服务端处理*/
        server: {
            /**服务器校验手机号是否已经被注册，没注册就发送验证码*/
            cellphone_number_is_used: function (params, callback) {
                Shell.util.Server.ajax({
                    showError: true,
                    url: Shell.util.Path.rootPath + "/WeiXinAppService.svc/VaildMobileCode?MobileCode=" + params.cellphone_number
                }, callback);
            },
            /**提交注册信息*/
            register: function (params, callback) {
                Shell.util.Server.ajax({
                    url: Shell.util.Path.rootPath + "/WeiXinAppService.svc/ST_UDTO_UpdateBWeiXinAccountMobileCodeByOpenid?MobileCode=" + params.cellphone_number
                }, callback);
            }
        },
        /**计时器*/
        time_meter: {
            /**周期*/
            cycle: 1000,
            /**执行对象*/
            timer: null,
            /**启动*/
            start: function (fun) {
                this.timer = setInterval(fun, this.cycle);
            },
            /**终止*/
            end: function () {
                clearInterval(this.timer);
            }
        },
        hidecontrol: function () {
            $('#late_span').hide();
            $('#offsite_signin_span').hide();
            $('#leaveearly_span').hide();
            $('#offsite_signout_span').hide();
        },
        /**获取签到签退状态信息*/
        LoadSignInfo: function () {
            ShellComponent.mask.loading();
            Shell.util.Server.ajax({
                type: "get",
                url: Shell.util.Path.rootPath + "/WeiXinAppService.svc/GetATEmpAttendanceEventLogByDTCode"
            }, function (data) {
                ShellComponent.mask.hide();
                if (data.success) {
                    //alert(data.ResultDataValue);
                    //var str = "{\"id\":\"123123123\"}";
                    var jsona = $.parseJSON(data.ResultDataValue);
                    $('#datetimeinfo_span').html(jsona.WeekInfo);
                    //$('#bbb').css('display', 'block');
                    //$('#late_span').addClass('show');
                    if (jsona.SignInId) {
                        $('#signin_btn').addClass('disabled');
                        if (jsona.SignInTime) {
                            $('#signintime_span').html(jsona.SignInTime)
                        }
                        //alert(jsona.SignInType);
                        if (jsona.SignInType) {
                            //alert(jsona.SignInType);
                            $('#late_span').show();
                        }
                        //alert(jsona.SignInIsOffsite);
                        if (jsona.SignInIsOffsite) {
                            //alert('123');
                            $('#offsite_signin_span').show();
                        }
                    }
                    else {
                        $('#late_span').hide();
                        $('#offsite_signin_span').hide();
                    }
                    //alert("123");
                    if (jsona.SignOutId) {
                        $('#signout_btn').addClass('disabled');
                        if (jsona.SignOutTime) {
                            $('#signouttime_span').html(jsona.SignOutTime)
                        }
                        //alert(jsona.SignOutType);
                        if (jsona.SignOutType) {
                            $('#leaveearly_span').show();
                        }
                        //alert(jsona.SignOutIsOffsite);
                        if (jsona.SignOutIsOffsite) {
                            //alert('123');
                            $('#offsite_signout_span').show();
                        }
                    }
                    else {
                        $('#leaveearly_span').hide();
                        $('#offsite_signout_span').hide();
                    }
                } else {
                    //shell_win.system.msg.error("content_config_patient_info_msg", data.msg);
                    ATCommon.info.error(data.msg);
                }
            });
        },
        /**签到按钮触摸事件*/
        signin_btn_click: function () {
            $("#sign_in_div").modal('show');
            Shell.util.weixin.getuserlocation(function (latitude, longitude) {
                var clocal = new qq.maps.LatLng(latitude, longitude);
                shell_win.tencentmap.init(clocal, null, document.getElementById('container_in'));
            });
            //alert(Shell.util.weixin.userlocationlatitude)			
        },
        /**签到动作按钮触摸事件*/
        signin_action_btn_click: function () {
            ShellComponent.mask.save();
            var ateventtypename = '签到';
            //var memo = '签到';
            var ateventlogpostion = 'ATEventLogPostion';

            Shell.util.weixin.getuserlocation(function (latitude, longitude) {
                var geocoder = new qq.maps.Geocoder({
                    complete: function (result) {
                        //alert(result.detail.address);
                        var data = {
                            ATEventTypeName: ateventtypename,
                            //Memo: memo,
                            ATEventLogPostion: latitude + ',' + longitude,
                            ATEventLogPostionName: result.detail.address
                        };
                        Shell.util.Server.ajax({
                            type: "post",
                            data: Shell.util.JSON.encode({ entity: data }),
                            url: Shell.util.Path.rootPath + "/WeiXinAppService.svc/ST_UDTO_AddATEmpAttendanceEventLogSignIn"
                        }, function (data) {
                            ShellComponent.mask.hide();
                            $("#sign_in_div").modal('hide');
                            if (data.success) {
                                shell_win.LoadSignInfo();
                                //shell_win.patient.change_member_list(info, data.value);
                                //shell_win.page.back("#" + shell_win.page.lev.L2.now, "#" + shell_win.page.lev.L2.back);
                                //shell_win.patient.to_page();
                            } else {
                                shell_win.system.msg.error("content_config_patient_info_msg", data.msg);
                            }
                        });
                    }
                });
                var lat = parseFloat(latitude);
                var lng = parseFloat(longitude);
                var latLng = new qq.maps.LatLng(lat, lng);

                //调用获取位置方法
                geocoder.getAddress(latLng);
            });
            //alert(Shell.util.weixin.userlocationlatitude)			
        },
        /**签退按钮触摸事件*/
        signout_btn_click: function () {
            $("#sign_out_div").modal('show');
            Shell.util.weixin.getuserlocation(function (latitude, longitude) {
                var clocal = new qq.maps.LatLng(latitude, longitude);
                shell_win.tencentmap.init(clocal, null, document.getElementById('container_out'));
            });
        },
        /**签到动作按钮触摸事件*/
        signout_action_btn_click: function () {
            ShellComponent.mask.save();
            var ateventtypename = '签退';
            //var memo = '签退';
            var ateventlogpostion = 'ATEventLogPostion';

            Shell.util.weixin.getuserlocation(function (latitude, longitude) {
                var geocoder = new qq.maps.Geocoder({
                    complete: function (result) {
                        var data = {
                            ATEventTypeName: ateventtypename,
                            //Memo: memo,
                            ATEventLogPostion: latitude + ',' + longitude,
                            ATEventLogPostionName: result.detail.address
                        };
                        Shell.util.Server.ajax({
                            type: "post",
                            data: Shell.util.JSON.encode({ entity: data }),
                            url: Shell.util.Path.rootPath + "/WeiXinAppService.svc/ST_UDTO_AddATEmpAttendanceEventLogSignOut"
                        }, function (data) {
                            ShellComponent.mask.hide();
                            $("#sign_out_div").modal('hide');
                            if (data.success) {
                                shell_win.LoadSignInfo();
                                //shell_win.patient.change_member_list(info, data.value);
                                //shell_win.page.back("#" + shell_win.page.lev.L2.now, "#" + shell_win.page.lev.L2.back);
                                //shell_win.patient.to_page();
                            } else {
                                shell_win.system.msg.error("content_config_patient_info_msg", data.msg);
                            }
                        });
                    }
                });
                var lat = parseFloat(latitude);
                var lng = parseFloat(longitude);
                var latLng = new qq.maps.LatLng(lat, lng);

                //调用获取位置方法
                geocoder.getAddress(latLng);

            });
            //提交数据

            //alert(Shell.util.weixin.userlocationlatitude)			
        },
        /**上报位置按钮触摸事件*/
        uploadpostion_btn_click: function () {
            $("#uploadpostion_div").modal('show');
            Shell.util.weixin.getuserlocation(function (latitude, longitude) {
                var clocal = new qq.maps.LatLng(latitude, longitude);
                shell_win.tencentmap.init(clocal, null, document.getElementById('container_uploadpostion'));
            });
            //alert(Shell.util.weixin.userlocationlatitude)			
        },
        /**上报位置动作按钮触摸事件*/
        uploadpostion_action_btn_click: function () {
            ShellComponent.mask.save();
            var ateventtypename = '上报位置';
            //var memo = '签到';
            var ateventlogpostion = 'ATEventLogPostion';

            Shell.util.weixin.getuserlocation(function (latitude, longitude) {
                var geocoder = new qq.maps.Geocoder({
                    complete: function (result) {
                        //alert(result.detail.address);
                        var data = {
                            ATEventTypeName: ateventtypename,
                            //Memo: memo,
                            ATEventLogPostion: latitude + ',' + longitude,
                            ATEventLogPostionName: result.detail.address
                        };
                        Shell.util.Server.ajax({
                            type: "post",
                            data: Shell.util.JSON.encode({ entity: data }),
                            url: Shell.util.Path.rootPath + "/WeiXinAppService.svc/ST_UDTO_AddATEmpAttendanceEventLogUploadPostion"
                        }, function (data) {
                            ShellComponent.mask.hide();
                            $("#uploadpostion_div").modal('hide');
                            if (data.success) {
                                shell_win.LoadSignInfo();
                                //shell_win.patient.change_member_list(info, data.value);
                                //shell_win.page.back("#" + shell_win.page.lev.L2.now, "#" + shell_win.page.lev.L2.back);
                                //shell_win.patient.to_page();
                            } else {
                                shell_win.system.msg.error("content_config_patient_info_msg", data.msg);
                            }
                        });
                    }
                });
                var lat = parseFloat(latitude);
                var lng = parseFloat(longitude);
                var latLng = new qq.maps.LatLng(lat, lng);

                //调用获取位置方法
                geocoder.getAddress(latLng);
            });
            //alert(Shell.util.weixin.userlocationlatitude)			
        },
        /**请假动作按钮触摸事件*/
        leave_btn_click: function () {
            location.href = "EmpLeaveEventAdd.html";
        },
        /**签到动作按钮触摸事件*/
        egress_btn_click: function () {
            location.href = "EmpEgressEventAdd.html";
        },
        /**签到动作按钮触摸事件*/
        trip_btn_click: function () {
            location.href = "EmpTripEventAdd.html";
        },
        /**签到动作按钮触摸事件*/
        overtime_btn_click: function () {
            location.href = "EmpOvertimeEventAdd.html";
        },
        /**下一步处理成功后处理*/
        after_next_ok: function () {
            $("#page1").hide();
            $("#page2").show();
        },
        /**注册成功后处理*/
        after_register_ok: function () {
            shell_win.time_meter.end(); //结束计时器
            window.location.href = Shell.util.Path.uiPath + "/index.html";
        },
        /**地图功能*/
        tencentmap: {
            map: null,
            center: null,
            marker: null,
            /**地图初始化*/
            init: function (center, markers,domobj) {
                //shell_win.tencentmap.center = new qq.maps.LatLng(39.96116,116.3794);
                if (center) {
                    shell_win.tencentmap.center = center;
                }
                shell_win.tencentmap.map = new qq.maps.Map(domobj, {
                    center: shell_win.tencentmap.center,
                    zoom: 15,
                    disableDefaultUI: true    //禁止所有控件
                });
                shell_win.tencentmap.marker = new qq.maps.Marker({
                    position: shell_win.tencentmap.center,
                    map: shell_win.tencentmap.map
                });
                qq.maps.event.addListener(shell_win.tencentmap.marker, "click", function () {
                    alert("you clicked me1")
                });
            }
        },
        Backpage: function () {
                location.href = '../index.html';
        },
        /**初始化*/
        init: function () {
            Shell.util.weixin.init();
            shell_win.hidecontrol();

            $("#getlocal").on('click', Shell.util.weixin.getuserlocation);

            $("#aaa").on('click', shell_win.signin_action_btn_click);

            //签到按钮监听
            $("#signin_btn").on('click', shell_win.signin_btn_click);
            //签退按钮监听
            $("#signout_btn").on('click', shell_win.signout_btn_click);
            //上报位置按钮监听
            $("#uploadpostion").on('click', shell_win.uploadpostion_btn_click);
            //签到动作按钮监听
            $("#signin_action_btn").on('click', shell_win.signin_action_btn_click);
            //签退动作按钮监听
            $("#signout_action_btn").on('click', shell_win.signout_action_btn_click);
            //上报位置动作按钮监听
            $("#uploadpostion_action_btn").on('click', shell_win.uploadpostion_action_btn_click);
            //请假按钮监听
            $("#leave_btn").on('click', shell_win.leave_btn_click);
            //外出按钮监听
            $("#egress_btn").on('click', shell_win.egress_btn_click);
            //出差按钮监听
            $("#trip_btn").on('click', shell_win.trip_btn_click);
            //加班按钮监听
            $("#overtime_btn").on('click', shell_win.overtime_btn_click);

            //签到说明按钮监听
            $("#signin_memo_btn").on('click', shell_win.signin_btn_click);

            //我的打卡记录按钮监听
            $("#mysignlog").on('click', function () { location.href = "EmpATMySignLog.html" });
            //其它打卡记录按钮监听
            $("#othersignlog").on('click', function () { location.href = "EmpATOtherSignLog.html" });

            //我的申请记录按钮监听
            $("#myapplylog").on('click', function () { location.href = "EmpATMyApplyLog.html" });

            //我的审批记录按钮监听
            $("#otherapplylog").on('click', function () { location.href = "EmpATMyApprovaLog.html" });
            
            $("#cancelspan").on(Shell.util.Event.touch, shell_win.Backpage);
            //初始化系统
            shell_win.system.init();
            //获取签到签退状态信息
            shell_win.LoadSignInfo();
        }
    };
    shell_win.init();
});