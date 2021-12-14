/**
    @name：公用基础方法
    @author：zhangda
    @version 2021-10-27
 */
layui.extend({
}).define(['uxutil'], function (exports) {
    "use strict";

    var $ = layui.$,
        uxutil = layui.uxutil;

    var MOD_NAME = 'uxbase';

    var APP = {};
    //弹出层
    APP.MSG = {
        success: { icon: 6, anim: 0, time: 2000 },
        error: { icon: 5, anim: 0, time: 5000, title: '<span style="color:red;">失败信息</span>', btn: ['关闭'] },
        warn: { icon: 0, anim: 0, time: 5000, title: '<span style="color:orange;">警告信息</span>', btn: ['关闭'] },
        //成功提示 msg:"提示内容",config:弹出层配置对象 { }
        onSuccess: function (msg, config) {
            var me = this,
                config = config || {},
                configs = $.extend({}, me.success, config);
            layer.msg(msg, configs);
        },
        //失败提示
        onError: function (msg, config) {
            var me = this,
                config = config || {},
                configs = $.extend({}, me.error, config);
            layer.msg(msg, configs);
        },
        //警告提示
        onWarn: function (msg, config) {
            var me = this,
                config = config || {},
                configs = $.extend({}, me.warn, config);
            layer.msg(msg, configs);
        },
        //确认提示框
        onConfirm: function (msg,config,callback1,callback2) {
            var me = this,
                config = config || {};
            if(config.enter){ //带回车事件
            	config.success = function(layero,index){
            		var yesButton = layero.find(".layui-layer-btn0"), //确定按钮
            			cancelButton = layero.find(".layui-layer-btn1"); //取消按钮
            		
            		layero.attr("tabindex","1");//div不能监听键盘事件，因此加上tabindex
            		layero.focus();//div获取焦点
            		layero.on("keydown",function(event){
            			if (event.keyCode == 13 || event.keyCode === 32){
            				if(cancelButton.attr("isChecked")){
            					cancelButton.click();
            				}else{
            					yesButton.click();
            				}
            			}else if(event.keyCode == 37){//左方向
            				if(yesButton.attr("isChecked")){
	        					yesButton.attr("isChecked","");
	        					cancelButton.attr("isChecked","1");
	        					yesButton.removeClass("focus-border");
	        					cancelButton.addClass("focus-border");
	        				}else{
	        					yesButton.attr("isChecked","1");
	        					cancelButton.attr("isChecked","");
	        					yesButton.addClass("focus-border");
	        					cancelButton.removeClass("focus-border");
	        				}
            			}else if(event.keyCode == 39){//右方向
            				if(cancelButton.attr("isChecked")){
	        					yesButton.attr("isChecked","1");
	        					cancelButton.attr("isChecked","");
	        					yesButton.addClass("focus-border");
	        					cancelButton.removeClass("focus-border");
	        				}else{
	        					yesButton.attr("isChecked","");
	        					cancelButton.attr("isChecked","1");
	        					yesButton.removeClass("focus-border");
	        					cancelButton.addClass("focus-border");
	        				}
            			}
            		});
//          		layero.on("click",function(){
//          			layero.focus();
//          		});
				};
            }
            var  configs = $.extend({},config);
            layer.confirm(msg, configs,
            	function(index){ //执行是
            	    callback1(index);
                },function(index){//执行否
                	callback2(index);
                }
            );
        }
    };
    //列表表单配置
    APP.CONFIGITEM = {
        config: {
            saveFormCount: 0,//表单保存总数
            saveFormSuccessCount: 0,//表单保存成功数量
            saveFormErrorCount: 0,//表单保存失败数量
            saveGridCount: 0,//列表保存总数
            saveGridSuccessCount: 0,//列表保存成功数量
            saveGridErrorCount: 0,//列表保存失败数量
            TableControl_Config: {},//表格类型枚举
            getFormSettingUrl: uxutil.path.ROOT + "/ServerWCF/ModuleConfigService.svc/SearchBModuleFormControlSetListByFormCode?isPlanish=true",//获得表单配置项
            getGridSettingUrl: uxutil.path.ROOT + "/ServerWCF/ModuleConfigService.svc/SearchBModuleGridControlListByGridCode?isPlanish=true",//获得列表配置项
            GetClassDicListUrl: uxutil.path.ROOT + '/ServerWCF/CommonService.svc/GetClassDicList' //获得枚举
        },
        //根据classnamespace和classname获得枚举
        getClassDicList: function (params, callBack) {
            var me = this;
            uxutil.server.ajax({
                type: "POST",
                url: me.config.GetClassDicListUrl,
                async: false,
                data: JSON.stringify({
                    'jsonpara': params
                })
            }, function (data) {
                if (data && data.ResultDataValue) {
                    var list = JSON.parse(data.ResultDataValue);
                    callBack(list);
                } else {
                    layer.msg(data.ErrorInfo);
                }
            });
        },
        //获得枚举的表格列头类型
        getTableHeadType: function () {
            var me = this;
            me.getClassDicList([{
                "classnamespace": "ZhiFang.Entity.Common",
                "classname": "TableControl_Config"
            }], function (list) {
                if (list.length > 0) {
                    $.each(list[0]["TableControl_Config"], function (i, item) {
                        me.config.TableControl_Config[item.Id] = {
                            name: item.Name,
                            code: item.Code
                        };
                    });
                }
            });
        },
        //单个获得表单配置
        getFormConfig: function (CodeListOne, indexs) {
            if (!CodeListOne) return;
            var me = this,
                url = me.config.getFormSettingUrl + "&FormCode=" + CodeListOne["Code"] + "&sort=[{'property': 'DispOrder','direction': 'ASC'}]";
            uxutil.server.ajax({
                url: url
            }, function (res) {
                if (res.success) {
                    me.config.saveFormSuccessCount++;
                    if (CodeListOne["IsClear"] != 'undefined' && String(CodeListOne["IsClear"]) == 'true') $(CodeListOne["Elem"]).html("");
                    if (res.ResultDataValue) {
                        var data = JSON.parse(res.ResultDataValue).list;
                        if (data.length > 0 && String(CodeListOne["NotHandle"]) != 'true') {
                            var html = "";
                            $.each(data, function (i, itemI) {
                                var objI = JSON.parse(JSON.stringify(itemI).replace(/BModuleFormControlList_/g, ""));
                                html += formExtend.render(objI, "CreateHtml");
                            });
                            $(CodeListOne["Elem"]).prepend(html);
                            $.each(data, function (j, itemJ) {
                                var objJ = JSON.parse(JSON.stringify(itemJ).replace(/BModuleFormControlList_/g, ""));
                                formExtend.render(objJ, "initData");
                            });
                            form.render();
                        }
                        if (CodeListOne["CallBack"] && typeof (CodeListOne["CallBack"]) == 'function') CodeListOne["CallBack"](data);
                    }
                } else {
                    me.config.saveFormErrorCount++;
                    layer.msg(res.ErrorInfo, {
                        icon: 5,
                        anim: 6
                    });
                }
                if (me.config.saveFormSuccessCount + me.config.saveFormErrorCount == me.config.saveFormCount) layer.close(indexs);
            });
        },
        //单个获得列表配置
        getGridConfig: function (CodeListOne, indexs) {
            if (!CodeListOne) return;
            var that = this,
                url = that.config.getGridSettingUrl + "&GridCode=" + CodeListOne["Code"] + "&sort=[{'property': 'DispOrder','direction': 'ASC'}]";
            if (JSON.stringify(that.config.TableControl_Config) == '{}') that.getTableHeadType();
            uxutil.server.ajax({
                url: url
            }, function (res) {
                if (res.success) {
                    that.config.saveGridSuccessCount++;
                    if (res.ResultDataValue) {
                        var data = eval('(' + res.ResultDataValue + ')').list;
                        if (data.length > 0) {
                            var cols = [],//列头
                                sort = [];//初始排序
                            $.each(data, function (i, item) {
                                //拼接列头
                                var obj = {};
                                if (item.BModuleGridControlList_TypeID) obj["type"] = that.config.TableControl_Config[item.BModuleGridControlList_TypeID]["code"];
                                if (item.BModuleGridControlList_MapField) obj["field"] = item.BModuleGridControlList_MapField;
                                if (item.BModuleGridControlList_ColName) obj["title"] = item.BModuleGridControlList_ColName;
                                if (item.BModuleGridControlList_Width) obj["width"] = Number(item.BModuleGridControlList_Width);
                                if (item.BModuleGridControlList_MinWidth) obj["minWidth"] = item.BModuleGridControlList_MinWidth;
                                if (item.BModuleGridControlList_Fixed) obj["fixed"] = item.BModuleGridControlList_Fixed;
                                if (item.BModuleGridControlList_IsHide) obj["hide"] = String(item.BModuleGridControlList_IsHide) == "true" ? true : false;
                                if (item.BModuleGridControlList_IsOrder) obj["sort"] = String(item.BModuleGridControlList_IsOrder) == "true" ? true : false;
                                if (item.BModuleGridControlList_Edit) obj["edit"] = item.BModuleGridControlList_Edit;
                                if (item.BModuleGridControlList_StyleContent) obj["style"] = item.BModuleGridControlList_StyleContent;
                                if (item.BModuleGridControlList_Align) obj["align"] = item.BModuleGridControlList_Align;
                                if (item.BModuleGridControlList_Toolbar) obj["toolbar"] = String(item.BModuleGridControlList_Toolbar) == "false" ? false : item.BModuleGridControlList_Toolbar;
                                if (item.BModuleGridControlList_ColData) {
                                    var str = "{" + item.BModuleGridControlList_ColData + "}";
                                    var colDataObj = eval("(" + str + ")");
                                    obj = $.extend({}, colDataObj, obj);
                                }
                                cols.push(obj);
                                //获得初始排序
                                if (String(item.BModuleGridControlList_IsOrder) == "true" && (item.BModuleGridControlList_OrderType.toUpperCase() == "ASC" || item.BModuleGridControlList_OrderType.toUpperCase() == "DESC")) {
                                    sort.push({
                                        "property": (item.BModuleGridControlList_MapField.indexOf("_") != -1 ? item.BModuleGridControlList_MapField.split("_")[1] : item.BModuleGridControlList_MapField),
                                        "direction": item.BModuleGridControlList_OrderType
                                    });
                                }
                            });
                            if (CodeListOne["CallBack"] && typeof (CodeListOne["CallBack"]) == 'function') CodeListOne["CallBack"](cols, sort);
                        }
                    } else {
                        layer.msg("未配置表格列头！");
                    }
                } else {
                    that.config.saveGridErrorCount++;
                    layer.msg(res.ErrorInfo, {
                        icon: 5,
                        anim: 6
                    });
                }
                if (that.config.saveGridSuccessCount + that.config.saveGridErrorCount == that.config.saveGridCount) layer.close(indexs);
            });
        },
        //获得表单配置//CodeList: [{ Code:'FormCode1',IsClear:true,NotHandle:false,Elem:'#FormID1',CallBack:function(){  } }]
        initFormConfig: function (CodeList) {
            if (CodeList && CodeList.length == 0) return;
            var me = this,
                indexs = layer.load();
            me.config.saveFormCount = CodeList.length;
            me.config.saveFormSuccessCount = 0;
            me.config.saveFormErrorCount = 0;
            $.each(CodeList, function (a, itemA) {
                me.getFormConfig(itemA, indexs);
            });
        },
        //获得列表配置//CodeList: [{ Code:'GridCode1',Elem:'#TableID1',CallBack:function(){  } }]
        initGridConfig: function (CodeList) {
            if (CodeList && CodeList.length == 0) return;
            var me = this,
                indexs = layer.load();
            me.config.saveGridCount = CodeList.length;
            me.config.saveGridSuccessCount = 0;
            me.config.saveGridErrorCount = 0;
            $.each(CodeList, function (a, itemA) {
                me.getGridConfig(itemA, indexs);
            });
        },
    };


    //暴露接口
    exports(MOD_NAME, APP);
});