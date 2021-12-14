/**
	@name：文本域配置项
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

    var textArea = {
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
        me.config = $.extend({}, textArea.config, me.config, options);
        me = $.extend(true, {}, textArea, Class.pt, me);
        return me;
    };
    Class.pt = Class.prototype;
    //默认配置
    Class.pt.config = {

    };
    //核心入口
    textArea.render = function (options, functionName) {
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
                '<textarea class="layui-textarea ' + (obj["ClassName"] ? obj["ClassName"] : "") + (String(obj["IsReadOnly"]) == "true" ? " layui-disabled" : "") + '" name="' + obj["MapField"] + '" id="' + obj["MapField"] + '" placeholder="' + (obj["placeholder"] ? obj["placeholder"] : "") + '" autocomplete="off" style="' + (obj["StyleContent"] ? obj["StyleContent"] : "") + '" lay-verify="' + (obj["verify"] ? obj["verify"] : "") +'"'+ (String(obj["IsReadOnly"]) == "true" ? " disabled" : "") + '></textarea>'+
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
        if (obj["DefaultValue"] != "" && obj["DefaultValue"] != "undefined" && obj["DefaultValue"] != null) $("#" + obj["MapField"]).val(obj["DefaultValue"]);
    };
    //用于监听事件等后续处理
    Class.pt.handle = function (obj) {
        var me = this;
        if (obj["CallBackFunc"] != "") eval("(" + obj["CallBackFunc"] + ")");
    };
    //暴露接口
    exports('textArea', textArea);
});
