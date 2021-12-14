/**
 * 语言包
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.ux.Langage', {
	//extend:'Ext.Component',
	/**获取语言对象*/
	getLangage:function(className){
		var data = null;
		
		JShell.Msg.log('需要加载语言包的文件：' + className);
		if(JShell.System.Lang){
			var name = className.split('.');
			name[0] = name[0] + '.locale.' + JShell.System.Lang.toLowerCase();
			var lang = Ext.create(name.join('.'));
			data = lang.Langage;
		}
		
		return data;
	},
	/**替换语言*/
	changeLangage:function(className){
		var me = this;
		//默认中文语言
		if(!JShell.System.Lang || JShell.System.Lang.toLocaleUpperCase() == 'CN'){
			return;
		}
		me.changeLangageByClassName(me,className,0);
	},
	changeLangageByClassName:function(com,className,count,_proto){
		var obj = _proto || com;
		var proto = obj.__proto__ || com.__proto__;
		var name = proto.$className;
		
		if(count >50){
			return;
		}
		
		if(name == className){
			//支持语言替换
			var data = com.getLangage(name);
			if(data){
				var value = name.replace(/\./g,'_');
				com[value] = data;
			}
		}else{
			com.changeLangageByClassName(com,className,count++,proto);
		}
	}
});