/**
	@name：单选框配置项
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

    var radio = {
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
        me.config = $.extend({}, radio.config, me.config, options);
        me = $.extend(true, {}, radio, Class.pt, me);
        return me;
    };
    Class.pt = Class.prototype;
    //默认配置
    Class.pt.config = {

    };
    //核心入口
    radio.render = function (options, functionName) {
        var me = this;
        return Class.pt[functionName](options);
    };
    //获得html
    Class.pt.CreateHtml = function (obj) {
        var me = this,
            str = "",
            html = "";

        if (obj["URL"].trim()) {
            var url = uxutil.path.ROOT + obj["URL"].trim();
            uxutil.server.ajax({
                url: url,
                async: false
            }, function (data) {
                if (data && data.ResultDataValue) {
                    var list = JSON.parse(data.ResultDataValue).list || [];
                    //获得option项
                    $.each(list, function (i, itemI) {
                        str += '<input type="radio" class="' + (obj["ClassName"] ? obj["ClassName"] : "") + (String(obj["IsReadOnly"]) == "true" ? " layui-disabled" : "") +'" name="' + obj["MapField"] + '" lay-filter="' + obj["MapField"] + '" value="' + itemI[obj["ValueField"]] + '" title="' + itemI[obj["TextField"]] + '" style="' + (obj["StyleContent"] ? obj["StyleContent"] : "") + '" checked=' + (obj["DefaultValue"] == itemI[obj["ValueField"]]) + (String(obj["IsReadOnly"]) == "true" ? " disabled" : "") +  '>';
                    });
                } else {
                    layer.msg(data.msg);
                }
            });
        } else if (obj["DataJSON"]) {
            $.each(eval("(" + obj["DataJSON"] + ")"), function (i, itemI) {
                str += '<input type="radio" class="' + (obj["ClassName"] ? obj["ClassName"] : "") + (String(obj["IsReadOnly"]) == "true" ? " layui-disabled" : "") +'" name="' + obj["MapField"] + '" lay-filter="' + obj["MapField"] + '" value="' + itemI["value"] + '" title="' + itemI["text"] + '" style="' + (obj["StyleContent"] ? obj["StyleContent"] : "") + '" checked=' + (obj["DefaultValue"] == itemI["value"]) + (String(obj["IsReadOnly"]) == "true" ? " disabled" : "") +  '>';
            });
        } else {
            //不做处理
        }
        html = '<div class="layui-col-xs' + obj["Cols"] + (String(obj["IsDisplay"]) == "true" ? "" : " layui-hide") + '">' +
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
        var me = this;
        if (obj["DefaultValue"] != "" && obj["DefaultValue"] != "undefined" && obj["DefaultValue"] != null) {
            $("input[name='" + obj["MapField"] + "'][value='" + obj["DefaultValue"] + "']").attr("checked", true).click();
            form.render("radio"); //更新全部
        }
    };
    //用于监听事件等后续处理
    Class.pt.handle = function (obj) {
        var me = this;
        if (obj["CallBackFunc"] != "") eval("(" + obj["CallBackFunc"] + ")");
    };
    //暴露接口
    exports('radio', radio);
});
