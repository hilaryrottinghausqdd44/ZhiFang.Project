/**
 * 职责选择列表
 * @author liangyl	
 * @version 2017-11-23
 */
Ext.define('Shell.class.qms.equip.res.empduty.CheckGrid',{
    extend:'Shell.class.qms.equip.res.manage.basic.CheckGrid',
    title:'职责选择列表',
    width:450,
    height:350,
    
  	/**是否单选*/
	checkOne:true,
//	defaultWhere:'eresponsibility.IsUse=1',
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		var columns = me.callParent(arguments);
		columns.push({
			dataIndex: 'EResponsibility_SName',text: '简称',width: 100,defaultRenderer: true
		},{
			dataIndex: 'EResponsibility_Shortcode',text: '代码',width: 80,defaultRenderer: true
		},{
			dataIndex: 'EResponsibility_Comment',text: '描述',width: 180,editor:{},
			renderer: function(value, meta, record) {
            	var v=me.showMemoText(value, meta);
				return v;
			}
		},{
			dataIndex: 'EResponsibility_DispOrder',text: '次序',
			width: 70,align:'center',defaultRenderer: true
		});
		return columns;
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
	}
	
});