/**
	@name：申请录入表单配置项
	@author：zhangda
	@version 2020-06-22
 */
layui.extend({
    uxutil: 'ux/util',
    textInput: 'ux/form/component/textInput',
    select: 'ux/form/component/select',
    radio: 'ux/form/component/radio',
    checkbox: 'ux/form/component/checkbox',
    dateInput: 'ux/form/component/dateInput',
    textArea: 'ux/form/component/textArea',
}).define(['form', 'uxutil', 'select', 'textInput', 'radio', 'checkbox', 'dateInput','textArea'], function (exports) {
    "use strict";

    var $ = layui.$,
        uxutil = layui.uxutil,
        textInput = layui.textInput,
        select = layui.select,
        radio = layui.radio,
        checkbox = layui.checkbox,
        dateInput = layui.dateInput,
        textArea = layui.textArea,
        form = layui.form;

    var formExtend = {
        //全局项
        config: {

        },
        //设置全局项
        set: function (options) {
            var me = this;
            me.config = $.extend({}, me.config, options);
            return me;
        }
    };
    //构造器
    var Class = function (options) {
        var me = this;
        me.config = $.extend({}, formExtend.config, me.config, options);
        me = $.extend(true, {}, formExtend, Class.pt, me);
        return me;
    };
    Class.pt = Class.prototype;
    //默认配置
    Class.pt.config = {
        ComponentType: {
            1001: {
                name: textInput,
                params: {}
            },//文本输入框
            1002: {
                name: dateInput, params: { type: 'year', range: false, format: 'yyyy' }
            },//日期框年类型
            1003: {
                name: dateInput, params: { type: 'year', range: true, format: 'yyyy' }
            },//日期框范围年类型
            1004: {
                name: dateInput, params: { type: 'month', range: false, format: 'yyyy-MM' }
            },//日期框年月类型
            1005: {
                name: dateInput, params: { type: 'month', range: true, format: 'yyyy-MM' }
            },//日期框范围年月类型
            1006: {
                name: dateInput, params: { type: 'date', range: false, format: 'yyyy-MM-dd' }
            },//日期选择器
            1007: {
                name: dateInput, params: { type: 'date', range: true, format: 'yyyy-MM-dd' }
            },//日期选择器范围
            1008: {
                name: dateInput, params: { type: 'time', range: false, format: 'HH:mm:ss' }
            },//时间选择器
            1009: {
                name: dateInput, params: { type: 'time', range: true, format: 'HH:mm:ss' }
            },//时间选择器范围
            1010: {
                name: dateInput, params: { type: 'datetime', range: false, format: 'yyyy-MM-dd HH:mm:ss' }
            },//日期时间选择器
            1011: {
                name: dateInput, params: { type: 'datetime', range: true, format: 'yyyy-MM-dd HH:mm:ss' }
            },//日期时间选择器范围
            1012: {
                name: textArea,
                params: {}
            },//文本域
            1013: {
                name: select,
                params: { type:'radio' }
            },//单选下拉框
            1014: {
                name: select,
                params: { type:'check' }
            },//复选下拉框
            1015: {
                name: radio,
                params: {}
            },//单选框
            1016: {
                name: checkbox,
                params: {}
            }//复选框
        }
    };
    
    //核心入口
    formExtend.render = function (options,functionName) {
        var me = this;
        var returnData = Class.pt.init(options, functionName);
        return returnData;
    };
    //创建组件
    Class.pt.init = function (obj, functionName) {
        var me = this,
            obj = $.extend({}, obj, me.config.ComponentType[obj["TypeID"]]["params"]);
        return me.config.ComponentType[obj["TypeID"]]["name"].render(obj, functionName);
    };
    //暴露接口
    exports('formExtend', formExtend);
});
