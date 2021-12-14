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
		var me = this;
		var v = Ext.clone(value);
		if(!v || v == ''){return "";}
		
		var str = "";
		
		var type = Ext.typeOf(v);
		if(type === 'string'){
			var obj = Ext.JSON.decode(v);
			obj.panelParams.hasLab = true;
			str = me.getStringByObj(obj);
		}else{
			str = Ext.JSON.encode(v);
		}
		//var str = str ? str.replace(/\"/g,"'") : "";
		str = str.replace(/\\\"/g,"@@").replace(/\\\\\'/g,"\\'");
		return str;
	}
});