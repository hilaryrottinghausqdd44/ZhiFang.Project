/**
	@name：下拉框配置项
	@author：zhangda
	@version 2020-06-24
 */
layui.extend({
    formSelects: 'ux/form_select/form_selects_v4.min'
}).define(['form', 'uxutil', 'formSelects'], function (exports) {
    "use strict";

    var $ = layui.$,
        uxutil = layui.uxutil,
        formSelects = layui.formSelects,
        form = layui.form;

    var select = {
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
        me.config = $.extend({}, select.config, me.config, options);
        me = $.extend(true, {}, select, Class.pt, me);
        return me;
    };
    Class.pt = Class.prototype;
    //默认配置
    Class.pt.config = {

    };
    //核心入口
    select.render = function (options,functionName) {
        var me = this;
        return Class.pt[functionName](options);
    };
    //获得html
    Class.pt.CreateHtml = function (obj) {
        var me = this,
            html = "";
        if (formSelects && obj["type"] == 'check') {
            var isRadio = true;
            if (obj["type"] == 'check') isRadio = false;
            html = '<div class="layui-col-xs' + obj["Cols"] + (String(obj["IsDisplay"]) == "true" ? "" : " layui-hide") + '">' +
                '<div class="layui-form-item"> ' +
                (obj["Label"] ? '<label class="layui-form-label">' + obj["Label"] + '</label>' : '') +
                '<div class="layui-input-block" style="' + (obj["Label"] ? "" : "margin-left:30px;") + '">' +
                '<select class="' + (obj["ClassName"] ? obj["ClassName"] : "") + (String(obj["IsReadOnly"]) == "true" ? " layui-disabled" : "") + '" name="' + obj["MapField"] + '" lay-filter="' + obj["MapField"] + '" id="' + obj["MapField"] + '" style="' + (obj["StyleContent"] ? obj["StyleContent"] : "") + '" xm-select="' + obj["MapField"] + '" xm-select-height="36px" xm-select-search="" xm-select-show-count="2" xm-select-search-type="dl"' + (isRadio ? " xm-select-radio" : "") + (String(obj["IsReadOnly"]) == "true" ? " disabled" : "") + '></select >' +
                '</div>' +
                '</div >' +
                '</div >';
        } else {
            html = '<div class="layui-col-xs' + obj["Cols"] + (String(obj["IsDisplay"]) == "true" ? "" : " layui-hide") + '">' +
                '<div class="layui-form-item"> ' +
                (obj["Label"] ? '<label class="layui-form-label">' + obj["Label"] + '</label>':'') +
                '<div class="layui-input-block" style="' + (obj["Label"] ? "" : "margin-left:30px;")+'">' +
                '<select class="' + (obj["ClassName"] ? obj["ClassName"] : "") + (String(obj["IsReadOnly"]) == "true" ? " layui-disabled" : "") +'" name="' + obj["MapField"] + '" lay-filter="' + obj["MapField"] + '" id="' + obj["MapField"] + '" style="' + (obj["StyleContent"] ? obj["StyleContent"] : "") + '" lay-search="" ' + (String(obj["IsReadOnly"]) == "true" ? " disabled" : "")+'></select >' +
                '</div>' +
                '</div >' +
                '</div >';
        }
        return html;
    };
    //获得下拉框数据
    Class.pt.initData = function (obj) {
        var me = this;
        if (!obj["MapField"]) return;
        if (obj["URL"].trim()) {
            var url = uxutil.path.ROOT + obj["URL"].trim();
            uxutil.server.ajax({
                url: url
            }, function (data) {
                if (data && data.ResultDataValue) {
                    var list = JSON.parse(data.ResultDataValue).list || [];
                    //判断是否存在空值
                    if (String(obj["IsHasNull"]) == "true") {
                        var str = '<option value="">请选择</option>';
                    } else {
                        var str = "";
                    }
                    //获得option项
                    $.each(list, function (i, itemI) {
                        str += '<option value="' + itemI[obj["ValueField"]] + '">' + itemI[obj["TextField"]] + '</option>';
                    });
                    $("#" + obj["MapField"]).html(str);
                    me.handle(obj);
                    me.setValue(obj);
                } else {
                    layer.msg(data.msg);
                }
            });
        } else if (obj["DataJSON"]) {
            var str = "",
                isHasNullValue = false;
            $.each(eval("(" + obj["DataJSON"] + ")"), function (i, itemI) {
                if (itemI["value"] == "") isHasNullValue = true;
                str += '<option value="' + itemI["value"] + '">' + itemI["text"] + '</option>';
            });
            //判断是否存在空值
            if (String(obj["IsHasNull"]) == "true" && !isHasNullValue) {
                str = '<option value="">请选择</option>' + str;
            }
            $("#" + obj["MapField"]).html(str);
            me.handle(obj);
            me.setValue(obj);
        }else {
            //不做处理
            me.handle(obj);
        }
    };
    //下拉框赋值
    Class.pt.setValue = function (obj) {
        var me = this;
        if (formSelects && obj["type"] == 'check') {
            formSelects.render(obj["MapField"]);//render
            if (obj["DefaultValue"] != "" && obj["DefaultValue"] != "undefined" && obj["DefaultValue"] != null) formSelects.value(obj["MapField"], [obj["DefaultValue"]]);//选中
        } else {
            if (obj["DefaultValue"] != "" && obj["DefaultValue"] != "undefined" && obj["DefaultValue"] != null) $("#" + obj["MapField"]).val(obj["DefaultValue"]);//选中
            form.render('select');//render
        }
    };
    //用于监听事件等后续处理
    Class.pt.handle = function (obj) {
        var me = this;
        if (obj["CallBackFunc"] != "") eval("(" + obj["CallBackFunc"] + ")");
    };
    //暴露接口
    exports('select', select);
});
