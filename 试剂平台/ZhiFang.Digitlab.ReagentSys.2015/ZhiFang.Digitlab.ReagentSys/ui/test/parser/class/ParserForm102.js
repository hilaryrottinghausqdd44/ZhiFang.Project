/**
 * 表单解析器
 * @version 1.0.2
 * @author Jcall
 */
Ext.ns('Ext.build');
Ext.define('Ext.build.ParserForm102',{
	extend:'Ext.build.ParserForm',
	/**解析器版本号*/
	version:'ParserForm 1.0.2',
	/**
	 * 将字符转化
	 * @private
	 * @param {} value
	 * @return {}
	 */
	changeString:function(value){
		var str = value;
		if(!str || str == ''){return "";}
		
		if(Ext.typeOf(value) === 'object'){
			if(value.formHtml){
				value.formHtml = value.formHtml.replace(/\'/g,"@@");
			}
			str = Ext.JSON.encode(value);
		}else{
			var v = "{panelParams:{";
			str = str.slice(0,v.length) + "hasLab:true," + str.slice(v.length);
		}
		//var str = str ? str.replace(/\"/g,"'") : "";
		str = str.replace(/\\\"/g,"@@").replace(/\\\\\'/g,"\\'");
		return str;
	}
});