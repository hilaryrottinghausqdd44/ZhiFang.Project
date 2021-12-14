var Shell = Shell || {};
Shell.util.weixin = Shell.util.weixin || {};
/**微信功能*/
Shell.util.weixin = {
    signature: null,
    timeout: null,
    timestamp: 1414587457,
    url: window.document.location.href.split("#")[0],
    nonceStr: "AAAA",
    error_no_signature: "没有获取到微信签名,请刷新页面",
    /**微信功能初始化*/
    init: function () {
        //获取微信签名
        Shell.util.weixin.get_signature(Shell.util.weixin.set_sonfig);
    },
    /**设置微信参数*/
    set_sonfig: function () {
        wx.config({
            debug: false, // 开启调试模式,调用的所有api的返回值会在客户端alert出来，若要查看传入的参数，可以在pc端打开，参数信息会通过log打出，仅在pc端时才会打印。
            appId: 'wxb10e2abd68b58220', // 必填，公众号的唯一标识
            timestamp: Shell.util.weixin.timestamp, // 必填，生成签名的时间戳
            nonceStr: Shell.util.weixin.nonceStr, // 必填，生成签名的随机串
            signature: Shell.util.weixin.signature, // 必填，签名，见附录1
            jsApiList: ['scanQRCode', 'openLocation', 'getLocation', 'chooseImage', 'uploadImage', 'previewImage', 'hideOptionMenu', 'showOptionMenu', 'hideToolbar', 'closeWindow', 'startRecord', 'stopRecord', 'onVoiceRecordEnd', 'translateVoice'] // 必填，需要使用的JS接口列表，所有JS接口列表见附录2
        });
        if (Shell.util.weixin.timeout && Shell.util.weixin.timeout > 0) {
            setTimeout(Shell.util.weixin.get_signature, Shell.util.weixin.timeout * 1000);
        }
    },
    /**获取微信签名*/
    get_signature: function (callback) {
        var call = Shell.util.typeOf(callback) === "function" ? [callback] :
            Shell.util.typeOf(callback) === "array" ? callback : [];
        ShellComponent.mask.to_server("微信签名获取中...");
        Shell.util.Server.ajax({
            url: Shell.util.Path.rootPath + "/WeiXinAppService.svc/GetJSAPISignature?" +
                "noncestr=" + Shell.util.weixin.nonceStr + "&timestamp=" + Shell.util.weixin.timestamp +
                "&url=" + Shell.util.weixin.url
        }, function (data) {
            ShellComponent.mask.hide();
            if (data.success) {
                Shell.util.weixin.signature = data.value.signature;
                Shell.util.weixin.timeout = data.value.TimeOut;
                for (var i in call) {
                    call[i]();
                }
            } else {
                ShellComponent.messagebox.msg(Shell.util.weixin.error_no_signature);
            }
        });
    },
    /**扫条码*/
    scan: function (callback) {
        if (Shell.util.weixin.signature) {
            wx.scanQRCode({
                needResult: 1, // 默认为0，扫描结果由微信处理，1则直接返回扫描结果，
                scanType: ["qrCode", "barCode"], // 可以指定扫二维码还是一维码，默认二者都有
                success: function (res) {
                    // 当needResult 为 1 时，扫码返回的结果
                    callback(res.resultStr);
                }
            });
        } else {
            Shell.util.weixin.get_signature([Shell.util.weixin.set_sonfig, function () {
                setTimeout(function () {
                    wx.scanQRCode({
                        needResult: 1, // 默认为0，扫描结果由微信处理，1则直接返回扫描结果，
                        scanType: ["qrCode", "barCode"], // 可以指定扫二维码还是一维码，默认二者都有
                        success: function (res) {
                            // 当needResult 为 1 时，扫码返回的结果
                            callback(res.resultStr);
                        }
                    });
                }, 300);
            }]);
        }
    },
    /**获取用户坐标*/
    getuserlocation: function (callback) {
        if (Shell.util.weixin.signature) {
            wx.getLocation({
                //type: 'wgs84', // 默认为wgs84的gps坐标，如果要返回直接给openLocation用的火星坐标，可传入'gcj02'
                type: 'gcj02',
                success: function (res) {
                    var latitude = res.latitude; // 纬度，浮点数，范围为90 ~ -90
                    var longitude = res.longitude; // 经度，浮点数，范围为180 ~ -180。
                    var speed = res.speed; // 速度，以米/每秒计
                    var accuracy = res.accuracy; // 位置精度
                    //alert( latitude);
                    //alert(' 纬度:'+latitude+'@@@经度'+longitude+'@@@速度'+speed+'@@@位置精度'+accuracy)
                    callback(latitude, longitude);
                }
            });
        } else {
            Shell.util.weixin.get_signature([Shell.util.weixin.set_sonfig, function () {
                setTimeout(function () {
                    wx.getLocation({
                        type: 'gcj02', // 默认为wgs84的gps坐标，如果要返回直接给openLocation用的火星坐标，可传入'gcj02'
                        success: function (res) {
                            var latitude = res.latitude; // 纬度，浮点数，范围为90 ~ -90
                            var longitude = res.longitude; // 经度，浮点数，范围为180 ~ -180。
                            var speed = res.speed; // 速度，以米/每秒计
                            var accuracy = res.accuracy; // 位置精度
                            //alert( latitude);
                            //alert(' 纬度:'+latitude+'@@@经度'+longitude+'@@@速度'+speed+'@@@位置精度'+accuracy)
                            callback(latitude, longitude);
                        }
                    });
                }, 300);
            }]);
        }

    },
    /**地图标注用户坐标*/
    setuserlocationmap: function (callback) {
        if (Shell.util.weixin.signature) {
            wx.openLocation({
                latitude: 39.93385, // 纬度，浮点数，范围为90 ~ -90
                longitude: 116.2696, // 经度，浮点数，范围为180 ~ -180。
                name: '123', // 位置名
                address: '123', // 地址详情说明
                scale: 5, // 地图缩放级别,整形值,范围从1~28。默认为最大
                infoUrl: '123123123' //,// 在查看位置界面底部显示的超链接,可点击跳转 					    
                ///success: function(res) {
                // 当needResult 为 1 时，扫码返回的结果
                //alert(res.address);
                //},
                // fail: function(res) {
                // 当needResult 为 1 时，扫码返回的结果
                //	alert(res.errMsg);
                //}
            });
        } else {
            Shell.util.weixin.get_signature([Shell.util.weixin.set_sonfig, function () {
                setTimeout(function () {
                    wx.openLocation({
                        latitude: 0, // 纬度，浮点数，范围为90 ~ -90
                        longitude: 0, // 经度，浮点数，范围为180 ~ -180。
                        name: '', // 位置名
                        address: '', // 地址详情说明
                        scale: 5, // 地图缩放级别,整形值,范围从1~28。默认为最大
                        infoUrl: '' //,// 在查看位置界面底部显示的超链接,可点击跳转 					    
                        ///success: function(res) {
                        // 当needResult 为 1 时，扫码返回的结果
                        //alert(res.address);
                        //},
                        // fail: function(res) {
                        // 当needResult 为 1 时，扫码返回的结果
                        //	alert(res.errMsg);
                        //}
                    });
                }, 300);
            }]);
        }
    },
    /**获取用户图像*/
    getchooseImage: function (callback) {
        if (Shell.util.weixin.signature) {
            wx.chooseImage({
                count: 1, // 默认9
                sizeType: ['original', 'compressed'], // 可以指定是原图还是压缩图，默认二者都有
                sourceType: ['album', 'camera'], // 可以指定来源是相册还是相机，默认二者都有
                success: function (res) {
                    var localIds = res.localIds; // 返回选定照片的本地ID列表，localId可以作为img标签的src属性显示图片
                    if (res.localIds && callback) {
                        callback(localIds);
                    }
                }
            });
        } else {
            Shell.util.weixin.get_signature([Shell.util.weixin.set_sonfig, function () {
                setTimeout(function () {
                    wx.chooseImage({
                        count: 1, // 默认9
                        sizeType: ['original', 'compressed'], // 可以指定是原图还是压缩图，默认二者都有
                        sourceType: ['album', 'camera'], // 可以指定来源是相册还是相机，默认二者都有
                        success: function (res) {
                            var localIds = res.localIds; // 返回选定照片的本地ID列表，localId可以作为img标签的src属性显示图片
                            if (res.localIds && callback) {
                                callback(localIds);
                            }
                        }
                    });
                }, 300);
            }]);
        }
    },
    /*预览图片接口*/
    previewImage: function (curl, urls) {
        if (Shell.util.weixin.signature) {
            wx.previewImage({
                current: curl, // 当前显示图片的http链接
                urls: urls // 需要预览的图片http链接列表
            });
        } else {
            Shell.util.weixin.get_signature([Shell.util.weixin.set_sonfig, function () {
                setTimeout(function () {
                    wx.previewImage({
                        current: curl, // 当前显示图片的http链接
                        urls: urls // 需要预览的图片http链接列表
                    });
                }, 300);
            }]);
        }
    },
    /**上传图片*/
    uploadImage: function (localId, index, callback) {
        if (Shell.util.weixin.signature) {
            wx.uploadImage({
                localId: localId, // 需要上传的图片的本地ID，由chooseImage接口获得
                isShowProgressTips: 1, // 默认为1，显示进度提示
                success: function (res) {
                    var serverId = res.serverId; // 返回图片的服务器端ID
                    if (serverId && callback) {
                        callback(serverId, index);
                    }
                }
            });
        } else {
            Shell.util.weixin.get_signature([Shell.util.weixin.set_sonfig, function () {
                setTimeout(function () {
                    wx.uploadImage({
                        localId: localId, // 需要上传的图片的本地ID，由chooseImage接口获得
                        isShowProgressTips: 1, // 默认为1，显示进度提示
                        success: function (res) {
                            var serverId = res.serverId; // 返回图片的服务器端ID
                            if (serverId && callback) {
                                callback(localIds);
                            }
                        }
                    });
                }, 300);
            }]);
        }
    },
    /**隐藏右上角菜单接口*/
    hideOptionMenu: function (callback) {
        if (Shell.util.weixin.signature) {
            wx.hideOptionMenu();
        } else {
            Shell.util.weixin.get_signature([Shell.util.weixin.set_sonfig, function () {
                setTimeout(function () {
                    wx.hideOptionMenu();
                }, 300);
            }]);
        }
    },
    /**显示右上角菜单接口*/
    showOptionMenu: function (callback) {
        if (Shell.util.weixin.signature) {
            wx.showOptionMenu();
        } else {
            Shell.util.weixin.get_signature([Shell.util.weixin.set_sonfig, function () {
                setTimeout(function () {
                    wx.showOptionMenu();
                }, 300);
            }]);
        }
    },
    /**隐藏所有非基础按钮接口*/
    hideAllNonBaseMenuItem: function (callback) {
        if (Shell.util.weixin.signature) {
            wx.hideAllNonBaseMenuItem();
        } else {
            Shell.util.weixin.get_signature([Shell.util.weixin.set_sonfig, function () {
                setTimeout(function () {
                    wx.hideAllNonBaseMenuItem();
                }, 300);
            }]);
        }
    },
    /**关闭当前网页窗口接口*/
    closeWindow: function (callback) {
        if (Shell.util.weixin.signature) {
            wx.closeWindow();
        }
        else {
            Shell.util.weixin.get_signature([Shell.util.weixin.set_sonfig, function () {
                setTimeout(function () {
                    wx.closeWindow();
                }, 300);
            }]);
        }
    },
    /*开始录音接口*/
    startRecord: function (callback) {
        if (Shell.util.weixin.signature) {
            wx.startRecord();
        }
        else {
            Shell.util.weixin.get_signature([Shell.util.weixin.set_sonfig, function () {
                setTimeout(function () {
                    wx.startRecord();
                }, 300);
            }]);
        }
    },
    /*停止录音接口*/
    stopRecord: function (callback) {
        if (Shell.util.weixin.signature) {
            wx.stopRecord({
                success: function (res) {
                    var localId = res.localId;
                    callback(localId);
                }
            });
        }
        else {
            Shell.util.weixin.get_signature([Shell.util.weixin.set_sonfig, function () {
                setTimeout(function () {
                    wx.stopRecord({
                        success: function (res) {
                            var localId = res.localId;
                            callback(localId);
                        }
                    });
                }, 300);
            }]);
        }
    },
    /*智能接口*/
    translateVoice: function (localid) {
        if (Shell.util.weixin.signature) {
            wx.translateVoice({
                localId: localid, // 需要识别的音频的本地Id，由录音相关接口获得
                isShowProgressTips: 1, // 默认为1，显示进度提示
                success: function (res) {
                    alert(res.translateResult); // 语音识别的结果
                }
            });
        }
        else {
            Shell.util.weixin.get_signature([Shell.util.weixin.set_sonfig, function () {
                setTimeout(function (localid) {
                    wx.translateVoice({
                        localId: '', // 需要识别的音频的本地Id，由录音相关接口获得
                        isShowProgressTips: 1, // 默认为1，显示进度提示
                        success: function (res) {
                            alert(res.translateResult); // 语音识别的结果
                        }
                    });
                }, 300);
            }]);
        }
    }
}