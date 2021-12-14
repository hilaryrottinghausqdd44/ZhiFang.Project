/**
	@name：复选框配置项
	@author：zhangda
	@version 2020-06-24
 */
layui.extend({
    //uxutil: 'ux/util',
}).define(['form', 'uxutil'], function (exports) {
    "use strict";

    var $ = layui.$,
        uxutil = layui.uxutil,
        form = layui.form;

    var checkbox = {
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
        me.config = $.extend({}, checkbox.config, me.config, options);
        me = $.extend(true, {}, checkbox, Class.pt, me);
        return me;
    };
    Class.pt = Class.prototype;
    //默认配置
    Class.pt.config = {

    };
    //核心入口
    checkbox.render = function (options, functionName) {
        var me = this;
        return Class.pt[functionName](options);
    };
    //获得html
    Class.pt.CreateHtml = function (obj) {
        var me = this,
            str = "",
            html = "",
            varArr = [],
            val = String(obj["DefaultValue"]);
        if (val.indexOf(",") != -1) {
            varArr = val.split(",");
        } else if (val != "") {
            varArr.push(val);
        }
        //组件html内容
        if (obj["DataJSON"]) {
            $.each(eval("(" + obj["DataJSON"] + ")"), function (i, itemI) {
                var isCheck = false;
                if (varArr.length > 0 && varArr[i]) {
                    if (String(varArr[i]) != "" && String(varArr[i]) != "0" && String(varArr[i]) != "false" && String(varArr[i]) != "null" && String(varArr[i]) != "undefined") {
                        isCheck = true;
                    }
                }
                str += '<input type="checkbox" class="' + (obj["ClassName"] ? obj["ClassName"] : "") + (String(obj["IsReadOnly"]) == "true" ? " layui-disabled" : "") + '" name="' + (itemI["name"] ? itemI["name"] : "") + '" lay-filter="' + obj["MapField"] + '" value="' + (itemI["value"] ? itemI["value"] : "") + '" title="' + (itemI["text"] ? itemI["text"] : "") + '" style="' + (obj["StyleContent"] ? obj["StyleContent"] : "") + '" lay-skin="primary" ' + (isCheck ? "checked" : "") + (String(obj["IsReadOnly"]) == "true" ? " disabled" : "" ) +'>';
            });
        } else {
            var isCheck = false;
            if (varArr.length > 0) {
                if (String(varArr[0]) != "" && String(varArr[0]) != "0" && String(varArr[0]) != "false" && String(varArr[0]) != "null" && String(varArr[0]) != "undefined") {
                    isCheck = true;
                }
            }
            str = '<input type="checkbox" class="' + (obj["ClassName"] ? obj["ClassName"] : "") + (String(obj["IsReadOnly"]) == "true" ? " layui-disabled" : "") + '" name="' + obj["MapField"] + '" id="' + obj["MapField"] + '" lay-filter="' + obj["MapField"] + '" style="' + (obj["StyleContent"] ? obj["StyleContent"] : "") + '" lay-skin="primary" ' + (isCheck ? "checked" : "") + (String(obj["IsReadOnly"]) == "true" ? " disabled" : "") + '>';
        }

        //组件容器html内容
        html = '<div class="layui-col-xs' + obj["Cols"] + (String(obj["IsDisplay"]) == "true" ? "" : " layui-hide") +'">' +
            '<div class="layui-form-item"> ' +
            (obj["Label"] ? '<label class="layui-form-label">' + obj["Label"] + '</label>' : '') +
            '<div class="layui-input-block" style="' + (obj["Label"] ? "" : "margin-left:30px;") + '">' + str +
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
            isCheck = false;
        //if (String(obj["DefaultValue"]) != "" && String(obj["DefaultValue"]) != "0" && String(obj["DefaultValue"]) != "false" && String(obj["DefaultValue"]) != "null" && String(obj["DefaultValue"]) != "undefined") {
        //    isCheck = true;
        //}
        //if (obj["MapField"]) $("#" + obj["MapField"]).attr("checked", isCheck);
        form.render("checkbox"); //更新全部
    };
    //用于监听事件等后续处理
    Class.pt.handle = function (obj) {
        var me = this;
        if (obj["CallBackFunc"] != "") eval("(" + obj["CallBackFunc"] + ")");
    };
    //暴露接口
    exports('checkbox', checkbox);
});