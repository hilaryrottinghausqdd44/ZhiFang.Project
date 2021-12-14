/**
	@name：日期框配置项
	@author：zhangda
	@version 2020-06-24
 */
layui.extend({
    //uxutil: 'ux/util',
}).define(['form','laydate', 'uxutil'], function (exports) {
    "use strict";

    var $ = layui.$,
        uxutil = layui.uxutil,
        laydate = layui.laydate,
        form = layui.form;

    var dateInput = {
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
        me.config = $.extend({}, dateInput.config, me.config, options);
        me = $.extend(true, {}, dateInput, Class.pt, me);
        return me;
    };
    Class.pt = Class.prototype;
    //默认配置
    Class.pt.config = {

    };
    //核心入口
    dateInput.render = function (options, functionName) {
        var me = this;
        return Class.pt[functionName](options);
    };
    //获得html
    Class.pt.CreateHtml = function (obj) {
        var me = this,
            html = '<div class="layui-col-xs' + obj["Cols"] + (String(obj["IsDisplay"]) == "true" ? "" : " layui-hide") + '">' +
                '<div class="layui-form-item"> ' +
                (obj["Label"] ? '<label class="layui-form-label">' + obj["Label"] + '</label>' : '') +
                '<div class="layui-input-block" style="' + (obj["Label"] ? "" : "margin-left:30px;") + '">' +
                '<input type="text" class="layui-input ' + (obj["ClassName"] ? obj["ClassName"] : "") + (String(obj["IsReadOnly"]) == "true" ? " layui-disabled" : "") + '" name="' + obj["MapField"] + '" id="' + obj["MapField"] + '" placeholder="' + (obj["placeholder"] ? obj["placeholder"] : "") + '" autocomplete="off" lay-verify="' + (obj["verify"] ? obj["verify"] : "") + '" style="' + (obj["StyleContent"] ? obj["StyleContent"] : "") +'"' + (String(obj["IsReadOnly"]) == "true" ? " disabled" : "") + '/>'+
                '</div>' +
                '</div >' +
                '</div >';
        return html;
    };
    //获得数据
    Class.pt.initData = function (obj) {
        var me = this;
        me.handle(obj);
        me.setValue(obj);
    };
    //赋值
    Class.pt.setValue = function (obj) {
        var me = this,
            data = obj["DefaultValue"],
            dateFormat = obj["format"] || 'yyyy-MM-dd HH:mm:ss',
            year = new Date().getFullYear(),
            month = new Date().getMonth() + 1,
            val = "";
        if (data == "") {

        } else if (!isNaN(data)) { // 只是数字  30
            val = uxutil.date.getNextDate(new Date(), data).format(dateFormat);
        } else if (data == "start") {//月初
            val = uxutil.date.getMonthFirstDate(year, month,true);
        } else if (data == "end") {//月末
            val = uxutil.date.getMonthLastDate(year, month, true);
        } else { //对象 { start:0,end:30 }
            try {
                data = eval("(" + data + ")");
                var startDate, endDate;
                if (obj["range"]) {
                    if (data["start"] != 'undefined' && data["end"] != 'undefined') {
                        if (!isNaN(data["start"]))
                            startDate = uxutil.date.getNextDate(new Date(), data["start"]).format(dateFormat);
                        else
                            startDate = uxutil.date.getMonthFirstDate(year, month, true);
                        if (!isNaN(data["end"]))
                            endDate = uxutil.date.getNextDate(new Date(), data["end"]).format(dateFormat);
                        else 
                            endDate = uxutil.date.getMonthLastDate(year, month, true);
                        val = startDate + " - " + endDate;
                        if (dateFormat == 'yyyy-MM-dd HH:mm:ss') {//日期时间范围
                            val = startDate.split(" ")[0] + " 00:00:00" + " - " + endDate.split(" ")[0] + " 23:59:59";
                        }
                    }
                    
                } else {
                    if (data["start"] != 'undefined') {
                        if (!isNaN(data["start"]))
                            val = uxutil.date.getNextDate(new Date(), data["start"]).format(dateFormat);
                        else
                            val = uxutil.date.getMonthFirstDate(year, month, true);
                    }
                }
            }
            catch (err) {
                val = data;
            }
        }
        laydate.render({
            elem: '#' + obj["MapField"],
            type: obj["type"] || 'datetime',
            format: obj["format"] || 'yyyy-MM-dd HH:mm:ss',
            range: obj["range"] || false,
            value: val
        });
    };
    //用于监听事件等后续处理
    Class.pt.handle = function (obj) {
        var me = this;
        if (obj["CallBackFunc"] != "") eval("(" + obj["CallBackFunc"] + ")");
    };
    //给Date原型加format方法
    Date.prototype.format = function (fmt) {
        var o = {
            "M+": this.getMonth() + 1,                 //月份 
            "d+": this.getDate(),                    //日 
            "H+": this.getHours(),                   //小时 
            "m+": this.getMinutes(),                 //分 
            "s+": this.getSeconds(),                 //秒 
            "q+": Math.floor((this.getMonth() + 3) / 3), //季度 
            "S": this.getMilliseconds()             //毫秒 
        };
        if (/(y+)/.test(fmt)) {
            fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
        }
        for (var k in o) {
            if (new RegExp("(" + k + ")").test(fmt)) {
                fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
            }
        }
        return fmt;
    }        
    //暴露接口
    exports('dateInput', dateInput);
});
