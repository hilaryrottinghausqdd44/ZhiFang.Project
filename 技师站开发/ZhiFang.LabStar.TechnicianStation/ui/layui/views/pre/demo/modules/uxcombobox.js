/**
	@name：简单下拉框
	@author：liangyl
	@version 
 */
layui.define('form',function(exports) {
	"use strict";
     var $ = layui.$,
        form =layui.form,
	    uxutil= layui.uxutil;
	var uxcombobox = {
	    /**@overwrite 返回数据处理方法*/
		changeResult:function(data){
			var value = data[JcallShell.Server.resultParams.value];
            if (value && typeof (value) === "string") {
                if (isNaN(value)) {
                    value = value.replace(/\\u000d\\u000a/g, '').replace(/\\u000a/g, '</br>').replace(/[\r\n]/g, '');
                    value = value.replace(/\\"/g, '&quot;');
                    value = value.replace(/\\/g, '\\\\');
                    value = value.replace(/&quot;/g, '\\"');
                    value = eval("(" + value + ")");
                } else {
                    value = value + "";
                }
            }
			return value;
		}
	};

	//暴露接口
	exports('uxcombobox', uxcombobox);
});
