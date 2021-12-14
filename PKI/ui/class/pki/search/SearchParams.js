/**
 * 查询表单处理
 * @author longfc
 * @version 2016-05-27
 */
Ext.define('Shell.class.pki.search.SearchParams', {
	extend: 'Ext.toolbar.Toolbar',
	alias: 'widget.uxSearchParams',
	/**
	 * @param {Boolean} params 最终的查询条件的数组
	 * @param {Boolean} comList 待查询的选择项
	 * @param {Boolean} comListType 待查询的选择项类型名称
	 * @param {Boolean} me 当前查询条件的表单对象
	 */
	getSearchParams: function(params, comList, comListType) {
		var me = this;
		if(params == null) {
			params = {};
		}
		switch(comListType) {
			case "textList":
				for(var i in comList) {
					var name = comList[i];
					var com = me.getComponent(name);
					if(com) {
						var v = com.getValue();
						if(v) {
							params[name] = v;
						}
					}
				}
				break;
			case "comboList":
				for(var i in comList) {
					var name = comList[i];
					var com = me.getComponent(name);
					if(com) {
						var v = com.getValue();
						if(v != null && v != '' && v !== 0) {
							params[name] = v;
						}
					}
				}
				break;
			case "booleanList":
				//booleanList这一类的选择项的值为部时值是null,不需要传参数值
				for(var i in comList) {
					var name = comList[i];
					var com = me.getComponent(name);
					if(com) {
						var v = com.getValue();
						if(typeof(v) == "boolean") {
							if(v == true) {
								params[name] = '1';
							} else {
								params[name] = '0';
							}
						}
					}
				}
				break;
			case "checkList":
				for(var i in comList) {
					var name = comList[i];
					var com = me.getComponent(name);
					if(com) {
						var v = com.getValue();
						if(v) {
							params[name] = v;
						}
					}
				}
				break;
			default:
				break;
		}
		return params;
	}

});