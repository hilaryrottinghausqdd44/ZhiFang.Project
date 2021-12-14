/**
 * 文档处理基础类
 * @author Jcall
 * @version 2017-02-12
 */
Ext.define('Shell.class.small.qms.file.file.ActionGrid',{
    extend: 'Shell.class.small.qms.file.basic.ShowGrid',
    
    /**默认树节点ID*/
	IDS:'',
	/**文档类型*/
	FTYPE:'1',
	/**处理环节字段*/
	ActionField:'',
	/**状态值*/
	ActionStatusValue:'',
	
	initComponent: function() {
		var me = this;
		
		//类型=1 and 状态=3
		//me.defaultWhere = 'ffile.Type=1 and ffile.Status in' + me.ActionStatusValue;
		me.defaultWhere = 'ffile.Type=1';
		
		var arr = me.ActionStatusValue.split(','),
			arrLen = arr.length,
			statusWhere = [];
			
		for(var i=0;i<arrLen;i++){
			statusWhere.push('ffile.Status=' + arr[i]);
		}
		if(statusWhere.length > 0){
			me.defaultWhere += ' and (' + statusWhere.join(' or ') + ')';
		}
		
		//审批人是登陆者
		var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;
		var userName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		
		me.defaultWhereChecker = "ffile.IsUse=1 and ffile." + me.ActionField + "=" + userId+"";
		
		if(userId && userName != JShell.System.ADMINNAME) {
			me.defaultWhere += " and " + me.defaultWhereChecker;
		}
		
		me.defaultWhere = "id=" + me.IDS + "^(" + me.defaultWhere + ")";
		
		me.callParent(arguments);
	}
});