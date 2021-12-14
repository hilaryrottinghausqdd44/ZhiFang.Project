/**
 * 所有的职责信息
 * @author liangyl
 * @version 2017-11-23
 */
Ext.define('Shell.class.qms.equip.res.dutyemp.DutyGrid', {
	extend: 'Shell.class.qms.equip.res.manage.basic.Grid',
	title: '职责列表',
   
	/**默认加载数据*/
	defaultLoad: false,
	/**是否启用查询框*/
	hasSearch: true,


	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		var columns = me.callParent(arguments);
		columns.push({
			dataIndex: 'EResponsibility_SName',
			text: '简称',width: 100,defaultRenderer: true
		},{
			dataIndex: 'EResponsibility_EName',text: '英文名称',width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'EResponsibility_Comment',
			text: '描述',width: 180,sortable:false,
			renderer: function(value, meta, record) {
            	var v=me.showMemoText(value, meta);
				return v;
			}
		},{
			dataIndex: 'EResponsibility_DispOrder',text: '次序',
			width: 100,align:'center',type:'int',	defaultRenderer: true
		});
		return columns;
	},
	changeDefaultWhere:function(){
		var me=this;
		//defaultWhere追加上IsUse约束
		if(me.defaultWhere){
			var index = me.defaultWhere.indexOf('eresponsibility.IsUse=1');
			if(index == -1){
				me.defaultWhere += ' and eresponsibility.IsUse=1';
			}
		}else{
			me.defaultWhere = 'eresponsibility.IsUse=1';
		}
	},
	 /**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			search = null,params = [];
			
		//改变默认条件
		me.changeDefaultWhere();
			
		me.internalWhere = '';
			
		if(buttonsToolbar){
			search = buttonsToolbar.getComponent('search').getValue();
		}
		
		if(params.length > 0) {
			me.internalWhere = params.join(' and ');
		} else {
			me.internalWhere = '';
		}
		if(search) {
			if(me.internalWhere) {
				me.internalWhere += ' and (' + me.getSearchWhere(search) + ')';
			} else {
				me.internalWhere = me.getSearchWhere(search);
			}
		}
		return me.callParent(arguments);
	},
	showMemoText:function(value, meta){
		var me=this	;
        var val=value.replace(/(^\s*)|(\s*$)/g, ""); 	
		val = val.replace(/\\r\\n/g, "<br />");
        val = val.replace(/\\n/g, "<br />");
		var v = "" + value;
		var index1=v.indexOf("</br>");
		if(index1>0)v=v.substring(0,index1);
		if(v.length > 0)v = (v.length > 32 ? v.substring(0, 32) : v);
		if(value.length>32){
			v= v+"...";
		}
        var qtipValue = "<p border=0 style='vertical-align:top;font-size:12px; word-break:break-all;'>" + value + "</p>";
        meta.tdAttr = 'data-qtip="' + qtipValue + '"';
        return v
	}
});